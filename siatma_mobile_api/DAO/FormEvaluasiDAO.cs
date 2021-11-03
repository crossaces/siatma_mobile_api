using System;
using System.Data.SqlClient;
using Dapper;

namespace siatma_mobile_api.DAO
{
    public class FormEvaluasiDAO
    {
        public dynamic getDataEvaluasi(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT DISTINCT 
                      sa.SEMESTER_AKADEMIK AS SEMESTER, k.KODE_MK AS KODE, k.NAMA_MK AS MATAKULIAH, k.KELAS,d.NAMA_DOSEN_LENGKAP AS DOSEN, 
					  COALESCE (e.NAMA_DOSEN_LENGKAP, e.NAMA_DOSEN_LENGKAP, e.NAMA_DOSEN_LENGKAP, '-') as DOSEN_2, KR.ID_KRS
                      
FROM         TBL_KRS AS KR INNER JOIN
                      TBL_KELAS AS k ON k.ID_KELAS = KR.ID_KELAS INNER JOIN
                      TBL_SEMESTER_AKADEMIK_EVALUASI AS sa ON k.ID_TAHUN_AKADEMIK = sa.ID_TAHUN_AKADEMIK AND k.NO_SEMESTER = sa.NO_SEMESTER INNER JOIN
                      MST_DOSEN AS d ON d.NPP = k.NPP_DOSEN1 INNER JOIN
                      REF_DETAIL_EVALUASI ON k.ID_KELAS = REF_DETAIL_EVALUASI.ID_KELAS left outer JOIN
                      MST_DOSEN as e ON k.NPP_DOSEN2 = e.NPP
WHERE (sa.ISCURRENT = 1) and (KR.NPM = @npm) order by ID_KRS";

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
