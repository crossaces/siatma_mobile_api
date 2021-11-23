

using System.Collections.Generic;

namespace siatma_mobile_api.Model
{
    public class Pertanyaan
    {
        public string Nomor { get; set; }
        public int ID_Jenis_Pertanyaan { get; set; }
        public int ID_PERTANYAAN { get; set; }
        public IList<Jawaban> Jawaban { get; set; }
        public string Soal { get; set; }

    }


    public class Jawaban
    {
        public string Text{ get; set; }

        public int ID_PERTANYAAN { get; set; }
        public int  Nilai { get; set; }
    }
}
