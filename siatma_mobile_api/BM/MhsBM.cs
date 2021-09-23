using siatma_mobile_api.DAO;
using siatma_mobile_api.Model;

namespace siatma_mobile_api.BM
{
    public class MhsBM
    {
        MhsDAO dao;
        OutPutApi output;
        public MhsBM()
        {
            dao = new MhsDAO();
            output = new OutPutApi();
        }

        public OutPutApi GetDataPrfMhsBM(string npm)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetDataPrfMhs(npm); ;
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
