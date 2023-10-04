using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace docker_poc.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string Env = "Not Provided";

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        if (!string.IsNullOrEmpty(HttpContext.Request.Query["env"]))
            Env = HttpContext.Request.Query["env"].ToString();
    }
}
