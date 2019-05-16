using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Project;
using System.Drawing;
using mDitaEditor.Dita.Controls;
using LearningContentList = mDitaEditor.Project.LearningContentList;
using mDitaEditor.CustomControls;
using System.Diagnostics;

namespace mDitaEditor.Utils
{
    static partial class DitaClipboard
    {
        private static IState _state;
        private static IState _savedState;

              
        public static void saveCurrentState()
        {
            _savedState = _state;
        }
        
        public static bool isStateChanged()
        {
            return _state != _savedState;
        }

        private static IState State
        {
            get { return _state; }
            set
            {
                _state = value;
                if (_state != null)
                {
                    CanUndo = _state.Prev != null;
                    CanRedo = _state.Next != null;
                }
                else
                {
                    CanUndo = CanRedo = false;
                }

                if (CanUndo)
                {
                    MainForm.Instance.rbUndo.ToolTip = _state.InfoString();
                }
                if (CanRedo)
                {
                    MainForm.Instance.rbRedo.ToolTip = _state.Next.InfoString();
                }

                MainForm.Instance.rbUndo.Enabled = CanUndo;
                MainForm.Instance.rbRedo.Enabled = CanRedo;
            }
        }

        public static bool CanUndo { get; private set; }
        public static bool CanRedo { get; private set; }

     
        private static volatile bool _working = false;

        public static void Undo()
        {
            if (!_working && State != null)
            {
                _working = true;
                State.Undo();
                _working = false;
            }
        }



        public static void Redo()
        {
            if (!_working && State != null)
            {
                _working = true;
                State.Redo();
                _working = false;
            }
        }


        private abstract class IState
        {
            public IState Prev;
            public IState Next;

            public virtual string InfoString()
            {
                return ToString();
            }

            public void SetCurrent()
            {
                Prev = State;
                State = this;
                if (Prev != null)
                {
                    Prev.Next = this;
                }

                RemoveLast(100);

                 
            }

            private void RemoveLast(int states)
            {
                IState prev = Prev;

                while (prev != null)
                {
                    if (--states <= 0)
                    {
                        prev.Prev = new InitialState();
                        return;
                    }
                    prev = prev.Prev;
                }
            }

            public void Undo()
            {
                if (this is InitialState)
                {
                    return;
                }
                if (Prev != null)
                {
                    DoUndo();
                    State = Prev;
                }
            }

            public void Redo()
            {
                if (Next != null)
                {
                    Next.DoRedo();
                    State = Next;
                }
            }

         

            protected abstract void DoRedo();

            protected abstract void DoUndo();
        }


        private class InitialState : IState
        {
            public override string InfoString()
            {
                return "";
            }

            protected override void DoRedo()
            {
            }

            protected override void DoUndo()
            {
            }
        }

        public static void ResetStates()
        {
            State = null;
            if (ProjectSingleton.Project != null)
            {
                new InitialState().SetCurrent();
            }
            saveCurrentState();
        }


        /// <summary>
        /// Klasa preko koje se realizuje stanje dodavanja sekcije.
        /// </summary>
        private class SectionAddDeleteState : IState
        {
            readonly Section StateSection;
            readonly int Index;
            readonly SectionList ParentList;

            private readonly bool _add;

            public SectionAddDeleteState(Section section, bool add)
            {
                StateSection = section;
                ParentList = StateSection.Parent.LearningBody.Sections;
                Index = ParentList.IndexOf(section);
                _add = add;
                MainForm.Instance.OpenSlide((IDitaSlide)StateSection);
            }

            private void Add()
            {
                ParentList.Insert(Index, StateSection);
                MainForm.Instance.RecreateMenu();
                MainForm.Instance.OpenSlide((IDitaSlide)StateSection);
            }

            private void Remove()
            {
                int index = MainForm.Instance.OpenSlideIndex;
                ParentList.Remove(StateSection);
                MainForm.Instance.RecreateMenu();
                MainForm.Instance.OpenSlideIndex = index;
                if (StateSection == ProjectSingleton.SelectedSection) {
                    MainForm.Instance.CloseSlide();
                }
                
            }

            protected override void DoRedo()
            {
                if (_add)
                {
                    Add();
                }
                else
                {
                    Remove();
                }
            }

            protected override void DoUndo()
            {
                if (_add)
                {
                    Remove();
                }
                else
                {
                    Add();
                }
            }
        }

        /// <summary>
        /// Metoda koja dodaje novo add stanje sekcije.
        /// </summary>
        /// <param name="section"></param>
        public static void AddSectionAddedState(Section section)
        {
            new SectionAddDeleteState(section, true).SetCurrent();
        }

