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
    public class DiscountController : ControllerBase
    {
        private readonly IDiscount discountrepository;
        private ConsumerRespons Response;
        private readonly IUserRepository userRepo;
        private readonly Ipartener partenerRepo;

        public DiscountController(IDiscount discountrepository,ConsumerRespons _Response,IUserRepository _userRepo,
                                  Ipartener _partenerRepo)
        {
            this.discountrepository = discountrepository;
            Response = _Response;
            userRepo = _userRepo;
            partenerRepo = _partenerRepo;
        }
        [HttpPost]
        public IActionResult AddNewDiscount(DiscountDTO NewDiscount)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                    if (user == null)
                        return BadRequest(new { Success = false, Message = "You Must Login First" });

                    Partener partener = partenerRepo.getByUserID(user.Id);
                    if (partener == null)
                        return BadRequest(new { Success = false, Message = "You Must Be Partener" });


                    Discount discount = new Discount();
                    discount.Name = NewDiscount.Name;
                    discount.Description_Ar = NewDiscount.Description_Ar;
                    discount.Name_Ar = NewDiscount.Name_Ar;
                    discount.Description = NewDiscount.Description;
                    discount.Descount_Persent = NewDiscount.Descount_Persent;
                    discount.CreatedAt = DateTime.Now;
                    discount.EndTime = NewDiscount.EndTime;
                    discount.StartTime = NewDiscount.StartTime;
                    discount.Active = NewDiscount.Active;
                    discount.PartnerId = partener.Id;
                    discountrepository.AddnewDiscount(discount);
                    Response.succcess = true;
                    Response.Message = "Discount Added";
                    Response.Data = "";
                    return Ok(Response);
                }
                catch (Exception ex)
                {
                    Response.succcess = false;
                    Response.Message = ex.Message;
                    Response.Data = "";
                    return Ok(Response);
                }

            }
         
                Response.succcess = false;
            Response.Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage));
            Response.Data = "";
                return Ok(Response);
            
        }
        [HttpDelete("DeleteDiscount")]
        public IActionResult DeleteDiscount(int Id)
        {
            try
            {
              int res=  discountrepository.DeleteDiscount(Id);
                if(res!=0)
                {
                    Response.succcess = true;
                    Response.Message = "Discount Deleted successfully";
                    Response.Data = "";
                    return Ok(Response);
                }
                else
                {
                    Response.succcess = false;
                    Response.Message = "this Discount Not Found";
                    Response.Data = "";
                    return NotFound(Response);
                }
            }
            catch(Exception ex)
            {
                Response.Message = ex.Message;
                Response.succcess = false;
                Response.Data = "";
                return BadRequest(Response);
            }
        }

        [HttpGet("PartenerDiscounts")]
        public IActionResult GetAllPartenerDiscounts()
        {
            try
            {
                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return BadRequest(new { Success = false, Message = "You Must Login First" });

                Partener partener = partenerRepo.getByUserID(user.Id);
                if (partener == null)
                    return BadRequest(new { Success = false, Message = "You Must Be Partener" });

                int PartnerID = partener.Id;

                List<Discount> PartnerDiscounts = discountrepository.GetAllByPartener(PartnerID);
                List<Discount> test = discountrepository.GetAllByPartener(PartnerID);
                List<DiscountPartnerDTO> DiscountList = new List<DiscountPartnerDTO>();

                for (int i = 0; i < PartnerDiscounts.Count; i++)
                {
                    DiscountList.Add(new DiscountPartnerDTO());
                    DiscountList[i].Id = PartnerDiscounts[i].ID;
                    DiscountList[i].Name = PartnerDiscounts[i].Name;
                    DiscountList[i].Name_Ar = PartnerDiscounts[i].Name_Ar;
                    DiscountList[i].Description = PartnerDiscounts[i].Description;
                    DiscountList[i].Description_Ar = PartnerDiscounts[i].Description_Ar;
                    DiscountList[i].Active = PartnerDiscounts[i].Active;
                    DiscountList[i].StartTime = PartnerDiscounts[i].StartTime;
                    DiscountList[i].EndTime = PartnerDiscounts[i].EndTime;
                    DiscountList[i].Descount_Persent = PartnerDiscounts[i].Descount_Persent;

                    if(PartnerDiscounts[i].Products != null)
                    {
                        List<Product> products = PartnerDiscounts[i].Products.ToList();
                        DiscountList[i].Products = new List<DiscountProductDTO>();
                        for (int j = 0; j < PartnerDiscounts[i].Products.Count; j++)
                        {
                            DiscountList[i].Products.Add(new DiscountProductDTO());
                            DiscountList[i].Products[j].Id = products[j].ID;
                            DiscountList[i].Products[j].Name = products[j].Name;
                            DiscountList[i].Products[j].Description = products[j].Description;
                            DiscountList[i].Products[j].Price = products[j].Price;
                        }
                    }
                }

                return Ok(new { Success = true, Message = "Data Found Successfuly", Data = DiscountList });

            }
            catch (Exception ex)
            {
                Response.Message = ex.InnerException.Message;
                Response.succcess = false;
                Response.Data = "";
                return BadRequest(Response);
            }
        }
        [HttpGet("DiscountsByPartnerId")]
        public IActionResult GetAllDiscountsBypartnerId()
        {
            try
            {
                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return BadRequest(new { Success = false, Message = "You Must Login First" });

                Partener partener = partenerRepo.getByUserID(user.Id);
                if (partener == null)
                    return BadRequest(new { Success = false, Message = "You Must Be Partener" });

                int PartnerID = partener.Id;

                List<Discount> PartnerDiscounts = discountrepository.GetAllDiscountByPartener(PartnerID);
                List<Discount> test = discountrepository.GetAllByPartener(PartnerID);
                List<DiscountPartnerDTO> DiscountList = new List<DiscountPartnerDTO>();

                for (int i = 0; i < PartnerDiscounts.Count; i++)
                {
                    DiscountList.Add(new DiscountPartnerDTO());
                    DiscountList[i].Id = PartnerDiscounts[i].ID;
                    DiscountList[i].Name = PartnerDiscounts[i].Name;
                    DiscountList[i].Name_Ar = PartnerDiscounts[i].Name_Ar;
                    DiscountList[i].Description = PartnerDiscounts[i].Description;
                    DiscountList[i].Description_Ar = PartnerDiscounts[i].Description_Ar;
                    DiscountList[i].Active = PartnerDiscounts[i].Active;
                    DiscountList[i].StartTime = PartnerDiscounts[i].StartTime;
                    DiscountList[i].EndTime = PartnerDiscounts[i].EndTime;
                    DiscountList[i].Descount_Persent = PartnerDiscounts[i].Descount_Persent;
                }

                return Ok(new { Success = true, Message = "Data Found Successfuly", Data = DiscountList });

            }
            catch (Exception ex)
            {
                Response.Message = ex.InnerException.Message;
                Response.succcess = false;
                Response.Data = "";
                return BadRequest(Response);
            }
        }

        [HttpDelete("DeleteForPartener/{DiscountId:int}")]
        public IActionResult DeleteForPartener(int DiscountId)
        {
            try
            {
                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return NotFound(new { Success = false, Message = "You Must Login First" });

                Partener partener = partenerRepo.getByUserID(user.Id);
                if (partener == null)
                    return NotFound(new { Success = false, Message = "You Must Be Partener" });

                int PartnerID = partener.Id;
                
                Discount discount = discountrepository.getDiscountById(DiscountId);
                if (discount == null)
                    return Ok(new { Success = false, Message = "There Is No Discount With This ID" });

                int result = discountrepository.DeleteForPartener(DiscountId, PartnerID);
                if(result == -1)
                    return Ok(new { Success = false, Message = "There Are Other Products With This Discount" });
                return Ok(new { Success = true, Message = "Discount Deleted Successfuly" });
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.succcess = false;
                Response.Data = "";
                return BadRequest(Response);
            }
        }
        [HttpPut]
        public IActionResult UpdateDiscount(int Id,DiscountDTO NewDiscount)
        {
            try
            {
                int res = discountrepository.UpdateDiscount(Id, NewDiscount);
                if (res != 0)
                {
                    Response.succcess = true;
                    Response.Message = "Discount Updated successfully";
                    Response.Data = "";
                    return Ok(Response);
                }
                else
                {
                    Response.succcess = false;
                    Response.Message = "this Discount Not Found";
                    Response.Data = "";
                    return NotFound(Response);
                }
            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.succcess = false;
                Response.Data = "";
                return BadRequest(Response);
            }
        }
        [HttpGet("GetDiscount")]
        public IActionResult GetAllDiscounts()
        {
             try
             {
                 List<Discount> ListDiscount = discountrepository.getDiscount();
                 List<DiscountDTO> DiscountList=new List<DiscountDTO>();

                 foreach(Discount discount in ListDiscount)
                 {
                     DiscountDTO discountDTO = new DiscountDTO();
                     discountDTO.Id = discount.ID;
                     discountDTO.Name_Ar = discount.Name_Ar;
                     discountDTO.Description_Ar = discount.Description_Ar;
                     discountDTO.Description= discount.Description;
                     discountDTO.Active = discount.Active;
                     discountDTO.StartTime = discount.StartTime;
                     discountDTO.EndTime = discount.EndTime;
                     discountDTO.Descount_Persent = discount.Descount_Persent;
                     discountDTO.Name= discount.Name;
                     DiscountList.Add(discountDTO);
                 }
                 Response.Message = "this is All Discount";
                 Response.succcess = true;
                 Response.Data = DiscountList;
                 return Ok(Response);

             }
             catch(Exception ex)
             {
                 Response.Message = ex.InnerException.Message;
                 Response.succcess = false;
                 Response.Data = "";
                 return BadRequest(Response);
             }
        }
        [HttpGet("getDiscountByID")]
        public IActionResult GetDiscountById(int Id)
        {
            try
            {
               Discount discount = discountrepository.getDiscountById(Id);
                DiscountDTO discountDTO = new DiscountDTO();
                discountDTO.Id = discount.ID;
                discountDTO.Name_Ar = discount.Name_Ar;
                discountDTO.Description_Ar = discount.Description_Ar;
                discountDTO.Description = discount.Description;
                discountDTO.Active = discount.Active;
                discountDTO.StartTime = discount.StartTime;
                discountDTO.EndTime = discount.EndTime;
                discountDTO.Descount_Persent = discount.Descount_Persent;
                discountDTO.Name = discount.Name;
                Response.Message = "this is the Discount";
                Response.succcess = true;
                Response.Data = discountDTO;
                return Ok(Response);

            }
            catch (Exception ex)
            {
                Response.Message = ex.Message;
                Response.succcess = false;
                Response.Data = "";
                return BadRequest(Response);
            }
        }
        [HttpPost("DiscountAssign")]
        public IActionResult AssignDiscount(DiscountIDPartnerIDProductIDDTO Discount)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    int res=discountrepository.AssignDiscount(Discount);
                    if (res ==2)
                    {
                        Response.Message = "assign discount successflly";
                        Response.succcess = true;
                        Response.Data = "";
                        return Ok(Response);
                    }
                    
                    else
                    {
                        Response.Message = "cant assign discount to this product  becouse ther exists discount on this product not finish until now";
                        Response.succcess = false;
                        Response.Data = "";
                        return BadRequest(Response);
                    }
                }
                catch (Exception ex)
                {
                    Response.succcess = false;
                    Response.Message = ex.Message;
                    Response.Data = "";
                    return BadRequest(Response);
                }
            }

            Response.Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage));
            Response.succcess = false;
            Response.Data = "";
            return BadRequest(Response);
        }
    }
}
