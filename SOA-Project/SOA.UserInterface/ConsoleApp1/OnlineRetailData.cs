using Microsoft.ML.Data;
using System;

namespace ConsoleApp1
{
    public class OnlineRetailData
    {
        [LoadColumn(0)]
        public string InvoiceNo;

        [LoadColumn(1)]
        public string StockCode;

        [LoadColumn(2)]
        public int Quantity;

        [LoadColumn(3)]
        public DateTime InvoiceDate;

        [LoadColumn(4)]
        public double UnitPrice;

        [LoadColumn(5)]
        public int CustomerID;

        [LoadColumn(6)]
        public string Country;
    }

    public class OnlineRetailTransformedData
    {
        public float InvoiceNoTransformed;
        public float StockCodeTransformed;
        public float QuantityTransformed;
        public float InvoiceDateTransformed;
        public float UnitPriceTransformed;
        public float CustomerIDTransformed;
        public float CountryTransformed;
    }

    public class ClusterPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId;

        [ColumnName("Score")]
        public float[] Distances;
    }
}
