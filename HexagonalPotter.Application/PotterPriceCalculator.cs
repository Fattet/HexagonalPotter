using HexagonalPotter.Domain;

namespace HexagonalPotter.Application
{
    public class PotterPriceCalculator : IBookPriceCalculator
    {
        private readonly IDiscountStrategy _strategy;
        private readonly Book _book;

        public PotterPriceCalculator(IDiscountStrategy strategy)
        {
            _strategy = strategy;
            _book = new Book(id: 0);
        }

        public double Price(List<int> bookList)
        {
            int[] bookCounts = new int[5];
            foreach (var book in bookList)
            {
                bookCounts[book]++;
            }

            OrderDesc(bookCounts);
            return _strategy.CalculatePrice(bookCounts, _book);
        }

        private static void OrderDesc(int[] bookCounts)
        {
            Array.Sort(bookCounts);
            Array.Reverse(bookCounts);
        }
    }

}