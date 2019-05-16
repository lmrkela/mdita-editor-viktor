using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class JavaGraderGui : Form
    {
        private static readonly string JAVAGRADER_COMPILER_LOCATION = Program.ProgramPath + "\\JavaGraderCompiler.jar";
        private static readonly string JAVA_LOCATION = Program.ProgramPath + "\\jre7\\bin\\java.exe";

        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        System.Windows.Forms.Timer paramTimer = new System.Windows.Forms.Timer();

        LamsJavaGrader grader;

        LamsJavaGrader.JavagraderQuestion editQuestion = null;
        public LearningBase LearningObject;
        bool isEdit = false;
        bool isEditTool= false;
        TextBox activeTextBox = new TextBox();
        public JavaGraderGui(LearningBase selectedObject, LamsJavaGrader grader = null)
        {
            Icon = Icon.FromHandle(Resources.java24.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();
            timer1.Interval = 700;
            timer1.Tick += Timer_Tick;
            paramTimer.Interval = 300;
            paramTimer.Tick += ParamTimer_Tick;
            if(grader != null)
            {
                this.grader = grader;
                ReloadList();
                isEditTool = true;
                txtNaslov.Text = grader.Name;
            }
            else
            {
                this.grader = new LamsJavaGrader();
            }
        }

        private void ParamTimer_Tick(object sender, EventArgs e)
        {
            paramTimer.Stop();
            testParam(activeTextBox);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            testMethod();
        }

        private void fastColoredTextBox1_Load(object sender, EventArgs e)
        {

        }

        private void JavaGraderGUI_Load(object sender, EventArgs e)
        {

        }


        public void putStringToRightTextBox(TextBox textBox, string param)
        {
            switch (textBox.Name)
            {
                case "txtParam1":
                    txtResults1.Text = param;
                    break;
                case "txtParam2":
                    txtResults2.Text = param;
                    break;
                case "txtParam3":
                    txtResults3.Text = param;
                    break;
                case "txtParam4":
                    txtResults4.Text = param;
                    break;
                case "txtParam5":
                    txtResults5.Text = param;
                    break;
                case "txtParam6":
                    txtResults6.Text = param;
                    break;
                case "txtParam7":
                    txtResults7.Text = param;
                    break;
                case "txtParam8":
                    txtResults8.Text = param;
                    break;
                case "txtParam9":
                    txtResults9.Text = param;
                    break;
                case "txtParam10":
                    txtResults10.Text = param;
                    break;
            }
        }

        public void testParam(TextBox param)
        {

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                string methodToTest = txtAnswer.Text;
                methodToTest = methodToTest.Replace(System.Environment.NewLine, " ");
                methodToTest = methodToTest.Replace("\"", "\\\"");
                string paramTxt = "";
                paramTxt = param.Text.Replace(System.Environment.NewLine, " ");
                paramTxt = paramTxt.Replace("\"", "\\\"");
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = JAVA_LOCATION,
                        Arguments = "-jar \"" + JAVAGRADER_COMPILER_LOCATION + "\" \"parameter\" \"" + methodToTest + "\" \"" + paramTxt + "\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        Verb = "runas",
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    try
                    {
                        string line = proc.StandardOutput.ReadLine();
                        this.Invoke((MethodInvoker)delegate
                        {
                            putStringToRightTextBox(param, line);
                        });
                    }
                    catch { }
                }
            }).Start();
        }


        public void testMethod()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                string methodToTest = txtAnswer.Text;
                methodToTest = methodToTest.Replace(Environment.NewLine, " ");
                methodToTest = methodToTest.Replace("\"", "\\\"");
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = JAVA_LOCATION,
                        Arguments = "-jar \"" + JAVAGRADER_COMPILER_LOCATION + "\" \"method\" \"" + methodToTest + "\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        Verb = "runas",
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    try
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            if (line == "true")
                            {
                                picBoxCorrect.Image = Properties.Resources.correct;
                            }
                            else
                            {
                                picBoxCorrect.Image = Properties.Resources.delete;
                            }
                        });
                    }
                    catch  { }
                }
            }).Start();
        }
        public string getMethodHead(string method)
        {
            int x = method.IndexOf("{");
            return method.Substring(0, x);
        }

        public string getMethodName(string method)
        {
            int x = method.IndexOf("(");
            string name = method.Substring(0, x);
            if (name.Contains("public"))
            {
                name = name.Replace("public", "");
            }
            if (name.Contains("private"))
            {
                name = name.Replace("private", "");
            }
            if (name.Contains("protected"))
            {
                name = name.Replace("protected", "");
            }
            if (name.Contains("byte"))
            {
                name = name.Replace("byte", "");
            }
            if (name.Contains("short"))
            {
                name = name.Replace("short", "");
            }
            if (name.Contains("int"))
            {
                name = name.Replace("int", "");
            }
            if (name.Contains("long"))
            {
                name = name.Replace("long", "");
            }
            if (name.Contains("float"))
            {
                name = name.Replace("float", "");
            }
            if (name.Contains("double"))
            {
                name = name.Replace("double", "");
            }
            if (name.Contains("char"))
            {
                name = name.Replace("char", "");
            }
            if (name.Contains("String"))
            {
                name = name.Replace("String", "");
            }
            if (name.Contains("boolean"))
            {
                name = name.Replace("boolean", "");
            }
            if (name.Contains("[]"))
            {
                name = name.Replace("[]", "");
            }

            name = name.Trim();
            return name;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtTekstPitanja.Text == "")
            {
                MessageBox.Show("Niste uneli tekst pitanja");
                return;
            }
            if (txtAnswer.Text == "")
            {
                MessageBox.Show("Niste uneli metodu");
                return;
            }
            if (!isEdit) {
                LamsJavaGrader.JavagraderQuestion question = new LamsJavaGrader.JavagraderQuestion();


                if (grader.JavagraderQuestions.JavagraderQuestion.Count != 0)
                {
                    question.OrderId = grader.JavagraderQuestions.JavagraderQuestion.Count+1;
                }
                 question.Method = txtAnswer.Text;
                question.Text = txtTekstPitanja.Text;
                //Parameters
                question.Params1 = txtParam1.Text;
                question.Params2 = txtParam2.Text;
                question.Params3 = txtParam3.Text;
                question.Params4 = txtParam4.Text;
                question.Params5 = txtParam5.Text;
                question.Params6 = txtParam6.Text;
                question.Params7 = txtParam7.Text;
                question.Params8 = txtParam8.Text;
                question.Params9 = txtParam9.Text;
                //Results
                question.Params10 = txtParam10.Text;
                question.Returns1 = txtResults1.Text;
                question.Returns2 = txtResults2.Text;
                question.Returns3 = txtResults3.Text;
                question.Returns4 = txtResults4.Text;
                question.Returns5 = txtResults5.Text;
                question.Returns6 = txtResults6.Text;
                question.Returns7 = txtResults7.Text;
                question.Returns8 = txtResults8.Text;
                question.Returns9 = txtResults9.Text;
                question.Returns10 = txtResults10.Text;
                // Methods
                question.MethodName = getMethodName(question.Method);
                question.MethodHead = getMethodHead(question.Method);
                // JavagraderQuestions
                grader.JavagraderQuestions.JavagraderQuestion.Add(question);
            }
            else
            {
                editQuestion.Method = txtAnswer.Text;
                editQuestion.Text = txtTekstPitanja.Text;
                //Parameters
                editQuestion.Params1 = txtParam1.Text;
                editQuestion.Params2 = txtParam2.Text;
                editQuestion.Params3 = txtParam3.Text;
                editQuestion.Params4 = txtParam4.Text;
                editQuestion.Params5 = txtParam5.Text;
                editQuestion.Params6 = txtParam6.Text;
                editQuestion.Params7 = txtParam7.Text;
                editQuestion.Params8 = txtParam8.Text;
                editQuestion.Params9 = txtParam9.Text;
                //Results
                editQuestion.Params10 = txtParam10.Text;
                editQuestion.Returns1 = txtResults1.Text;
                editQuestion.Returns2 = txtResults2.Text;
                editQuestion.Returns3 = txtResults3.Text;
                editQuestion.Returns4 = txtResults4.Text;
                editQuestion.Returns5 = txtResults5.Text;
                editQuestion.Returns6 = txtResults6.Text;
                editQuestion.Returns7 = txtResults7.Text;
                editQuestion.Returns8 = txtResults8.Text;
                editQuestion.Returns9 = txtResults9.Text;
                editQuestion.Returns10 = txtResults10.Text;
                // Methods
                editQuestion.MethodName = getMethodName(editQuestion.Method);
                editQuestion.MethodHead = getMethodHead(editQuestion.Method);
            }
            ReloadList();
            ResetFields();
            isEdit = false;
            editQuestion = null;
        }

        public void ResetFields()
        {
            foreach (Control con in this.Controls)
            {
                if (con is TextBox)
                {
                    if (con.Name.StartsWith("txtParam") || con.Name.StartsWith("txtResults") || con.Name.StartsWith("txtTekstPitanja"))
                    {
                        con.Text = "";
                    }
                }
                if (con is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    con.Text = "";
                }
            }
        }

        private void txtAnswer_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        private void txtParam1_TextChanged(object sender, EventArgs e)
        {
            activeTextBox = (TextBox)sender;
            paramTimer.Stop();
            paramTimer.Start();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            LamsJavaGrader.JavagraderQuestion question = (LamsJavaGrader.JavagraderQuestion)listBox1.SelectedItem;
            if(question != null)
            {
                isEdit = true;
                editQuestion = question;
                LoadEdit(editQuestion);
            }
        }

        public void ReloadList()
        {
            listBox1.Items.Clear();
            foreach(LamsJavaGrader.JavagraderQuestion que in grader.JavagraderQuestions.JavagraderQuestion){
                listBox1.Items.Add(que);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            LamsJavaGrader.JavagraderQuestion question = (LamsJavaGrader.JavagraderQuestion)listBox1.SelectedItem;
            if (question != null)
            {
                listBox1.Items.Remove(question);
                grader.JavagraderQuestions.JavagraderQuestion.Remove(question);
            }
            ReloadList();
            isEdit = false;
            editQuestion = null;
            ResetFields();
            
        }

        public void MoveCom(bool up)
        {
            if (listBox1.SelectedItem != null)
            {
                var list = grader.JavagraderQuestions.JavagraderQuestion;
                var temp = listBox1.SelectedItem as LamsJavaGrader.JavagraderQuestion;
                int index = list.IndexOf(listBox1.SelectedItem as LamsJavaGrader.JavagraderQuestion);
                int newIndex = index + (up ? -1 : 1);

                if (newIndex < 0 || newIndex >= list.Count)
                {
                    return;
                }


                list[index] = list[newIndex];
                list[newIndex] = temp;

                int orderTemp = list[index].OrderId;
                int order = list[newIndex].OrderId;
                list[index].OrderId = order;
                list[newIndex].OrderId = orderTemp;



            }
            ReloadList();
        }

        public void LoadEdit(LamsJavaGrader.JavagraderQuestion question)
        {
            //Params
            txtParam1.Text = question.Params1;
            txtParam2.Text = question.Params2;
            txtParam3.Text = question.Params3;
            txtParam4.Text = question.Params4;
            txtParam5.Text = question.Params5;
            txtParam6.Text = question.Params6;
            txtParam7.Text = question.Params7;
            txtParam8.Text = question.Params8;
            txtParam9.Text = question.Params9;
            txtParam10.Text = question.Params10;
            //Results
            txtResults1.Text = question.Returns1;
            txtResults2.Text = question.Returns2;
            txtResults3.Text = question.Returns3;
            txtResults4.Text = question.Returns4;
            txtResults5.Text = question.Returns5;
            txtResults6.Text = question.Returns6;
            txtResults7.Text = question.Returns7;
            txtResults8.Text = question.Returns8;
            txtResults9.Text = question.Returns9;
            txtResults10.Text = question.Returns10;
            //Textpitanja
            txtTekstPitanja.Text = question.Text;
            txtAnswer.Text = question.Method;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveCom(true);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveCom(false);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool isError = false;
            if (grader.Name == "")
            {
                MessageBox.Show("Niste definisali naslov za javagrader");
                isError = true;
            }
            if (grader.JavagraderQuestions.JavagraderQuestion.Count ==0)
            {
                MessageBox.Show("Niste definisali pitanja za Javagrader");
                isError = true;
            }

            if (!isError)
            {
                if (!isEditTool)
                {
                    LearningObject.ToolList.Add(this.grader);
                }
                this.Close();
                DialogResult = DialogResult.OK;
            }
        }

        private void txtNaslov_TextChanged(object sender, EventArgs e)
        {
            grader.Name = txtNaslov.Text;
        }
    }
}
