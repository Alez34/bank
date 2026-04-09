using System;
using System.Linq;

namespace BankSystem
{
    public class BankAccount : AbstractAccount
    {
        protected string _currency;
        
        public string Currency => _currency;
        public string AccountType => "BankAccount";

        public BankAccount(string ownerName, string currency, string? accountId = null) 
            : base(ownerName, accountId)
        {
            ValidateCurrency(currency);
            _currency = currency.ToUpper();
        }

        private void ValidateCurrency(string currency)
        {
            string[] validCurrencies = { "RUB", "USD", "EUR", "KZT", "CNY" };
            if (!validCurrencies.Contains(currency.ToUpper()))
                throw new InvalidOperationError($"валюта {currency} не поддерживается.");
        }

        public override void Deposit(decimal amount)
        {
            ValidateAmount(amount);
            ValidateStatus();
            _balance += amount;
            Console.WriteLine($"пополнение на {amount} {_currency}. баланс: {_balance:F2} {_currency}");
        }

        public override void Withdraw(decimal amount)
        {
            ValidateAmount(amount);
            ValidateStatus();
            if (amount > _balance)
                throw new InsufficientFundsError("недостаточно средств.");
            _balance -= amount;
            Console.WriteLine($"снятие {amount} {_currency}. баланс: {_balance:F2} {_currency}");
        }

        public override string GetAccountInfo()
        {
            string lastFourDigits = _accountId.Length >= 4 ? _accountId.Substring(_accountId.Length - 4) : _accountId;
            return $"тип: BankAccount | клиент: {_ownerName} | счет: ****{lastFourDigits} | статус: {_status} | баланс: {_balance:F2} {_currency}";
        }

        public override string ToString() => GetAccountInfo();
    }
}