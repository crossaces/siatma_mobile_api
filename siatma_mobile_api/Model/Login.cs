using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siatma_mobile_api.Model
{
    public class Login
    {
        public bool status { get; set; }
        public string token { get; set; }
        public string pesan { get; set; }
        
        public byte[]
            FOTO { get; set; }
        public string NPM { get; set; }
        public string ID_PRODI { get; set; }

       // public string NAMA_MHS { get; set; }
        public int THN_MASUK { get; set; }
        public string PANGGILAN { get; set; }



    }
    //192.168.15.156
    //202.14.92.208
}
