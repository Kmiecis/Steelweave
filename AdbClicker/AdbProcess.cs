using System;
using System.Diagnostics;

namespace AdbClicker
{
    public class AdbProcess
    {
        private Process m_Process;

        public AdbProcess(string fileName, string arguments = null)
        {
            m_Process = StartProcess(fileName, arguments);
        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            var data = e.Data;
            if (!string.IsNullOrEmpty(data))
            {
                Console.WriteLine(data);
            }
        }

        public virtual void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            var data = e.Data;
            if (!string.IsNullOrEmpty(data))
            {
                Console.WriteLine(data);
            }
        }

        protected void SendAdbShellCommand(string format, params object[] args)
        {
            m_Process.StandardInput.WriteLine(string.Format(format, args));
        }

        Process StartProcess(string fileName, string arguments = null)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(fileName, arguments);
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.OutputDataReceived += OnOutputDataReceived;
            process.ErrorDataReceived += OnErrorDataReceived;
            process.Start();
            process.BeginOutputReadLine();

            return process;
        }

        static void Connect(Process process)
        {
            SendAdbCommand(process, COMMAND_ADB_TCPIP, PORT);
            SendAdbCommand(process, COMMAND_ADB_CONNECT, IP_ADDRESS);
        }

        static void Disconnect(Process process)
        {
            SendAdbCommand(process, COMMAND_ADB_KILLSERVER);
        }

        static void SendAdbCommand(Process process, string format, params object[] args)
        {
            process.StandardInput.WriteLine(ADB_PATH + string.Format(format, args));
        }

        ~AdbProcess()
        {
            if (m_Process != null) {
                m_Process.Close();
            }
        }

        public const int SCREEN_HEIGHT = 720;
        public const int SCREEN_WIDTH = 1280;
        public const float SCREEN_HEIGHT_CM = 6.2f;
        public const float SCREEN_WIDTH_CM = 11.0f;

        public const string COMMAND_ADB_KILLSERVER = " kill-server";
        public const string COMMAND_ADB_TCPIP = " tcpip {0}";
        public const string COMMAND_ADB_CONNECT = " connect {0}";
        public const string COMMAND_ADB_INPUT_TAP = " input tap {0} {1}";
        public const string COMMAND_ADB_SHELL_INPUT_TAP = " shell input tap {0} {1}";
        public const string COMMAND_ADB_SHELL_GETEVENT_TAP = " shell -- getevent -lt /dev/input/event9";
        public const string COMMAND_ADB_GETEVENT_TAP = " getevent -lt /dev/input/event9";

        public const string CMD_PATH = "C:/Windows/System32/cmd.exe";
        public const string ADB_PATH = "C:/Users/Kmiecik/AppData/Local/Android/Sdk/platform-tools/adb.exe";
        public const string ADB_ARGUMENTS = "shell";

        public const string PORT = "5555";
        public const string IP_ADDRESS = "192.168.0.27";
    }
}
