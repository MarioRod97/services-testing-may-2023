using Banking;

namespace ScratchTests.BankAccount;

public class NewAccount
{
    [Fact]
    public void NewAccountsHaveCorrectBalance()
    {
        var account = new Account();

        decimal openingBalance = account.GetBalance();

        Assert.Equal(5000M, openingBalance);
    }
}
