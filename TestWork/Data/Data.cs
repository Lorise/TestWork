using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace TestWork.Data
{
    class Data
    {
        public List<MachineTool> MachineTools { get; } = new List<MachineTool>();
        public List<Nomenclature> Nomenclatures { get; } = new List<Nomenclature>();
        public List<Party> Parties { get; } = new List<Party>();
        public List<Time> Times { get; } = new List<Time>();

        public bool Read()
        {
            return ReadDataMachineTools() && ReadDataNomenclatures() && ReadDataParties() && ReadDataTimes();
        }

        public bool CheckHarmony()
        {
            bool check = true;

            foreach (Time time in Times)
            {
                if (!MachineTools.Exists((tool => tool.Id == time.MachineToolId)))
                {
                    Console.WriteLine($"MachineTool ID not exist: {time.MachineToolId}");

                    check = false;
                }

                if (!Nomenclatures.Exists((nomenclature => nomenclature.Id == time.NomenclatureId)))
                {
                    Console.WriteLine($"Nomenclature ID not exist: {time.NomenclatureId}");

                    check = false;
                }
            }

            foreach (Party party in Parties)
            {
                if (!Nomenclatures.Exists((nomenclature => nomenclature.Id == party.NomenclatureId)))
                {
                    Console.WriteLine($"Nomenclature ID not exist: {party.NomenclatureId}");

                    check = false;
                }
            }

            return check;
        }

        private bool ReadDataMachineTools()
        {
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(PathData.MachineTools)))
            {
                ExcelWorksheet myWorksheet = xlPackage.Workbook.Worksheets.First();
                int totalRows = myWorksheet.Dimension.End.Row;
                int totalColumns = myWorksheet.Dimension.End.Column;

                for (int row = MachineTool.StartDataRow; row <= totalRows; row++)
                {
                    string strId = myWorksheet.GetValue(row, 1).ToString();
                    string strName = myWorksheet.GetValue(row, 2).ToString();

                    if (!CheckId(strId))
                        return false;

                    int id = int.Parse(strId);
                    string name = strName;

                    if (MachineTools.Exists((machineTool => machineTool.Id == id)))
                        return false;

                    MachineTools.Add(new MachineTool(id, name));
                }
            }

            return true;
        }

        private bool ReadDataNomenclatures()
        {
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(PathData.Nomenclatures)))
            {
                ExcelWorksheet myWorksheet = xlPackage.Workbook.Worksheets.First();
                int totalRows = myWorksheet.Dimension.End.Row;
                int totalColumns = myWorksheet.Dimension.End.Column;

                for (int row = Nomenclature.StartDataRow; row <= totalRows; row++)
                {
                    string strId = myWorksheet.GetValue(row, 1).ToString();
                    string strNomenclature = myWorksheet.GetValue(row, 2).ToString();

                    if (!CheckId(strId))
                        return false;

                    int id = int.Parse(strId);
                    string nomenclature = strNomenclature;

                    if (Nomenclatures.Exists((n => n.Id == id)))
                        return false;

                    Nomenclatures.Add(new Nomenclature(id, nomenclature));
                }
            }

            return true;
        }

        private bool ReadDataParties()
        {
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(PathData.Parties)))
            {
                ExcelWorksheet myWorksheet = xlPackage.Workbook.Worksheets.First();
                int totalRows = myWorksheet.Dimension.End.Row;
                int totalColumns = myWorksheet.Dimension.End.Column;

                for (int row = Party.StartDataRow; row <= totalRows; row++)
                {
                    string strId = myWorksheet.GetValue(row, 1).ToString();
                    string strNomenclatureId = myWorksheet.GetValue(row, 2).ToString();

                    if (!CheckId(strId))
                        return false;

                    if (!CheckId(strNomenclatureId))
                        return false;

                    int id = int.Parse(strId);
                    int nomenclatureId = int.Parse(strNomenclatureId);

                    if (Parties.Exists((party => party.Id == id)))
                        return false;

                    Parties.Add(new Party(id, nomenclatureId));
                }
            }

            return true;
        }

        private bool ReadDataTimes()
        {
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(PathData.Times)))
            {
                ExcelWorksheet myWorksheet = xlPackage.Workbook.Worksheets.First();
                int totalRows = myWorksheet.Dimension.End.Row;
                int totalColumns = myWorksheet.Dimension.End.Column;

                for (int row = Party.StartDataRow; row <= totalRows; row++)
                {
                    string strMachineToolId = myWorksheet.GetValue(row, 1).ToString();
                    string strNomenclatureId = myWorksheet.GetValue(row, 2).ToString();
                    string strOperationTime = myWorksheet.GetValue(row, 3).ToString();

                    if (!CheckId(strMachineToolId))
                        return false;

                    if (!CheckId(strNomenclatureId))
                        return false;

                    if (!CheckTime(strOperationTime) && int.TryParse(strOperationTime, out _))
                        return false;

                    int machineToolId = int.Parse(strMachineToolId);
                    int nomenclatureId = int.Parse(strNomenclatureId);
                    TimeSpan operationTime = TimeSpan.FromMinutes(int.Parse(strOperationTime));

                    Times.Add(new Time(machineToolId, nomenclatureId, operationTime));
                }
            }

            return true;
        }

        private bool CheckId(string id)
        {
            if (int.TryParse(id, out int id_))
            {
                if (CheckId(id_))
                    return true;

                return false;
            }

            return false;
        }

        private bool CheckId(int id)
        {
            return id >= 0;
        }

        private bool CheckTime(string time)
        {
            return TimeSpan.TryParse(time, out _);
        }
    }
}
