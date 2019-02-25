using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SimpleSOAPClient.Models;

namespace RadarSoft.XmlaClient.Metadata
{
    public class OlapInfoCubeCollection : ICollection
    {
        private SoapEnvelope _envelope;
        private List<XElement> _xCubs;
        private List<XElement> XCubes => _xCubs ?? 
            (_xCubs = EnvelopeHelpers.GetXCubes(_envelope.GetXOlapInfo()).ToList());
        private OlapInfoCube[] _innerCollection;

        public bool IsSynchronized => false;

        public object SyncRoot { get; }

        public OlapInfoCube this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (_innerCollection == null)
                    _innerCollection = new OlapInfoCube[Count];

                var res = _innerCollection[index];

                if (res != null)
                    return res;

                res = XCubes[index].ToObject<OlapInfoCube>();

                _innerCollection[index] = res;

                return res;
            }
        }

        public OlapInfoCube this[string index]
        {
            get
            {
                OlapInfoCube cube = Find(index);
                if (null == cube)
                    throw new ArgumentException("index");
                return cube;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => XCubes.Count;

        internal OlapInfoCubeCollection(SoapEnvelope envelope)
        {
            _envelope = envelope;
        }

        public OlapInfoCube Find(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            for (int i = 0; i < Count; i++)
            {
                var res = this[i];
                if (res.CubeName == name)
                    return res;
            }

            return null;
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
            private OlapInfoCubeCollection _cubes;

            public OlapInfoCube Current
            {
                get
                {
                    try
                    {
                        return _cubes[_currentIndex];
                    }
                    catch (ArgumentException ex)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            internal Enumerator(OlapInfoCubeCollection cubes)
            {
                _cubes = cubes;
                _currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _cubes.Count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }
        }
    }
}