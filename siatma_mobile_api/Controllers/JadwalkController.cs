using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using siatma_mobile_api.BM;
using siatma_mobile_api.DAO;

namespace siatma_mobile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JadwalkController : ControllerBase
    {
        JadwalkBM bm;
        MhsDAO dao;
        JadwalkDAO dat;
        public JadwalkController()
        {
            bm = new JadwalkBM();
            dao = new MhsDAO();
            dat = new JadwalkDAO();
        }

        [HttpGet("Kuliah")]
        public ActionResult Ujian([FromQuery(Name = "Tahun")] string Tahun)
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                //var mhs = dao.GetDataPrfMhs(npm);
                var data = bm.GetDataJadwalKuliahBM(npm, Tahun);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Waktu")]
        public ActionResult WaktuKuliah()
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                var mhs = dao.GetDataPrfMhs(npm);
                var data = bm.GetDataWaktuJadwalKuliahBM(mhs.ID_PRODI);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
