using System;
using System.Collections.Generic;
using System.Text;

namespace TradeAnalyzer.Objects
{
    public static class Enums
    {

        public enum Category
        {
            [System.ComponentModel.Description("Soldi")]
            CASH,
            [System.ComponentModel.Description("Investimenti")]
            TRADING,
            CORPORATE_ACTION
        }

        public enum Currency
        {
            [System.ComponentModel.Description("Euro")]
            EUR,
            USD,
            CAD,
            CHF,
            NOTHING

        }

    }
}
