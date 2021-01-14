using System;
using System.Collections.Generic;

namespace TestWork
{
    class Machine
    {
        public int Id { get; }
        public string Name { get; }

        public TimeSpan GeneralWokrTime { get; private set; }

        public List<int> PartyId { get; } = new List<int>();
        public List<string> nomenclatureNames { get; } = new List<string>();
        public List<TimeSpan> WorksTime { get; } = new List<TimeSpan>();

        public Machine(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddWork(int partyId, string nomenclatureName, TimeSpan workTime)
        {
            PartyId.Add(partyId);

            nomenclatureNames.Add(nomenclatureName);

            GeneralWokrTime += workTime;
            WorksTime.Add(workTime);
        }
    }
}
