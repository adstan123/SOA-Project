using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "OnlineRetail2.csv");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "OnlineRetailTrainedModel.zip");

        static void Main(string[] args)
        {
            var mlContext = new MLContext(seed: 0);

            /*
            IDataView dataView = mlContext.Data.LoadFromTextFile<OnlineRetailData>(_dataPath, hasHeader: false, separatorChar: ',');

            string featuresColumnName = "Features";

           

            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "StockCodeEncoded", inputColumnName: "StockCode")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "CountryEncoded", inputColumnName: "Country"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "InvoiceNoEncoded", inputColumnName: "InvoiceNo"))
                                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "UnitPriceEncoded", inputColumnName: "UnitPrice"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "QuantityEncoded", inputColumnName: "Quantity"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "CustomerIDEncoded", inputColumnName: "CustomerID"))
                                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "InvoiceDateEncoded", inputColumnName: "InvoiceDate"))

                .Append(mlContext.Transforms.Concatenate(featuresColumnName, "StockCodeEncoded", "CountryEncoded", "InvoiceNoEncoded", "UnitPriceEncoded", "QuantityEncoded","CustomerIDEncoded", "InvoiceDateEncoded"))
                .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 5));

            var model = pipeline.Fit(dataView);


            using (var fileStream = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                mlContext.Model.Save(model, dataView.Schema, fileStream);
            }

    */
            ITransformer trainedModel = mlContext.Model.Load(_modelPath, out _);
            var predictor = mlContext.Model.CreatePredictionEngine<OnlineRetailData, ClusterPrediction>(trainedModel);

            var prediction = predictor.Predict(TestIrisData.Setosa);
            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");

 
        }


    }
}
