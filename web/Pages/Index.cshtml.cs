using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net;

namespace docker_poc.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string WebServerData = "N/A";
    public string APIData = "N/A";

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        string hostName = Dns.GetHostName(); // Retrive the Name of HOST
        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

        WebServerData = "Enquire time: " + DateTime.Now.ToString("HH:mm:ss.fffff") + " - Hostname: " + hostName + " - IP: " + myIP;
        APIData = QueryAPI();
    }

    private string QueryAPI()
    {
        string ret = "";

        HttpClient client = new HttpClient();

        client.BaseAddress = new Uri("http://api");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        
        Random rnd = new();

        string path = "balance/";

        try
        {
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                ret = response.Content.ReadAsStringAsync().Result;
            }
            else
                ret = "Calling the service got " + response.StatusCode + "\r\n" + response.Content;
        }
        catch (Exception ex)
        {
            ret = "Erro calling " + client.BaseAddress + "\r\n" + ex.ToString();
        }


        return ret;
    }
}
