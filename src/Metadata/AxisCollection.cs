using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    public class AxisCollection : ICollection
    {
        private SoapEnvelope _envelope;
        private CubeDef _cube;
        private List<XElement> _xAxes;
        internal Axis FilterAxis { get; private set; }

        private Axis[] _innerCollection;

        private List<XElement> XAxes {
            get
            {
                if (_xAxes == null)
                {
                    var allXAxes = _envelope.GetXAxes().ToList();

                    int filterXAxisIndex =
                        allXAxes.FindIndex(x => x.Attribute(XName.Get("name"))?.Value == "SlicerAxis");
                    FilterAxis = new Axis(allXAxes[filterXAxisIndex], _cube);
                    allXAxes.RemoveAt(filterXAxisIndex);
                    _xAxes = allXAxes;
                }
                return _xAxes;
            } 
        }

        public bool IsSynchronized => false;

        public object SyncRoot { get; }

        public Axis this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (_innerCollection == null)
                    _innerCollection = new Axis[Count];

                var res = _innerCollection[index];

                if (res != null)
                    return res;

                res = new Axis(XAxes[index], _cube);

                _innerCollection[index] = res;

                return res;
            }
        }

        public Axis this[string index]
        {
            get
            {
                Axis axis = Find(index);
                if (null == axis)
                    throw new ArgumentException("index");
                return axis;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => XAxes.Count;

        internal AxisCollection(SoapEnvelope envelope, CubeDef cube)
        {
            _envelope = envelope;
            _cube = cube;
        }

        public Axis Find(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var res = _innerCollection.FirstOrDefault(x => x.Name == name);

            if (res != null)
                return res;

            foreach (XElement xaxis in XAxes)
            {
                if (xaxis.Attribute(XName.Get("name"))?.Value == name)
                {
                    res = new Axis(xaxis, _cube);
                    int axisIndex = XAxes.IndexOf(xaxis);
                    _innerCollection[axisIndex] = res;
                    break;
                }
            }

            return res;
        }

        public void CopyTo(Axis[] array, int index)
        {
            ((ICollection)this).CopyTo(array, index);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator
        {
            private int _currentIndex;
            private AxisCollection _axes;

            public Axis Current
            {
                get
                {
                    try
                    {
                        return _axes[_currentIndex];
                    }
                    catch (ArgumentException ex)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            internal Enumerator(AxisCollection axes)
            {
                _axes = axes;
                _currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _axes.Count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }
        }
    }
}