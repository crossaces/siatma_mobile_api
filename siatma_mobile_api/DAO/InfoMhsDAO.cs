using System;
using System.Data.SqlClient;
using Dapper;

namespace siatma_mobile_api.DAO
{
    public class InfoMhsDAO
    {

        public dynamic GetPresensiMahasiswa(string npm, string semester)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @" SELECT TBL_KELAS.NAMA_MK AS[MATA KULIAH], TBL_KELAS.KELAS AS KLS,
                          coalesce(TBL_PRESENSI.Status1, TBL_PRESENSI.Status1, TBL_PRESENSI.Status1,'-') AS[1],
                         coalesce(TBL_PRESENSI.Status2, TBL_PRESENSI.Status2, TBL_PRESENSI.Status2,'-') AS[2], 
                         coalesce(TBL_PRESENSI.Status3, TBL_PRESENSI.Status3, TBL_PRESENSI.Status3,'-') AS[3], 
                         coalesce(TBL_PRESENSI.Status4, TBL_PRESENSI.Status4, TBL_PRESENSI.Status4,'-') AS[4], 
                         coalesce(TBL_PRESENSI.Status5, TBL_PRESENSI.Status5, TBL_PRESENSI.Status5,'-') AS[5], 
                         coalesce(TBL_PRESENSI.Status6, TBL_PRESENSI.Status6, TBL_PRESENSI.Status6,'-') AS[6], 
                         coalesce(TBL_PRESENSI.Status7, TBL_PRESENSI.Status7, TBL_PRESENSI.Status7,'-') AS[7], 
                         coalesce(TBL_PRESENSI.Status8, TBL_PRESENSI.Status8, TBL_PRESENSI.Status8,'-') AS[8], 
                         coalesce(TBL_PRESENSI.Status9, TBL_PRESENSI.Status9, TBL_PRESENSI.Status9,'-') AS[9], 
                         coalesce(TBL_PRESENSI.Status10, TBL_PRESENSI.Status10, TBL_PRESENSI.Status10,'-') AS[10],
                         coalesce(TBL_PRESENSI.Status11, TBL_PRESENSI.Status11, TBL_PRESENSI.Status11,'-') AS[11], 
                         coalesce(TBL_PRESENSI.Status12, TBL_PRESENSI.Status12, TBL_PRESENSI.Status12,'-') AS[12], 
                         coalesce(TBL_PRESENSI.Status13, TBL_PRESENSI.Status13, TBL_PRESENSI.Status13,'-') AS[13],
                         coalesce(TBL_PRESENSI.Status14, TBL_PRESENSI.Status14, TBL_PRESENSI.Status14,'-') AS[14], 
                         coalesce(TBL_PRESENSI.Status15, TBL_PRESENSI.Status15, TBL_PRESENSI.Status15,'-') AS[15], 
                         coalesce(TBL_PRESENSI.Status16, TBL_PRESENSI.Status16, TBL_PRESENSI.Status16,'-') AS[16], 
                      coalesce(TBL_PRESENSI.Status17, TBL_PRESENSI.Status17, TBL_PRESENSI.Status17,'-') AS[17],                          
					  coalesce(TBL_PRESENSI.Status18, TBL_PRESENSI.Status18, TBL_PRESENSI.Status18,'-') AS[18], 
                    coalesce(TBL_PRESENSI.Status19, TBL_PRESENSI.Status19, TBL_PRESENSI.Status19,'-') AS[19], 
                         coalesce(TBL_PRESENSI.Status20, TBL_PRESENSI.Status20, TBL_PRESENSI.Status20,'-') AS[20], 
                         coalesce(TBL_PRESENSI.Status21, TBL_PRESENSI.Status21, TBL_PRESENSI.Status21,'-') AS[21],                         
						 coalesce(TBL_PRESENSI.Status22, TBL_PRESENSI.Status22, TBL_PRESENSI.Status22,'-') AS[22], 
                         coalesce(TBL_PRESENSI.Status23, TBL_PRESENSI.Status23, TBL_PRESENSI.Status23,'-') AS[23], 
                         coalesce(TBL_PRESENSI.Status24, TBL_PRESENSI.Status24, TBL_PRESENSI.Status24,'-') AS[24], 
                         coalesce(TBL_PRESENSI.Status25, TBL_PRESENSI.Status25, TBL_PRESENSI.Status25,'-') AS[25],
                         coalesce(TBL_PRESENSI.Status26, TBL_PRESENSI.Status26, TBL_PRESENSI.Status26,'-') AS[26], 
                         coalesce(TBL_PRESENSI.Status27, TBL_PRESENSI.Status27, TBL_PRESENSI.Status27,'-') AS[27], 
                         coalesce(TBL_PRESENSI.Status28, TBL_PRESENSI.Status28, TBL_PRESENSI.Status28,'-') AS[28],
rekap_presensi.hadir, rekap_presensi.Alpha, rekap_presensi.Ijin, rekap_presensi.Total
 FROM TBL_KELAS INNER JOIN rekap_presensi ON rekap_presensi.ID_Kelas = TBL_KELAS.ID_KELAS INNER JOIN
TBL_PRESENSI ON rekap_presensi.ID_Kelas = TBL_PRESENSI.ID_Kelas AND rekap_presensi.ID_Kelas = TBL_PRESENSI.ID_Kelas AND rekap_presensi.NPM = TBL_PRESENSI.NPM INNER JOIN
 TBL_SEMESTER_AKADEMIK ON TBL_SEMESTER_AKADEMIK.NO_SEMESTER = TBL_KELAS.NO_SEMESTER AND TBL_SEMESTER_AKADEMIK.ID_TAHUN_AKADEMIK = TBL_KELAS.ID_TAHUN_AKADEMIK WHERE (TBL_PRESENSI.NPM = @npm )and(SEMESTER_AKADEMIk=@semester) ORDER BY [MATA KULIAH]";

