# LogService
基于C#的日志记录组件，该组件采用多线程写日志队列，单线程写日志文件的模式记录日志。平均每秒写入20万条记录。


# 功能介绍

 1.多类型日志自动归类，将不同类型的日志写入不同的文件中。例如：登录日志，操作日志，错误日志等
 2.批量写入日志，减少IO操作 
 3.延时写入功能，当日志缓存区大小小于某个阈值时不写入
 4.强制写入功能，当日志在批处理列表中保留时间超过某个阈值时强制写入
 5.日志分割(按天，按体积分割)
 6.报警机制，当写入日志出错，并且连续出错10次(可配置)，可发送报警短信(需自己实现)
 
 详情请参照：http://www.cnblogs.com/eggTwo/p/6394028.html 
 
 # 测试代码如下
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
