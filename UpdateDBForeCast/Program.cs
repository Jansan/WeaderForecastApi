using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using WeaderForecastApi.Models;

namespace UpdateDBForeCast
{
    public class Program
    {

        public static System.Timers.Timer aTimer;
        static HttpClient client = new HttpClient();
        static List<Temperatures> ListOfTemperatures = new List<Temperatures>();
        static Random rnd1 = new Random();
        static Random rnd2 = new Random();
        static int round = 0;

        static void Main(string[] args)
        {
            RunAsync().Wait();
            
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:55821/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {

                aTimer = new System.Timers.Timer(5000);
                aTimer.Elapsed += new ElapsedEventHandler(progress);
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
                await GetAllTemperatures();
                
                

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);

            }

            Console.ReadLine();
        }

        //static int result = 0;
        private async static void progress(object source, ElapsedEventArgs e)
        {
            Console.Clear();


            foreach (Temperatures temp in ListOfTemperatures)
            {


                int value1 = rnd1.Next(-15, 25);
                int value2 = rnd2.Next(-20, 30);
                int result = (value1 + value2);
                round = 1;
                temp.Temperature = +result-- / 2;
                round = 2;
                temp.Temperature = +result-- / 3;
                round = 3;
                temp.Temperature = +result-- / 2;
                round++;
                temp.Temperature = +result-- / round++;
                await UpdateTemperatureAsync(temp);





            }

            await GetAllTemperatures();


        }

        //Update Temperature
        static async Task<Temperatures> UpdateTemperatureAsync(Temperatures temp)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/TemperaturesApi/{temp.Id}", temp);
            response.EnsureSuccessStatusCode();

            //Deserialize the update temperature form the response body.
            temp = await response.Content.ReadAsAsync<Temperatures>();
            return temp;
        
        }

        static async Task GetAllTemperatures()
        {
            HttpResponseMessage res = await client.GetAsync("api/TemperaturesApi");
            res.EnsureSuccessStatusCode();

            var temp = res.Content.ReadAsAsync<IEnumerable<Temperatures>>().Result;

            foreach (var t in temp)
            {
                Console.WriteLine("{0} {1}", t.CityName, t.Temperature);
                ListOfTemperatures.Add(t);
            }
            Console.ReadLine();
        }


    }

}
