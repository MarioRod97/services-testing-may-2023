namespace ScratchTests.BankAccount;

public class MakingWithdrawals {
    
    [Theory]
    [InlineData(100)]
    [InlineData(5000)]
    public void MakingWithdrawalsLowersBalance(decimal amountToWithdraw)
    {
        var account = new Account();
        var openingBalance = account.GetBalance();

        account.Withdraw(amountToWithdraw);

        Assert.Equal(openingBalance - amountToWithdraw, account.GetBalance());
    }

    [Fact]
    public void CanTakeAllMoneyFromAccount()
    {
        var account = new Account();
        var openingBalance = account.GetBalance();

        account.Withdraw(openingBalance);

        Assert.Equal(0M, account.GetBalance());
    }
}
