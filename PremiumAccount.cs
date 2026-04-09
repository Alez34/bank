using System;

namespace BankSystem
{
    public class PremiumAccount : BankAccount
    {
        private decimal _overdraftLimit;
        private decimal _fixedCommission;

        public PremiumAccount(string ownerName, string currency, decimal increasedLimit, decimal overdraftLimit, decimal fixedCommission, string? accountId = null) 
            : base(ownerName, currency, accountId)
        {
            _overdraftLimit = overdraftLimit;
            _fixedCommission = fixedCommission;
        }

        public override void Withdraw(decimal amount)
        {
            ValidateAmount(amount);
            ValidateStatus();
            if (_balance - amount < -_overdraftLimit)
                throw new InsufficientFundsError($"превышен лимит овердрафта. доступно: {_balance + _overdraftLimit:F2} {_currency}");
            
            _balance -= amount;
            if (_balance < 0)
            {
                _balance -= _fixedCommission;
                Console.WriteLine($"комиссия овердрафта: {_fixedCommission} {_currency}");
            }
            Console.WriteLine($"снятие {amount} {_currency}. баланс: {_balance:F2} {_currency}");
        }

        public override string GetAccountInfo()
        {
            string lastFourDigits = _accountId.Length >= 4 ? _accountId.Substring(_accountId.Length - 4) : _accountId;
            return $"тип: PremiumAccount | клиент: {_ownerName} | счет: ****{lastFourDigits} | статус: {_status} | баланс: {_balance:F2} {_currency} | овердрафт: {_overdraftLimit} | комиссия: {_fixedCommission}";
        }
    }
}