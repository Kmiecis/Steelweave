using System;
using System.Threading;

namespace AdbClicker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Run();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught: " + e);
            }
            Console.ReadKey();
        }

        private static AdbClickProcess m_AdbClickProcess;
        private static AdbOnTouchProcess m_AdbOnTouchProcess;

        static void Run()
        {
            m_AdbOnTouchProcess = new AdbOnTouchProcess();
            m_AdbClickProcess = new AdbClickProcess();

            m_AdbOnTouchProcess.Start();
            m_AdbClickProcess.Start();

            Thread.Sleep(THREAD_DELAY);

            while (true)
            {
                m_AdbOnTouchProcess.Update();

                if (!m_AdbOnTouchProcess.HasTouchedRecently)
                {
                    m_AdbClickProcess.Update();
                }
            }
        }

        public const int THREAD_DELAY = 2000;
    }
}
