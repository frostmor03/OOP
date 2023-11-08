using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace lab2_2.Library
{
    public class Pyramid<T> : Figure<T> where T : INumber<T>
    {
        private T _a;
        private T _h;

        public Pyramid(T a, T h) : base(nameof(Pyramid<T>), FigureType.Figure3D)
        {
            if (a <= T.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (h <= T.Zero)
            {
                throw new ArgumentOutOfRangeException();
            }
            _a = a;
            _h = h;
        }

        public void Deconstruct(out T a, out T h)
        {
            a = _a;
            h = _h;
        }

        public override T CalculatePerimeter()
        {
            OnCalculatePerimeterEvent(EventArgs.Empty);
            return T.CreateChecked(4) * _a;
        }

        public override T CalculateSquare()
        {
            OnCalculateSquareEvent(EventArgs.Empty);
            T lSquared = T.CreateChecked((_a * _a)/T.CreateChecked(4)+ _h * _h);
            T l = T.CreateChecked(double.Sqrt(double.CreateChecked(lSquared)));

            T factor = T.CreateChecked(2) * _a * l;
            T result = T.CreateChecked(factor + (_a * _a));
            T roundedResult = T.CreateChecked(double.Round(Convert.ToDouble(result), 3, MidpointRounding.ToZero));

            return roundedResult;
        }

        public override T CalculateVolume()
        {
            OnCalculateVolumeEvent(EventArgs.Empty);
            T res = (T.CreateChecked(1) / T.CreateChecked(3)) * (_a * _a) * _h;
            double roundedResult = double.Round(Convert.ToDouble(res), 3, MidpointRounding.ToZero); 
            return T.CreateChecked(roundedResult);

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
            string str = @$"Длина стороны основания равна {_a}, высота равна {_h}, значит объем пирамиды равен {CalculateVolume()}";
            writer.Write(str);
            writer.Flush();
        }

        public override async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            using StreamWriter writer = new StreamWriter(FileStream, leaveOpen: true);
            string str = @$"Длина стороны основания равна {_a}, высота равна {_h}, значит объем пирамиды равен {CalculateVolume()}";
            await writer.WriteAsync(str);
            await writer.FlushAsync();
        }
    }
}