        /// <summary>
        /// Metoda koja dodaje novo delete stanje sekcije.
        /// </summary>
        /// <param name="section"></param>
        public static void AddSectionDeletedState(Section section)
        {
            new SectionAddDeleteState(section, false).SetCurrent();
        }

        /// <summary>
        /// Klasa preko koje se realizuje stanje dodavanja objekta.
        /// </summary>
        private class ObjectAddDeleteState : IState
        {
            readonly LearningContent lc;
            readonly int index;
            readonly LearningContentList lcList;
           

            private readonly bool _add;

            public ObjectAddDeleteState(LearningContent obj, bool add)
            {

               
                MainForm.Instance.OpenSlide((IDitaSlide)obj);
                lc = obj;
                lcList = ProjectSingleton.Project.LearningContents;
                if (lc.Parent == null)
                {
                    index = lcList.IndexOf(lc);
                }
                else
                {
                    index = obj.Parent.SubObjects.IndexOf(lc);
                }
                _add = add;

               
            }

            private void Add()
            {
                lcList.InsertObject(index, lc);
                MainForm.Instance.RecreateMenu();

                MainForm.Instance.OpenSlide((IDitaSlide)lc);
            }

            private void Remove()
            {
                int index = MainForm.Instance.OpenSlideIndex;
                lcList.Remove(lc);
                MainForm.Instance.RecreateMenu();
                MainForm.Instance.OpenSlideIndex = index;

                if (lc == ProjectSingleton.SelectedContent)
                {
                    MainForm.Instance.CloseSlide();
                }


            }

            protected override void DoRedo()
            {
                if (_add)
                {
                    Add();
                }
                else
                {
                    Remove();
                }
            }

            protected override void DoUndo()
            {
                if (_add)
                {
                    Remove();
                }
                else
                {
                    Add();
                }
            }
        }

        /// <summary>
        /// Metoda koja dodaje novo add stanje objekta.
        /// </summary>
        /// <param name="lc"></param>
        public static void AddObjectAddedState(LearningContent lc)
        {
            new ObjectAddDeleteState(lc, true).SetCurrent();
        }

        /// <summary>
        /// Metoda koja dodaje novo delete stanje objekta.
        /// </summary>
        /// <param name="lc"></param>
        public static void AddObjectDeletedState(LearningContent lc)
        {
            new ObjectAddDeleteState(lc, false).SetCurrent();
        }

        // STANJA NA NIVOU SEKCIJE        

        // Staticki atribut u koji se smesta sectiondiv nad kojim ce se izvrsavati Undo i Redo operacije.
        public static Sectiondiv ActiveSectiondiv;

        /// <summary>
        /// Klasa preko koje se realizuje cuvanje stanja promene layout-a sekcije.
        /// </summary>
        private class ChangeLayoutState : IState
        {
            readonly Section section;
            readonly string prevLayout;
            readonly string tempLayout;

            public ChangeLayoutState(string prevLayout, string tempLayout)
            {
                this.prevLayout = prevLayout;
                this.tempLayout = tempLayout;
                section = ProjectSingleton.SelectedSection;
            }

            protected override void DoRedo()
            {
                SetLayout(tempLayout);
            }

            protected override void DoUndo()
            {
                SetLayout(prevLayout);
            }

            private void SetLayout(string layout)
            {
                section.SectionDivs[section.SectionDivs.Count - 1].Outputclass = layout;
                section.Outputclass = "section-" + layout;
                if (section.Parent is LearningOverview)
                {
                    section.Outputclass = null;
                }
                MainForm.Instance.OpenSlide(section);
                //var control = section.ChangeLayout(prevLayout);
                //if (control != null)
                //{
                //    MainForm.Instance.OpenSlide(control);
                //}  
            }
        }

        private class DeleteImageState : IState
        {
            readonly Sectiondiv sectiondiv;
            readonly Sectiondiv parent;
            readonly Image img;
            readonly string Path;
            readonly int index;
            readonly Section section;

            public DeleteImageState(Sectiondiv parent, Sectiondiv sectiondiv, Image img, string path)
            {
                this.sectiondiv = sectiondiv;
                this.parent = parent;
                this.Path = path;
                this.img = img;
                index = parent.SectionDivs.IndexOf(sectiondiv);
                section = ProjectSingleton.SelectedSection;
            }

            protected override void DoRedo()
            {
                parent.SectionDivs.Remove(sectiondiv);
                MainForm.Instance.OpenSlide(section);
            }

            protected override void DoUndo()
            {
                string returnUrl = ProjectSingleton.Project.ResourcesDir + Path;
                System.IO.File.Delete(returnUrl);
                img.Save(returnUrl);
                if (index != -1)
                {
                    parent.SectionDivs.Insert(index, sectiondiv);
                    MainForm.Instance.OpenSlide(section);
                }
            }
        }

