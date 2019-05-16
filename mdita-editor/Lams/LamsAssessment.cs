using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using mDitaEditor.Lams.Editor.Conditions;
using mDitaEditor.Lams.Editor.XMLExporter;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.assessment.model.Assessment")]
    public class LamsAssessment : LamsTool, IHasConditions
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
                this.Reference = "../..";
            }

            [XmlAttribute(AttributeName = "reference")]
            public string Reference { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "createdBy")]
        public class CreatedBy
        {
            public CreatedBy()
            {
                this.UserId = "1";
                this.FirstName = "Admin";
                this.LastName = "Admin";
                this.LoginName = "sysadmin";
                this.SessionFinished = "false";
                this.Assessment = new Assessment();
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
                this.Class = "org.lamsfoundation.lams.tool.assessment.util.SequencableComparator";
            }
            [XmlAttribute(AttributeName = "class")]
            public string Class { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "createBy")]
        public class CreateByAss
        {
            public CreateByAss()
            {
                this.Reference = "../../../createdBy";
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
                this.OptionString = "";
                this.OptionFloat = "0.0";
                this.AcceptedError = "0.0";
                this.Grade = 0;
                this.AnswerInt = "-1";
                this.AnswerBoolean = "false";
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
                this.Class = "tree-set";
                this.ComparatorAss = new ComparatorAss();
                this.AssessmentQuestionOption = new List<AssessmentQuestionOption>();
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
                this.Class = "tree-set";
                this.ComparatorAss = new ComparatorAss();
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
                this.Type = "";
                this.Title = "";
                this.Question = "";
                this.DefaultGrade = 1;
                this.PenaltyFactor = "0.0";
                this.AnswerRequired = "false";
                this.GeneralFeedback = "";
                this.MultipleAnswersAllowed = "false";
                this.FeedbackOnCorrect = "";
                this.FeedbackOnPartiallyCorrect = "";
                this.FeedbackOnIncorrect = "";
                this.Shuffle = "false";
                this.CaseSensitive = "false";
                this.CorrectAnswer = "false";
                this.AllowRichEditor = "false";
                this.CreateBy = new LamsShareResource.CreateBy();
                this.Options = new Options();
                this.Units = new Units();
                this.MaxWordsLimit = "0";
                this.MinWordsLimit = "0";
                this.AnswerFloat = "0.0";
                this.AnswerBoolean = "false";
                this.Grade = "0";
                this.Mark = "0.0";
                this.Penalty = "0.0";

            }

            public bool ShouldSerializeMark()
            {
                return this.Type != "6";
            }
            public bool ShouldSerializeAnswerBoolean()
            {
                return this.Type != "6";
            }
            public bool ShouldSerializeGrade()
            {
                return this.Type != "6";
            }
            public bool ShouldSerializePenalty()
            {
                return this.Type != "6";
            }
            public bool ShouldSerializeAnswerFloat()
            {
                return this.Type != "6";
            }
            public bool ShouldSerializeCreateBy()
            {
                return this.Type != "6";
            }
            public bool ShouldSerializeFeedbackOnPartiallyCorrect()
            {
                return this.Type != "6";
            }
            public bool ShouldSerializeFeedbackOnCorrect()
            {
                return this.Type != "6";
            }
            public bool ShouldSerializeFeedbackOnIncorrect()
            {
                return this.Type != "6";
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
                else if ( Type == "6" )
                {
                    return "Essay - " + Title;
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
            [XmlElement(ElementName = "createBy")]
            public LamsShareResource.CreateBy CreateBy { get; set; }
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
        public class Questions
        {
            public Questions()
            {
                this.Class = "tree-set";
                this.ComparatorAss = new ComparatorAss();
                this.AssessmentQuestion = new List<AssessmentQuestion>();

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

                this.Reference = "../../../questions/org.lamsfoundation.lams.tool.assessment.model.AssessmentQuestion";
            }

            public Question(string referenca)
            {
                this.Reference = referenca;

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
                this.Question = new Question();
                this.SequenceId = idQuestion + "";
                this.Type = "0";
                this.DefaultGrade = grade + "";
                this.RandomQuestion = "true";


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
        public class QuestionReferencesClass
        {
            public QuestionReferencesClass()
            {
                this.Class = "tree-set";
                this.ComparatorAss = new ComparatorAss();
                this.QuestionReference = new List<QuestionReference>();

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
        public class OverallFeedbacksClass
        {
            public OverallFeedbacksClass()
            {
                this.Class = "tree-set";
                this.ComparatorAss = new ComparatorAss();


            }
            [XmlElement(ElementName = "comparator")]
            public ComparatorAss ComparatorAss { get; set; }
            [XmlAttribute(AttributeName = "class")]
            public string Class { get; set; }
        }
        public LamsAssessment()
        {

            this.ContentId = "101";
            this.Title = "";
            this.Instructions = "";
            this.UseSelectLeaderToolOuput = "false";
            this.TimeLimit = "0";
            this.QuestionsPerPage = "0";
            this.AttemptsAllowed = "0";
            this.PassingMark = "0";
            this.Shuffled = "true";
            this.Numbered = "true";
            this.AllowQuestionFeedback = "true";
            this.AllowOverallFeedbackAfterQuestion = "false";
            this.AllowRightAnswersAfterQuestion = "true";
            this.AllowWrongAnswersAfterQuestion = "true";
            this.AllowGradesAfterAttempt = "true";
            this.AllowHistoryResponses = "false";
            this.DisplaySummary = "true";
            this.DefineLater = "false";
            this.NotifyTeachersOnAttemptCompletion = "true";
            this.ReflectOnActivity = "false";
            this.ReflectInstructions = "";
            this.CreatedByAss = new CreatedBy();
            this.QuestionsAss = new Questions();
            this.QuestionReferences = new QuestionReferencesClass();
            this.OverallFeedbacks = new OverallFeedbacksClass();
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
        public CreatedBy CreatedByAss { get; set; }
        [XmlElement(ElementName = "questions")]
        public Questions QuestionsAss { get; set; }
        [XmlElement(ElementName = "questionReferences")]
        public QuestionReferencesClass QuestionReferences { get; set; }
        [XmlElement(ElementName = "overallFeedbacks")]
        public OverallFeedbacksClass OverallFeedbacks { get; set; }

        [XmlIgnore]
        public override string TitleText { get { return Title; } }

        public override string ToString()
        {
            return "Assessment - " + Title;
        }
        [XmlIgnore]
        public override Image Icon { get { return Resources.lms_assesment; } }

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

        [XmlIgnore]
        public LamsConditionType[] ConditionsAvailable
        {
            get
            {
                var max = QuestionsAss.AssessmentQuestion.Count;
                if (max > 0)
                {
                    max *= QuestionsAss.AssessmentQuestion[0].DefaultGrade;
                }
                return new[]
                {
                    new LamsConditionType("Number of attempts", ConditionDTO.ConditionName.NumberOfAttempts, ConditionDTO.ValueType.Long),
                    new LamsConditionType("Time taken", ConditionDTO.ConditionName.TimeTaken, ConditionDTO.ValueType.Long),
                    new LamsConditionType("Total score", ConditionDTO.ConditionName.TotalScore, ConditionDTO.ValueType.Long, 0, max)
                };
            }
        }
    }

}
