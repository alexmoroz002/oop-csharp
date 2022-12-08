using Banks.Accounts.Interfaces;
using Banks.Client.Builder;

namespace Banks.Client;

public class Client : IClient
{
    private int? _passport;
    private string _address;
    private List<IBankAccount> _accounts;

    internal Client(IBuilder builder)
    {
        IsSuspicious = true;
        Name = builder.Name;
        Surname = builder.Surname;
        Address = builder.Address;
        Passport = builder.Passport;
    }

    public string Name { get; init; }

    public string Surname { get; init; }

    public int? Passport
    {
        get => _passport;
        set
        {
            if (_passport != null)
                throw new NotImplementedException();
            _passport = value;
            if (Passport == null || Address == null) return;

            IsSuspicious = false;
            foreach (IBankAccount bankAccount in _accounts)
            {
                bankAccount.UpdateTerms();
            }
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            if (_address != null)
                throw new NotImplementedException();
            _address = value;
            if (Passport == null || Address == null) return;

            IsSuspicious = false;
            foreach (IBankAccount bankAccount in _accounts)
            {
                bankAccount.UpdateTerms();
            }
        }
    }

    public bool IsSuspicious { get; private set; }
    public IReadOnlyList<IBankAccount> Accounts { get; }

    public void TransferMoney()
    {
        throw new NotImplementedException();
    }
}