namespace mDitaEditor.Dita.Controls
{
    class SnippetCtrl
    {
        public static int LastSelectedIndex = 0;
        private string Code;
        private string Language;
        private string Lines;
        private bool ShowLineNumber;
        private SnippetControl SnippetForUpdate;
        private Sectiondiv div;
        private SelectableFlowPanel panel;

        public SnippetCtrl(string code, string lang, string lines, bool showLineNumber, SnippetControl snippetForUpdate, SelectableFlowPanel _panel, Sectiondiv div = null)
        {
            Code = code;
            Language = lang;
            Lines = lines;
            ShowLineNumber = showLineNumber;
            SnippetForUpdate = snippetForUpdate;
            panel = _panel;
            this.div = div;
        }

        /// <summary>
        /// Dodavanje novog snipeta u panel koji je poslednji selektovan ili modifikovanje snipeta koji se nalazi u atributu SnipetForUpdate.
        /// </summary>
        public void AddOrUpdateSnippet()
        {
            if (SnippetForUpdate == null)
            {
                InsertSnippet();
            }
            else
            {
                UpdateSnippet();
            }
        }

        /// <summary>
        /// Dodavanje novog snipeta u poslednje selektovani panel.
        /// </summary>
        private void InsertSnippet()
        {
            if (div == null)
            {
                div = SnippetControl.InitSectionDiv(panel.Column);
            }
            SnippetControl snippet = new SnippetControl(MeasureHeight(), Language, Code, ShowLineNumber, panel, div);
            ControlFactory.getSnippetForPanel(panel, snippet, div);
        }

        /// <summary>
        /// Modifikavanje snippet koji je definisan u atributu snippetForUpdate.
        /// </summary>
        public void UpdateSnippet()
        {
            SnippetForUpdate.RedefineControl(MeasureHeight(), Language, Code, ShowLineNumber);
        }

        /// <summary>
        /// Metoda koja meri visinu snippeta na osnovu broja linija koje je korsinik uneo.
        /// </summary>
        /// <returns></returns>
        public int MeasureHeight()
        {
            int i = 0;
            if (int.TryParse(Lines, out i))
            {
                return i * SnippetControl.LINE_HEIGHT;
            }
            else
            {
                return 0;
            }
        }
        
    }        

}
