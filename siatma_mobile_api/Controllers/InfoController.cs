using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using siatma_mobile_api.BM;
using siatma_mobile_api.DAO;

namespace siatma_mobile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        InfoMhsBm bm;
        
        
        public InfoController()
        {
            bm = new InfoMhsBm();
            
        }

        [HttpGet("akademik")]
        public ActionResult Akademik([FromQuery(Name = "Masuk")] string masuk, [FromQuery(Name = "Prodi")] string prodi)
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                
                var data = bm.GetTahunAkademik(masuk, prodi);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("presensi")]
        public ActionResult Presensi([FromQuery(Name = "Tahun")] string semester)
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();

                var data = bm.GetPresensiMahasiswa(npm,semester);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
