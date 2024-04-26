using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MatrixUsersItems
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public byte ItemId { get; set; }
        public double Rating { get; set; }
    }
}
