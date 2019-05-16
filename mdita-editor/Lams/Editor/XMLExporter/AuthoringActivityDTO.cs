using System.Xml.Serialization;

namespace mDitaEditor.Lams.Editor.XMLExporter
{
    [XmlRoot(ElementName = "org.lamsfoundation.lams.learningdesign.dto.AuthoringActivityDTO")]
    public class AuthoringActivityDTO
    {
        [XmlElement(ElementName = "activityID")]
        public long ActivityID { get; set; }

        [XmlElement(ElementName = "activityUIID")]
        public long ActivityUIID { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "activityTitle")]
        public string ActivityTitle { get; set; }

        [XmlElement(ElementName = "helpText")]
        public string HelpText { get; set; }

        [XmlElement(ElementName = "helpURL")]
        public string HelpURL { get; set; }

        [XmlElement(ElementName = "xCoord", IsNullable = true)]
        public int? XCoord { get; set; }

        public bool ShouldSerializeXCoord()
        {
            return XCoord.HasValue;
        }

        [XmlElement(ElementName = "yCoord", IsNullable = true)]
        public int? YCoord { get; set; }

        public bool ShouldSerializeYCoord()
        {
            return YCoord.HasValue;
        }

        [XmlElement(ElementName = "activityTypeID")]
        public long? ActivityTypeID { get; set; }

        [XmlElement(ElementName = "learningDesignID")]
        public long? LearningDesignID { get; set; }

        [XmlElement(ElementName = "learningLibraryID")]
        public long? LearningLibraryID { get; set; }

        public bool ShouldSerializeLearningLibraryID()
        {
            return LearningLibraryID.HasValue;
        }

        [XmlElement(ElementName = "createDateTime")]
        public DateTimeDTO CreateDateTime { get; set; }

        [XmlElement(ElementName = "toolSignature")]
        public string ToolSignature { get; set; }

        [XmlElement(ElementName = "toolID")]
        public long? ToolID { get; set; }

        public bool ShouldSerializeToolID()
        {
            return ToolID.HasValue;
        }

        [XmlElement(ElementName = "toolContentID")]
        public long? ToolContentID { get; set; }

        [XmlElement(ElementName = "toolDisplayName")]
        public string ToolDisplayName { get; set; }

        [XmlElement(ElementName = "toolVersion")]
        public long? ToolVersion { get; set; }

        public bool ShouldSerializeToolVersion()
        {
            return ToolVersion.HasValue;
        }

        [XmlElement(ElementName = "authoringURL")]
        public string AuthoringURL { get; set; }

        [XmlElement(ElementName = "monitoringURL")]
        public string MonitoringURL { get; set; }

        [XmlElement(ElementName = "activityCategoryID")]
        public long? ActivityCategoryID { get; set; }

        [XmlElement(ElementName = "applyGrouping")]
        public bool? ApplyGrouping { get; set; }

        [XmlElement(ElementName = "groupingSupportType")]
        public int? GroupingSupportType { get; set; }

        [XmlElement(ElementName = "libraryActivityUIImage")]
        public string LibraryActivityUIImage { get; set; }

        [XmlElement(ElementName = "readOnly")]
        public bool? ReadOnly { get; set; }

        [XmlElement(ElementName = "initialised")]
        public bool Initialised { get; set; }

        [XmlElement(ElementName = "stopAfterActivity")]
        public bool? StopAfterActivity { get; set; }

        [XmlElement(ElementName = "inputActivities")]
        public InputActivities InputActivities { get; set; }

        [XmlElement(ElementName = "competenceMappingTitles")]
        public string CompetenceMappingTitles { get; set; }

        [XmlElement(ElementName = "activityEvaluations")]
        public ActivityEvaluations ActivityEvaluations { get; set; }

        [XmlElement(ElementName = "languageCode")]
        public string LanguageCode { get; set; }

        [XmlElement(ElementName = "supportsOutputs")]
        public bool? SupportsOutputs { get; set; }

        [XmlElement(ElementName = "defineLater")]
        public bool? DefineLater { get; set; }

        [XmlElement(ElementName = "runOffline")]
        public bool? RunOffline { get; set; }

        [XmlElement(ElementName = "gateActivityLevelID", IsNullable = true)]
        public int? GateActivityLevelId { get; set; }

        public bool ShouldSerializeGateActivityLevelId()
        {
            return GateActivityLevelId.HasValue;
        }

        [XmlElement(ElementName = "toolActivityUIID", IsNullable = true)]
        public long? ToolActivityUiId { get; set; }

        public bool ShouldSerializeToolActivityUiId()
        {
            return ToolActivityUiId.HasValue;
        }

        [XmlElement(ElementName = "parentActivityID", IsNullable = true)]
        public long? ParentActivityId { get; set; }

        public bool ShouldSerializeParentActivityId()
        {
            return ParentActivityId.HasValue;
        }

        [XmlElement(ElementName = "parentUIID", IsNullable = true)]
        public long? ParentActivityUiId { get; set; }

        public bool ShouldSerializeParentActivityUiId()
        {
            return ParentActivityUiId.HasValue;
        }

        [XmlElement(ElementName = "orderID", IsNullable = true)]
        public int? OrderId { get; set; }

        public bool ShouldSerializeOrderId()
        {
            return OrderId.HasValue;
        }

        [XmlElement(ElementName = "defaultActivityUIID", IsNullable = true)]
        public long? DefaultActivityUiId { get; set; }

