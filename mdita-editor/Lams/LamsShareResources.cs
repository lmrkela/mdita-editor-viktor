using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.rsrc.model.Resource")]
    public class LamsShareResource : LamsTool
    {
        [Serializable]
        [XmlRoot(ElementName = "resource")]
        public class Resource
        {

            public Resource()
            {
                this.Reference = "../..";
            }
            [XmlAttribute(AttributeName = "reference")]
            public string Reference { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "createdBy")]
        public class CreatedByShareClass
        {

            public CreatedByShareClass()
            {
                this.UserId = "1";
                this.FirstName = "Admin";
                this.LastName = "Admin";
                this.LoginName = "sysadmin";
                this.SessionFinished = "false";
                this.Resource = new Resource();

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
            [XmlElement(ElementName = "resource")]
            public Resource Resource { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.rsrc.model.ResourceItemInstruction")]
        public class ResourceItemInstruction
        {

            public ResourceItemInstruction()
            {
                this.Description = "";

            }
            [XmlElement(ElementName = "sequenceId")]
            public string SequenceId { get; set; }
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "itemInstructions")]
        public class ItemInstructions
        {
            public ItemInstructions()
            {
                this.ResourceItemInstruction = new List<ResourceItemInstruction>();

            }
            [XmlElement(ElementName = "org.lamsfoundation.lams.tool.rsrc.model.ResourceItemInstruction")]
            public List<ResourceItemInstruction> ResourceItemInstruction { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "createBy")]
        public class CreateBy
        {
            public CreateBy()
            {
                this.Reference = "../../../createdBy";
            }
            [XmlAttribute(AttributeName = "reference")]
            public string Reference { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.rsrc.model.ResourceItem")]
        public class ResourceItem
        {

            public ResourceItem()
            {
                this.Type = "1";
                this.Title = "";
                this.Url = "";
                this.OpenUrlNewWindow = "false";
                this.IsHide = "false";
                this.IsCreateByAuthor = "true";
                this.CreateBy = new CreateBy();
                this.Complete = "false";

            }
            [XmlElement(ElementName = "type")]
            public string Type { get; set; }
            [XmlElement(ElementName = "title")]
            public string Title { get; set; }
            [XmlElement(ElementName = "url")]
            public string Url { get; set; }
            [XmlElement(ElementName = "openUrlNewWindow")]
            public string OpenUrlNewWindow { get; set; }
            [XmlElement(ElementName = "itemInstructions")]
            public ItemInstructions ItemInstructions { get; set; }
            [XmlElement(ElementName = "orderId")]
            public string OrderId { get; set; }
            [XmlElement(ElementName = "isHide")]
            public string IsHide { get; set; }
            [XmlElement(ElementName = "isCreateByAuthor")]
            public string IsCreateByAuthor { get; set; }
            [XmlElement(ElementName = "createBy")]
            public CreateBy CreateBy { get; set; }
            [XmlElement(ElementName = "complete")]
            public string Complete { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "resourceItems")]
        public class ResourceItemsClass
        {
            public ResourceItemsClass()
            {
                this.ResourceItem = new List<ResourceItem>();
            }
            [XmlElement(ElementName = "org.lamsfoundation.lams.tool.rsrc.model.ResourceItem")]
            public List<ResourceItem> ResourceItem { get; set; }
        }

        public LamsShareResource()
        {
            this.ContentId = "101";
            this.Title = "";
            this.Instructions = "";
            this.RunAuto = "false";
            this.MiniViewResourceNumber = "0";
            this.AllowAddFiles = "false";
            this.AllowAddUrls = "false";
            this.LockWhenFinished = "false";
            this.DefineLater = "false";
            this.ContentInUse = "false";
            this.NotifyTeachersOnAssigmentSumbit = "true";
            this.CreatedByShare = new CreatedByShareClass();
            this.ResourceItems = new ResourceItemsClass();
            this.ReflectOnActivity = "false";
            this.ReflectInstructions = "";
        }
        [XmlElement(ElementName = "contentId")]
        public string ContentId { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "instructions")]
        public string Instructions { get; set; }
        [XmlElement(ElementName = "runAuto")]
        public string RunAuto { get; set; }
        [XmlElement(ElementName = "miniViewResourceNumber")]
        public string MiniViewResourceNumber { get; set; }
        [XmlElement(ElementName = "allowAddFiles")]
        public string AllowAddFiles { get; set; }
        [XmlElement(ElementName = "allowAddUrls")]
        public string AllowAddUrls { get; set; }
        [XmlElement(ElementName = "lockWhenFinished")]
        public string LockWhenFinished { get; set; }
        [XmlElement(ElementName = "defineLater")]
        public string DefineLater { get; set; }
        [XmlElement(ElementName = "contentInUse")]
        public string ContentInUse { get; set; }
        [XmlElement(ElementName = "notifyTeachersOnAssigmentSumbit")]
        public string NotifyTeachersOnAssigmentSumbit { get; set; }
        [XmlElement(ElementName = "createdBy")]
        public CreatedByShareClass CreatedByShare { get; set; }
        [XmlElement(ElementName = "resourceItems")]
        public ResourceItemsClass ResourceItems { get; set; }
        [XmlElement(ElementName = "reflectOnActivity")]
        public string ReflectOnActivity { get; set; }
        [XmlElement(ElementName = "reflectInstructions")]
        public string ReflectInstructions { get; set; }
        public override string ToString()
        {
            return "Share Resources - " + Title;
        }


        [XmlIgnore]
        public override string TitleText { get { return Title; } }
        [XmlIgnore]
        public override Image Icon { get { return Resources.lms_share_resources; } }

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
            get { return "Uploading your resources to share with others."; }
        }

        [XmlIgnore]
        public override string HelpURL
        {
            get { return "http://wiki.lamsfoundation.org/display/lamsdocs/larsrc11"; }
        }

        [XmlIgnore]
        public override long LearningLibraryID
        {
            get { return 12; }
        }

        [XmlIgnore]
        public override string ToolSignature
        {
            get { return "larsrc11"; }
        }

        [XmlIgnore]
        public override long ToolID
        {
            get { return 12; }
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
            get { return "Shared Resources"; }
        }

        [XmlIgnore]
        public override long ToolVersion
        {
            get { return 20140102; }
        }

        [XmlIgnore]
        public override string AuthoringURL
        {
            get { return "tool/larsrc11/authoring/start.do"; }
        }

        [XmlIgnore]
        public override string MonitoringURL
        {
            get { return "tool/larsrc11/monitoring/summary.do"; }
        }

        [XmlIgnore]
        public override long ActivityCategoryID
        {
            get { return 4; }
        }

        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get { return "tool/larsrc11/images/icon_rsrc.swf"; }
        }
    }


}
