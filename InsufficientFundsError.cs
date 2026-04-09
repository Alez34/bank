using System;

namespace BankSystem
{
    public class InsufficientFundsError : Exception
    {
        public InsufficientFundsError(string msg) : base(msg) { }
    }
}   