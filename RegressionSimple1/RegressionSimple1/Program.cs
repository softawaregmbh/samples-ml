using ArffTools;
using Microsoft.ML;
using System;
using System.Collections.Generic;

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
        }
    }
}
