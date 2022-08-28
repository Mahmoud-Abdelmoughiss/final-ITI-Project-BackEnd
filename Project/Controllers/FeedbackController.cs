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
    public class FeedbackController : ControllerBase
    {
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Add Successfuly";

        private readonly Ifeedback feedbackrepository;
        private readonly IProductRepository productrepository;
        private readonly ConsumerRespons Respons;
        private readonly Ipartener partnerrepository;
        private readonly IOrder_DetailsRepository order_DetailsRepo;

        public FeedbackController(Ifeedback feedbackrepository,IProductRepository productrepository,ConsumerRespons respons,Ipartener partnerrepository,IOrder_DetailsRepository _order_DetailsRepo)
        {
            this.feedbackrepository = feedbackrepository;
            this.productrepository = productrepository;
            this.Respons = respons;
            this.partnerrepository = partnerrepository;
            order_DetailsRepo = _order_DetailsRepo;
        }
        [HttpGet("getFeedBackbyproduct")]
        public IActionResult getFeedBackbyproductId(int Id)
        {
            List<FeedBackDTO> feedbackListdto = new List<FeedBackDTO>();
            try { 
            List<feedback> feedbackList = feedbackrepository.getByfeedbackProductID(Id);
           
            foreach (feedback feedback in feedbackList)
            {
                FeedBackDTO feed = new FeedBackDTO();
                    feed.productID = feedback.productID;
                //feed.UserID=feedback.UserID;
                feed.Comment = feedback.Comment;
                feed.OrderID = feedback.OrderID;
                feed.Rate = feedback.Rate;
                feedbackListdto.Add(feed);
            }
            }
            catch
            {
                return NotFound(new { Success = false, Message = NotFoundMSG, Data = "notfound" });
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = feedbackListdto });

        }


            [HttpGet("getbyproductId")]
        public IActionResult GetFeedbackByProductID(int Id)
        {
            Product product = productrepository.Get(Id);
            if (product != null)
            {
                try
                {
                    List<feedback> feedbackList = feedbackrepository.getByfeedbackProductID(Id);
                    if (feedbackList.Count>0)
                    {
                        List<FeedBackDTO> feedbackListdto = new List<FeedBackDTO>();
                        int UserID;
                        try { UserID = int.Parse(User?.FindFirstValue("UserId")); }
                        catch
                        {
                            UserID = 3;
                        }
                        foreach (feedback feedback in feedbackList)
                        {
                            FeedBackDTO feed = new FeedBackDTO();
                            //feed.UserID=feedback.UserID;
                            feed.Comment=feedback.Comment;
                            feed.OrderID=feedback.OrderID;
                            feed.Rate=feedback.Rate;
                            feedbackListdto.Add(feed);
                        }
                        Respons.succcess = true;
                        Respons.Message = " get feedback Done";
                        Respons.Data = feedbackListdto;
                        return Ok(Respons);
                      
                    }
                    else
                    {
                        Respons.Message = "no feedback for this product";
                        Respons.succcess = false;
                        Respons.Data = "";
                        return BadRequest(Respons);
                        
                    }
                }
                catch (Exception ex)
                {
                    Respons.Message = ex.InnerException.Message;
                    Respons.succcess = false;
                    Respons.Data = "";
                    return BadRequest(Respons);
                }
            }
            Respons.Message = "this product not found";
            Respons.succcess = false;
            Respons.Data = "";
            return BadRequest(Respons);
        }
        [HttpGet("getfeedParttnerBypartnerId")]
        public IActionResult getrateofpartner(int Id)
        {
            Partener partener=partnerrepository.getByID(Id);
            if (partener != null)
            {
                try
                {
                    int rate = (int)feedbackrepository.getratepartnerbyId(Id);
                    if (rate > 0)
                    {
                        Respons.succcess = true;
                        Respons.Message = " get Rate of Partner Done";
                        rate = (rate * 100) / 100;
                        Respons.Data = (rate)+"%";
                        return Ok(Respons);
                        
                    }
                    else
                    {
                        Respons.Message = "no exists rate for this partner";
                        Respons.succcess = false;
                        Respons.Data = "";
                        return BadRequest(Respons);
                      
                    }
                }
                catch(Exception ex)
                {
                    Respons.Message = ex.InnerException.Message;
                    Respons.succcess = false;
                    Respons.Data = "";
                    return BadRequest(Respons);
                
                }
            }
            Respons.Message = "not exists this partner ";
            Respons.succcess = false;
            Respons.Data = "";
            return BadRequest(Respons);
        }

        [HttpPost("AddFeedback")]
        public IActionResult AddFeedbackByProductID(FeedBackDTO feedBackDTO)
        {
            try
            {

                int UserID;
                try { UserID = int.Parse(User?.FindFirstValue("UserId")); }
                catch
                {
                    UserID = 3;
                }
                
                //int UserID = 5;
                Order_Details Order = order_DetailsRepo.Get(feedBackDTO.OrderID);

                if (Order != null && Order.UserID == UserID)
                {
                    Order_Items ProductCheck = Order.Order_Items.FirstOrDefault(o => o.ProductID == feedBackDTO.productID);
                    if(ProductCheck == null)
                        return NotFound(new { Success = false, Message = NotFoundMSG, Data = new List<string>() });

                    feedback NewfeedBack = new feedback();
                    NewfeedBack.OrderID = feedBackDTO.OrderID;
                    NewfeedBack.UserID = UserID;
                    NewfeedBack.productID = feedBackDTO.productID;
                    NewfeedBack.Comment = feedBackDTO.Comment;
                    NewfeedBack.Rate = feedBackDTO.Rate;

                    feedbackrepository.insert(NewfeedBack);

                    return Ok(new { Success = true, Message = SuccessMSG, Data = new { } });
                }
                else
                {
                    return NotFound(new { Success = false, Message = NotFoundMSG, Data = new List<string>() });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}
