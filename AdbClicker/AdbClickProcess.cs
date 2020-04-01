using System;

namespace AdbClicker
{
    public class AdbClickProcess : AdbProcess
    {
        private Random m_Random = new Random();
        private Command[] m_Commands = new Command[]
        {
            new Command() { format = COMMAND_ADB_INPUT_TAP, args = new object[] { 604, 595 } }, // 604 x 595
            new Command() { format = COMMAND_ADB_INPUT_TAP, args = new object[] { 582, 441 } }  // 582 x 441
        };
        private double m_NextClickTimestamp;
        private int m_NextClickIndex;

        public AdbClickProcess() :
            base(ADB_PATH, ADB_ARGUMENTS)
        {
        }

        public override void Update()
        {
            base.Update();

            var now = Timestamp.NowUtc;
            if (m_NextClickTimestamp < now)
            {
                m_NextClickTimestamp = now + NextDouble(m_Random, DELAY_MIN, DELAY_MAX);

                var command = m_Commands[m_NextClickIndex];
                SendAdbShellCommand(command.format, command.args);
                m_NextClickIndex = (m_NextClickIndex + 1) % m_Commands.Length;
            }
        }

        static double NextDouble(Random random, double min, double max)
        {
            return min + random.NextDouble() * (max - min);
        }

        private const double DELAY_MIN = 2.0;
        private const double DELAY_MAX = 2.6;
    }
}
