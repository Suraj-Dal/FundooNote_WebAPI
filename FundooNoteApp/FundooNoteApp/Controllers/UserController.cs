using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooNoteApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        public UserController(IUserBL iuserBL)
        {
            this.iuserBL = iuserBL;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUSer(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = iuserBL.Registration(userRegistrationModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Registration Unsuccessful"});
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                var result = iuserBL.Login(userLoginModel);
                if (result != null)
                {
                    return Ok(new {success = true, message = "Login Successful", data = result});
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [Route("Forget")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = iuserBL.ForgetPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset mail sent successfuly", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Reset mail couldn't be send" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost]
        [Route("Reset")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = iuserBL.ResetPassword(Email, password, confirmPassword);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Reset Password Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Password cannot be resetted." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
