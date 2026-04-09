using System;
using System.Collections.Generic;

namespace BankSystem
{
    public class Bank
    {
        private Dictionary<int, Client> _clients = new();
        private Dictionary<string, dynamic> _accounts = new();

        public Client AddClient(string name, int age, string phone, string email)
        {
            var c = new Client(name, age, phone, email);
            _clients[c.Id] = c;
            Console.WriteLine($"добавлен: {c}");
            return c;
        }

        public void OpenAccount(int clientId, string type, string currency, params object[] extra)
        {
            if (!_clients.ContainsKey(clientId)) throw new InvalidOperationError("клиент не найден");
            var client = _clients[clientId];
            if (client.IsBlocked) throw new SecurityException("клиент заблокирован");

            string accId = Guid.NewGuid().ToString().Substring(0, 8);
            dynamic acc = type.ToLower() switch
            {
                "saving" => new SavingAccount(client.FullName, currency, (decimal)extra[0], (decimal)extra[1], accId),
                "premium" => new PremiumAccount(client.FullName, currency, 0, (decimal)extra[0], (decimal)extra[1], accId),
                "investment" => new InvestmentAccount(client.FullName, currency, accId),
                _ => throw new InvalidOperationError("неизвестный тип")
            };
            _accounts[accId] = acc;
            client.AccountIds.Add(accId);
            Console.WriteLine($"открыт {type} счет {accId} для {client.FullName}");
        }

        public dynamic GetAccount(string id) => _accounts.ContainsKey(id) ? _accounts[id] : throw new InvalidOperationError("счет не найден");

        public void FreezeAccount(string id) { var a = GetAccount(id); a.SetStatus(AccountStatus.Frozen); Console.WriteLine($"счет {id} заморожен"); }
        public void UnfreezeAccount(string id) { var a = GetAccount(id); a.SetStatus(AccountStatus.Active); Console.WriteLine($"счет {id} разморожен"); }
        public void CloseAccount(string id) { var a = GetAccount(id); a.SetStatus(AccountStatus.Closed); Console.WriteLine($"счет {id} закрыт"); }

        public bool AuthenticateClient(int id, string phone)
        {
            var now = DateTime.Now;
            if (now.Hour >= 0 && now.Hour < 5) throw new SecurityException("аутентификация недоступна с 00:00 до 05:00");
            if (!_clients.ContainsKey(id)) throw new InvalidOperationError("клиент не найден");
            var c = _clients[id];
            if (c.IsBlocked) throw new SecurityException("клиент заблокирован");
            if (c.Phone != phone)
            {
                c.FailedLoginAttempts++;
                if (c.FailedLoginAttempts >= 3) { c.IsBlocked = true; throw new SecurityException("3 неудачные попытки - блокировка"); }
                throw new SecurityException($"неверный телефон. попыток: {c.FailedLoginAttempts}/3");
            }
            c.FailedLoginAttempts = 0;
            Console.WriteLine($"аутентификация успешна: {c.FullName}");
            return true;
        }

        public void Deposit(string accId, decimal amount) => GetAccount(accId).Deposit(amount);
        public void Withdraw(string accId, decimal amount)
        {
            if (amount > 100000) Console.WriteLine($"⚠️ ПОДОЗРИТЕЛЬНАЯ ОПЕРАЦИЯ: снятие {amount} со счета {accId}");
            GetAccount(accId).Withdraw(amount);
        }
    }
}