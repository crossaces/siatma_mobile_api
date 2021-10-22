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
        OutPutApi outputapi;

        public AuthBM()
        {
            dao = new AuthDAO();
            output = new Login();
            outputapi = new OutPutApi();
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

                        
                        var id_prodi = data.ID_PRODI;
                        var foto = data.FOTO;
                        var thn_masuk = data.THN_MASUK;
                        var npm = data.NPM;                   
                        var panggilan = data.PANGGILAN;

                        output.token = gentoken;
                        output.status = true;
                        output.pesan = "Berhasil masuk!";
                        output.FOTO = foto;
                        output.ID_PRODI = id_prodi;
                        output.THN_MASUK = thn_masuk;
                    
                        output.NPM = npm;
                        output.PANGGILAN = panggilan;

                    }
                    else
                    {
                        output.token = "";
                        output.status = false;
                        output.pesan = "Gagal Login! Status Mahasiswa tidak aktif";
                       
                    }
                }
                else
                {
                    output.token = "";
                    output.status = false;
                    output.pesan = "Gagal Login! Password salah";
                  
                }
            }
            else
            {
                output.token = "";
                output.status = false;
                output.pesan = "Gagal Login! Data tidak ditemukan.";
             
            }

            return output;
        }
        public OutPutApi updatePasswordMahasiswa(string npm, string password,string passwordlama)
        {
            outputapi.status = true;
            outputapi.pesan = "Berhasil Update Password";
            var ul = dao.GetDataMhs(npm);
            if (ul != null)
            {
                if (cekpasswordMhs(passwordlama, ul.PASSWORD))
                {
                    string pwd = getRIPEMD160(password);
                    byte[] pwd1 = getMD5(password);

                    //dao.updatepasswordmFakultas();
                    dao.updatepasswordmWarehouse(npm, pwd, pwd1);
                    dao.updatepasswordmFakultas(npm, pwd, pwd1,ul.ID_PRODI);

                    outputapi.pesan = "Berhasil Ganti Password";
                    outputapi.data = "";
                }
                else
                {
                    outputapi.status = false;
                    outputapi.pesan = "Password Lama Salah";
                    outputapi.data = "";
                }
            }
            return outputapi;
        }


        public string CekStrongPass(string pass)
        {
            int temp = 0;
            bool kapital = false;
            bool str = false;
            bool angka = false;
            bool karakter = false;

            string strkapital = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strstr = "abcdefghijklmnopqrstuvwxyz";
            string strangka = "1234567890";
            string strkarakter = "!@#$%^&*()?.,;";

            char[] cek = pass.ToCharArray();

            if (cek.Length >= 8)
            {
                temp++;
            }

            for (int i = 0; i < cek.Length; i++)
            {
                if (kapital == false && strkapital.Contains(cek[i].ToString()))
                {
                    kapital = true;
                    temp++;
                }

                if (str == false && strstr.Contains(cek[i].ToString()))
                {
                    str = true;
                    temp++;
                }

                if (angka == false && strangka.Contains(cek[i].ToString()))
                {
                    angka = true;
                    temp++;
                }

                if (karakter == false && strkarakter.Contains(cek[i].ToString()))
                {
                    karakter = true;
                    temp++;
                }
            }

            return temp.ToString();
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



        public string getRIPEMD160(string input)
        {
            Encoding enc = Encoding.GetEncoding(1252);
            RIPEMD160 ripemdHasher = RIPEMD160.Create();
            byte[] data = ripemdHasher.ComputeHash(Encoding.Default.GetBytes(input));
            string str = enc.GetString(data);
            return str;
        }

        //get password MD5
        public static Byte[] getMD5(string password)
        {
            HashAlgorithm hashAlg = HashAlgorithm.Create("md5");
            byte[] hash = hashAlg.ComputeHash(Encoding.Default.GetBytes(password));
            return hash;
        }

    }
}
