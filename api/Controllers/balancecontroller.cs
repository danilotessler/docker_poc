using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Balance.Controllers;

[Route("[controller]")]
[ApiController]
public class BalanceController : ControllerBase
{
    public BalanceController() {}

    [HttpGet("")]
    public async Task<ActionResult<string>> GetBalance()
    {
        string hostName = Dns.GetHostName(); // Retrive the Name of HOST
        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

        return "Enquire time: " + DateTime.Now.ToString("HH:mm:ss.fffff") + " - Hostname: " + hostName + " - IP: " + myIP;
    }
}