using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using siatma_mobile_api.BM;
using siatma_mobile_api.Model;

namespace siatma_mobile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        AuthBM bm;
        public AuthController()
        {
            bm = new AuthBM();
        }

        [AllowAnonymous]
        [HttpPost("LoginSiatma")]
        public ActionResult LoginSiatma(UserLogin ul)
        {
            try
            {
                var data = bm.LoginSiatma(ul.username, ul.password);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("change")]
        public ActionResult GantiPassword(GantiPassword ul)
        {
            try
            {
                var npm = User.Claims
                       .Where(c => c.Type == "username")
                           .Select(c => c.Value).SingleOrDefault();
                var data = bm.updatePasswordMahasiswa(npm, ul.password,ul.passwordlama);;

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
