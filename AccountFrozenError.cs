using System;

namespace BankSystem
{
    public class AccountFrozenError : Exception
    {
        public AccountFrozenError() : base("счет заморожен") { }
    }
}