using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using mDitaEditor.Project;
using System.Diagnostics;

namespace mDitaEditor.Lams.Editor.XMLExporter
{
    [XmlRoot(ElementName = "activityEvaluations")]
    public class ActivityEvaluations
    {
        [XmlElement(ElementName = "string")]
        public ConditionDTO.ConditionName String { get; set; }
    }

    [XmlRoot(ElementName = "inputActivities")]
    public class InputActivities
    {
        [XmlElement(ElementName = "int")]
        public long Int { get; set; }
    }

    [XmlRoot(ElementName = "activities")]
    public class Activities
    {
        [XmlElement(ElementName = "org.lamsfoundation.lams.learningdesign.dto.AuthoringActivityDTO")]
        public List<AuthoringActivityDTO> List { get; set; }

        public Activities()
        {
            List = new List<AuthoringActivityDTO>();
        }
    }

    [XmlRoot(ElementName = "transitions")]
    public class Transitions
    {
        [XmlElement(ElementName = "org.lamsfoundation.lams.learningdesign.dto.TransitionDTO")]
        public List<TransitionDTO> List { get; set; }

        public Transitions()
        {
            List = new List<TransitionDTO>();
        }
    }

    [XmlRoot(ElementName = "branchMappings")]
    public class BranchMappings
    {
        [XmlElement(ElementName = "org.lamsfoundation.lams.learningdesign.dto.ToolOutputBranchActivityEntryDTO")]
        public List<ToolOutputBranchActivityEntryDTO> BranchList { get; set; }

        [XmlElement(ElementName = "org.lamsfoundation.lams.learningdesign.dto.ToolOutputGateActivityEntryDTO")]
        public List<ToolOutputGateActivityEntryDTO> GateList { get; set; }

        public BranchMappings()
        {
            BranchList = new List<ToolOutputBranchActivityEntryDTO>();
            GateList = new List<ToolOutputGateActivityEntryDTO>();
        }
    }

    [XmlRoot(ElementName = "org.lamsfoundation.lams.learningdesign.dto.LearningDesignDTO")]
    public class LearningDesignDTO
    {
        [XmlElement(ElementName = "learningDesignID")]
        public long LearningDesignID { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "firstActivityID")]
        public long FirstActivityID { get; set; }

        [XmlElement(ElementName = "firstActivityUIID")]
        public long FirstActivityUIID { get; set; }

        [XmlElement(ElementName = "floatingActivityID")]
        public long FloatingActivityID { get; set; }

        [XmlElement(ElementName = "maxID")]
        public long MaxID { get; set; }

        [XmlElement(ElementName = "validDesign")]
        public bool ValidDesign { get; set; }

        [XmlElement(ElementName = "readOnly")]
        public bool ReadOnly { get; set; }

        [XmlElement(ElementName = "editOverrideLock")]
        public bool EditOverrideLock { get; set; }

        [XmlElement(ElementName = "userID")]
        public long UserID { get; set; }

        [XmlElement(ElementName = "originalUserID")]
        public long OriginalUserID { get; set; }

        [XmlElement(ElementName = "copyTypeID")]
        public long CopyTypeID { get; set; }

        [XmlElement(ElementName = "createDateTime")]
        public DateTimeDTO CreateDateTime { get; set; }

        [XmlElement(ElementName = "version")]
        public string Version { get; set; }

        [XmlElement(ElementName = "designVersion")]
        public long DesignVersion { get; set; }

        [XmlElement(ElementName = "workspaceFolderID")]
        public long WorkspaceFolderID { get; set; }

        [XmlElement(ElementName = "lastModifiedDateTime")]
        public DateTimeDTO LastModifiedDateTime { get; set; }

        [XmlElement(ElementName = "contentFolderID")]
        public string ContentFolderID { get; set; }

        [XmlElement(ElementName = "groupings")]
        public string Groupings { get; set; }

        [XmlElement(ElementName = "activities")]
        public Activities Activities { get; set; }

        [XmlElement(ElementName = "transitions")]
        public Transitions Transitions { get; set; }

        [XmlElement(ElementName = "branchMappings")]
        public BranchMappings BranchMappings { get; set; }

        [XmlElement(ElementName = "competences")]
        public string Competences { get; set; }

        [XmlElement(ElementName = "annotations")]
        public string Annotations { get; set; }

