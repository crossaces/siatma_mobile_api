using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using siatma_mobile_api.BM;
using siatma_mobile_api.DAO;

namespace siatma_mobile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaftarHSController : ControllerBase
    {
        DaftarHSBM bm;
        DaftarHSDAO dao;
        public DaftarHSController()
        {
            bm = new DaftarHSBM();
            dao = new DaftarHSDAO();

        }
        [HttpGet("DHS")]
        public ActionResult DaftarHasilStudi()
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                var data = bm.GetDaftarHasilstudiBM(npm);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
