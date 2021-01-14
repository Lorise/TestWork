namespace TestWork.Data
{
    class MachineTool
    {
        public int Id { get; }
        public string Name { get; }

        public const int StartDataRow = 2;
        public const int Columns = 2;
        public const string Column1Name = "id";
        public const string Column2Name = "name";
        
        public MachineTool(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }
}
