using System;
using System.Collections.Generic;

namespace BankAccountSystem
{
    public class InvestmentAccount : BankAccount
    {
        private Dictionary<string, decimal> _portfolio; // актив -> сумма
        private Dictionary<string, decimal> _annualReturns; // актив -> годовая доходность %

        public Dictionary<string, decimal> Portfolio => _portfolio;

        public InvestmentAccount(string ownerName, string currency, string? accountId = null) 
            : base(ownerName, currency, accountId)
        {
            _portfolio = new Dictionary<string, decimal>();
            _annualReturns = new Dictionary<string, decimal>();
        }

        public void AddAsset(string assetName, decimal amount, decimal annualReturnPercent)
        {
            if (amount <= 0)
                throw new InvalidOperationError("сумма актива должна быть больше нуля.");
            
            if (_portfolio.ContainsKey(assetName))
                _portfolio[assetName] += amount;
            else
                _portfolio.Add(assetName, amount);
            
            if (_annualReturns.ContainsKey(assetName))
                _annualReturns[assetName] = annualReturnPercent;
            else
                _annualReturns.Add(assetName, annualReturnPercent);
            
            // пополняем баланс счета
            _balance += amount;
            Console.WriteLine($"добавлен актив {assetName} на сумму {amount} {_currency} (доходность {annualReturnPercent}% годовых)");
        }

        public void RemoveAsset(string assetName, decimal amount)
        {
            if (!_portfolio.ContainsKey(assetName))
                throw new InvalidOperationError($"актив {assetName} не найден в портфеле.");
            
            if (_portfolio[assetName] < amount)
                throw new InvalidOperationError($"недостаточно активов {assetName}. доступно: {_portfolio[assetName]}");
            
            _portfolio[assetName] -= amount;
            _balance -= amount;
            
            if (_portfolio[assetName] == 0)
                _portfolio.Remove(assetName);
            
            Console.WriteLine($"продажа актива {assetName} на сумму {amount} {_currency}");
        }

        public decimal ProjectYearlyGrowth()
        {
            decimal totalGrowth = 0;
            
            foreach (var asset in _portfolio)
            {
                string assetName = asset.Key;
                decimal assetAmount = asset.Value;
                decimal returnRate = _annualReturns.ContainsKey(assetName) ? _annualReturns[assetName] : 0;
                
                totalGrowth += assetAmount * (returnRate / 100);
            }
            
            return totalGrowth;
        }

        public override void Withdraw(decimal amount)
        {
            ValidateAmount(amount);
            ValidateStatus();
            
            if (amount > _balance)
                throw new InsufficientFundsError();
            
            _balance -= amount;
            Console.WriteLine($"снятие {amount} {_currency} с инвестиционного счета. текущий баланс: {_balance:F2} {_currency}");
        }

        public override string GetAccountInfo()
        {
            string lastFourDigits = _accountId.Length >= 4 
                ? _accountId.Substring(_accountId.Length - 4) 
                : _accountId;
            
            string portfolioInfo = "";
            foreach (var asset in _portfolio)
            {
                decimal returnRate = _annualReturns.ContainsKey(asset.Key) ? _annualReturns[asset.Key] : 0;
                portfolioInfo += $"\n   - {asset.Key}: {asset.Value:F2} {_currency} (доходность {returnRate}%)";
            }
            
            if (string.IsNullOrEmpty(portfolioInfo))
                portfolioInfo = "\n   (портфель пуст)";
            
            return $"тип счета: InvestmentAccount\n" +
                   $"клиент: {_ownerName}\n" +
                   $"номер счета: ****{lastFourDigits}\n" +
                   $"статус: {_status}\n" +
                   $"баланс: {_balance:F2} {_currency}\n" +
                   $"портфель активов:{portfolioInfo}\n" +
                   $"прогнозируемая годовая доходность: {ProjectYearlyGrowth():F2} {_currency}";
        }

        public override string ToString()
        {
            return GetAccountInfo();
        }
    }
}