using System;
using BankSystem;

namespace BankSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var bank = new Bank();

            // создание клиентов
            var c1 = bank.AddClient("иван петров", 25, "+79991234567", "ivan@mail.ru");
            var c2 = bank.AddClient("анна смирнова", 30, "+79997654321", "anna@mail.ru");
            
            // открытие счетов
            bank.OpenAccount(c1.Id, "saving", "RUB", 1000m, 5.5m);
            bank.OpenAccount(c1.Id, "premium", "USD", 10000m, 50m);
            bank.OpenAccount(c2.Id, "investment", "EUR");

            // операции
            Console.WriteLine("\n=== ОПЕРАЦИИ СО СЧЕТАМИ ===\n");
            bank.Deposit(c1.AccountIds[0], 10000);
            
            Console.WriteLine("\nпопытка снять 150000 (должна быть ошибка):");
            try
            {
                bank.Withdraw(c1.AccountIds[0], 150000);
            }
            catch (InvalidOperationError e)
            {
                Console.WriteLine($"ошибка: {e.Message}");
            }

            // аутентификация
            Console.WriteLine("\n=== АУТЕНТИФИКАЦИЯ ===\n");
            bank.AuthenticateClient(c1.Id, "+79991234567");
            
            // неверные попытки
            Console.WriteLine("\nневерная попытка 1:");
            try { bank.AuthenticateClient(c1.Id, "wrong"); } catch (Exception e) { Console.WriteLine($"ошибка: {e.Message}"); }
            
            Console.WriteLine("\nневерная попытка 2:");
            try { bank.AuthenticateClient(c1.Id, "wrong"); } catch (Exception e) { Console.WriteLine($"ошибка: {e.Message}"); }
            
            Console.WriteLine("\nневерная попытка 3 (блокировка):");
            try { bank.AuthenticateClient(c1.Id, "wrong"); } catch (Exception e) { Console.WriteLine($"ошибка: {e.Message}"); }

            // заморозка
            Console.WriteLine("\n=== ЗАМОРОЗКА СЧЕТА ===\n");
            bank.FreezeAccount(c1.AccountIds[0]);
            
            Console.WriteLine("попытка пополнения замороженного счета:");
            try { bank.Deposit(c1.AccountIds[0], 1000); } catch (Exception e) { Console.WriteLine($"ошибка: {e.Message}"); }
            
            bank.UnfreezeAccount(c1.AccountIds[0]);
            Console.WriteLine("после разморозки:");
            bank.Deposit(c1.AccountIds[0], 1000);
            
            Console.WriteLine("\n=== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО ===");
        }
    }
}