using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace lab2_2.Library
{
    public class Circle<T> : Figure<T>, IDisposable where T : INumber<T>
    {
        readonly T _radius;

        public Circle(T radius) : base(nameof(Circle<T>), FigureType.Figure2D)
        {
            if (radius <= T.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(radius));
            }

            _radius = radius;
        }

        public void Deconstruct(out T radius)
        {
            radius = _radius;
        }

        public override T CalculatePerimeter()
        {
            OnCalculatePerimeterEvent(EventArgs.Empty);

            return T.CreateChecked(double.Round(double.CreateChecked(T.CreateChecked(double.Pi) * T.CreateChecked(2) * _radius), 3, MidpointRounding.ToZero));
        }   

        public override T CalculateSquare()
        {
            OnCalculateSquareEvent(EventArgs.Empty);

            return T.CreateChecked(double.Round(double.CreateChecked(T.CreateChecked(double.Pi) * _radius * _radius), 3, MidpointRounding.ToZero));
        }

        public override T CalculateVolume()
        {   
            OnCalculateVolumeEvent(EventArgs.Empty);
            throw new ArgumentOutOfRangeException();
        }

        public override async Task<T> CalculatePerimeterAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            T result = CalculatePerimeter();
            if (OnCalculatePerimeterAsyncEvent(EventArgs.Empty) is Task @event)
            {
                await @event;
            }
            return result;
        }

        public override async Task<T> CalculateSquareAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            T result = CalculateSquare();
            if (OnCalculateSquareAsyncEvent(EventArgs.Empty) is Task @event)
            {
                await @event;
            }
            return result;
        }

        public override async Task<T> CalculateVolumeAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            T result = CalculateVolume();
            if (OnCalculateVolumeAsyncEvent(EventArgs.Empty) is Task @event)
            {
                await @event;
            }
            return result;
        }

        public override void Save()
        {
            using StreamWriter writer = new StreamWriter(FileStream, leaveOpen: true);
            string str = @$"Радиус круга равен: {_radius}, значит периметр равен: {CalculatePerimeter()}, площадь равна: {CalculateSquare()}, двумерная фигура не имеет объема!";
            writer.Write(str);
            writer.Flush();
        }

        public override async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            using StreamWriter writer = new StreamWriter(FileStream, leaveOpen: true);
            string str = @$"Радиус круга равен: {_radius}, значит периметр равен: {CalculatePerimeter()}, площадь равна: {CalculateSquare()}, двумерная фигура не может иметь объема!";
            await writer.WriteAsync(str);
            await writer.FlushAsync();
        }
    }
}
