using HexagonalPotter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalPotter.Application
{
    public class GroupOf4FirstStrategy : IDiscountStrategy
    {
        public double CalculatePrice(int[] bookCounts, Book book)
        {
            int[] workingCounts = (int[])bookCounts.Clone();
            double total = 0;

            int groupSize = 0;
            for (int i = 0; i < workingCounts.Length && groupSize < 4; i++)
            {
                if (workingCounts[i] > 0)
                {
                    workingCounts[i]--;
                    groupSize++;
                }
            }

            if (groupSize > 0)
                total += book.BasePrice * groupSize * DiscountPolicy.GetDiscount(groupSize);

            while (workingCounts.Sum() > 0)
            {
                int distinct = 0;
                for (int i = 0; i < workingCounts.Length; i++)
                {
                    if (workingCounts[i] > 0)
                    {
                        workingCounts[i]--;
                        distinct++;
                    }
                }

                if (distinct > 0)
                    total += book.BasePrice * distinct * DiscountPolicy.GetDiscount(distinct);
            }

            return total;
        }
    }
}
