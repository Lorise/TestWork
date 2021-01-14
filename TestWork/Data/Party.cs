namespace TestWork.Data
{
    class Party
    {
        public int Id { get; }
        public int NomenclatureId { get;}

        public const int StartDataRow = 2;
        public const int Columns = 2;
        public const string Column1Name = "id";
        public const string Column2Name = "nomenclature id";

        public Party(int id, int nomenclatureId)
        {
            Id = id;
            NomenclatureId = nomenclatureId;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nomenclature ID: {NomenclatureId}";
        }
    }
}
