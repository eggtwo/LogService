
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogService
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log/");
            LogServiceHelper.Intance.Start(dir);
            LogWriteTest();
        }
        static void LogWriteTest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 5; i++)
            {
                var _thread = new Thread(Start);
                _thread.IsBackground = true;
                _thread.Start(i);
            }
            while (LogServiceHelper.Intance.GetQueueCount() > 0)
            {

            }
            sw.Stop();
            LogServiceHelper.Intance.Stop();
            long s = sw.ElapsedMilliseconds / 1000;
            Console.WriteLine("共耗时："+s+"秒");
            Console.ReadLine();
          
        }
        static void Start(object tag)
        {
            for (int i = 0; i < 200000; i++)
            {
                LogServiceHelper.Intance.Write("测试" + tag, i + "测试测试测试测试测试", "sa");
            }
        }
    }
}
