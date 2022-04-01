using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCappCoreWeb.Models
{
    public class Serials
    {
        [Key]
        public int idArtikl { get; set; }
        public int idDokumentVeza { get; set; }
        public string TipDokumentVeza { get; set; }
        public string Serial { get; set; }
        public string Smjer { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string ChangeBy { get; set; }
        public DateTime ChangeDate { get; set; }
    }
}
