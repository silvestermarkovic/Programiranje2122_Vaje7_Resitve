using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PrimeriKlicevTask
{
    class sync
    {
        public async void test(string url)
        {
            //string result = Get(queryString).Result;
            string result = await vrniStranAsync(url);

            Console.WriteLine(result);
        }
        public async void  test_url(string url)
        {
            //string result = Get(queryString).Result;
            string result = await vrniStranAsync(url);
            Console.WriteLine(url);
            
        }

        public async Task<string> vrniStranAsync(string p_url)
        {
            HttpClient _httpClient = new HttpClient();
            string url = p_url;

            // The actual Get method
            using (var result = await _httpClient.GetAsync($"{url}"))
            {
                string content = await result.Content.ReadAsStringAsync();
                return content;
            }
        }
    }
}
