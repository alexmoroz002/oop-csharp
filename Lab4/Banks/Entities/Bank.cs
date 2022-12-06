using Banks.Interfaces;

namespace Banks.Entities;

public class Bank
{
    private List<IClient> _clients;
    private List<IBankAccount> _accounts;
}