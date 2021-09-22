using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OireachtasAPI
{
    public class Program
    {
        public static string LEGISLATION_DATASET = "legislation.json";
        public static string MEMBERS_DATASET = "members.json";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bill and Legislation Management System");

            Console.WriteLine("Press 1 to Filter Bills based on Sponsor");

            Console.WriteLine("Press 2 to Filter Bills Last Updated");

            string choice = Console.ReadLine();
            int n = Convert.ToInt32(choice);
            GetInput(n);

        }

        public static Func<string, dynamic> load = jfname => JsonConvert.DeserializeObject((new System.IO.StreamReader(jfname)).ReadToEnd());

        //public static Func<string, dynamic> load1 = jfname => JsonConvert.DeserializeObject<Legistlation>((new System.IO.StreamReader(jfname)).ReadToEnd());

        //public static Func<string, dynamic> load2 = jfname => JsonConvert.DeserializeObject<Members>((new System.IO.StreamReader(jfname)).ReadToEnd());

        public static void GetInput(int number)
        {
            Console.WriteLine("You have selected option " + number );
            switch(number)
            {
                case 1:
                    Console.WriteLine("Enter PID");
                    string pId = Console.ReadLine();
                    CheckFilterBillsByPID(pId);
                    break;

                case 2:
                    CheckBillsLastUpdated();
                    break;

                default:
                    Console.WriteLine("Invalid Input");
                    break;

               
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();

        }

        public static void CheckFilterBillsByPID(string pId)
        {
            List<Bill> bills = filterBillsSponsoredBy(pId);
            // IvanaBacik
            if (bills.Count > 0)
            {
                foreach (Bill bill in bills)
                {
                    Console.WriteLine("Bill No:{0}\n" +
                    "Bill Type:{1}\n" + "Bill Year:{2}\n" +
                    "Bill Last Updated:{3}\n" + "Bill Status:{4}\n"
               , bill.billNo, bill.billType, bill.billYear, bill.lastUpdated, bill.status
                );
                }
            }
            else
            {
                Console.WriteLine("No matching bills found for this PID");
            }

        }

        public static void CheckBillsLastUpdated()
        {
            try
            {
                Console.Write("Enter the since date for the bill (e.g. 10/22/1987): ");
                 DateTime since = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter the until date for the bill (e.g. 10/22/1987): ");
                DateTime until = DateTime.Parse(Console.ReadLine());

                List<Bill> billsByLastUpdated = filterBillsByLastUpdated(since, until);

                if (billsByLastUpdated.Count > 0)
                {
                    foreach (Bill bill in billsByLastUpdated)
                    {
                        Console.WriteLine("Bill No:{0}\n" +
                        "Bill Type:{1}\n" + "Bill Year:{2}\n" +
                        "Bill Last Updated:{3}\n" + "Bill Status:{4}\n"
                   , bill.billNo, bill.billType, bill.billYear, bill.lastUpdated, bill.status
                    );
                    }
                }
                else
                {
                    Console.WriteLine("No matching bills found for this datetime period");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid date entered");
            }
        }

        /// <summary>
        /// Return bills sponsored by the member with the specified pId
        /// </summary>
        /// <param name="pId">The pId value for the member</param>
        /// <returns>List of bill records</returns>
        public static List<Bill> filterBillsSponsoredBy(string pId)
        {
            //dynamic leg = load1(LEGISLATION_DATASET);

            //dynamic mem = load2(MEMBERS_DATASET);

            string legString = MakeGetRequest("https://api.oireachtas.ie/v1/legislation?limit=50");

            string memString = MakeGetRequest("https://api.oireachtas.ie/v1/members?limit=50");

            Legistlation leg = JsonConvert.DeserializeObject<Legistlation>(legString.ToString());

            Members mem = JsonConvert.DeserializeObject<Members>(memString.ToString());

            Stopwatch stopwatch = new Stopwatch();

            // Begin timing
            stopwatch.Start();
            List<Bill> ret = new List<Bill>();

            var allResults = leg.results;
            var bills = allResults.Select(x => x.bill).ToList();

            int count = 0;
            var allMembers = mem.results;

            foreach (Bill bill in bills)
            {
                List<Result2> members2 = allMembers.Where(y => y.member.pId == pId).ToList();

                var sponsors = bill.sponsors.Select(x => x.sponsor).ToList();

                string fullName = members2[0].member.fullName;
                count = sponsors.Where(x => x.by.showAs == fullName).Count();
                if(count>0)
                {
                    ret.Add(bill);
                }

            }
            Console.WriteLine(ret);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            // The new code is very efficient. It only takes 1ms to run.


            //Old code commented out for comparison
            //foreach (Result res in leg.results)
            //{
            //    List<Sponsor> p = res.bill.sponsors;

            //    foreach (Sponsor i in p)
            //    {
            //        string name = i.sponsor.by.showAs;
            //        //  string name = i["sponsor"]["by"]["showAs"];
            //        foreach (dynamic result in mem.results)
            //        {
            //            string fname = result.member.fullName;
            //            string rpId = result.member.pId;
            //            if (fname == name && rpId == pId)
            //            {
            //                ret.Add(res.bill);
            //            }
            //        }
            //    }
            //}

            return ret;
        }

        /// <summary>
        /// Return bills updated within the specified date range
        /// </summary>
        /// <param name="since">The lastUpdated value for the bill should be greater than or equal to this date</param>
        /// <param name="until">The lastUpdated value for the bill should be less than or equal to this date.If unspecified, until will default to today's date</param>
        /// <returns>List of bill records</returns>
        public static List<Bill> filterBillsByLastUpdated(DateTime since, DateTime until)
        {
            string Bills = MakeGetRequest("https://api.oireachtas.ie/v1/legislation?date_start="+ since + "&date_end=" + until + "&limit=50");

            Legistlation leg = JsonConvert.DeserializeObject<Legistlation>(Bills.ToString());

            var allResults = leg.results;

            var getBillsByLastUpdated =  allResults.Where(x => x.bill.lastUpdated >= since && x.bill.lastUpdated <= until);

            var bills = getBillsByLastUpdated.Select(x => x.bill).ToList();

            return bills.ToList();
        }

        public static string MakeGetRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            string responseString;
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    responseString = reader.ReadToEnd();
                    return responseString;
                }
            }
        }
    }
}
