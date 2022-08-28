using EcommerseApplication.Repository;
using EcommerseApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerseApplication.DTO;

namespace EcommerseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Found Successfuly";
        private readonly Ipartener partenerRepo;

        public PartnerController(Ipartener _partenerRepo)
        {
            partenerRepo = _partenerRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<PartnerDTO> AllPartenersDTO = new List<PartnerDTO>();
            try
            {


                List<Partener> AllParteners = partenerRepo.getAll();
                if (AllParteners.Count == 0 || AllParteners == null)
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = AllPartenersDTO });

                if (AllParteners != null)
                {
                    for (int i = 0; i < AllParteners.Count; i++)
                    {
                        AllPartenersDTO.Add(new PartnerDTO());

                        AllPartenersDTO[i].Id = AllParteners[i].Id;
                        AllPartenersDTO[i].Name = AllParteners[i].Name;
                        AllPartenersDTO[i].Type = AllParteners[i].Type;
                        AllPartenersDTO[i].numberOfBranches = AllParteners[i].numberOfBranches;
                        AllPartenersDTO[i].Ownername = AllParteners[i].identity.UserName;
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = AllPartenersDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = AllPartenersDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}
