using EcommerseApplication.Data;
using EcommerseApplication.DTO;
using EcommerseApplication.Models;
using EcommerseApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
//using System.Linq;
//using System.Net.Mail;
using System.Security.Claims;

namespace EcommerseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyController : ControllerBase
    {
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Found Successfuly";

        private readonly IShopping_SessionRepository shopping_SessionRrpo;
        private readonly ICart_ItemRepository cart_ItemRepo;
        private readonly IOrder_DetailsRepository order_DetailsRepo;
        private readonly IOrder_ItemsRepository order_ItemsRepo;
        private readonly IUserPayement userPayementRepo;
        private readonly IPayment_DetailsRepository payment_DetailsRepo;
        private readonly IUserAddress userAddress;
        private readonly IshippingDetails shippingDetailsRepo;
        private readonly IProductRepository productRepo;
        private readonly IOptions<StripeSettings> configuration;
        private readonly Ishipper shipperRepo;
        private readonly IUserRepository userRepo;

        //IConfiguration _configuration
        public BuyController(IShopping_SessionRepository _shopping_SessionRrpo,
                             ICart_ItemRepository _cart_ItemRepo, IOrder_DetailsRepository _order_DetailsRepo,
                             IOrder_ItemsRepository _order_ItemsRepo, IUserPayement _userPayementRepo,
                             IPayment_DetailsRepository _payment_DetailsRepo, IUserRepository _userRepo,
                             IUserAddress _userAddress, IshippingDetails _shippingDetailsRepo,
                             IProductRepository _productRepo, IOptions<StripeSettings> _configuration,
                              Ishipper _shipperRepo)
        {
            shopping_SessionRrpo = _shopping_SessionRrpo;
            cart_ItemRepo = _cart_ItemRepo;
            order_DetailsRepo = _order_DetailsRepo;
            order_ItemsRepo = _order_ItemsRepo;
            userPayementRepo = _userPayementRepo;
            payment_DetailsRepo = _payment_DetailsRepo;
            userAddress = _userAddress;
            shippingDetailsRepo = _shippingDetailsRepo;
            productRepo = _productRepo;
            configuration = _configuration;
            shipperRepo = _shipperRepo;
            userRepo = _userRepo;
        }


        [HttpPost("BuyNow")]
        public IActionResult BuyProducts(BuyDTO buyDTO)
        {
            try
            {
                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return BadRequest(new { Success = false, Message = "You Must Login First" });
                int UserID = user.Id;
                //int UserID = 5;
                int PaymentID = buyDTO.PaymentID;
                int ShipperID = buyDTO.ShipperID;
                int AddressID = buyDTO.AddressID;

                User_address userAddresssCheck = userAddress.GetAddress(AddressID);
                if (userAddresssCheck == null)
                    return BadRequest(new { Success = false, Message = "You Must Add Address First" });

                User_Payement payment_DetailsCheck = userPayementRepo.GetUserPayment(PaymentID);
                if (payment_DetailsCheck == null)
                    return BadRequest(new { Success = false, Message = "You Must Add Payment Method First" });

                Shipper shipperCheck = shipperRepo.getByID(ShipperID);
                if (shipperCheck == null)
                    return BadRequest(new { Success = false, Message = "There Is No Available Shipper Now" });

                int Shopping_SessionID;
                double TotalPrice = 0;
                List<Shopping_Session> UserSessions = shopping_SessionRrpo.GetByUserId(UserID);
                if (UserSessions.Count == 0 || UserSessions == null)
                {
                    return BadRequest(new { Success = false, Message = BadRequistMSG });
                }
                else { Shopping_SessionID = UserSessions[UserSessions.Count - 1].Id; }

                List<Cart_Item> cart_Items = cart_ItemRepo.GetAllBySessionID(Shopping_SessionID);

                if (cart_Items.Count == 0 || cart_Items == null)
                {
                    return NotFound(new { Success = false, Message = "No Items In Cart" });
                }

                foreach (var item in cart_Items)
                {
                    if (item.Quantity <= item.product.Product_Inventory.Quantity)
                    {
                        double PriceDiscont = 0;
                        if (item.product.Discount != null )
                            PriceDiscont = item.product.Price * (double)item.product.Discount.Descount_Persent;
                        //if (item.product.Discount.Descount_Persent > 1)
                            //PriceDiscont = item.product.Price * (double)item.product.Discount.Descount_Persent / 100;
                        TotalPrice += (item.product.Price - PriceDiscont) * item.Quantity;
                    }
                    else
                    {
                        return BadRequest(new { Success = false, Message = $"{item.product.Name} Quantity Is Not Enough" });
                    }
                }
                //Payment Chech
                CheckoutDTO checkoutDTO = new CheckoutDTO
                {
                    Amount = (decimal)TotalPrice,
                    PaymentID = PaymentID,
                };
                PaymentStatus PaymentStatus = Checkout(checkoutDTO);  //User User
                //bool PaymentStutus = true;

                if (PaymentStatus.Success)
                {
                    //Order Details
                    User_Payement user_Payement = userPayementRepo.GetUserPayment(PaymentID);

                    Order_Details newOrder = new Order_Details();
                    newOrder.Total = (int)PaymentStatus.Amount;
                    newOrder.UserID = UserID;

                    Payment_Details newPayment = new Payment_Details();
                    newPayment.Provider = user_Payement.Provider;
                    newPayment.Amount = (int)PaymentStatus.Amount;
                    newPayment.TransactionID = PaymentStatus.BalanceTransaction;
                    payment_DetailsRepo.AddPayment_Details(newPayment);

                    newOrder.Payment_ID = newPayment.ID;
                    order_DetailsRepo.Create(newOrder);

                    //Order Items
                    Order_Items Neworder_Item;
                    for (int i = 0; i < cart_Items.Count; i++)
                    {
                        Neworder_Item = new Order_Items();

                        Neworder_Item.OrderID = newOrder.Id;
                        Neworder_Item.ProductID = cart_Items[i].ProductId;
                        Neworder_Item.Quantity = cart_Items[i].Quantity;

                        order_ItemsRepo.Create(Neworder_Item);
                    }

                    for (int i = 0; i < cart_Items.Count; i++)
                    {
                        //Reduce Quantity From Order
                        productRepo.ReduseQuantity(cart_Items[i].ProductId, cart_Items[i].Quantity);
                    }

                    for (int i = cart_Items.Count - 1; i >= 0; i--)
                    {
                        //Remove Orders from Cart_Item
                        cart_ItemRepo.DeleteCart_Item(cart_Items[i]);
                    }


                    shopping_SessionRrpo.ClearTotal(Shopping_SessionID);
                    //Add Shipping Details

                    //User user = userRepo.GetUserById(UserID);
                    User_address userAddresss = userAddress.GetAddress(AddressID);
                    shippingDetails shippingDetails = new shippingDetails();
                    shippingDetails.shipperID = ShipperID;
                    shippingDetails.shipName = user.FirstName + user.LastName;
                    shippingDetails.userID = UserID;
                    shippingDetails.addressID = AddressID;
                    shippingDetails.orderDetailsID = newOrder.Id;
                    shippingDetails.shippingstate = "Pick Up";
                    shippingDetails.arabicshippingstate = "استلام شركة الشحن";
                    shippingDetails.ALLaddress = userAddresss.Country + " - " + userAddresss.City + " - " + userAddresss.PostalCode;
                    shippingDetails.ALLaddress_Ar = userAddresss.arabicCountry + " - " + userAddresss.arabicCity + " - " + userAddresss.PostalCode;
                    shippingDetails.CustomerMobile = userAddresss.mobile;

                    shippingDetailsRepo.insert(shippingDetails);
                    return Ok(new { Success = true, Message = "Order Purched Successfuly",Amount = PaymentStatus.Amount,
                                    TransactionID = PaymentStatus.BalanceTransaction,ShippingID = shippingDetails.ID ,OrderID = newOrder.Id});
                }
                else { return BadRequest(new { Success = false, Message = PaymentStatus.Message }); }
            }
            catch (Exception ex)
            {

                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }


        //user / 
        //[HttpPost("chechout")]
        private PaymentStatus Checkout(CheckoutDTO checkoutDTO)  //User User
        //private bool Checkout(CheckoutDTO checkoutDTO)
        {
            try
            {
                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                //User user = userRepo.GetUserById(5);

                if (!ModelState.IsValid)
                    return new PaymentStatus { Success = false, Message = "Invalid Credit Card Details" }; //Transation

                if (checkoutDTO.Currency != "USD" || checkoutDTO.Currency != "EGP")
                    checkoutDTO.Currency = "USD";

                var customerService = new CustomerService();
                //string sourceID;
                string PaymentTokenID;
                //string CardTokenID;
                User_Payement PaymentMythod = user.User_Pyment.FirstOrDefault(p => p.Id == checkoutDTO.PaymentID);
                if (PaymentMythod == null)
                    return new PaymentStatus { Success = false, Message = "Invalid Credit Card Details" };

                //if (PaymentMythod.StripePaymentToken == null || PaymentMythod.StripePaymentToken == String.Empty)
                //{
                Token token = CreatePaymentStripe(PaymentMythod);
                PaymentTokenID = token.Id;
                ///CardTokenID = token.Card.Id;
                ///
                #region y
                //if (user.StripeTokenID != null && user.StripeTokenID != String.Empty)
                //{
                //    //remove & Add
                //    CustomerUpdateOptions customerUpdate = new CustomerUpdateOptions { Source = PaymentTokenID };
                //    customerService.Update(user.StripeTokenID, customerUpdate);

                //    //attach
                //    //var newSource = new SourceAttachOptions { Source = PaymentTokenID };
                //    //var sourceService = new SourceService();
                //    //Source source = sourceService.Attach(user.StripeTokenID, newSource);
                //    ////sourceID = source.Id;
                //    //var DefaultSourceOptions = new CustomerUpdateOptions
                //    //{
                //    //    DefaultSource = source.Id
                //    //};
                //    //customerService.Update(user.StripeTokenID, DefaultSourceOptions);

                //}
                //else { }//customer Not Created case
                //}
                //else 
                //{
                //    PaymentTokenID = PaymentMythod.StripePaymentToken;
                //    CustomerUpdateOptions customerUpdate = new CustomerUpdateOptions { Source = PaymentTokenID };
                //    customerService.Update(user.StripeTokenID, customerUpdate);

                //    //if(user.StripeTokenID == null || user.StripeTokenID == String.Empty)
                //    //    CreateCustomerStripe(user, PaymentTokenID);
                //    //Customer customerCheck = customerService.Get(user.StripeTokenID);
                //    ////sourceID = customerCheck.DefaultSource.Id;
                //    ////string OldSource = customerCheck.Sources.FirstOrDefault(e => e.Id == PaymentTokenID).Id;
                //    //var DefaultSourceOptions = new CustomerUpdateOptions
                //    //{
                //    //    DefaultSource = PaymentTokenID
                //    //};
                //    //customerService.Update(user.StripeTokenID, DefaultSourceOptions);
                //}
                #endregion


                string CustomerTokenID;
                //if (checkoutDTO.CustomerTokenID == null || checkoutDTO.CustomerTokenID == String.Empty)
                if (user.StripeTokenID == null || user.StripeTokenID == String.Empty)
                {
                    Customer customer = CreateCustomerStripe(user, PaymentTokenID);
                    if (customer == null)
                        return new PaymentStatus { Success = false, Message = "Invald Credit Card Or Customer Info" };

                    CustomerTokenID = customer.Id;
                }
                else
                {
                    CustomerTokenID = user.StripeTokenID;
                    CustomerUpdateOptions customerUpdate = new CustomerUpdateOptions { Source = PaymentTokenID };
                    customerService.Update(user.StripeTokenID, customerUpdate);
                }

                //Customer customer2 = customerService.Get("cus_MESX0zyi34eYXw");
                var optionsCharge = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt64(checkoutDTO.Amount * 100),   // 50.1 $  * 100 => 5010 cint
                    Currency = checkoutDTO.Currency,
                    Description = "Buying From Ecommers WebSite",
                    ReceiptEmail = user.Identity.Email,
                    Customer = CustomerTokenID,
                    //Source = sourceID,
                    
                };
                var ChargeService = new ChargeService();
                Charge charge = ChargeService.Create(optionsCharge);
                if (charge.Status == "succeeded")
                {
                    //List<string> st = new List<string>();
                    //st.Add(configuration.GetSection("Stripe").GetSection("PublishableKey").Value);
                    //st.Add(configuration.GetValue<string>("Stripe:SecretKey"));
                    //st.Add(configuration.Value.PublishableKey);
                    //st.Add(configuration.Value.SecretKey);
                    return new PaymentStatus
                    {
                        Success = true,
                        Amount = charge.Amount/100,
                        Message = charge.Status,
                        BalanceTransaction = charge.BalanceTransactionId,
                    };
                }
                else
                {
                    return new PaymentStatus { Success = false, Message = "Error Occurred , " + charge.FailureMessage };
                }
            }
            catch (Exception ex)
            {
                return new PaymentStatus { Success = false, Message = ex.Message };
            }
        }


        private Token CreatePaymentStripe(User_Payement User_Payement)
        {
            try
            {
                if (!ModelState.IsValid)
                    return null;

                var token = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Name = User_Payement.HolderName,
                        Number = User_Payement.CardNumber,
                        Cvc = User_Payement.Cvc,
                        ExpMonth = User_Payement.ExpMonth,
                        ExpYear = User_Payement.ExpYear,
                    },
                };
                var tokenService = new TokenService();
                Token CardToken = tokenService.Create(token);

                if (CardToken != null)
                {
                    userPayementRepo.SetPaymentTokenID(User_Payement.Id, CardToken.Id);
                    return CardToken;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //private AnyOf<Customer,bool> CreateCustomerWithCard(CheckoutDTO checkoutDTO)
        private Customer CreateCustomerStripe(User User, string PaymentTokenID)
        {
            try
            {
                if (!ModelState.IsValid)
                    return null;


                var customerOpttions = new CustomerCreateOptions
                {
                    Email = User.Identity.Email,
                    Name = User.FirstName + User.LastName,
                    Phone = User.Phone,
                    Source = PaymentTokenID,     //YES
                    //PaymentMethod = pay.Id,
                    //Source = "tok_visa",     //YES
                };

                var customerService = new CustomerService();
                Customer customer = customerService.Create(customerOpttions);
                if (customer != null)
                    userRepo.SetStripeTokenID(User.Id, customer.Id);
                return customer;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
