using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCappCoreWeb.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
namespace MVCappCoreWeb.DBLayer
{
    public class GenericsBack
    {
        public static List<Ugovori> GetUgovoriList(string oib, int idUgovora = 0)
        {
            List<Ugovori> ugovori = new List<Ugovori>();
            try
            {
                DataSet dsDS = new DataSet();
                if (oib != null && oib.Length > 0)
                {
                    using (SqlConnection connection = ConnectionManager.GetConnection())
                    {
                        SqlCommand command = new SqlCommand("proc_Ugovori_Select", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.Parameters.Add("@OIB", SqlDbType.NVarChar, 50).Value = oib;
                        if (idUgovora > 0)
                        {
                            command.Parameters.Add("@IDUgovora", SqlDbType.Int).Value = idUgovora;
                        }
                        SqlDataReader reader = command.ExecuteReader();

                        Ugovori ugovor = null;
                   
                        while (reader.Read())
                        {

                            ugovor = new Ugovori();
                            ugovor.IDUgovora = int.Parse(reader["IDUgovora"].ToString());
                            ugovor.NazivUgovora = reader["NazivUgovora"].ToString();
                            ugovor.StatusUgovora = reader["StatusUgovora"].ToString();
                            if (ugovor.StatusUgovora == "REZERVIRAN") continue;
                            ugovor.BrojUgovora = reader["BrojUgovora"].ToString();
                            try {
                                ugovor.DatumUgovora = DateTime.Parse(reader["DatumUgovora"].ToString());
                            }
                            catch {
                                ugovor.DatumUgovora = DateTime.Now;
                            }
                            try { ugovor.IznosUgovoraHRK = Decimal.Parse(reader["IznosUgovoraHRK"].ToString()); }
                            catch { ugovor.IznosUgovoraHRK = 0.0M; }
                            try { ugovor.IznosUgovoraVAL = Decimal.Parse(reader["IznosUgovoraVAL"].ToString()); }
                            catch { ugovor.IznosUgovoraVAL = 0.0M; }
                            try { ugovor.TecajUgovora = Decimal.Parse(reader["TecajUgovora"].ToString()); }
                            catch { ugovor.TecajUgovora = 0.0M; }
                            ugovor.VremPeriodMj = int.Parse(reader["VremPeriodMj"].ToString());
                            try { ugovor.Ucesce = Decimal.Parse(reader["Ucesce"].ToString()); }
                            catch { ugovor.Ucesce = 0.0M; }
                            try { ugovor.Akontacija = Decimal.Parse(reader["Akontacija"].ToString()); }
                            catch { ugovor.Akontacija = 0.0M; }
                            try { ugovor.Jamcevina = Decimal.Parse(reader["Jamcevina"].ToString()); }
                            catch { ugovor.Jamcevina = 0.0M; }
                            try { ugovor.FrontUpAdminFee = Decimal.Parse(reader["FrontUpAdminFee"].ToString()); }
                            catch { ugovor.FrontUpAdminFee = 0.0M; }
                            try { ugovor.OstatakVrijednosti = Decimal.Parse(reader["FrontUpAdminFee"].ToString()); }
                            catch { ugovor.OstatakVrijednosti = 0.0M; }
                            if (ugovor.StatusUgovora == "AKTIVAN")
                            {
                                 ugovori.Add(ugovor);
                            }
                          
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // throw; log ex
            }
            var newList = ugovori.OrderBy(x => x.DatumUgovora).ToList();
            return newList;
        }

        public static Klijent GetKlijent(string oib)
        {
            Klijent klijent = null;
            try
            {

                DataSet dsDS = new DataSet();
                if (oib != null && oib.Length > 0)
                {
                    using (SqlConnection connection = ConnectionManager.GetConnection())
                    {
                        SqlCommand command = new SqlCommand("proc_Klijenti_Select", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.Parameters.Add("@OIB", SqlDbType.NVarChar, 50).Value = oib;
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            klijent = new Klijent();
                            klijent.IDKlijenta = int.Parse(reader["IDKlijenta"].ToString());
                            klijent.OIB = reader["OIB"].ToString();
                            klijent.ImeTvrtke = reader["ImeTvrtke"].ToString();
                            klijent.Tip = reader["Tip"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw; log ex
            }
            return klijent;
        }

        public static List<Faktura> GetFakturaList(string oib=null, int idUgovora = 0, int idFakture = 0)
        {
            List<Faktura> fakture = new List<Faktura>();
            try
            {
                DataSet dsDS = new DataSet();
                if (oib != null && oib.Length > 0)
                {
                    using (SqlConnection connection = ConnectionManager.GetConnection())
                    {
                        SqlCommand command = new SqlCommand("proc_Fakture_Select", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                       // command.Parameters.Add("@OIBKlijenta", SqlDbType.NVarChar, 50).Value = oib;
                      
                        if (idUgovora > 0)
                        {
                            command.Parameters.Add("@IDUgovora", SqlDbType.Int).Value = idUgovora;
                        }
                        
                        if (idFakture > 0)
                        {
                            command.Parameters.Add("@IDFakture", SqlDbType.Int).Value = idFakture;
                        }
                        SqlDataReader reader = command.ExecuteReader();
                        Faktura faktura = null;
                        while (reader.Read())
                        {
                            faktura = new Faktura();
                            faktura.IDFakture = int.Parse(reader["IDFakture"].ToString());
                            faktura.VrstaFakture = reader["VrstaFakture"].ToString();
                            faktura.TipFakture = reader["TipFakture"].ToString();
                            try { faktura.placeno = bool.Parse(reader["placeno"].ToString()); } catch { faktura.placeno = false; } 
                            faktura.Broj= reader["Broj"].ToString();
                            faktura.BrojGodinaFakture = reader["BrojGodinaFakture"].ToString();
                            try { faktura.IznPLaceno = Decimal.Parse(reader["IznPLaceno"].ToString()); }
                            catch { faktura.IznPLaceno = 0.0M; }
                            try { faktura.VrijednostFaktureKN = Decimal.Parse(reader["VrijednostFaktureKN"].ToString()); }
                            catch { faktura.VrijednostFaktureKN = 0.0M; }
                            try { faktura.PDVIznos = Decimal.Parse(reader["PDVIznos"].ToString()); }
                            catch { faktura.PDVIznos = 0.0M; }
                            try { faktura.SumaStavki = Decimal.Parse(reader["SumaStavki"].ToString()); }
                            catch { faktura.SumaStavki = 0.0M; }
                            try { faktura.DatumDS = DateTime.Parse(reader["DatumDS"].ToString()); }
                            catch { faktura.DatumDS = DateTime.Now; }
                            //if((!faktura.placeno) && (faktura.PDVIznos>0))
                            //{
                            //    faktura.Color = "#458f7e"; //
                            //}
                            //else if ((faktura.placeno) && (faktura.PDVIznos == 0))
                            //{
                            //    faktura.Color = "#c06c84";
                            //}
                            //else if ((!faktura.placeno) && (faktura.PDVIznos == 0))
                            //{
                            //    faktura.Color = "#c06c84";
                            //}
                             // var bl = faktura.placeno;
                             fakture.Add(faktura);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // throw; log ex VrstaFaktureKN
            }
            return fakture;
        }

        public static List<Aset> GetAsetByUgovor(int idUgovor,string serial=null)
        {
           // asetKorisnik(idUgovor);
            List<Aset> Asets = new List<Aset>();
            try
            {
                DataSet dsDS = new DataSet();
                if (idUgovor>0)
                {
                    using (SqlConnection connection = ConnectionManager.GetConnection())
                    {
                        SqlCommand command = new SqlCommand("proc_UgovorOpremaPreostalo_Select", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.Parameters.Add("@IDUgovora", SqlDbType.Int).Value = idUgovor;
                        SqlDataReader reader = command.ExecuteReader();
                        Aset aset = null;
                        while (reader.Read())
                        {
                            aset = new Aset();
                            aset.idDokumentVeza = int.Parse(reader["idDokumentVeza"].ToString());
                            aset.Opis = reader["Opis"].ToString();
                            aset.Serial = reader["Serial"].ToString();
                            aset.Mjesto = reader["Mjesto"].ToString();
                            aset.UlicaiBr = reader["UlicaiBr"].ToString();
                            aset.Soba = reader["Soba"].ToString();
                            aset.Kat = reader["Kat"].ToString();
                            aset.ZaduzenaOsoba = reader["ZaduzenaOsoba"].ToString();
                            aset.PreostalaKolicina = "1";
                            Asets.Add(aset);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                // log ex
            }
            if (serial != null)
            {
                 List<Aset> filteredList = Asets.Where(x => x.Serial == serial).ToList();
                Asets = filteredList;
            }

                return Asets;

            
        }

        public static void asetKorisnik(int idUgovor)
        {
            List<int> list = new List<int>();
            try
            {
                using (SqlConnection connection = ConnectionManager.GetConnection())
                {
                    SqlCommand command = new SqlCommand("proc_DokumentIZ_Select", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;
                    command.Parameters.Add("@idDokumentVeza", SqlDbType.Int).Value = idUgovor;
                    command.Parameters.Add("@TipDokumentVeza", SqlDbType.NVarChar).Value = "UGOVOR";
                    using (var reader = command.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            int i= int.Parse(reader["IDDokument"].ToString());
                            list.Add(i); 
                        }
                           
                    
                    }
                }
                int brDoc = list.Count;
                if ( brDoc > 0) 
                {

                }
            }
            catch (Exception ex)
            {
               // throw;
            }
        }

        /******************Utility DodjelaBrojaDokumenta************************************/

        public static bool DodjelaBrojaDokumenta(

                         out string Broj,
                         out int Godina,
                         out string BrojDokumenta,
                         DateTime DatumDokumenta,
                         string VrstaDokumenta
                                               )
        {

            int intBroj = 1;
            Godina = DatumDokumenta.Year;
            BrojDokumenta = "";

            string strSQL = "Select MAX (Cast (Broj as int))as Broj, Godina from DokumentIZ Where Godina = " + Godina + " and DatumDokumenta >='2013-07-01' AND VrstaDokumenta = '" + VrstaDokumenta + "' " +
                                   "GROUP BY Godina";

            using (SqlConnection connection = ConnectionManager.GetConnection())
            {
                SqlCommand command = new SqlCommand(strSQL, connection);
                command.CommandType = CommandType.Text;

                DataSet datasetDS = new DataSet("HydraWebDataSet");
                SqlDataAdapter adapterDA = new SqlDataAdapter(command);
                adapterDA.Fill(datasetDS, "dTable");

                foreach (DataRow row in datasetDS.Tables["dTable"].Rows)
                {
                    try
                    {
                        intBroj = int.Parse(row["Broj"].ToString());
                        intBroj += 1;

                    }
                    catch { }
                }

                string Br = "00000" + intBroj.ToString();


                BrojDokumenta = Br.ToString().Substring(Br.ToString().Length - 5, 5) + "/" + Godina.ToString();
                Broj = intBroj.ToString();
            }
            return true;

        }
        /******************KlijentIDPremaOIBu***********************************************/
        public static int KlijentIDPremaOIBu(string OIB)
        {
            int result = 0;

            string strSQL = "Select IDKlijenta FROM Klijenti WHERE OIB= '" + OIB + "' Group By IDKlijenta";
            using (SqlConnection connection = ConnectionManager.GetConnection())
            {
                SqlCommand command = new SqlCommand(strSQL, connection);
                command.CommandType = CommandType.Text;

                try
                {
                    result = (int)command.ExecuteScalar();
                }
                catch { }

                if (result == null)
                    return 0;


            }

            return result;

        }

        /******************idArtiklaIzSeriala***********************************************/
        public static int idArtiklaIzSeriala(string Serial)
        {
            int id = 0;

            if (Serial == string.Empty) return 0;

            string strSQL = "Select MAX (DISTINCT idArtikl) as idArtikl from Serial where serial = '" + Serial + "'";
            using (SqlConnection connection = ConnectionManager.GetConnection())
            {
                SqlCommand command = new SqlCommand(strSQL, connection);
                command.CommandType = CommandType.Text;

                try
                {
                    id = (int)command.ExecuteScalar();
                }
                catch { }

                if (id == 0)
                    return 0;
            }
            return id;
        }


    }
}