        public LearningDesignDTO()
        {
            LearningDesignID = 32295;
            FirstActivityID = 1;
            FirstActivityUIID = 1;
            FloatingActivityID = 101;
            MaxID = 15;
            ValidDesign = true;
            ReadOnly = false;
            EditOverrideLock = false;
            UserID = 2992;
            OriginalUserID = 2992;
            CopyTypeID = 1;
            CreateDateTime = new DateTimeDTO();
            Version = "2.4.0.201204131000";
            DesignVersion = 1;
            WorkspaceFolderID = 3748;
            LastModifiedDateTime = new DateTimeDTO();
            ContentFolderID = "d958b8dc533343ec01537ec861f313f2";
            Groupings = "";
            Activities = new Activities();
            Transitions = new Transitions();
            BranchMappings = new BranchMappings();
            Competences = "";
            Annotations = "";
        }
        
        [XmlIgnore]
        public List<SavingError> Errors { get; private set; }

        [XmlIgnore]
        public bool CanvasEmpty { get; private set; }

        [XmlIgnore]
        public LamsTool SequenceChoosingTool { get; private set; }





        public LearningDesignDTO(GrafikaPanel grafikaPanel, ProjectFile project = null, bool check = false, bool export = false) : this()
        {

            GrafikaCanvas canvas = grafikaPanel.Canvas;
            if (project == null)
            {
                project = ProjectSingleton.Project;
            }
            Title = string.Format("{0} - {1} ({2})", project.CourseCode, project.LessonNumber, DateTime.UtcNow.Ticks);
            Errors = new List<SavingError>();


            // EMPTY CANVAS
            if (canvas.Items.Count == 0)
            {

                if (check)
                {
                    return;
                }
                CanvasEmpty = true;
                canvas.arrange();
            }
          


            grafikaPanel.Canvas.NormalizeCoordinates();
            var objects = grafikaPanel.Objects;
            var items = grafikaPanel.Canvas.Items;

                      

            // CHECK ALL CONNECTIONS
            var groups = items.Where(item => !(item is GrafikaBranchEndItem) && item.Previous == null);
            GrafikaItem startItem = null;
            int largestGroupCount = 0;
            foreach (var item in groups)
            {
                var count = 0;
                for (var i = item; i != null; i = i.Next)
                {
                    ++count;
                }
                if (count > largestGroupCount)
                {
                    startItem = item;
                    largestGroupCount = count;
                }
            }
            if (startItem == null)
            {
                Errors.Add(new SavingError("Objekti ne smeju biti povezani u krug."));
            }
            else
            {
                //FindItemsInSequence(startItem, sequenceItems);
                foreach (var item in groups)
                {
                    if (item == startItem)
                    {
                        continue;
                    }
                    var sb = new StringBuilder();
                    var subItems = new List<GrafikaItem>();
                    for (var i = item; i != null; i = i.Next)
                    {
                        //sequenceItems.Add(i);
                        subItems.Add(i);
                        if (sb.Length > 0)
                        {
                            sb.Append(i.Next == null ? " i " : ", ");
                        }
                        sb.Append(i.GrafikaObject.TitleText);
                    }
                    Errors.Add(new SavingError(item,
                        (subItems.Count == 1 ? "Objekat " : "Objekti ") + sb +
                        (subItems.Count == 1 ? " nije povezan" : " nisu povezani") + " u sekvencu."));
                }
            }
            foreach (var item in items)
            {
                item.CheckErrors(Errors);
            }

            // CHECK ALL OBJECTS
            for (int i = 0; i < objects.Count; ++i)
            {
                var obj = objects[i];
                obj.ToolContentID = FirstActivityID + i;
                if (!items.Contains_(obj))
                {
                    Errors.Add(new SavingError(obj, string.Format("Objekat {0} nije ubačen na platno.", obj.TitleText)));
                    if (!check)
                    {
                        items.Insert(objects.IndexOf(obj),
                            GrafikaItem.Create(grafikaPanel.Canvas, Point.Empty, obj, false));
                    }
                }
            }


            if (export)
            {
                grafikaPanel.Canvas.addSequenceChoosing();
            }


            Debug.WriteLine(Activities.List.Count + " export: " + export);


            // CHECK BRANCH ITEMS
            var branchItems = new HashSet<GrafikaItem>();
            foreach (var grafikaItem in items)
            {
                var branch = grafikaItem as GrafikaBranchStartItem;
                if (branch != null)
                {
                    Debug.WriteLine(branch.GrafikaObject.TitleText);
                    foreach (var grafikaBranchConnection in branch.Branch.Branches)
                    {
                        for (var branchItem = grafikaBranchConnection.EndItem;
                        branchItem != null;
                        branchItem = branchItem.Next)
                        {
                            if (branchItem is GrafikaBranchEndItem)
                            {
                                break;
                            }
                            branchItems.Add(branchItem);
                        }
                    }
                }
            }

                Debug.WriteLine("canvas items " + MainForm.Instance.grafikaPanel.Canvas.Items.Count);

                Debug.WriteLine("items " + items.Count);

                Debug.WriteLine(Activities.List.Count + " export: " + export);

                // ADD ITEMS
                for (int i = 0; i < items.Count; ++i)
                {
                    var item = items[i];
                    if (branchItems.Contains(item))
                    {
                        Debug.WriteLine(item.GrafikaObject.TitleText);
                        continue;
                    }

                    AddActivity(items[i]);
                    if (!item.Initialized)
                    {
                        --i;
                        items.Remove(item);
                    }
                }

                // ADD CONNECTIONS
                AddConnections(grafikaPanel.Canvas.Connections);

                Debug.WriteLine(Activities.List.Count + " export: " + export);

                // CHECK TOOLS
                foreach (var activity in Activities.List)
                {
                    if (activity.Gate != null)
                    {
                        CheckGate(activity, items.Find(item => item.GrafikaObject == activity.GrafikaObject));
                    }
                    else if (activity.Branch != null)
                    {
                        CheckBranch(activity, items.Find(item => item.GrafikaObject == activity.GrafikaObject));
                    }
                }

                Debug.WriteLine(Activities.List.Count + " export: " + export);
            
        }

       

