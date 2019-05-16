using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using mDitaEditor.Dita;

namespace mDitaEditor.Lams
{
    public class LamsClipboard
    {

        public static object CopiedObject {
            get; set; }
        public static void Paste(LearningBase content)
        {
            if (CopiedObject != null)
            {
               LamsTool copiedSectiondiv = GetCopyOfObject(CopiedObject);
                content.ToolList.Add(copiedSectiondiv);
            }
            else if (Clipboard.GetDataObject() is LamsTool)
            {
                LamsTool copiedSectiondiv = GetCopyOfObject(CopiedObject);
                content.ToolList.Add(copiedSectiondiv);
            }
            else
            {
                MessageBox.Show("Niste prethodno kopirali objekat");
                return;
            }
        
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
