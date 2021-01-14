namespace TestWork.Data
{
    class Nomenclature
    {
        public int Id { get; }
        public string Nomenclature_ { get;}

        public const int StartDataRow = 2;
        public const int Columns = 2;
        public const string Column1Name = "id";
        public const string Column2Name = "nomenclature";

        public Nomenclature(int id, string nomenclature)
        {
            Id = id;
            Nomenclature_ = nomenclature;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nomenclature: {Nomenclature_}";
        }
    }
}