        private void FindItemsInSequence(GrafikaItem item, HashSet<GrafikaItem> sequence)
        {
            if (item == null || item is GrafikaBranchEndItem)
            {
                return;
            }
            sequence.Add(item);
            var branch = item as GrafikaBranchStartItem;
            if (branch != null)
            {
                sequence.Add(branch.EndItem);
                foreach (var branchConnection in branch.Branch.Branches)
                {
                    FindItemsInSequence(branchConnection.EndItem, sequence);
                }
            }
            FindItemsInSequence(item.Next, sequence);
        }
        
        private long _nextId = 1;
        private long _toolcontentid = 1;

       

        private AuthoringActivityDTO AddActivity(GrafikaItem item, GrafikaBranchConnection connection = null, AuthoringActivityDTO parent = null, int orderId = 0)
        {
            if (item?.GrafikaObject is LamsQa) {
                Debug.WriteLine("Q and A");
                Debug.WriteLine(((LamsTool)item.GrafikaObject).ToolContentID);
            }


           

            if (item is GrafikaBranchEndItem)
            {
                return null;
            }

            AuthoringActivityDTO activity;
            if (item != null)
            {
                activity = new AuthoringActivityDTO(item, this);
              
            }
            else
            {
                activity = new AuthoringActivityDTO(connection, this);
            }
            activity.ActivityUIID = _nextId;
            activity.ActivityID = _nextId;


            activity.ToolContentID = _toolcontentid;

    
            if (!activity.SequenceChoosing)
            {
                _toolcontentid++;
                
            }
            else if(item?.GrafikaObject is LamsOptional)
            {
                _toolcontentid = 1;
            }

            //Lams3 fix
            //Cant use same q and a activity multiple timess
            if (item?.GrafikaObject is LamsQa && item.Optional)
            {
                Debug.WriteLine("Optional: " + _toolcontentid);
                activity.ToolContentID = _toolcontentid;
            }

            if(item?.GrafikaObject is LamsQa)
            {
                _toolcontentid++;
            }

            ++_nextId;
            if (parent != null)
            {
                activity.ParentActivityId = parent.ActivityID;
                activity.ParentActivityUiId = parent.ActivityUIID;
                activity.OrderId = orderId;
            }
            Activities.List.Add(activity);

            if (item != null)
            {
                if (item.GrafikaObject is LamsTool)
                {
                    ((LamsTool)item.GrafikaObject).ToolContentID = activity.ToolContentID.Value;
                }
                else if (item.GrafikaObject is LamsOptional)
                {
          
                    var p = new Point(8, 57);
                    int i = 0;
                    foreach (var obj in ((LamsOptional)item.GrafikaObject).SubObjects)
                    {

                        //Lams3 fix
                        GrafikaItem subItem = GrafikaItem.Create(item.Parent, p, obj, item.Initialized);
                        if (item.SequenceChoosing)
                        {
                            subItem.Optional = true;
                        }
                        AddActivity(subItem, null, activity, ++i);
                        p = new Point(p.X, p.Y + 60);
                    }
                }
                else if (item.GrafikaObject is LamsBranch)
                {
                    var branch = (LamsBranch)item.GrafikaObject;
                    int i = 0;
                    foreach (var branchConnection in branch.Branches)
                    {
                        var act = AddActivity(null, branchConnection, activity, ++i);
                        if(branchConnection == branch.DefaultBranch)
                        {
                            activity.DefaultActivityUiId = act.ActivityUIID;
                        }
                    }
                }
            }
            else
            {
                int i = 0;
                for (var branchItem = connection.EndItem;
                    branchItem != null;
                    branchItem = branchItem.Next)
                {
                    if(branchItem is GrafikaBranchEndItem)
                    {
                        break;
                    }
                    var act = AddActivity(branchItem, null, activity, ++i);
                    if (!activity.DefaultActivityUiId.HasValue)
                    {
                        activity.DefaultActivityUiId = act.ActivityUIID;
                    }
                }
            }
            return activity;
        }

