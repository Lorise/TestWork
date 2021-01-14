using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace TestWork
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            Plan plan = new Plan();
            plan.Calculate();

            List<WorkInfo> workInfos = plan.GetResult();

            Console.WriteLine();
            foreach (WorkInfo workInfo in workInfos) 
                Console.WriteLine(workInfo.ToString());
            Console.WriteLine();

            PrintExcel(workInfos);

            Console.WriteLine("Press enter");
            Console.ReadLine();
        }

        static void PrintExcel(List<WorkInfo> workInfos)
        {
            FileInfo newFile = new FileInfo("result.xlsx");

            if(File.Exists("result.xlsx"))
                File.Delete("result.xlsx");

            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Result");

                worksheet.Cells["A1"].LoadFromText("Партия, Номенклатура, Машина, Время старт, Время стоп");

                for (int i = 2; i <= workInfos.Count; i++) 
                    worksheet.Cells["A" + i].LoadFromText(workInfos[i - 2].ToExcel());

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                xlPackage.Save();
            }
        }
    }
}
