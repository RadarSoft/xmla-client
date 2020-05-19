# XMLA provider for a .NET Core OLAP client

Provides data access to any OLAP servers that support XMLA protocol and can be accessed over an HTTP connection.
It is designed for development of an OLAP client in .NET Core applications that are completely independent of .NET Framework and therefore cannot use ADOMD.NET.

## Testing
To run the provided tests you must enter the specifics (datasource, username, password, etc) of your OLAP environment in `/tests/TestHelper.cs`. 

**Please remember not to commit these to source control!**


## Connection
```csharp
var connection = new XmlaConnection("Data Source=http://localhost/OLAP/msmdpump.dll;Initial Catalog=AdventureWorksDW2012Multidimensional-SE");
```
### Open Connection
```csharp
connection.Open();
var sessionId = connection.SessionID;
 ```   
### Close Connection
```csharp
connection.Close();
 ```    
### Open Connection with SessionId
```csharp
var connection = new XmlaConnection("Data Source=http://localhost/OLAP/msmdpump.dll;Initial Catalog=AdventureWorksDW2012Multidimensional-SE");
    
connection.SessionID = sessionId;
connection.Open();
 ``` 
### Close Connection with SessionId

```csharp
var connection = new XmlaConnection("Data Source=http://localhost/OLAP/msmdpump.dll;Initial Catalog=AdventureWorksDW2012Multidimensional-SE");
    
connection.SessionID = sessionId;
connection.Close();
 ``` 
## Discover
```csharp
connection.Open();
var command = new XmlaCommand("MDSCHEMA_CUBES", connection);
SoapEnvelope result = command.Execute() as SoapEnvelope;
connection.Close();
 ```
 
### Cube Metadata
```csharp
connection.Open();
CubeDef cube = connection.Cubes.Find("Adventure Works");

KpiCollection kpis = cube.Kpis;
Property prop = kpis[0].Properties.Find("KPI_PARENT_KPI_NAME");

NamedSetCollection nsets = cube.NamedSets;

MeasureGroupCollection mgroups = cube.MeasureGroups;

MeasureCollection meas = cube.Measures;
prop = meas[0].Properties.Find("DEFAULT_FORMAT_STRING");

DimensionCollection dims = cube.Dimensions;
prop = dims[0].Properties.Find("DIMENSION_CARDINALITY");

HierarchyCollection hiers = dims[0].Hierarchies;
Hierarchy hier = cube.Dimensions.FindHierarchy("[Customer].[Customer Geography]");
prop = hier.Properties.Find("HIERARCHY_CARDINALITY");

LevelCollection levels = hiers[0].Levels;
prop = levels[0].Properties.Find("LEVEL_NUMBER");

MemberCollection members = levels[1].GetMembers();
MemberProperty prop = members[0].MemberProperties.Find("PARENT_UNIQUE_NAME");

ActionCollection acts = connection.GetActions("Adventure Works", "([Measures].[Internet Sales-Unit Price])", CoordinateType.Cell);

connection.Close();
 ```
## Execute CellSet
```csharp
connection.Open();
var mdx = "SELECT FROM [Adventure Works] WHERE [Measures].[Internet Sales Count]";
var command = new XmlaCommand(mdx, connection);
CellSet cellset = command.ExecuteCellSet();
connection.Close();
 ```
## Tested/Working OLAP servers

* Microsoft Analysis Services (Multi-Dimensional semantic model)

## Notes on Mondrian:
It is important to note that Mondrian does not implement the full XMLA (or MDX language) specification. 

One of these gaps is that Mondrian does not handle authentication nor does it return a Session ID in the header. The current implementation of `xmla-client` requires a Session ID
