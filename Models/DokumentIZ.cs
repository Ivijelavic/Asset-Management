using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCappCoreWeb.Models
{
	public class DokumentIZ
	{
		[Key]
		public int IDDokument { get; set; }
		public string Smjer { get; set; }
		public int Godina { get; set; }
		public string Broj { get; set; }
		public int IDKlijenta { get; set; }
		public string VrstaDokumenta { get; set; }
		public DateTime DatumDokumenta { get; set; }
		public string DokumentNapomena1 { get; set; }
		public string DokumentNapomena2 { get; set; }
		public string CreateBy { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime DatIzDokumenta { get; set; }
		public string OdgOsoba { get; set; }
	}

	public class ShortDokumentIZ
	{
		public int _IDDokument { get; set; }
		public string _Broj { get; set; }
		public DateTime _DatumDokumenta { get; set; }
		public string _DokumentNapomena2 { get; set; }
		public string _StavkaOpis { get; set; }

		public int _Godina { get; set; }

		public ShortDokumentIZ(string Broj, DateTime DatumDokumenta, string DokumentNapomena2, int Godina, int IDDokument,string StavkaOpis)
        {
			_Broj = Broj;
			_DatumDokumenta = DatumDokumenta;
			_DokumentNapomena2 = DokumentNapomena2;
			_Godina = Godina;
			_IDDokument = IDDokument;
			_StavkaOpis = StavkaOpis;

		}
	}
}