        private void AddConnections(List<GrafikaConnection> connections)
        {
            for (int i = 0; i < connections.Count; ++i)
            {
                var connection = connections[i];
                if (connection is GrafikaBranchConnection || connection.EndItem is GrafikaBranchEndItem)
                {
                    continue;
                }

                var transition = new TransitionDTO(connection, this)
                {
                    TransitionUIID = _nextId,
                    TransitionID = _nextId
                };
                ++_nextId;
                Transitions.List.Add(transition);

                if (!connection.StartItem.Initialized || !connection.EndItem.Initialized)
                {
                    connections.Remove(connection);
                    --i;
                }
            }
        }

        private void CheckGate(AuthoringActivityDTO activity, GrafikaItem item)
        {
            var gate = activity.Gate;
            BranchMappings.GateList.AddRange(gate.Entries);
            foreach (var a in Activities.List)
            {
                if (gate.InputTool == a.Tool)
                {
                    activity.ToolActivityUiId = a.ActivityUIID;
                    activity.InputActivities = new InputActivities()
                    {
                        Int = a.ActivityUIID
                    };
                    if (gate.Entries.Count != 0)
                    {
                        foreach (var entry in gate.Entries)
                        {
                            entry.EntryID = _nextId;
                            entry.EntryUIID = _nextId;
                            ++_nextId;
                            entry.GateActivityUIID = activity.ActivityUIID;
                            entry.BranchingActivityUIID = activity.ActivityUIID;

                            var condition = entry.Condition;
                            condition.ConditionId = _nextId;
                            condition.ConditionUIID = _nextId;
                            ++_nextId;
                            condition.OrderID = 1;
                            condition.ToolActivityUIID = a.ActivityUIID;
                        }
                        a.ActivityEvaluations = new ActivityEvaluations()
                        {
                            String = gate.Entries[0].Condition.Name
                        };
                    }
                    return;
                }
            }
            Errors.Add(new SavingError(item, "Nije pronađen input tool za Gate."));
        }

        private void CheckBranch(AuthoringActivityDTO activity, GrafikaItem item)
        {
            var branch = activity.Branch;
            BranchMappings.BranchList.AddRange(branch.Entries);
            foreach (var a in Activities.List)
            {
                if (branch.InputTool == a.Tool)
                {
                    activity.ToolActivityUiId = a.ActivityUIID;
                    activity.InputActivities = new InputActivities()
                    {
                        Int = a.ActivityUIID
                    };
                    if (branch.Entries.Count != 0)
                    {
                        foreach (var entry in branch.Entries)
                        {
                            entry.EntryID = _nextId;
                            entry.EntryUIID = _nextId;
                            ++_nextId;
                            entry.BranchingActivityUIID = activity.ActivityUIID;

                            var condition = entry.Condition;
                            condition.ConditionId = _nextId;
                            condition.ConditionUIID = _nextId;
                            ++_nextId;
                            condition.OrderID = 1;
                            condition.ToolActivityUIID = a.ActivityUIID;
                            
                            if (entry.BranchPath == null)
                            {
                                entry.BranchPath = branch.DefaultBranch;
                            }
                            if (entry.BranchPath != null)
                            {
                                foreach (var act in Activities.List)
                                {
                                    if (entry.BranchPath == act.BranchConnection)
                                    {
                                        entry.SequenceActivityUIID = act.ActivityUIID;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                entry.SequenceActivityUIID = null;
                            }
                        }
                        a.ActivityEvaluations = new ActivityEvaluations()
                        {
                            String = branch.Entries[0].Condition.Name
                        };
                    }
                    return;
                }
            }
           // Errors.Add(new SavingError(item, "Nije pronađen input tool za Branch."));
        }
    }
}
