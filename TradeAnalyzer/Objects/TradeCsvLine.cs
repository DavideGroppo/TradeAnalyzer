using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TradeAnalyzer.Objects;
using static TradeAnalyzer.Objects.Enums;

namespace TradeAnalyzer.Objects
{
    public class TradeCsvLine
    {

        private DateTime operationDateTime;
        private DateOnly operationDate;
        private string accountType;
        private Category category;
        private string type;
        private string assetClass;
        private string name;
        private string symbol;
        private double shares;
        private double price;
        private double amount;
        private double fee;
        private double tax;
        private Currency currency;
        private double originalAmount;
        private Currency originalCurrency;
        private double fxRate;
        private string description;
        private string transactionId;
        private string counterPartyName;
        private string counterPartyIBAN;
        private string paymentReference;
        private float mccCode;

        // Public read-only properties exposing the private fields
        public DateTime OperationDateTime => operationDateTime;
        public DateOnly OperationDate => operationDate;
        public string AccountType => accountType;
        public Category Category => category;
        public string Type => type;
        public string AssetClass => assetClass;
        public string Name => name;
        public string Symbol => symbol;
        public double Shares => shares;
        public double Price => price;
        public double Amount => amount;
        public double Fee => fee;
        public double Tax => tax;
        public Currency Currency => currency;
        public double OriginalAmount => originalAmount;
        public Currency OriginalCurrency => originalCurrency;
        public double FxRate => fxRate;
        public string Description => description;
        public string TransactionId => transactionId;
        public string CounterPartyName => counterPartyName;
        public string CounterPartyIBAN => counterPartyIBAN;
        public string PaymentReference => paymentReference;
        public float MccCode => mccCode;
        public float RealValue => (float)(Amount + Fee + Tax);

        // Constructor that sets all private fields
        public TradeCsvLine(DateTime operationDateTime,
                            DateOnly operationDate,
                            string accountType,
                            Category category,
                            string type,
                            string assetClass,
                            string name,
                            string symbol,
                            double shares,
                            double price,
                            double amount,
                            double fee,
                            double tax,
                            Currency currency,
                            double originalAmount,
                            Currency originalCurrency,
                            double fxRate,
                            string description,
                            string transactionId,
                            string counterPartyName,
                            string counterPartyIBAN,
                            string paymentReference,
                            float mccCode)
        {
            this.operationDateTime = operationDateTime;
            this.operationDate = operationDate;
            this.accountType = accountType;
            this.category = category;
            this.type = type;
            this.assetClass = assetClass;
            this.name = name;
            this.symbol = symbol;
            this.shares = shares;
            this.price = price;
            this.amount = amount;
            this.fee = fee;
            this.tax = tax;
            this.currency = currency;
            this.originalAmount = originalAmount;
            this.originalCurrency = originalCurrency;
            this.fxRate = fxRate;
            this.description = description;
            this.transactionId = transactionId;
            this.counterPartyName = counterPartyName;
            this.counterPartyIBAN = counterPartyIBAN;
            this.paymentReference = paymentReference;
            this.mccCode = mccCode;
        }

    }
}
