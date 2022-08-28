using EcommerseApplication.DTO;
using EcommerseApplication.Models;
using EcommerseApplication.Repository;
using EcommerseApplication.Respository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Found Successfuly";
        private readonly ICart_ItemRepository cart_ItemRepo;
        private readonly IShopping_SessionRepository shopping_SessionRrpo;
        private readonly IWebHostEnvironment environment;
        public IUserRepository userRepo;
        private readonly IProductRepository productRepository;
        public IProduct_ImageRepository product_ImageRepository;
        public IDiscount discount;
        public string baseUrl2;

        public CartItemController(ICart_ItemRepository _cart_ItemRepo, IShopping_SessionRepository _shopping_SessionRrpo, IWebHostEnvironment _environment, IUserRepository _userRepo,IProductRepository _productRepository,IProduct_ImageRepository _product_ImageRepository, IDiscount _discount, IHttpContextAccessor baseUrl)
        {
            cart_ItemRepo = _cart_ItemRepo;
            shopping_SessionRrpo = _shopping_SessionRrpo;
            environment = _environment;
            this.userRepo = _userRepo;
            this.productRepository = _productRepository;
            this.product_ImageRepository = _product_ImageRepository;
            this.discount = _discount;
            this.baseUrl2 = string.Format("{0}://{1}//", baseUrl.HttpContext.Request.Scheme, baseUrl.HttpContext.Request.Host.Value);
        }

        //لازم تبقي عامل log in
        [HttpPost("AddToCart")]
        public IActionResult AddToCart(CartItemRequestDTO NewCartItemDTO)
        {
            //int userid; Shopping_Session shopping;
            //  shopping = shopping_SessionRrpo.GetByUserId(userid).First();
            if (NewCartItemDTO.Quantity <= 0)
                return BadRequest(new { success = false, message = "Invalid Quantity" });

            User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("userid"));
            if (user == null)
                return BadRequest(new { success = false, message = BadRequistMSG });
            int userid = user.Id;
            try
            {
                if (ModelState.IsValid && NewCartItemDTO != null)
                {
                    int Shopping_SessionID;
                    List<Shopping_Session> UserSessions = shopping_SessionRrpo.GetByUserId((int)userid);
                    if (UserSessions.Count == 0 || UserSessions == null)
                    {
                        Shopping_Session NewShopping_Session = new Shopping_Session();
                        NewShopping_Session.UserID = userid;
                        NewShopping_Session.Total = 0;
                        shopping_SessionRrpo.AddShopping_Session(NewShopping_Session);
                        Shopping_SessionID = NewShopping_Session.Id;
                    }
                    else { Shopping_SessionID = UserSessions[UserSessions.Count - 1].Id; }

                    Cart_Item cart_ItemCheck = cart_ItemRepo.GetCardItemByproductAndSession(Shopping_SessionID, NewCartItemDTO.ProductId);
                    if (cart_ItemCheck != null)
                    {
                        return Ok(new { Success = false, Message = "Product Is Already In Your Cart" });
                    }

                    Cart_Item cart_Item = new Cart_Item();
                    cart_Item.SessionId = Shopping_SessionID;
                    cart_Item.ProductId = NewCartItemDTO.ProductId;
                    cart_Item.Quantity = NewCartItemDTO.Quantity;

                    cart_ItemRepo.AddCart_Item(cart_Item);

                    return Ok(new { Success = true, Message = "Data Added Successfuly" });
                }
                else
                {
                    return Ok(new { Success = false, Message = BadRequistMSG });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        /************/
        [HttpGet("howMyCartItems")]
        public IActionResult ShowMyCartItems()
        {
            User user; int userid = 1; Shopping_Session shopping;
            try
            {
                user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId")); userid = user.Id;
                //  shopping = shopping_SessionRrpo.GetByUserId(userid).First();
            }
            catch
            {
                userid = 1; //shopping = shopping_SessionRrpo.GetByUserId(1).First();
                            //

            }

            int Shopping_SessionID;
            List<Shopping_Session> UserSessions = shopping_SessionRrpo.GetByUserId((int)userid);
            if (UserSessions.Count == 0 || UserSessions == null)
            {
                Shopping_Session NewShopping_Session = new Shopping_Session();
                NewShopping_Session.UserID = userid;
                NewShopping_Session.Total = 0;
                shopping_SessionRrpo.AddShopping_Session(NewShopping_Session);
                Shopping_SessionID = NewShopping_Session.Id;
            }
            else { Shopping_SessionID = UserSessions[UserSessions.Count - 1].Id; }

            //get All cart_Items by sesion ID
            List<Cart_Item> cart_Items = cart_ItemRepo.GetAllCart_ItemsBySession(Shopping_SessionID);
            List<ProductCartDTO> product_cart_dtos = new List<ProductCartDTO>();
            foreach(var item in cart_Items)
            {
              Product product=  productRepository.GetIncludeById(item.ProductId);
                ProductCartDTO productCart = new ProductCartDTO();
                productCart.ProductId = product.ID;
                productCart.Name=product.Name;
                productCart.arabicName = product.Name_Ar;
                productCart.Discription = product.Description;
                productCart.arabicDiscription = product.Description_Ar;
                productCart.Price=product.Price;
                productCart.QuantityOrdered = item.Quantity;
                string img  = productRepository.GetImages(product.ID).FirstOrDefault();
                if (img == null) { productCart.Image = " "; }
                else {
                    productCart.Image = Path.Combine(baseUrl2, "Images", "Product", img);
                }
               
               
              
                 productCart.categoryName = product.Product_Category.Name;
                if (product.Discount != null) {
                    productCart.Descount_Persent = product.Discount.Descount_Persent;
                }
                productCart.subCategoryName = product.subcategory.Name;
                productCart.QuantityAvailable = product.Product_Inventory.Quantity;
                product_cart_dtos.Add(productCart);

            }

            return Ok(new { Success = true, Message = product_cart_dtos });
        }
        /********/
        [HttpGet("CartItemsByUser/{UserID:int}")]
        public IActionResult GetCartItemsByUserID(int UserID)
        {
            try
            {
                if (ModelState.IsValid && UserID != 0)
                {
                    List<CartItemResponseDTO> cartItemsDTO = new List<CartItemResponseDTO>();
                    int Shopping_SessionID;
                    List<Shopping_Session> UserSessions = shopping_SessionRrpo.GetByUserId((int)UserID);
                    if (UserSessions.Count == 0 || UserSessions == null)
                    {
                        return Ok(new { Success = false, Message = NotFoundMSG, Data = "notfound" });
                    }
                    else { Shopping_SessionID = UserSessions[UserSessions.Count - 1].Id; }

                    List<Cart_Item> cart_Items = cart_ItemRepo.GetAllBySessionID(Shopping_SessionID);


                    for (int i = 0; i < cart_Items.Count; i++)
                    {
                        cartItemsDTO.Add(new CartItemResponseDTO());
                        cartItemsDTO[i].ProductId = cart_Items[i].ProductId;
                        cartItemsDTO[i].QuantityOrdered = cart_Items[i].Quantity;
                        cartItemsDTO[i].ProductName = cart_Items[i].product.Name;

                        cartItemsDTO[i].ProductImage = Path.Combine(environment.WebRootPath, "Images/Product", cart_Items[i].product.Product_Images.FirstOrDefault().ImageFileName);
                        cartItemsDTO[i].ProductDiscription = cart_Items[i].product.Description;
                        cartItemsDTO[i].QuantityAvailable = cart_Items[i].product.Product_Inventory.Quantity;

                    }

                    return Ok(new { Success = true, Message = SuccessMSG, Data = cartItemsDTO });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = BadRequistMSG });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
        //Enter product Id To Remove It From Card Item
        [HttpDelete("deleteFromCard")]

        public IActionResult deleteFromCard(int id)
        {
            User user;int userid=1;
            try {user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId")); userid = user.Id; }
            catch { userid = 1;}
            int Shopping_SessionID;
            try {
            Shopping_Session shopping = shopping_SessionRrpo.GetByUserId(userid).First();
            Cart_Item card = cart_ItemRepo.GetCardItemByproductAndSession(shopping.Id, id);
            cart_ItemRepo.DeleteCart_Item(card);
            }
            catch
            {
                return BadRequest(new { Success = false, Message = BadRequistMSG });
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = "deletedCardItem" });

        }
    }
}
