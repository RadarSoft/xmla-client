using System;
using System.Xml.Linq;

namespace RadarSoft.XmlaClient.Metadata
{
    public sealed class Position
    {
        internal Position(MemberCollection members)
        {
            Members = members;
        }

        public MemberCollection Members { get; }
    }
}