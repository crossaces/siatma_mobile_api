using siatma_mobile_api.DAO;
using siatma_mobile_api.Model;

namespace siatma_mobile_api.BM
{
    public class InfoMhsBm
    {

        InfoMhsDAO dao;
        OutPutApi output;
        public InfoMhsBm()
        {
            dao = new InfoMhsDAO();
            output = new OutPutApi();
        }
        public OutPutApi GetTahunAkademik(string masuk, string prodi)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetDataTahunAkademik(masuk, prodi);
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


        public OutPutApi GetPresensiMahasiswa(string npm, string semester)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetPresensiMahasiswa(npm,semester);
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
