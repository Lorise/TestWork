using System;

namespace TestWork
{
    static class PathData
    {
        private static readonly string Folder = AppDomain.CurrentDomain.BaseDirectory;

        public static string MachineTools = Folder + "machine_tools.xlsx";
        public static string Nomenclatures = Folder + "nomenclatures.xlsx";
        public static string Parties = Folder + "parties.xlsx";
        public static string Times = Folder + "times.xlsx";

        public static string Output = Folder;
    }
}
