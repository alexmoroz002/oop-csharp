using Banks.Accounts.Interfaces;
using Banks.Banks;
using Banks.Banks.Config;
using Banks.Client.Builder;

namespace Banks.Console;

public class Program
{
    public static void Main()
    {
        var cb = new CentralBank();

        System.Console.WriteLine("Available options:\n" +
                                 "-register {name} {DebitInterest} {DepositInterest1} {DepositInterest2} {DepositInterest3} {CreditCommission} {SuspiciousLimit} {CreditAccountLimit} - register new bank\n" +
                                 "-addClient {bankId} {name} {surname} {passportNumber}? {address}? - add new client\n" +
                                 "-banks - show registered banks\n" +
                                 "-accounts {bankId} - show accounts in bank with BankId\n" +
                                 "-openDebitAccount {bankId} {clientId}\n" +
                                 "-openCreditAccount {bankId} {clientId}\n" +
                                 "-openDepositAccount {bankId} {clientId} {accountTermMonth} {money}\n" +
                                 "-put {bankId} {accountId} {money} - put money to accountId\n" +
                                 "-take {bankId} {accountId} {money} - take money from accountId\n" +
                                 "-transfer {bankId} {accountId1} {accountId2} {money} - transfer money from accountId1 to accountId2\n" +
                                 "-leapDays {daysCount} - time leap to daysCount days in future\n" +
                                 "-leapMonths {monthsCount} - time leap to monthsCount months in future\n" +
                                 "-leapYears {yearsCount} - time leap to yearsCount years in future\n" +
                                 "-exit - close program\n");

        string line;
        string[] input;
        while (true)
        {
            line = System.Console.ReadLine();
            input = line?.Split(' ');
            switch (input[0])
            {
                case "-register":
                {
                    if (input.Length != 9)
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    decimal[] inputDecimal = new decimal[9];
                    for (int index = 2; index < input.Length; index++)
                    {
                        string s = input[index];
                        if (!decimal.TryParse(s, out inputDecimal[index]))
                        {
                            System.Console.WriteLine("Error parsing");
                            break;
                        }
                    }

                    cb.RegisterBank(
                        input[1],
                        new Config(
                            inputDecimal[2],
                            inputDecimal[3],
                            inputDecimal[4],
                            inputDecimal[5],
                            inputDecimal[6],
                            inputDecimal[7],
                            inputDecimal[8]));
                    break;
                }

                case "-addClient":
                {
                    if (input.Length is < 4 or > 6)
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    if (!int.TryParse(input[1], out int bankId))
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    var clientBuilder = new ClientBuilder(input[2], input[3]);
                    if (input.Length >= 5)
                    {
                        int.TryParse(input[4], out int passportNumber);
                        clientBuilder.AddPassport(passportNumber);
                    }

                    if (input.Length == 6)
                            clientBuilder.AddAddress(input[5]);
                    cb.Banks[bankId].AddClient(clientBuilder.Build());
                    break;
                }

                case "-banks":
                {
                    System.Console.WriteLine("BankId    BankName");
                    for (int index = 0; index < cb.Banks.Count; index++)
                    {
                        IBank bank = cb.Banks[index];
                        System.Console.WriteLine($"{index}          {bank.Name}");
                    }

                    break;
                }

                case "-accounts":
                {
                    if (input.Length != 2)
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    if (!int.TryParse(input[1], out int bankId))
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    System.Console.WriteLine("AccountId    Money");
                    for (int index = 0; index < cb.Banks[bankId].BankAccounts.Count; index++)
                    {
                        IBankAccount bankAccount = cb.Banks[bankId].BankAccounts[index];
                        System.Console.WriteLine($"{index}          {bankAccount.Money}");
                    }

                    break;
                }

                case "-openDebitAccount":
                {
                    if (input.Length != 3)
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    int[] inputInt = new int[3];
                    for (int i = 1; i < 3; i++)
                    {
                        if (!int.TryParse(input[i], out inputInt[i]))
                        {
                            System.Console.WriteLine("Parse error");
                            break;
                        }
                    }

                    cb.Banks[inputInt[1]].OpenDebitAccount(cb.Banks[inputInt[1]].Clients[inputInt[2]]);
                    break;
                }

                case "-openCreditAccount":
                {
                    if (input.Length != 3)
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    int[] inputInt = new int[3];
                    for (int i = 1; i < 3; i++)
                    {
                        if (!int.TryParse(input[i], out inputInt[i]))
                        {
                            System.Console.WriteLine("Parse error");
                            break;
                        }
                    }

                    cb.Banks[inputInt[1]].OpenCreditAccount(cb.Banks[inputInt[1]].Clients[inputInt[2]]);
                    break;
                }

                case "-openDepositAccount":
                {
                    if (input.Length != 5)
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    int[] inputInt = new int[4];
                    for (int i = 1; i < 4; i++)
                    {
                        if (!int.TryParse(input[i], out inputInt[i]))
                        {
                            System.Console.WriteLine("Parse error");
                            break;
                        }
                    }

                    if (!decimal.TryParse(input[4], out decimal money))
                    {
                        System.Console.WriteLine("Parse error");
                        break;
                    }

                    cb.Banks[inputInt[1]].OpenDepositAccount(cb.Banks[inputInt[1]].Clients[inputInt[2]], inputInt[3], money);
                    break;
                }

                case "-put":
                {
                    if (input.Length != 4)
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    int[] inputInt = new int[3];
                    for (int i = 1; i < 3; i++)
                    {
                        if (!int.TryParse(input[i], out inputInt[i]))
                        {
                            System.Console.WriteLine("Parse error");
                            break;
                        }
                    }

                    if (!decimal.TryParse(input[3], out decimal money))
                    {
                        System.Console.WriteLine("Parse error");
                        break;
                    }

                    cb.Banks[inputInt[1]].BankAccounts[inputInt[2]].PutMoney(money);

                    break;
                }

                case "-take":
                {
                    if (input.Length != 4)
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    int[] inputInt = new int[3];
                    for (int i = 1; i < 3; i++)
                    {
                        if (!int.TryParse(input[i], out inputInt[i]))
                        {
                            System.Console.WriteLine("Parse error");
                            break;
                        }
                    }

                    if (!decimal.TryParse(input[3], out decimal money))
                    {
                        System.Console.WriteLine("Parse error");
                        break;
                    }

                    cb.Banks[inputInt[1]].BankAccounts[inputInt[2]].TakeMoney(money);

                    break;
                }

                case "-transfer":
                {
                    if (input.Length != 5)
                    {
                        System.Console.WriteLine("Error");
                        break;
                    }

                    int[] inputInt = new int[4];
                    for (int i = 1; i < 4; i++)
                    {
                        if (!int.TryParse(input[i], out inputInt[i]))
                        {
                            System.Console.WriteLine("Parse error");
                            break;
                        }
                    }

                    if (!decimal.TryParse(input[4], out decimal money))
                    {
                        System.Console.WriteLine("Parse error");
                        break;
                    }

                    cb.Banks[inputInt[1]].PerformTransaction(cb.Banks[inputInt[1]].BankAccounts[inputInt[2]], money, cb.Banks[inputInt[1]].BankAccounts[inputInt[3]]);

                    break;
                }

                case "-leapDays":
                {
                    for (int i = 0; i < int.Parse(input[1]); i++)
                    {
                        cb.PlusDay();
                    }

                    break;
                }

                case "-leapMonths":
                {
                    for (int i = 0; i < int.Parse(input[1]); i++)
                    {
                        cb.PlusMonth();
                    }

                    break;
                }

                case "-leapYears":
                {
                    for (int i = 0; i < int.Parse(input[1]); i++)
                    {
                        cb.PlusYear();
                    }

                    break;
                }

                case "-exit":
                {
                    Environment.Exit(0);
                    break;
                }

                default:
                {
                    System.Console.WriteLine("Unknown command");
                    break;
                }
            }
        }
    }
}