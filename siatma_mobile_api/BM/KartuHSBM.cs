using siatma_mobile_api.DAO;
using siatma_mobile_api.Model;

namespace siatma_mobile_api.BM
{
    public class KartuHSBM
    {
        KartuHasilStudiDAO dao;
        OutPutApi output;
        public KartuHSBM()
        {
            dao = new KartuHasilStudiDAO();
            output = new OutPutApi();
        }

        public OutPutApi GetKartuHasilStudiBM(string npm, string smt)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetKartuHasilStudi(npm, smt); ;
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

        public OutPutApi GetNilaiMahasiswaBM(string npm, string smt)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetNilaiMahasiswa(npm, smt);
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

        public OutPutApi GetCekJatahSKSBM(string prodi, string ips)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetJatahSKS(prodi, ips);
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

        public OutPutApi GetSumSKSSemesterBM(string npm, string smt)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetSumSKSStudi(npm, smt);
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
