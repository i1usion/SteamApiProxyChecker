using NUnit.Framework;
using RestEase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WeatherAppExample.Api;
using WeatherAppExample.Models;

namespace WeatherAppExample
{
    class Program
    {

        private static ISteamChecker client;


        static async Task Main(string[] args)
        {



            string[] ApiArray = new string[1024];
            string[] ProxyArray = new string[10000];
            string[] CorrectApi = new string[1024];
            string[] CorrectProxy = new string[10000];
            int ApiCount = 0;
            int ProxyCount = 0;
            int CurrentCorrectApi = 0;
            int CurrentCorrectProxy = 0;

            try
            {
                using (StreamWriter sw = new StreamWriter("api.txt", true, System.Text.Encoding.Default))

                {
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            using (StreamReader sr = new StreamReader("api.txt", System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ApiArray[ApiCount] = line;
                    ApiCount++;
                }
            }


            string url = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002";
            client = RestClient.For<ISteamChecker>(url);


            for (int CurrentApi = 0; CurrentApi < ApiCount;)
            {

                var tasks = new List<Task>();
                tasks.Add(Get("76561198143987061", ApiArray, CurrentApi));

                CurrentApi++;
                Console.WriteLine(Convert.ToString(CurrentApi));

                try
                {
                    Task.WaitAll(tasks.ToArray());
                }

                catch (Exception)
                {
                    Console.WriteLine("Api is not valid - " + ApiArray[CurrentApi]);
                    continue;
                }

                CorrectApi[CurrentCorrectApi] = ApiArray[CurrentApi - 1];
                CurrentCorrectApi++;

            }
            Console.WriteLine("Scan is done. Api count before scan - " + ApiCount + "\nApi count after scan - " + CurrentCorrectApi);



            try
            {
                using (StreamWriter sw = new StreamWriter("api_updated.txt", false, System.Text.Encoding.Default))

                {
                    for (int CurrentApi = 0; CurrentApi < CurrentCorrectApi - 1; CurrentApi++)
                    {
                        sw.WriteLine(CorrectApi[CurrentApi]);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            //The end of api checker part


            try
            {
                using (StreamWriter sw = new StreamWriter("proxy.txt", true, System.Text.Encoding.Default))

                {
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            using (StreamReader sr = new StreamReader("proxy.txt", System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ProxyArray[ProxyCount] = line;
                    ProxyCount++;
                }
            }


            WebClient web = new WebClient();


            for (int CurrentProxy = 0; CurrentProxy < ProxyCount;)

            {

                {
                    WebProxy myproxy = new WebProxy(ProxyArray[CurrentProxy]);
                    //myproxy.Credentials = new NetworkCredential("Q8OpfdbG9U", "VAGA5X6Wk1");
                    web.Proxy = myproxy;
                    CurrentProxy++;


                    try
                    {
                        //web.OpenReadAsync(new Uri("https://steamcommunity.com/")); //async one
                        web.OpenRead("https://steamcommunity.com/"); //sync one

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("Proxy is not valid - " + ProxyArray[CurrentProxy - 1]);
                        continue;
                    }

                    Console.WriteLine("Proxy is correct - " + ProxyArray[CurrentProxy - 1]);
                    CorrectProxy[CurrentCorrectProxy] = ProxyArray[CurrentProxy - 1];
                    CurrentCorrectProxy++;
                }


            }

            Console.WriteLine("Scan is done. Proxy count before scan - " + ProxyCount + "\nProxy count after scan - " + CurrentCorrectProxy);

            try
            {
                using (StreamWriter sw = new StreamWriter("proxy_updated.txt", false, System.Text.Encoding.Default))

                {
                    for (int CurrentProxy = 0; CurrentProxy < CurrentCorrectProxy - 1; CurrentProxy++)
                    {
                        sw.WriteLine(CorrectProxy[CurrentProxy]);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }

        private static async Task<SteamChecker> Get(string steamids, string[] ApiArray, int CurrentApi)
        {

            string key = ApiArray[CurrentApi];

            var response1 = await client.GetSteam(key, steamids);
            return response1;
        }

    }
    }
