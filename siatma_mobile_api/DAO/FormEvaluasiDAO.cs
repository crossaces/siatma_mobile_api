using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using siatma_mobile_api.Model;

namespace siatma_mobile_api.DAO
{
    public class FormEvaluasiDAO
    {
        public dynamic GetDataEvaluasi(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT DISTINCT 
                      sa.SEMESTER_AKADEMIK AS SEMESTER, k.KODE_MK AS KODE, k.NAMA_MK AS MATAKULIAH, k.KELAS,d.NAMA_DOSEN_LENGKAP AS DOSEN, 
					  COALESCE (e.NAMA_DOSEN_LENGKAP, e.NAMA_DOSEN_LENGKAP, e.NAMA_DOSEN_LENGKAP, '-') as DOSEN_2, KR.ID_KRS,
					  CASE WHEN (SELECT id_krs FROM dbo.TBL_JAWABAN_EVALUASI where id_krs = KR.id_krs) > 0 THEN 'True' Else 'False' END as Status
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



        public dynamic GetDataForm()
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT NAMA_FORM,TGL_MULAI,TGL_SELESAI,JAM_MULAI,JAM_SELESAI, ID_FORM_EVALUASI from TBL_FORM_EVALUASI where ID_FORM_EVALUASI=(Select ID_FORM_EVALUASI FROM TBL_FORM_EVALUASI WHERE IS_AKTIF = 'True')";
                var data = conn.QuerySingleOrDefault<dynamic>(query);

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



        public List<Pertanyaan> GetDataPertanyaan()
        {
            
            using(SqlConnection conn = new SqlConnection(DBKoneksi.koneksi)) { 
                
                string query = @"SELECT p.ID_JENIS_PERTANYAAN as ID_Jenis_Pertanyaan, p.id_pertanyaan as ID_Pertanyaan, p.soal as Soal from TBL_PERTANYAAN p 
                INNER join TBL_FORM_EVALUASI f on p.ID_FORM_EVALUASI=f.ID_FORM_EVALUASI  where p.ID_FORM_EVALUASI=(Select ID_FORM_EVALUASI FROM TBL_FORM_EVALUASI WHERE IS_AKTIF = 'True')";


                var data = conn.Query<Pertanyaan>(query).ToList();
                conn.Dispose();
                return data;
            }
         
                
            
        }


        public List<Jawaban> GetDataJawaban()
        {


            using (SqlConnection conn = new SqlConnection(DBKoneksi.koneksi))
            {

                string query = @"SELECT j.jawaban as Text, j.nilai as Nilai, p.id_pertanyaan as ID_Pertanyaan from TBL_PERTANYAAN p 
                INNER join tbl_jawaban j on p.ID_PERTANYAAN= j.ID_PERTANYAAN 
                INNER join TBL_FORM_EVALUASI f on p.ID_FORM_EVALUASI=f.ID_FORM_EVALUASI  where p.ID_FORM_EVALUASI=(Select ID_FORM_EVALUASI FROM TBL_FORM_EVALUASI WHERE IS_AKTIF = 'True')
";


                var data = conn.Query<Jawaban>(query).ToList();
                conn.Dispose();
                return data;
            }
           
        }

       


        public dynamic SubmitForm(string idkrs,List<FormEvaluasi> jawaban)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"insert into TBL_JAWABAN_EVALUASI values(Cast(GETDATE() AS DATE) , CONVERT(VARCHAR(8), GETDATE(), 108), @idkrs)";
                var param = new { idkrs = idkrs };
                conn.Query(query, param);
                string query2 = @"SELECT ID_JAWABAN_EVALUASI FROM TBL_JAWABAN_EVALUASI WHERE ID_KRS = @idkrs";
             
                var data = conn.QuerySingleOrDefault<dynamic>(query2, param);
                string query3 = @"INSERT INTO[TBL_DETAIL_JAWABAN_EVALUASI] (DETAIL_JAWABAN, ID_JAWABAN_EVALUASI, ID_PERTANYAAN, DETAIL_JAWABAN_NILAI) VALUES(@DETAIL_JAWABAN, '" + data.ID_JAWABAN_EVALUASI + "', @ID_PERTANYAAN, @DETAIL_JAWABAN_NILAI)";
                conn.Execute(query3, jawaban);
                //conn.Query(query3, jawaban);


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
