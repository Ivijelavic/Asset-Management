using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCappCoreWeb.Models
{
    public class Ticket
    {
        public int IDTicket { get; set; }
        public DateTime DatumTicket { get; set; }
        public int Godina { get; set; }
        public string Broj { get; set; }
        public int IDKlijenta { get; set; }
        public string  Klijent { get; set; }
        public string VrstaDokumenta { get; set; }
        public DateTime DatumDokumenta { get; set; }
        public string DokumentNapomena1 { get; set; }
        public string DokumentNapomena2 { get; set; }
        public string CreateBy { get; set; }
        public string Serial { get; set; }
        public string NazivSeriala { get; set; }

    }
}