        public bool ShouldSerializeDefaultActivityUiId()
        {
            return DefaultActivityUiId.HasValue;
        }

        [XmlElement(ElementName = "startXCoord", IsNullable = true)]
        public int? StartXCoord { get; set; }

        public bool ShouldSerializeStartXCoord()
        {
            return StartXCoord.HasValue;
        }

        [XmlElement(ElementName = "startYCoord", IsNullable = true)]
        public int? StartYCoord { get; set; }

        public bool ShouldSerializeStartYCoord()
        {
            return StartYCoord.HasValue;
        }

        [XmlElement(ElementName = "endXCoord", IsNullable = true)]
        public int? EndXCoord { get; set; }

        public bool ShouldSerializeEndXCoord()
        {
            return EndXCoord.HasValue;
        }

        [XmlElement(ElementName = "endYCoord", IsNullable = true)]
        public int? EndYCoord { get; set; }

        public bool ShouldSerializeEndYCoord()
        {
            return EndYCoord.HasValue;
        }

        [XmlElement(ElementName = "maxOptions", IsNullable = true)]
        public int? MaxOptions { get; set; }

        public bool ShouldSerializeMaxOptions()
        {
            return MaxOptions.HasValue;
        }

        [XmlElement(ElementName = "minOptions", IsNullable = true)]
        public int? MinOptions { get; set; }

        public bool ShouldSerializeMinOptions()
        {
            return MinOptions.HasValue;
        }



        [XmlIgnore]
        public IGrafikaObject GrafikaObject { get; private set; }

        [XmlIgnore]
        public LamsTool Tool
        {
            get { return GrafikaObject as LamsTool; }
            set { GrafikaObject = value; }
        }

        [XmlIgnore]
        public LamsBranch Branch { get { return GrafikaObject as LamsBranch; } }

        [XmlIgnore]
        public LamsGate Gate { get { return GrafikaObject as LamsGate; } }

        [XmlIgnore]
        public LamsOptional Optional { get { return GrafikaObject as LamsOptional; } }

        [XmlIgnore]
        public bool SequenceChoosing {get; set;}

        [XmlIgnore]
        public bool SequenceChoosingContent { get; set; }

        public AuthoringActivityDTO()
        {
            ActivityTypeID = 1;
            LearningDesignID = 32295;
            CreateDateTime = new DateTimeDTO();
            ApplyGrouping = false;
            GroupingSupportType = 2;
            ReadOnly = false;
            Initialised = false;
            StopAfterActivity = false;
            CompetenceMappingTitles = "";
            LanguageCode = "";
            SupportsOutputs = false;
            DefineLater = false;
            RunOffline = false;
        }

        public AuthoringActivityDTO(GrafikaItem item, LearningDesignDTO learningDesign) : this()
        {
            GrafikaObject = item.GrafikaObject;
            LearningDesignID = learningDesign.LearningDesignID;
            XCoord = item.X;
            YCoord = item.Y;
            Initialised = item.Initialized;
            Description = Tool?.Description;
            ActivityTitle = Tool?.ActivityTitle;
            HelpText = Tool?.HelpText;
            HelpURL = Tool?.HelpURL;
            LearningLibraryID = Tool?.LearningLibraryID;
            ToolSignature = Tool?.ToolSignature;
            ToolID = Tool?.ToolID;
            ToolContentID = Tool?.ToolContentID;
            ToolDisplayName = Tool?.ToolDisplayName;
            ToolVersion = Tool?.ToolVersion;
            AuthoringURL = Tool?.AuthoringURL;
            MonitoringURL = Tool?.MonitoringURL;
            ActivityCategoryID = Tool?.ActivityCategoryID;
            LibraryActivityUIImage = Tool?.LibraryActivityUIImage;
            SequenceChoosing = item.SequenceChoosing;
            if (Tool == null)
            {
                if (item.GrafikaObject is LamsBranch)
                {
                    ActivityTitle = Branch.TitleText;

                    ActivityTypeID = item.SequenceChoosing ? 13 : 12;
                    if (item.SequenceChoosing)
                    {
                        MinOptions = 1;
                        MaxOptions = 2;
                    }

                    ActivityCategoryID = 1;
                    StartXCoord = item.X;
                    StartYCoord = item.Y;
                    var branchEnd = ((GrafikaBranchStartItem)item).EndItem;
                    EndXCoord = branchEnd.X;
                    EndYCoord = branchEnd.Y;
                }
                else if (item.GrafikaObject is LamsGate)
                {
                    ActivityTitle = Gate.TitleText;
                    ActivityTypeID = 14;
                    GateActivityLevelId = 1;
                    ActivityCategoryID = 1;
                }
                else if (item.GrafikaObject is LamsOptional)
                {
                    ActivityTitle = Optional.TitleText;
                    ActivityTypeID = 7;
                    ActivityCategoryID = 1;
                }
            }

        }

        [XmlIgnore]
        public GrafikaBranchConnection BranchConnection { get; private set; }



        public AuthoringActivityDTO(GrafikaBranchConnection connection, LearningDesignDTO learningDesign) : this()
        {
            BranchConnection = connection;
            ActivityTypeID = 8;
            ActivityTitle = connection.Title;
            ActivityCategoryID = 1;
            Initialised = true;
            SequenceChoosing = connection.SequenceChoosing;
        }
    }
}