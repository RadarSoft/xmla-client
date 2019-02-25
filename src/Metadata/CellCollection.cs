using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    public class CellCollection : ICollection
    {
        private SoapEnvelope _envelope;
        private List<XElement> _xCells;
        private List<XElement> XCells => _xCells ??
            (_xCells = _envelope.GetXCells().ToList());

        private Cell[] _innerCollection;

        public bool IsSynchronized => false;

        public object SyncRoot { get; }

        public Cell this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (_innerCollection == null)
                    _innerCollection = new Cell[Count];

                var res = _innerCollection[index];

                if (res != null)
                    return res;

                res = XCells[index].ToObject<Cell>();
                res.InitCell(XCells[index]);

                _innerCollection[index] = res;

                return res;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => XCells.Count;

        internal CellCollection(SoapEnvelope envelope)
        {
            _envelope = envelope;
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
            private CellCollection _cells;

            public Cell Current
            {
                get
                {
                    try
                    {
                        return _cells[_currentIndex];
                    }
                    catch (ArgumentException ex)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            internal Enumerator(CellCollection cells)
            {
                _cells = cells;
                _currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _cells.Count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }
        }
    }
}