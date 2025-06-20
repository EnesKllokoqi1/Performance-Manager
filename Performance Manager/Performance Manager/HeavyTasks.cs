using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Performance_Manager
{
    internal class HeavyTasks
    {
        public async Task<int> CalculatingStuff()
        {
            int a = await Task.Run(() =>
            {
                Thread.Sleep(12000);
                return 432034;
            });
            return a;
        }
        public async   Task<int> Calculate()
        {
            int b = await Task.Run(() =>
            {
                Thread.Sleep(9000);
                return 320012;
            });
            return b;
        }
        public async Task<int> Calculate2()
        {
            int c = await Task.Run(() =>
            {
                Thread.Sleep(5000);
                return 213200;
            });
            return c;
        }
        public async Task<int> Calculate3()
        {
            int d = await Task.Run(() =>
            {
                Thread.Sleep(3200);
                return 143299;
            });
            return d;
        }
    }
}
