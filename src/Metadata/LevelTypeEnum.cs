using System;

namespace RadarSoft.XmlaClient.Metadata
{
    [Flags]
    public enum LevelTypeEnum
    {
        Regular = 0,
        All = 1,
        Calculated = 2,
        Time = 4,
        Reserved1 = 8,
        TimeYears = 20,
        TimeHalfYears = 36,
        TimeQuarters = 68,
        TimeMonths = 132,
        TimeWeeks = 260,
        TimeDays = 516,
        TimeHours = 772,
        TimeMinutes = 1028,
        TimeSeconds = 2052,
        TimeUndefined = 4100,
        OrgUnit = 4113,
        BomResource = 4114,
        Quantitative = 4115,
        Account = 4116,
        Scenario = 4117,
        Utility = 4118,
        Customer = 4129,
        CustomerGroup = 4130,
        CustomerHousehold = 4131,
        Product = 4145,
        ProductGroup = 4146,
        Person = 4161,
        Company = 4162,
        CurrencySource = 4177,
        CurrencyDestination = 4178,
        Channel = 4193,
        Representative = 4194,
        Promotion = 4209,
        GeoContinent = 8193,
        GeoRegion = 8194,
        GeoCountry = 8195,
        GeoStateOrProvince = 8196,
        GeoCounty = 8197,
        GeoCity = 8198,
        GeoPostalCode = 8199,
        GeoPoint = 8200
    }
}