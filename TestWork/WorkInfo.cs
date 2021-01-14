using System;

namespace TestWork
{
    class WorkInfo
    {
        public int Party { get; }
        public string Nomenclature { get; }
        public string MachineTool { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public WorkInfo(int party, string nomenclature, string machineTool, DateTime startTime, DateTime endTime)
        {
            Party = party;
            Nomenclature = nomenclature;
            MachineTool = machineTool;
            StartTime = startTime;
            EndTime = endTime;
        }

        public override string ToString()
        {
            return $"Партия: {Party}, Номенклатура: {Nomenclature}, Машина: {MachineTool}, Время старт: {StartTime}, Время конец: {EndTime}";
        }

        public string ToExcel()
        {
            return $"{Party}, {Nomenclature}, {MachineTool}, {StartTime}, {EndTime}";
        }
    }
}
