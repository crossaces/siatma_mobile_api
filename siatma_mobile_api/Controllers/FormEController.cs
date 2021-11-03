using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using siatma_mobile_api.BM;

namespace siatma_mobile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormEController : ControllerBase
    {

        FormEvaluasiBM bm;

        public FormEController()
        {
            bm = new FormEvaluasiBM();


        }
        [HttpGet("Evaluasi")]
        public ActionResult DaftarHasilStudi()
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                var data = bm.GetDataEvaluasiBM(npm);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
