using DemoClients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleWebClientDemo
{
    internal class Program {

        static Uri _host = new Uri("https://localhost:7135");

        static IEnumerable<Organization> GetOrganizations(int skip = 0, int take = 50)
        {
            var url = new Uri(_host, $"api/Organizations?take={take}&skip={skip}");
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("accept", "*/*");
            try
            {
                string s = webClient.DownloadString(url);
                var result = JsonConvert.DeserializeObject<QueryResult<Organization>>(s);
                return result.Items;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
        static Organization PostOrganization(Organization organization)
        {
            var url = new Uri(_host, $"api/Organizations");
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("accept", "*/*");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            try
            {
                string data = JsonConvert.SerializeObject(organization);
                string s = webClient.UploadString(url, data);
                var result =  JsonConvert.DeserializeObject<Organization>(s);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static void DeleteOrganization(long id)
        {
            var url = new Uri(_host, $"api/Organizations/{id}");
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "DELETE";
            request.Headers.Add("accept", "*/*");

            try
            {
                request.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static string ReadThePage(string host)
        {
            var url = new Uri(host);
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("accept", "*/*");
            try
            {
                string s = webClient.DownloadString(url);
                //var result = JsonConvert.DeserializeObject<string>(s);
                using (StreamWriter writer = new StreamWriter("C:\\Users\\Privat\\Desktop\\ASd.html"))
                {
                    // Write the string data to the file
                    writer.Write(s);
                }
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        static void Main(string[] args)
        {
            
            //var p = PostOrganization(new Organization() { FullName = "TestOrgConsoleFullName", Name = "TestOrgConsoleName" });
            //Console.WriteLine(p.FullName);
            
            var s = ReadThePage("https://www.google.com");
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
