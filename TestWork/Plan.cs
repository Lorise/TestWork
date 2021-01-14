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

        public Plan()
        {
            if (!_data.Read())
                Console.WriteLine("Error data read");

            SetupMachines();
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

                Machine machine = _machines.Find((machine1 => machine1.GeneralWokrTime == _machines.Min(m => m.GeneralWokrTime)));

                try
                {
                    TimeSpan time = _data.Times.Find((time1 => 
                        time1.NomenclatureId == nomenclature.Id && 
                        time1.MachineToolId == machine.Id))
                        .OperationTime;
                    machine.AddWork(party.Id, nomenclature.Nomenclature_, time);
                }
                catch (Exception e)
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

                Console.WriteLine($"Tool: {machine.Name}, General time: {machine.GeneralWokrTime}");

                for (int i = 0; i < machine.WorksTime.Count; i++)
                {
                    workInfos.Add(new WorkInfo(machine.PartyId[i], machine.nomenclatureNames[i], machine.Name, startTime, startTime + machine.WorksTime[i]));
                    
                    Console.WriteLine(workInfos.Last());

                    startTime += machine.WorksTime[i];
                }
            }

            workInfos.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            workInfos.Sort((x, y) => x.Party.CompareTo(y.Party));

            return workInfos;
        }
    }
}
