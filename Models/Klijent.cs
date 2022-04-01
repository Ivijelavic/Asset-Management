using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCappCoreWeb.Models
{
    public class Klijent
    {
		public int IDKlijenta { get; set; }
		public string Tip { get; set; }
		public string ImeTvrtke { get; set; }
		public string OIB { get; set; }

	}
}


    //@IDKlijenta int = NULL,
	//@Tip Varchar(30) = NULL,								
	//@ImeTvrtke nvarchar(255) = NULL,	
	//@Ime varchar(50) = NULL,							
	//@Adresa nvarchar(255) = NULL,								
	//@Sjediste nvarchar(255) = NULL,								
	//@PostanskiBroj nvarchar(255) = NULL,								
	//@OIB nvarchar(13) = NULL,	
	//@MB nvarchar(13) = NULL,		
	//@KontaktOsoba nvarchar(255) = NULL,								
	//@DopAdresa nvarchar(255) = NULL,								
	//@DopPostanskiBroj nvarchar(255) = NULL,								
	//@DopSjediste nvarchar(255) = NULL,
	//@Sektor nchar(2) = NULL,
	//@TranSektor nchar(3) = NULL