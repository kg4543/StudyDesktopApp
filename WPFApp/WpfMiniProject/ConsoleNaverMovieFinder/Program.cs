using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNaverMovieFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientID = "Jj1Shad6ZWLtrFfb1bBd";
            string clientSecret = "b06WyD2SYf";
            string search = "starwars"; //변경가능
            string openApiUrl = $"https://openapi.naver.com/v1/search/movie?query={search}";

            var responseJson = GetOpenApiResult(openApiUrl, clientID, clientSecret);

            JObject parsedJson = JObject.Parse(responseJson);

            int total = Convert.ToInt32(parsedJson["total"]);
            Console.WriteLine($"총 검색결과 : {total}");
            
            int display = Convert.ToInt32(parsedJson["display"]);
            Console.WriteLine($"총 검색결과 : {display}");

            JToken items = parsedJson["items"];
            JArray json_array = (JArray)items;

            foreach (var item in json_array)
            {
                Console.WriteLine($"{item["title"]} /{item["image"]} / {item["subtitle"]} / {item["actor"]}" );
            }
        }

        private static string GetOpenApiResult(string openApiUrl, string clientID, string clientSecret)
        {
            var result = "";

            try
            {
                WebRequest request = WebRequest.Create(openApiUrl);
                request.Headers.Add("X-Naver-Client-ID",clientID);
                request.Headers.Add("X-Naver-Client-Secret", clientSecret);

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                result = reader.ReadToEnd();

                reader.Close();
                stream.Close();
                response.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외 발생 : {ex}");
            }

            return result;
        }
    }
}
