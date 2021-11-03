using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using siatma_mobile_api.BM;

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


        [HttpGet("SKSMAT")]
        public ActionResult JumlahMatkulSKS()
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();

                var data = bm.GetJumlahSKSdanMatkul(npm);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("infomhs")]
        public ActionResult infomahasiswal()
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();

                var data = bm.getInfoMahasiswa(npm);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("pembayaran")]
        public ActionResult getPembayaranMahasiswal()
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();

                var data = bm.getInfoPembayaran(npm);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
