using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerseApplication.Repository;
using EcommerseApplication.DTO;
using EcommerseApplication.Models;
using System.Security.Claims;

namespace EcommerseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPaymentController : ControllerBase
    {

        private readonly IUserPayement userpaymentrepo;
        private readonly ConsumerRespons Respons;
        private readonly IUserRepository userRepo;

        public UserPaymentController(IUserPayement _userpaymentrepo,ConsumerRespons Respons,IUserRepository _userRepo)
        {
            userpaymentrepo = _userpaymentrepo;
            this.Respons = Respons;
            userRepo = _userRepo;
        }


        [HttpPost]
        public IActionResult AddNewUserPayment([FromBody] UserPaymentDTO NewUserPayment)
        {
            if (ModelState.IsValid == true)
            {
                try
                {

                    //int UserID = int.Parse(User?.FindFirstValue("UserId"));

                    //int UserID =5;
                    User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("userid"));
                    if (user == null)
                        return BadRequest(new { success = false, message = "You Must Login First" });
                    int UserID = user.Id;

                    User_Payement user_Payement = new User_Payement();
                    user_Payement.PayementType = NewUserPayment.PayementType;
                    user_Payement.Provider = NewUserPayment.Provider;
                    user_Payement.arabicProvider = NewUserPayment.arabicProvider;

                    user_Payement.HolderName = NewUserPayment.HolderName;
                    user_Payement.CardNumber = NewUserPayment.CardNumber;
                    user_Payement.ExpYear = NewUserPayment.ExpYear;
                    user_Payement.ExpMonth = NewUserPayment.ExpMonth;
                    user_Payement.Cvc = NewUserPayment.Cvc;
                    user_Payement.UserId = UserID;


                    if (NewUserPayment.AccountNo != 0)
                        user_Payement.AccountNo = NewUserPayment.AccountNo;
                    if (NewUserPayment.Expiry != null)
                        user_Payement.Expiry = NewUserPayment.Expiry;

                    ///
                    userpaymentrepo.AddUSerPayment(user_Payement);
                    Respons.succcess = true;
                    Respons.Message = "User Payment Added";
                    Respons.Data = "";
                    return Ok(Respons);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException.Message);
                    Respons.succcess = false;
                    Respons.Message = ex.InnerException.Message;
                    Respons.Data = "";
                    return BadRequest(Respons);
                }


            }
            Respons.succcess = false;
            Respons.Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage));
            Respons.Data = "";
            return BadRequest(Respons);
        }
        [HttpGet]
        public IActionResult GetAllUserPayment()
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    //int UserID = int.Parse(User?.FindFirstValue("UserId"));
                    //int UserID =5;
                    User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("userid"));
                    if (user == null)
                        return BadRequest(new { success = false, message = "You Must Login First" });
                    int UserID = user.Id;

                    List<UserPaymentResponseDTO> userPaymentsDTO = new List<UserPaymentResponseDTO>();

                    List<User_Payement> user_Payements = userpaymentrepo.GetAllByUser(UserID);
                    if(user_Payements.Count == 0 || user_Payements == null)
                        return Ok(new { Success = true, Message = "Data Not Found", Data = userPaymentsDTO });
                    for (int i = 0; i < user_Payements.Count; i++)
                    {
                        userPaymentsDTO.Add(new UserPaymentResponseDTO());
                        userPaymentsDTO[i].Id = user_Payements[i].Id;
                        userPaymentsDTO[i].PayementType = user_Payements[i].PayementType;
                        userPaymentsDTO[i].Provider = user_Payements[i].Provider;
                        userPaymentsDTO[i].arabicProvider = user_Payements[i].arabicProvider;

                        userPaymentsDTO[i].HolderName = user_Payements[i].HolderName;
                        userPaymentsDTO[i].CardNumber = user_Payements[i].CardNumber;
                        userPaymentsDTO[i].ExpYear = user_Payements[i].ExpYear;
                        userPaymentsDTO[i].ExpMonth = user_Payements[i].ExpMonth;
                        userPaymentsDTO[i].Cvc = user_Payements[i].Cvc;

                    }
                    

                    ///
                    Respons.succcess = true;
                    Respons.Message = "User Payment Found Successfuly";
                    Respons.Data = userPaymentsDTO;
                    return Ok(Respons);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException.Message);
                    Respons.succcess = false;
                    Respons.Message = ex.InnerException.Message;
                    Respons.Data = "";
                    return BadRequest(Respons);
                }


            }
            Respons.succcess = false;
            Respons.Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage));
            Respons.Data = "";
            return BadRequest(Respons);
        }
    }
}
