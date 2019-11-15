using ArffTools;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RegressionSimple1
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataFolder = @"D:\softaware\samples-ml\data";

            var wineReviews = new List<WineReview>();

            using (ArffReader arffReader = new ArffReader($"{dataFolder}\\openml-wine_reviews.arff"))
            {
                var header = arffReader.ReadHeader();
                object[][] instances = arffReader.ReadAllInstances();

                for (int row = 0; row < instances.GetLength(0); row++)
                {
                    wineReviews.Add(instances[row].MapTo<WineReview>(header));
                }
            }

            var points = (from r in wineReviews
                         group r by r.Points into g
                         orderby g.Key descending
                         select g).ToList();

            foreach (var p in points)
            {
                Console.WriteLine($"{p.Key}   |   {p.Count()}");
            }

            //Console.ReadKey();

            MLContext mlContext = new MLContext();
            IDataView trainingData = mlContext.Data.LoadFromEnumerable(wineReviews);

            //var preview = trainingData.Preview(50);

            //var pipeline = mlContext.Data.TrainTestSplit()
            // NormalizeMinMax


            //var pipeline = mlContext.Transforms.Concatenate(
            //                "Features",
            //                nameof(WineReview.Country), nameof(WineReview.Price), nameof(WineReview.Province), nameof(WineReview.Variety));
            // throws Schema mismatch for input column 'Price': expected String, got Single ' -> OneHotEncoding

            var trainer = mlContext.Regression.Trainers.Sdca(new Microsoft.ML.Trainers.SdcaRegressionTrainer.Options()
            {
                MaximumNumberOfIterations = 100   
            });

            var pipeline = mlContext.Transforms.CopyColumns("Label", nameof(WineReview.Points))
                                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Country_Encoded", nameof(WineReview.Country)))
                                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Province_Encoded", nameof(WineReview.Province)))
                                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Variety_Encoded", nameof(WineReview.Variety)))
                                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Winery_Encoded", nameof(WineReview.Winery)))
                                .Append(mlContext.Transforms.Concatenate(
                                    "Features",
                                    "Country_Encoded", nameof(WineReview.Price), "Province_Encoded", "Variety_Encoded", "Winery_Encoded"));

            //var preview = pipeline.Preview(trainingData, 50);

            var model = pipeline.Append(trainer).Fit(trainingData);

            //mlContext.Model.CreatePredictionEngine<WineReview, WinePrediction>

            IDataView testData = mlContext.Data.LoadFromEnumerable(wineReviews);

            var predictions = model.Transform(trainingData);

            var metrics = mlContext.Regression.Evaluate(predictions);
            Console.WriteLine($"RSquared Score:          {metrics.RSquared:0.##}");
            Console.WriteLine($"Root Mean Squared Error: {metrics.RootMeanSquaredError:#.##}");
        }
    }
}
