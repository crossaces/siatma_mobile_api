using siatma_mobile_api.DAO;
using siatma_mobile_api.Model;

namespace siatma_mobile_api.BM
{
    public class JadwaluBM
    {
        JadwaluDAO dao;
        OutPutApi output;
        public JadwaluBM()
        {
            dao = new JadwaluDAO();
            output = new OutPutApi();
        }

        public OutPutApi GetDataJadwalUjianBM(string npm, string smt)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetDataJadwaluKuliah(npm, smt);
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

        public OutPutApi GetDataWaktuJadwalUjianBM(string prodi)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetDataWaktuJadwaluKuliah(prodi);
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
