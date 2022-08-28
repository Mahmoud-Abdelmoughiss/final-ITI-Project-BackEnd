using EcommerseApplication.Data;
using EcommerseApplication.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Claims;

namespace EcommerseApplication.Controllers
{

   // [Authorize]

    //[Authorize]

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpContextAccessor baseUrl;
        private readonly IWebHostEnvironment env;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor baseUrl,IWebHostEnvironment _env)
        {
            _logger = logger;
            this.baseUrl = baseUrl;
            env = _env;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("asd")]
        public string GetMe()
        {
            string userId = User?.FindFirstValue("UserId");
            var baseUrl2 = string.Format("{0}://{1}//", baseUrl.HttpContext.Request.Scheme, baseUrl.HttpContext.Request.Host.Value);

            string ss = ProductApprovelEnum.Approved.ToString();
            //CheckoutDTO dd = new CheckoutDTO();
            //dd.Email = "aaaaa";
            //MailAddress m;
            //bool x = MailAddress.TryCreate(dd.Email,out m);
            //bool y = MailAddress.TryCreate("asd@asd.com",out m);

            //List<string> asf = new List<string>();
            //asf.Add( baseUrl.HttpContext.Request.Host.Value);
            //asf.Add( baseUrl.HttpContext.Request.PathBase.Value);
            //asf.Add( baseUrl.HttpContext.Request.Path.Value);
            //asf.Add( baseUrl.HttpContext.Request.Query.ToString());
            //asf.Add( baseUrl.HttpContext.Request.Scheme);
            //asf.Add( baseUrl.HttpContext.Request.Protocol);
            return baseUrl2;
        }

        [HttpPost("file")]
        public bool GetFile([FromForm] string test) //[FromBody] testDTO test  //byte[] test
        {
            var files = Request.Form.Files;
            if (files == null || files.Count == 0)
                return false;
            string path = Path.Combine(env.WebRootPath, "Images", "SubCategory");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
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
            }
            return true;
        }
    }
}