        /// <summary>
        /// Klasa preko koje se realizuje cuvanje stanja prilikom dodavanja i brisanja kontrola.
        /// Ukoliko se atributu add prosledi vrednost true zanci da se radi o dodavanju, a u suprotnom brisanju oglasa.
        /// </summary>
        private class ControlAddDeleteState : IState
        {
            readonly Sectiondiv parent;
            readonly Sectiondiv sectiondiv;
            readonly Section section;
            readonly int index;
            private readonly bool add;

            public ControlAddDeleteState(Sectiondiv parent, Sectiondiv root, bool add)
            {
                sectiondiv = root;
                this.parent = parent;
                index = parent.SectionDivs.IndexOf(sectiondiv);
                section = ProjectSingleton.SelectedSection;
                this.add = add;
            }

            private void addSectiondiv()
            {
                if (index != -1)
                {
                    parent.SectionDivs.Insert(index, sectiondiv);
                    MainForm.Instance.OpenSlide(section);
                }
                else
                {
                    //Log doslo je do greske pri vracanju
                }
            }

            private void removeSectiondiv()
            {
                parent.SectionDivs.Remove(sectiondiv);
                MainForm.Instance.OpenSlide(section);
            }

            protected override void DoRedo()
            {
                if (add)
                {
                    addSectiondiv();
                }
                else
                {
                    removeSectiondiv();
                }
            }

            protected override void DoUndo()
            {
                if (add)
                {
                    removeSectiondiv();
                }
                else
                {
                    addSectiondiv();
                }
            }
        }

      


        /// <summary>
        /// Klasa koja cuva stanje pomeranja kontrola na gore ili dole u okviru sekcije.
        /// </summary>
        private class ControlMoveState : IState
        {
            readonly Sectiondiv sectiondiv;
            readonly Sectiondiv parent;
            readonly Section section;
            int up;

            public ControlMoveState(Sectiondiv parent, Sectiondiv root, int up)
            {
                this.parent = parent;
                sectiondiv = root;
                section = ProjectSingleton.SelectedSection;
                this.up = up;
            }

            protected override void DoRedo()
            {
                up = (up == 1) ? 0 : 1;
                SectionsGuiUtil.SwapSectionDivs(sectiondiv, up, parent);
                MainForm.Instance.OpenSlide(section);
            }

            protected override void DoUndo()
            {
                up = (up == 1) ? 0 : 1;
                SectionsGuiUtil.SwapSectionDivs(sectiondiv, up, parent);
                MainForm.Instance.OpenSlide(section);
            }
        }

        /// <summary>
        /// Klasa preko koje se realizuje stanje promene slike u okviru ImageBox kontrole.
        /// </summary>
        private class ChangeImageState : IState
        {
            readonly Sectiondiv sectiondiv;
            readonly string pre;
            readonly string posle;
            readonly string path;
            readonly Image prevImg;
            readonly Image tempImg;
            readonly Section section;

            public ChangeImageState(Sectiondiv sectiondiv, string pre, string posle, string path, Image prevImg,
                Image tempImg)
            {
                this.sectiondiv = sectiondiv;
                this.section = ProjectSingleton.SelectedSection;
                this.pre = pre;
                this.posle = posle;
                this.path = path;
                this.prevImg = prevImg;
                this.tempImg = tempImg;
            }

            protected override void DoRedo()
            {
                string returnUrl = ProjectSingleton.Project.ResourcesDir + path;
                System.IO.File.Delete(returnUrl);
                tempImg.Save(returnUrl);
                sectiondiv.Content = posle;
                MainForm.Instance.OpenSlide(section);
            }

            protected override void DoUndo()
            {
                string returnUrl = ProjectSingleton.Project.ResourcesDir + path;
                System.IO.File.Delete(returnUrl);
                prevImg.Save(returnUrl);
                sectiondiv.Content = pre;
                MainForm.Instance.OpenSlide(section);
            }

        }

        /// <summary>
        /// Klasa preko koje se realizuje cuvanje stanja promene sadrzaja snipet-a.
        /// </summary>
        private class UpadateSnippetState : IState
        {
            readonly Section section;
            readonly Sectiondiv sectiondiv;
            readonly string prevContent;
            readonly string tempContent;

            public UpadateSnippetState(Sectiondiv sectiondiv, string prevContent, string tempContent)
            {
                this.sectiondiv = sectiondiv;
                this.prevContent = prevContent;
                this.tempContent = tempContent;
                section = ProjectSingleton.SelectedSection;
            }
            
            protected override void DoRedo()
            {
                sectiondiv.SectionDivs[0].Content = tempContent;
                MainForm.Instance.OpenSlide(section);
            }

            protected override void DoUndo()
            {
                sectiondiv.SectionDivs[0].Content = prevContent;
                MainForm.Instance.OpenSlide(section);
            }
        }

