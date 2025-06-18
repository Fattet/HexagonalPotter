using HexagonalPotter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalPotter.Application
{
    public class BasicDiscountStrategy : IDiscountStrategy
    {
        public double CalculatePrice(int[] bookCounts, Book book)
        {
            if (bookCounts.Sum() == 0) return 0;

            int distinct = 0;
            for (int i = 0; i < bookCounts.Length; i++)
            {
                if (bookCounts[i] > 0)
                {
                    bookCounts[i]--;
                    distinct++;
                }
            }

            return book.BasePrice * distinct * DiscountPolicy.GetDiscount(distinct) + CalculatePrice(bookCounts, book);
        }
    }
}
