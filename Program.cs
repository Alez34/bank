using System;
using BankAccountSystem;

namespace BankAccountSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== ТЕСТИРОВАНИЕ БАНКОВСКИХ СЧЕТОВ ===\n");

            // ========== 1. ТЕСТИРОВАНИЕ SAVINGACCOUNT ==========
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("1. ТЕСТИРОВАНИЕ SAVINGACCOUNT");
            Console.WriteLine("═══════════════════════════════════════\n");

            var savingAcc = new SavingAccount("анна смирнова", "RUB", 1000, 5.5m);
            Console.WriteLine(savingAcc);
            Console.WriteLine();

            Console.WriteLine("пополнение на 10000 руб:");
            savingAcc.Deposit(10000);
            Console.WriteLine();

            Console.WriteLine("расчет прибыли за месяц:");
            decimal profit = savingAcc.CalculateMonthlyProfit();
            Console.WriteLine($"прибыль за месяц: {profit:F2} RUB");
            Console.WriteLine();

            Console.WriteLine("начисление процентов:");
            savingAcc.ApplyMonthlyInterest();
            Console.WriteLine();

            Console.WriteLine($"текущий баланс: {savingAcc.Balance:F2} RUB, минимальный остаток: 1000 RUB");
            Console.WriteLine();

            Console.WriteLine("попытка снять 9500 руб (останется 1050 >= 1000):");
            savingAcc.Withdraw(9500);
            Console.WriteLine();

            Console.WriteLine("попытка снять 200 руб (останется 850 < 1000) - должна быть ошибка:");
            try
            {
                savingAcc.Withdraw(200);
            }
            catch (InvalidOperationError e)
            {
                Console.WriteLine($"ошибка: {e.Message}");
            }
            Console.WriteLine();

            Console.WriteLine("текущее состояние счета:");
            Console.WriteLine(savingAcc);
            Console.WriteLine();

            // ========== 2. ТЕСТИРОВАНИЕ PREMIUMACCOUNT ==========
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("2. ТЕСТИРОВАНИЕ PREMIUMACCOUNT");
            Console.WriteLine("═══════════════════════════════════════\n");

            var premiumAcc = new PremiumAccount("алексей морозов", "USD", 500000, 10000, 50);
            Console.WriteLine(premiumAcc);
            Console.WriteLine();

            Console.WriteLine("пополнение на 20000 USD:");
            premiumAcc.Deposit(20000);
            Console.WriteLine();

            Console.WriteLine("снятие 25000 USD (уход в овердрафт на 5000 + комиссия 50):");
            premiumAcc.Withdraw(25000);
            Console.WriteLine();

            Console.WriteLine("повторная информация по счету:");
            Console.WriteLine(premiumAcc);
            Console.WriteLine();

            Console.WriteLine("попытка снять еще 6000 USD (превышение лимита овердрафта):");
            try
            {
                premiumAcc.Withdraw(6000);
            }
            catch (InsufficientFundsError e)
            {
                Console.WriteLine($"ошибка: {e.Message}");
            }
            Console.WriteLine();

            // ========== 3. ТЕСТИРОВАНИЕ INVESTMENTACCOUNT ==========
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("3. ТЕСТИРОВАНИЕ INVESTMENTACCOUNT");
            Console.WriteLine("═══════════════════════════════════════\n");

            var investAcc = new InvestmentAccount("елена волкова", "EUR");
            Console.WriteLine(investAcc);
            Console.WriteLine();

            Console.WriteLine("добавление активов в портфель:");
            investAcc.AddAsset("Stocks (Tech)", 15000, 15.2m);
            investAcc.AddAsset("Bonds (Government)", 8000, 4.5m);
            investAcc.AddAsset("ETF (Global)", 12000, 9.8m);
            Console.WriteLine();

            Console.WriteLine("прогнозируемая годовая доходность:");
            decimal yearlyGrowth = investAcc.ProjectYearlyGrowth();
            Console.WriteLine($"{yearlyGrowth:F2} EUR");
            Console.WriteLine();

            Console.WriteLine("информация об инвестиционном счете:");
            Console.WriteLine(investAcc);
            Console.WriteLine();

            Console.WriteLine("продажа части активов (Stocks на 5000 EUR):");
            investAcc.RemoveAsset("Stocks (Tech)", 5000);
            Console.WriteLine();

            Console.WriteLine("финальная информация:");
            Console.WriteLine(investAcc);
            Console.WriteLine();

            // ========== 4. ДОПОЛНИТЕЛЬНЫЕ ТЕСТЫ ==========
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("4. ДОПОЛНИТЕЛЬНЫЕ ТЕСТЫ");
            Console.WriteLine("═══════════════════════════════════════\n");

            Console.WriteLine("создание второго сберегательного счета (USD):");
            var savingAcc2 = new SavingAccount("петр иванов", "USD", 500, 3.2m);
            savingAcc2.Deposit(3000);
            Console.WriteLine(savingAcc2);
            Console.WriteLine();

            Console.WriteLine("создание второго премиум счета (EUR):");
            var premiumAcc2 = new PremiumAccount("мария козлова", "EUR", 300000, 5000, 30);
            premiumAcc2.Deposit(100000);
            premiumAcc2.Withdraw(102000);
            Console.WriteLine(premiumAcc2);
            Console.WriteLine();

            Console.WriteLine("создание второго инвестиционного счета с криптоактивами:");
            var investAcc2 = new InvestmentAccount("игорь соколов", "USD");
            investAcc2.AddAsset("Bitcoin", 50000, 25.0m);
            investAcc2.AddAsset("Ethereum", 30000, 18.5m);
            Console.WriteLine(investAcc2);
            Console.WriteLine();

            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("ТЕСТИРОВАНИЕ ЗАВЕРШЕНО");
            Console.WriteLine("═══════════════════════════════════════");
        }
    }
}