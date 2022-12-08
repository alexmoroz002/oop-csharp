namespace Banks.Client.Builder;

public interface IBuilder
{
    string Name { get; }
    string Surname { get; }
    int Passport { get; }
    string Address { get; }
    IBuilder AddPassport(int passport);
    IBuilder AddAddress(string address);
    IClient Build();
}