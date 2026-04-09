using System;

namespace BankSystem
{
    public class InvalidOperationError : Exception
    {
        public InvalidOperationError(string msg) : base(msg) { }
    }
}