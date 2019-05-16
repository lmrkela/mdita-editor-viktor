using mDitaEditor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace mDitaEditor.Lams
{
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.imageGallery.model.ImageGallery")]
    public class LamsImageGallery : LamsTool
    {
        [Serializable]
        [XmlRoot(ElementName = "imageGallery")]

        public class ImageGallery
        {

            public ImageGallery()
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
                this.ImageGallery = new ImageGallery();

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
            [XmlElement(ElementName = "imageGallery")]
            public ImageGallery ImageGallery { get; set; }
        }
        [Serializable]
        [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.imageGallery.model.ImageGalleryItemInstruction")]
        public class ImageGalleryItemInstruction
        {

            public ImageGalleryItemInstruction()
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
                this.ImageGalleryItemInstruction = new List<ImageGalleryItemInstruction>();

            }
            [XmlElement(ElementName = "org.lamsfoundation.lams.tool.imageGallery.model.ImageGalleryItemInstruction")]
            public List<ImageGalleryItemInstruction> ImageGalleryItemInstruction { get; set; }
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
        [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.imageGallery.model.ImageGalleryItem")]
        public class ImageGalleryItem
        {
            public string imagePath { get; set; }

            public bool ShouldSerializeImagePath()
            {
                return imagePath != "";
            }

            public void ignoreImagePath()
            {
                imagePath = null;
            }
            public ImageGalleryItem()
            {
                this.Title = "";
                this.Description = "";
                this.SequenceId = "";
                this.IsHide = "false";
                this.IsCreateByAuthor = "true";
                this.CreateBy = new CreateBy();

                this.OriginalFileUuid = "";
                this.OriginalImageWidth = "1024";
                this.OriginalImageHeight = "768";
                this.MediumFileUuid = "";
                this.MediumImageWidth = "640";
                this.MediumImageHeight = "480";
                this.ThumbnailFileUuid = "";
                this.FileVersionId = "1";
                this.FileName = "";
                this.FileType = "";

                this.OriginalFile = new OriginalFileClass();
                this.MediumFile = new MediumFileClass();
                this.ThumbnailFile = new ThumbnailFileClass();

            }
            [XmlElement(ElementName = "title")]
            public string Title { get; set; }
            [XmlElement(ElementName = "description")]
            public string Description { get; set; }
            [XmlElement(ElementName = "sequenceId")]
            public string SequenceId { get; set; }
            [XmlElement(ElementName = "isHide")]
            public string IsHide { get; set; }
            [XmlElement(ElementName = "isCreateByAuthor")]
            public string IsCreateByAuthor { get; set; }
            [XmlElement(ElementName = "createBy")]
            public CreateBy CreateBy { get; set; }
            [XmlElement(ElementName = "originalFileUuid")]
            public string OriginalFileUuid { get; set; }
            [XmlElement(ElementName = "originalImageWidth")]
            public string OriginalImageWidth { get; set; }
            [XmlElement(ElementName = "originalImageHeight")]
            public string OriginalImageHeight { get; set; }
            [XmlElement(ElementName = "mediumFileUuid")]
            public string MediumFileUuid { get; set; }
            [XmlElement(ElementName = "mediumImageWidth")]
            public string MediumImageWidth { get; set; }
            [XmlElement(ElementName = "mediumImageHeight")]
            public string MediumImageHeight { get; set; }
            [XmlElement(ElementName = "thumbnailFileUuid")]
            public string ThumbnailFileUuid { get; set; }
            [XmlElement(ElementName = "fileVersionId")]
            public string FileVersionId { get; set; }
            [XmlElement(ElementName = "fileName")]
            public string FileName { get; set; }
            [XmlElement(ElementName = "fileType")]
            public string FileType { get; set; }


            [XmlElement(ElementName = "originalFile")]
            public OriginalFileClass OriginalFile { get; set; }
            [XmlElement(ElementName = "mediumFile")]
            public MediumFileClass MediumFile { get; set; }
            [XmlElement(ElementName = "thumbnailFile")]
            public ThumbnailFileClass ThumbnailFile { get; set; }

            [Serializable]
            [XmlRoot(ElementName = "originalFile")]
            public class OriginalFileClass
            {
                public OriginalFileClass()
                {
                    this.FileUuid = "";
                    this.FileVersionId = "1";
                    this.FileName = "";
                }
                [XmlElement(ElementName = "fileUuid")]
                public string FileUuid { get; set; }
                [XmlElement(ElementName = "fileVersionId")]
                public string FileVersionId { get; set; }
                [XmlElement(ElementName = "fileName")]
                public string FileName { get; set; }
            }
            [Serializable]
            [XmlRoot(ElementName = "mediumFile")]
            public class MediumFileClass
            {
                public MediumFileClass()
                {
                    this.FileUuid = "";
                    this.FileVersionId = "1";
                    this.FileName = "";
                }
                [XmlElement(ElementName = "fileUuid")]
                public string FileUuid { get; set; }
                [XmlElement(ElementName = "fileVersionId")]
                public string FileVersionId { get; set; }
                [XmlElement(ElementName = "fileName")]
                public string FileName { get; set; }
            }
            [Serializable]
            [XmlRoot(ElementName = "thumbnailFile")]
            public class ThumbnailFileClass
            {
                public ThumbnailFileClass()
                {
                    this.FileUuid = "";//
                    this.FileVersionId = "1";//
                    this.FileName = "";
                }
                [XmlElement(ElementName = "fileUuid")]
                public string FileUuid { get; set; }
                [XmlElement(ElementName = "fileVersionId")]
                public string FileVersionId { get; set; }
                [XmlElement(ElementName = "fileName")]
                public string FileName { get; set; }
            }

        }


        [Serializable]
        [XmlRoot(ElementName = "imageGalleryItems")]
        public class ImageGalleryItemsClass
        {
            public ImageGalleryItemsClass()
            {
                this.ImageGalleryItem = new List<ImageGalleryItem>();
            }
            [XmlElement(ElementName = "org.lamsfoundation.lams.tool.imageGallery.model.ImageGalleryItem")]
            public List<ImageGalleryItem> ImageGalleryItem { get; set; }
        }


        public LamsImageGallery()
        {
             
            this.ContentId = "101";
            this.Title = "";
            this.Instructions = "";
            this.NextImageTitle = "1";
            this.AllowVote = "false";
            this.AllowShareImages = "true";
            this.LockWhenFinished = "false";
            this.DefineLater = "false";
            this.ContentInUse = "false";
            this.AllowRank = "false";
            this.MaximumRates = "0";
            this.MinimumRates = "0";
            this.RatingCriterias = "";
            this.CreatedByShare = new CreatedByShareClass();
            this.ImageGalleryItems = new ImageGalleryItemsClass();
            this.ReflectOnActivity = "false";
            this.ReflectInstructions = "";
            this.NotifyTeachersOnImageSumbit = "false";


        }

        [XmlElement(ElementName = "contentId")]
        public string ContentId { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "instructions")]
        public string Instructions { get; set; }
        [XmlElement(ElementName = "nextImageTitle")]
        public string NextImageTitle { get; set; }
        [XmlElement(ElementName = "allowVote")]
        public string AllowVote { get; set; }
        [XmlElement(ElementName = "allowShareImages")]
        public string AllowShareImages { get; set; }
        [XmlElement(ElementName = "lockWhenFinished")]
        public string LockWhenFinished { get; set; }
        [XmlElement(ElementName = "defineLater")]
        public string DefineLater { get; set; }
        [XmlElement(ElementName = "contentInUse")]
        public string ContentInUse { get; set; }
        [XmlElement(ElementName = "allowRank")]
        public string AllowRank { get; set; }
        [XmlElement(ElementName = "maximumRates")]
        public string MaximumRates { get; set; }
        [XmlElement(ElementName = "minimumRates")]
        public string MinimumRates { get; set; }
        [XmlElement(ElementName = "ratingCriterias")]
        public string RatingCriterias { get; set; }
        [XmlElement(ElementName = "createdBy")]
        public CreatedByShareClass CreatedByShare { get; set; }
        [XmlElement(ElementName = "imageGalleryItems")]
        public ImageGalleryItemsClass ImageGalleryItems { get; set; }
        [XmlElement(ElementName = "reflectOnActivity")]
        public string ReflectOnActivity { get; set; }
        [XmlElement(ElementName = "reflectInstructions")]
        public string ReflectInstructions { get; set; }
        [XmlElement(ElementName = "notifyTeachersOnImageSumbit")]
        public string NotifyTeachersOnImageSumbit { get; set; }


        public override string ToString()
        {
            return "Image Gallery - " + Title;
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
            get { return "Uploading images to share with others."; }
        }

        [XmlIgnore]
        public override string HelpURL
        {
            get { return "http://wiki.lamsfoundation.org/display/lamsdocs/laimag10"; }
        }
        
        [XmlIgnore]
        public override long LearningLibraryID
        {
            get { return 12; }
        }

        [XmlIgnore]
        public override string ToolSignature
        {
            get { return "laimag10"; }
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
            get { return "Image Gallery"; }
        }

        [XmlIgnore]
        public override long ToolVersion
        {
            get { return 20170101; }
        }

        [XmlIgnore]
        public override string AuthoringURL
        {
            get { return "tool/laimag10/authoring/start.do"; }
        }

        [XmlIgnore]
        public override string MonitoringURL
        {
            get { return "tool/laimag10/monitoring/summary.do"; }
        }

        [XmlIgnore]
        public override long ActivityCategoryID
        {
            get { return 4; }
        }

        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get { return "tool/laimag10/images/icon_rsrc.swf"; }
        }
    }
}
