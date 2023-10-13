using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace docker_poc.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string Message = "No API URI provided";

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Message = "Call 1 : " + GetResult("api") + "<br />";
    }

    private string GetResult(string uri)
    {
        string ret = "";

        HttpClient client = new HttpClient();

        client.BaseAddress = new Uri("http://" + uri);
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
