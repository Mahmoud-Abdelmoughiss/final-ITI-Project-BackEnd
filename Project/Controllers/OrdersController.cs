using EcommerseApplication.DTO;
using EcommerseApplication.Models;
using EcommerseApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Found Successfuly";
        private string baseUrl2;

        private readonly IOrder_DetailsRepository order_DetailsRepo;
        private readonly IWebHostEnvironment environment;
        private readonly IUserRepository userRepo;
        private readonly IHttpContextAccessor baseUrl;

        public OrdersController(IOrder_DetailsRepository _order_DetailsRepo,IWebHostEnvironment _environment,
                                IUserRepository _userRepo, IHttpContextAccessor _baseUrl)
        {
            order_DetailsRepo = _order_DetailsRepo;
            environment = _environment;
            userRepo = _userRepo;
            baseUrl = _baseUrl;
            baseUrl2 = string.Format("{0}://{1}//", baseUrl.HttpContext.Request.Scheme, baseUrl.HttpContext.Request.Host.Value);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAllByUserID()
        {
            try
            {
                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return BadRequest(new { Success = false, Message = "You Must Login First" });

                int UserID = user.Id;
                List < Order_Details > Orders = order_DetailsRepo.GetAllByUserID(UserID); 
                //List<Order_Details> Orders = order_DetailsRepo.GetAllByUserID(5);
                if (Orders.Count == 0)
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = new List<OrderDetailsDTO>() });

                string wwwrootPath = environment.WebRootPath;

                if (Orders != null)
                {
                    List<OrderDetailsDTO> OrdersDTO = new List<OrderDetailsDTO>();
                    for (int i = 0; i < Orders.Count; i++)
                    {
                        OrdersDTO.Add(new OrderDetailsDTO());

                        OrdersDTO[i].OrderId = Orders[i].Id;
                        OrdersDTO[i].TotalPrice = Orders[i].Total;
                        OrdersDTO[i].CreatedAt = Orders[i].CreatedAt;

                        List<Order_Items> Items = Orders[i].Order_Items.ToList();
                        OrdersDTO[i].OrderItems = new List<OrderItemsDTO>();
                        for (int j = 0; j < Items.Count; j++)
                        {
                            OrdersDTO[i].OrderItems.Add(new OrderItemsDTO());

                            OrdersDTO[i].OrderItems[j].ItemID = Items[j].ID;
                            OrdersDTO[i].OrderItems[j].Quantity = Items[j].Quantity;
                            OrdersDTO[i].OrderItems[j].CreatedAt = Items[j].CreatedAt;
                            OrdersDTO[i].OrderItems[j].ProductID = Items[j].ProductID;
                            OrdersDTO[i].OrderItems[j].ProductName = Items[j].Product.Name;

                            if(Items[j].Product.Product_Images.Count > 0 && Items[j].Product.Product_Images != null)
                            {
                                string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", Items[j].Product.Product_Images.FirstOrDefault().ImageFileName);
                                if (System.IO.File.Exists(ImageFullPath))
                                    OrdersDTO[i].OrderItems[j].ProductImage = Path.Combine(baseUrl2, "Images", "Product", Items[j].Product.Product_Images.FirstOrDefault().ImageFileName);

                            }
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = OrdersDTO });
                }
                else
                {
                    return NotFound(new { Success = false, Message = NotFoundMSG , Data = new List<OrderDetailsDTO>() });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}
