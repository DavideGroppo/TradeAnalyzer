using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TradeAnalyzer.Objects
{
    public class AssetItem : INotifyPropertyChanged
    {
        private string _nome = string.Empty;
        private double _valore;
        private double _giaPresi;
        private string _assetType;
        private double _actionAmount;

        public string Nome
        {
            get => _nome;
            set { _nome = value; OnPropertyChanged(); }
        }

        public double Valore
        {
            get => _valore;
            set { _valore = value; OnPropertyChanged(); }
        }

        public double GiaPresi
        {
            get => _giaPresi;
            set { _giaPresi = value; OnPropertyChanged(); }
        }

        public string AssetType
        {
            get => _assetType;
            set { _assetType = value; OnPropertyChanged(); }
        }

        public double ActionAmount
        {
            get => _actionAmount;
            set { _actionAmount = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
