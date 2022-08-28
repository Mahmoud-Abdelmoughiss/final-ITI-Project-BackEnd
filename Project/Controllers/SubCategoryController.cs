using EcommerseApplication.DTO;
using EcommerseApplication.Models;
using EcommerseApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace EcommerseApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Found Successfuly";
        private string baseUrl2 ;

        private readonly IsubCategory subCategoryRepo;
        private readonly IWebHostEnvironment environment;
        private readonly ConsumerRespons Respons;
        private readonly IHttpContextAccessor baseUrl;

        public SubCategoryController(IsubCategory _subCategoryRepo, IWebHostEnvironment _environment,
                                    ConsumerRespons _Response, IHttpContextAccessor _baseUrl)
        {
            subCategoryRepo = _subCategoryRepo;
            environment = _environment;
            Respons = _Response;
            baseUrl = _baseUrl;

            baseUrl2 = string.Format("{0}://{1}//", baseUrl.HttpContext.Request.Scheme, baseUrl.HttpContext.Request.Host.Value);

        }

        [HttpGet("SubCategorysByCategoryID/{CategoryID:int}")]
        public IActionResult GetAllByCategoryID(int CategoryID)
        {
            try
            {
                List<SubCategoryResponseDTO> AllSubCategoryDTOs = new List<SubCategoryResponseDTO>();
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<SubCategoryResponseDTO>()
                    });

                List<subCategory> AllSubCategorys = subCategoryRepo.GetAllWithIncludeByCategoryID(CategoryID);
                if (AllSubCategorys.Count == 0)
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = AllSubCategoryDTOs });
                if (AllSubCategorys != null)
                {
                    // string path = Path.Combine(baseUrl2, "Images/SubCategory");
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/SubCategory");
                    //string domainName = HttpContext.Request.Host.ToString();

                    for (int i = 0; i < AllSubCategorys.Count; i++)
                    {
                        AllSubCategoryDTOs.Add(new SubCategoryResponseDTO());
                        AllSubCategoryDTOs[i].Id = AllSubCategorys[i].Id;
                        AllSubCategoryDTOs[i].Name = AllSubCategorys[i].Name;
                        AllSubCategoryDTOs[i].Description = AllSubCategorys[i].Description;
                        AllSubCategoryDTOs[i].arabicName = AllSubCategorys[i].arabicName;
                        AllSubCategoryDTOs[i].arabicDescription = AllSubCategorys[i].arabicDescription;
                       
                        string fileNameWithPath = Path.Combine(path, AllSubCategorys[i].image);
                        if (System.IO.File.Exists(fileNameWithPath))
                        {
                            //byte[] imgByte = System.IO.File.ReadAllBytes(fileNameWithPath);
                            //AllSubCategoryDTOs[i].Image = Convert.ToBase64String(imgByte);
                            AllSubCategoryDTOs[i].Image = baseUrl2 + "//Images/SubCategory/" + AllSubCategorys[i].image; //fileNameWithPath;
                        }

                        AllSubCategoryDTOs[i].CategoryName = AllSubCategorys[i].category.Name;
                        AllSubCategoryDTOs[i].categoryId = AllSubCategorys[i].category.ID;
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = AllSubCategoryDTOs });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = AllSubCategoryDTOs });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("CreateSubCategory")]
        public IActionResult CreateSubCategoryByCategoryID([FromForm] SubCategoryRequestDTO subCategoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = subCategoryDTO
                    });

                if (subCategoryDTO == null)
                    return BadRequest(new { Success = false, Message = BadRequistMSG });



                var files = Request.Form.Files;
                if (files == null || files.Count == 0)
                    return BadRequest(new { Success = false, Message = "You Must Add Image/s" });

                string path = Path.Combine(environment.WebRootPath, "Images", "SubCategory");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //string ImageName = Guid.NewGuid() + "_" + subCategoryDTO.image.FileName;
                //string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/SubCategory");
                //string fileNameWithPath = Path.Combine(path, ImageName);

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                //{
                //    subCategoryDTO.image.CopyTo(stream);
                //}

                var file = files[0];
                if (file == null)
                    return BadRequest(new { Success = false, Message = "You Must Add Image/s" });

                string ImageName = Guid.NewGuid() + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                subCategory NewSubCategory = new subCategory
                {
                    Name = subCategoryDTO.Name,
                    arabicName = subCategoryDTO.arabicName,
                    Description = subCategoryDTO.Description,
                    arabicDescription = subCategoryDTO.arabicDescription,
                    image = ImageName,
                    CategoryId = subCategoryDTO.CategoryId
                };

                //for (int i = 0; i < files.Count; i++)
                //{
                    //var file = files[0];

                    //string ImageName = Guid.NewGuid() + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fileNameWithPath = Path.Combine(path, ImageName);
                    var extension = Path.GetExtension(file.FileName);
                    var size = file.Length;
                    using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                //}

                subCategoryRepo.insert(NewSubCategory);
                return Ok(new { Success = true, Message = "Data Inserted Successfuly", Data = NewSubCategory });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("DeleteSubCategory")]
        public IActionResult DeletesubCategory(int Id)
        {
            try
            {
                int result = subCategoryRepo.DeletesubCategory(Id);
                if (result == 1)
                {
                    Respons.succcess = true;
                    Respons.Message = "  subcategoryCategory Deleted successfuly";
                    Respons.Data = "";
                    return Ok(Respons);
                }
                else
                {
                    Respons.succcess = false;
                    Respons.Message = "not found this  subcategoryCategory";
                    Respons.Data = "";
                    return Ok(Respons);
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



        [HttpPut("updateSubcategory")]
        public IActionResult updateSubCategoryByCategoryID(int Id, [FromForm] SubCategoryRequestDTO subCategoryDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = subCategoryDTO
                    });

                if (subCategoryDTO == null)
                    return NotFound(new { Success = false, Message = BadRequistMSG });

                var files = Request.Form.Files;
                if (files == null || files.Count == 0)
                    return BadRequest(new { Success = false, Message = "You Must Add Image/s" });

                string path = Path.Combine(environment.WebRootPath, "Images", "SubCategory");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var file = files[0];
                if (file == null)
                    return BadRequest(new { Success = false, Message = "You Must Add Image/s" });

                string ImageName = Guid.NewGuid() + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                //string ImageName = Guid.NewGuid() + "_" + subCategoryDTO.image.FileName;
                //string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/SubCategory");
                //string fileNameWithPath = Path.Combine(path, ImageName);

                subCategory OldSubCategory = subCategoryRepo.getByID(Id);

                string OldImage = OldSubCategory.image;

                OldSubCategory.Name = subCategoryDTO.Name;
                OldSubCategory.arabicName = subCategoryDTO.arabicName;
                OldSubCategory.Description = subCategoryDTO.Description;
                OldSubCategory.arabicDescription = subCategoryDTO.arabicDescription;
                OldSubCategory.image = ImageName;
                OldSubCategory.CategoryId = subCategoryDTO.CategoryId;

                ////add image
                //for (int i = 0; i < files.Count; i++)
                //{
                    //var file = files[i];

                    //string ImageName = Guid.NewGuid() + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fileNameWithPath = Path.Combine(path, ImageName);
                    var extension = Path.GetExtension(file.FileName);
                    var size = file.Length;
                    using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                //}
                if (System.IO.File.Exists(Path.Combine(path, OldImage)))
                { 
                    System.IO.File.Delete(Path.Combine("wwwroot", "Images", "SubCategory", OldImage));

                }

                subCategoryRepo.updateSubCategory(OldSubCategory);


                return Ok(new { Success = true, Message = "Data updated Successfuly", Data = "" });
            }
            catch (Exception ex)
            {
               return BadRequest(new { Success = false, Message = ex.Message, Data= subCategoryDTO });
            }
        }

        [HttpGet("GetsubCategoryByID/{Id:int}")]
        public IActionResult GetsubCategoryByID(int Id)
        {
            try
            {
                SubCategoryResponseDTO SubCategoryDTOs = new SubCategoryResponseDTO();
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<SubCategoryResponseDTO>()
                    });

                subCategory SubCategory = subCategoryRepo.getByID(Id);
                if (SubCategory != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/SubCategory");




                    SubCategoryDTOs.Id = SubCategory.Id;
                    SubCategoryDTOs.Name = SubCategory.Name;
                    SubCategoryDTOs.Description = SubCategory.Description;
                    SubCategoryDTOs.arabicName = SubCategory.arabicName;
                    SubCategoryDTOs.arabicDescription = SubCategory.arabicDescription;
                    string domainName = HttpContext.Request.Host.ToString();
                    string fileNameWithPath = Path.Combine(path, SubCategory.image);
                    if (System.IO.File.Exists(fileNameWithPath))
                    {
                        //byte[] imgByte = System.IO.File.ReadAllBytes(fileNameWithPath);
                        //AllSubCategoryDTOs[i].Image = Convert.ToBase64String(imgByte);
                        SubCategoryDTOs.Image = baseUrl2 + "//Images/SubCategory/" + SubCategory.image; //fileNameWithPath;
                    }
                    SubCategoryDTOs.CategoryName = SubCategory.category.Name;
                    SubCategoryDTOs.categoryId = SubCategory.category.ID;

                    return Ok(new { Success = true, Message = SuccessMSG, Data = SubCategoryDTOs });
                }

                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = SubCategoryDTOs });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
        [HttpGet("getAllSubcategory")]
        public IActionResult GetAllBysubCategory()
        {
            try
            {
                List<SubCategoryResponseDTO> AllSubCategoryDTOs = new List<SubCategoryResponseDTO>();
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<SubCategoryResponseDTO>()
                    });

                List<subCategory> AllSubCategorys = subCategoryRepo.getAll();
                if (AllSubCategorys.Count == 0)
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = AllSubCategoryDTOs });
                if (AllSubCategorys != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/SubCategory");
                    
                    for (int i = 0; i < AllSubCategorys.Count; i++)
                    {
                        AllSubCategoryDTOs.Add(new SubCategoryResponseDTO());
                        AllSubCategoryDTOs[i].Id = AllSubCategorys[i].Id;
                        AllSubCategoryDTOs[i].Name = AllSubCategorys[i].Name;
                        AllSubCategoryDTOs[i].Description = AllSubCategorys[i].Description;
                        AllSubCategoryDTOs[i].arabicName = AllSubCategorys[i].arabicName;
                        AllSubCategoryDTOs[i].arabicDescription = AllSubCategorys[i].arabicDescription;

                        string fileNameWithPath = Path.Combine(path, AllSubCategorys[i].image);
                        if (System.IO.File.Exists(fileNameWithPath))
                        {
                        //byte[] imgByte = System.IO.File.ReadAllBytes(fileNameWithPath);
                        //AllSubCategoryDTOs[i].Image = Convert.ToBase64String(imgByte);
                       // http://localhost:5092
                            string domainName = HttpContext.Request.Host.ToString();
                            AllSubCategoryDTOs[i].Image = baseUrl2 + "//Images/SubCategory/" + AllSubCategorys[i].image; //fileNameWithPath;
                        }

                        AllSubCategoryDTOs[i].CategoryName = AllSubCategorys[i].category.Name;
                        AllSubCategoryDTOs[i].categoryId = AllSubCategorys[i].category.ID;
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = AllSubCategoryDTOs });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = AllSubCategoryDTOs });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

    }
}
