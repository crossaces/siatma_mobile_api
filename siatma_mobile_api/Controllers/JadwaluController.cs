using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using siatma_mobile_api.BM;
using siatma_mobile_api.DAO;

namespace siatma_mobile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JadwalUController : ControllerBase
    {
        JadwaluBM bm;
        MhsDAO dao;
        JadwalkDAO dat;
        public JadwalUController()
        {
            bm = new JadwaluBM();
            dao = new MhsDAO();
            dat = new JadwalkDAO();
        }
        [HttpGet("Ujian")]
        public ActionResult Ujian([FromQuery(Name = "Tahun")] string Tahun)
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                //var mhs = dao.GetDataPrfMhs(npm);
                var data = bm.GetDataJadwalUjianBM(npm, Tahun);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Waktu")]
        public ActionResult WaktuUjian()
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                var mhs = dao.GetDataPrfMhs(npm);
                var data = bm.GetDataWaktuJadwalUjianBM(mhs.ID_PRODI);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

