namespace Lib;

public class Matrix : AMatrix
{
    public override IIteratorFactory<IMatrix, double> IteratorFactory => new MatrixIteratorFactory<Iterator>();

    public Matrix(uint rowCount, uint columnCount) : base(rowCount, columnCount)
    {
    }

    protected override IVector InitializeVector(uint size) => new Vector(size);

    private class Iterator : IIterator<IMatrix, double>
    {
        public IMatrix Collection { get; init; }
        private (int, int)? _currentElement;

        public Iterator()
        {
            _currentElement = null;
        }

        public double GetNext()
        {
            throw new NotImplementedException();
        }

        public bool HasNext()
        {
            throw new NotImplementedException();
        }
    }
}
