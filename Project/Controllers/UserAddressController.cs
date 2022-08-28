using EcommerseApplication.DTO;
using EcommerseApplication.Models;
using EcommerseApplication.Repository;
using EcommerseApplication.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace EcommerseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        IUserAddress userAddressrepo;
        private readonly ConsumerRespons Respons;
        private readonly IUserRepository userRepo;
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Found Successfuly";
        private string baseUrl2;
        public UserAddressController(IUserAddress _userAddress,ConsumerRespons response, IUserRepository _userRepo)
        {
            userAddressrepo = _userAddress;
            this.Respons = response;
            this.userRepo = _userRepo;
        }
        [HttpPost]
        public IActionResult AddNewAdress(UserAddressUSerIdDTO NewAdress)
        {
           if(ModelState.IsValid==true)
            {
                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("userid"));
                if (user == null)
                    return BadRequest(new { success = false, message = "You Must Login First" });
                int UserID = user.Id;
                try
                {
                    userAddressrepo.AddNewAddresss(UserID,NewAdress);
                    Respons.succcess = true;
                    Respons.Message = "User Address Added";
                    Respons.Data = "";
                    return Ok(Respons);
                }
                catch(Exception ex)
                {
                    //ModelState.AddModelError("",ex.InnerException.Message);
                    Respons.Message = ex.InnerException.Message;
                    Respons.succcess=false;
                    Respons.Data = "";
                    return BadRequest(Respons);
                }
            }
            Respons.Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage));
            Respons.succcess = false;
            Respons.Data = "";
            return BadRequest(Respons);
            
        }
        [HttpGet("GetUserAdress")]
        public IActionResult GetAddress()
        {
            User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
            if (user == null)
                return BadRequest(new { Success = false, Message = BadRequistMSG ,Data="not LoggedIn"});
            List<User_address> GetAll;
            Console.WriteLine(user.Id);
            try
            {
                GetAll = userAddressrepo.GetAllAddress(user.Id);
                if (GetAll == null) {
                    return Ok(new { Success = false, Message = BadRequistMSG, Data = "not Found" });
                }
                //Ahmed
            }
            catch
            {
                return BadRequest(new { Success = false, Message = BadRequistMSG, Data = "not Found" });
            }
            List<userAddressDisplayDTO>adresses=new List<userAddressDisplayDTO>();
            foreach(User_address address in GetAll)
            {
                userAddressDisplayDTO userAddressDisplayDTO = new userAddressDisplayDTO();
                userAddressDisplayDTO.Id = address.Id;
                userAddressDisplayDTO.AddressLine1 = address.AddressLine1;
                userAddressDisplayDTO.AddressLine2 = address.AddressLine2;
                userAddressDisplayDTO.telephone = address.telephone;
                userAddressDisplayDTO.City = address.City;
                userAddressDisplayDTO.Country= address.Country;
                userAddressDisplayDTO.PostalCode = address.PostalCode;
                userAddressDisplayDTO.mobile = address.mobile;
                userAddressDisplayDTO.arabicAddressLine1 = address.arabicAddressLine1;
                userAddressDisplayDTO.arabicAddressLine2 = address.arabicAddressLine2;
                userAddressDisplayDTO.arabicCity=address.arabicCity;
                userAddressDisplayDTO.arabicCountry=address.arabicCountry;
                userAddressDisplayDTO.UserId=address.UserId;
                adresses.Add(userAddressDisplayDTO);



            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = adresses });
        }
    }
}
