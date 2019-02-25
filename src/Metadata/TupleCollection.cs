using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    public class TupleCollection : ICollection
    {
        private XElement _xAxis;
        private CubeDef _cube;
        private List<XElement> _xTuples;
        private List<XElement> XTuples => _xTuples ??
            (_xTuples = EnvelopeHelpers.GetXTuples(_xAxis).ToList());

        private Tuple[] _innerCollection;

        public bool IsSynchronized => false;

        public object SyncRoot { get; }

        public Tuple this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (_innerCollection == null)
                    _innerCollection = new Tuple[Count];

                var res = _innerCollection[index];

                if (res != null)
                    return res;

                res = new Tuple(XTuples[index], _cube);
                _innerCollection[index] = res;

                return res;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => XTuples.Count;

        internal TupleCollection(XElement xAxis, CubeDef cube)
        {
            _xAxis = xAxis;
            _cube = cube;
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
            private TupleCollection _tuples;

            public Tuple Current
            {
                get
                {
                    try
                    {
                        return _tuples[_currentIndex];
                    }
                    catch (ArgumentException ex)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            internal Enumerator(TupleCollection tuples)
            {
                _tuples = tuples;
                _currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _tuples.Count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }
        }
    }
}