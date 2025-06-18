using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalPotter.Domain
{
    public static class DiscountPolicy
    {
        private static readonly Dictionary<int, double> _discounts = new()
        {
            { 1, 1.0 },
            { 2, 0.95 },
            { 3, 0.9 },
            { 4, 0.8 },
            { 5, 0.75 }
        };

        public static double GetDiscount(int groupSize)
        {
            return _discounts.TryGetValue(groupSize, out var discount)
                ? discount
                : throw new ArgumentOutOfRangeException(nameof(groupSize), $"Unsupported group size: {groupSize}");
        }
    }
}
