using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    public class MemberCollection : ICollection<Member>
    {
        private Level _Level;
        private XElement _xTuple;
        private CubeDef _cube;
        private List<XElement> _xMembers;
        private List<XElement> XMembers => _xMembers ??
            (_xMembers = EnvelopeHelpers.GetXMembers(_xTuple).ToList());

        private TypleMember[] _TupleMembers;
        private TypleMember[] TupleMembers
        {
            get
            {
                if (_TupleMembers == null)
                {
                    _TupleMembers = new TypleMember[Count];
                    for (int i = 0; i < Count; i++)
                    {
                        var xMember = XMembers[i];
                        var tMember = xMember.ToObject<TypleMember>();
                        _TupleMembers[i] = tMember;
                    }
                }

                return _TupleMembers;
            }
        }

        private Dictionary<string, Member> _UniqeNamesMembers;
        private Dictionary<string, Member> UniqeNamesMembers
        {
            get
            {
                if (_UniqeNamesMembers == null)
                {
                    _UniqeNamesMembers = new Dictionary<string, Member>(XMembers.Count);
                    foreach (var xMember in XMembers)
                    {
                        var member = xMember.ToObject<Member>(_Level);
                        _UniqeNamesMembers.Add(member.UniqueName, member);
                    }
                }

                return _UniqeNamesMembers;
            }
        }

        private Member[] _Members;
        private Member[] Members => _Members ?? (_Members = UniqeNamesMembers.Values.ToArray());

        public bool IsSynchronized => false;

        public object SyncRoot { get; }

        public Member this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                Member res;
                if (_cube != null)
                {
                    var tmember = TupleMembers[index];
                    var uname = tmember.UniqueName;
                    var h = _cube.Dimensions.FindHierarchy(tmember.Hierarchy);
                    _Level = h.FindLevel(tmember.LevelName);

                    res = _Level.FindMember(uname);
                }
                else
                    res = Members[index];

                return res;
            }
        }

        public Member Find(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            Member res = null;
            UniqeNamesMembers.TryGetValue(name, out res);
            return res;
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count => XMembers.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        internal MemberCollection(XElement xTuple, CubeDef cube)
        {
            _xTuple = xTuple;
            _cube = cube;
        }

        public MemberCollection(Level level, IEnumerable<XElement> xMembers)
        {
            _Level = level;
            _xMembers = xMembers.ToList();
        }

        public void CopyTo(Axis[] array, int index)
        {
            ((ICollection)this).CopyTo(array, index);
        }

        IEnumerator<Member> IEnumerable<Member>.GetEnumerator()
        {
            return GetEnumerator() as IEnumerator<Member>;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void Add(Member item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Member item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Member[] array, int arrayIndex)
        {
            for (int index1 = 0; index1 < Count; ++index1)
                array.SetValue(this[index1], arrayIndex + index1);
        }

        public bool Remove(Member item)
        {
            throw new NotImplementedException();
        }

        public struct Enumerator : IEnumerator<Member>
        {
            private int _currentIndex;
            private MemberCollection _members;

            public Member Current
            {
                get
                {
                    try
                    {
                        return _members[_currentIndex];
                    }
                    catch (ArgumentException ex)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            object IEnumerator.Current => Current;

            internal Enumerator(MemberCollection members)
            {
                _members = members;
                _currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++_currentIndex < _members.Count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            public void Dispose()
            {
            }
        }
    }
}