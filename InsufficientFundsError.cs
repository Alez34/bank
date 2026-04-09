using System;

namespace BankAccountSystem
{
    public class InsufficientFundsError : Exception
    {
        public InsufficientFundsError() : base("недостаточно средств на счете.") { }
        
        public InsufficientFundsError(string message) : base(message) { }
    }
}