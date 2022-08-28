using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerseApplication.Respository;
using EcommerseApplication.DTO;
using EcommerseApplication.Models;

namespace EcommerseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Found Successfuly";

        private readonly IProductCategory catigoryRepo;

        public ProductCategoryController(IProductCategory _CatigoryRepo)
        {
            catigoryRepo = _CatigoryRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<CategoryResponseDTO> AllCategoryDTOs = new List<CategoryResponseDTO>();
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });

                List<Product_Category> AllCategorys = catigoryRepo.GetAllWithSubCategory();
                if (AllCategorys.Count == 0)
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = AllCategoryDTOs });

                if (AllCategorys != null)
                {
                    for (int i = 0; i < AllCategorys.Count; i++)
                    {
                        AllCategoryDTOs.Add(new CategoryResponseDTO());
                        AllCategoryDTOs[i].ID = AllCategorys[i].ID;
                        AllCategoryDTOs[i].Name = AllCategorys[i].Name;
                        AllCategoryDTOs[i].Name_Ar = AllCategorys[i].Name_Ar;
                        AllCategoryDTOs[i].Description = AllCategorys[i].Description;
                        AllCategoryDTOs[i].Description_Ar = AllCategorys[i].Description_Ar;
                        AllCategoryDTOs[i].SubCategories = new List<SubCategoryResponseDTO>();
                        for (int j = 0; j < AllCategorys[i].SubCategories.Count; j++)
                        {
                            AllCategoryDTOs[i].SubCategories.Add(new SubCategoryResponseDTO());
                            AllCategoryDTOs[i].SubCategories[j].Id = AllCategorys[i].SubCategories[j].Id;
                            AllCategoryDTOs[i].SubCategories[j].Name = AllCategorys[i].SubCategories[j].Name;
                            AllCategoryDTOs[i].SubCategories[j].Description = AllCategorys[i].SubCategories[j].Description;
                            AllCategoryDTOs[i].SubCategories[j].Image = AllCategorys[i].SubCategories[j].image;
                            AllCategoryDTOs[i].SubCategories[j].CategoryName = AllCategorys[i].Name;
                            AllCategoryDTOs[i].SubCategories[j].categoryId = AllCategorys[i].ID;
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = AllCategoryDTOs });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = AllCategoryDTOs });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}
