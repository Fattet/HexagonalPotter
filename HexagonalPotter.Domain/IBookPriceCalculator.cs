namespace HexagonalPotter.Domain
{
    public interface IBookPriceCalculator
    {
        double Price(List<int> books);
    }
}