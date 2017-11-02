using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader oSr = new StreamReader("Countries.json");
            string sJson = "";
            using (oSr)
            {
                sJson = oSr.ReadToEnd();
            }

            JObject oJson = JObject.Parse(sJson);
            var oCountries = oJson["countries"].ToList();
            List<Country> lCountries = new List<Country>();

            for(int i=0; i<oCountries.Count; i++)
            {
                lCountries.Add(new Country
                {
                    sCode = (string)oCountries[i]["alpha2code"],
                    sName = (string)oCountries[i]["name"],
                    sCapital = (string)oCountries[i]["capital"],
                    nPopulation = (int)oCountries[i]["population"],
                    fArea = (float)oCountries[i]["area"]
                });
            }
            
            for(int i=0;i<lCountries.Count;i++)
            {
                Console.WriteLine(lCountries[i].sCode);
                Console.WriteLine(lCountries[i].sName);
                Console.WriteLine(lCountries[i].sCapital);
                Console.WriteLine(lCountries[i].nPopulation);
                Console.WriteLine(lCountries[i].fArea);
            }

            var OrderByQuery = from c in lCountries.OrderBy(o => o.nPopulation) select c;
            List<Country> lSortedListCountries = OrderByQuery.ToList();
            for (int i = 0; i < lSortedListCountries.Count; i++)
            {
                Console.WriteLine(lSortedListCountries[i].sCode);
                Console.WriteLine(lSortedListCountries[i].sName);
                Console.WriteLine(lSortedListCountries[i].sCapital);
                Console.WriteLine(lSortedListCountries[i].nPopulation);
                Console.WriteLine(lSortedListCountries[i].fArea);
            }


            Console.WriteLine("FILTRIRANJE");
            var EqualQuery = from c in lCountries where c.nPopulation < 3000000 select c;
            List<Country> lFilteredListCountries = EqualQuery.ToList();
            for(int i=0;i<lFilteredListCountries.Count;i++)
            {
                Console.WriteLine(lFilteredListCountries[i].sCode);
                Console.WriteLine(lFilteredListCountries[i].sName);
                Console.WriteLine(lFilteredListCountries[i].sCapital);
                Console.WriteLine(lFilteredListCountries[i].nPopulation);
                Console.WriteLine(lFilteredListCountries[i].fArea);
            }

            Console.ReadKey();
        }
    }
}
