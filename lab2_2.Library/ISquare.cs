public interface ISquare<T> where T : INumber<T>
{
    T CalculateSquare();
    Task<T> CalculateSquareAsync(CancellationToken cancellationToken = default);
}
