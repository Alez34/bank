using System;
using BankAccountSystem;

namespace BankAccountSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== тестирование банковского счета ===\n");

            Console.WriteLine("1. создание активного счета (RUB):");
            var activeAccount = new BankAccount("иван петров", "RUB");
            Console.WriteLine(activeAccount);
            Console.WriteLine();

            Console.WriteLine("2. пополнение на 5000 руб:");
            activeAccount.Deposit(5000);
            Console.WriteLine();

            Console.WriteLine("3. снятие 1500 руб:");
            activeAccount.Withdraw(1500);
            Console.WriteLine();

            Console.WriteLine("4. создание замороженного счета (USD):");
            var frozenAccount = new BankAccount("мария сидорова", "USD");
            frozenAccount.SetStatus(AccountStatus.Frozen);
            Console.WriteLine(frozenAccount);
            Console.WriteLine();

            Console.WriteLine("5. попытка пополнения замороженного счета:");
            try
            {
                frozenAccount.Deposit(1000);
            }
            catch (AccountFrozenError e)
            {
                Console.WriteLine($"ошибка: {e.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("6. попытка снятия с замороженного счета:");
            try
            {
                frozenAccount.Withdraw(500);
            }
            catch (AccountFrozenError e)
            {
                Console.WriteLine($"ошибка: {e.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("7. попытка снять больше чем на счете:");
            try
            {
                activeAccount.Withdraw(10000);
            }
            catch (InsufficientFundsError e)
            {
                Console.WriteLine($"ошибка: {e.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("8. попытка внести отрицательную сумму:");
            try
            {
                activeAccount.Deposit(-500);
            }
            catch (InvalidOperationError e)
            {
                Console.WriteLine($"ошибка: {e.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("9. попытка создать счет с неверной валютой:");
            try
            {
                var invalidAccount = new BankAccount("тест тестов", "BTC");
            }
            catch (InvalidOperationError e)
            {
                Console.WriteLine($"ошибка: {e.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("10. финальная информация по активному счету:");
            Console.WriteLine(activeAccount);
            Console.WriteLine();

            Console.WriteLine("=== тестирование завершено ===");
        }
    }
}