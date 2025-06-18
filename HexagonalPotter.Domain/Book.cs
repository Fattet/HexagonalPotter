using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalPotter.Domain
{
    public class Book
    {
        public int Id { get; }
        public double BasePrice { get; }

        public static readonly double DefaultBasePrice = 8.0;

        public Book(int id)
        {
            Id = id;
            BasePrice = DefaultBasePrice;
        }
    }
}
