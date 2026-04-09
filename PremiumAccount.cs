using System;

namespace BankAccountSystem
{
    public class PremiumAccount : BankAccount
    {
        private decimal _increasedLimit;
        private decimal _overdraftLimit;
        private decimal _fixedCommission;

        public decimal IncreasedLimit => _increasedLimit;
        public decimal OverdraftLimit => _overdraftLimit;
        public decimal FixedCommission => _fixedCommission;

        public PremiumAccount(string ownerName, string currency, decimal increasedLimit, decimal overdraftLimit, decimal fixedCommission, string? accountId = null) 
            : base(ownerName, currency, accountId)
        {
            _increasedLimit = increasedLimit;
            _overdraftLimit = overdraftLimit;
            _fixedCommission = fixedCommission;
        }

        public override void Withdraw(decimal amount)
        {
            ValidateAmount(amount);
            ValidateStatus();
            
            // премиум счет может уходить в минус до лимита овердрафта
            if (_balance - amount < -_overdraftLimit)
                throw new InsufficientFundsError($"превышен лимит овердрафта. доступно: {_balance + _overdraftLimit:F2} {_currency}");
            
            _balance -= amount;
            
            // если ушли в минус, снимаем комиссию
            if (_balance < 0)
            {
                _balance -= _fixedCommission;
                Console.WriteLine($"комиссия за овердрафт: {_fixedCommission} {_currency}");
            }
            
            Console.WriteLine($"снятие {amount} {_currency}. текущий баланс: {_balance:F2} {_currency}");
        }

        public override void Deposit(decimal amount)
        {
            ValidateAmount(amount);
            ValidateStatus();
            
            _balance += amount;
            
            // проверка на превышение лимита (если нужно)
            if (_balance > _increasedLimit)
            {
                Console.WriteLine($"внимание! превышен увеличенный лимит: {_balance:F2} / {_increasedLimit} {_currency}");
            }
            
            Console.WriteLine($"пополнение на {amount} {_currency}. текущий баланс: {_balance:F2} {_currency}");
        }

        public override string GetAccountInfo()
        {
            string lastFourDigits = _accountId.Length >= 4 
                ? _accountId.Substring(_accountId.Length - 4) 
                : _accountId;
            
            return $"тип счета: PremiumAccount\n" +
                   $"клиент: {_ownerName}\n" +
                   $"номер счета: ****{lastFourDigits}\n" +
                   $"статус: {_status}\n" +
                   $"баланс: {_balance:F2} {_currency}\n" +
                   $"увеличенный лимит: {_increasedLimit} {_currency}\n" +
                   $"лимит овердрафта: {_overdraftLimit} {_currency}\n" +
                   $"фиксированная комиссия: {_fixedCommission} {_currency}";
        }

        public override string ToString()
        {
            return GetAccountInfo();
        }
    }
}