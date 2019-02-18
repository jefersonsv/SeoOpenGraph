using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SeoOpenGraph.ObjectTypes
{
    public class Currency : IObjectType
    {
        public double? Amount { get; set; }

        [Description("currency")]
        public string CurrencyText { get; set; }
    }
}
