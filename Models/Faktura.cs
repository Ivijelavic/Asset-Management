using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCappCoreWeb.Models
{
    public class Faktura
    {
        public int IDFakture { get; set; }
        public string TipFakture { get; set; }
        public string VrstaFakture { get; set; }
        public bool placeno { get; set; }
        public decimal IznPLaceno { get; set; }
        public decimal PDVIznos { get; set; }
        public decimal VrijednostFaktureKN { get; set; }
        public decimal SumaStavki { get; set; }
        public DateTime DatumDS { get; set; }
        public string Broj { get; set; }
        public string Color { get; set; }

        public string BrojGodinaFakture { get; set; }
    }
}
