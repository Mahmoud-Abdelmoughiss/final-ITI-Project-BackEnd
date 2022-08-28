using EcommerseApplication.Models;
using EcommerseApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerseApplication.DTO;
using System.Security.Claims;

namespace EcommerseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ShippingDetailsController : ControllerBase
    {
        private IshippingDetails shippingDetails;
        private readonly IOrder_DetailsRepository order_DetailsRepo;
        private IUserRepository userRepo;
        private Ishipper ishipper;
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Found Successfuly";
        public ShippingDetailsController(IshippingDetails _shippingDetails ,IOrder_DetailsRepository _order_DetailsRepo,IUserRepository _userRepo,Ishipper _ishipper)
        {
            this.shippingDetails = _shippingDetails;
            order_DetailsRepo = _order_DetailsRepo;
            this.userRepo = _userRepo;
            this.ishipper = _ishipper;
        }
        //ShippingDetails/1   --Get
        [HttpGet("{id:int}")]
        public IActionResult getAllShippingByUserID(int id)
        {
            List<shippingDetails> shippingDetailsList;
            try
            {
                shippingDetailsList = shippingDetails.getAllbyUserID(id);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = NotFoundMSG, Data ="notfound" });
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = shippingDetailsList });
           
        }

        [HttpGet("GetMyShipping")]
        public IActionResult getAllShippingByUserID()
        {
            User user; int id=3;
            try { user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));id = user.Id; }
            catch
            {
                id = 3;
            }
            
             
            List<shippingDetails> shippingDetailsList;
            try
            {
                shippingDetailsList = shippingDetails.getAllbyUserID(id);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = NotFoundMSG, Data = "notfound" });
            }
            List<ShowUserShippingDTO> userShiping = new List<ShowUserShippingDTO>();
            foreach (var shippD in shippingDetailsList)
            {
                ShowUserShippingDTO showUserShippingDTO = new ShowUserShippingDTO();
                showUserShippingDTO.ID=shippD.ID;
                showUserShippingDTO.shipName = shippD.shipName;
                showUserShippingDTO.shippingstate = shippD.shippingstate;
                showUserShippingDTO.ALLaddress = shippD.ALLaddress;
                showUserShippingDTO.ALLaddress_Ar = shippD.ALLaddress_Ar;
                showUserShippingDTO.arabicshippingstate = shippD.arabicshippingstate;
                Shipper sh = ishipper.getByID(shippD.shipperID);
                showUserShippingDTO.shipperName = sh.Name;
                showUserShippingDTO.shipperPhone = sh.officePhone;
                
                userShiping.Add(showUserShippingDTO);



            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = userShiping });

        }
        //ShippingDetails/1    --put
        //"shipName" ,"shippingstate","arabicshippingstate"userID": shipperID:

        [HttpPut("{id:int}")]
        public IActionResult updateShipingState(int id,[FromBody]shippingDetails shipping)
        {
            try
            {
                shippingDetails.updateShippingDetails(id, shipping);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = BadRequistMSG, Data = "dontsaved" });
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = "saved" });
            //shippingDetails.updateState(id, shippingstate);
            
            
        }


        [HttpPut("updateshippingState")]
        public IActionResult updateShipingState(UpdateshippingDTO updateshippingDTO)
        {
            try
            {
                shippingDetails.updateStatewithDTo(updateshippingDTO);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = BadRequistMSG, Data = "dontsaved" });
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = "saved" });
            //shippingDetails.updateState(id, shippingstate);


        }

        //
        [HttpPut("updateshippingStateToPickUp")]
        public IActionResult updateShipingStateTopickup(int shipId)
        {
            UpdateshippingDTO updateshippingDTO=new UpdateshippingDTO();
            updateshippingDTO.shipID = shipId;//Pick-up, on-process, on-delivery, Delivered)
                                              ////(استلام شركة الشحن, جاري التنفيذ, جاري التوصيل, تم التوصيل
            updateshippingDTO.shippingState = "Pick-up";
            updateshippingDTO.arabicshippingState = "استلام شركة الشحن";
            try
            {
                shippingDetails.updateStatewithDTo(updateshippingDTO);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = BadRequistMSG, Data = "dontsaved" });
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = "saved" });
            //shippingDetails.updateState(id, shippingstate);


        }

        [HttpPut("updateshippingStateToOnProcess")]
        public IActionResult updateShipingStateToOnProcess(int shipId)
        {
            UpdateshippingDTO updateshippingDTO = new UpdateshippingDTO();
            updateshippingDTO.shipID = shipId;//Pick-up, on-process, on-delivery, Delivered)
                                              ////(استلام شركة الشحن, جاري التنفيذ, جاري التوصيل, تم التوصيل
            updateshippingDTO.shippingState = "on-process";
            updateshippingDTO.arabicshippingState = "جاري التنفيذ";
            try
            {
                shippingDetails.updateStatewithDTo(updateshippingDTO);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = BadRequistMSG, Data = "dontsaved" });
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = "saved" });
            //shippingDetails.updateState(id, shippingstate);
        }

        [HttpPut("updateshippingStateToOnDelivery")]
        public IActionResult updateShipingStateToOnOnDelivery(int shipId)
        {
            UpdateshippingDTO updateshippingDTO = new UpdateshippingDTO();
            updateshippingDTO.shipID = shipId;//Pick-up, on-process, on-delivery, Delivered)
                                              ////(استلام شركة الشحن, جاري التنفيذ, جاري التوصيل, تم التوصيل
            updateshippingDTO.shippingState = "on-delivery";
            updateshippingDTO.arabicshippingState = "جاري التوصيل";
            try
            {
                shippingDetails.updateStatewithDTo(updateshippingDTO);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = BadRequistMSG, Data = "dontsaved" });
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = "saved" });
            //shippingDetails.updateState(id, shippingstate);
        }

        [HttpPut("updateshippingStateToDelivered")]
        public IActionResult updateShipingStateToDelivered(int shipId)
        {
            UpdateshippingDTO updateshippingDTO = new UpdateshippingDTO();
            updateshippingDTO.shipID = shipId;//Pick-up, on-process, on-delivery, Delivered)
                                              ////(استلام شركة الشحن, جاري التنفيذ, جاري التوصيل, تم التوصيل
            updateshippingDTO.shippingState = "Delivered";
            updateshippingDTO.arabicshippingState = "تم التوصيل";
            try
            {
                shippingDetails.updateStatewithDTo(updateshippingDTO);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = BadRequistMSG, Data = "dontsaved" });
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = "saved" });
            //shippingDetails.updateState(id, shippingstate);
        }









        //ShippingDetails/shipper/1   --Get
        [HttpGet("shipper/{id}")]
        public IActionResult getbyShipperID(int id)
        {
            List<shippingDetails> shippingDetailsList;
            try
            {
                shippingDetailsList = shippingDetails.getAllbyShipperID(id);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = NotFoundMSG, Data = "notfound" });
            }
            
            return Ok(new { Success = true, Message = SuccessMSG, Data = shippingDetailsList });
            
        }

        /****/
        [HttpGet("shipper/getmyShipping")]
        public IActionResult getbyShipperID()
        {
            string UserID = User?.FindFirstValue("UserId");
            Shipper shipper = ishipper.getByIdentityID(UserID);
            if (shipper == null)
                return BadRequest(new { Success = false, Message = "You Must Be Shipper" });

            int id = shipper.ID;
            List<shippingDetails> shippingDetailsList;
            try
            {
                shippingDetailsList = shippingDetails.getAllbyShipperID(id);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = NotFoundMSG, Data = "notfound" });
            }
            List <ShowShippingtoshipperDTO> userShiping = new List<ShowShippingtoshipperDTO>();
            foreach (var shippD in shippingDetailsList)
            {
                ShowShippingtoshipperDTO showUserShippingDTO = new ShowShippingtoshipperDTO();
                showUserShippingDTO.ID = shippD.ID;
                showUserShippingDTO.shipName = shippD.shipName;
                showUserShippingDTO.shippingstate = shippD.shippingstate;
                showUserShippingDTO.ALLaddress = shippD.ALLaddress;
                showUserShippingDTO.ALLaddress_Ar = shippD.ALLaddress_Ar;
                showUserShippingDTO.arabicshippingstate = shippD.arabicshippingstate;
                showUserShippingDTO.customerPhone = shippD.CustomerMobile;



                userShiping.Add(showUserShippingDTO);



            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = userShiping });

        }
        /* public IActionResult ADDNewShip([FromBody] AddShippingDTO shipping)
         {
             shippingDetails sh = new shippingDetails();
             sh.
         }*/


        [HttpGet("showShippingProgress/{Orderid:int}")]
        public IActionResult showShippingProgressState(int Orderid)
        {
            try
            {
                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return BadRequest(new { Success = false, Message = "You Must Login First" });
                int userID = user.Id;
                List<Order_Details> Orders = order_DetailsRepo.GetAllByUserID(userID);
                //int userID = 5;
                //List<Order_Details> Orders = order_DetailsRepo.GetAllByUserID(5);

                Order_Details CurntOrder = Orders.FirstOrDefault(o => o.Id == Orderid);

                if (Orders.Count == 0 || CurntOrder == null)
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = new List<string>() });

                shippingDetails shippingData = shippingDetails.getByUserAndOrder(userID, Orderid);

                if (shippingData != null)
                {
                    return Ok(new { Success = true, Message = SuccessMSG, Data = new { ShippingStatt = shippingData.shippingstate, ShippingStatt_Ar = shippingData.arabicshippingstate } });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = new List<string>() });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }

        }

        [HttpGet("user/getByshippingID")]
        public IActionResult getShippingByID(int id)
        {
            shippingDetails shippD;
            try
            {
                shippD = shippingDetails.getByID(id);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = NotFoundMSG, Data = "notfound" });
            }
                 ShowShippingtoshipperDTO showUserShippingDTO = new ShowShippingtoshipperDTO();
                showUserShippingDTO.ID = shippD.ID;
                showUserShippingDTO.shipName = shippD.shipName;
                showUserShippingDTO.shippingstate = shippD.shippingstate;
                showUserShippingDTO.ALLaddress = shippD.ALLaddress;
                showUserShippingDTO.ALLaddress_Ar = shippD.ALLaddress_Ar;
                showUserShippingDTO.arabicshippingstate = shippD.arabicshippingstate;
            showUserShippingDTO.customerPhone = shippD.CustomerMobile;

            return Ok(new { Success = true, Message = SuccessMSG, Data = showUserShippingDTO });



        }

        //
        [HttpGet("shipper/getShippingByShipID")]
        public IActionResult getShipperShippingByShipID(int id)
        {
            
            shippingDetails shippD;
            try
            {
                shippD = shippingDetails.getByID(id);
            }
            catch
            {
                return BadRequest(new { Success = true, Message = NotFoundMSG, Data = "notfound" });
            }
            
           
                ShowShippingtoshipperDTO showUserShippingDTO = new ShowShippingtoshipperDTO();
                showUserShippingDTO.ID = shippD.ID;
                showUserShippingDTO.shipName = shippD.shipName;
                showUserShippingDTO.shippingstate = shippD.shippingstate;
                showUserShippingDTO.ALLaddress = shippD.ALLaddress;
                showUserShippingDTO.ALLaddress_Ar = shippD.ALLaddress_Ar;
                showUserShippingDTO.arabicshippingstate = shippD.arabicshippingstate;
                showUserShippingDTO.customerPhone = shippD.CustomerMobile;



              
            return Ok(new{ Success = true, Message = SuccessMSG, Data = showUserShippingDTO});

        }


    }
}
