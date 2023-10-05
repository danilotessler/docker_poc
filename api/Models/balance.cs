namespace Balance.Models;

public class Balance
{
    public int Account { get; set; }
    public double Available { get; set; }

    public Balance(int account, double available)
    {
        this.Account = account;
        this.Available = available;
    }
}