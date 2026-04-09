using System;

namespace BankAccountSystem
{
    public abstract class AbstractAccount
    {
        protected string _accountId;
        protected string _ownerName;
        protected decimal _balance;
        protected AccountStatus _status;

        public string AccountId => _accountId;
        public string OwnerName => _ownerName;
        public decimal Balance => _balance;
        public AccountStatus Status => _status;

        protected AbstractAccount(string ownerName, string? accountId = null)
        {
            _ownerName = ownerName;
            _balance = 0;
            _status = AccountStatus.Active;
            
            if (string.IsNullOrEmpty(accountId))
                _accountId = GenerateShortId();
            else
                _accountId = accountId;
        }

        private string GenerateShortId()
        {
            Random rand = new Random();
            return rand.Next(10000000, 99999999).ToString();
        }

        public abstract void Deposit(decimal amount);
        public abstract void Withdraw(decimal amount);
        public abstract string GetAccountInfo();

        protected void ValidateStatus()
        {
            if (_status == AccountStatus.Frozen)
                throw new AccountFrozenError();
            if (_status == AccountStatus.Closed)
                throw new AccountClosedError();
        }

        protected void ValidateAmount(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationError("сумма должна быть больше нуля.");
        }

        public void SetStatus(AccountStatus newStatus)
        {
            _status = newStatus;
        }
    }

    public enum AccountStatus
    {
        Active,
        Frozen,
        Closed
    }
}