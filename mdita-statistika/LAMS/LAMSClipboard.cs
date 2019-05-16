using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata.LAMS
{
    public class LAMSClipboard
    {

        public static object CopiedObject {
            get; set; }
        public static void Paste(LearningBase content)
        {
            if (CopiedObject == null)
            {
                MessageBox.Show("Niste prethodno kopirali objekat");
                return;
            }
            var copiedSectiondiv = GetCopyOfObject(CopiedObject);
            content.ToolList.Add(copiedSectiondiv);
        }


        public static LamsTool GetCopyOfObject(object obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return formatter.Deserialize(ms) as LamsTool;
            }
        }
    }
}
