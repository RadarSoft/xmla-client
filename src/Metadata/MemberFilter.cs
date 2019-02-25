using System;

namespace RadarSoft.XmlaClient.Metadata
{
    public class MemberFilter
    {
        public MemberFilter(string propertyName, string propertyValue)
        {
            throw new NotImplementedException();
        }

        public MemberFilter(string propertyName, MemberFilterType filterType, string propertyValue)
        {
            throw new NotImplementedException();
        }

        public MemberFilterType FilterType { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
    }
}