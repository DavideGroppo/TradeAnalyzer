using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using TradeAnalyzer.Objects;

namespace TradeAnalyzer.Helper
{
    public class TradeObjectHelper
    {

        //Sistemare
        //NOW IN TRADE
        //TOT Investiti come somma dei totali vari

        private readonly string[] CashInTypes = new string[] { "TRANSFER_INSTANT_INBOUND", "TRANSFER_INBOUND" };
        private readonly string[] CashOutTypes = new string[] { "TRANSFER_INSTANT_OUTBOUND", "CARD_TRANSACTION", "TAX_OPTIMIZATION" };
        private readonly string[] TradeAssetTypes = new string[] { "STOCK", "BOND", "CRYPTO", "PRIVATE_FUND" };
        private readonly string[] StockInActions = new string[] { "SELL", "DIVIDEND" };
        private readonly string[] BondInActions = new string[] { "INTEREST_PAYMENT" };
        private readonly string[] CryptoInActions = new string[] { "SELL" };
        private readonly string[] PrivateFundInActions = new string[] { };

        private IEnumerable<AssetItem> assetItems = Enumerable.Empty<AssetItem>();

        private TradeCsvLine[] tradeCsvLine;

        public TradeObjectHelper()
        {
           this.tradeCsvLine = CsvImportHelper.LoadFromCsv();
        }

        public double GetTotalInAmount()
        {
            var interestedLines = tradeCsvLine.Where(t => t.Category == Enums.Category.CASH && CashInTypes.Contains(t.Type));
            return interestedLines.Sum(t => t.RealValue);
        }

        public double GetTotalOutAmount()
        {
            var interestedLines = tradeCsvLine.Where(t => t.Category == Enums.Category.CASH && CashOutTypes.Contains(t.Type));
            return interestedLines.Sum(t => t.RealValue);
        }

        public double GetTotalInTradeAmount()
        {
            var interestedLines = tradeCsvLine.Where(t => t.Category == Enums.Category.TRADING && TradeAssetTypes.Contains(t.AssetClass) && t.Type.Contains("BUY"));
            return interestedLines.Sum(t => t.RealValue);
        }

        public double GetCashInterestsIn()
        {
            var interestedLines = tradeCsvLine.Where(t => t.Category == Enums.Category.CASH && t.Type == "INTEREST_PAYMENT" && String.IsNullOrEmpty(t.AssetClass));
            return interestedLines.Sum(t => t.RealValue);
        }

        public void GetAllAssetItems()
        {

            var tradingLines = tradeCsvLine.Where(t => t.Category == Enums.Category.TRADING && TradeAssetTypes.Contains(t.AssetClass));
            var assets = tradingLines.DistinctBy(t => t.Name).Select(t => t.Name);

            foreach (var asset in assets)
            {

                AssetItem item = new AssetItem();
                item.Nome = asset;

                var assetLines = tradingLines.Where(t => t.Name.Equals(asset));
                item.AssetType = assetLines.First().AssetClass;

                var buy = assetLines.Where(t => t.Type.Contains("BUY"));
                var sell = assetLines.Where(t => t.Type.Contains("SELL"));
                var others = tradeCsvLine.Where(t => t.Name == asset && !t.Type.Contains("SELL") && !t.Type.Contains("BUY"));

                switch (assetLines.First().AssetClass)
                {
                    case "STOCK":

                        double actionSell = sell.Any() ? sell.Sum(t => t.Shares) : 0;
                        double dividend = others.Any() ? others.Where(t => t.Type.Equals("DIVIDEND")).Sum(t => t.RealValue) : 0;

                        item.ActionAmount = buy.Sum(t => t.Shares) + actionSell;
                        item.Valore = sell.Sum(t => t.RealValue) + buy.Sum(t => t.RealValue);
                        item.GiaPresi = dividend;

                        if (item.ActionAmount == 0)
                        {
                            item.GiaPresi += item.Valore;
                            item.Valore = 0;
                        }

                        item.Valore = Math.Abs(item.Valore);

                        break;

                    case "BOND":

                        item.ActionAmount = 0;
                        item.GiaPresi = others.Any() ? others.Where(t => t.Type.Equals("INTEREST_PAYMENT")).Sum(t => t.RealValue) : 0;
                        item.Valore = Math.Abs(buy.Sum(t => t.RealValue));

                        break;
                    case "CRYPTO":

                        item.ActionAmount = buy.Sum(t => t.Shares);
                        item.Valore = Math.Abs(buy.Sum(t => t.RealValue));
                        item.GiaPresi = 0;
                        break;

                    case "PRIVATE_FUND":
                        item.GiaPresi = 0;
                        item.ActionAmount = 0;
                        item.Valore = Math.Abs(buy.Sum(t => t.Price * t.Shares));
                        break;
                }

                assetItems = assetItems.Append(item);


            }
        }

        public AssetItem[] GetStocks()
        {
            if (!assetItems.Any())
            {
                GetAllAssetItems();
            }
            return assetItems.Where(t => t.AssetType.Equals("STOCK")).OrderBy(t => t.Valore).Reverse().ToArray();
        }

        public AssetItem[] GetBonds()
        {
            if (!assetItems.Any())
            {
                GetAllAssetItems();
            }
            return assetItems.Where(t => t.AssetType.Equals("BOND")).OrderBy(t => t.Valore).Reverse().ToArray();
        }

        public AssetItem[] GetCryptos()
        {
            if (!assetItems.Any())
            {
                GetAllAssetItems();
            }
            return assetItems.Where(t => t.AssetType.Equals("CRYPTO")).OrderBy(t => t.Valore).Reverse().ToArray();
        }

        public AssetItem[] GetPrivateFunds()
        {
            if (!assetItems.Any())
            {
                GetAllAssetItems();
            }
            return assetItems.Where(t => t.AssetType.Equals("PRIVATE_FUND")).OrderBy(t => t.Valore).Reverse().ToArray();
        }

        public double GetTotalAmout()
        {
            double totalAmount = 0;
            
            

            return totalAmount;
        }

    }
}
