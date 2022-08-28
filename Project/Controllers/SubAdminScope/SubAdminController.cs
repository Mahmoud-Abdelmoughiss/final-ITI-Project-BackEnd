using EcommerseApplication.DTO;
using EcommerseApplication.Models;
using EcommerseApplication.Repository;
using EcommerseApplication.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerseApplication.Controllers.SubAdminScope
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubAdminController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly Ishipper shiperRepository;

        public SubAdminController(Ishipper ishiper, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            shiperRepository = ishiper;
        }
        [HttpPost]
        [Route("AsignRolesBySubAdmin")]
        public async Task<IActionResult> AddUSerToSpecificRole([FromBody] AssignRolesByEmail model)
        {
            if (ModelState.IsValid)
            {
                if (model.RoleName == "Shiper" || model.RoleName == "Partener")
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Email Does not Exist" });
                    }
                    if (!await _roleManager.RoleExistsAsync(model.RoleName))
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role Does not Exist" });
                    }
                    if (await _userManager.IsInRoleAsync(user, model.RoleName))
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"User already assigned to {model.RoleName} Role" });

                    }
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"Shiper or Partener Only" });
                }

            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
               .SelectMany(v => v.Errors)
               .Select(e => e.ErrorMessage));
                return StatusCode(StatusCodes.Status500InternalServerError, message);
                //{
                //    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                //   "title": "One or more validation errors occurred.",
                //   "status": 400,
                //   "traceId": "00-3bd746565e6013fab2c615e972c050d0-3670e1a7eeef39c4-00",
                //    "errors": {
                //        "Email": [
                //          "Email is required"
                //       ],
                //      "RoleName": [
                //     "Role Name is required"
                //         ]
                //            }
                //}
            }

            return Ok(new Response { Status = "Ok", Message = "Created Successfuly" });
        }
       
    }
}