        private class UpdateYouTubeState : IState
        {
            readonly Section section;
            readonly Sectiondiv sectiondiv;
            readonly string prevContent;
            readonly string tempContent;

            public UpdateYouTubeState(Sectiondiv sectiondiv, string prevContent, string tempContent)
            {
                this.sectiondiv = sectiondiv;
                this.prevContent = prevContent;
                this.tempContent = tempContent;
                section = ProjectSingleton.SelectedSection;
            }

            protected override void DoRedo()
            {
                sectiondiv.Content = tempContent;
                MainForm.Instance.OpenSlide(section);
            }

            protected override void DoUndo()
            {
                sectiondiv.Content = prevContent;
                MainForm.Instance.OpenSlide(section);
            }
        }



        private class UpdateObjectDifficultyState : IState
        {

            private IDitaSlide content;
            private CueComboBox comboBox;
            private int oldIndex;
            private int newIndex;

            public UpdateObjectDifficultyState(CueComboBox comboBox, int oldIndex, int newIndex)
            {

                this.comboBox = comboBox;
                this.oldIndex = oldIndex;
                this.newIndex = newIndex;
                content = ProjectSingleton.SelectedContent;

            }

            private void focus()
            {

                comboBox.saveCurrentIndex();
                comboBox.Focus();

            }

            private void openSlide()
            {
                if (ProjectSingleton.SelectedContent != content || ProjectSingleton.SelectedSection != null)
                {
                    MainForm.Instance.OpenSlide(content);
                }

            }

            protected override void DoRedo()
            {
                comboBox.UndoRedoEvent = true;
                openSlide();
                comboBox.SelectedIndex = newIndex;
                focus();
                comboBox.UndoRedoEvent = false;

            }

            protected override void DoUndo()
            {
                comboBox.UndoRedoEvent = true;
                openSlide();
                comboBox.SelectedIndex = oldIndex;
                focus();
                comboBox.UndoRedoEvent = false;

            }

        }

        private class UpdateSlideTextState : IState
        {
            private IDitaSlide slide;
            private string oldText;
            private string newText;
            private CueTextBox textBox;

            public UpdateSlideTextState(IDitaSlide slide,CueTextBox textBox, string oldText, string newText)
            {

                this.slide = slide;
                this.textBox = textBox;
                this.oldText = oldText;
                this.newText = newText;
                                 
            }

            private void focus()
            {

                textBox.OldText = textBox.Text;
                textBox.Focus();
                textBox.SelectionStart = textBox.Text.Length;
                textBox.SelectionLength = 0;
            }

            private void openSlide()
            {
               
                IDitaSlide currentSlide =  
                    slide is LearningBase ? 
                    (IDitaSlide) ProjectSingleton.SelectedContent : 
                    (IDitaSlide) ProjectSingleton.SelectedSection;

                             

                if ( slide != currentSlide || ((slide is LearningBase) && ProjectSingleton.SelectedSection != null))
                {
                    Debug.WriteLine("New slide");
                    MainForm.Instance.OpenSlide(slide);
                }
            }

            protected override void DoRedo()
            {
                openSlide();             
                textBox.Text = newText;
                focus();
                Debug.WriteLine("New text: " + newText);

            }

            protected override void DoUndo()
            {
                openSlide();              
                textBox.Text = oldText;
                focus();
                Debug.WriteLine("Old text: " + oldText);

            }
           
        }

        /// <summary>
        /// Klasa preko koje se realizuje cuvanje stanja promene sadrzaja snipet-a.
        /// </summary>
        private class UpdateLatexState : IState
        {
            readonly Section section;
            readonly Sectiondiv sectiondiv;
            readonly string prevContent;
            readonly string tempContent;

            public UpdateLatexState(Sectiondiv sectiondiv, string prevContent, string tempContent)
            {
                this.sectiondiv = sectiondiv;
                this.prevContent = prevContent;
                this.tempContent = tempContent;
                section = ProjectSingleton.SelectedSection;
            }

            protected override void DoRedo()
            {
                sectiondiv.Content = tempContent;
                MainForm.Instance.OpenSlide(section);
            }

            protected override void DoUndo()
            {
                sectiondiv.Content = prevContent;
                MainForm.Instance.OpenSlide(section);
            }
        }

        /// <summary>
        /// Klasa koja cuva stanje na resize kontrola.
        /// </summary>
        private class SectiondivChangedState : IState
        {
            private readonly Section _section;
            private string _oldContent;
            private readonly int[] _sectionDivIndices;

            private Sectiondiv Sectiondiv
            {
                get
                {
                    Sectiondiv sectiondiv = null;
                    List<Sectiondiv> list = _section.SectionDivs;
                    foreach (var i in _sectionDivIndices)
                    {
                        sectiondiv = list[i];
                        list = sectiondiv.SectionDivs;
                    }
                    return sectiondiv;
                }
            }

