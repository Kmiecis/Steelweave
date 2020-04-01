using System.Diagnostics;

namespace AdbClicker
{
    public class AdbOnTouchProcess : AdbProcess
    {
        private double m_OnTouchReceivedEnd = ON_TOUCH_DELAY;
        private Command m_Command = new Command() { format = COMMAND_ADB_GETEVENT_TAP };

        public bool HasTouchedRecently
        {
            get { return Timestamp.NowUtc < m_OnTouchReceivedEnd; }
        }

        public AdbOnTouchProcess() :
            base(ADB_PATH, ADB_ARGUMENTS)
        {
        }

        public override void Start()
        {
            base.Start();
            SendAdbShellCommand(m_Command.format, m_Command.args);
        }

        public override void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            m_OnTouchReceivedEnd = Timestamp.NowUtc + ON_TOUCH_DELAY;
        }

        public const int ON_TOUCH_DELAY = 5;
    }
}
