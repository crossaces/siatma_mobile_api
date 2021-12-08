using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using siatma_mobile_api.BM;
using siatma_mobile_api.Model;

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
        public ActionResult GetDataEvaluasiMahasiswa()
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


        [HttpGet("Form")]
        public ActionResult GetDataFormEvaluasi()
        {
            try
            {
                var npm = User.Claims
                        .Where(c => c.Type == "username")
                            .Select(c => c.Value).SingleOrDefault();
                var data = bm.GetDataFormBM();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("Submit")]
        public ActionResult SubmitForm(Submit submit)
        {
            try
            {
                var data = bm.SubmitFormBM(submit.Idkrs, submit.Jawaban);
                Task.Run(() => bm.SumBM(submit.Idkrs));                              
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("Pertanyaan")]
        public ActionResult GetPertanyaan()
        {
            try
            {
            
                var data = bm.GetDataPertanyaanBM();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("isiform")]
        public ActionResult isiJawaban([FromBody] List<FormEvaluasi> jsonData)
        {
            try
            {
                
                var data = bm.Isijawaban(jsonData); 

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
