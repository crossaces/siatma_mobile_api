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
					  CASE WHEN (SELECT id_krs FROM dbo.TBL_JAWABAN_EVALUASI where id_krs = KR.id_krs) > 0 THEN CONVERT(BIT, 1) Else CONVERT(BIT, 0) END as Status
                      FROM TBL_KRS AS KR INNER JOIN
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
                
                string query = @"SELECT row_number() OVER (ORDER BY p.id_pertanyaan) Nomor, p.ID_JENIS_PERTANYAAN as ID_Jenis_Pertanyaan, p.id_pertanyaan as ID_Pertanyaan, p.soal as Soal from TBL_PERTANYAAN p 
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

                string query = @"SELECT j.jawaban as Text, j.nilai as Nilai, p.id_pertanyaan as ID_Pertanyaan, j.ID_JAWABAN from TBL_PERTANYAAN p 
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


        public dynamic SUM(string idkrs,int idpertanyaan,int idjawaban, int nilai)
        {
            
            SqlConnection conn = new();
          
          
            try
            {
                
                conn = new SqlConnection(DBKoneksi.koneksi);
              
                string cek = @"SELECT ID_KELAS FROM TBL_DETAIL_HASIL_EVALUASI 
                                    WHERE ID_KELAS = (SELECT ID_KELAS FROM TBL_KRS WHERE ID_KRS = @idkrs) and
                                    ID_JAWABAN = @idjawaban ";
                var paramcek = new { idkrs = idkrs, idjawaban = idjawaban };

                var temp = conn.QuerySingleOrDefault<dynamic>(cek, paramcek);
                if (temp != null)
                {

                    string update = @"UPDATE [dbo].[TBL_DETAIL_HASIL_EVALUASI]
                                           SET TOTAL_NILAI = (SELECT Sum(DETAIL_JAWABAN_NILAI) FROM TBL_DETAIL_JAWABAN_EVALUASI INNER JOIN
                                    TBL_JAWABAN_EVALUASI on TBL_JAWABAN_EVALUASI.ID_JAWABAN_EVALUASI = TBL_DETAIL_JAWABAN_EVALUASI.ID_JAWABAN_EVALUASI INNER JOIN
                                    TBL_KRS on TBL_JAWABAN_EVALUASI.ID_KRS = TBL_KRS.ID_KRS INNER JOIN
                                    TBL_PERTANYAAN on TBL_PERTANYAAN.ID_PERTANYAAN = TBL_DETAIL_JAWABAN_EVALUASI.ID_PERTANYAAN
                                    WHERE TBL_PERTANYAAN.ID_FORM_EVALUASI = (SELECT ID_FORM_EVALUASI from TBL_FORM_EVALUASI where ID_FORM_EVALUASI=(Select ID_FORM_EVALUASI FROM TBL_FORM_EVALUASI WHERE IS_AKTIF = 'True')) AND
                                    TBL_KRS.ID_KELAS =  (SELECT ID_KELAS FROM TBL_KRS WHERE ID_KRS = @idkrs)
                                    and TBL_PERTANYAAN.ID_PERTANYAAN = @IDPERTANYAAN
                                    and TBL_DETAIL_JAWABAN_EVALUASI.DETAIL_JAWABAN_NILAI = @Nilai)           
                                    WHERE ID_KELAS = (SELECT ID_KELAS FROM TBL_KRS WHERE ID_KRS = @idkrs) and ID_JAWABAN = @IDJAWABAN";
                    var paramu = new { idkrs = idkrs, IDPERTANYAAN = idpertanyaan, Nilai = nilai, IDJAWABAN = idjawaban };
                    var data = conn.Execute(update, paramu);
                    return data;
                }
                else
                {
                    string insert = @"INSERT INTO [dbo].[TBL_DETAIL_HASIL_EVALUASI]
                                       (TOTAL_NILAI
                                       ,ID_FORM_EVALUASI
                                       ,ID_PERTANYAAN
                                       ,ID_JAWABAN
                                       ,ID_KELAS)
                                        VALUES
                                   ((SELECT Sum(DETAIL_JAWABAN_NILAI) FROM TBL_DETAIL_JAWABAN_EVALUASI INNER JOIN
                                   TBL_JAWABAN_EVALUASI on TBL_JAWABAN_EVALUASI.ID_JAWABAN_EVALUASI = TBL_DETAIL_JAWABAN_EVALUASI.ID_JAWABAN_EVALUASI INNER JOIN
                                   TBL_KRS on TBL_JAWABAN_EVALUASI.ID_KRS = TBL_KRS.ID_KRS INNER JOIN
                                   TBL_PERTANYAAN on TBL_PERTANYAAN.ID_PERTANYAAN = TBL_DETAIL_JAWABAN_EVALUASI.ID_PERTANYAAN
                                   WHERE TBL_PERTANYAAN.ID_FORM_EVALUASI = (SELECT ID_FORM_EVALUASI from TBL_FORM_EVALUASI where ID_FORM_EVALUASI=(Select ID_FORM_EVALUASI FROM TBL_FORM_EVALUASI WHERE IS_AKTIF = 'True')) AND
                                   TBL_KRS.ID_KELAS = (SELECT ID_KELAS FROM TBL_KRS WHERE ID_KRS = @idkrs) 
                                   and TBL_PERTANYAAN.ID_PERTANYAAN =@IDPERTANYAAN  and TBL_DETAIL_JAWABAN_EVALUASI.DETAIL_JAWABAN_NILAI = @Nilai) ,
                                   (SELECT ID_FORM_EVALUASI from TBL_FORM_EVALUASI where ID_FORM_EVALUASI=(Select ID_FORM_EVALUASI FROM TBL_FORM_EVALUASI WHERE IS_AKTIF = 'True')),@IDPERTANYAAN, @IDJAWABAN,
                                   (SELECT ID_KELAS FROM TBL_KRS WHERE ID_KRS = @idkrs))";

                            var param = new { idkrs = idkrs, IDPERTANYAAN = idpertanyaan, Nilai = nilai, IDJAWABAN = idjawaban };
                            var data =  conn.Query(insert, param);
                    return data;
                }             

               
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
