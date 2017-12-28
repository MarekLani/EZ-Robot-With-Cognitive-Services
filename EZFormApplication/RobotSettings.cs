using EZ_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZFormApplication
{
    class RobotSettings
    {
        public struct ServoLimits
        {
            public readonly int MaxPosition;
            public readonly int CenterPosition;
            public readonly int MinPosition;

            public ServoLimits(int min, int max)
            {
                this.MinPosition = min;
                this.MaxPosition = max;
                this.CenterPosition = (max - min) / 2 + min;
            }
        }

        public static readonly Dictionary<Servo.ServoPortEnum, ServoLimits> mapPortToServoLimits = new Dictionary<Servo.ServoPortEnum, ServoLimits>()
        {
            {Servo.ServoPortEnum.D0, new ServoLimits(5, 176)}, //Head Horizontal
            {Servo.ServoPortEnum.D1, new ServoLimits(70, 176)}, //Head Vertical 
            {Servo.ServoPortEnum.D2, new ServoLimits(5, 176)}, //Shoulder
            {Servo.ServoPortEnum.D5, new ServoLimits(5, 176)} //Palm
        };

        public const Servo.ServoPortEnum HeadServoHorizontalPort = Servo.ServoPortEnum.D0;
        public const Servo.ServoPortEnum HeadServoVerticalPort = Servo.ServoPortEnum.D1;
        public const Servo.ServoPortEnum RightShoulderServoPort = Servo.ServoPortEnum.D2;
        public const Servo.ServoPortEnum RightPalmPort = Servo.ServoPortEnum.D5;

        public const int CameraWidth = 320;
        public const int CameraHeight = 240;
        public const int ServoStepValue = 1;
        public const int ServoSpeed = 15;
        public const int YDiffMargin = CameraWidth / 80;
        public const int XDiffMargin = CameraHeight / 80;
        public static int sensitivity = 0;
        public static int audioLevel = 0;
    }
}
