using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatistikaProjekata
{
    static class ProjectSingleton
    {
  
        public static IEnumerable<string> CustomSort(IEnumerable<string> list)
        {
            if (!list.Any())
            {
                return list;
            } 
            

            var maxLen = list.Select(s => s.Length).Max();

            return list.Select(s => new
            {
                OrgStr = s,
                SortStr = Regex.Replace(s, @"(\d+)|(\D+)", m => m.Value.PadLeft(maxLen, char.IsDigit(m.Value[0]) ? ' ' : '\xffff'))
            })
            .OrderBy(x => x.SortStr)
            .Select(x => x.OrgStr);
        }

        public static ProjectFile LoadFile(string fileName)
        {
            var project = ProjectFile.OpenProjectForFile(fileName);

            var files = Directory.GetFiles(project.ProjectDir, "*.dita");
            files = CustomSort(files).ToArray();
            foreach (var file in files)
            {
                project.OpenContentFile(file, false);
            }
            if (!project.OpenToolsFile_Deprecated())
            {
                project.OpenToolsFile();
            }
            return project;
        }
    }
}
