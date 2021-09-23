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
        public dynamic data { get; set; }
    }
}
