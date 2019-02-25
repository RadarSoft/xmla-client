using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    public class HierarchyCollection : ICollection<Hierarchy>
    {
        private Dimension _dimentsion;
        private List<XElement> XHierarchies { get; }
        private Hierarchy[] _innerCollection;


        public bool IsSynchronized => false;

        public object SyncRoot { get; }

        internal Hierarchy FindHierarchy(string name)
        {
            return this.FirstOrDefault(x => x.UniqueName == name);
        }

        public Hierarchy this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (_innerCollection == null)
                    _innerCollection = new Hierarchy[Count];

                var res = _innerCollection[index];

                if (res != null)
                    return res;

                res = XHierarchies[index].ToObject<Hierarchy>(_dimentsion);

                _innerCollection[index] = res;

                return res;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => XHierarchies.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        internal HierarchyCollection(Dimension dimension, IEnumerable<XElement> xHierarchies)
        {
            _dimentsion = dimension;
            XHierarchies = xHierarchies.ToList();
        }

        public void CopyTo(Axis[] array, int index)
        {
            ((ICollection)this).CopyTo(array, index);
        }


        public void Add(Hierarchy item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Hierarchy item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Hierarchy[] array, int arrayIndex)
        {
            for (int index1 = 0; index1 < Count; ++index1)
                array.SetValue(this[index1], arrayIndex + index1);
        }

        public bool Remove(Hierarchy item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<Hierarchy> IEnumerable<Hierarchy>.GetEnumerator()
        {
            return GetEnumerator() as IEnumerator<Hierarchy>;
        }

        public IEnumerator GetEnumerator()
        {
            return new HierarchyCollection.Enumerator(this);
        }

        public struct Enumerator : IEnumerator<Hierarchy>
        {
            private int _currentIndex;
            private HierarchyCollection _hierarchies;

            public Hierarchy Current
            {
                get
                {
                    try
                    {
                        return _hierarchies[_currentIndex];
                    }
                    catch (ArgumentException ex)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            internal Enumerator(HierarchyCollection hierarchies)
            {
                _hierarchies = hierarchies;
                _currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _hierarchies.Count;
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
