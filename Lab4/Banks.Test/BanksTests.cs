using Banks.Accounts.Interfaces;
using Banks.Banks;
using Banks.Banks.Config;
using Banks.Client;
using Banks.Client.Builder;
using Xunit;

namespace Banks.Test;

public class BanksTests
{
    [Fact]
    public void AddBankWithAccount_AccountCreated()
    {
        var cb = new CentralBank();
        IBank sber = cb.RegisterBank("Sber", new Config(365, 10, 12, 15, 5, 10000, 50000));
        IClient client = new ClientBuilder("Alex", "Moroz").AddAddress("null").Build();
        sber.AddClient(client);
        IBankAccount acc = sber.OpenDebitAccount(client);
        acc.PutMoney(100);
        cb.PlusDay();
    }

    [Fact]
    public void CreateSuspiciousClient_ChangeSuspiciousness_LimitsDisabled()
    {
        var cb = new CentralBank();
        IBank sber = cb.RegisterBank("Sber", new Config(365, 10, 12, 15, 5, 10000, 50000));
        var builder = new ClientBuilder("Alex", "Moroz");
        IClient client = builder.AddAddress("null").Build();
        sber.AddClient(client);
        IBankAccount acc = sber.OpenDebitAccount(client);
        bool prevState = acc.IsSuspicious;
        client.Passport = 1234567890;
        Assert.NotEqual(prevState, acc.IsSuspicious);
    }

    [Fact]
    public void PutMoneyInAccount_WaitOneMonth_MoneyAmountChanged()
    {
        var cb = new CentralBank();
        IBank sber = cb.RegisterBank("Sber", new Config(10, 10, 12, 12, 5, 10000, 50000));
        var builder = new ClientBuilder("Alex", "Moroz");
        IClient client = builder.AddAddress("null").AddPassport(1234567890).Build();
        sber.AddClient(client);
        IBankAccount acc = sber.OpenDepositAccount(client, 12, 10000);
        cb.PlusMonth();
        Assert.NotEqual(10000, acc.Money);
    }

    [Fact]
    public void SubscribeToTermsChange_NotificationGot()
    {
        var cb = new CentralBank();
        IBank sber = cb.RegisterBank("Sber", new Config(10, 10, 12, 12, 5, 10000, 50000));
        var builder = new ClientBuilder("Alex", "Moroz");
        IClient client = builder.AddAddress("null").AddPassport(1234567890).Build();
        sber.AddClient(client);
        sber.AddSubscriber(client);
        sber.ChangeAccountTerms(sber.Config);
    }
}