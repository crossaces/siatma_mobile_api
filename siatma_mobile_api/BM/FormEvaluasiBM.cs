using System.Collections.Generic;
using System.Linq;
using siatma_mobile_api.DAO;
using siatma_mobile_api.Model;

namespace siatma_mobile_api.BM
{
    public class FormEvaluasiBM
    {
        FormEvaluasiDAO dao;
        OutPutApi output;
        public FormEvaluasiBM()
        {
            dao = new FormEvaluasiDAO();
            output = new OutPutApi();
        }

        public OutPutApi GetDataEvaluasiBM(string npm)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetDataEvaluasi(npm);
            if (data != null)
            {

           
                output.pesan = "Berhasil ditemukan";
                output.data = data;
            }
            else
            {
                output.status = false;
                output.pesan = "Data tidak ditemukan";
            }

            return output;
        }



        public OutPutApi GetDataFormBM()
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetDataForm();
            if (data != null)
            {


                output.pesan = "Berhasil ditemukan";
                output.data = data;
            }
            else
            {
                output.status = false;
                output.pesan = "Data tidak ditemukan";
            }

            return output;
        }


        public OutPutApi SubmitFormBM(string idkrs, List<FormEvaluasi> jawaban)
        {

            output.status =false;
            output.pesan = "Gagal Submit";

            output.status = true;
            output.pesan = "Berhasil Submit";
            if (idkrs != null)
            {
                
                output.data = dao.SubmitForm(idkrs,jawaban);
            }
           

            return output;
        }


        public OutPutApi GetDataPertanyaanBM()
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";
            List<Pertanyaan> pertanyaan;
            List<Jawaban> jawaban;
         
            pertanyaan = dao.GetDataPertanyaan();
            jawaban = dao.GetDataJawaban();


            if (pertanyaan != null)
            {
                output.pesan = "Berhasil ditemukan";


                foreach(var row in pertanyaan)
                {
                    var filtersub = jawaban.Where(x => x.ID_PERTANYAAN == row.ID_PERTANYAAN).ToList();
                    row.Jawaban = filtersub;
                }

                output.data = pertanyaan;



            }
            else
            {
                output.status = false;
                output.pesan = "Data tidak ditemukan";
            }

            return output;
        }


        public OutPutApi GetDataJawabanBM()
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetDataJawaban();

            if (data != null)
            {
                output.pesan = "Berhasil ditemukan";
                output.data = data;
            }
            else
            {
                output.status = false;
                output.pesan = "Data tidak ditemukan";
            }

            return output;
        }



        public OutPutApi Isijawaban(List<FormEvaluasi> data)
        {
            output.status = true;
            output.pesan = "Berhasil Mengambil data";


            if (data!=null)
            {
                output.pesan = "Berhasil ditemukan";
                output.data = data;
            }
            else
            {
                output.status = false;
                output.pesan = "Data tidak ditemukan";
            }

            return output;
        }
    }
}
