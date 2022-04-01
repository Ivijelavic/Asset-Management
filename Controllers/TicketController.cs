using System.Linq;
using System.Collections.Generic;
using MVCappCoreWeb.EfDbLayer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCappCoreWeb.Areas.Identity.Data;
using MVCappCoreWeb.DBLayer;
using Microsoft.EntityFrameworkCore;
using MVCappCoreWeb.Models;
using System;

namespace MVCappCoreWeb.Controllers
{
    [Authorize(Roles = "Ticketing")]

    public class TicketController : Controller
    {
        private readonly UserManager<WebUser> _userManager;

        private readonly OrbisDbContext _context;

        private readonly WebUser _webUser;

        public TicketController(UserManager<WebUser> userManager, OrbisDbContext context)
        {
            _userManager = userManager;
            _context = context;
           
        }
        public  IActionResult Index()
        {
            //DokumentIZ tck = new DokumentIZ();

            //var doc= View(await _context.DokumentIZ.ToListAsync());
            //WebUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            //bool ins = EventsManager.InsertEvent(EventsManager.getMessageViewModel(user, "Ticketing"));
            return View();
        }
        public string  CheckSerial(string strValue)
        {
            string msg = "Nepostojeći uređaj.Opišite kvar u napomeni.";
            var principal = User?.Claims.Where(c => c.Type == "Oib").SingleOrDefault().Value;
            List<Ugovori> ugovori = GenericsBack.GetUgovoriList(principal.ToString());
            foreach(Ugovori ugvr in ugovori)
            {
                List<Aset> Asets = GenericsBack.GetAsetByUgovor(ugvr.IDUgovora);
                foreach (Aset ast in Asets)
                {
                    if (strValue == ast.Serial)
                    {
                        msg = ast.Opis;
                    }
                }
            }           
            return msg;
        }
        public async Task<IActionResult> SendTicket(string serial,string napomena, string selectedItems)
        {
            //string msg = "Obavijest je uspješno poslana!";
           // bool isSave = false;
            var principal = User?.Claims.Where(c => c.Type == "Oib").SingleOrDefault().Value;
            WebUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

            ////Mail-Event-insert-ProcessResultMessage:Ticket
            //bool ins = EventsManager.InsertEvent(EventsManager.getMessageViewModel(user, "Ticket",2,2,2));


            DokumentIZ tck = new DokumentIZ();
            string outBroj = null;
            string BrojDoc = null;
            int Godina = DateTime.Now.Year;
            string VrstaDokumenta = "TICKET";

            bool brojdoc = GenericsBack.DodjelaBrojaDokumenta(out outBroj, out Godina, out BrojDoc, DateTime.Now, VrstaDokumenta);

            tck.Broj = outBroj;
            tck.Smjer = "SMIJERUL";
            tck.Godina = Godina;
            tck.VrstaDokumenta = VrstaDokumenta;
            tck.IDKlijenta =  GenericsBack.KlijentIDPremaOIBu(principal.ToString());
            tck.DatumDokumenta = DateTime.Now;
            tck.DokumentNapomena1 = selectedItems + ":" + napomena;
            tck.DokumentNapomena2 = "Zaprimljeno: " + DateTime.Now.ToLongDateString();
            tck.DatumDokumenta = DateTime.Now;
            tck.CreateBy = User.Identity.Name;
            tck.CreateDate = DateTime.Now;
            tck.DatIzDokumenta = DateTime.Now;
            tck.OdgOsoba = User.Identity.Name;
            _context.DokumentIZ.Add(tck);
             int s= _context.SaveChanges();         
             int br = tck.IDDokument;
             
             if (s==1 && serial!=null)
                {
                int IDStavkeDokumenta = 0;
              StavkeDokumentaIZ stavka = new StavkeDokumentaIZ();
                stavka.IDStavkeDokumenta = IDStavkeDokumenta;
                stavka.IDDokumenta = br;
                stavka.StavkaOpis = CheckSerial(serial);
                stavka.CreateDate = DateTime.Now;
                stavka.idArtiklUsluga = GenericsBack.idArtiklaIzSeriala(serial);
                stavka.JedMjere = "kom";
                stavka.Kolicina = 1;
                stavka.CreateBy = User.Identity.Name;
                _context.StavkeDokumentaIZ.Add(stavka);
                int s1 = _context.SaveChanges();
                if (s1 == 1 && serial != null)
                {
                    Serials serialstavke = new Serials();
                    serialstavke.idDokumentVeza = br;
                    serialstavke.Serial = serial;
                    serialstavke.TipDokumentVeza = "TICKET";
                    serialstavke.idArtikl= GenericsBack.idArtiklaIzSeriala(serial);
                    serialstavke.Smjer = "SMIJERUL";
                    serialstavke.CreateDate = DateTime.Now;
                    serialstavke.ChangeDate = DateTime.Now;
                    serialstavke.ChangeBy = User.Identity.Name;
                    _context.Serial.Add(serialstavke);
                    int s2 = _context.SaveChanges();
                }
            }
            //Mail-Event-insert-ProcessResultMessage:Ticket
            bool ins = EventsManager.InsertEvent(EventsManager.getMessageViewModel(user,br.ToString(), 2, 2, 2));
            bool insmailresp = EventsManager.InsertEvent(EventsManager.getMessageViewModel(user, br.ToString(), 21, 1, 1));
            return RedirectToAction("Statusi");
           // return View();
        }

