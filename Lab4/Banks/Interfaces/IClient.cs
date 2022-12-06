namespace Banks.Interfaces;

public interface IClient
{
    long Id { get; }
    string Name { get; }
    string Surname { get; }
    void TransferMoney();
}