using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    public class LevelPropertyCollection : ICollection
    {
        private List<XElement> XLevelProp { get; }
        private Level _Level;
        private LevelProperty[] _innerCollection;
        public bool IsSynchronized => false;

        public object SyncRoot { get; }

        public LevelProperty this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (_innerCollection == null)
                    _innerCollection = new LevelProperty[Count];

                var res = _innerCollection[index];

                if (res != null)
                    return res;

                res = XLevelProp[index].ToObject<LevelProperty>(_Level);

                _innerCollection[index] = res;

                return res;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => XLevelProp.Count;

        internal LevelPropertyCollection(Level level, IEnumerable<XElement> xLevelProps)
        {
            _Level = level;
            XLevelProp = xLevelProps.ToList();
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
            private LevelPropertyCollection _levelProps;

            public LevelProperty Current
            {
                get
                {
                    try
                    {
                        return _levelProps[_currentIndex];
                    }
                    catch (ArgumentException ex)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            internal Enumerator(LevelPropertyCollection levelProps)
            {
                _levelProps = levelProps;
                _currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _levelProps.Count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }
        }
    }
}