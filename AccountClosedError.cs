using System;

namespace BankAccountSystem
{
    public class AccountClosedError : Exception
    {
        public AccountClosedError() : base("счет закрыт. операции недоступны.") { }
        
        public AccountClosedError(string message) : base(message) { }
    }
}