            private bool GetIndex(List<Sectiondiv> list, Sectiondiv sectiondiv, List<int> indices)
            {
                if (list.Contains(sectiondiv))
                {
                    indices.Add(list.IndexOf(sectiondiv));
                    return true;
                }
                for (int i = 0; i < list.Count; ++i)
                {
                    indices.Add(i);
                    if (GetIndex(list[i].SectionDivs, sectiondiv, indices))
                    {
                        return true;
                    }
                    indices.RemoveAt(indices.Count - 1);
                }
                return false;
            }

            public SectiondivChangedState(Section section, Sectiondiv sectiondiv, string oldContent)
            {
                _section = section;
                _oldContent = oldContent;

                var indices = new List<int>();
                if (!GetIndex(section.SectionDivs, sectiondiv, indices))
                {
                    throw new ArgumentException("Nema Sectiondiva.");
                }
                _sectionDivIndices = indices.ToArray();
            }

            private void Restore()
            {
                Sectiondiv sectiondiv = Sectiondiv;
                string content = sectiondiv.Content;
                sectiondiv.Content = _oldContent;
                _oldContent = content;

                MainForm.Instance.OpenSlide(_section);
            }

            protected override void DoRedo()
            {
                Restore();
            }

            protected override void DoUndo()
            {
                Restore();
            }
        }


        /// <summary>
        /// Metoda koja dodaje novo stanje kontrole u listu stanja.
        /// </summary>
        public static void ControlAddOrDeleteState(Sectiondiv parent, Sectiondiv root, bool add)
        {
            new ControlAddDeleteState(parent, root, add).SetCurrent();
        }

        /// <summary>
        /// Metoda koja dodaje novo stanje slike pri brisanju u listu stanja.
        /// </summary>
        public static void ImageDeleteState(Sectiondiv parent, Sectiondiv root, Image img, string Path)
        {
            new DeleteImageState(parent, root, img, Path).SetCurrent();
        }

        /// <summary>
        /// Metoda koja dodaje novo stanje kontrole u listu stanja prilikom pomeranja kontrole na gore ili dole.
        /// </summary>
        public static void ControlMoveUndoState(Sectiondiv parent, Sectiondiv root, int up)
        {
            new ControlMoveState(parent, root, up).SetCurrent();
        }

        /// <summary>
        /// Metoda koja kreira novo stanje ImageBox-a nakon promene slike.
        /// </summary>
        public static void ChangeImageUndoState(Sectiondiv sectiondiv, string pre, string posle, string path,
            Image prevImg, Image tempImg)
        {
            new ChangeImageState(sectiondiv, pre, posle, path, prevImg, tempImg).SetCurrent();
        }

        /// <summary>
        /// Metoda koja kreira novo stanje nakon promene layout-a sekcije.
        /// </summary>
        public static void ChangeLayoutUndoState(string prevLayout, string tempLayout)
        {
            new ChangeLayoutState(prevLayout, tempLayout).SetCurrent();
        }

        /// <summary>
        /// Metoda koja kreira novo stanje Snippet-a nakon azuriranja.
        /// </summary>
        public static void UpdateSnippetUnodState(Sectiondiv sectiondiv, string prevCont, string tempCont)
        {
            new UpadateSnippetState(sectiondiv, prevCont, tempCont).SetCurrent();
        }

        public static void UpdateYouTubeUndoState(Sectiondiv sectiondiv, string prevCont, string tempCont)
        {
            new UpdateYouTubeState(sectiondiv, prevCont, tempCont).SetCurrent();
        }

        public static void UpdateSlideTextUndoState(IDitaSlide slide, CueTextBox textInput, string oText, string nText)
        {
            if (oText != nText)
            {
                Debug.WriteLine("leave " + oText + " " + nText);
                textInput.saveCurrentText();
                new UpdateSlideTextState(slide,textInput, oText, nText).SetCurrent();
            }
        }
        
        public static void UpdateObjectDifficultyUndoState(CueComboBox comboBox)
        {
            if (comboBox.SelectedIndex != comboBox.OldIndex && comboBox.OldIndex != -1 )
            {               
                new UpdateObjectDifficultyState(comboBox, comboBox.OldIndex, comboBox.SelectedIndex).SetCurrent();
            }
        }

        /// <summary>
        /// Metoda koja kreira novo stanje Latexu nakon azuriranja.
        /// </summary>
        public static void UpdateLatexUndoState(Sectiondiv sectiondiv, string prevCont, string tempCont)
        {
            new UpdateLatexState(sectiondiv, prevCont, tempCont).SetCurrent();
        }

