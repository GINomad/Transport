using System;

namespace Transport.Business.Util
{
    public class TransportHelper
    {
    }

    public abstract class TransportHandler
    {
        public TransportHandler(TransportHandler handler)
        {
            _handler = handler;
        }
        protected TransportHandler _handler;
        protected double _weight;
        protected double _height;
        protected double _width;
        protected double _length;
        protected int _transportType;

        protected bool HasNext()
        {
            return _handler != null;
        }

        public virtual int Process(double weight, double height, double width, double length)
        {
            if(this._weight <= weight && this._height <= height && this._width <= width && this._length <= length)
            {
                return this._transportType;
            }
            else if (HasNext())
            {
                return _handler.Process(weight, height, width, length);
            }
            else
            {
                return 0;
            }
        }
    }

    public class CarHandler : TransportHandler
    {
        public CarHandler(TransportHandler handler) : base(handler)
        {
            _weight = 200;
            _width = 0.5;
            _length = 1;
            _height = 0.75;
            _transportType = 4;
        }       
    }

    public class VanHandler : TransportHandler
    {
        public VanHandler(TransportHandler handler) : base(handler)
        {
        }
    }
}