        public  IActionResult Detail(int id)
        { 
            Ticket tck = new Ticket();
            try
            {
               
                var principal = User?.Claims.Where(c => c.Type == "Oib").SingleOrDefault().Value;
                int idKlijent = GenericsBack.KlijentIDPremaOIBu(principal.ToString());
                var lst = _context.DokumentIZ
                          .Where(s => s.IDDokument == id)
                          .Select(x => new { x.IDDokument, x.Godina, x.Broj, x.IDKlijenta, x.DokumentNapomena1, x.DokumentNapomena2, x.VrstaDokumenta, x.OdgOsoba, x.DatumDokumenta, x.CreateBy })
                          .ToList().OrderByDescending(d => d.IDDokument);
                foreach (var ls in lst)
                {
                    tck.IDTicket = ls.IDDokument;
                    tck.Broj = ls.Broj;
                    tck.Godina = ls.Godina;
                    tck.Klijent = GenericsBack.GetKlijent(principal).ImeTvrtke;
                    tck.DatumTicket = ls.DatumDokumenta;
                    tck.VrstaDokumenta = ls.VrstaDokumenta;
                    tck.CreateBy = ls.CreateBy;
                    tck.DokumentNapomena1 = ls.DokumentNapomena1;
                    tck.DokumentNapomena2 = ls.DokumentNapomena2;
                }
                var lstser = _context.Serial
                             .Where(x => x.idDokumentVeza == tck.IDTicket)
                              .ToList();
                foreach (var lstsr in lstser)
                {
                    tck.Serial = lstsr.Serial;
                    tck.NazivSeriala = CheckSerial(tck.Serial);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(tck);
        }

        public IActionResult Statusi()
        {
            List<ShortDokumentIZ> list = new List<ShortDokumentIZ>();
           // ShortDokumentIZ shrt = new ShortDokumentIZ();
            var principal = User?.Claims.Where(c => c.Type == "Oib").SingleOrDefault().Value;
            int idKlijent = GenericsBack.KlijentIDPremaOIBu(principal.ToString());
            var lst = _context.DokumentIZ
                      .Where(s => s.IDKlijenta == idKlijent && s.VrstaDokumenta == "TICKET")
                      .Select(x => new {x.Broj, x.DatumDokumenta, x.DokumentNapomena2 ,x.Godina,x.IDDokument })
                      .ToList().OrderByDescending(d=>d.IDDokument);

            foreach(var ls in lst)
            {
                string OsOpis = "";
                var opis = _context.StavkeDokumentaIZ.Where(x => x.IDDokumenta == ls.IDDokument).ToList();
                foreach (var lsopis in opis)
                {
                    OsOpis = lsopis.StavkaOpis;
                }
                    list.Add(new ShortDokumentIZ(ls.Broj, ls.DatumDokumenta,ls.DokumentNapomena2,ls.Godina,ls.IDDokument, OsOpis));
            }


            return View(list);
        }


    }
}

