using System.Data;
using System.Globalization;
using System.IO;
using TradeAnalyzer.Objects;
using static TradeAnalyzer.Objects.Enums;

namespace TradeAnalyzer.Helper
{
    public class CsvImportHelper
    {
        public static TradeCsvLine[] LoadFromCsv()
        {
            var culture = CultureInfo.InvariantCulture;

            return File.ReadLines("C:\\Users\\gropp\\Documents\\TradeRepublic\\Esportazione operazioni.csv")
                .Skip(1) // Salta la riga dell'intestazione (header)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var colonne = line.Split(',').Select(c => c.Trim('"',' ')).ToArray();

                    return new TradeCsvLine(
                        operationDateTime: DateTimeOffset.Parse(colonne[0], culture).LocalDateTime,
                        operationDate: DateOnly.ParseExact(colonne[1], "yyyy-MM-dd", culture),
                        accountType: colonne[2],
                        category: Enum.Parse<Category>(colonne[3]),
                        type: colonne[4],
                        assetClass: colonne[5],
                        name: colonne[6],
                        symbol: colonne[7],
                        shares: String.IsNullOrWhiteSpace(colonne[8]) ? 0 : double.Parse(colonne[8], culture),
                        price: String.IsNullOrWhiteSpace(colonne[9]) ? 0 : double.Parse(colonne[9], culture),
                        amount: String.IsNullOrWhiteSpace(colonne[10]) ? 0 : double.Parse(colonne[10], culture),
                        fee: String.IsNullOrWhiteSpace(colonne[11]) ? 0 : double.Parse(colonne[11], culture),
                        tax: String.IsNullOrWhiteSpace(colonne[12]) ? 0 : double.Parse(colonne[12], culture),
                        currency: String.IsNullOrWhiteSpace(colonne[13]) ? Currency.NOTHING : Enum.Parse<Currency>(colonne[13]),
                        originalAmount: String.IsNullOrWhiteSpace(colonne[14]) ? 0 : double.Parse(colonne[14], culture),
                        originalCurrency: String.IsNullOrWhiteSpace(colonne[15]) ? Currency.NOTHING : Enum.Parse<Currency>(colonne[15]),
                        fxRate: String.IsNullOrWhiteSpace(colonne[16]) ? 0 : double.Parse(colonne[16], culture),
                        description: colonne[17],
                        transactionId: colonne[18],
                        counterPartyName: colonne[19],
                        counterPartyIBAN: colonne[20],
                        paymentReference: colonne[21],
                        mccCode: String.IsNullOrWhiteSpace(colonne[22]) ? 0 : float.Parse(colonne[22], culture)
                    );
                })
                .ToArray();
        }
    }
}
