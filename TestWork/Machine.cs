using System;
using System.Collections.Generic;

namespace TestWork
{
    class Machine
    {
        public int Id { get; }
        public string Name { get; }

        public TimeSpan GeneralWorkTime { get; private set; }

        public List<int> PartyIdList { get; } = new List<int>();
        public List<string> NomenclaturesList { get; } = new List<string>();
        public List<TimeSpan> WorkTimeList { get; } = new List<TimeSpan>();

        public Machine(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddWork(int partyId, string nomenclatureName, TimeSpan workTime)
        {
            PartyIdList.Add(partyId);

            NomenclaturesList.Add(nomenclatureName);

            GeneralWorkTime += workTime;
            WorkTimeList.Add(workTime);
        }
    }
}
