using Microsoft.AspNetCore.Mvc;
using MVCappCoreWeb.Models;
using System.Collections.Generic;
using System.Linq;
using MVCappCoreWeb.DBLayer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization.Json;
using MVCappCoreWeb.Areas.Identity.Data;
using System.Threading.Tasks;
using MVCappCoreWeb.EfDbLayer;


namespace MVCappCoreWeb.Controllers
{
    [Authorize(Roles = "Korisnik")]
    public class KorisnikController : Controller
    {

        private readonly OrbisDbContext _context;


        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WebUser> _userManager;
       // private IUgovoriRepository _ugovoriRepository;
      
        List<WebUser> _listUsers;
        public KorisnikController(UserManager<WebUser> userMgr, RoleManager<IdentityRole> roleMgr, OrbisDbContext context)
        {
            _roleManager = roleMgr;
            _userManager = userMgr;
            _listUsers = userMgr.Users.ToList();
            _context = context;
        }
        public async  Task<IActionResult> Index()
        {
            //entity test
          //  var ugovorief = _ugovoriRepository.GetAllUgovore();

            //rest!!!
            //string strrequest = getfromrest();
            //var resp= MakeApiCallAndReturnJson(endpoint);
            //string jsonFilePath = resp;

            var principal = User?.Claims.Where(c => c.Type == "Oib").SingleOrDefault().Value;
            // ViewBag.Oib = principal;
            List<Ugovori> ugovori = GenericsBack.GetUgovoriList(principal.ToString());
            // @ViewBag.Ugovori = FromJson(jsonFilePath);
            //InsertEvent-Ugovori
            WebUser user = await _userManager.FindByEmailAsync(User.Identity.Name);
            bool ins = EventsManager.InsertEvent(EventsManager.getMessageViewModel(user, "Ugovori",18,2,2));
            return View(ugovori);
        }
        public Ugovori FromJson(string json)
        {
            // Make a stream to read from.
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(json);
            writer.Flush();
            stream.Position = 0;

            // Deserialize from the stream.
            DataContractJsonSerializer serializer
                = new DataContractJsonSerializer(typeof(Ugovori));
            Ugovori cust = (Ugovori)serializer.ReadObject(stream);

            // Return the result.
            return cust;
        }

        private const string endpoint = "http://192.168.5.70/orbisrest/api/klijenti";
        private const string URL = "http://192.168.5.205:8082/api/klijenti";
        private string ConsumerKey = "APIKey";
        private string ConsumerSecret = "12345";
        public string getfromrest()
        {
            string strBack = "";
            WebRequest request = WebRequest.Create(URL);
            request.Method = WebRequestMethods.Http.Get;
            NetworkCredential networkCredential = new NetworkCredential("APIKey", "12345"); // logon in format "domain\username"
            CredentialCache myCredentialCache = new CredentialCache { { new Uri(URL), "Basic", networkCredential } };
            request.PreAuthenticate = true;
            request.Credentials = myCredentialCache;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        string responseFromServer = reader.ReadToEnd();
                        strBack=responseFromServer;
                    }
                }
            }
            return strBack;
        }        
        private string MakeApiCallAndReturnJson(string endpoint, Dictionary<string, string> parameters = null, string method = "GET")

        {

            if (parameters == null)

            {

                parameters = new Dictionary<string, string>();

            }



            WebClient wc = new WebClient();



            wc.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(ConsumerKey + ":" + ConsumerSecret));

            wc.Headers[HttpRequestHeader.ContentType] = "application/json";

            wc.Encoding = Encoding.UTF8;

            StringBuilder sb = new StringBuilder();

            foreach (var pair in parameters)

            {

                sb.AppendFormat("&{0}={1}", HttpUtility.UrlEncode(pair.Key), HttpUtility.UrlEncode(pair.Value));

            }



            var url = "";

            if (parameters.Count > 1)

            {

                url = endpoint + "?" + sb.ToString().Substring(1).Replace("%5b", "%5B").Replace("%5d", "%5D");



            }

            else

            {

                url =  endpoint;

            }

            var result = wc.DownloadString(url);

            return result;

        }
       
        public  IActionResult FaktureAset(int id)
        {
           
            var principal = User?.Claims.Where(c => c.Type == "Oib").SingleOrDefault().Value;
            List<Ugovori> ugovor = GenericsBack.GetUgovoriList(principal.ToString(),id);
            @ViewBag.IdUgovor = id;
            return View(ugovor);
        }
        [HttpGet]
        [Route("/korisnik/Fakture/{id}")]
        public IActionResult Fakture(int id)
        {
            var principal = User?.Claims.Where(c => c.Type == "Oib").SingleOrDefault().Value;
            List<Faktura> fakture = GenericsBack.GetFakturaList(principal.ToString(), id);
            return View(fakture);
        }
        [HttpGet]
        [Route("/korisnik/FaktureDetail/{id}")]
        public IActionResult FaktureDetail(int id)
        {

            var principal = User?.Claims.Where(c => c.Type == "Oib").SingleOrDefault().Value;
            List<Faktura> fakture = GenericsBack.GetFakturaList(principal.ToString(), 0,id );
            return View(fakture);
        }


        [HttpGet]
        [Route("/korisnik/Aset/{id}")]
        public IActionResult Aset(int id)
        {
            List<Aset> Asets = GenericsBack.GetAsetByUgovor(id);
            return View(Asets);
        }
        //SerialDetail
        [HttpGet]
        [Route("/korisnik/SerialDetail/{id}")]
        public IActionResult SerialDetail(int id, string serial)
        {
            List<Aset> Asets = GenericsBack.GetAsetByUgovor(id, serial);
            return View(Asets);
        }
    }
}

