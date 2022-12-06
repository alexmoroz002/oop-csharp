using Banks.Entities;

namespace Banks.Builder;

public interface IBuilder
{
    void AddName(string name);
    void AddSurname(string surname);
    void AddPassport(int passport);
    void AddAddress(string address);
    Client BuildClient(ClientBuilder builder);
}