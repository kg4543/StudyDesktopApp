using System;

namespace IoTSensorMonApp
{
    internal class SensorData
    {
        public DateTime Current { get; set; } // 현 시간

        public int Value { get; set; } // 센서 값

        public bool SimulFlag { get; set; } //시뮬레이션 여부

        public SensorData(DateTime current, int value,bool simulFlag)
        {
            Current = current;
            Value = value;
            SimulFlag = simulFlag;
        }
    }
}
