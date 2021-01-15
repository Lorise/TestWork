using System;
using System.Collections.Generic;
using System.Linq;
using TestWork.Data;

namespace TestWork
{
    class Plan
    {
        private readonly Data.Data _data = new Data.Data();
        private readonly List<Machine> _machines = new List<Machine>();

        public bool Load()
        {
            if (!_data.Read())
            {
                Console.WriteLine("Error data read");
                return false;
            }

            if (!_data.CheckHarmony())
            {
                Console.WriteLine("Error CheckHarmony");
                return false;
            }

            SetupMachines();

            return true;
        }

        public void SetupMachines()
        {
            foreach (MachineTool machineTool in _data.MachineTools)
            {
                _machines.Add(new Machine(machineTool.Id, machineTool.Name)); 
            }
        }

        public void Calculate()
        {
            foreach (Party party in _data.Parties)
            {
                Nomenclature nomenclature = _data.Nomenclatures.Find((nomenclature1 => nomenclature1.Id == party.NomenclatureId));

                Machine machine = _machines.Find((machine1 => machine1.GeneralWorkTime == _machines.Min(m => m.GeneralWorkTime)));

                try
                {
                    TimeSpan time = _data.Times.Find((time1 => 
                        time1.NomenclatureId == nomenclature.Id && 
                        time1.MachineToolId == machine.Id))
                        .OperationTime;
                    machine.AddWork(party.Id, nomenclature.Nomenclature_, time);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Нет подходящей печи. Nomencloture: {nomenclature.Id}, MachineID: {machine.Id}");
                }
            }
        }

        public List<WorkInfo> GetResult()
        {
            List<WorkInfo> workInfos = new List<WorkInfo>();

            DateTime nowTime = DateTime.Now;

            foreach (Machine machine in _machines)
            {
                DateTime startTime = nowTime;

                Console.WriteLine($"Tool: {machine.Name}, General time: {machine.GeneralWorkTime}");

                for (int i = 0; i < machine.WorkTimeList.Count; i++)
                {
                    workInfos.Add(new WorkInfo(machine.PartyIdList[i], machine.NomenclaturesList[i], machine.Name, startTime, startTime + machine.WorkTimeList[i]));
                    
                    Console.WriteLine(workInfos.Last());

                    startTime += machine.WorkTimeList[i];
                }
            }

            workInfos.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            workInfos.Sort((x, y) => x.Party.CompareTo(y.Party));

            return workInfos;
        }
    }
}
