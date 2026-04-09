using System;
using System.Collections.Generic;

namespace BankSystem
{
    public class InvestmentAccount : BankAccount
    {
        private Dictionary<string, decimal> _portfolio = new();
        private Dictionary<string, decimal> _returns = new();

        public InvestmentAccount(string ownerName, string currency, string? accountId = null) : base(ownerName, currency, accountId) { }

        public void AddAsset(string name, decimal amount, decimal annualReturn)
        {
            if (amount <= 0) throw new InvalidOperationError("сумма > 0");
            if (_portfolio.ContainsKey(name)) _portfolio[name] += amount;
            else _portfolio.Add(name, amount);
            if (_returns.ContainsKey(name)) _returns[name] = annualReturn;
            else _returns.Add(name, annualReturn);
            _balance += amount;
        }

        public void RemoveAsset(string name, decimal amount)
        {
            if (!_portfolio.ContainsKey(name)) throw new InvalidOperationError($"актив {name} не найден");
            if (_portfolio[name] < amount) throw new InvalidOperationError($"недостаточно {name}");
            _portfolio[name] -= amount;
            _balance -= amount;
            if (_portfolio[name] == 0) _portfolio.Remove(name);
        }

        public decimal ProjectYearlyGrowth()
        {
            decimal total = 0;
            foreach (var a in _portfolio)
                if (_returns.ContainsKey(a.Key))
                    total += a.Value * (_returns[a.Key] / 100);
            return total;
        }

        public override string GetAccountInfo()
        {
            string lastFourDigits = _accountId.Length >= 4 ? _accountId.Substring(_accountId.Length - 4) : _accountId;
            return $"тип: InvestmentAccount | клиент: {_ownerName} | счет: ****{lastFourDigits} | статус: {_status} | баланс: {_balance:F2} {_currency} | доходность: {ProjectYearlyGrowth():F2}";
        }
    }
}