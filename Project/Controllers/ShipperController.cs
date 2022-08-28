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
    public class ShipperController : ControllerBase
    {
        Ishipper shipper;
        IshipperRequest IshipperRequest;
        
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Found Successfuly";
        public ShipperController(Ishipper _shipper,IshipperRequest _ishipperRequest)
        {
            this.shipper = _shipper;
            this.IshipperRequest = _ishipperRequest;
        }
        [HttpGet]
        public IActionResult getAllShippers()
        {
            List<Shipper> shippers;
            try { shippers = shipper.getAll(); }
            catch
            {
                return Ok(new { Success = false, Message = NotFoundMSG, Data = "notFound" });
            }
            
            return Ok(new { Success = true, Message = SuccessMSG, Data = shippers });
           
        }
        [HttpGet("{id:int}")]
        public IActionResult getShipperById(int id)
        {
            Shipper shipp;
            try
            {
                shipp = shipper.getByID(id);

            }
            catch
            {
                return NotFound(new { Success = false, Message = NotFoundMSG, Data = "notFound" });
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = shipp });
        }
        [HttpGet("{Name:alpha}")]
        public IActionResult getByName(string Name)
        {
            return Ok(shipper.getByName(Name));
        }
        [HttpPost]
        public IActionResult AddNewShipper(Shipper sh)
        {
            if (ModelState.IsValid)
            {
                try { shipper.insert(sh); 
                }
                catch
                {
                    return BadRequest(new { Success = false, Message = BadRequistMSG, Data = "dontSaved" });
                }

            }
            else
            {
                return BadRequest(new { Success = false, Message = BadRequistMSG, Data = "dontSaved" });
            }
            
            return Ok(new { Success = true, Message = SuccessMSG, Data = "saved" });
        }
        [HttpPut("{id:int}")]
        public IActionResult upDateShipper(int id ,Shipper sh)
        {
            try
            {
                shipper.update(id, sh);
            }
            catch
            {
                return BadRequest(new { Success = false, Message = BadRequistMSG, Data = "dontSaved" });
            }

            return Ok(new { Success = true, Message = SuccessMSG, Data = "Updated" });

        }
        [HttpDelete]
        public IActionResult deleteShipper([FromBody]int id)
        {
            try
            {
                shipper.delete(id);
            }
            catch
            {
                return BadRequest(new { Success = false, Message = BadRequistMSG, Data = "dontDeleted" });
            }

            return Ok(new { Success = true, Message = SuccessMSG, Data = "deleted" });
            
           
        }

       
        //User requests to be shipper
       [HttpPost("IamShipper")] //test string id,
        public IActionResult shipperRequest(ShipperRequestDTo shipperRequestDTO)
        {
            ShipperRequest shipperRequest = new ShipperRequest();
            shipperRequest.Name=shipperRequestDTO.Name;
            shipperRequest.officePhone = shipperRequestDTO.officePhone;
            shipperRequest.arabicName = shipperRequestDTO.arabicName;
            string uid;
            uid = User?.FindFirstValue("UserId");
            ShipperRequest shipperRequest2 = IshipperRequest.GetByIntityId(uid);

            if(shipperRequest2 != null)
            {
                return Ok(new { Success = false, Message = BadRequistMSG, Data = "You Already Have A request" });
            }
            shipperRequest.AccountID =(uid!=null?uid: "daec1e1e-8b1b-4114-bced-09874df8cd5d");
            try {
                IshipperRequest.Add(shipperRequest);
            }
            catch
            {
                return Ok(new { Success = false, Message = BadRequistMSG, Data = "dontSaved" });
            }

            return Ok(new { Success = true, Message = SuccessMSG, Data = "dataSaved" });
        }

        //Admin Show aLL Shippers Requests
        [HttpGet("ShowAllRequests")]
        public IActionResult showShippersRequest()
        {
            List<ShipperRequest> requests;
            try
            {
                requests = IshipperRequest.GetAll();
                if (requests != null)
                {
                    return Ok(new { Success = true, Message = SuccessMSG, Data = requests });
                }
                else
                {
                    return BadRequest(new { Success = false, Message = BadRequistMSG, Data = "notFound" });
                }
            }
            catch
            {
                return BadRequest(new { Success = false, Message = BadRequistMSG, Data = "notFound" });
            }
        }
        /*[HttpGet("{id:int}/{name}")]
        public IActionResult getٍِِall(int id,string name)
        {
            return Ok("hello");
        }
        [HttpGet("{ID:int}/{Name:alpha}/{officePhone}")]
        public IActionResult add([FromRoute]Shipper sh)
        {
            return Ok("hello");
        }*/

        

    }
}