        public static void ControlDelete(Sectiondiv root, Control panel)
        {
            DitaClipboard.ActiveSectiondiv = root;
            DitaClipboard.ControlAddOrDeleteState(((SelectableFlowPanel)panel).Column, DitaClipboard.ActiveSectiondiv,
                false);
        }

        public static void AddSectiondivChangedState(Section section, Sectiondiv sectiondiv, string oldContent)
        {
            new SectiondivChangedState(section, sectiondiv, oldContent).SetCurrent();
        }

        /// <summary>
        /// Klasa preko koje se realizuje cuvanje stanja prilikom dodavanja i brisanja kontrola.
        /// Ukoliko se atributu add prosledi vrednost true zanci da se radi o dodavanju, a u suprotnom brisanju oglasa.
        /// </summary>
        private class GalleryImageMovedState : IState
        {
            public override string InfoString()
            {

                string nazivObjekta = "";
                if (_section.Parent is LearningOverview)
                {
                    nazivObjekta = "Uvod";
                }
                else if (_section.Parent is LearningSummary)
                {
                    nazivObjekta = "Zaključak";
                }
                else if (_section.Parent is LearningContent)
                {
                    var lc = (LearningContent)_section.Parent;
                    nazivObjekta = lc.Id;
                }
                return string.Format("Galerija \"{0}: {1}\" - Slika {2} pomerena {3}.", nazivObjekta, _section.Title,
                    _index + 1, _up ? "gore" : "dole");

            }

            private readonly Section _section;

            private readonly int _index;
            private readonly bool _up;

            public GalleryImageMovedState(Section section, int imageIndex, bool up)
            {
                _section = section;
                _index = imageIndex;
                _up = up;
            }

            private void Move()
            {
                if (ProjectSingleton.SelectedSection != _section)
                {
                    MainForm.Instance.OpenSlide(_section);
                }
                var gallery = MainForm.Instance.sectionControl._panels[0].Controls[0] as GalleryControl;
                if (gallery != null)
                {
                    gallery.MoveImage(_index, _up);
                }
            }

            protected override void DoRedo()
            {
                Move();
            }

            protected override void DoUndo()
            {
                Move();
            }
        }

        public static void AddGalleryImageMovedState(Section section, int imageIndex, bool up)
        {
            new GalleryImageMovedState(section, imageIndex, up).SetCurrent();
        }

        /// <summary>
        /// Klasa preko koje se realizuje cuvanje stanja prilikom dodavanja i brisanja slike u galeriji.
        /// Ukoliko se atributu add prosledi vrednost true zanci da se radi o dodavanju, a u suprotnom brisanju slike.
        /// </summary>
        private class GalleryImageAddDeleteState : IState
        {
            public override string InfoString()
            {
                return string.Format("Galerija \"{0}: {1}\" - Slika {2} {3}.", _section.Parent.TitleDescription, _section.Title, _index + 1,
                    _add ? "dodata" : "obrisana");
            }

            private readonly Section _section;
            private readonly int _index;
            private readonly bool _add;
            private readonly string _imageName;
            private readonly string _title;
            private readonly string _description;

            public GalleryImageAddDeleteState(Section section, int imageIndex, bool add, string imageName, string title,
                string description)
            {
                _section = section;
                _index = imageIndex;
                _add = add;
                _imageName = imageName;
                _title = title;
                _description = description;
            }

            private void Add()
            {
                if (ProjectSingleton.SelectedSection != _section)
                {
                    MainForm.Instance.OpenSlide(_section);
                }
                var gallery = MainForm.Instance.sectionControl._panels[0].Controls[0] as GalleryControl;
                if (gallery != null)
                {
                    gallery.AddImage(_index, _imageName, _title, _description);
                }
            }

            private void Delete()
            {
                if (ProjectSingleton.SelectedSection != _section)
                {
                    MainForm.Instance.OpenSlide(_section);
                }
                var gallery = MainForm.Instance.sectionControl._panels[0].Controls[0] as GalleryControl;
                if (gallery != null)
                {
                    gallery.DeleteImage(_index);
                }
            }

            protected override void DoRedo()
            {
                if (_add)
                {
                    Add();
                }
                else
                {
                    Delete();
                }
            }

            protected override void DoUndo()
            {
                if (!_add)
                {
                    Add();
                }
                else
                {
                    Delete();
                }
            }
        }

        public static void AddGalleryImageAddDeleteState(Section section, int imageIndex, bool add, string imageName,
            string title, string description)
        {
            new GalleryImageAddDeleteState(section, imageIndex, add, imageName, title, description).SetCurrent();
        }

