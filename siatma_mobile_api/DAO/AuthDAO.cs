using System;
using System.Data.SqlClient;
using Dapper;

namespace siatma_mobile_api.DAO
{
    public class AuthDAO
    {
        public dynamic GetDataMhs(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);
                string query = @"SELECT NPM, NAMA_MHS, PASSWORD, KD_STATUS_MHS
                                FROM dbo.MST_MHS_AKTIF
                                WHERE NPM = @npm";

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

        public dynamic GetProfileMhs(string npm)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);

                string query = @"SELECT dbo.MST_MHS_AKTIF.NPM, dbo.MST_MHS_AKTIF.NAMA_MHS, dbo.MST_MHS_FOTO.FOTO, dbo.REF_PRODI.PRODI, dbo.REF_FAKULTAS.FAKULTAS
                    FROM dbo.MST_MHS_AKTIF LEFT OUTER JOIN
                      dbo.MST_MHS_FOTO ON dbo.MST_MHS_AKTIF.NPM = dbo.MST_MHS_FOTO.NPM LEFT OUTER JOIN
                      dbo.REF_PRODI ON dbo.MST_MHS_AKTIF.ID_PRODI = dbo.REF_PRODI.ID_PRODI LEFT OUTER JOIN
                      dbo.REF_FAKULTAS ON dbo.REF_PRODI.ID_FAKULTAS = dbo.REF_FAKULTAS.ID_FAKULTAS
                    WHERE (dbo.MST_MHS_AKTIF.NPM = @NPM) AND KD_STATUS_MHS ='A'";

                var param = new { NPM = npm };
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
