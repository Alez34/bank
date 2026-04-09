using System;

namespace BankSystem
{
    public class AccountClosedError : Exception
    {
        public AccountClosedError() : base("счет закрыт") { }
    }
}