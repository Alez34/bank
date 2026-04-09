using System;

namespace BankAccountSystem
{
    public class SavingAccount : BankAccount
    {
        private decimal _minBalance;
        private decimal _monthlyInterestRate;

        public decimal MinBalance => _minBalance;
        public decimal MonthlyInterestRate => _monthlyInterestRate;

        public SavingAccount(string ownerName, string currency, decimal minBalance, decimal monthlyInterestRate, string? accountId = null) 
            : base(ownerName, currency, accountId)
        {
            _minBalance = minBalance;
            _monthlyInterestRate = monthlyInterestRate;
        }

        public override void Withdraw(decimal amount)
        {
            ValidateAmount(amount);
            ValidateStatus();
            
            // проверка на минимальный остаток
            if (_balance - amount < _minBalance)
                throw new InvalidOperationError($"снятие невозможно. минимальный остаток должен быть не менее {_minBalance} {_currency}");
            
            _balance -= amount;
            Console.WriteLine($"снятие {amount} {_currency}. текущий баланс: {_balance:F2} {_currency}");
        }

        public decimal CalculateMonthlyProfit()
        {
            decimal profit = _balance * (_monthlyInterestRate / 100);
            return profit;
        }

        public void ApplyMonthlyInterest()
        {
            decimal profit = CalculateMonthlyProfit();
            _balance += profit;
            Console.WriteLine($"начислены проценты: {profit:F2} {_currency}. ставка: {_monthlyInterestRate}%. новый баланс: {_balance:F2} {_currency}");
        }

        public override string GetAccountInfo()
        {
            string lastFourDigits = _accountId.Length >= 4 
                ? _accountId.Substring(_accountId.Length - 4) 
                : _accountId;
            
            return $"тип счета: SavingAccount\n" +
                   $"клиент: {_ownerName}\n" +
                   $"номер счета: ****{lastFourDigits}\n" +
                   $"статус: {_status}\n" +
                   $"баланс: {_balance:F2} {_currency}\n" +
                   $"минимальный остаток: {_minBalance} {_currency}\n" +
                   $"месячная ставка: {_monthlyInterestRate}%";
        }

        public override string ToString()
        {
            return GetAccountInfo();
        }
    }
}