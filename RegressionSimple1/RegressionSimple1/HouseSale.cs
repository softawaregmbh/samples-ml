using System;
using System.Collections.Generic;
using System.Text;

namespace RegressionSimple1
{
    public class HouseSale
    {
        public string Date { get; set; }

        public float Price { get; set; }

        public float Bedrooms { get; set; }

        public float Bathrooms { get; set; }

        public float Sqft_Living { get; set; }

        public float Sqft_Lot { get; set; }

        public float Sqft_Above { get; set; }

        public float Floors { get; set; }

        public float View { get; set; }

        public float Waterfront { get; set; }

        public float Yr_Built { get; set; }

        public float Yr_Renovated { get; set; }

        public float ZipCode { get; set; }

        public float Grade { get; set; }

        public float Condition { get; set; }

        public override string ToString()
        {
            return $"House for sale: built {this.Yr_Built}, {this.Sqft_Living} ft² living, {this.Sqft_Lot} ft² lot, {this.Bedrooms} bedrooms, {this.Bathrooms} bathrooms";
        }
    }
}
