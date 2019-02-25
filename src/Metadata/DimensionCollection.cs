using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    public class DimensionCollection : ICollection<Dimension>
    {
        private CubeDef _cube;
        private List<XElement> XDimensions { get; }
        private Dimension[] _innerDimensions;
        public bool IsSynchronized => false;

        public object SyncRoot { get; }

        public Hierarchy FindHierarchy(string uname)
        {
            var dim = this.FirstOrDefault(x => x.Hierarchies.Select(y => y.UniqueName).Contains(uname));
            return dim.Hierarchies.FindHierarchy(uname);
        }

        public Dimension this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (_innerDimensions == null)
                    _innerDimensions = new Dimension[Count];

                Dimension dimRes = _innerDimensions[index];

                if (dimRes != null)
                    return dimRes;

                dimRes = XDimensions[index].ToObject<Dimension>(_cube);

                _innerDimensions[index] = dimRes;

                return dimRes;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => XDimensions.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        internal DimensionCollection(CubeDef cube, IEnumerable<XElement> xDimentions)
        {
            _cube = cube;
            XDimensions = xDimentions.ToList();
        }

        public void CopyTo(Axis[] array, int index)
        {
            ((ICollection)this).CopyTo(array, index);
        }


        public void Add(Dimension item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Dimension item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Dimension[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Dimension item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<Dimension> IEnumerable<Dimension>.GetEnumerator()
        {
            return GetEnumerator() as IEnumerator<Dimension>;
        }

        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<Dimension>
        {
            private int _currentIndex;
            private DimensionCollection _dimensions;

            public Dimension Current
            {
                get
                {
                    try
                    {
                        return _dimensions[_currentIndex];
                    }
                    catch (ArgumentException ex)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            internal Enumerator(DimensionCollection dimensions)
            {
                _dimensions = dimensions;
                _currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _dimensions.Count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            public void Dispose()
            {
                //throw new NotImplementedException();
            }
        }
    }
}
