namespace Banks.Client.Builder;

public class ClientBuilder : IBuilder
{
    public ClientBuilder(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public string Name { get; private set; }
    public string Surname { get; private set; }
    public int Passport { get; private set; }
    public string Address { get; private set; }

    public IBuilder AddPassport(int passport)
    {
        Passport = passport;
        return this;
    }

    public IBuilder AddAddress(string address)
    {
        Address = address;
        return this;
    }

    public IClient Build()
    {
        return new Client(this);
    }
}