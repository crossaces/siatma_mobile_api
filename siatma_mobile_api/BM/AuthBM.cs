using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using siatma_mobile_api.DAO;
using siatma_mobile_api.Model;

namespace siatma_mobile_api.BM
{
    public class AuthBM
    {
        AuthDAO dao;
        Login output;

        public AuthBM()
        {
            dao = new AuthDAO();
            output = new Login();
        }

        public Login LoginSiatma(string username, string password)
        {
            var ul = dao.GetDataMhs(username);
            if (ul != null)
            {
                if (cekpasswordMhs(password, ul.PASSWORD))
                {
                    if (ul.KD_STATUS_MHS == "A")
                    {
                        var data = dao.GetProfileMhs(username);

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(AppSettings.secret);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                        new Claim(ClaimTypes.Name, data.NAMA_MHS),
                                        new Claim(ClaimTypes.Role, "Mahasiswa"),
                                        new Claim("username", data.NPM)
                            }),
                            Expires = DateTime.UtcNow.AddDays(7),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var gentoken = tokenHandler.WriteToken(token);

                    
                        output.token = gentoken;
                        output.status = true;
                        output.pesan = "Berhasil masuk!";
                        output.data = data;
                    }
                    else
                    {
                        output.token = "";
                        output.status = false;
                        output.pesan = "Gagal Login! Status Mahasiswa tidak aktif";
                        output.data = new { };
                    }
                }
                else
                {
                    output.token = "";
                    output.status = false;
                    output.pesan = "Gagal Login! Password salah";
                    output.data = new { };
                }
            }
            else
            {
                output.token = "";
                output.status = false;
                output.pesan = "Gagal Login! Data tidak ditemukan.";
                output.data = new { };
            }

            return output;
        }

        private bool cekpasswordMhs(string password, string passwordDatabase)
        {
            Encoding enc = Encoding.GetEncoding(1252);
            RIPEMD160 ripemdHasher = RIPEMD160.Create();
            byte[] data = ripemdHasher.ComputeHash(Encoding.Default.GetBytes(password));
            string str = enc.GetString(data);
            if (str == passwordDatabase)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
