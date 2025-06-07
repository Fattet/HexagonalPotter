using HexagonalPotter.Domain;

namespace HexagonalPotter.Application
{
    public class PotterPriceCalculator : IBookPriceCalculator
    {
        #region Constants
        private const double BPrice = 8.0;
        #endregion

        #region Public Methods
        /// <summary>
        /// Calculates the price of a list of books based on the Potter book pricing rules.
        /// </summary>
        /// <param name="bookList"></param>
        /// <returns>Lowest price posible (double)</returns>
        public double Price(List<int> bookList)
        {
            int[] bookCounts = new int[5];
            foreach (var book in bookList)
            {
                bookCounts[book]++;
            }

            OrderDesc(bookCounts);
            int[] bookCounts2 = (int[])bookCounts.Clone();

            return Math.Min(GetPrice2(bookCounts), GetPrice1GroupOf4(bookCounts2));
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Outdated version. Calculates the biggest group of books and applies the discount.
        /// </summary>
        /// <param name="bookCounts"></param>
        /// <returns>Discounted price for biggest groups (double)</returns>
        static double GetPrice(int[] bookCounts)
        {
            if (bookCounts[0] > 0)
            {
                bookCounts[0]--;
                if (bookCounts[1] > 0)
                {
                    bookCounts[1]--;
                    if (bookCounts[2] > 0)
                    {
                        bookCounts[2]--;
                        if (bookCounts[3] > 0)
                        {
                            bookCounts[3]--;
                            if (bookCounts[4] > 0)
                            {
                                bookCounts[4]--;
                                return BPrice * 5 * 0.75 + GetPrice(bookCounts);
                            }
                            return BPrice * 4 * 0.8 + GetPrice(bookCounts);
                        }
                        return BPrice * 3 * 0.9 + GetPrice(bookCounts);
                    }
                    return BPrice * 2 * 0.95 + GetPrice(bookCounts);
                }
                return BPrice + GetPrice(bookCounts);
            }
            return 0;
        }

        /// <summary>
        /// Improved version of GetPrice that uses recursion to find the best price for any combination of books.
        /// </summary>
        /// <param name="bookCounts"></param>
        /// <returns>Discounted price for biggest groups (double)</returns>
        private static double GetPrice2(int[] bookCounts)
        {
            double[] discounts = { 1.0, 0.95, 0.9, 0.8, 0.75 };
            if (bookCounts.Sum() == 0) return 0;
            for (int i = 0; i < bookCounts.Length; i++)
            {
                if (bookCounts[i] > 0)
                {
                    bookCounts[i]--;
                }
                else return BPrice * i * discounts[i - 1] + GetPrice2(bookCounts);
            }
            return BPrice * 5 * discounts[4] + GetPrice2(bookCounts);
        }

        /// <summary>
        /// Calculates the price for a group of 4 books first.
        /// </summary>
        /// <param name="bookCounts"></param>
        /// <returns>Discounted price for a group of 4 first (double)</returns>
        private static double GetPrice1GroupOf4(int[] bookCounts)
        {
            if (bookCounts[0] > 0)
            {
                bookCounts[0]--;
                if (bookCounts[1] > 0)
                {
                    bookCounts[1]--;
                    if (bookCounts[2] > 0)
                    {
                        bookCounts[2]--;
                        if (bookCounts[3] > 0)
                        {
                            bookCounts[3]--;
                            OrderDesc(bookCounts);
                            return BPrice * 4 * 0.8 + GetPrice2(bookCounts);
                        }
                        return BPrice * 3 * 0.9 + GetPrice2(bookCounts);
                    }
                    return BPrice * 2 * 0.95 + GetPrice2(bookCounts);
                }
                return BPrice + GetPrice2(bookCounts);
            }
            return 0;
        }

        /// <summary>
        /// Orders the book counts in descending order to aboid skipping books in the calculation.
        /// </summary>
        /// <param name="bookCounts"></param>
        private static void OrderDesc(int[] bookCounts)
        {
            Array.Sort(bookCounts);
            Array.Reverse(bookCounts);
        }
        #endregion
    }
}