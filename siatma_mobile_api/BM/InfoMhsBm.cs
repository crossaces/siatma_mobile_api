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

        public OutPutApi getInfoPembayaran(string npm)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetInfoPembayaran(npm);
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



        public OutPutApi getBeritaBM(string prodi,int fakultas)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetBerita(prodi,fakultas);
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

        public OutPutApi getInfoMahasiswa(string npm)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetInfoMhsDHS(npm);
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




        public OutPutApi GetJumlahSKSdanMatkul(string npm)
        {

            output.status = true;
            output.pesan = "Berhasil Mengambil data";

            var data = dao.GetSumSKSSemua(npm);
            var data1 = dao.GetMatakuliahSemua(npm);
            var data2= dao.getNilaiE(npm);
            if (data != null && data1 != null && data2 != null)
            {
                output.pesan = "Berhasil ditemukan";
                output.data = new { totalsks = data.TotalSKS, totalmatkul = data1.JumlahMatkul, nilaiE = data2.nilaiE };


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
