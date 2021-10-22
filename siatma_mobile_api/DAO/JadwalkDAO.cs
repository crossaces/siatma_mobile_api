using System;
using System.Data.SqlClient;
using Dapper;
namespace siatma_mobile_api.DAO
{
    public class JadwalkDAO
    {
        public dynamic GetDataJadwalKuliah(string npm, string smt)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT dbo.TBL_KELAS.KODE_MK AS KODE, dbo.TBL_KELAS.NAMA_MK AS MATAKULIAH, dbo.TBL_KELAS.KELAS AS KLS, dbo.MST_DOSEN.NAMA_DOSEN_LENGKAP AS DOSEN, 
                      dbo.MST_RUANG.RUANG, COALESCE (REF_HARI.HARI + ' - ' + REF_SESI.SESI, REF_HARI.HARI + ' - ' + REF_SESI.SESI, REF_HARI.HARI + ' - ' + REF_SESI.SESI, '-') 
                      AS [Jadwal 1], COALESCE (REF_HARI_1.HARI + ' - ' + REF_SESI_1.SESI, REF_HARI_1.HARI + ' - ' + REF_SESI_1.SESI, REF_HARI_1.HARI + ' - ' + REF_SESI_1.SESI, 
                      '-') AS [Jadwal 2], COALESCE (REF_HARI_3.HARI + ' - ' + REF_SESI_2.SESI, REF_HARI_3.HARI + ' - ' + REF_SESI_2.SESI, 
                      REF_HARI_3.HARI + ' - ' + REF_SESI_2.SESI, '-') AS [Jadwal 3], COALESCE (REF_HARI_2.HARI + ' - ' + REF_SESI_3.SESI, 
                      REF_HARI_2.HARI + ' - ' + REF_SESI_3.SESI, REF_HARI_2.HARI + ' - ' + REF_SESI_3.SESI, '-') AS [Jadwal 4], dbo.TBL_KELAS.NAMA_MK
FROM         dbo.REF_HARI AS REF_HARI_1 RIGHT OUTER JOIN
                      dbo.REF_HARI AS REF_HARI_2 RIGHT OUTER JOIN
                      dbo.REF_SESI RIGHT OUTER JOIN
                      dbo.MST_DOSEN INNER JOIN
                      dbo.TBL_KRS INNER JOIN
                      dbo.TBL_KELAS ON dbo.TBL_KRS.ID_KELAS = dbo.TBL_KELAS.ID_KELAS INNER JOIN
                      dbo.TBL_SEMESTER_AKADEMIK ON dbo.TBL_KELAS.ID_TAHUN_AKADEMIK = dbo.TBL_SEMESTER_AKADEMIK.ID_TAHUN_AKADEMIK AND 
                      dbo.TBL_KELAS.NO_SEMESTER = dbo.TBL_SEMESTER_AKADEMIK.NO_SEMESTER ON dbo.MST_DOSEN.NPP = dbo.TBL_KELAS.NPP_DOSEN1 INNER JOIN
                      dbo.MST_RUANG ON dbo.TBL_KELAS.RUANG1 = dbo.MST_RUANG.RUANG LEFT OUTER JOIN
                      dbo.REF_SESI AS REF_SESI_3 ON dbo.TBL_KELAS.ID_SESI_KULIAH4 = REF_SESI_3.ID_SESI LEFT OUTER JOIN
                      dbo.REF_SESI AS REF_SESI_1 ON dbo.TBL_KELAS.ID_SESI_KULIAH2 = REF_SESI_1.ID_SESI ON dbo.REF_SESI.ID_SESI = dbo.TBL_KELAS.ID_SESI_KULIAH1 LEFT OUTER JOIN
                      dbo.REF_HARI AS REF_HARI_3 ON dbo.TBL_KELAS.ID_HARI4 = REF_HARI_3.ID_HARI ON REF_HARI_2.ID_HARI = dbo.TBL_KELAS.ID_HARI3 ON 
                      REF_HARI_1.ID_HARI = TBL_KELAS.ID_HARI2 LEFT OUTER JOIN
                      REF_HARI ON TBL_KELAS.ID_HARI1 = REF_HARI.ID_HARI LEFT OUTER JOIN
                      REF_SESI AS REF_SESI_2 ON dbo.TBL_KELAS.ID_SESI_KULIAH3 = REF_SESI_2.ID_SESI
WHERE     (dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk = '" + @smt + "') AND (dbo.TBL_KRS.NPM = '" + @npm + "') and dbo.TBL_KELAS.ID_KELAS not in (select ID_KELAS_ASAL   FROM dbo.TBL_KRS INNER JOIN dbo.TBL_KELAS ON dbo.TBL_KRS.ID_KELAS = dbo.TBL_KELAS.ID_KELAS WHERE  (dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk = '" + @smt + "') and npm = '" + @npm + "' and id_kelas_asal is not null )" +
"ORDER BY REF_HARI.ID_HARI, REF_SESI.ID_SESI, REF_HARI_1.ID_HARI, REF_SESI_1.ID_SESI, REF_HARI_3.ID_HARI, REF_SESI_2.ID_SESI,REF_HARI_2.ID_HARI, REF_SESI_3.ID_SESI";

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



        public dynamic GetDataTahunAkademik(string masuk, string prodi)
        {
            SqlConnection conn = new();
            string query;
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);


                if (prodi == "70")
                {
                    query = @"SELECT     dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk 
                    FROM         dbo.TBL_SEMESTER_AKADEMIK INNER JOIN  
                    dbo.TBL_TAHUN_AKADEMIK ON dbo.TBL_SEMESTER_AKADEMIK.ID_TAHUN_AKADEMIK = dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK 
                    WHERE     (dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK >= '" + masuk + "') ORDER BY dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK DESC, dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk DESC";
                }
                else
                {
                    query = @"SELECT     dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk
                    FROM         dbo.TBL_SEMESTER_AKADEMIK INNER JOIN
                                    dbo.TBL_TAHUN_AKADEMIK ON dbo.TBL_SEMESTER_AKADEMIK.ID_TAHUN_AKADEMIK = dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK
                    WHERE     (dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK >='" + masuk + "') ORDER BY dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK DESC, dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk DESC";

                }


                var param = new { masuk = masuk };
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

        public dynamic GetDataWaktuKuliahAkademik(string prodi)
        {
            SqlConnection conn = new();
            string query;
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);


                if (prodi.CompareTo("07") == 0 || prodi.CompareTo("06") == 0 || prodi.CompareTo("14") == 0 || prodi.CompareTo("16") == 0)
                {
                    query = @"SELECT   Sesi, jam_masuk as Masuk,
                            case when (jam_keluar_seharusnya %60 = 0) then  
                            (cast(jam_keluar_seharusnya/60 as varchar) +':00')
                            else
                            (cast(jam_keluar_seharusnya/60 as varchar) +':'+cast(jam_keluar_seharusnya % 60 as varchar))  
                            END Keluar
                             from dbo.sesi_kelas_fti where id_sesi in('411','412','413','414','415','416','431')
                            ";
                }
                else
                {
                    query = @"SELECT    Sesi, jam_Masuk as Masuk, jam_Keluar as Keluar
                                FROM         sesi_kelas
                                WHERE     (ID_PRODI = '" + prodi + "') AND (JENIS_SESI = 'Kuliah')";

                }


                var param = new { prodi = prodi };
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
