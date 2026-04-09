using System;
using System.Collections.Generic;

namespace BankSystem
{
    public class Client
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public string FullName { get; private set; }
        public int Age { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public bool IsBlocked { get; set; }
        public int FailedLoginAttempts { get; set; }
        public List<string> AccountIds { get; private set; }

        public Client(string fullName, int age, string phone, string email)
        {
            if (age < 18) throw new InvalidOperationError("возраст должен быть не менее 18 лет.");
            FullName = fullName;
            Age = age;
            Phone = phone;
            Email = email;
            Id = _nextId++;
            IsBlocked = false;
            FailedLoginAttempts = 0;
            AccountIds = new List<string>();
        }

        public override string ToString() => $"#{Id}: {FullName}, {Age} лет, тел:{Phone}, заблокирован:{IsBlocked}, счетов:{AccountIds.Count}";
    }
}