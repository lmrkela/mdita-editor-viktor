using System.Collections.Generic;

namespace mDitaEditor.Lams.Editor.XMLExporter
{
    public static class XmlExporterHelper
    {
        public static bool Contains_(this List<GrafikaItem> items, IGrafikaObject obj)
        {
            return Get_(items, obj) != null;
        }

        public static GrafikaItem Get_(this List<GrafikaItem> items, IGrafikaObject obj)
        {
            var noticeboardObj = (obj as LamsNoticeboard)?.LearningObject;
            foreach (var i in items)
            {
                if (i.GrafikaObject == obj)
                {
                    return i;
                }
                var optional = i.GrafikaObject as LamsOptional;
                if (optional != null)
                {
                    foreach (var subObject in optional.SubObjects)
                    {
                        if (subObject == obj)
                        {
                            return i;
                        }
                        if (noticeboardObj != null)
                        {
                            var noticeboardGrafika = subObject as LamsNoticeboard;
                            if (noticeboardGrafika != null && noticeboardObj == noticeboardGrafika.LearningObject)
                            {
                                return i;
                            }
                        }
                    }
                }
                if (noticeboardObj != null)
                {
                    var noticeboardGrafika = i.GrafikaObject as LamsNoticeboard;
                    if (noticeboardGrafika != null && noticeboardObj == noticeboardGrafika.LearningObject)
                    {
                        return i;
                    }
                }
            }
            return null;
        }

        public static AuthoringActivityDTO Get_(this List<AuthoringActivityDTO> activities, GrafikaItem item)
        {
            foreach (var activity in activities)
            {
                if (activity.GrafikaObject == item.GrafikaObject)
                {
                    return activity;
                }
            }
            return null;
        }

        public static bool Contains_(this List<LamsTool> tools, LamsTool obj)
        {
            if (obj == null)
            {
                return false;
            }
            foreach (var tool in tools)
            {
                if (tool == obj)
                {
                    return true;
                }
                var noticeboardTool = tool as LamsNoticeboard;
                var noticeboardObj = obj as LamsNoticeboard;
                if (noticeboardTool != null && noticeboardObj != null && noticeboardObj.LearningObject == noticeboardTool.LearningObject)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
