using System;
using System.Data.SqlClient;
using Dapper;

namespace siatma_mobile_api.DAO
{
    public class JadwaluDAO
    {
        public dynamic GetDataJadwaluKuliah(string npm, string smt)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT        dbo.TBL_KELAS.KODE_MK AS KODE, dbo.TBL_KELAS.NAMA_MK AS MATAKULIAH, COALESCE (CONVERT(VARCHAR(12), dbo.TBL_KELAS.TANGGAL_UTS, 106) + ' SESI ' + dbo.REF_SESI.SESI,           
                    CONVERT(VARCHAR(12), dbo.TBL_KELAS.TANGGAL_UTS, 106) + ' SESI ' + dbo.REF_SESI.SESI,           CONVERT(VARCHAR(12), dbo.TBL_KELAS.TANGGAL_UTS, 106) + ' SESI ' + dbo.REF_SESI.SESI,           
                    CONVERT(VARCHAR(12), dbo.TBL_KELAS.TANGGAL_UTS, 106), '***')                           AS UTS, 
                    COALESCE (CONVERT(VARCHAR(12), dbo.TBL_KELAS.TANGGAL_UAS, 106) + ' SESI ' + 
                    REF_SESI_1.SESI,           CONVERT(VARCHAR(12), dbo.TBL_KELAS.TANGGAL_UAS, 106) + ' SESI ' + REF_SESI_1.SESI,           
                    CONVERT(VARCHAR(12), dbo.TBL_KELAS.TANGGAL_UAS, 106) + ' SESI ' + REF_SESI_1.SESI,           CONVERT(VARCHAR(12), dbo.TBL_KELAS.TANGGAL_UAS, 106), '***')                           AS UAS 
                    FROM            dbo.REF_SESI AS REF_SESI_1 RIGHT OUTER JOIN                          dbo.TBL_KRS INNER JOIN                          
                    dbo.TBL_KELAS ON dbo.TBL_KRS.ID_KELAS = dbo.TBL_KELAS.ID_KELAS INNER JOIN                          dbo.TBL_SEMESTER_AKADEMIK ON dbo.TBL_KELAS.ID_TAHUN_AKADEMIK = dbo.TBL_SEMESTER_AKADEMIK.ID_TAHUN_AKADEMIK AND                           
                    dbo.TBL_KELAS.NO_SEMESTER = dbo.TBL_SEMESTER_AKADEMIK.NO_SEMESTER ON REF_SESI_1.ID_SESI = dbo.TBL_KELAS.ID_SESI_UAS                           LEFT OUTER JOIN                          
                    REF_SESI ON TBL_KELAS.ID_SESI_UTS = REF_SESI.ID_SESI 
                    WHERE        (dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk = '" + @smt + "') AND (TBL_KRS.NPM = '" + @npm + "') and dbo.TBL_KELAS.ID_KELAS not in (select ID_KELAS_ASAL   FROM dbo.TBL_KRS INNER JOIN dbo.TBL_KELAS ON dbo.TBL_KRS.ID_KELAS = dbo.TBL_KELAS.ID_KELAS WHERE  (dbo.TBL_SEMESTER_AKADEMIK.SEMESTER_AKADEMIk = '" + @smt + "') and npm = '" + @npm + "' and id_kelas_asal is not null ) ORDER BY dbo.TBL_KELAS.TANGGAL_UTS, dbo.TBL_KELAS.TANGGAL_UAS, dbo.REF_SESI.SESI, REF_SESI_1.SESI";

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

        public dynamic GetDataWaktuJadwaluKuliah(string prodi)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT dbo.REF_SESI.SESI as Sesi, case when dbo.REF_SESI.AWAL_UNIT_WKT%2=0 then convert(varchar,dbo.ref_sesi.awal_unit_wkt/2)+'.00' 
                          else convert(varchar,ref_sesi.awal_unit_wkt/2)+'.30' END as [Masuk], 
                          case when dbo.REF_SESI.AKHIR_UNIT_WKT%2=0 then convert(varchar,dbo.ref_sesi.akhir_unit_wkt/2)+'.00' 
                          else convert(varchar,dbo.ref_sesi.akhir_unit_wkt/2)+'.30' END as [Keluar]  
                          FROM            REF_SESI INNER JOIN 
                          REF_PRODI ON REF_SESI.ID_PRODI = REF_PRODI.ID_PRODI 
                          WHERE        (dbo.REF_PRODI.ID_PRODI = '07') and ref_sesi.jenis_sesi = 'Ujian' 
                          order by dbo.ref_sesi.ID_SESI, dbo.ref_sesi.sesi";

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
