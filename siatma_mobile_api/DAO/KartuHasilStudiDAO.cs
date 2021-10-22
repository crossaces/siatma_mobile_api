using System;
using System.Data.SqlClient;
using Dapper;

namespace siatma_mobile_api.DAO
{
    public class KartuHasilStudiDAO
    {
        public dynamic GetKartuHasilStudi(string npm, string smt)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT     dbo.TBL_KELAS.NAMA_MK AS MATAKULIAH, dbo.TBL_KELAS.KODE_MK AS KODE, dbo.TBL_KELAS.SKS, COALESCE (dbo.TBL_KRS.NILAI, dbo.TBL_KRS.NILAI, 
                      dbo.TBL_KRS.NILAI, '***') AS NILAI, 
                      CASE TBL_KRS.NILAI 
                      WHEN 'A' THEN CONVERT(varchar(7), TBL_KELAS.SKS * 4) 
                      WHEN 'A-' THEN CONVERT(varchar(7), TBL_KELAS.SKS * 3.7) 
                      WHEN 'B+' THEN CONVERT(varchar(7), TBL_KELAS.SKS * 3.3) 
                      WHEN 'B' THEN CONVERT(varchar(7), TBL_KELAS.SKS * 3) 
                      WHEN 'B-' THEN CONVERT(varchar(7), TBL_KELAS.SKS * 2.7) 
                      WHEN 'C+' THEN CONVERT(varchar(7), TBL_KELAS.SKS * 2.3) 
                      WHEN 'C' THEN CONVERT(varchar(7), TBL_KELAS.SKS * 2)  
                      WHEN 'D' THEN CONVERT(varchar(7), TBL_KELAS.SKS * 1.0) 
                      WHEN 'E' THEN CONVERT(varchar(7), TBL_KELAS.SKS * 0) ELSE '***' END AS BOBOT, 
                      CASE dbo.TBL_KRS.IS_REMIDI WHEN '1' THEN 'R' ELSE '*' END AS REMIDI,TBL_KRS.NILAI_REMIDI
                      FROM         dbo.TBL_KELAS INNER JOIN
                      dbo.TBL_KRS ON dbo.TBL_KELAS.ID_KELAS = dbo.TBL_KRS.ID_KELAS INNER JOIN
                      dbo.TBL_SEMESTER_AKADEMIK ON dbo.TBL_KELAS.ID_TAHUN_AKADEMIK = dbo.TBL_SEMESTER_AKADEMIK.ID_TAHUN_AKADEMIK AND 
                      dbo.TBL_KELAS.NO_SEMESTER = dbo.TBL_SEMESTER_AKADEMIK.NO_SEMESTER
                      WHERE (TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk = '" + @smt + "') AND (TBL_KRS.NPM ='" + @npm + "') Order by MATAKULIAH";

                var param = new { npm = npm, smt = smt };

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

        public dynamic GetSumSKSStudi(string npm, string smt)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT SUM(TBL_KELAS.SKS) AS Total 
                                FROM         dbo.TBL_KELAS as TBL_KELAS INNER JOIN 
                      dbo.TBL_KRS as TBL_KRS ON (TBL_KELAS.ID_KELAS = TBL_KRS.ID_KELAS) INNER JOIN 
                      dbo.TBL_SEMESTER_AKADEMIK as TBL_SEMESTER_AKADEMIK ON TBL_KELAS.ID_TAHUN_AKADEMIK = TBL_SEMESTER_AKADEMIK.ID_TAHUN_AKADEMIK AND 
                      TBL_KELAS.NO_SEMESTER = TBL_SEMESTER_AKADEMIK.NO_SEMESTER
                      WHERE     (TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk = '" + @smt + "') AND(TBL_KRS.NPM = '" + @npm + "')";

                var param = new { npm = npm, smt = smt };

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


        public dynamic GetNilaiMahasiswa(string npm, string smt)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT SUM(TBL_KELAS.SKS) SKS, 
                BOBOT = SUM(case TBL_KRS.NILAI 							
                when 'A' then TBL_KELAS.SKS*4 							
                when 'A-' then TBL_KELAS.SKS*3.7 							
                when 'B+' then TBL_KELAS.SKS*3.3 							
                when 'B' then TBL_KELAS.SKS*3 							
                when 'B-' then TBL_KELAS.SKS*2.7 							
                when 'C+' then TBL_KELAS.SKS*2.3 							
                when 'C' then TBL_KELAS.SKS*2 							
                when 'C-' then TBL_KELAS.SKS*1.7 							
                when 'D+' then TBL_KELAS.SKS*1.3 							
                when 'D' then TBL_KELAS.SKS*1 							
                when 'D-' then TBL_KELAS.SKS*0.7 							
                when 'E+' then TBL_KELAS.SKS*0.3 							
                when 'E' then TBL_KELAS.SKS*0 			
                else TBL_KELAS.SKS*0 END), 
                IPS = round(SUM(case TBL_KRS.NILAI 							
                when 'A' then TBL_KELAS.SKS*4 							
                when 'A-' then TBL_KELAS.SKS*3.7 							
                when 'B+' then TBL_KELAS.SKS*3.3 							
                when 'B' then TBL_KELAS.SKS*3 							
                when 'B-' then TBL_KELAS.SKS*2.7 							
                when 'C+' then TBL_KELAS.SKS*2.3 							
                when 'C' then TBL_KELAS.SKS*2 							
                when 'C-' then TBL_KELAS.SKS*1.7 							
                when 'D+' then TBL_KELAS.SKS*1.3 							
                when 'D' then TBL_KELAS.SKS*1 							
                when 'D-' then TBL_KELAS.SKS*0.7 							
                when 'E+' then TBL_KELAS.SKS*0.3 							
                when 'E' then TBL_KELAS.SKS*0 			
                else TBL_KELAS.SKS*0 END)/SUM(TBL_KELAS.SKS),2) 
                FROM dbo.TBL_KELAS as TBL_KELAS INNER JOIN                       
                dbo.TBL_KRS as TBL_KRS ON (TBL_KELAS.ID_KELAS = TBL_KRS.ID_KELAS) 
                INNER JOIN                       dbo.TBL_SEMESTER_AKADEMIK as  TBL_SEMESTER_AKADEMIK
                ON TBL_KELAS.ID_TAHUN_AKADEMIK = TBL_SEMESTER_AKADEMIK.ID_TAHUN_AKADEMIK 
                AND                       TBL_KELAS.NO_SEMESTER = TBL_SEMESTER_AKADEMIK.NO_SEMESTER 
                WHERE  (TBL_KRS.NPM = '" + @npm + "') GROUP BY TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk HAVING (TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk = '" + @smt + "')";

                var param = new { npm = npm, smt = smt };

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


        public dynamic GetJatahSKS(string prodi, string ips)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"select ip_awal, ip_akhir, sks_diambil from ref_jatah_sks where cast(replace('" + @ips + "',',','.') as float) between ip_awal and ip_akhir and id_prodi = '" + @prodi + "'";

                var param = new { ips = ips, prodi = prodi };

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



