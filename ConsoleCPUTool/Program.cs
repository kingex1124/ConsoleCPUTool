using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleCPUTool
{
    class Program
    {
        static PerformanceCounter cpu = new PerformanceCounter(
            "Processor", "% Processor Time", "_Total");
        static PerformanceCounter memory = new PerformanceCounter(
            "Memory", "% Committed Bytes in Use");
        static void Main(string[] args)
        {

            Process testProcess = new Process();
            testProcess.StartInfo.FileName = @"D:\011714\study\ConsoleSocketTest\ConsoleSocketTest\bin\Debug\ConsoleSocketTest.exe";

            testProcess.Start();

            Thread.Sleep(1000);

            // 透過名稱關閉執行續
            //Process[] closeProcesses = Process.GetProcessesByName("ConsoleSocketTest");

            //if (closeProcesses.Length != 0)
            //    closeProcesses[0].Kill();
          
            // 列出系統中所有的程序
            Process[] processes = Process.GetProcesses();

            // 可以取得所有執行續的名稱
            var category = new PerformanceCounterCategory("Process");
            var instances = category.GetInstanceNames();
         

            // 依照名稱排序執行續
            processes = processes.OrderBy(o => o.ProcessName).ToArray();

            foreach (var processItem in processes)
            {
                // 可判斷哪隻執行續的名稱
                if (processItem.ProcessName == "ConsoleSocketTest")
                {
                    Console.WriteLine("Rrocess: {0}", processItem.ProcessName);
                    // 可以關閉該執行續
                    processItem.Kill();
                }
            }

            // 監控CPU 記憶體
            while (true)
            {
                Console.WriteLine("CPU: {0:n1}%", cpu.NextValue());
                Console.WriteLine("Memory: {0:n0}%", memory.NextValue());
                Thread.Sleep(1000);
            }
        }
    }
}
