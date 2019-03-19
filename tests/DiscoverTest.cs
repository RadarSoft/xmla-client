using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadarSoft.XmlaClient;
using SimpleSOAPClient.Models;
using RadarSoft.XmlaClient.Metadata;

namespace UnitTest.XmlaClient.NetCore
{
    [TestClass]
    public class DiscoverTest
    {
        [TestMethod]
        public void GetNamedSets()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            NamedSetCollection nsets = cube.NamedSets;

            connection.Close();

            Assert.IsTrue(nsets.Count > 0);
        }

        [TestMethod]
        public void GetKpis()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            var kpis = cube.Kpis;

            connection.Close();

            Assert.IsTrue(kpis.Count > 0);
        }

        [TestMethod]
        public void FindMemberProperty()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            KpiCollection kpis = cube.Kpis;

            MeasureCollection meas = cube.Measures;

            DimensionCollection dims = cube.Dimensions;

            HierarchyCollection hiers = dims[0].Hierarchies;

            LevelCollection levels = hiers[0].Levels;

            MemberCollection members = levels[1].GetMembers();

            MemberProperty prop = members[0].MemberProperties.Find("PARENT_UNIQUE_NAME");

            Assert.IsTrue(!string.IsNullOrEmpty(prop.Value.ToString()));

            connection.Close();
        }

        [TestMethod]
        public void FineProperty()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            Property prop;

            var kpis = cube.Kpis;
            prop = kpis[0].Properties.Find("KPI_PARENT_KPI_NAME");
            Assert.IsTrue(prop != null);

            var meas = cube.Measures;
            prop = meas[0].Properties.Find("DEFAULT_FORMAT_STRING");
            Assert.IsTrue(prop != null);


            var dims = cube.Dimensions;
            prop = dims[0].Properties.Find("DIMENSION_CARDINALITY");
            Assert.IsTrue(prop != null);

            var hiers = dims[0].Hierarchies;
            prop = hiers[0].Properties.Find("HIERARCHY_CARDINALITY");
            Assert.IsTrue(prop != null);

            var levels = hiers[0].Levels;
            prop = levels[0].Properties.Find("LEVEL_NUMBER");
            Assert.IsTrue(prop != null);

            var members = levels[1].GetMembers();
            prop = members[0].Properties.Find("MEMBER_ORDINAL");
            Assert.IsTrue(prop != null);

            connection.Close();
        }

        [TestMethod]
        public void GetMeasures()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            var meas = cube.Measures;

            connection.Close();

            Assert.IsTrue(meas.Count > 0);
        }

        [TestMethod]
        public void GetMeasureGroups()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            MeasureGroupCollection mgroups = cube.MeasureGroups;

            connection.Close();

            Assert.IsTrue(mgroups.Count > 0);
        }

        [TestMethod]
        public void GetMembers()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            //var dims = cube.Dimensions;


            //var hierscols = dims.Select(d => d.Hierarchies);
            //var hiers = hierscols.SelectMany(x => x);

            //Debug.WriteLine(hiers.Count());

            //var levelscols = hiers.Select(x => x.Levels);
            //var levels = levelscols.SelectMany(x => x);

            //Debug.WriteLine(levels.Count());

            var levels = cube.Dimensions.FindHierarchy("[Customer].[Customer Geography]")?
                .Levels;

            var memscols = levels.Select(x => x.GetMembers());
            var mems = memscols.SelectMany(x => x);

            Debug.WriteLine(mems.Count());

            Assert.IsTrue(mems.Count() > 0);

            connection.Close();
        }

        [TestMethod]
        public void GetLevelProperties()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            var dims = cube.Dimensions;
            var hiers = dims[1].Hierarchies;
            var levels = hiers[0].Levels;

            int count = levels[1].LevelProperties.Count;

            connection.Close();

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void GetLevels()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            var levels = cube.Dimensions.FirstOrDefault()?
                .Hierarchies.FirstOrDefault()?
                .Levels;

            Assert.IsTrue(levels?.Count > 0);
            connection.Close();
        }

        [TestMethod]
        public void GetHierarchies()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            var dims = cube.Dimensions;
            var hiers = dims[1].Hierarchies;

            connection.Close();

            Assert.IsTrue(hiers.Count > 0);
        }

        [TestMethod]
        public void FindHierarchy()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            var dims = cube.Dimensions;
            var hier = dims.FindHierarchy("[Customer].[Customer Geography]");
            Assert.IsTrue(hier != null);
            connection.Close();
        }


        [TestMethod]
        public void GetActions()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();


            var actions = connection.GetActions(
                "Analysis Services Tutorial",
                "([Measures].[Internet Sales-Unit Price])",
                CoordinateType.Cell
                );

            connection.Close();

            Assert.IsTrue(actions.Count > 0);
        }

        [TestMethod]
        public void GetDimensions()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = TestHelper.GetCube(connection);

            var dims = cube.Dimensions;

            Assert.IsTrue(dims.Count == 13);

            connection.Close();
        }

        [TestMethod]
        public void FindCube()
        {
            string cubeName = "Analysis Services Tutorial";
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            CubeDef cube = connection.Cubes.Find(cubeName);

            connection.Close();

            Assert.AreEqual(cube.CubeName, cubeName);
        }

        [TestMethod]
        public void DiscoverCubes()
        {
            bool assert = true;
            var connection = TestHelper.CreateConnectionToSsas();
            connection.Open();

            var command = new XmlaCommand("MDSCHEMA_CUBES", connection);
            try
            {
                SoapEnvelope result = command.Execute() as SoapEnvelope;
                connection.Close();
            }
            catch (Exception e)
            {
                assert = false;
            }
            Assert.IsTrue(assert);
        }       
    }
}
