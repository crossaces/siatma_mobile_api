using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using siatma_mobile_api.BM;
using siatma_mobile_api.DAO;

namespace siatma_mobile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KartuHSController : ControllerBase
    {
        KartuHSBM bm;
        MhsDAO dao;
        KartuHasilStudiDAO dao1;
        public KartuHSController()
        {
            bm = new KartuHSBM();
            dao = new MhsDAO();
            dao1 = new KartuHasilStudiDAO();

        }
        [HttpGet("KHS")]
        public ActionResult KartuHasilStudi([FromQuery(Name = "Tahun")] string Tahun)
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                //var mhs = dao.GetDataPrfMhs(npm);
                var data = bm.GetKartuHasilStudiBM(npm, Tahun);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SKSSUM")]
        public ActionResult SumSKS([FromQuery(Name = "Tahun")] string Tahun)
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                var data = bm.GetSumSKSSemesterBM(npm, Tahun);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ips")]
        public ActionResult IPS([FromQuery(Name = "Tahun")] string Tahun)
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                var data = bm.GetNilaiMahasiswaBM(npm, Tahun);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("jatahsks")]
        public ActionResult JatahSKS([FromQuery(Name = "Tahun")] string Tahun)
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                var mhs = dao.GetDataPrfMhs(npm);
                //var nilai = dao1.GetNilaiMahasiswa(npm, Tahun);
                var data = bm.GetCekJatahSKSBM(mhs.ID_PRODI, "3.20");
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
