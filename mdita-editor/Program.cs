using System;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Windows.Forms;

namespace mDitaEditor
{
    static class Program
    {
        public static long AppVersion { get; }

        public static string ProgramPath { get; }

        public static string DocumentsFolder { get; }

        public static string HtmlFolder { get; }

        public static string DitaFolder { get; }

        static Program()
        {
            AppVersion = GetVersion();
            ProgramPath = GetProgramDirectory();
            DocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\mDitaEditor";
            HtmlFolder = DocumentsFolder + @"\HTML";
            DitaFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "DITA-OT");
        }

        private static long GetVersion()
        {
            long v = 0;
            var revisions = Application.ProductVersion.Split('.');
            int mod = 1;
            for (var i = revisions.Length - 2; i >= 0; --i, mod *= 1000)
            {
                var r = long.Parse(revisions[i]);
                v += r * mod;
            }
            return v;
        }

        private static string GetProgramDirectory()
        {
            var dir = Assembly.GetExecutingAssembly().CodeBase;
            return Path.GetDirectoryName(dir).Substring("file:\\".Length);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
