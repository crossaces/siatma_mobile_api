using System;
using System.Data.SqlClient;
using Dapper;

namespace siatma_mobile_api.DAO
{
    public class InfoMhsDAO
    {
        public dynamic GetInfoMhsDHS(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT     t.NPM, t.NAMA_MHS, p.PRODI, k.KONSENTRASI_STUDI, ROUND(ip_kumulatif.ipk , 2) as ipk, d.NAMA_DOSEN_LENGKAP
                                FROM         dbo.MST_MHS_AKTIF AS t INNER JOIN
                      dbo.REF_PRODI AS p ON p.ID_PRODI = t.ID_PRODI LEFT OUTER JOIN
                      dbo.TBL_KONSENTRASI_STUDI AS k ON t.ID_KONSENTRASI = k.ID_KONSENTRASI_STUDI INNER JOIN
                      dbo.TBL_TRANSKRIP AS tr ON t.NPM = tr.NPM INNER JOIN
                      dbo.ip_kumulatif ON t.NPM = ip_kumulatif.npm LEFT OUTER JOIN
                      dbo.MST_DOSEN AS d ON t.NPP_PEMBIMBING_AKADEMIK = d.NPP
                      WHERE   (t.NPM = '" + @npm + "')";

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


        public dynamic GetSumSKSSemua(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT SUM(TBL_MATAKULIAH.SKS) AS Total SKS
                         FROM dbo.TBL_KURIKULUM as TBL_KURIKULUM INNER JOIN 
                         dbo.TBL_MATAKULIAH  as TBL_MATAKULIAH ON (TBL_KURIKULUM.ID_KURIKULUM = TBL_MATAKULIAH.ID_KURIKULUM) INNER JOIN 
                         dbo.TBL_TRANSKRIP_DETAIL as TBL_TRANSKRIP_DETAIL ON (TBL_MATAKULIAH.ID_MK = TBL_TRANSKRIP_DETAIL.ID_MK)INNER JOIN 
                         dbo.TBL_TRANSKRIP as TBL_TRANSKRIP ON (TBL_TRANSKRIP_DETAIL.ID_TRANSKRIP = TBL_TRANSKRIP.ID_TRANSKRIP) 
                         WHERE  (TBL_KURIKULUM.ISCURRENT = 1) AND (TBL_TRANSKRIP.NPM = '" + @npm + "') AND (TBL_MATAKULIAH.KD_SIFAT_MK = 'W') AND (TBL_TRANSKRIP_DETAIL.NILAI IS NOT NULL) OR (TBL_KURIKULUM.ISCURRENT = 1) AND(TBL_TRANSKRIP.NPM = '" + @npm + "') AND(TBL_MATAKULIAH.KD_SIFAT_MK = 'P') AND (TBL_TRANSKRIP_DETAIL.NILAI IS NOT NULL)";

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
