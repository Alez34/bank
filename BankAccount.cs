using System;
using System.Linq;

namespace BankAccountSystem
{
    public class BankAccount : AbstractAccount
    {
        private string _currency;
        
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
                throw new InvalidOperationError($"валюта {currency} не поддерживается. доступны: RUB, USD, EUR, KZT, CNY");
        }

        private void ValidateAmountForWithdraw(decimal amount)
        {
            if (amount > _balance)
                throw new InsufficientFundsError();
        }

        public override void Deposit(decimal amount)
        {
            ValidateAmount(amount);
            ValidateStatus();
            
            _balance += amount;
            Console.WriteLine($"пополнение на {amount} {_currency}. текущий баланс: {_balance} {_currency}");
        }

        public override void Withdraw(decimal amount)
        {
            ValidateAmount(amount);
            ValidateStatus();
            ValidateAmountForWithdraw(amount);
            
            _balance -= amount;
            Console.WriteLine($"снятие {amount} {_currency}. текущий баланс: {_balance} {_currency}");
        }

        public override string GetAccountInfo()
        {
            string lastFourDigits = _accountId.Length >= 4 
                ? _accountId.Substring(_accountId.Length - 4) 
                : _accountId;
            
            return $"тип счета: {AccountType}\n" +
                   $"клиент: {_ownerName}\n" +
                   $"номер счета: ****{lastFourDigits}\n" +
                   $"статус: {_status}\n" +
                   $"баланс: {_balance:F2} {_currency}";
        }

        public override string ToString()
        {
            return GetAccountInfo();
        }
    }
}