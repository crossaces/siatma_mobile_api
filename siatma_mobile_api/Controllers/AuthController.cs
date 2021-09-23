using System;
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
    }
}
