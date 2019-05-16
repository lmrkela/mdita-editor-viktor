using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata.LAMS
{
    [Serializable]
    [XmlRoot(ElementName = "created")]
    public class CreatedTF
    {
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "assessment")]
    public class Assessment
    {
        public Assessment()
        {
            Reference = "../..";
        }

        [XmlAttribute(AttributeName = "reference")]
        public string Reference { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "createdBy")]
    public class CreatedByAss
    {
        public CreatedByAss()
        {
            UserId = "1";
            FirstName = "Admin";
            LastName = "Admin";
            LoginName = "sysadmin";
            SessionFinished = "false";
            Assessment = new Assessment();
        }
        [XmlElement(ElementName = "userId")]
        public string UserId { get; set; }
        [XmlElement(ElementName = "firstName")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "lastName")]
        public string LastName { get; set; }
        [XmlElement(ElementName = "loginName")]
        public string LoginName { get; set; }
        [XmlElement(ElementName = "sessionFinished")]
        public string SessionFinished { get; set; }
        [XmlElement(ElementName = "assessment")]
        public Assessment Assessment { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "comparator")]
    public class ComparatorAss
    {
        public ComparatorAss()
        {
            Class = "org.lamsfoundation.lams.tool.assessment.util.SequencableComparator";
        }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "createDate")]
    public class CreateDateAss
    {
        public CreateDateAss()
        {
            Class = "sql-timestamp";
            Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");
        }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "createBy")]
    public class CreateByAss
    {
        public CreateByAss()
        {
            Reference = "../../../createdBy";
        }
        [XmlAttribute(AttributeName = "reference")]
        public string Reference { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.assessment.model.AssessmentQuestionOption")]
    public class AssessmentQuestionOption
    {

        public AssessmentQuestionOption()
        {
            OptionString = "";
            OptionFloat = "0.0";
            AcceptedError = "0.0";
            Grade = 0;
            AnswerInt = "-1";
            AnswerBoolean = "false";
        }

        [XmlElement(ElementName = "sequenceId")]
        public string SequenceId { get; set; }
        [XmlElement(ElementName = "optionString")]
        public string OptionString { get; set; }
        [XmlElement(ElementName = "optionFloat")]
        public string OptionFloat { get; set; }
        [XmlElement(ElementName = "acceptedError")]
        public string AcceptedError { get; set; }
        [XmlElement(ElementName = "grade")]
        public double Grade { get; set; }
        [XmlElement(ElementName = "answerInt")]
        public string AnswerInt { get; set; }
        [XmlElement(ElementName = "answerBoolean")]
        public string AnswerBoolean { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "options")]
    public class Options
    {

        public Options()
        {
            Class = "tree-set";
            ComparatorAss = new ComparatorAss();
            AssessmentQuestionOption = new List<AssessmentQuestionOption>();
        }

        [XmlElement(ElementName = "comparator")]
        public ComparatorAss ComparatorAss { get; set; }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.assessment.model.AssessmentQuestionOption")]
        public List<AssessmentQuestionOption> AssessmentQuestionOption { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "units")]
    public class Units
    {

        public Units()
        {
            Class = "tree-set";
            ComparatorAss = new ComparatorAss();
        }
        [XmlElement(ElementName = "comparator")]
        public ComparatorAss ComparatorAss { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.assessment.model.AssessmentQuestion")]
    public class AssessmentQuestion
    {

        public AssessmentQuestion()
        {
            Type = "";
            Title = "";
            Question = "";
            DefaultGrade = 1;
            PenaltyFactor = "0.0";
            AnswerRequired = "false";
            GeneralFeedback = "";
            MultipleAnswersAllowed = "false";
            FeedbackOnCorrect = "";
            FeedbackOnPartiallyCorrect = "";
            FeedbackOnIncorrect = "";
            Shuffle = "false";
            CaseSensitive = "false";
            CorrectAnswer = "false";
            AllowRichEditor = "false";
            CreateDate = new CreateDate();
            CreateBy = new CreateBy();
            Options = new Options();
            Units = new Units();
            MaxWordsLimit = "0";
            MinWordsLimit = "0";
            AnswerFloat = "0.0";
            AnswerBoolean = "false";
            Grade = "0";
            Mark = "0.0";
            Penalty = "0.0";

        }

        public override string ToString()
        {
            if (Type == "1")
            {
                return "Multiple Choice - " + Title;
            }
            else if (Type == "5")
            {
                return "True/False - " + Title;
            }
            else if (Type == "4")
            {
                return "Numerical Answer - " + Title;
            }
            else
            {
                return Title;
            }
        }

        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "question")]
        public string Question { get; set; }
        [XmlElement(ElementName = "sequenceId")]
        public string SequenceId { get; set; }
        [XmlElement(ElementName = "defaultGrade")]
        public int DefaultGrade { get; set; }
        [XmlElement(ElementName = "penaltyFactor")]
        public string PenaltyFactor { get; set; }
        [XmlElement(ElementName = "answerRequired")]
        public string AnswerRequired { get; set; }
        [XmlElement(ElementName = "generalFeedback")]
        public string GeneralFeedback { get; set; }
        [XmlElement(ElementName = "multipleAnswersAllowed")]
        public string MultipleAnswersAllowed { get; set; }
        [XmlElement(ElementName = "feedbackOnCorrect")]
        public string FeedbackOnCorrect { get; set; }
        [XmlElement(ElementName = "feedbackOnPartiallyCorrect")]
        public string FeedbackOnPartiallyCorrect { get; set; }
        [XmlElement(ElementName = "feedbackOnIncorrect")]
        public string FeedbackOnIncorrect { get; set; }
        [XmlElement(ElementName = "shuffle")]
        public string Shuffle { get; set; }
        [XmlElement(ElementName = "caseSensitive")]
        public string CaseSensitive { get; set; }
        [XmlElement(ElementName = "correctAnswer")]
        public string CorrectAnswer { get; set; }
        [XmlElement(ElementName = "allowRichEditor")]
        public string AllowRichEditor { get; set; }
        [XmlElement(ElementName = "createDate")]
        public CreateDate CreateDate { get; set; }
        [XmlElement(ElementName = "createBy")]
        public CreateBy CreateBy { get; set; }
        [XmlElement(ElementName = "options")]
        public Options Options { get; set; }
        [XmlElement(ElementName = "units")]
        public Units Units { get; set; }
        [XmlElement(ElementName = "maxWordsLimit")]
        public string MaxWordsLimit { get; set; }
        [XmlElement(ElementName = "minWordsLimit")]
        public string MinWordsLimit { get; set; }
        [XmlElement(ElementName = "answerFloat")]
        public string AnswerFloat { get; set; }
        [XmlElement(ElementName = "answerBoolean")]
        public string AnswerBoolean { get; set; }
        [XmlElement(ElementName = "grade")]
        public string Grade { get; set; }
        [XmlElement(ElementName = "mark")]
        public string Mark { get; set; }
        [XmlElement(ElementName = "penalty")]
        public string Penalty { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "questions")]
    public class QuestionsAss
    {
        public QuestionsAss()
        {
            Class = "tree-set";
            ComparatorAss = new ComparatorAss();
            AssessmentQuestion = new List<AssessmentQuestion>();

        }

        [XmlElement(ElementName = "comparator")]
        public ComparatorAss ComparatorAss { get; set; }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.assessment.model.AssessmentQuestion")]
        public List<AssessmentQuestion> AssessmentQuestion { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "question")]
    public class Question
    {
        public Question()
        {

            Reference = "../../../questions/org.lamsfoundation.lams.tool.assessment.model.AssessmentQuestion";
        }

        public Question(string referenca)
        {
            Reference = referenca;

        }
        [XmlAttribute(AttributeName = "reference")]
        public string Reference { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.assessment.model.QuestionReference")]
    public class QuestionReference
    {
        public QuestionReference()
        {

        }
        public QuestionReference(int idQuestion, int grade)
        {
            Question = new Question();
            SequenceId = idQuestion + "";
            Type = "0";
            DefaultGrade = grade + "";
            RandomQuestion = "true";


        }
        [XmlElement(ElementName = "question")]
        public Question Question { get; set; }
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "sequenceId")]
        public string SequenceId { get; set; }
        [XmlElement(ElementName = "defaultGrade")]
        public string DefaultGrade { get; set; }
        [XmlElement(ElementName = "randomQuestion")]
        public string RandomQuestion { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "questionReferences")]
    public class QuestionReferences
    {
        public QuestionReferences()
        {
            Class = "tree-set";
            ComparatorAss = new ComparatorAss();
            QuestionReference = new List<QuestionReference>();

        }
        [XmlElement(ElementName = "comparator")]
        public ComparatorAss ComparatorAss { get; set; }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.assessment.model.QuestionReference")]
        public List<QuestionReference> QuestionReference { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "overallFeedbacks")]
    public class OverallFeedbacks
    {
        public OverallFeedbacks()
        {
            Class = "tree-set";
            ComparatorAss = new ComparatorAss();


        }
        [XmlElement(ElementName = "comparator")]
        public ComparatorAss ComparatorAss { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.assessment.model.Assessment")]
    public class LamsAssessment : LamsTool
    {
        public LamsAssessment()
        {

            ContentId = "101";
            Title = "";
            Instructions = "";
            UseSelectLeaderToolOuput = "false";
            TimeLimit = "0";
            QuestionsPerPage = "0";
            AttemptsAllowed = "0";
            PassingMark = "0";
            Shuffled = "true";
            Numbered = "true";
            AllowQuestionFeedback = "true";
            AllowOverallFeedbackAfterQuestion = "false";
            AllowRightAnswersAfterQuestion = "true";
            AllowWrongAnswersAfterQuestion = "true";
            AllowGradesAfterAttempt = "true";
            AllowHistoryResponses = "false";
            DisplaySummary = "true";
            DefineLater = "false";
            NotifyTeachersOnAttemptCompletion = "true";
            ReflectOnActivity = "false";
            ReflectInstructions = "";
            CreatedByAss = new CreatedByAss();
            QuestionsAss = new QuestionsAss();
            QuestionReferences = new QuestionReferences();
            OverallFeedbacks = new OverallFeedbacks();
        }

        [XmlElement(ElementName = "contentId")]
        public string ContentId { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "instructions")]
        public string Instructions { get; set; }
        [XmlElement(ElementName = "useSelectLeaderToolOuput")]
        public string UseSelectLeaderToolOuput { get; set; }
        [XmlElement(ElementName = "timeLimit")]
        public string TimeLimit { get; set; }
        [XmlElement(ElementName = "questionsPerPage")]
        public string QuestionsPerPage { get; set; }
        [XmlElement(ElementName = "attemptsAllowed")]
        public string AttemptsAllowed { get; set; }
        [XmlElement(ElementName = "passingMark")]
        public string PassingMark { get; set; }
        [XmlElement(ElementName = "shuffled")]
        public string Shuffled { get; set; }
        [XmlElement(ElementName = "numbered")]
        public string Numbered { get; set; }
        [XmlElement(ElementName = "allowQuestionFeedback")]
        public string AllowQuestionFeedback { get; set; }
        [XmlElement(ElementName = "allowOverallFeedbackAfterQuestion")]
        public string AllowOverallFeedbackAfterQuestion { get; set; }
        [XmlElement(ElementName = "allowRightAnswersAfterQuestion")]
        public string AllowRightAnswersAfterQuestion { get; set; }
        [XmlElement(ElementName = "allowWrongAnswersAfterQuestion")]
        public string AllowWrongAnswersAfterQuestion { get; set; }
        [XmlElement(ElementName = "allowGradesAfterAttempt")]
        public string AllowGradesAfterAttempt { get; set; }
        [XmlElement(ElementName = "allowHistoryResponses")]
        public string AllowHistoryResponses { get; set; }
        [XmlElement(ElementName = "displaySummary")]
        public string DisplaySummary { get; set; }
        [XmlElement(ElementName = "defineLater")]
        public string DefineLater { get; set; }
        [XmlElement(ElementName = "notifyTeachersOnAttemptCompletion")]
        public string NotifyTeachersOnAttemptCompletion { get; set; }
        [XmlElement(ElementName = "reflectOnActivity")]
        public string ReflectOnActivity { get; set; }
        [XmlElement(ElementName = "reflectInstructions")]
        public string ReflectInstructions { get; set; }
        [XmlElement(ElementName = "createdBy")]
        public CreatedByAss CreatedByAss { get; set; }
        [XmlElement(ElementName = "questions")]
        public QuestionsAss QuestionsAss { get; set; }
        [XmlElement(ElementName = "questionReferences")]
        public QuestionReferences QuestionReferences { get; set; }
        [XmlElement(ElementName = "overallFeedbacks")]
        public OverallFeedbacks OverallFeedbacks { get; set; }

        [XmlIgnore]
        public override string TitleText { get { return Title; } }

        public override string ToString()
        {
            return "Assessment - " + Title;
        }
        [XmlIgnore]
        public override Image Icon { get { return null; } }

        [XmlIgnore]
        public override string Description
        {
            get { return Instructions; }
        }

        [XmlIgnore]
        public override string ActivityTitle
        {
            get { return Title; }
        }

        [XmlIgnore]
        public override string HelpText
        {
            get { return "Create questions to assess learners."; }
        }

        [XmlIgnore]
        public override string HelpURL
        {
            get { return "http://wiki.lamsfoundation.org/display/lamsdocs/laasse10"; }
        }

        [XmlIgnore]
        public override long LearningLibraryID
        {
            get { return 1; }
        }

        [XmlIgnore]
        public override string ToolSignature
        {
            get { return "laasse10"; }
        }

        [XmlIgnore]
        public override long ToolID
        {
            get { return 1; }
        }

        [XmlIgnore]
        public override long ToolContentID
        {
            get { return long.Parse(ContentId); }
            set { ContentId = value.ToString(); }
        }

        [XmlIgnore]
        public override string ToolDisplayName
        {
            get { return "Assessment"; }
        }

        [XmlIgnore]
        public override long ToolVersion
        {
            get { return 20140707; }
        }

        [XmlIgnore]
        public override string AuthoringURL
        {
            get { return "tool/laasse10/authoring/start.do"; }
        }

        [XmlIgnore]
        public override string MonitoringURL
        {
            get { return "tool/laasse10/monitoring/summary.do"; }
        }

        [XmlIgnore]
        public override long ActivityCategoryID
        {
            get { return 3; }
        }

        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get { return "tool/laasse10/images/icon_assessment.swf"; }
        }
    }

}
