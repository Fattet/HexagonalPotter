using HexagonalPotter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalPotter.Application
{
    public class BestPriceStrategy : IDiscountStrategy
    {
        private readonly IDiscountStrategy[] _strategies;

        public BestPriceStrategy(params IDiscountStrategy[] strategies)
        {
            _strategies = strategies;
        }

        public double CalculatePrice(int[] bookCounts, Book book)
        {
            return _strategies
                .Select(s => s.CalculatePrice((int[])bookCounts.Clone(), book))
                .Min();
        }
    }
}
