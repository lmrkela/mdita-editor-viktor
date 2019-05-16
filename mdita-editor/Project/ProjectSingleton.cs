using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Utils;

namespace mDitaEditor.Project
{
    static class ProjectSingleton
    {
        public static ProjectFile Project { get; set; }

        public static List<ImageDeleted> ImagesToSaveOnClose = new List<ImageDeleted>();

        public static Section SelectedSection { get; set; }
        public static LearningBase SelectedContent { get; set; }

        public static void SaveUnsavedImages()
        {
            foreach(ImageDeleted img in ImagesToSaveOnClose)
            {
                if (!File.Exists(img.Path))
                {
                    img.Image.Save(img.Path);
                }
            }
            ImagesToSaveOnClose.Clear();
        }
    }
}
