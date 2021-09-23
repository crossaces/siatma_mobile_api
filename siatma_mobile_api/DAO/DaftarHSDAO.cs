using System;
using System.Data.SqlClient;
using Dapper;

namespace siatma_mobile_api.DAO
{
    public class DaftarHSDAO
    {
        public dynamic GetDaftarHasilStudi(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT     TBL_MATAKULIAH.KODE_MK as KODE, TBL_MATAKULIAH.NAMA_MK as MATAKULIAH, TBL_MATAKULIAH.SKS 
                                ,COALESCE (TBL_TRANSKRIP_DETAIL.NILAI, TBL_TRANSKRIP_DETAIL.NILAI, TBL_TRANSKRIP_DETAIL.NILAI, '-') AS NILAI 
                                FROM         dbo.TBL_MATAKULIAH as TBL_MATAKULIAH INNER JOIN 
                                dbo.TBL_TRANSKRIP_DETAIL as TBL_TRANSKRIP_DETAIL INNER JOIN
                                dbo.TBL_TRANSKRIP as TBL_TRANSKRIP ON TBL_TRANSKRIP_DETAIL.ID_TRANSKRIP = TBL_TRANSKRIP.ID_TRANSKRIP ON  
                                TBL_MATAKULIAH.ID_MK = TBL_TRANSKRIP_DETAIL.ID_MK INNER JOIN 
                                dbo.TBL_KURIKULUM as TBL_KURIKULUM ON TBL_MATAKULIAH.ID_KURIKULUM = TBL_KURIKULUM.ID_KURIKULUM 
                                WHERE     (TBL_TRANSKRIP.NPM = '" + npm + "') AND (TBL_TRANSKRIP_DETAIL.ISTERPILIH = 1) AND (TBL_KURIKULUM.ISCURRENT = 1) and(((tbl_matakuliah.kd_sifat_mk = 'P' and tbl_transkrip_detail.nilai is not null) or tbl_matakuliah.kd_sifat_mk = 'W'))" +
                                "ORDER BY TBL_MATAKULIAH.SEMESTER, TBL_MATAKULIAH.KODE_MK ";

                var param = new { npm = npm };

                var data = conn.Query(query, param);

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
