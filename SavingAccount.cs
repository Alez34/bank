using System;

namespace BankSystem
{
    public class SavingAccount : BankAccount
    {
        private decimal _minBalance;
        private decimal _monthlyInterestRate;

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
            if (_balance - amount < _minBalance)
                throw new InvalidOperationError($"минимальный остаток {_minBalance} {_currency}");
            _balance -= amount;
            Console.WriteLine($"снятие {amount} {_currency}. баланс: {_balance:F2} {_currency}");
        }

        public decimal CalculateMonthlyProfit() => _balance * (_monthlyInterestRate / 100);

        public void ApplyMonthlyInterest()
        {
            decimal profit = CalculateMonthlyProfit();
            _balance += profit;
            Console.WriteLine($"начислены проценты: {profit:F2} {_currency}. баланс: {_balance:F2} {_currency}");
        }

        public override string GetAccountInfo()
        {
            string lastFourDigits = _accountId.Length >= 4 ? _accountId.Substring(_accountId.Length - 4) : _accountId;
            return $"тип: SavingAccount | клиент: {_ownerName} | счет: ****{lastFourDigits} | статус: {_status} | баланс: {_balance:F2} {_currency} | мин.остаток: {_minBalance} | ставка: {_monthlyInterestRate}%";
        }
    }
}