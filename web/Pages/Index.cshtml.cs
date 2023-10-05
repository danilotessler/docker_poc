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
        if (!string.IsNullOrEmpty(HttpContext.Request.Query["uri"]))
        {
            string uri = HttpContext.Request.Query["uri"].ToString();
            Balance bal = GetBalance(uri);

            Message = "After calling the service: " + uri + "<br/> The balance for the member " + bal.Account + " is : " + bal.Available;
        }
    }

    private Balance GetBalance(string uri)
    {
        Balance ret = new();

        HttpClient client = new HttpClient();

        client.BaseAddress = new Uri("http://" + uri);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        
        Random rnd = new Random();

        string path = "balance/" + rnd.Next(100).ToString();

        HttpResponseMessage response = client.GetAsync(path).Result;
        if (response.IsSuccessStatusCode)
        {
            ret = response.Content.ReadFromJsonAsync<Balance>().Result;
        }
        else
            throw new ApplicationException("Calling the service got " + response.StatusCode + "\r\n" + response.Content);

        return ret;
    }
}

public class Balance
{
    public int Account { get; set; }
    public double Available { get; set; }
}