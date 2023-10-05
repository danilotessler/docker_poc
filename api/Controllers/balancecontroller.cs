using Microsoft.AspNetCore.Mvc;
using Balance.Models;

namespace Balance.Controllers;

[Route("[controller]")]
[ApiController]
public class BalanceController : ControllerBase
{
    public BalanceController() {}

    [HttpGet("{account}")]
    public async Task<ActionResult<Balance.Models.Balance>> GetBalance(int account)
    {
        Random rnd = new Random();

        var ret = new Balance.Models.Balance(account, rnd.Next());

        return ret;
    }
}