using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata.LAMS
{
    [Serializable]
    [XmlRoot(ElementName = "created")]
    public class CreatedShare
    {
        public CreatedShare()
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
    [XmlRoot(ElementName = "updated")]
    public class UpdatedShare
    {

        public UpdatedShare()
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
    [XmlRoot(ElementName = "resource")]
    public class Resource
    {

        public Resource()
        {
            Reference = "../..";
        }
        [XmlAttribute(AttributeName = "reference")]
        public string Reference { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "createdBy")]
    public class CreatedByShare
    {

        public CreatedByShare()
        {
            UserId = "1";
            FirstName = "Admin";
            LastName = "Admin";
            LoginName = "sysadmin";
            SessionFinished = "false";
            Resource = new Resource();

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
            Description = "";

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
            ResourceItemInstruction = new List<ResourceItemInstruction>();

        }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.rsrc.model.ResourceItemInstruction")]
        public List<ResourceItemInstruction> ResourceItemInstruction { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "createDate")]
    public class CreateDate
    {

        public CreateDate()
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
    public class CreateBy
    {
        public CreateBy()
        {
            Reference = "../../../createdBy";
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
            Type = "1";
            Title = "";
            Url = "";
            OpenUrlNewWindow = "false";
            IsHide = "false";
            IsCreateByAuthor = "true";
            CreateDate = new CreateDate();
            CreateBy = new CreateBy();
            Complete = "false";

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
        [XmlElement(ElementName = "createDate")]
        public CreateDate CreateDate { get; set; }
        [XmlElement(ElementName = "createBy")]
        public CreateBy CreateBy { get; set; }
        [XmlElement(ElementName = "complete")]
        public string Complete { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "resourceItems")]
    public class ResourceItems
    {
        public ResourceItems()
        {
            ResourceItem = new List<ResourceItem>();
        }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.rsrc.model.ResourceItem")]
        public List<ResourceItem> ResourceItem { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.rsrc.model.Resource")]
    public class LamsShareResource : LamsTool
    {

        public LamsShareResource()
        {
            ContentId = "101";
            Title = "";
            Instructions = "";
            RunAuto = "false";
            MiniViewResourceNumber = "0";
            AllowAddFiles = "false";
            AllowAddUrls = "false";
            LockWhenFinished = "false";
            DefineLater = "false";
            ContentInUse = "false";
            NotifyTeachersOnAssigmentSumbit = "true";
            UpdatedShare = new UpdatedShare();
            CreatedByShare = new CreatedByShare();
            ResourceItems = new ResourceItems();
            ReflectOnActivity = "false";
            ReflectInstructions = "";
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
        [XmlElement(ElementName = "created")]
        public CreatedShare CreatedShare { get; set; }
        [XmlElement(ElementName = "updated")]
        public UpdatedShare UpdatedShare { get; set; }
        [XmlElement(ElementName = "createdBy")]
        public CreatedByShare CreatedByShare { get; set; }
        [XmlElement(ElementName = "resourceItems")]
        public ResourceItems ResourceItems { get; set; }
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
