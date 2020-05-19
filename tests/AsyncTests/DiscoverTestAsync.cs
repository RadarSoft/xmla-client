using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadarSoft.XmlaClient.Metadata;

namespace UnitTest.XmlaClient.NetCore.AsyncTests
{
    [TestClass]
    public class DiscoverTestAsync
    {
        [TestMethod]
        public async Task GetDimensionsAsync()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            await connection.OpenAsync();

            CubeDef cube = await TestHelper.GetCubeAsync(connection);

            DimensionCollection dims = await cube.GetDimensionsAsync();

            await connection.CloseAsync();

            Assert.IsTrue(dims.Count > 0);
        }

        [TestMethod]
        public async Task GetMembersAsync()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            await connection.OpenAsync();

            CubeDef cube = await TestHelper.GetCubeAsync(connection);

            DimensionCollection dims = await cube.GetDimensionsAsync();

            var hiersTasks = new List<Task<HierarchyCollection>>();

            foreach (Dimension dim in dims)
            {
                hiersTasks.Add(dim.GetHierarchiesAsync());
            }

            var hiersCols = await Task.WhenAll(hiersTasks);

            var hiers = hiersCols.SelectMany(x => x);

            Debug.WriteLine(hiers.Count());

            var levelsTasks = new List<Task<LevelCollection>>();

            foreach (var hier in hiers)
            {
                levelsTasks.Add(hier.GetLevelsAsync());
            }

            var levelsCols = await Task.WhenAll(levelsTasks);
            var levels = levelsCols.SelectMany(x => x);

            var membersTasks = new List<Task<MemberCollection>>();

            foreach (var level in levels)
            {
                membersTasks.Add(level.GetMembersAsync());
            }

            var membersCols = await Task.WhenAll(membersTasks);
            var members = membersCols.SelectMany(x => x);
            Debug.WriteLine(members.Count());

            await connection.CloseAsync();
        }

        [TestMethod]
        public async Task GetMembersAsync2()
        {
            var connection = TestHelper.CreateConnectionToSsas();
            await connection.OpenAsync();

            CubeDef cube = await TestHelper.GetCubeAsync(connection);

            var levels = cube.Dimensions.FindHierarchy(TestHelper.TEST_HIERARCHY)?
                .Levels;

            var memsTasks = levels.Select(x => x.GetMembersAsync()).ToArray();
            var memscols = await Task.WhenAll(memsTasks);
            var mems = memscols.SelectMany(x => x);

            Debug.WriteLine(mems.Count());

            Assert.IsTrue(mems.Count() > 0);

            await connection.CloseAsync();
        }
    }
}
