using System;

namespace BankAccountSystem
{
    public class InvalidOperationError : Exception
    {
        public InvalidOperationError() : base("недопустимая операция.") { }
        
        public InvalidOperationError(string message) : base(message) { }
    }
}