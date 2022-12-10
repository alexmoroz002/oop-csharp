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
}