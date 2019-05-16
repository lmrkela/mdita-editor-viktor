using System;
using System.Windows.Forms;
using mDitaEditor.Dita.Forms;
using mDitaEditor.Project;
using mDitaEditor.Properties;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita.Controls
{
    partial class SlideListControl
    {
        partial class SlidePreviewControl
        {
            /// <summary>
            /// Meni koji se prikazuje desnim klikom na slajd za objekat ucenja.
            /// </summary>
            private static readonly ContextMenuStrip MenuObject;
            /// <summary>
            /// Meni koji se prikazuje desnim klikom na slajd za objekat ucenja.
            /// </summary>
            private static readonly ContextMenuStrip MenuObjectReduced;
            /// <summary>
            /// Meni koji se prikazuje desnim klikom na slajd za sekciju unutar objekta.
            /// </summary>
            private static readonly ContextMenuStrip MenuSection;
            /// <summary>
            /// Meni koji se prikazuje desnim klikom na slajd za sekciju unutar objekta.
            /// </summary>
            private static readonly ContextMenuStrip MenuSectionReduced;

            private static ToolStripMenuItem createAObject;

            private static ToolStripMenuItem createSubObjectFromObject;

            public static ToolStripMenuItem deleteObject;

            static SlidePreviewControl()
            {
                MenuObject = new ContextMenuStrip();
                MenuObject.Opening += Menu_Opening;
                MenuObject.Closed += Menu_Closed;
                MenuObject.Items.Add(new ToolStripMenuItem("Move up", Resources.arrowup, MoveObjectUp_Click));
                MenuObject.Items.Add(new ToolStripMenuItem("Move down", Resources.arrowdown, MoveObjectDown_Click));
                deleteObject = new ToolStripMenuItem("Delete", Resources.delete, DeleteObject_Click);
                MenuObject.Items.Add(deleteObject);
                MenuObject.Items.Add(new ToolStripSeparator());
                MenuObject.Items.Add(new ToolStripMenuItem("Paste", Resources.paste, PasteButton_Click));
                MenuObject.Items.Add(new ToolStripSeparator());
                MenuObject.Items.Add(new ToolStripMenuItem("Insert section below", Resources.newiconsmall, NewSection_Click));
                MenuObject.Items.Add(new ToolStripMenuItem("Insert subobject below", Resources.newiconsmall, NewSubobject_Click));
                MenuObject.Items.Add(new ToolStripSeparator());
                MenuObjectReduced = new ContextMenuStrip();
                MenuObjectReduced.Items.Add(new ToolStripMenuItem("Insert section below", Resources.newiconsmall, NewSection_Click));

                MenuSection = new ContextMenuStrip();
                MenuSection.Opening += Menu_Opening;
                MenuSection.Closed += Menu_Closed;
                MenuSection.Items.Add(new ToolStripMenuItem("Move up", Resources.arrowup, MoveSectionUp_Click));
                MenuSection.Items.Add(new ToolStripMenuItem("Move down", Resources.arrowdown, MoveSectionDown_Click));
                MenuSection.Items.Add(new ToolStripMenuItem("Delete", Resources.delete, DeleteSection_Click));
                MenuSection.Items.Add(new ToolStripMenuItem("Duplicate", Resources.duplicate, Duplicate_Click));
                MenuSection.Items.Add(new ToolStripSeparator());
                MenuSection.Items.Add(new ToolStripMenuItem("Cut", Resources.cut, CutButton_Click));
                MenuSection.Items.Add(new ToolStripMenuItem("Copy", Resources.copy, CopyButton_Click));
                MenuSection.Items.Add(new ToolStripMenuItem("Paste", Resources.paste, PasteButton_Click));
                MenuSection.Items.Add(new ToolStripSeparator());
                MenuSection.Items.Add(new ToolStripMenuItem("Insert section below", Resources.newiconsmall, NewSection_Click));

                MenuSectionReduced = new ContextMenuStrip();
                MenuSectionReduced.Opening += Menu_Opening;
                MenuSectionReduced.Closed += Menu_Closed;
                MenuSectionReduced.Items.Add(new ToolStripMenuItem("Move up", Resources.arrowup, MoveSectionUp_Click));
                MenuSectionReduced.Items.Add(new ToolStripMenuItem("Move down", Resources.arrowdown, MoveSectionDown_Click));
                MenuSectionReduced.Items.Add(new ToolStripMenuItem("Delete", Resources.delete, DeleteSection_Click));
                MenuSectionReduced.Items.Add(new ToolStripMenuItem("Duplicate", Resources.duplicate, Duplicate_Click));
                MenuSectionReduced.Items.Add(new ToolStripSeparator());
                MenuSectionReduced.Items.Add(new ToolStripMenuItem("Insert section below", Resources.newiconsmall, NewSection_Click));

                createAObject = new ToolStripMenuItem("Create object from subobject", Resources.newiconsmall, ConvertToObject_Click);

                createSubObjectFromObject = new ToolStripMenuItem("Create subobject from object", Resources.newiconsmall, ConvertFromObject_Click);
            }

            private static void Menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)sender;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                control.Invalidate();
            }

            private static void Menu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)sender;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                menu.Items[0].Enabled = control.Slide.CanMove(true);
                menu.Items[1].Enabled = control.Slide.CanMove(false);
                AddMenuItemIfSubobject(control);
            }

            /// <summary>
            /// Event koji konvertuje objekat u sub objekat
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private static void ConvertFromObject_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                LearningContent content = null;
                if (control.Slide is LearningContent)
                {
                    content = (LearningContent)control.Slide;
                }

                if (content.SubObjects.Count == 0)
                {
                    ChooseObjectForm objectChooser = new ChooseObjectForm(content);
                    if (objectChooser.ShowDialog() == DialogResult.OK)
                    {
                        if (objectChooser.LearningContent != null)
                        {
                            DitaClipboard.AddObjectToSubObjectState(content, content.Id, objectChooser.LearningContent);
                            ProjectSingleton.Project.LearningContents.Remove(content);
                            content.Id = objectChooser.LearningContent.Id;
                            content.Parent = objectChooser.LearningContent;
                            objectChooser.LearningContent.SubObjects.Add(content);
                        }
                    }
                    MainForm.Instance.RecreateMenu();
                    if (content != null)
                    {
                        MainForm.Instance.OpenSlide(content);
                    }
                }
                else
                {
                    MessageBox.Show("Objekat koji želite da pretvorite u pod-objekat ima pod-objekte pa se ne može predstaviti kao pod-objekat");
                }
            }
            /// <summary>
            /// Event koji konvertuje sub objekat u objekat
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private static void ConvertToObject_Click(object sender, EventArgs e)
            {
                if (ProjectSingleton.Project.LearningContents.Count < 14)
                {
                    ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                    SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                    LearningContent content = null;
                    if (control.Slide is LearningContent)
                    {
                        content = (LearningContent)control.Slide;
                    }
                    if (content != null)
                    {
                        LearningContent rootObject = content.Parent;
                        if (rootObject != null)
                        {
                            ProjectSingleton.Project.GetLastIdForObject(content);
                            DitaClipboard.AddSubObjectState(content, content.Id, rootObject);
                            rootObject.SubObjects.Remove(content);
                            content.Parent = null;
                            ProjectSingleton.Project.LearningContents.Add(content);
                        }
                    }
                    MainForm.Instance.RecreateMenu();
                    if (content != null)
                    {
                        MainForm.Instance.OpenSlide(content);
                    }
                }
                else
                {
                    MessageBox.Show("Imate već 14 objekata u projektu!");
                }
            }

            /// <summary>
            /// Ako je u pitanju podobjekat dodaje mogucnost pravljenja objekta od podobjekta
            /// </summary>
            /// <param name="control"></param>
            public static void AddMenuItemIfSubobject(SlidePreviewControl control)
            {
                LearningContent parent = null;
                if (control.Slide is LearningContent)
                {
                    bool isSub = true;
                    parent = (LearningContent)control.Slide;
                    foreach (LearningContent content in ProjectSingleton.Project.LearningContents)
                    {
                        if (content == parent)
                        {
                            isSub = false;
                        }
                    }
                    if (isSub)
                    {
                        MenuObject.Items.Add(createAObject);
                        MenuObject.Items.Remove(createSubObjectFromObject);
                    }
                    else {
                        MenuObject.Items.Remove(createAObject);
                        MenuObject.Items.Add(createSubObjectFromObject);
                    }
                }
            }

            private static void NewSection_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;

                LearningBase parent = null;
                int index = 0;
                if (control.Slide is LearningBase)
                {
                    parent = (LearningBase)control.Slide;
                }
                else if (control.Slide is Section)
                {
                    Section section = (Section)control.Slide;
                    parent = section.Parent;
                    index = parent.LearningBody.Sections.IndexOf(section) + 1;
                }

                var newSection = SectionsGuiUtil.AddNewSection("section-columns1", true, true, parent, index);
                MainForm.Instance.OpenSlide(newSection);
            }

            private static void NewSubobject_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                LearningContent content = control.Slide as LearningContent;

                if (content == null)
                {
                    return;
                }
                var parent = content.Parent;
                if (parent == null)
                {
                    parent = content;
                }
                int index = parent.SubObjects.IndexOf(content) + 1;

                LearningContent newObject = new LearningContent(parent, index);
                ProjectSingleton.SelectedContent = newObject;
                SectionsGuiUtil.AddNewSection("section-columns1", false);

                DitaClipboard.AddObjectAddedState(newObject);

                MainForm.Instance.CheckErrorsAndStatistics();
                MainForm.Instance.RecreateMenu();
                MainForm.Instance.OpenSlide(newObject);
                MainForm.Instance.slideList.setSectionActive();
            }

            private static void DeleteObject_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                LearningContent content = (LearningContent)control.Slide;
                DeleteObject(content, control);
            }


            public static void DeleteObject(LearningContent content, SlidePreviewControl control)
            {
                SlideListControl parent = control._parentList;
                DialogResult dialogResult = MessageBox.Show("Brisanjem objekata obrisaćete i njegove sekcije.",
                   "Obrisati objekat?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int index = MainForm.Instance.OpenSlideIndex;
                    DitaClipboard.AddObjectDeletedState(content);
                    content.Delete();
                    parent.DeleteSlide(control);
                    MainForm.Instance.OpenSlideIndex = index;
                    MainForm.Instance.CheckErrorsAndStatistics();
                }

            }
            private static void DeleteSection_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                if (control != null)
                {
                    Section section = (Section)control.Slide;
                    DeleteSection(section, control);
                }
            }

            public static void DeleteSection(Section section, SlidePreviewControl control)
            {
                SlideListControl parent = control._parentList;

                DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da obrišete sekciju?", "Brisanje sekcije",
                    MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int index = MainForm.Instance.OpenSlideIndex;
                    DitaClipboard.AddSectionDeletedState(section);
                    section.Delete();
                    parent.DeleteSlide(control);
                    MainForm.Instance.CheckErrorsAndStatistics();
                    if (parent.SelectedSlide == section)
                    {
                        MainForm.Instance.OpenSlideIndex = index;
                    }
                }
            }

            private static void MoveObjectUp_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                LearningContent content = (LearningContent)control.Slide;

                if (content.Parent == null)
                {
                    int index = ProjectSingleton.Project.LearningContents.IndexOf(content) - 1;
                    if (index < 0)
                    {
                        return;
                    }
                    MoveContent(content, index);
                }
                else
                {
                    int index = content.Parent.SubObjects.IndexOf(content) - 1;
                    if (index < 0)
                    {
                        return;
                    }
                    MoveContent(content, content.Parent, index);
                }
            }

            private static void MoveObjectDown_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                LearningContent content = (LearningContent)control.Slide;

                if (content.Parent == null)
                {
                    int index = ProjectSingleton.Project.LearningContents.IndexOf(content) + 2;
                    if (index > ProjectSingleton.Project.LearningContents.Count)
                    {
                        return;
                    }
                    MoveContent(content, index);
                }
                else
                {
                    int index = content.Parent.SubObjects.IndexOf(content) + 2;
                    if (index > content.Parent.SubObjects.Count)
                    {
                        return;
                    }
                    MoveContent(content, content.Parent, index);
                }
            }

            private static void MoveSectionUp_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                Section section = (Section)control.Slide;

                int index = section.Parent.LearningBody.Sections.IndexOf(section) - 1;
                if (index < 0)
                {
                    return;
                }
                MoveSection(section, section.Parent, index);
            }

            private static void MoveSectionDown_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                Section section = (Section)control.Slide;

                int index = section.Parent.LearningBody.Sections.IndexOf(section) + 2;
                if (index > section.Parent.LearningBody.Sections.Count)
                {
                    return;
                }
                MoveSection(section, section.Parent, index);
            }

            private static void CutButton_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                SlideListControl parent = control._parentList;

                int index = MainForm.Instance.OpenSlideIndex;
                Section section = (Section)control.Slide;
                DitaClipboard.CopiedSection = section;
                DitaClipboard.AddSectionDeletedState(section);
                section.Parent.LearningBody.Sections.Remove(section);

                if (parent.SelectedSlide == section)
                {
                    MainForm.Instance.OpenSlideIndex = index;
                }
                MainForm.Instance.CheckErrorsAndStatistics();

                parent.DeleteSlide(control);
            }

            private static void CopyButton_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;

                DitaClipboard.CopiedSection = control.Slide as Section;
            }

            private static void PasteButton_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                if (control != null)
                {
                    SlideListControl parent = control._parentList;
                    var learningObject = control.Slide as LearningBase;
                    if (learningObject == null)
                    {
                        learningObject = ((Section)control.Slide).Parent;
                    }
                    var section = DitaClipboard.Paste(learningObject);
                    if (section != null)
                    {
                        DitaClipboard.AddSectionAddedState(section);
                    }
                    parent.RecreateMenu();
                    MainForm.Instance.CheckErrorsAndStatistics();
                }
            }

            private static void Duplicate_Click(object sender, EventArgs e)
            {
                ContextMenuStrip menu = (ContextMenuStrip)((ToolStripMenuItem)sender).Owner;
                SlidePreviewControl control = (SlidePreviewControl)menu.SourceControl;
                if (control != null)
                {
                    SlideListControl parent = control._parentList;

                    Section learningSection = (Section)control.Slide;
                    Section sectionCopy = learningSection.Clone();
                    sectionCopy.Title = sectionCopy.Title + " - Kopija";
                    learningSection.Parent.LearningBody.Sections.InsertAfter(learningSection, sectionCopy);
                    sectionCopy.Parent = learningSection.Parent;

                    DitaClipboard.AddSectionAddedState(sectionCopy);

                    parent.RecreateMenu();
                    MainForm.Instance.CheckErrorsAndStatistics();
                }
            }
        }
    }
}