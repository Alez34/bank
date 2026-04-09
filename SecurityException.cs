using System;

namespace BankSystem
{
    public class SecurityException : Exception
    {
        public SecurityException(string msg) : base(msg) { }
    }
}