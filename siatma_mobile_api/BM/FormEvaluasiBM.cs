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

            var data = dao.getDataEvaluasi(npm);
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

      
    }
}