        private class SectionDivMovedState : IState
        {
            private readonly Section _section;
            private readonly Sectiondiv _movedDiv;
            private readonly Sectiondiv _source;
            private readonly int _sourceIndex;
            private readonly Sectiondiv _destination;
            private readonly int _destinationIndex;

            public SectionDivMovedState(Section section, Sectiondiv movedDiv, Sectiondiv source, int sourceIndex,
                Sectiondiv destination, int destinationIndex)
            {
                _section = section;
                _movedDiv = movedDiv;
                _source = source;
                _sourceIndex = sourceIndex;
                _destination = destination;
                _destinationIndex = destinationIndex;
            }

            protected override void DoRedo()
            {
                Move(_source, _destination, _destinationIndex);
            }

            protected override void DoUndo()
            {
                Move(_destination, _source, _sourceIndex);
            }

            private void Move(Sectiondiv from, Sectiondiv to, int index)
            {
                from.SectionDivs.Remove(_movedDiv);
                to.SectionDivs.Insert(index, _movedDiv);
                MainForm.Instance.OpenSlide(_section);
            }
        }

        public static void AddSectionDivMovedState(Section section, Sectiondiv movedDiv, Sectiondiv source,
            int sourceIndex,
            Sectiondiv destination, int destinationIndex)
        {
            new SectionDivMovedState(section, movedDiv, source, sourceIndex, destination, destinationIndex).SetCurrent();
        }

        private class SectionMovedState : IState
        {
            public override string InfoString()
            {
                return string.Format("Sekcija {0} - {1} pomerena u objekat {2} - {3}.", _source.TitleDescription,
                    _sourceIndex + 1, _destination.TitleDescription, _destinationIndex + 1);
            }

            private readonly Section _section;
            private readonly LearningBase _source;
            private readonly int _sourceIndex;
            private readonly LearningBase _destination;
            private readonly int _destinationIndex;

            public SectionMovedState(Section section, LearningBase source, int sourceIndex, LearningBase destination,
                int destinationIndex)
            {
                _section = section;
                _source = source;
                _sourceIndex = sourceIndex;
                _destination = destination;
                _destinationIndex = destinationIndex;
            }

            protected override void DoRedo()
            {
                _section.MoveTo(_destination, _destinationIndex);
                MainForm.Instance.RecreateMenu();
            }

            protected override void DoUndo()
            {
                _section.MoveTo(_source, _sourceIndex);
                MainForm.Instance.RecreateMenu();
            }
        }

        public static void AddSectionMovedState(Section section, LearningBase source, int sourceIndex,
            LearningBase destination,
            int destinationIndex)
        {
            new SectionMovedState(section, source, sourceIndex, destination, destinationIndex).SetCurrent();
        }
        private class SubObjectToObjectState : IState
        {
            private readonly LearningContent _content;
            private readonly LearningContent _parent;
            private readonly string originalId;

            public override string InfoString()
            {
                return string.Format("Podobjekat {0} objekta {1}. je pretvoren u objekat", _content.Id, _parent.Id);

            }
            public SubObjectToObjectState(LearningContent content, string originalId, LearningContent parent)
            {
                _content = content;
                _parent = parent;
                this.originalId = originalId;
            }
            protected override void DoRedo()
            {
                _parent.SubObjects.Remove(_content);
                _content.Parent = null;
                _content.Id = originalId;
                ProjectSingleton.Project.LearningContents.Add(_content);
                MainForm.Instance.RecreateMenu();
                MainForm.Instance.OpenSlide(_content);
            }

            protected override void DoUndo()
            {
                ProjectSingleton.Project.LearningContents.Remove(_content);
                _content.Parent = _parent;
                _content.Id = _parent.Id;
                _parent.SubObjects.Add(_content);
                MainForm.Instance.RecreateMenu();
                MainForm.Instance.OpenSlide(_content);
            }
        }


        private class ObjectToSubObjectState : IState
        {
            private readonly LearningContent _content;
            private readonly LearningContent _contentConverted;
            private readonly string originalId;

            public override string InfoString()
            {
                return string.Format("Objekat {0} je pretvoren u podobjekat objekta {1}", _content.Id, _contentConverted.Id);

            }
            public ObjectToSubObjectState(LearningContent content, string originalId, LearningContent contentConverted)
            {
                _content = content;
                _contentConverted = contentConverted;
                this.originalId = originalId;
            }
            protected override void DoRedo()
            {
                ProjectSingleton.Project.LearningContents.Remove(_content);
                _content.Parent = _contentConverted;
                _content.Id = _contentConverted.Id;
                _contentConverted.SubObjects.Add(_content);
                MainForm.Instance.RecreateMenu();
                MainForm.Instance.OpenSlide(_content);
            }

