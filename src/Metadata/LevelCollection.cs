using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    public class LevelCollection : ICollection<Level>
    {
        private Hierarchy _hierarchy;
        private List<XElement> XLevels { get; }
        private Level[] _innerCollection;

        public bool IsSynchronized => false;

        public object SyncRoot { get; }

        internal Level FindLevel(string name)
        {
            return this.FirstOrDefault(x => x.UniqueName == name);
        }

        public Level this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (_innerCollection == null)
                    _innerCollection = new Level[Count];

                var res = _innerCollection[index];

                if (res != null)
                    return res;

                res = XLevels[index].ToObject<Level>(_hierarchy);

                _innerCollection[index] = res;

                return res;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => XLevels.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        internal LevelCollection(Hierarchy hierarchy, IEnumerable<XElement> xLevels)
        {
            _hierarchy = hierarchy;
            XLevels = xLevels.ToList();
        }

        public void CopyTo(Axis[] array, int index)
        {
            ((ICollection)this).CopyTo(array, index);
        }


        public void Add(Level item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Level item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Level[] array, int arrayIndex)
        {
            for (int index1 = 0; index1 < Count; ++index1)
                array.SetValue(this[index1], arrayIndex + index1);
        }

        public bool Remove(Level item)
        {
            throw new NotImplementedException();
        }

        IEnumerator<Level> IEnumerable<Level>.GetEnumerator()
        {
            return GetEnumerator() as IEnumerator<Level>;
        }

        public IEnumerator GetEnumerator()
        {
            return new LevelCollection.Enumerator(this);
        }

        public struct Enumerator : IEnumerator<Level>
        {
            private int _currentIndex;
            private LevelCollection _levels;

            public Level Current
            {
                get
                {
                    try
                    {
                        return _levels[_currentIndex];
                    }
                    catch (ArgumentException ex)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            internal Enumerator(LevelCollection levels)
            {
                _levels = levels;
                _currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _levels.Count;
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
