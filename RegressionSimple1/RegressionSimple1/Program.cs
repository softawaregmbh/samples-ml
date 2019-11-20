using ArffTools;
using Microsoft.ML;
using Microsoft.ML.Data;
using PLplot;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RegressionSimple1
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataFolder = @"D:\softaware\samples-ml\data";

            CreateNewModel("HouseSaleModel.zip");
            //Predict("HouseSaleModel.zip", new HouseSale()
            //{
            //    Bedrooms = 4,
            //    Bathrooms = 1,
            //    Sqft_Living = 1000,
            //    Sqft_Lot = 7134,
            //    View = 0,
            //    Waterfront = 0,
            //    Yr_Built = 1943,
            //    Floors = 1,
            //    Sqft_Above = 1000,
            //    ZipCode = 98178,
            //    Condition = 3,
            //    Grade = 6
            //});

            //Predict("HouseSaleModel.zip", new HouseSale()
            //{
            //    Bedrooms = 4,
            //    Bathrooms = 3,
            //    Sqft_Living = 1960,
            //    Sqft_Lot = 5000,
            //    View = 0,
            //    Waterfront = 0,
            //    Yr_Built = 1965,
            //    Floors = 1,
            //    Sqft_Above = 1050,
            //    ZipCode = 98178,
            //    Condition = 5,
            //    Grade = 7
            //});


            //Predict("HouseSaleModel.zip", new HouseSale()
            //{
            //    Bedrooms = 3,
            //    Bathrooms = 2.5f,
            //    Sqft_Living = 2340,
            //    Sqft_Lot = 10005,
            //    View = 0,
            //    Waterfront = 0,
            //    Yr_Built = 1978,
            //    Floors = 1,
            //    Sqft_Above = 1460,
            //    ZipCode = 98058,
            //    Condition = 4,
            //    Grade = 8
            //});

            PlotRegressionChart("HouseSaleModel.zip", dataFolder, new MLContext(), 1000);

            //var preview = trainingData.Preview(50);

            //var pipeline = mlContext.Data.TrainTestSplit()
            // NormalizeMinMax


            //var pipeline = mlContext.Transforms.Concatenate(
            //                "Features",
            //                nameof(WineReview.Country), nameof(WineReview.Price), nameof(WineReview.Province), nameof(WineReview.Variety));
            // throws Schema mismatch for input column 'Price': expected String, got Single ' -> OneHotEncoding

            //var trainer = mlContext.Regression.Trainers.Sdca(new Microsoft.ML.Trainers.SdcaRegressionTrainer.Options()
            //{
            //    MaximumNumberOfIterations = 100   
            //});

            //var pipeline = mlContext.Transforms.CopyColumns("Label", nameof(WineReview.Points))
            //                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("Country_Encoded", nameof(WineReview.Country)))
            //                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("Province_Encoded", nameof(WineReview.Province)))
            //                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("Variety_Encoded", nameof(WineReview.Variety)))
            //                    .Append(mlContext.Transforms.Categorical.OneHotEncoding("Winery_Encoded", nameof(WineReview.Winery)))
            //                    .Append(mlContext.Transforms.Concatenate(
            //                        "Features",
            //                        "Country_Encoded", nameof(WineReview.Price), "Province_Encoded", "Variety_Encoded", "Winery_Encoded"));

            //var preview = pipeline.Preview(trainingData, 50);

            //var model = pipeline.Append(trainer).Fit(trainingData);

            //mlContext.Model.CreatePredictionEngine<WineReview, WinePrediction>

            //IDataView testData = mlContext.Data.LoadFromEnumerable(houseSales);

            //var predictions = model.Transform(trainingData);

            //var metrics = mlContext.Regression.Evaluate(predictions);
            //Console.WriteLine($"RSquared Score:          {metrics.RSquared:0.##}");
            //Console.WriteLine($"Root Mean Squared Error: {metrics.RootMeanSquaredError:#.##}");
        }

        private static void Predict(string modelFileName, HouseSale houseSale)
        {
            Console.WriteLine(houseSale);

            MLContext mlContext = new MLContext();
            var trainedModel = mlContext.Model.Load(modelFileName, out var modelInputSchema);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<HouseSale, HouseSalePrediction>(trainedModel);
            
            var result = predictionEngine.Predict(houseSale);

            Console.WriteLine($"I guess the price is around {result.Score} $.");
            Console.WriteLine();
        }

        private static void CreateNewModel(string fileName)
        {
            string dataFolder = @"D:\softaware\samples-ml\data";
            List<HouseSale> houseSales = LoadTestData(dataFolder);

            MLContext mlContext = new MLContext();
            IDataView trainingData = mlContext.Data.LoadFromEnumerable(houseSales);

            //var pipeline =
            //    mlContext.Transforms.NormalizeSupervisedBinning("Sqft_Living_Normalized", nameof(HouseSale.Sqft_Living), nameof(HouseSale.Price), maximumBinCount: 20)
            //    .Append(mlContext.Transforms.NormalizeSupervisedBinning("Sqft_Lot_Normalized", nameof(HouseSale.Sqft_Lot), nameof(HouseSale.Price), maximumBinCount: 20))
            //    .Append(mlContext.Transforms.NormalizeSupervisedBinning("Sqft_Above_Normalized", nameof(HouseSale.Sqft_Above), nameof(HouseSale.Price), maximumBinCount: 20))
            //    .Append(mlContext.Transforms.NormalizeMeanVariance("Bathrooms_Normalized", nameof(HouseSale.Bathrooms)))
            //    .Append(mlContext.Transforms.NormalizeMeanVariance("Bedrooms_Normalized", nameof(HouseSale.Bedrooms)))
            //    .Append(mlContext.Transforms.NormalizeMeanVariance("Yr_Built_Normalized", nameof(HouseSale.Yr_Built)))
            //    .Append(mlContext.Transforms.Categorical.OneHotEncoding("Floors_Encoded", nameof(HouseSale.Floors)))
            //    .Append(mlContext.Transforms.Categorical.OneHotEncoding("View_Encoded", nameof(HouseSale.View)))
            //    .Append(mlContext.Transforms.Categorical.OneHotEncoding("Waterfront_Encoded", nameof(HouseSale.Waterfront)))
            //    .Append(
            //        mlContext.Transforms.Concatenate(
            //        "Features",
            //        "Bathrooms_Normalized", "Bedrooms_Normalized", "Floors_Encoded",
            //        "Sqft_Living_Normalized", "Sqft_Lot_Normalized", "Sqft_Above_Normalized",
            //        "View_Encoded", "Waterfront_Encoded", "Yr_Built_Normalized"));

            //var grouped = (from h in houseSales
            //               group h by Math.Round(h.Sqft_Living/50f)*50 into g
            //               orderby g.Key
            //               select g).ToList();

            //foreach (var y in grouped)
            //{
            //    Console.WriteLine($"{y.Key}   |  {y.Count()}");
            //}

            //Console.ReadKey();

            IEstimator<ITransformer> pipeline =
                mlContext.Transforms.NormalizeBinning("Sqft_Living_Normalized", nameof(HouseSale.Sqft_Living))
                .Append(mlContext.Transforms.NormalizeBinning("Sqft_Lot_Normalized", nameof(HouseSale.Sqft_Lot)))
                .Append(mlContext.Transforms.NormalizeBinning("Sqft_Above_Normalized", nameof(HouseSale.Sqft_Above)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("View_Normalized", nameof(HouseSale.View)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Waterfront_Normalized", nameof(HouseSale.Waterfront)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Yr_Built_Normalized", nameof(HouseSale.Yr_Built)))
                .Append(mlContext.Transforms.NormalizeMinMax("Bedrooms_Normalized", nameof(HouseSale.Bedrooms)))
                .Append(mlContext.Transforms.NormalizeMinMax("Bathrooms_Normalized", nameof(HouseSale.Bathrooms)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("ZipCode_Normalized", nameof(HouseSale.ZipCode)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Floors_Normalized", nameof(HouseSale.Floors)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Grade_Normalized", nameof(HouseSale.Grade)))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("Condition_Normalized", nameof(HouseSale.Condition)))
                .Append(
                    mlContext.Transforms.Concatenate(
                        "Features",
                            "Sqft_Living_Normalized", 
                            "Sqft_Lot_Normalized",
                            "View_Normalized",
                            "Waterfront_Normalized",
                            "Yr_Built_Normalized",
                            "Sqft_Above_Normalized",
                            "Bedrooms_Normalized",
                            "Bathrooms_Normalized",
                            "ZipCode_Normalized",
                            "Floors_Normalized",
                            "Grade_Normalized",
                            "Condition_Normalized"
                            ));


            // "Sqft_Living_Normalized", "Sqft_Lot_Normalized", "Sqft_Above_Normalized",

            var transformationModel = pipeline.Fit(trainingData);
            var transformedData = transformationModel.Transform(trainingData);

            var algorithm = mlContext.Regression.Trainers.LbfgsPoissonRegression(nameof(HouseSale.Price));

            var regressionModel = algorithm.Fit(transformedData);

            var completeModel = pipeline.Append(algorithm).Fit(trainingData);
            var predictions = completeModel.Transform(trainingData);

            var metrics = mlContext.Regression.Evaluate(predictions, nameof(HouseSale.Price));
            Console.WriteLine($"RSquared Score:          {metrics.RSquared:0.##}");
            Console.WriteLine($"Root Mean Squared Error: {metrics.RootMeanSquaredError:#.##}");

            mlContext.Model.Save(completeModel, trainingData.Schema, fileName);

            var featureImportance =
                mlContext.Regression.PermutationFeatureImportance(
                    regressionModel, transformedData, labelColumnName: nameof(HouseSale.Price), permutationCount: 3);

            var featureImportanceMetrics =
                featureImportance
                    .Select((metric, index) => new { index, metric.RSquared })
                    .OrderByDescending(myFeatures => Math.Abs(myFeatures.RSquared.Mean));

            Console.WriteLine("Feature\tPFI");

            foreach (var feature in featureImportanceMetrics)
            {
                if (feature.index >= transformedData.Schema.Count 
                    || (transformedData.Schema[feature.index].IsHidden 
                    || transformedData.Schema[feature.index].Name == "Label" 
                    || transformedData.Schema[feature.index].Name == "Features"))
                {
                    continue;
                }

                Console.WriteLine($"{transformedData.Schema[feature.index].Name,-20}|\t{feature.RSquared.Mean:F6}");
            }
        }

        private static List<HouseSale> LoadTestData(string dataFolder)
        {
            var houseSales = new List<HouseSale>();

            using (ArffReader arffReader = new ArffReader($"{dataFolder}\\openml-house_sales.arff"))
            {
                var header = arffReader.ReadHeader();
                object[][] instances = arffReader.ReadAllInstances();

                for (int row = 0; row < instances.GetLength(0); row++)
                {
                    houseSales.Add(instances[row].MapTo<HouseSale>(header));
                }
            }

            return houseSales;
        }

        private static void PlotRegressionChart(string modelPath,
                                                string dataFolder,
                                                MLContext mlContext,
                                                int numberOfRecordsToRead)
        {
            ITransformer trainedModel;
            using (var stream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                trainedModel = mlContext.Model.Load(stream, out var modelInputSchema);
            }

            // Create prediction engine related to the loaded trained model
            var predFunction = mlContext.Model.CreatePredictionEngine<HouseSale, HouseSalePrediction>(trainedModel);

            string chartFileName = "";
            using (var pl = new PLStream())
            {
                // use SVG backend and write to SineWaves.svg in current directory
                pl.sdev("svg");
                chartFileName = "plot.svg";
                pl.sfnam(chartFileName);

                // use white background with black foreground
                pl.spal0("cmap0_alternate.pal");

                // Initialize plplot
                pl.init();

                // set axis limits
                const int xMinLimit = 0;
                const int xMaxLimit = 1_000_000;
                const int yMinLimit = 0;
                const int yMaxLimit = 1_000_000;
                pl.env(xMinLimit, xMaxLimit, yMinLimit, yMaxLimit, AxesScale.Independent, AxisBox.BoxTicksLabelsAxes);

                // Set scaling for mail title text 125% size of default
                pl.schr(0, 1.25);

                // The main title
                pl.lab("Measured", "Predicted", "Distribution of Prediction");

                // plot using different colors
                // see http://plplot.sourceforge.net/examples.php?demo=02 for palette indices
                pl.col0(1);

                int totalNumber = numberOfRecordsToRead;
                var testData = LoadTestData(dataFolder).Take(totalNumber).ToList();

                //This code is the symbol to paint
                char code = (char)9;

                // plot using other color
                //pl.col0(9); //Light Green
                //pl.col0(4); //Red
                pl.col0(2); //Blue

                double yTotal = 0;
                double xTotal = 0;
                double xyMultiTotal = 0;
                double xSquareTotal = 0;

                for (int i = 0; i < testData.Count; i++)
                {
                    var x = new double[1];
                    var y = new double[1];

                    //Make Prediction
                    var prediction = predFunction.Predict(testData[i]);

                    var greaterValue = Math.Max(testData[i].Price, prediction.Score);
                    var smallerValue = Math.Min(testData[i].Price, prediction.Score);

                    x[0] = testData[i].Price;
                    y[0] = prediction.Score;

                    //Paint a dot
                    pl.poin(x, y, code);

                    xTotal += x[0];
                    yTotal += y[0];

                    double multi = x[0] * y[0];
                    xyMultiTotal += multi;

                    double xSquare = x[0] * x[0];
                    xSquareTotal += xSquare;

                    double ySquare = y[0] * y[0];
                }

                // Regression Line calculation explanation:
                // https://www.khanacademy.org/math/statistics-probability/describing-relationships-quantitative-data/more-on-regression/v/regression-line-example

                double minY = yTotal / totalNumber;
                double minX = xTotal / totalNumber;
                double minXY = xyMultiTotal / totalNumber;
                double minXsquare = xSquareTotal / totalNumber;

                double m = ((minX * minY) - minXY) / ((minX * minX) - minXsquare);

                double b = minY - (m * minX);

                //Generic function for Y for the regression line
                // y = (m * x) + b;

                double x1 = 1;
                //Function for Y1 in the line
                double y1 = (m * x1) + b;

                double x2 = 39;
                //Function for Y2 in the line
                double y2 = (m * x2) + b;

                var xArray = new double[2];
                var yArray = new double[2];
                xArray[0] = x1;
                yArray[0] = y1;
                xArray[1] = x2;
                yArray[1] = y2;

                pl.col0(4);
                pl.line(xArray, yArray);

                // end page (writes output to disk)
                pl.eop();

                // output version of PLplot
                pl.gver(out var verText);
                Console.WriteLine("PLplot version " + verText);

            } // the pl object is disposed here

            // Open Chart File In Microsoft Photos App (Or default app, like browser for .svg)

            Console.WriteLine("Showing chart...");
            var p = new Process();
            string chartFileNamePath = @".\" + chartFileName;
            p.StartInfo = new ProcessStartInfo(chartFileNamePath)
            {
                UseShellExecute = true
            };
            p.Start();
        }
    }
}
