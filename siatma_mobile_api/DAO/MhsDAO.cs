using System;
using System.Data.SqlClient;
using Dapper;

namespace siatma_mobile_api.DAO
{
    public class MhsDAO
    {
       
        public dynamic GetDataPrfMhs(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT dbo.MST_MHS_AKTIF.NPM, SUBSTRING(dbo.MST_MHS_AKTIF.NAMA_MHS,1,charindex(' ',dbo.MST_MHS_AKTIF.NAMA_MHS)-1) AS PANGGILAN, dbo.MST_MHS_AKTIF.ID_FAKULTAS, dbo.MST_DOSEN.NAMA_DOSEN_LENGKAP,
                                dbo.MST_MHS_AKTIF.NAMA_MHS, dbo.MST_MHS_AKTIF.ALAMAT, dbo.MST_MHS_AKTIF.TMP_LAHIR, dbo.MST_MHS_AKTIF.TGL_LAHIR, YEAR(GETDATE()) - dbo.MST_MHS_AKTIF.THN_MASUK AS lama, dbo.MST_MHS_AKTIF.ID_PRODI, dbo.MST_MHS_AKTIF.THN_MASUK,
                                dbo.MST_MHS_AKTIF.KD_STATUS_MHS, dbo.MST_MHS_FOTO.FOTO, dbo.REF_PRODI.PRODI, dbo.REF_FAKULTAS.FAKULTAS,
                                tbl_induk_mhs.almtjogja AS ALAMAT_JOGJA,
								tbl_induk_mhs.almtortu as ALAMAT_ORTU, tbl_induk_mhs.namaortu as NAMA_ORTU,tbl_induk_mhs.agama as AGAMA,tbl_induk_mhs.namasma as SMA
                                FROM dbo.MST_MHS_AKTIF LEFT OUTER JOIN
                                dbo.MST_MHS_FOTO ON dbo.MST_MHS_AKTIF.NPM = dbo.MST_MHS_FOTO.NPM LEFT OUTER JOIN
                                dbo.MST_ORTU ON dbo.MST_MHS_AKTIF.ID_ORTU = dbo.MST_ORTU.ID_ORTU LEFT OUTER JOIN
                                dbo.REF_PRODI ON dbo.MST_MHS_AKTIF.ID_PRODI = dbo.REF_PRODI.ID_PRODI LEFT OUTER JOIN
                                dbo.MST_DOSEN ON dbo.MST_MHS_AKTIF.NPP_PEMBIMBING_AKADEMIK = dbo.MST_DOSEN.NPP LEFT OUTER JOIN
								tbl_induk_mhs ON dbo.MST_MHS_AKTIF.NPM = tbl_induk_mhs.npm  LEFT OUTER JOIN
                                dbo.REF_FAKULTAS ON dbo.REF_PRODI.ID_FAKULTAS = dbo.REF_FAKULTAS.ID_FAKULTAS
                                WHERE (dbo.MST_MHS_AKTIF.NPM = '" + npm + "') AND KD_STATUS_MHS ='A'";

                var param = new { npm = npm };
                var data = conn.QuerySingleOrDefault<dynamic>(query, param);

                return data;
            }
            catch (Exception ex)
            {
                return ex;
            }
            finally
            {
                conn.Dispose();
            }
        }
    }
}
