using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCappCoreWeb.Models
{
    public class StavkeDokumentaIZ
    {
        [Key]
        public int IDStavkeDokumenta { get; set; }
        public int IDDokumenta { get; set; }
        public int idArtiklUsluga { get; set; }
        public string StavkaOpis { get; set; }
        public decimal Kolicina { get; set; }
        public string JedMjere { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
