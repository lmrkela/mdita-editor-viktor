using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using mDitaEditor.Lams;

namespace mDitaEditor.Dita
{
    partial class LearningBase
    {
        [Serializable]
        public class LamsObjectList : IList<LamsTool>
        {
            private List<LamsTool> _list = new List<LamsTool>();

            public LearningBase Parent { get; private set; }

            public LamsObjectList(LearningBase parent)
            {
                Parent = parent;
            }
             
            public IEnumerator<LamsTool> GetEnumerator()
            {
                return _list.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Add(LamsTool item)
            {
                if (item != null)
                {
                    item.Parent = Parent;
                    _list.Add(item);
                }
            }

            public void Clear()
            {
                foreach (var lamsObject in _list)
                {
                    lamsObject.Parent = null;
                }
                _list.Clear();
            }

            public bool Contains(LamsTool item)
            {
                return _list.Contains(item);
            }

            public void CopyTo(LamsTool[] array, int arrayIndex)
            {
                _list.CopyTo(array, arrayIndex);
            }

            public bool Remove(LamsTool item)
            {
                if (_list.Remove(item))
                {
                    item.Parent = null;
                    return true;
                }
                return false;
            }

            public int Count
            {
                get { return _list.Count; }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            public int IndexOf(LamsTool item)
            {
                return _list.IndexOf(item);
            }

            public void Insert(int index, LamsTool item)
            {
                if (item != null)
                {
                    item.Parent = Parent;
                    _list.Insert(index, item);
                }
            }

            public void RemoveAt(int index)
            {
                var item = _list[index];
                if (item != null)
                {
                    item.Parent = null;
                    _list.RemoveAt(index);
                }
            }

            public LamsTool this[int index]
            {
                get { return _list[index]; }
                set
                {
                    _list[index].Parent = null;
                    _list[index] = value;
                    if (value != null)
                    {
                        value.Parent = Parent;
                    }
                }
            }
        }
    }
}
