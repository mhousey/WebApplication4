using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {

        public List<Members> _lstMembers = new List<Members>();
        public List<Claim> _lstClaims = new List<Claim>();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [HttpGet]
        public JsonResult GetClaims(string ClaimDate)
        {

            var sdate = ClaimDate.Split('-');
            var dt = DateTime.Parse(ClaimDate);//sdate[1] + '/' + sdate[2] + '/' + sdate[0];
           


            
          
            string path = Server.MapPath("\\App_Data\\Member.csv"); 
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {


                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new Members
                    {

                        MemberID = csv.GetField<int>("MemberID"),
                        FirstName = csv.GetField<string>("FirstName"),
                        LastName = csv.GetField<string>("LastName"),
                        EnrollmentDate = csv.GetField<DateTime>("EnrollmentDate")
                    };
                    _lstMembers.Add(record);
                }
            }
             path = Server.MapPath("\\App_Data\\Claim.csv");
            using (var reader = new StreamReader(path))            
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var members = new List<Members>();
                var claims = new List<Claim>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new Claim
                    {

                        MemberID = csv.GetField<int>("MemberID"),
                        ClaimDate = csv.GetField<DateTime>("ClaimDate"),
                        ClaimAmount = csv.GetField<decimal>("ClaimAmount")

                    };
                    _lstClaims.Add(record);
                }
            }
            foreach(var m in _lstMembers)
            {
                m.lstClaims = _lstClaims.Where(x => x.MemberID == m.MemberID && x.ClaimDate <=dt).ToList();
            }

            return Json(_lstMembers, JsonRequestBehavior.AllowGet);

        }

    }
   
}
