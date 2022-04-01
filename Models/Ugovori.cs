using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCappCoreWeb.Models
{
    public class Ugovori
    {
		[Key]
		public int IDUgovora { get; set; }
		public string BrojUgovora { get; set; }
		public string NazivUgovora { get; set; }


		public DateTime DatumUgovora { get; set; }
		public string StatusUgovora { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal IznosUgovoraHRK { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal IznosUgovoraVAL { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal TecajUgovora { get; set; }		
		public int VremPeriodMj { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Ucesce { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Akontacija { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Jamcevina { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal FrontUpAdminFee { get; set; }
		[Column(TypeName = "decimal(18, 2)")]
		public decimal OstatakVrijednosti { get; set; }
		public int IDKlijenta { get; set; }

	}
}


/*
@IDUgovora int = NULL,								
	@Broj int = NULL,								
	@Mjesec int = NULL,								
	@Godina int = NULL,								
	@Tip nvarchar(2) = NULL,	
	@BrojUgovora varchar(30) = NULL,
	@NazivUgovora varchar(30) = NULL,							
	@DatumUgovora datetime = NULL,
	@DatumPonude   datetime =NULL,
	@BrojPonude   nvarchar(30) = NULL,								
	@NazivKlijenta nvarchar(100) = NULL,								
	@NazivDobavljaca nvarchar(100) = NULL,
	@NazivBanke nvarchar(100) = NULL,		
	@VremPeriodMj int = NULL,								
	@NacinPlacanja nvarchar(255) = NULL,								
	@NasaOdgovornaOsoba nvarchar(255) = NULL,								
	@KalkVal nvarchar(3) = NULL,
	@StatusUgovora varchar(30) = NULL,
	@RataUzEuribor nvarchar(1) = NULL,
	@Param varchar(255) = NULL,
	@OIB as varchar (25) = NULL
*/