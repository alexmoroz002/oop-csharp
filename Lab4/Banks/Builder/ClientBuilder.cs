using Banks.Entities;

namespace Banks.Builder;

public class ClientBuilder : IBuilder
{
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public int Passport { get; private set; }
    public string Address { get; private set; }

    public void AddName(string name)
    {
        Name = name;
    }

    public void AddSurname(string surname)
    {
        Surname = surname;
    }

    public void AddPassport(int passport)
    {
        Passport = passport;
    }

    public void AddAddress(string address)
    {
        Address = address;
    }

    public Client BuildClient(ClientBuilder builder)
    {
        return new Client(builder);
    }
}