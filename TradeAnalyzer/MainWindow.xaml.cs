using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;
using System.Windows;
using TradeAnalyzer.Helper;
using TradeAnalyzer.Objects;

namespace TradeAnalyzer
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        

        private double _totIn;
        private double _totOut;
        private double _nowInTrade;
        private double _cashInterestsIn;
        private double _totInvestiti;

        public double TotIn
        {
            get => _totIn;
            set { _totIn = value; OnPropertyChanged(); }
        }

        public double TotOut
        {
            get => _totOut;
            set { _totOut = value; OnPropertyChanged(); }
        }

        public double NowInTrade
        {
            get => _nowInTrade;
            set { _nowInTrade = value; OnPropertyChanged(); }
        }

        public double CashInterestsIn
        {
            get => _cashInterestsIn;
            set { _cashInterestsIn = value; OnPropertyChanged(); }
        }

        public double TotInvestiti
        {
            get => _totInvestiti;
            set { _totInvestiti = value; OnPropertyChanged(); }
        }

        // Collezioni
        public ObservableCollection<AssetItem> Obbligazioni { get; set; } = new();
        public ObservableCollection<AssetItem> Azioni { get; set; } = new();
        public ObservableCollection<AssetItem> Crypto { get; set; } = new();
        public ObservableCollection<AssetItem> MercatiPrivati { get; set; } = new();

        // Proprietà cumulative calcolate con LINQ
        public double TotaleObbligazioni => Obbligazioni.Sum(x => x.Valore);
        public double TotalePresiObbligazioni => Obbligazioni.Sum(x => x.GiaPresi);

        public double TotaleAzioni => Azioni.Sum(x => x.Valore);
        public double TotalePresiAzioni => Azioni.Sum(x => x.GiaPresi);

        public double TotaleCrypto => Crypto.Sum(x => x.Valore);
        public double TotalePresiCrypto => Crypto.Sum(x => x.GiaPresi);

        public double TotaleMercatiPrivati => MercatiPrivati.Sum(x => x.Valore);
        public double TotalePresiMercatiPrivati => MercatiPrivati.Sum(x => x.GiaPresi);



        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            TradeObjectHelper tradeObjectHelper = new TradeObjectHelper();
            TotIn = Math.Abs(tradeObjectHelper.GetTotalInAmount());
            TotOut = Math.Abs(tradeObjectHelper.GetTotalOutAmount());
            CashInterestsIn = Math.Abs(tradeObjectHelper.GetCashInterestsIn());
            NowInTrade = TotIn - TotOut - TotInvestiti;

            Obbligazioni = new ObservableCollection<AssetItem>(tradeObjectHelper.GetBonds());
            Azioni = new ObservableCollection<AssetItem>(tradeObjectHelper.GetStocks());
            Crypto = new ObservableCollection<AssetItem>(tradeObjectHelper.GetCryptos());
            MercatiPrivati = new ObservableCollection<AssetItem>(tradeObjectHelper.GetPrivateFunds());

            TotInvestiti = TotaleAzioni + TotaleCrypto + TotaleMercatiPrivati + TotaleObbligazioni;
            double totGiaPresi = TotalePresiAzioni + TotalePresiCrypto + TotalePresiMercatiPrivati + TotalePresiObbligazioni;
            NowInTrade = TotIn - TotOut - TotInvestiti + CashInterestsIn + totGiaPresi;  

            //// Populate collections from helper
            //var allAssets = _helper.GetAssetItems();
            //foreach (var asset in allAssets)
            //{
            //    var cls = (asset.AssetClass ?? string.Empty).ToUpperInvariant();
            //    switch (cls)
            //    {
            //        case "BOND":
            //            Obbligazioni.Add(asset);
            //            break;
            //        case "STOCK":
            //            Azioni.Add(asset);
            //            break;
            //        case "CRYPTO":
            //            Crypto.Add(asset);
            //            break;
            //        case "PRIVATE_FUND":
            //            MercatiPrivati.Add(asset);
            //            break;
            //        default:
            //            // fallback: put unknown types into Azioni
            //            Azioni.Add(asset);
            //            break;
            //    }
            //}

            // update computed properties that read from helper

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}