using siatma_mobile_api.DAO;
using siatma_mobile_api.Model;

namespace siatma_mobile_api.BM
{
    public class JadwalkBM
    {
        JadwalkDAO dao;
        OutPutApi output;
        public JadwalkBM()
        {
            dao = new JadwalkDAO();
            output = new OutPutApi();
        }

        public OutPutApi GetDataJadwalKuliahBM(string npm, string smt)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetDataJadwalKuliah(npm, smt); ;
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


        public OutPutApi GetDataWaktuJadwalKuliahBM(string prodi)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetDataWaktuKuliahAkademik(prodi);
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
