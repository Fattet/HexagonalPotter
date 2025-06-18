using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalPotter.Domain
{
    public interface IDiscountStrategy
    {
        double CalculatePrice(int[] bookCounts, Book book);
    }
}
