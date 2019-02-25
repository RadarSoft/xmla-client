using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    public class PositionCollection : ICollection
    {
        private TupleCollection _tupleCollection;
        private Position[] _innerCollection;

        public bool IsSynchronized => false;

        public object SyncRoot { get; }

        public Position this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (_innerCollection == null)
                    _innerCollection = new Position[Count];

                var res = _innerCollection[index];

                if (res != null)
                    return res;

                res = new Position(_tupleCollection[index].Members);

                _innerCollection[index] = res;

                return res;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => _tupleCollection.Count;

        public PositionCollection(TupleCollection tupleCollection)
        {
            _tupleCollection = tupleCollection;
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
            private PositionCollection _positions;

            public Position Current
            {
                get
                {
                    try
                    {
                        return _positions[_currentIndex];
                    }
                    catch (ArgumentException ex)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            internal Enumerator(PositionCollection positions)
            {
                _positions = positions;
                _currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _positions.Count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }
        }
    }
}