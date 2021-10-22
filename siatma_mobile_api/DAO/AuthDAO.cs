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
                string query = @"SELECT NPM, NAMA_MHS, PASSWORD, KD_STATUS_MHS, ID_PRODI
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

                string query = @"SELECT dbo.MST_MHS_AKTIF.NPM, SUBSTRING(dbo.MST_MHS_AKTIF.NAMA_MHS,1,charindex(' ',dbo.MST_MHS_AKTIF.NAMA_MHS)-1) AS PANGGILAN,
dbo.MST_MHS_AKTIF.NAMA_MHS as NAMA_MHS, dbo.MST_MHS_AKTIF.ALAMAT, dbo.MST_ORTU.ALAMAT_ORTU, dbo.MST_MHS_AKTIF.TMP_LAHIR, dbo.MST_MHS_AKTIF.TGL_LAHIR, YEAR(GETDATE()) - dbo.MST_MHS_AKTIF.THN_MASUK AS lama, dbo.MST_MHS_AKTIF.ID_PRODI, dbo.MST_MHS_AKTIF.THN_MASUK,
                                dbo.MST_MHS_AKTIF.KD_STATUS_MHS, dbo.MST_MHS_FOTO.FOTO, dbo.REF_PRODI.PRODI, dbo.REF_FAKULTAS.FAKULTAS
                                FROM dbo.MST_MHS_AKTIF LEFT OUTER JOIN
                                dbo.MST_MHS_FOTO ON dbo.MST_MHS_AKTIF.NPM = dbo.MST_MHS_FOTO.NPM LEFT OUTER JOIN
                                dbo.MST_ORTU ON dbo.MST_MHS_AKTIF.ID_ORTU = dbo.MST_ORTU.ID_ORTU LEFT OUTER JOIN
                                dbo.REF_PRODI ON dbo.MST_MHS_AKTIF.ID_PRODI = dbo.REF_PRODI.ID_PRODI LEFT OUTER JOIN
                                dbo.REF_FAKULTAS ON dbo.REF_PRODI.ID_FAKULTAS = dbo.REF_FAKULTAS.ID_FAKULTAS
                                WHERE (dbo.MST_MHS_AKTIF.NPM = @npm) AND KD_STATUS_MHS ='A';";

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


        public dynamic updatepasswordmWarehouse(string npm,string password, byte[] password1)
        {
            SqlConnection conn = new();
            try
            {
                conn = new SqlConnection(DBKoneksi.koneksi);

                string query = @"UPDATE MST_MHS_AKTIF SET PASSWORD = @password,PASSWORD1 = @password1 WHERE NPM = @npm";

                var param = new { npm = npm, password= password, password1 = password1 };
                var data = conn.Query<dynamic>(query, param);

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



        public dynamic updatepasswordmFakultas(string npm, string password, byte[] password1,string prodi)
        {
            SqlConnection conn = new();
            try
            {

                              
                if (prodi.Equals("03") || prodi.Equals("04") || prodi.Equals("15") || prodi.Equals("11") || prodi.Equals("12") || prodi.Equals("50"))
                {
                     conn = new SqlConnection(DBKoneksi.connFT);
                }
                else if (prodi.Equals("05") || prodi.Equals("52"))
                {
                    conn = new SqlConnection(DBKoneksi.connFT);

                }
                else if (prodi.Equals("08"))
                {
                    conn = new SqlConnection(DBKoneksi.connFTB);

                }
                else if (prodi.Equals("01") || prodi.Equals("02") || prodi.Equals("13") || prodi.Equals("70") || prodi.Equals("51") || prodi.Equals("54"))
                {
                    conn = new SqlConnection(DBKoneksi.connFT);

                }
                else if (prodi.Equals("06") || prodi.Equals("07") || prodi.Equals("14") || prodi.Equals("53") || prodi.Equals("56"))
                {
                    conn = new SqlConnection(DBKoneksi.connFTI);
                }
                else if (prodi.Equals("09") || prodi.Equals("10") || prodi.Equals("55"))
                {
                    conn = new SqlConnection(DBKoneksi.connFISIP);

                }                
              

                string query = @"UPDATE MST_MHS_AKTIF SET PASSWORD = @password,PASSWORD1 = @password1 WHERE NPM = @npm";

                var param = new { NPM = npm, PASSWORD = password, PASSWORD1 = password1 };
                var data = conn.Query<dynamic>(query, param);

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
