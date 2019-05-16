﻿using System;
using System.Drawing;
using System.Xml.Serialization;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.sbmt.SubmitFilesContent")]
    public class LamsSubmitFiles : LamsTool
    {
        [Serializable]
        [XmlRoot(ElementName = "createdBy")]
        public class CreatedByClass
        {
            public CreatedByClass()
            {
                this.UserID = "1";
                this.FirstName = "Admin";
                this.LastName = "Admin";
                this.Login = "sysadmin";
                this.ContentID = "101";
                this.Finished = "false";
            }

            [XmlElement(ElementName = "userID")]
            public string UserID { get; set; }

            [XmlElement(ElementName = "firstName")]
            public string FirstName { get; set; }

            [XmlElement(ElementName = "lastName")]
            public string LastName { get; set; }

            [XmlElement(ElementName = "login")]
            public string Login { get; set; }

            [XmlElement(ElementName = "contentID")]
            public string ContentID { get; set; }

            [XmlElement(ElementName = "finished")]
            public string Finished { get; set; }
        }

        public LamsSubmitFiles()
        {
            this.ContentID = "101";
            this.Title = "";
            this.Instruction = "";
            this.LockOnFinished = "false";
            this.NotifyLearnersOnMarkRelease = "true";
            this.NotifyTeachersOnFileSubmit = "true";
            this.DefineLater = "false";
            this.ContentInUse = "false";
            this.LimitUpload = "false";
            this.LimitUploadNumber = "0";
            this.ReflectOnActivity = "false";
            this.ReflectInstructions = "";
            this.CreatedBy = new CreatedByClass();

        }

        public override string ToString()
        {
            return "Submit Files - " + Title;
        }

        [XmlElement(ElementName = "contentID")]
        public string ContentID { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "instruction")]
        public string Instruction { get; set; }

        [XmlElement(ElementName = "lockOnFinished")]
        public string LockOnFinished { get; set; }

        [XmlElement(ElementName = "notifyLearnersOnMarkRelease")]
        public string NotifyLearnersOnMarkRelease { get; set; }

        [XmlElement(ElementName = "notifyTeachersOnFileSubmit")]
        public string NotifyTeachersOnFileSubmit { get; set; }

        [XmlElement(ElementName = "defineLater")]
        public string DefineLater { get; set; }

        [XmlElement(ElementName = "contentInUse")]
        public string ContentInUse { get; set; }

        [XmlElement(ElementName = "limitUpload")]
        public string LimitUpload { get; set; }

        [XmlElement(ElementName = "limitUploadNumber")]
        public string LimitUploadNumber { get; set; }

        [XmlElement(ElementName = "reflectOnActivity")]
        public string ReflectOnActivity { get; set; }

        [XmlElement(ElementName = "reflectInstructions")]
        public string ReflectInstructions { get; set; }

        [XmlElement(ElementName = "createdBy")]
        public CreatedByClass CreatedBy { get; set; }

        [XmlIgnore]
        public override string TitleText
        {
            get { return Title; }
        }

        [XmlIgnore]
        public override Image Icon
        {
            get { return Resources.lms_submit_files; }
        }
        
        [XmlIgnore]
        public override string Description
        {
            get { return Instruction; }
        }

        [XmlIgnore]
        public override string ActivityTitle
        {
            get { return TitleText; }
        }

        [XmlIgnore]
        public override string HelpText
        {
            get { return "Learners submit files for assessment by the instructor. Scores and comments for each learner are recorded and may be exported as a spreadsheet."; }
        }

        [XmlIgnore]
        public override string HelpURL
        {
            get { return "http://wiki.lamsfoundation.org/display/lamsdocs/lasbmt11"; }
        }
        
        [XmlIgnore]
        public override long LearningLibraryID
        {
            get { return 18; }
        }

        [XmlIgnore]
        public override string ToolSignature
        {
            get { return "lasbmt11"; }
        }

        [XmlIgnore]
        public override long ToolID
        {
            get { return 18; }
        }

        [XmlIgnore]
        public override long ToolContentID
        {
            get { return long.Parse(ContentID); }
            set { ContentID = value.ToString(); }
        }

        [XmlIgnore]
        public override string ToolDisplayName
        {
            get { return "Submit File"; }
        }

        [XmlIgnore]
        public override long ToolVersion
        {
            get { return 20140520; }
        }

        [XmlIgnore]
        public override string AuthoringURL
        {
            get { return "tool/lasbmt11/authoring.do"; }
        }

        [XmlIgnore]
        public override string MonitoringURL
        {
            get { return "tool/lasbmt11/monitoring.do"; }
        }

        [XmlIgnore]
        public override long ActivityCategoryID
        {
            get { return 3; }
        }

        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get { return "tool/lasbmt11/images/icon_reportsubmission.swf"; }
        }
    }
}
