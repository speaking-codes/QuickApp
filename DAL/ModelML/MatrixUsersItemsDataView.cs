using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelML
{
    public class MatrixUsersItemsDataView
    {
        [LoadColumn(0)] 
        public long UserId { get; set; }

        [LoadColumn(1)]
        public byte ItemId { get; set; }
        
        [LoadColumn(2)]
        public double Rating { get; set; }
    }

    public class PredictionRating
    {
        public float Label { get; set; }
        public float Score { get; set; }
    }
}
