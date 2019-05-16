using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Statistics = StatistikaProjekata.ProjectFile.Statistics;
using Excel = Microsoft.Office.Interop.Excel;

namespace StatistikaProjekata
{
    class ExcelExporter
    {
        public static void ExportToExcel(string filename, Statistics total, Statistics average, decimal fob, decimal fpm)
        {
            var excelApp = new Excel.Application();
            try
            {
                var workbook = excelApp.Workbooks.Add();
                var worksheet = (Excel.Worksheet) excelApp.ActiveSheet;

                /*worksheet.Cells[1, "A"] = "Predmet";
                worksheet.Cells[1, "B"] = "FIN1";
                worksheet.Cells[1, "C"] = "FIN2";
                worksheet.Cells[1, "D"] = "FMM";
                worksheet.Cells[1, "E"] = "FOB";
                worksheet.Cells[1, "F"] = "FPM";*/

                worksheet.Cells[1, "A"] = total.Predmet;
                worksheet.Cells[1, "B"] = total.ObjectCount;
                worksheet.Cells[1, "C"] = total.Fin1Count;
                worksheet.Cells[1, "D"] = average.Fin2Count;
                ((Excel.Range)worksheet.Cells[2, "D"]).NumberFormat = "0.00";
                worksheet.Cells[1, "E"] = average.VideoCount;
                ((Excel.Range)worksheet.Cells[2, "E"]).NumberFormat = "0.00";
                //worksheet.Cells[2, "E"] = fob;
                //worksheet.Cells[2, "F"] = fpm;

                for (int i = 1; i <= 6; ++i)
                {
                    worksheet.Columns[i].AutoFit();
                }

                workbook.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal);
                workbook.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save selected file: " + ex.Message);
            }
            finally
            {
                excelApp.Quit();
            }
        }
    }
}