            protected override void DoUndo()
            {
                _contentConverted.SubObjects.Remove(_content);
                _content.Parent = null;
                _content.Id = originalId;
                ProjectSingleton.Project.LearningContents.Add(_content);
                MainForm.Instance.RecreateMenu();
                MainForm.Instance.OpenSlide(_content);
            }
        }

        private class MergeObjects : IState
        {
            private readonly List<LearningContent> contentAdded;

            public override string InfoString()
            {
                return string.Format("Izvršenje merge-a projekta");

            }
            public MergeObjects(List<LearningContent> contentAdded)
            {
                this.contentAdded = contentAdded;
            }
            protected override void DoRedo()
            {
                foreach (LearningContent content in contentAdded)
                {
                    content.Parent = null;
                    content.SubObjects.Clear();
                    ProjectSingleton.Project.LearningContents.Add(content);
                }
                MainForm.Instance.RecreateMenu();
                if (ProjectSingleton.Project.LearningContents.Count > 0)
                {
                    MainForm.Instance.OpenSlide(ProjectSingleton.Project.LearningContents.Last());
                }
            }

            protected override void DoUndo()
            {
                string[] ids = new string[contentAdded.Count];
                int i = 0;
                foreach (LearningContent content in contentAdded)
                {
                    ids[i] = new string(content.Id.ToCharArray());
                    i++;
                }
                i = 0;
                foreach (LearningContent content in contentAdded)
                {
                    if (ProjectSingleton.Project.LearningContents.Contains(content))
                    {
                        ProjectSingleton.Project.LearningContents.Remove(content);
                    }
                    foreach (LearningContent contentSub in ProjectSingleton.Project.LearningContents)
                    {
                        if (contentSub.SubObjects.Contains(content))
                        {
                            contentSub.SubObjects.Remove(content);
                        }
                    }
                    content.Id = ids[i];
                    i++;
                }
                MainForm.Instance.RecreateMenu();
                MainForm.Instance.CloseSlide();
                if (ProjectSingleton.Project.LearningContents.Count > 0)
                {
                    MainForm.Instance.OpenSlide(ProjectSingleton.Project.LearningContents.Last());
                }
            }
        }
        private class ContentMovedState : IState
        {
            public override string InfoString()
            {
                if (_source == null || _destination == null)
                {
                    return string.Format("Objekat LC{0} pomeren u objekat LC{1}.", _sourceIndex + 1, _destinationIndex + 1);
                }
                return string.Format("Podobjekat {0}/{1} pomeren u objekat {2}/{3}.", _source.TitleDescription,
                    _sourceIndex + 1, _destination.TitleDescription, _destinationIndex + 1);
            }

            private readonly LearningContent _content;
            private readonly LearningContent _source;
            private readonly int _sourceIndex;
            private readonly LearningContent _destination;
            private readonly int _destinationIndex;

            public ContentMovedState(LearningContent content, LearningContent source, int sourceIndex,
                LearningContent destination, int destinationIndex)
            {
                _content = content;
                _source = source;
                _sourceIndex = sourceIndex;
                _destination = destination;
                _destinationIndex = destinationIndex;
            }

            protected override void DoRedo()
            {
                if (_destination != null)
                {
                    _content.MoveTo(_destination, _destinationIndex);
                }
                else
                {
                    _content.MoveTo(_destinationIndex);
                }

                MainForm.Instance.RecreateMenu();
                if (ProjectSingleton.SelectedContent == _content)
                {
                    MainForm.Instance.OpenSlide(_content);
                }
            }

            protected override void DoUndo()
            {
                if (_source != null)
                {
                    _content.MoveTo(_source, _sourceIndex);
                }
                else
                {
                    _content.MoveTo(_sourceIndex);
                }

                MainForm.Instance.RecreateMenu();
                if (ProjectSingleton.SelectedContent == _content)
                {
                    MainForm.Instance.OpenSlide(_content);
                }
            }
        }

        public static void AddContentMovedState(LearningContent content, LearningContent source, int sourceIndex,
            LearningContent destination, int destinationIndex)
        {
            new ContentMovedState(content, source, sourceIndex, destination, destinationIndex).SetCurrent();
        }

        public static void AddContentMovedState(LearningContent content, int startIndex, int destinationIndex)
        {
            new ContentMovedState(content, null, startIndex, null, destinationIndex).SetCurrent();
        }

        public static void AddSubObjectState(LearningContent content, string originalId, LearningContent parent)
        {
            new SubObjectToObjectState(content, originalId, parent).SetCurrent();
        }
        public static void AddObjectToSubObjectState(LearningContent content, string originalId, LearningContent convertedToObject)
        {
            new ObjectToSubObjectState(content, originalId, convertedToObject).SetCurrent();
        }
        public static void AddMergeProjectsState(List<LearningContent> content)
        {
            new MergeObjects(content).SetCurrent();
        }

        
 
    }
}