                var param = new { npm = npm , semester = semester};

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
           
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);


                if (prodi == "70")
                {
                    string query = @"SELECT     dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk 
                    FROM         dbo.TBL_SEMESTER_AKADEMIK INNER JOIN  
                    dbo.TBL_TAHUN_AKADEMIK ON dbo.TBL_SEMESTER_AKADEMIK.ID_TAHUN_AKADEMIK = dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK 
                    WHERE     (dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK >= '" + @masuk + "') ORDER BY dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK DESC, dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk DESC";
                    var param = new { masuk = masuk };
                    var data = conn.Query(query, param);

                    return data;
                }
                else
                {
                    string query1 = @"SELECT     dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk
                    FROM         dbo.TBL_SEMESTER_AKADEMIK INNER JOIN
                                    dbo.TBL_TAHUN_AKADEMIK ON dbo.TBL_SEMESTER_AKADEMIK.ID_TAHUN_AKADEMIK = dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK
                    WHERE     (dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK >='" + @masuk + "') ORDER BY dbo.TBL_TAHUN_AKADEMIK.ID_TAHUN_AKADEMIK DESC, dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk DESC";
                    var param = new { masuk = masuk };
                    var data = conn.Query(query1, param);

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
                string query = @"SELECT SUM(TBL_MATAKULIAH.SKS) AS TotalSKS
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




        public dynamic GetMatakuliahSemua(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT COUNT(SAMA) as JumlahMatkul FROM 
                (SELECT TBL_MATAKULIAH.NAMA_MK as MATAKULIAH, TBL_MATAKULIAH.KODE_MK as KODE, TBL_MATAKULIAH.SKS, coalesce(TBL_TRANSKRIP_DETAIL.NILAI,TBL_TRANSKRIP_DETAIL.NILAI,TBL_TRANSKRIP_DETAIL.NILAI,'-') as NILAI, TBL_MATAKULIAH.NILAI_LULUS 
                ,(CASE WHEN TBL_TRANSKRIP_DETAIL.NILAI > TBL_MATAKULIAH.NILAI_LULUS THEN 1 
                WHEN TBL_TRANSKRIP_DETAIL.NILAI = 'E' THEN 1
     	        WHEN TBL_TRANSKRIP_DETAIL.NILAI is null THEN 1 
                ELSE 0 END) AS sama 
                FROM  TBL_KURIKULUM INNER JOIN 
                TBL_MATAKULIAH ON (TBL_KURIKULUM.ID_KURIKULUM = TBL_MATAKULIAH.ID_KURIKULUM) INNER JOIN 
                TBL_TRANSKRIP_DETAIL ON (TBL_MATAKULIAH.ID_MK = TBL_TRANSKRIP_DETAIL.ID_MK)INNER JOIN 
                TBL_TRANSKRIP ON (TBL_TRANSKRIP_DETAIL.ID_TRANSKRIP = TBL_TRANSKRIP.ID_TRANSKRIP) 
                WHERE (TBL_KURIKULUM.ISCURRENT = 1) AND (TBL_TRANSKRIP.NPM = '" + @npm + "') AND (TBL_MATAKULIAH.KD_SIFAT_MK = 'W')  UNION SELECT TBL_MATAKULIAH.NAMA_MK as MATAKULIAH, TBL_MATAKULIAH.KODE_MK as KODE, TBL_MATAKULIAH.SKS, TBL_TRANSKRIP_DETAIL.NILAI, TBL_MATAKULIAH.NILAI_LULUS,(CASE WHEN TBL_TRANSKRIP_DETAIL.NILAI >= TBL_MATAKULIAH.NILAI_LULUS THEN 1 WHEN TBL_TRANSKRIP_DETAIL.NILAI = 'E' THEN 1 ELSE 0 END) AS sama FROM TBL_KURIKULUM INNER JOIN TBL_MATAKULIAH ON(TBL_KURIKULUM.ID_KURIKULUM = TBL_MATAKULIAH.ID_KURIKULUM) INNER JOIN TBL_TRANSKRIP_DETAIL ON(TBL_MATAKULIAH.ID_MK = TBL_TRANSKRIP_DETAIL.ID_MK)INNER JOIN TBL_TRANSKRIP ON(TBL_TRANSKRIP_DETAIL.ID_TRANSKRIP = TBL_TRANSKRIP.ID_TRANSKRIP) WHERE(TBL_KURIKULUM.ISCURRENT = 1) AND(TBL_TRANSKRIP.NPM = '" + @npm + "') AND(TBL_MATAKULIAH.KD_SIFAT_MK = 'P') AND(TBL_TRANSKRIP_DETAIL.NILAI IS NOT NULL) GROUP BY TBL_MATAKULIAH.NAMA_MK, TBL_MATAKULIAH.KODE_MK, TBL_MATAKULIAH.SKS, TBL_TRANSKRIP_DETAIL.NILAI, TBL_MATAKULIAH.NILAI_LULUS )A WHERE SAMA <> 1";

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


        public dynamic GetInfoPembayaran(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"select l.keterangan+' Semester '+s.keterangan+' '+substring(l.smt,0,5) [Keterangan Pembayaran], l.total[Yang Harus Dibayar], l.total_bayar [Sudah Dibayar], l.kurang[Kekurangan], CASE WHEN l.kurang = 0 THEN 'Lunas' Else 'Belum Lunas' END as Status
                from dbo.laporan_pembayaran_mhs l join dbo.refSemester s 
                on substring(l.smt,5,1) = s.idnya where npm =@npm order by l.smt asc";

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

        public dynamic getNilaiE(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @" SELECT COUNT(TBL_MATAKULIAH.KODE_MK) as nilaiE FROM
                TBL_KURIKULUM INNER JOIN
                TBL_MATAKULIAH ON(TBL_KURIKULUM.ID_KURIKULUM = TBL_MATAKULIAH.ID_KURIKULUM) INNER JOIN
                TBL_TRANSKRIP_DETAIL ON(TBL_MATAKULIAH.ID_MK = TBL_TRANSKRIP_DETAIL.ID_MK) INNER JOIN
                TBL_TRANSKRIP ON(TBL_TRANSKRIP_DETAIL.ID_TRANSKRIP = TBL_TRANSKRIP.ID_TRANSKRIP)
                WHERE(TBL_KURIKULUM.ISCURRENT = 1) AND(TBL_TRANSKRIP.NPM = '" + @npm + "') AND((TBL_MATAKULIAH.KD_SIFAT_MK = 'W') OR(TBL_MATAKULIAH.KD_SIFAT_MK = 'P'))AND (TBL_TRANSKRIP_DETAIL.NILAI LIKE '%E%')";

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
