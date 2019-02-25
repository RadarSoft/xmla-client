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

            Assert.IsTrue(dims.Count == 13);
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

            //DimensionCollection dims = await cube.GetDimensionsAsync();

            //var hiersTasks = dims.Select(d => d.GetHierarchiesAsync()).ToArray();
            //var hierscols = await Task.WhenAll(hiersTasks);

            //var hiers = hierscols.SelectMany(x => x);

            //Debug.WriteLine(hiers.Count());

            //var levelsTasks = hiers.Select(x => x.GetLevelsAsync()).ToArray();
            //var levelscols = await Task.WhenAll(levelsTasks);
            //var levels = levelscols.SelectMany(x => x);

            //Debug.WriteLine(levels.Count());

            var levels = cube.Dimensions.FindHierarchy("[Customer].[Customer Geography]")?
                .Levels;

            var memsTasks = levels.Select(x => x.GetMembersAsync()).ToArray();
            var memscols = await Task.WhenAll(memsTasks);
            var mems = memscols.SelectMany(x => x);

            Debug.WriteLine(mems.Count());

            Assert.IsTrue(mems.Count() > 0);

            await connection.CloseAsync();
        }

        [TestMethod]
        public async Task LoadSitesAsync()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var urls = new string[] {"http://rsdn.ru", "http://rusradio.ru/onlineradio/russianmovie",
                                        "http://rusradio.ru/onlineradio/russianmovie"};
            var tasks = (from url in urls
                    let webRequest = WebRequest.Create(url)
                    select new { Url = url, Response = webRequest.GetResponseAsync() })
                .ToList();
            var data = await Task.WhenAll(tasks.Select(t => t.Response));
            var sb = new StringBuilder();
            foreach (var s in tasks)
            {
                sb.AppendFormat("{0}: {1}, elapsed {2}ms. Thread Id: {3}", s.Url,
                        s.Response.Result.ContentLength,
                        sw.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId)
                    .AppendLine();
            }
            var outputText = sb.ToString();
            Debug.WriteLine("Web request results: {0}", outputText);
        }

        [TestMethod]
        public async Task LoadSites()
        {
            Stopwatch sw = Stopwatch.StartNew();
            string url1 = "http://rsdn.ru";
            string url2 = "http://rusradio.ru/onlineradio/russianmovie";
            string url3 = "http://rusradio.ru/onlineradio/russianmovie";

            var webRequest1 = WebRequest.Create(url1);
            Debug.WriteLine("Before webRequest1.GetResponseAsync(). Thread Id: {0}",
                Thread.CurrentThread.ManagedThreadId);

            var webResponse1 = await webRequest1.GetResponseAsync();
            Debug.WriteLine("{0} : {1}, elapsed {2}ms. Thread Id: {3}", url1,
                webResponse1.ContentLength, sw.ElapsedMilliseconds,
                Thread.CurrentThread.ManagedThreadId);

            var webRequest2 = WebRequest.Create(url2);
            Debug.WriteLine("Before webRequest2.GetResponseAsync(). Thread Id: {0}",
                Thread.CurrentThread.ManagedThreadId);

            var webResponse2 = await webRequest2.GetResponseAsync();
            Debug.WriteLine("{0} : {1}, elapsed {2}ms. Thread Id: {3}", url2,
                webResponse2.ContentLength, sw.ElapsedMilliseconds,
                Thread.CurrentThread.ManagedThreadId);

            var webRequest3 = WebRequest.Create(url3);
            Debug.WriteLine("Before webRequest3.GetResponseAsync(). Thread Id: {0}",
                Thread.CurrentThread.ManagedThreadId);
            var webResponse3 = await webRequest3.GetResponseAsync();
            Debug.WriteLine("{0} : {1}, elapsed {2}ms. Thread Id: {3}", url3,
                webResponse3.ContentLength, sw.ElapsedMilliseconds,
                Thread.CurrentThread.ManagedThreadId);

        }

        static async Task<int> FactorialAsync(int x)
        {
            int result = 1;

            return await Task.Run(() =>
                                  {
                                      for (int i = 1; i <= x; i++)
                                      {
                                          result *= i;
                                      }
                                      Thread.Sleep(3000);
                                      return result;
                                  });
        }

        [TestMethod]
        public async Task DisplayResultAsync()
        {
            int num = 5;
            int result = await FactorialAsync(num);
            Debug.WriteLine("Факториал числа {0} равен {1}", num, result);

            num = 6;
            result = FactorialAsync(num).GetAwaiter().GetResult();
            Debug.WriteLine("Факториал числа {0} равен {1}", num, result);

            result = await Task.Run(() =>
                                    {
                                        int res = 1;
                                        for (int i = 1; i <= 9; i++)
                                        {
                                            res += i * i;
                                        }
                                        return res;
                                    });
            Debug.WriteLine("Сумма квадратов чисел равна {0}", result);
        }

        [TestMethod]
        public async Task DisplayResultParalelAsync()
        {
            int num1 = 5;
            int num2 = 6;
            Task<int> t1 = FactorialAsync(num1);
            Task<int> t2 = FactorialAsync(num2);
            Task<int> t3 = Task.Run(() =>
                                    {
                                        int res = 1;
                                        for (int i = 1; i <= 9; i++)
                                        {
                                            res += i * i;
                                        }
                                        return res;
                                    });

            await Task.WhenAll(new[] { t1, t2, t3 });

            Debug.WriteLine("Факториал числа {0} равен {1}", num1, t1.Result);
            Debug.WriteLine("Факториал числа {0} равен {1}", num2, t2.Result);
            Debug.WriteLine("Сумма квадратов чисел равна {0}", t3.Result);
        }
    }
}
