using System;

namespace BankAccountSystem
{
    public class AccountFrozenError : Exception
    {
        public AccountFrozenError() : base("счет заморожен. операции недоступны.") { }
        public AccountFrozenError(string message) : base(message) { }
    }
}