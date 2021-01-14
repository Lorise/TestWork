using System;

namespace TestWork.Data
{
    class Time
    {
        public int MachineToolId { get; set; }
        public int NomenclatureId { get; set; }
        public TimeSpan OperationTime { get; set; }

        public const int StartDataRow = 2;
        public const int Columns = 3;
        public const string Column1Name = "machine tool id";
        public const string Column2Name = "nomenclature id";
        public const string Column3Name = "operation time";

        public Time(int machineToolId, int nomenclatureId, TimeSpan operationTime)
        {
            MachineToolId = machineToolId;
            NomenclatureId = nomenclatureId;
            OperationTime = operationTime;
        }

        public override string ToString()
        {
            return $"Machine tool ID: {MachineToolId}, Nomenclature ID: {NomenclatureId}, Operation time: {OperationTime}";
        }
    }
}
