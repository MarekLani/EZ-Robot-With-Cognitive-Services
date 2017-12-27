// This file has been generated in EZ-Builder using the Auto Position SDK Code Creator.

using System;
using EZ_B;
using EZ_B.Classes;

namespace EZFormApplication
{

    public class WavePositions
    {

        EZ_B.AutoPosition _wavePosition;

        public AutoPosition WavePosition
        {
            get
            {
                return _wavePosition;
            }
        }

        public WavePositions(EZ_B.EZB ezb)
        {

            _wavePosition = new AutoPosition(ezb, "My Auto Positions");

            // Add servos
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D16);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D17);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D12);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D13);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D14);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D3);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D2);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D7);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D8);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D4);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D5);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D9);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D6);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D1);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D0);
            _wavePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D18);

            // *******************************************************
            // ** Init Frames                                       **
            // *******************************************************

            // STAND
            _wavePosition.Config.AddFrame("STAND", "e3333875-865c-4b9f-a35f-589ef7b56abb", new int[] { 90, 90, 90, 90, 90, 180, 1, 30, 60, 150, 120, 90, 90, 90, 90, 90 });

            // Wave1
            _wavePosition.Config.AddFrame("Wave1", "ae98a3f5-d354-4c10-aa40-010fc6514e21", new int[] { 90, 90, 90, 90, 90, 13, 2, 27, 58, 161, 119, 90, 90, 65, 97, 90 });

            // Wave2
            _wavePosition.Config.AddFrame("Wave2", "fbf3b358-48dc-4a62-a75c-ebbe7d419279", new int[] { 90, 90, 90, 90, 90, 13, 11, 35, 51, 153, 75, 90, 53, 73, 82, 90 });

            // *******************************************************
            // ** Init Actions                                      **
            // *******************************************************

            // Wave
            _wavePosition.Config.AddAction(
              new AutoPositionAction(
                "Wave",
                "78f5fb25-4eef-4966-97ba-662a9e761b4e",
                true,
                false,
                new AutoPositionActionFrame[] {
            new AutoPositionActionFrame("ae98a3f5-d354-4c10-aa40-010fc6514e21", 5, 7, 0),
            new AutoPositionActionFrame("fbf3b358-48dc-4a62-a75c-ebbe7d419279", 25, 3, -1),
            new AutoPositionActionFrame("ae98a3f5-d354-4c10-aa40-010fc6514e21", 25, 3, -1),
            new AutoPositionActionFrame("fbf3b358-48dc-4a62-a75c-ebbe7d419279", 25, 3, -1),
            new AutoPositionActionFrame("ae98a3f5-d354-4c10-aa40-010fc6514e21", 25, 3, -1),
            new AutoPositionActionFrame("fbf3b358-48dc-4a62-a75c-ebbe7d419279", 25, 3, -1),
            new AutoPositionActionFrame("e3333875-865c-4b9f-a35f-589ef7b56abb", 25, 5, -1),
                },
                AutoPositionAction.ActionTypeEnum.NA));
        }

        public void StartAction_Wave()
        {

            _wavePosition.ExecAction("78f5fb25-4eef-4966-97ba-662a9e761b4e");
        }

        public void Stop()
        {

            _wavePosition.Stop();
        }
    }

    public class GrabPositions
    {

        EZ_B.AutoPosition _grabPosition;

        public AutoPosition AutoPosition
        {
            get
            {
                return _grabPosition;
            }
        }

        public GrabPositions(EZ_B.EZB ezb)
        {

            _grabPosition = new AutoPosition(ezb, "My Auto Positions");

            // Add servos
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D16);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D17);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D12);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D13);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D14);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D3);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D2);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D7);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D8);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D4);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D5);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D9);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D6);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D1);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D0);
            _grabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D18);

            // *******************************************************
            // ** Init Frames                                       **
            // *******************************************************

            // Basic
            _grabPosition.Config.AddFrame("Basic", "b12a5018-1e18-40db-a16c-a02d61e00136", new int[] { 90, 88, 90, 91, 90, 180, 180, 180, 90, 180, 90, 90, 90, 90, 90, 85 });

            // Righthandup
            _grabPosition.Config.AddFrame("Righthandup", "f1992a64-aaff-41a1-8359-196139767375", new int[] { 90, 90, 90, 90, 90, 174, 90, 90, 90, 178, 90, 90, 90, 90, 90, 90 });

            // Righthandmoveforward
            _grabPosition.Config.AddFrame("Righthandmoveforward", "9b2ad226-f9f7-44e4-af3d-f19fe1eb1b78", new int[] { 90, 90, 90, 90, 90, 180, 90, 4, 10, 180, 90, 37, 90, 90, 90, 90 });

            // Righthandgrab
            _grabPosition.Config.AddFrame("Righthandgrab", "731ce137-2d41-4f2b-9c23-894e09219fab", new int[] { 90, 90, 90, 90, 90, 180, 90, 4, 10, 180, 90, 78, 90, 90, 90, 90 });

            // Drink
            _grabPosition.Config.AddFrame("Drink", "8ec774f1-ffe9-4781-b7d5-e8485b297c90", new int[] { 90, 90, 90, 90, 90, 180, 161, 4, 10, 180, 90, 78, 90, 55, 90, 90 });

            // *******************************************************
            // ** Init Actions                                      **
            // *******************************************************

            // Takefood
            _grabPosition.Config.AddAction(
              new AutoPositionAction(
                "Takefood",
                "c710cafd-5456-47d4-86f6-3dae6382799a",
                true,
                false,
                new AutoPositionActionFrame[] {
            new AutoPositionActionFrame("f1992a64-aaff-41a1-8359-196139767375", 25, 3, -1),
            new AutoPositionActionFrame("b12a5018-1e18-40db-a16c-a02d61e00136", 25, 3, -1),
            new AutoPositionActionFrame("9b2ad226-f9f7-44e4-af3d-f19fe1eb1b78", 25, 3, -1),
            new AutoPositionActionFrame("731ce137-2d41-4f2b-9c23-894e09219fab", 125, 3, -1),
            new AutoPositionActionFrame("8ec774f1-ffe9-4781-b7d5-e8485b297c90", 25, 3, -1),
            new AutoPositionActionFrame("731ce137-2d41-4f2b-9c23-894e09219fab", 25, 3, -1),
            new AutoPositionActionFrame("8ec774f1-ffe9-4781-b7d5-e8485b297c90", 25, 3, -1),
            new AutoPositionActionFrame("731ce137-2d41-4f2b-9c23-894e09219fab", 25, 3, -1),
            new AutoPositionActionFrame("8ec774f1-ffe9-4781-b7d5-e8485b297c90", 25, 3, -1),
            new AutoPositionActionFrame("731ce137-2d41-4f2b-9c23-894e09219fab", 25, 3, -1),
            new AutoPositionActionFrame("9b2ad226-f9f7-44e4-af3d-f19fe1eb1b78", 79, 3, -1),
            new AutoPositionActionFrame("b12a5018-1e18-40db-a16c-a02d61e00136", 25, 3, -1),
                },
                AutoPositionAction.ActionTypeEnum.NA));
        }

        public void StartAction_Takefood()
        {

            _grabPosition.ExecAction("c710cafd-5456-47d4-86f6-3dae6382799a");
        }

        public void Stop()
        {

            _grabPosition.Stop();
        }
    }

    public class RightPositions
    {
        EZ_B.AutoPosition _RightPosition;

        public AutoPosition RightPosition
        {
            get
            {
                return _RightPosition;
            }
        }

        public RightPositions(EZ_B.EZB ezb)
        {

            _RightPosition = new AutoPosition(ezb, "My Auto Positions");

            // Add servos
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D16);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D17);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D12);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D13);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D14);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D3);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D2);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D7);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D8);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D4);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D5);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D9);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D6);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D1);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D0);
            _RightPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D18);

            // *******************************************************
            // ** Init Frames                                       **
            // *******************************************************

            // Pause
            _RightPosition.Config.AddFrame("Pause", "PAUSE", new int[] { 47, 46, 48, 46, 42, 13, 2, 53, 53, 48, 50, 46, 52, 157, 82, 113 });

            // STAND
            _RightPosition.Config.AddFrame("STAND", "e3333875-865c-4b9f-a35f-589ef7b56abb", new int[] { 90, 90, 90, 90, 90, 180, 1, 30, 60, 150, 120, 90, 90, 90, 90, 90 });

            // Walk 1
            _RightPosition.Config.AddFrame("Walk 1", "8fa6270a-e145-4db6-b9d0-a13899f685d1", new int[] { 90, 90, 90, 90, 70, 180, 21, 23, 53, 153, 120, 86, -1, -1, -1, 75 });

            // Walk 2
            _RightPosition.Config.AddFrame("Walk 2", "07c517f3-85e7-4f6b-93b7-66a9f119d742", new int[] { 67, 105, 59, 132, 63, 175, 34, 19, 50, 157, 120, 86, -1, -1, -1, 75 });

            // Walk 3
            _RightPosition.Config.AddFrame("Walk 3", "4428bd29-b7a4-4ae0-98ae-30ec4ab8aece", new int[] { 60, 119, 74, 110, 90, 174, 28, 20, 50, 154, 120, 86, -1, -1, -1, 90 });

            // Walk 8
            _RightPosition.Config.AddFrame("Walk 8", "9d5e2ca5-6b0d-4b1b-9fe3-74a4f7d5f8b2", new int[] { 105, 80, 120, 59, 60, 180, 11, 29, 50, 153, 132, -1, -1, -1, -1, 75 });

            // *******************************************************
            // ** Init Actions                                      **
            // *******************************************************

            // Right
            _RightPosition.Config.AddAction(
              new AutoPositionAction(
                "Right",
                "RIGHT",
                false,
                true,
                new AutoPositionActionFrame[] {
            new AutoPositionActionFrame("9d5e2ca5-6b0d-4b1b-9fe3-74a4f7d5f8b2", 45, 2, 1),
            new AutoPositionActionFrame("PAUSE", 155, 3, -1),
            new AutoPositionActionFrame("8fa6270a-e145-4db6-b9d0-a13899f685d1", 45, 2, -1),
            new AutoPositionActionFrame("07c517f3-85e7-4f6b-93b7-66a9f119d742", 45, 2, -1),
            new AutoPositionActionFrame("PAUSE", 105, 3, -1),
            new AutoPositionActionFrame("4428bd29-b7a4-4ae0-98ae-30ec4ab8aece", 45, 2, -1),
            new AutoPositionActionFrame("e3333875-865c-4b9f-a35f-589ef7b56abb", 25, 3, -1),
                },
                AutoPositionAction.ActionTypeEnum.NA));
        }

        public void StartAction_Right()
        {

            _RightPosition.ExecAction("RIGHT");
        }

        public void Stop()
        {

            _RightPosition.Stop();
        }
    }

    public class LeftPositions
    {

        EZ_B.AutoPosition _leftPosition;

        public AutoPosition LeftPosition
        {
            get
            {
                return _leftPosition;
            }
        }

        public LeftPositions(EZ_B.EZB ezb)
        {

            _leftPosition = new AutoPosition(ezb, "My Auto Positions");

            // Add servos
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D16);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D17);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D12);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D13);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D14);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D3);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D2);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D7);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D8);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D4);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D5);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D9);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D6);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D1);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D0);
            _leftPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D18);

            // *******************************************************
            // ** Init Frames                                       **
            // *******************************************************

            // Pause
            _leftPosition.Config.AddFrame("Pause", "PAUSE", new int[] { 47, 46, 48, 46, 42, 13, 2, 53, 53, 48, 50, 46, 52, 157, 82, 113 });

            // STAND
            _leftPosition.Config.AddFrame("STAND", "e3333875-865c-4b9f-a35f-589ef7b56abb", new int[] { 90, 90, 90, 90, 90, 180, 1, 30, 60, 150, 120, 90, 90, 90, 90, 90 });

            // Walk 4
            _leftPosition.Config.AddFrame("Walk 4", "a088300d-fc82-4233-82da-a6322e49b5a0", new int[] { 68, 113, 76, 101, 105, 177, 1, 30, 50, 156, 120, -1, -1, -1, -1, 114 });

            // Walk 5
            _leftPosition.Config.AddFrame("Walk 5", "0010fb5b-31db-46ac-8cf1-90fd650736a7", new int[] { 90, 90, 90, 90, 105, 162, 1, 30, 50, 155, 119, -1, -1, -1, -1, 104 });

            // Walk 6
            _leftPosition.Config.AddFrame("Walk 6", "70858915-cbfc-465a-a7f3-381cd852865b", new int[] { 119, 58, 113, 73, 105, 145, 1, 30, 50, 154, 116, -1, -1, -1, -1, 106 });

            // Walk 7
            _leftPosition.Config.AddFrame("Walk 7", "47a28429-a0f0-4eed-842d-dedf5ce06cab", new int[] { 110, 70, 120, 60, 90, 151, 1, 30, 50, 153, 130, -1, -1, -1, -1, 90 });

            // *******************************************************
            // ** Init Actions                                      **
            // *******************************************************

            // Left
            _leftPosition.Config.AddAction(
              new AutoPositionAction(
                "Left",
                "LEFT",
                false,
                true,
                new AutoPositionActionFrame[] {
            new AutoPositionActionFrame("a088300d-fc82-4233-82da-a6322e49b5a0", 45, 10, 1),
            new AutoPositionActionFrame("PAUSE", 155, 3, -1),
            new AutoPositionActionFrame("0010fb5b-31db-46ac-8cf1-90fd650736a7", 45, 2, -1),
            new AutoPositionActionFrame("70858915-cbfc-465a-a7f3-381cd852865b", 45, 2, -1),
            new AutoPositionActionFrame("PAUSE", 105, 3, -1),
            new AutoPositionActionFrame("47a28429-a0f0-4eed-842d-dedf5ce06cab", 45, 2, -1),
            new AutoPositionActionFrame("e3333875-865c-4b9f-a35f-589ef7b56abb", 25, 3, -1),
                },
                AutoPositionAction.ActionTypeEnum.NA));
        }

        public void StartAction_Left()
        {

            _leftPosition.ExecAction("LEFT");
        }

        public void Stop()
        {

            _leftPosition.Stop();
        }
    }

    public class SquatGrabPositions
    {

        EZ_B.AutoPosition _squatGrabPosition;

        public AutoPosition SquatGrabPosition
        {
            get
            {
                return _squatGrabPosition;
            }
        }

        public SquatGrabPositions(EZ_B.EZB ezb)
        {

            _squatGrabPosition = new AutoPosition(ezb, "My Auto Positions");

            // Add servos
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D16);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D17);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D12);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D13);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D14);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D3);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D2);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D7);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D8);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D4);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D5);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D9);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D6);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D1);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D0);
            _squatGrabPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D18);

            // *******************************************************
            // ** Init Frames                                       **
            // *******************************************************

            // Grab1
            _squatGrabPosition.Config.AddFrame("Grab1", "557a05bd-112b-493b-8eef-94ab7f7ba1d8", new int[] { 150, 56, 32, 128, 90, 114, 28, 18, 90, 153, 104, 90, 48, 85, 86, 90 });

            // Grab2
            _squatGrabPosition.Config.AddFrame("Grab2", "a9f20725-44f1-4c4d-afdb-84b280484e3c", new int[] { 151, 56, 29, 127, 90, 109, 28, 18, 90, 153, 104, 90, 83, 85, 86, 90 });

            // Grab3
            _squatGrabPosition.Config.AddFrame("Grab3", "1dc1a794-5bdb-4706-8488-117db07baa48", new int[] { 90, 90, 90, 90, 90, 54, 28, 18, 90, 153, 104, 90, 83, 85, 86, 90 });

            // Grab4
            _squatGrabPosition.Config.AddFrame("Grab4", "4d96e2cf-8c6e-4302-9bda-f3d484a52f12", new int[] { 90, 90, 90, 90, 90, 121, 28, 18, 90, 153, 104, 90, 83, 85, 86, 90 });

            // Pause
            _squatGrabPosition.Config.AddFrame("Pause", "PAUSE", new int[] { 47, 46, 48, 46, 42, 13, 2, 53, 53, 48, 50, 46, 52, 157, 82, 113 });

            // STAND
            _squatGrabPosition.Config.AddFrame("STAND", "e3333875-865c-4b9f-a35f-589ef7b56abb", new int[] { 90, 90, 90, 90, 90, 180, 1, 30, 60, 150, 120, 90, 90, 90, 90, 90 });

            // *******************************************************
            // ** Init Actions                                      **
            // *******************************************************

            // Grab
            _squatGrabPosition.Config.AddAction(
              new AutoPositionAction(
                "Grab",
                "1f60fbf3-4e1a-44e2-9f5f-acfc31fe0c31",
                true,
                false,
                new AutoPositionActionFrame[] {
            new AutoPositionActionFrame("557a05bd-112b-493b-8eef-94ab7f7ba1d8", 75, 10, 1),
            new AutoPositionActionFrame("PAUSE", 345, 3, -1),
            new AutoPositionActionFrame("a9f20725-44f1-4c4d-afdb-84b280484e3c", 75, 6, -1),
            new AutoPositionActionFrame("PAUSE", 345, 3, -1),
            new AutoPositionActionFrame("1dc1a794-5bdb-4706-8488-117db07baa48", 25, 3, -1),
            new AutoPositionActionFrame("4d96e2cf-8c6e-4302-9bda-f3d484a52f12", 25, 3, -1),
            new AutoPositionActionFrame("1dc1a794-5bdb-4706-8488-117db07baa48", 25, 3, -1),
            new AutoPositionActionFrame("4d96e2cf-8c6e-4302-9bda-f3d484a52f12", 25, 3, -1),
            new AutoPositionActionFrame("1dc1a794-5bdb-4706-8488-117db07baa48", 25, 3, -1),
            new AutoPositionActionFrame("4d96e2cf-8c6e-4302-9bda-f3d484a52f12", 25, 3, -1),
            new AutoPositionActionFrame("1dc1a794-5bdb-4706-8488-117db07baa48", 25, 3, -1),
            new AutoPositionActionFrame("a9f20725-44f1-4c4d-afdb-84b280484e3c", 75, 6, -1),
            new AutoPositionActionFrame("PAUSE", 345, 3, -1),
            new AutoPositionActionFrame("557a05bd-112b-493b-8eef-94ab7f7ba1d8", 75, 6, -1),
            new AutoPositionActionFrame("PAUSE", 315, 3, -1),
            new AutoPositionActionFrame("e3333875-865c-4b9f-a35f-589ef7b56abb", 25, 3, -1),
                },
                AutoPositionAction.ActionTypeEnum.NA));
        }

        public void StartAction_Grab()
        {

            _squatGrabPosition.ExecAction("1f60fbf3-4e1a-44e2-9f5f-acfc31fe0c31");
        }

        public void Stop()
        {

            _squatGrabPosition.Stop();
        }
    }

    public class ForwardPositions
    {

        EZ_B.AutoPosition _forwardPosition;

        public AutoPosition ForwardPosition
        {
            get
            {
                return _forwardPosition;
            }
        }

        public ForwardPositions(EZ_B.EZB ezb)
        {

            _forwardPosition = new AutoPosition(ezb, "My Auto Positions");

            // Add servos
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D16);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D17);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D12);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D13);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D14);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D3);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D2);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D7);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D8);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D4);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D5);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D9);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D6);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D1);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D0);
            _forwardPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D18);

            // *******************************************************
            // ** Init Frames                                       **
            // *******************************************************

            // Walk 1
            _forwardPosition.Config.AddFrame("Walk 1", "8fa6270a-e145-4db6-b9d0-a13899f685d1", new int[] { 90, 90, 90, 90, 70, 180, 21, 23, 53, 153, 120, 86, -1, -1, -1, 75 });

            // Walk 2
            _forwardPosition.Config.AddFrame("Walk 2", "07c517f3-85e7-4f6b-93b7-66a9f119d742", new int[] { 67, 105, 59, 132, 63, 175, 34, 19, 50, 157, 120, 86, -1, -1, -1, 75 });

            // Walk 3
            _forwardPosition.Config.AddFrame("Walk 3", "4428bd29-b7a4-4ae0-98ae-30ec4ab8aece", new int[] { 60, 119, 74, 110, 90, 174, 28, 20, 50, 154, 120, 86, -1, -1, -1, 90 });

            // Walk 4
            _forwardPosition.Config.AddFrame("Walk 4", "a088300d-fc82-4233-82da-a6322e49b5a0", new int[] { 68, 113, 76, 101, 105, 177, 1, 30, 50, 156, 120, -1, -1, -1, -1, 114 });

            // Walk 5
            _forwardPosition.Config.AddFrame("Walk 5", "0010fb5b-31db-46ac-8cf1-90fd650736a7", new int[] { 90, 90, 90, 90, 105, 162, 1, 30, 50, 155, 119, -1, -1, -1, -1, 104 });

            // Walk 6
            _forwardPosition.Config.AddFrame("Walk 6", "70858915-cbfc-465a-a7f3-381cd852865b", new int[] { 119, 58, 113, 73, 105, 145, 1, 30, 50, 154, 116, -1, -1, -1, -1, 106 });

            // Walk 7
            _forwardPosition.Config.AddFrame("Walk 7", "47a28429-a0f0-4eed-842d-dedf5ce06cab", new int[] { 110, 70, 120, 60, 90, 151, 1, 30, 50, 153, 130, -1, -1, -1, -1, 90 });

            // Walk 8
            _forwardPosition.Config.AddFrame("Walk 8", "9d5e2ca5-6b0d-4b1b-9fe3-74a4f7d5f8b2", new int[] { 105, 80, 120, 59, 60, 180, 11, 29, 50, 153, 132, -1, -1, -1, -1, 75 });

            // *******************************************************
            // ** Init Actions                                      **
            // *******************************************************

            // Forward
            _forwardPosition.Config.AddAction(
              new AutoPositionAction(
                "Forward",
                "FORWARD",
                false,
                true,
                new AutoPositionActionFrame[] {
            new AutoPositionActionFrame("8fa6270a-e145-4db6-b9d0-a13899f685d1", 45, 2, 1),
            new AutoPositionActionFrame("07c517f3-85e7-4f6b-93b7-66a9f119d742", 45, 2, -1),
            new AutoPositionActionFrame("4428bd29-b7a4-4ae0-98ae-30ec4ab8aece", 45, 2, -1),
            new AutoPositionActionFrame("a088300d-fc82-4233-82da-a6322e49b5a0", 45, 2, -1),
            new AutoPositionActionFrame("0010fb5b-31db-46ac-8cf1-90fd650736a7", 45, 2, -1),
            new AutoPositionActionFrame("70858915-cbfc-465a-a7f3-381cd852865b", 45, 2, -1),
            new AutoPositionActionFrame("47a28429-a0f0-4eed-842d-dedf5ce06cab", 45, 2, -1),
            new AutoPositionActionFrame("9d5e2ca5-6b0d-4b1b-9fe3-74a4f7d5f8b2", 45, 2, -1),
                },
                AutoPositionAction.ActionTypeEnum.NA));
        }

        public void StartAction_Forward()
        {

            _forwardPosition.ExecAction("FORWARD");
        }

        public void Stop()
        {

            _forwardPosition.Stop();
        }
    }

    public class ReversePositions
    {

        EZ_B.AutoPosition _reversePosition;

        public AutoPosition ReversePosition
        {
            get
            {
                return _reversePosition;
            }
        }

        public ReversePositions(EZ_B.EZB ezb)
        {

            _reversePosition = new AutoPosition(ezb, "My Auto Positions");

            // Add servos
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D16);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D17);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D12);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D13);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D14);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D3);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D2);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D7);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D8);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D4);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D5);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D9);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D6);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D1);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D0);
            _reversePosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D18);

            // *******************************************************
            // ** Init Frames                                       **
            // *******************************************************

            // Walk 1
            _reversePosition.Config.AddFrame("Walk 1", "8fa6270a-e145-4db6-b9d0-a13899f685d1", new int[] { 90, 90, 90, 90, 70, 180, 21, 23, 53, 153, 120, 86, -1, -1, -1, 75 });

            // Walk 2
            _reversePosition.Config.AddFrame("Walk 2", "07c517f3-85e7-4f6b-93b7-66a9f119d742", new int[] { 67, 105, 59, 132, 63, 175, 34, 19, 50, 157, 120, 86, -1, -1, -1, 75 });

            // Walk 3
            _reversePosition.Config.AddFrame("Walk 3", "4428bd29-b7a4-4ae0-98ae-30ec4ab8aece", new int[] { 60, 119, 74, 110, 90, 174, 28, 20, 50, 154, 120, 86, -1, -1, -1, 90 });

            // Walk 4
            _reversePosition.Config.AddFrame("Walk 4", "a088300d-fc82-4233-82da-a6322e49b5a0", new int[] { 68, 113, 76, 101, 105, 177, 1, 30, 50, 156, 120, -1, -1, -1, -1, 114 });

            // Walk 5
            _reversePosition.Config.AddFrame("Walk 5", "0010fb5b-31db-46ac-8cf1-90fd650736a7", new int[] { 90, 90, 90, 90, 105, 162, 1, 30, 50, 155, 119, -1, -1, -1, -1, 104 });

            // Walk 6
            _reversePosition.Config.AddFrame("Walk 6", "70858915-cbfc-465a-a7f3-381cd852865b", new int[] { 119, 58, 113, 73, 105, 145, 1, 30, 50, 154, 116, -1, -1, -1, -1, 106 });

            // Walk 7
            _reversePosition.Config.AddFrame("Walk 7", "47a28429-a0f0-4eed-842d-dedf5ce06cab", new int[] { 110, 70, 120, 60, 90, 151, 1, 30, 50, 153, 130, -1, -1, -1, -1, 90 });

            // Walk 8
            _reversePosition.Config.AddFrame("Walk 8", "9d5e2ca5-6b0d-4b1b-9fe3-74a4f7d5f8b2", new int[] { 105, 80, 120, 59, 60, 180, 11, 29, 50, 153, 132, -1, -1, -1, -1, 75 });

            // *******************************************************
            // ** Init Actions                                      **
            // *******************************************************

            // Reverse
            _reversePosition.Config.AddAction(
              new AutoPositionAction(
                "Reverse",
                "REVERSE",
                false,
                true,
                new AutoPositionActionFrame[] {
            new AutoPositionActionFrame("9d5e2ca5-6b0d-4b1b-9fe3-74a4f7d5f8b2", 45, 2, 1),
            new AutoPositionActionFrame("47a28429-a0f0-4eed-842d-dedf5ce06cab", 45, 2, -1),
            new AutoPositionActionFrame("70858915-cbfc-465a-a7f3-381cd852865b", 45, 2, -1),
            new AutoPositionActionFrame("0010fb5b-31db-46ac-8cf1-90fd650736a7", 45, 2, -1),
            new AutoPositionActionFrame("a088300d-fc82-4233-82da-a6322e49b5a0", 45, 2, -1),
            new AutoPositionActionFrame("4428bd29-b7a4-4ae0-98ae-30ec4ab8aece", 45, 2, -1),
            new AutoPositionActionFrame("07c517f3-85e7-4f6b-93b7-66a9f119d742", 45, 2, -1),
            new AutoPositionActionFrame("8fa6270a-e145-4db6-b9d0-a13899f685d1", 45, 2, -1),
                },
                AutoPositionAction.ActionTypeEnum.NA));
        }




        public void StartAction_Reverse()
        {

            _reversePosition.ExecAction("REVERSE");
        }

        public void Stop()
        {

            _reversePosition.Stop();
        }
    }


    public class LeftHandPositions
    {

        EZ_B.AutoPosition _leftHandPosition;

        public AutoPosition LeftHandPosition
        {
            get
            {
                return _leftHandPosition;
            }
        }

        public LeftHandPositions(EZ_B.EZB ezb)
        {

            _leftHandPosition = new AutoPosition(ezb, "My LeftHand Positions");

            // Add servos
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D16);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D17);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D12);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D13);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D14);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D3);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D2);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D7);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D8);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D4);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D5);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D9);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D6);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D1);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D0);
            _leftHandPosition.Config.AddServo(EZ_B.Servo.ServoPortEnum.D18);

            // *******************************************************
            // ** Init Frames                                       **
            // *******************************************************

            // Lefthandup
            _leftHandPosition.Config.AddFrame("Lefthandup", "a666fd3d-e268-4687-ae22-b8fa2d302f31", new int[] { 85, 85, 85, 85, 85, 88, 10, 10, 85, 86, 85, 85, 85, 85, 85, 85 });

            // Lefthanddown
            _leftHandPosition.Config.AddFrame("Lefthanddown", "e12aef86-3342-48fb-a669-9d001d5ef7c3", new int[] { 85, 85, 85, 85, 85, 170, 10, 10, 85, 170, 85, 85, 85, 85, 85, 85 });

            // *******************************************************
            // ** Init Actions                                      **
            // *******************************************************

            // Lefthanddown
            _leftHandPosition.Config.AddAction(
              new AutoPositionAction(
                "Lefthanddown",
                "cad0fca2-9143-4461-a450-b918ce2f350c",
                true,
                false,
                new AutoPositionActionFrame[] {
            new AutoPositionActionFrame("e12aef86-3342-48fb-a669-9d001d5ef7c3", 25, 3, -1),
                },
                AutoPositionAction.ActionTypeEnum.NA));

            // Lefthandup
            _leftHandPosition.Config.AddAction(
              new AutoPositionAction(
                "Lefthandup",
                "74a1b571-1945-4ebb-ae2e-44ff634c544d",
                true,
                false,
                new AutoPositionActionFrame[] {
            new AutoPositionActionFrame("a666fd3d-e268-4687-ae22-b8fa2d302f31", 25, 3, -1),
                },
                AutoPositionAction.ActionTypeEnum.NA));
        }

        public void StartAction_Lefthanddown()
        {

            _leftHandPosition.ExecAction("cad0fca2-9143-4461-a450-b918ce2f350c");
        }

        public void StartAction_Lefthandup()
        {

            _leftHandPosition.ExecAction("74a1b571-1945-4ebb-ae2e-44ff634c544d");
        }

        public void Stop()
        {

            _leftHandPosition.Stop();
        }
    }
}
