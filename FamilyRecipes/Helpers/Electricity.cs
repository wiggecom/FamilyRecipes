using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FamilyRecipes.Models;
using System.Diagnostics;

namespace FamilyRecipes.Helpers
{
    public class Electricity
    {
        private static Uri BaseAddress = new Uri("https://www.elprisetjustnu.se");
        public Electricity()
        {
            
        }

        public static async Task<List<ElectricityPrice>> GetPrice(int dayAdd)
        {
            List<ElectricityPrice> priceList = new List<ElectricityPrice>();

            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.AddDays(dayAdd).ToString("dd");
            string region = "SE3";
            string apiurl = "/api/v1/prices/" + year + "/" + month + "-" + day + "_" + region + ".json";

            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpResponseMessage response = await client.GetAsync(apiurl);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    priceList = JsonSerializer.Deserialize<List<ElectricityPrice>>(responseString);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No bueno hombre!");
                }
                return priceList;
            }
        }
    }


}
