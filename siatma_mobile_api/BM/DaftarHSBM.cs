using siatma_mobile_api.DAO;
using siatma_mobile_api.Model;

namespace siatma_mobile_api.BM
{
    public class DaftarHSBM
    {
        DaftarHSDAO dao;
        OutPutApi output;
        public DaftarHSBM()
        {
            dao = new DaftarHSDAO();
            output = new OutPutApi();
        }

        public OutPutApi GetDaftarHasilstudiBM(string npm)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetDaftarHasilStudi(npm); ;
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
