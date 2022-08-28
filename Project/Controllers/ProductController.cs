
using EcommerseApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EcommerseApplication.Models;
using EcommerseApplication.DTO;
using System.Security.Claims;
using System.Net.Http.Headers;
using EcommerseApplication.Data;
using Microsoft.AspNetCore.Identity;

namespace EcommerseApplication.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string NotFoundMSG = "Data Not Found";
        private readonly string BadRequistMSG = "Invalid Input Data";
        private readonly string SuccessMSG = "Data Found Successfuly";
        private string baseUrl2;

        private readonly IProductRepository productRepo;

        private readonly IProductRepository productrepository;
        private readonly IWebHostEnvironment environment;
        private readonly ConsumerRespons Respons;
        private readonly IProduct_InventoryRepository inventproductRepo;
        private readonly IUserRepository userRepo;
        private readonly Ipartener partenerRepo;
        private readonly IHttpContextAccessor baseUrl;
        private readonly IProduct_ImageRepository productImageRepo;
        private readonly UserManager<AppUser> userManager;

        public ProductController(IProductRepository _productRepo, IWebHostEnvironment _environment ,
                                 IProductRepository productrepository, ConsumerRespons _Response,
                                 IProduct_InventoryRepository _inventproductRepo, IUserRepository _userRepo,
                                 Ipartener _partenerRepo, IHttpContextAccessor _baseUrl, 
                                 IProduct_ImageRepository _productImageRepo,UserManager<AppUser> _userManager)
        {
           this. productRepo = _productRepo;
           this.environment = _environment;
           this.productrepository = productrepository;
           this. Respons = _Response;
           this.inventproductRepo= _inventproductRepo;
            userRepo = _userRepo;
            partenerRepo = _partenerRepo;
            this.baseUrl = _baseUrl;
            productImageRepo = _productImageRepo;
            userManager = _userManager;
            baseUrl2 = string.Format("{0}://{1}//", baseUrl.HttpContext.Request.Scheme, baseUrl.HttpContext.Request.Host.Value);
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            try
            {

                //productRepo.IsDiscountFinish();

                if(!ModelState.IsValid)
                    return BadRequest(new { Success = false,
                                            Message = String.Join("; ",ModelState.Values.SelectMany(n=>n.Errors)
                                            .Select(m=>m.ErrorMessage)),
                                            Data = new List<ProductResponseDTO>() });

                List<Product> AllProducts = productRepo.GetAllWithInclude();
                //List<Product> AllProducts = productRepo.GetAllWithInclude().Where(p2 => p2.StatusApproval == ProductApprovelEnum.Approved.ToString()).ToList();
                //List<Product> AllProducts = productRepo.GetAllWithInclude().Where(p2 => p2.StatusApproval == ProductApprovelEnum.Pending.ToString()).ToList();
                //List<Product> AllProducts = productRepo.GetAllWithInclude().Where(p2 => p2.StatusApproval == ProductApprovelEnum.Declined.ToString()).ToList();
                //List<Product> AllProducts = productRepo.GetAllWithInclude().Where(p2 => p2.StatusApproval != ProductApprovelEnum.Approved.ToString()).ToList();
                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts != null)
                {
                    string wwwrootPath = environment.WebRootPath;

                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                        if(AllProducts[i].Discount != null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryID = (int)AllProducts[i].CategoryID;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].subcategoryID = (int)AllProducts[i].subcategoryID;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;

                        if(AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images","Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }
                    }
                    return Ok(new { Success= true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<ProductResponseDTO>() });
            }
        }

        [HttpGet("UnApprovedProducts")]
        public IActionResult GetAllUnApproved()
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });

                List<Product> AllProducts = productRepo.GetAllNotApproved();
                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts.Count != 0 && AllProducts != null)
                {
                    string wwwrootPath = environment.WebRootPath;

                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        if(AllProducts[i].Partener != null)
                            ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                        if (AllProducts[i].Discount != null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }
                        //ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryID = (int)AllProducts[i].CategoryID;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].subcategoryID = (int)AllProducts[i].subcategoryID;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;


                        if (AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<Product>() });
            }
        }
        [HttpGet("ApprovedProducts")]
        public IActionResult GetAllApproved()
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });

                List<Product> AllProducts = productRepo.GetAllApproved();
                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts.Count != 0 && AllProducts != null)
                {
                    string wwwrootPath = environment.WebRootPath;

                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                        
                        if (AllProducts[i].Discount != null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryID = (int)AllProducts[i].CategoryID;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].subcategoryID = (int)AllProducts[i].subcategoryID;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;


                        if (AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<Product>() });
            }
        }
        [HttpGet("ApprovedProductss")]
        public IActionResult GetAllApprovedd()
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });

                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return BadRequest(new { Success = false, Message = "You Must Login First" });

                Partener partener = partenerRepo.getByUserID(user.Id);
                if (partener == null)
                    return BadRequest(new { Success = false, Message = "You Must Be Partener" });
                List<Product> AllProducts = productRepo.GetAllApprovedd(partener.Id);
                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts.Count != 0 && AllProducts != null)
                {
                    string wwwrootPath = environment.WebRootPath;

                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;

                        /*if (AllProducts[i].Discount == null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }*/
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryID = (int)AllProducts[i].CategoryID;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].subcategoryID = (int)AllProducts[i].subcategoryID;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;


                        if (AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<Product>() });
            }
        }


        [HttpGet("CategoryProducts/{id:int}")]
        public IActionResult GetAllByCategory(int Id)
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            try
            {
                //if (!ModelState.IsValid)
                //    return BadRequest(new
                //    {
                //        Success = false,
                //        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                //                            .Select(m => m.ErrorMessage)),
                //        Data = new List<ProductResponseDTO>()
                //    });

                List<Product> AllProducts = productRepo.GetAllByCategoryID(Id);
                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts.Count != 0 && AllProducts != null)
                {
                    string wwwrootPath = environment.WebRootPath;

                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                        if (AllProducts[i].Discount != null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryID = (int)AllProducts[i].CategoryID;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].subcategoryID = (int)AllProducts[i].subcategoryID;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;


                        if (AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<Product>() });
            }
        }

        [HttpGet("mycategoryproduct/{id::int}")]
        public IActionResult getbyCatID(int Id)
        {
            List<Product> AllProducts = productRepo.GetAllwithCategoryID(Id);
            return Ok(AllProducts);
        }
        [HttpGet("SubCategoryProducts/{id:int}")]
        public IActionResult GetAllBySubCategory(int Id)
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });

                List<Product> AllProducts = productRepo.GetAllBySubCategoryID(Id);
                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts.Count != 0 && AllProducts != null)
                {
                    string wwwrootPath = environment.WebRootPath;

                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                        if (AllProducts[i].Discount != null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryID = (int)AllProducts[i].CategoryID;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].subcategoryID = (int)AllProducts[i].subcategoryID;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;

                        if (AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<Product>() });
            }
        }
        [HttpGet("getmyproductbyID")]
        public IActionResult getProductById(int productID)
        {
            Product product;
            try {  product = productRepo.GetIncludeById(productID); }
           
            catch(Exception ex) { return NotFound(new { Success = false, Message = NotFoundMSG, Data = ex.Message }); }
            if (product == null) 
            { return Ok(new { Success = true, Message = NotFoundMSG, Data = "notfound" }); }
            try
            {
                ProductResponseDTO productResponseDTO = new ProductResponseDTO();
                productResponseDTO.ID = product.ID;
                productResponseDTO.Name = product.Name;
                productResponseDTO.Name_Ar = product.Name_Ar;
                productResponseDTO.Description = product.Description;
                productResponseDTO.Description_Ar = product.Description_Ar;
                productResponseDTO.Quantity = product.Product_Inventory.Quantity;
                productResponseDTO.Price = product.Price;
                productResponseDTO.IsAvailable = product.IsAvailable;
                productResponseDTO.StatusApproval = product.StatusApproval;

                productResponseDTO.CategoryName = product.Product_Category.Name;
                productResponseDTO.CategoryID = (int)product.CategoryID;
                productResponseDTO.subcategoryName = product.subcategory.Name;
                productResponseDTO.subcategoryID = (int)product.subcategoryID;
                productResponseDTO.PartenerName = product.Partener.Name;
                if (product.Discount != null)
                {
                    productResponseDTO.Discount = product.Discount.Descount_Persent == decimal.Zero ||
                                        DateTime.Compare((DateTime)product.Discount.EndTime, DateTime.Now) < 0 ||
                                         product.Discount.Active == false ?
                                                        0 :
                                                        product.Discount.Descount_Persent;
                }
                else { productResponseDTO.Discount = 0; }

                // productResponseDTO.Images = productRepo.GetImages(productID);
                //
                productResponseDTO.Images = new List<string>();
                List<string> imges = productRepo.GetImages(productID);
                string wwwrootPath = environment.WebRootPath;
                foreach (var item in imges)
                {
                    string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item);
                    //byte[] imgByte;
                    if (System.IO.File.Exists(ImageFullPath))
                    {
                        //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                        //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                        productResponseDTO.Images.Add(Path.Combine(baseUrl2, "Images", "Product", item));
                    }
                }

                //productResponseDTO.Discount = product.Discount.Descount_Persent;
                //return Ok(productResponseDTO);
                return Ok(new { Success = true, Message = SuccessMSG, Data = productResponseDTO });

            }
            catch (Exception ex)
            {

                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("GetAnyProduct/{productID:int}")]
        public IActionResult GetAnyProductById(int productID)
        {
            Product product;
            try { product = productRepo.GetUnApprovedAndApprovedById(productID); }

            catch (Exception ex) { return NotFound(new { Success = false, Message = NotFoundMSG, Data = ex.Message }); }
            if (product == null)
            { return Ok(new { Success = true, Message = NotFoundMSG, Data = "notfound" }); }
            try
            {
                ProductResponseDTO productResponseDTO = new ProductResponseDTO();
                productResponseDTO.ID = product.ID;
                productResponseDTO.Name = product.Name;
                productResponseDTO.Name_Ar = product.Name_Ar;
                productResponseDTO.Description = product.Description;
                productResponseDTO.Description_Ar = product.Description_Ar;
                productResponseDTO.Quantity = product.Product_Inventory.Quantity;
                productResponseDTO.Price = product.Price;
                productResponseDTO.IsAvailable = product.IsAvailable;
                productResponseDTO.StatusApproval = product.StatusApproval;

                productResponseDTO.CategoryName = product.Product_Category.Name;
                productResponseDTO.CategoryID = (int)product.CategoryID;
                productResponseDTO.subcategoryName = product.subcategory.Name;
                productResponseDTO.subcategoryID = (int)product.subcategoryID;
                productResponseDTO.PartenerName = product.Partener.Name;
                if (product.Discount != null)
                {
                    productResponseDTO.Discount = product.Discount.Descount_Persent == decimal.Zero ||
                                        DateTime.Compare((DateTime)product.Discount.EndTime, DateTime.Now) < 0 ||
                                         product.Discount.Active == false ?
                                                        0 :
                                                        product.Discount.Descount_Persent;
                }
                else { productResponseDTO.Discount = 0; }

                // productResponseDTO.Images = productRepo.GetImages(productID);
                //
                productResponseDTO.Images = new List<string>();
                List<string> imges = productRepo.GetImages(productID);
                string wwwrootPath = environment.WebRootPath;
                foreach (var item in imges)
                {
                    string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item);
                    //byte[] imgByte;
                    if (System.IO.File.Exists(ImageFullPath))
                    {
                        //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                        //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                        productResponseDTO.Images.Add(Path.Combine(baseUrl2, "Images", "Product", item));
                    }
                }

                //productResponseDTO.Discount = product.Discount.Descount_Persent;
                //return Ok(productResponseDTO);
                return Ok(new { Success = true, Message = SuccessMSG, Data = productResponseDTO });

            }
            catch (Exception ex)
            {

                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
        [HttpGet("PartnerProducts")]
        public IActionResult GetPartnerProducts()
        {
            try
            {
                List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });

                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return BadRequest(new { Success = false, Message = "You Must Login First" });

                Partener partener = partenerRepo.getByUserID(user.Id);
                if (partener == null)
                    return BadRequest(new { Success = false, Message = "You Must Be Partener" });

                int PartnerID = partener.Id;

                List<Product> AllProducts = productRepo.GetPartnerProducts(PartnerID);

                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts.Count != 0 && AllProducts != null)
                {
                    string wwwrootPath = environment.WebRootPath;

                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                        if (AllProducts[i].Discount != null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].CategoryID = (int)AllProducts[i].CategoryID;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;
                        ProductDTO[i].subcategoryID = (int)AllProducts[i].subcategoryID;

                        if (AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<Product>() });
            }
        }

        [HttpGet("PartnerProductsByCategory/{CategoryID:int}")]
        public IActionResult GetPartnerProductsByCategory(int CategoryID)
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });

                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return BadRequest(new { Success = false, Message = "You Must Login First" });

                Partener partener = partenerRepo.getByUserID(user.Id);
                if (partener == null)
                    return BadRequest(new { Success = false, Message = "You Must Be Partener" });

                int PartnerID = partener.Id;

                List<Product> AllProducts = productRepo.GetPartnerProductsByCategoryID(PartnerID, CategoryID);
                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts.Count != 0 && AllProducts != null)
                {
                    string wwwrootPath = environment.WebRootPath;

                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                        if (AllProducts[i].Discount != null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryID = (int)AllProducts[i].CategoryID;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].subcategoryID = (int)AllProducts[i].subcategoryID;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;

                        if (AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<Product>() });
            }
        }

        [HttpGet("PartnerProductsBySubCategory/{SubCategoryID:int}")]
        public IActionResult GetPartnerProductsBySubCategory(int SubCategoryID)
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });

                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return BadRequest(new { Success = false, Message = "You Must Login First" });

                Partener partener = partenerRepo.getByUserID(user.Id);
                if (partener == null)
                    return BadRequest(new { Success = false, Message = "You Must Be Partener" });

                int PartnerID = partener.Id;

                List<Product> AllProducts = productRepo.GetPartnerProductsBySubCategoryID(PartnerID, SubCategoryID);
                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts.Count != 0 && AllProducts != null)
                {
                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        string wwwrootPath = environment.WebRootPath;

                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                        if (AllProducts[i].Discount != null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].CategoryID = (int)AllProducts[i].CategoryID;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;
                        ProductDTO[i].subcategoryID = (int)AllProducts[i].subcategoryID;

                        if (AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }

                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<Product>() });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct([FromForm] ProductCetegorySubcategoryDTO NewProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });
                string UserIDIdentity = User?.FindFirstValue("UserId");
                var ss2 = User?.Claims;


                User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                if (user == null)
                    return Ok(new { Success = false, Message = "You Must Login First" });

                AppUser appUser = await userManager.FindByIdAsync(UserIDIdentity);
                var Roles = await userManager.GetRolesAsync(appUser);
                if (!Roles.Contains("Partener"))
                    return Ok(new { Success = false, Message = "You Must Be Partener" });
                Partener partener = partenerRepo.getByUserID(user.Id);
                if (partener == null)
                    return Ok(new { Success = false, Message = "You Must Be Partener" });
                int PartnerID = partener.Id;





                //var Roles2 = User?.FindAll(ClaimTypes.Role);

                //AppUser appUser = await userManager.FindByIdAsync(UserIDIdentity);
                //var Roles = await userManager.GetRolesAsync(appUser);

                //if (!Roles.Contains("Partener"))
                //    return BadRequest(new { Success = false, Message = "You Must Be Partener" });


                //User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                //if (user == null)
                //    return BadRequest(new { Success = false, Message = "You Must Login First" });

                //Partener partener = partenerRepo.getByUserID(user.Id);
                //if (partener == null)
                //    return BadRequest(new { Success = false, Message = "You Must Be Partener" });

               // int PartnerID = partener.Id;



                //Images
                var files = Request.Form.Files;
                if (files == null || files.Count == 0)
                    return BadRequest(new { Success = false, Message = "You Must Add Image/s" });


                string path = Path.Combine(environment.WebRootPath, "Images", "Product");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }



                Product product = new Product();
                product.CategoryID = NewProduct.CategoryID;
                product.CreatedAt = DateTime.Now;
                product.Name_Ar = NewProduct.Name_Ar;
                product.Description_Ar = NewProduct.Description_Ar;
                product.Description = NewProduct.Description;
                product.Name = NewProduct.Name;
                product.Price = NewProduct.Price;
                product.IsAvailable = NewProduct.IsAvailable;
                product.subcategoryID = NewProduct.subcategoryID;
                product.StatusApproval = ProductApprovelEnum.Pending.ToString();
                product.PartenerID = PartnerID;



                int ress = inventproductRepo.AddproductInventory(NewProduct.Quantity);
                if (ress == 0)
                    return BadRequest(new { Success = false, Message = BadRequistMSG });
                try
                {
                    product.InventoryID = ress;


                    productRepo.Create(product);
                    //add images



                    for (int i = 0; i < files.Count; i++)
                    {
                        var file = files[i];



                        string ImageName = Guid.NewGuid() + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string fileNameWithPath = Path.Combine(path, ImageName);
                        var extension = Path.GetExtension(file.FileName);
                        var size = file.Length;
                        using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        Product_Images NewImage = new Product_Images();
                        NewImage.ProductID = product.ID;
                        NewImage.ImageFileName = ImageName;
                        productImageRepo.Create(NewImage);
                    }


                    Respons.succcess = true;
                    Respons.Message = "product Added successfuly";
                    Respons.Data = "";
                    return Ok(Respons);
                }
                catch (Exception ex)
                {
                    inventproductRepo.Delete(ress);
                    Respons.Message = ex.InnerException.Message;
                    Respons.succcess = false;
                    Respons.Data = "";
                    return BadRequest(Respons);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }



        }


        //[HttpPost]
        //public IActionResult AddNewProduct([FromForm] ProductCetegorySubcategoryDTO NewProduct)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(new
        //            {
        //                Success = false,
        //                Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
        //                                    .Select(m => m.ErrorMessage)),
        //                Data = new List<ProductResponseDTO>()
        //            });
        //        string ss = User?.FindFirstValue("UserId");
        //        var ss2 = User?.Claims;
        //        //User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("userid"));
        //        //if (user == null)
        //        //    return BadRequest(new { success = false, message = BadRequistMSG });

        //        //Partener partener = partenerRepo.getByUserID(user.Id);
        //        //if (partener == null)
        //        //    return BadRequest(new { success = false, message = BadRequistMSG });

        //        //int partnerid = partener.Id;


        //        //Images
        //        var files = Request.Form.Files;
        //        if (files == null || files.Count == 0)
        //            return Ok(new { Success = false, Message = "You Must Add Image/s" });

        //        string path = Path.Combine(environment.WebRootPath, "Images", "Product");
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        Product product = new Product();
        //        product.CategoryID = NewProduct.CategoryID;
        //        product.CreatedAt = DateTime.Now;
        //        product.Name_Ar = NewProduct.Name_Ar;
        //        product.Description_Ar = NewProduct.Description_Ar;
        //        product.Description = NewProduct.Description;
        //        product.Name = NewProduct.Name;
        //        product.Price = NewProduct.Price;
        //        product.IsAvailable = NewProduct.IsAvailable;
        //        product.subcategoryID = NewProduct.subcategoryID;


        //        product.StatusApproval = ProductApprovelEnum.Pending.ToString();

        //        //product.PartenerID = PartnerID;


        //        int ress = inventproductRepo.AddproductInventory(NewProduct.Quantity);
        //        if (ress == 0)
        //            return BadRequest(new { Success = false, Message = BadRequistMSG });
        //        try
        //        {
        //            product.InventoryID = ress;
        //            productRepo.Create(product);
        //            //add images

        //            for (int i = 0; i < files.Count; i++)
        //            {
        //                var file = files[i];

        //                string ImageName = Guid.NewGuid() + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //                string fileNameWithPath = Path.Combine(path, ImageName);
        //                var extension = Path.GetExtension(file.FileName);
        //                var size = file.Length;
        //                using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
        //                {
        //                    file.CopyTo(stream);
        //                }
        //                Product_Images NewImage = new Product_Images();
        //                NewImage.ProductID = product.ID;
        //                NewImage.ImageFileName = ImageName;
        //                productImageRepo.Create(NewImage);
        //            }


        //            Respons.succcess = true;
        //            Respons.Message = "product Added successfuly";
        //            Respons.Data = product.ID;
        //            return Ok(Respons);
        //        }
        //        catch (Exception ex)
        //        {
        //            inventproductRepo.Delete(ress);
        //            Respons.Message = ex.InnerException.Message;
        //            Respons.succcess = false;
        //            Respons.Data = "";
        //            return BadRequest(Respons);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Success = false, Message = ex.Message });
        //    }

        //}
        //[HttpGet("AssignPartner/{ProductID:int}")]
        //public IActionResult AssignPartner(int ProductID)
        //{
        //    User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("userid"));
        //    if (user == null)
        //        return BadRequest(new { success = false, message = BadRequistMSG });

        //    Partener partener = partenerRepo.getByUserID(user.Id);
        //    if (partener == null)
        //        return BadRequest(new { success = false, message = BadRequistMSG });

        //    int partnerid = partener.Id;

        //    Product product = productRepo.Get(ProductID);
        //    if (product == null)
        //        return BadRequest(new { Success = false, Message = "You Must Add Product First" });
        //    product.PartenerID = partnerid;
        //    productRepo.Update(ProductID, product);
        //    return Ok(new { Success = true, Message = SuccessMSG });
        //}


        [HttpDelete("DeleteProductById/{Id:int}")]
        public IActionResult DeleteProduct(int Id)
        {
            try
            {
                int result = productrepository.Deletee(Id);
                if (result == 1)
                {
                    Respons.succcess = true;
                    Respons.Message = "Product Deleted successfuly";
                    Respons.Data = "";
                    return Ok(Respons);
                }
                else
                {
                    Respons.succcess = false;
                    Respons.Message = "not found this product";
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
        [HttpPut("updateProduct/{Id:int}")]
        public IActionResult UpdateProduct(int Id,[FromForm] ProductCetegorySubcategoryDTO NewProduct)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
                    if(user == null)
                        return BadRequest(new { Success = false, Message = BadRequistMSG });

                    Partener partener = partenerRepo.getByUserID(user.Id);
                    if (partener == null)
                        return BadRequest(new { Success = false, Message = BadRequistMSG });

                    int PartnerID = partener.Id;

                    //Images
                    var files = Request.Form.Files;
                    //if (files == null || files.Count == 0)
                        //return BadRequest(new { Success = false, Message = "You Must Add Image/s" });

                    string path = Path.Combine(environment.WebRootPath, "Images", "Product");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    Product oldproduct = productrepository.Get(Id);
                    if (oldproduct != null)
                    {
                        oldproduct.Name_Ar = NewProduct.Name_Ar;
                        oldproduct.Description_Ar = NewProduct.Description_Ar;
                        oldproduct.Description = NewProduct.Description;
                        oldproduct.Name = NewProduct.Name;
                        oldproduct.Price = NewProduct.Price;
                        oldproduct.IsAvailable = NewProduct.IsAvailable;
                        oldproduct.CategoryID = NewProduct.CategoryID;
                        oldproduct.subcategoryID = NewProduct.subcategoryID;
                        oldproduct.PartenerID = PartnerID;
                        oldproduct.StatusApproval = ProductApprovelEnum.Pending.ToString();

                        //Old Images to remove later
                        //List<string> OldImages = productRepo.GetImages(Id);
                        if(files != null && files.Count > 0)
                        {
                            List<Product_Images> OldImages = productRepo.GetImagesByProductID(Id);
                            for (int i = 0; i < files.Count; i++)
                            {
                                var file = files[i];

                                string ImageName = Guid.NewGuid() + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                string fileNameWithPath = Path.Combine(path, ImageName);
                                var extension = Path.GetExtension(file.FileName);
                                var size = file.Length;
                                using (FileStream stream = new FileStream(fileNameWithPath, FileMode.Create))
                                {
                                    file.CopyTo(stream);
                                }
                                Product_Images NewImage = new Product_Images();
                                NewImage.ProductID = Id;
                                NewImage.ImageFileName = ImageName;
                                productImageRepo.Create(NewImage);
                            }
                            foreach (var image in OldImages)
                            {
                                productImageRepo.Delete(image.Id);
                                System.IO.File.Delete(Path.Combine("wwwroot", "Images", "Product", image.ImageFileName));
                            }
                        }
                        
                        
                        
                        try
                        {
                            inventproductRepo.updateproductInventory((int)oldproduct.InventoryID, NewProduct.Quantity);
                            productrepository.Update(Id, oldproduct);

                            Respons.succcess = true;
                            Respons.Message = "product updated successfuly";
                            Respons.Data = "";
                            return Ok(Respons);

                        }
                        catch (Exception ex)
                        {
                            Respons.Message = ex.InnerException.Message;
                            Respons.succcess = false;
                            Respons.Data = new List<string>();
                            return BadRequest(Respons);
                        }
                    }
                    else
                    {
                        Respons.Message = "product Not Found";
                        Respons.succcess = false;
                        Respons.Data = new List<string>();
                        return BadRequest(Respons);
                    }
                }
                Respons.Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                             .Select(m => m.ErrorMessage));
                Respons.succcess = false;
                Respons.Data = NewProduct;
                return BadRequest(Respons);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message});
            }
            
        }

        [HttpGet("ApproveProduct/{ProductId:int}")]
        public IActionResult ApproveProductByAdmin(int ProductId)
        {
            try
            {
                Product product = productRepo.Get(ProductId);
                if (product == null)
                    return BadRequest(new { Success = false, Message = BadRequistMSG });
                product.StatusApproval = ProductApprovelEnum.Approved.ToString();
                productRepo.Update(ProductId,product);
                return Ok(new { Success = true, Message = "Data Approved Successfuly" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
        [HttpGet("DeclineProduct/{ProductId:int}")]
        public IActionResult DeclineProductByAdmin(int ProductId)
        {
            try
            {
                Product product = productRepo.Get(ProductId);
                if (product == null)
                    return BadRequest(new { Success = false, Message = BadRequistMSG });
                product.StatusApproval = ProductApprovelEnum.Declined.ToString();
                productRepo.Update(ProductId,product);
                return Ok(new { Success = true, Message = "Data Declined Successfuly" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
        [HttpGet("GetPartnerPendingProducts")]
        public IActionResult GetPartnerPendingProducts()
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            //User user=  partenerRepo.getByIDentity(User.FindFirstValue("UserId"));
            //Partener partener = partenerRepo.getByUserID(user.Id);
            User user = userRepo.GetUserByIdentityId(User?.FindFirstValue("UserId"));
            if (user == null)
                return BadRequest(new { Success = false, Message = "You Must Login First" });

            Partener partener = partenerRepo.getByUserID(user.Id);
            if (partener == null)
                return BadRequest(new { Success = false, Message = "You Must Be Partener" });

            List<Product> AllProducts = productRepo.GetNotApprovedByPartner(partener.Id);
            if (AllProducts.Count == 0)
                return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

            if (AllProducts.Count != 0 && AllProducts != null)
            {
                for (int i = 0; i < AllProducts.Count; i++)
                {
                    string wwwrootPath = environment.WebRootPath;

                    ProductDTO.Add(new ProductResponseDTO());
                    ProductDTO[i].ID = AllProducts[i].ID;
                    ProductDTO[i].Name = AllProducts[i].Name;
                    ProductDTO[i].Description = AllProducts[i].Description;
                    ProductDTO[i].Price = AllProducts[i].Price;
                    ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                    ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                    ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                    ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                    if (AllProducts[i].Discount != null)
                    {
                        ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                            DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                             AllProducts[i].Discount.Active == false ?
                                                            0 :
                                                            AllProducts[i].Discount.Descount_Persent;
                    }
                    else { ProductDTO[i].Discount = 0; }
                    ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                    ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                    ProductDTO[i].CategoryID = (int)AllProducts[i].CategoryID;
                    ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;
                    ProductDTO[i].subcategoryID = (int)AllProducts[i].subcategoryID;

                    if (AllProducts[i].Name_Ar != null)
                        ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                    if (AllProducts[i].Description_Ar != null)
                        ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                    ProductDTO[i].Images = new List<string>();
                    foreach (var item in AllProducts[i].Product_Images)
                    {
                        string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                        //byte[] imgByte;
                        if (System.IO.File.Exists(ImageFullPath))
                        {

                            ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                        }
                    }


                    
                }
            }
            return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
        }

        //
        [HttpGet("ProductsByArabicName")]
        public IActionResult GetAllByArabicName(string Name)
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });

                List<Product> AllProducts = productRepo.GetIncludeByArabicName(Name);
                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts.Count != 0 && AllProducts != null)
                {
                    string wwwrootPath = environment.WebRootPath;

                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                        if (AllProducts[i].Discount != null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;

                        if (AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<Product>() });
            }
        }

        //
        [HttpGet("ProductsByName")]
        public IActionResult GetAllByName(string Name)
        {
            List<ProductResponseDTO> ProductDTO = new List<ProductResponseDTO>();
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        Success = false,
                        Message = String.Join("; ", ModelState.Values.SelectMany(n => n.Errors)
                                            .Select(m => m.ErrorMessage)),
                        Data = new List<ProductResponseDTO>()
                    });

                List<Product> AllProducts = productRepo.GetIncludeByName(Name);
                if (AllProducts.Count == 0)
                    return Ok(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });

                if (AllProducts.Count != 0 && AllProducts != null)
                {
                    string wwwrootPath = environment.WebRootPath;

                    for (int i = 0; i < AllProducts.Count; i++)
                    {
                        ProductDTO.Add(new ProductResponseDTO());
                        ProductDTO[i].ID = AllProducts[i].ID;
                        ProductDTO[i].Name = AllProducts[i].Name;
                        ProductDTO[i].Description = AllProducts[i].Description;
                        ProductDTO[i].Price = AllProducts[i].Price;
                        ProductDTO[i].IsAvailable = AllProducts[i].IsAvailable;
                        ProductDTO[i].StatusApproval = AllProducts[i].StatusApproval;
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].Quantity = AllProducts[i].Product_Inventory.Quantity;
                        if (AllProducts[i].Discount != null)
                        {
                            ProductDTO[i].Discount = AllProducts[i].Discount.Descount_Persent == decimal.Zero ||
                                                DateTime.Compare((DateTime)AllProducts[i].Discount.EndTime, DateTime.Now) < 0 ||
                                                 AllProducts[i].Discount.Active == false ?
                                                                0 :
                                                                AllProducts[i].Discount.Descount_Persent;
                        }
                        else { ProductDTO[i].Discount = 0; }
                        ProductDTO[i].PartenerName = AllProducts[i].Partener.Name;
                        ProductDTO[i].CategoryName = AllProducts[i].Product_Category.Name;
                        ProductDTO[i].subcategoryName = AllProducts[i].subcategory.Name;

                        if (AllProducts[i].Name_Ar != null)
                            ProductDTO[i].Name_Ar = AllProducts[i].Name_Ar;
                        if (AllProducts[i].Description_Ar != null)
                            ProductDTO[i].Description_Ar = AllProducts[i].Description_Ar;

                        ProductDTO[i].Images = new List<string>();
                        foreach (var item in AllProducts[i].Product_Images)
                        {
                            string ImageFullPath = Path.Combine(wwwrootPath, "Images", "Product", item.ImageFileName);
                            //byte[] imgByte;
                            if (System.IO.File.Exists(ImageFullPath))
                            {
                                //imgByte = System.IO.File.ReadAllBytes(ImageFullPath);
                                //ProductDTO[i].Images.Add(Convert.ToBase64String(imgByte));
                                ProductDTO[i].Images.Add(Path.Combine(baseUrl2, "Images", "Product", item.ImageFileName));
                            }
                        }
                    }
                    return Ok(new { Success = true, Message = SuccessMSG, Data = ProductDTO });
                }
                else
                {
                    return NotFound(new { Success = true, Message = NotFoundMSG, Data = ProductDTO });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message, Data = new List<Product>() });
            }
        }



    }
}

 


