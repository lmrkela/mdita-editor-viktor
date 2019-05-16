using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using mDitaEditor.Lams.Editor.XMLExporter;
using mDitaEditor.Project;

namespace mDitaEditor.Lams.Editor
{
    public partial class GrafikaCanvas : PictureBox
    {
        public GrafikaPanel ParentPanel { get; private set; }

        private Point _offset;
        public Point Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        private float _zoom;
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                if (value > 2)
                {
                    _zoom = 2;
                }
                else if (value < 0.5f)
                {
                    _zoom = 0.5f;
                }
                else
                {
                    _zoom = value;
                }
            }
        }


        public Size VisibleSize
        {
            get
            {
                return new Size((int) (Width / Zoom), (int) (Height / Zoom));
            }
        }

        private int _gridSize;
        public int GridSize
        {
            get { return _gridSize; }
            set
            {
                if (value < 10)
                {
                    _gridSize = 10;
                }
                else if (value > 80)
                {
                    _gridSize = 80;
                }
                else
                {
                    _gridSize = value;
                }
            }
        }

        public bool ShowGrid { get; set; }

        public bool SnapToGrid { get; set; }
        
        public enum MouseListener
        {
            Move, Connect, Add
        }

        public void SetListener(MouseListener mouse, IGrafikaObject obj = null)
        {
            switch (mouse)
            {
                case MouseListener.Move:
                    Listener = MoveItemListener;
                    MainForm.Instance.btnGraphicsMove.Checked = true;
                    break;
                case MouseListener.Connect:
                    Listener = ConnectListener;
                    MainForm.Instance.btnGraphicsConnect.Checked = true;
                    break;
                case MouseListener.Add:
                    AddListener.NewObject = GrafikaItem.Create(this, Point.Empty, obj, false);
                    Listener = AddListener;
                    MainForm.Instance.btnGraphicsMove.Checked = true;
                    break;
            }
        }

        private readonly MoveItemMouseListener MoveItemListener;
        private readonly ConnectMouseListener ConnectListener;
        private readonly AddMouseListener AddListener;

        private GrafikaMouseListener _listener;
        private GrafikaMouseListener Listener
        {
            get { return _listener; }
            set { _listener = value; }
        }

        public ScrollTimer Scroll { get; private set; }


        public List<GrafikaItem> Items { get; private set; }

        public List<GrafikaConnection> Connections { get; private set; }

        public GrafikaItem HoverObject { get; set; }

        public GrafikaConnection HoverConnection { get; set; }

        public GrafikaArrow HoverArrow { get; set; }
        

        public GrafikaCanvas(GrafikaPanel parent)
        {
            _arrows = new [] { new GrafikaArrow(this, GrafikaArrow.EDirection.Up), new GrafikaArrow(this, GrafikaArrow.EDirection.Down), new GrafikaArrow(this, GrafikaArrow.EDirection.Left), new GrafikaArrow(this, GrafikaArrow.EDirection.Right) };
            InitializeComponent();
            InitializeMenu();

            ParentPanel = parent;

            AllowDrop = true;
            MouseWheel += GrafikaCanvas_MouseWheel;
            
            _offset = Point.Empty;
            _zoom = 1;

            GridSize = 20;
            ShowGrid = true;
            SnapToGrid = true;

            Scroll = new ScrollTimer(this);
            MoveItemListener = new MoveItemMouseListener(this);
            ConnectListener = new ConnectMouseListener(this);
            AddListener = new AddMouseListener(this);
            Listener = MoveItemListener;

            Items = new List<GrafikaItem>();
            Connections = new List<GrafikaConnection>();
        }

        public void Clear()
        {
            Items.Clear();
            Connections.Clear();
            ParentPanel.LoadProject();
            Invalidate();
        }

        private static void FindGateIndex(List<IGrafikaObject> objects, GrafikaItem item)
        {
            if (objects.Contains(item.GrafikaObject) || item.Previous == null)
            {
                return;
            }

            var prev = item.Previous.GrafikaObject;
            if (prev is LamsNoticeboard)
            {
                var noticeboard = (LamsNoticeboard)prev;
                prev = objects.Find(x => x is LamsNoticeboard && ((LamsNoticeboard)x).LearningObject == noticeboard.LearningObject);
            }
            else if (prev is LamsGate || prev is LamsOptional || prev is LamsBranch)
            {
                if (!objects.Contains(prev))
                {
                    FindGateIndex(objects, item.Previous);
                    if (!objects.Contains(prev))
                    {
                        return;
                    }
                }
            }

            var index = objects.IndexOf(prev) + 1;
            objects.Insert(index, item.GrafikaObject);
        }

        public void AutoArrange(SortType ordering, bool reloadProject = false)
        {
            if (reloadProject)
            {
                //ParentPanel.LoadProject();
            }
            var skip = false;

            foreach (var branch in Items.Select(item => item as GrafikaBranchStartItem).Where(branch => branch != null))
            {
                //if (!branch.CheckConnections())
                {
                    //MessageBox.Show("Nisu povezani svi objekti u Branch-u " + branch.Branch.TitleText + ".",
                    //        "Nemoguće sortirati objekte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show("Sekvenca sadrži Branch objekte koji ne mogu biti automatski sortitani.\nUkoliko želite da automatski sortirate sekvencu, morate obrisati ove objekte.",
                            "Nemoguće sortirati objekte", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            var objects = new List<IGrafikaObject>(Items.Count);
            objects.AddRange(ParentPanel.ListControl.PreviewControls.Select(control => control.GrafikaObject));
            foreach (var item in Items)
            {
                var optional = item.GrafikaObject as LamsOptional;
                if (optional != null)
                {
                    foreach (var subObject in optional.SubObjects)
                    {
                        var obj = subObject;
                        if (subObject is LamsNoticeboard)
                        {
                            obj =
                                objects.Find(
                                    o =>
                                        (o as LamsNoticeboard)?.LearningObject ==
                                        ((LamsNoticeboard) subObject).LearningObject);
                        }
                        objects.Remove(obj);
                    }
                }
            }
            foreach (var item in Items.Where(item => item.GrafikaObject is LamsGate || item.GrafikaObject is LamsOptional || item.GrafikaObject is LamsBranch))
            {
                if (item.Previous == null)
                {
                    if (skip)
                    {
                        continue;
                    }
                    var result =
                        MessageBox.Show(item.GrafikaObject.GetType().Name +
                                        " nije povezan u sekvencu.\nAutomatsko sortiranje će obrisati ovaj objekat.",
                            "Nastaviti sa sortiranjem?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        skip = true;
                        continue;
                    }
                    return;
                }
                FindGateIndex(objects, item);
            }

            Items.Clear();
            Connections.Clear();

            ParentPanel.ListControl.SuspendLayout();

            switch (ordering)
            {
                case SortType.Columns:
                    SortInColumns(objects);
                    break;
                case SortType.Rows:
                    SortInRows(objects);
                    break;
                case SortType.Rectange:
                    SortInRectangle(objects);
                    break;
                case SortType.Circle:
                    SortInCircle(objects);
                    break;
                case SortType.ByObjects:
                    SortByObjects(objects);
                    break;
                case SortType.Snake:
                    SortSnake(objects);
                    break;
                case SortType.Maze:
                    while(!SortMaze(objects));
                    break;
            }

            for (int i = 1; i < Items.Count; ++i)
            {
                Items[i - 1].Next = Items[i];
            }

            if (ordering == SortType.Circle)
            {
                foreach (var connection in Connections)
                {
                    connection.StartPoint = connection.StartItem.Center;
                    connection.EndPoint = connection.EndItem.Center;
                }
            }

            Offset = Point.Empty;

            ParentPanel.ListControl.ResumeLayout();
            Invalidate();
        }

        public void arrange()
        {
          
            var controls = ParentPanel.ListControl.PreviewControls;
            int x = 40;
            int y = 40;
            for (int i = 0; i < controls.Count; ++i)
            {
                var control = controls[i];
                var item = GrafikaItem.Create(this, new Point(x, y), control.GrafikaObject, false);
                Items.Add(item);
                x += 160;
                if (x >= 1600)
                {
                    x = 40;
                    y += 120;
                }
            }
            for (int i = 1; i < Items.Count; ++i)
            {
                Items[i - 1].Next = Items[i];
            }            
        }

        public void addSequenceChoosing()
        {
                  
            LamsOptional opt = new LamsOptional();
            opt.TitleText = "Lekcije";

            var controls = ParentPanel.ListControl.PreviewControls;
            for (int i = 0; i < controls.Count; ++i)
            {
                var control = controls[i];
                var obj = control.GrafikaObject;     
                if(obj is LamsQa)
                {
                    ((LamsQa)obj).Optional = true;
                }          
                opt.SubObjects.Add(obj);
                
            }
            var optItem = GrafikaItem.Create(this, new Point(200, 400), opt, false);
            optItem.SequenceChoosing = true;
            Items.Add(optItem);
            
                       
            LamsBranch branch = new LamsBranch();

            branch.SequenceChoosing = true;
                        

            GrafikaBranchStartItem start = new GrafikaBranchStartItem(this, new Point(200, 0), branch, false);
            GrafikaBranchEndItem end = new GrafikaBranchEndItem(start);
            end.Location = new Point(600, 0);



            //Kako pronaci prvi i poslednji item u sekvenci
            var startItem = Items.Where(item => !(item is GrafikaBranchEndItem) && item.Previous == null).ElementAt(0);
            var endItem = Items.Where(item => !(item is GrafikaBranchStartItem) && item.Next == null).ElementAt(0);
            

            GrafikaBranchConnection prvaGrana = new GrafikaBranchConnection(start, startItem);
            GrafikaBranchConnection drugaGrana = new GrafikaBranchConnection(start, optItem);

          


            start.Previous = null;
            end.Next = null;                                   
            
            optItem.Next = end;
            endItem.Next = end;

            branch.DefaultBranch = prvaGrana;

            prvaGrana.SequenceChoosing = true;
            drugaGrana.SequenceChoosing = true;
            end.SequenceChoosing = true;
            start.SequenceChoosing = true;

            Items.Add(start);
                
            
            
        }

        public void removeSequenceChoosingTmpElements()
        {
           Items.RemoveAll(x => !x.Initialized);
           Connections.RemoveAll(x => !x.EndItem.Initialized || !x.StartItem.Initialized);
        }

        public void ValidateObjects()
        {
            var objects = ParentPanel.Objects;
            for (int i = 0; i < Items.Count; ++i)
            {
                var item = Items[i];
                if (item.GrafikaObject is LamsTool)
                {
                    var obj = (LamsTool) item.GrafikaObject;
                    if (!objects.Contains_(obj))
                    {
                        if (item.Previous != null)
                        {
                            item.Previous.Next = item.Next;
                        }
                        Items.Remove(item);
                        --i;
                    }
                }
            }
            for (int i = 0; i < Connections.Count; ++i)
            {
                var conn = Connections[i];
                if (!Items.Contains(conn.StartItem) || !Items.Contains(conn.EndItem))
                {
                    conn.Delete();
                    --i;
                }
            }
        }

        public GrafikaItem ObjectAt(Point p)
        {
            for (int i = Items.Count - 1; i >= 0; --i)
            {
                var obj = Items[i];
                if (obj.Bounds.Contains(p))
                {
                    return obj;
                }
            }
            return null;
        }

        public GrafikaConnection ConnectionAt(Point p)
        {
            for (int i = Connections.Count - 1; i >= 0; --i)
            {
                var c = Connections[i];
                if (c.CheckMouse(p))
                {
                    return c;
                }
            }
            return null;
        }

        private GrafikaArrow[] _arrows;

        private GrafikaArrow ArrowAt(Point p)
        {
            foreach (var arrow in _arrows)
            {
                if (arrow.Bounds.Contains(p))
                {
                    return arrow;
                }
            }
            return null;
        }

        private bool CheckArrows()
        {
            foreach (var arrow in _arrows)
            {
                if (arrow.ScrollStarted)
                {
                    return true;
                }
            }
            return false;
        }

        private bool StopArrows()
        {
            foreach (var arrow in _arrows)
            {
                if (arrow.MouseUp())
                {
                    return true;
                }
            }
            return false;
        }

        public int TranslateToGrid(int i)
        {
            return (i + GridSize/2 - 1)/GridSize*GridSize;
        }

        public Point TranslateToGrid(Point p)
        {
            return new Point(TranslateToGrid(p.X), TranslateToGrid(p.Y));
        }

        public Point TranslateOffset(Point p)
        {
            return new Point((int)(p.X / Zoom) - Offset.X, (int)(p.Y / Zoom) - Offset.Y);
        }

        private Point LowestCoordinate
        {
            get
            {
                if (Items.Count == 0)
                {
                    return Point.Empty;
                }

                var p = new Point(int.MaxValue, int.MaxValue);
                foreach (var item in Items)
                {
                    if (item.X < p.X)
                    {
                        p.X = item.X;
                    }
                    if (item.Y < p.Y)
                    {
                        p.Y = item.Y;
                    }
                }
                return new Point(-p.X + 20, -p.Y + 20);
            }
        }

        public void NormalizeCoordinates()
        {
            var p = LowestCoordinate;
            if (p == Point.Empty)
            {
                return;
            }

            foreach (var item in Items)
            {
                item.Location = new Point(item.X + p.X, item.Y + p.Y);
            }
            Offset = new Point(Offset.X - p.X, Offset.Y - p.Y);
        }

        public void CenterCoordinates()
        {
            Offset = LowestCoordinate;
        }

        private void GrafikaCanvas_SizeChanged(object sender, EventArgs e)
        {
            int w = Width - 140;
            int h = Height - 140;
            _arrows[0].Location = new Point(w + 40, h);
            _arrows[1].Location = new Point(w + 40, h + 80);
            _arrows[2].Location = new Point(w, h + 40);
            _arrows[3].Location = new Point(w + 80, h + 40);
            AddListener.CalculateSize();
            ConnectListener.CalculateSize();
            MoveItemListener.CalculateSize();
            Invalidate();
        }

        private void GrafikaCanvas_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.Bilinear;

            g.ScaleTransform(Zoom, Zoom);
            if (ShowGrid)
            {
                DrawGrid(g);
            }

            if (ProjectSingleton.Project != null)
            {
                g.TranslateTransform(Offset.X, Offset.Y);

                try
                {
                    foreach (var a in Items)
                    {
                        a.Draw(g);
                    }
                    foreach (var c in Connections)
                    {
                        c.Draw(g);
                    }
                }
                catch (InvalidOperationException)
                {
                    Invalidate();
                    return;
                    // Pri Save() se nekad menjaju ove liste, a moguce je da se iscrtavaju u tom trenutku
                }

                if (HoverObject != null)
                {
                    if (HoverObject is GrafikaBranchStartItem)
                    {
                        g.DrawRectangle(GrafikaUtils.HoverPen, ((GrafikaBranchStartItem) HoverObject).EndItem.Bounds);
                    }
                    else if (HoverObject is GrafikaBranchEndItem)
                    {
                        g.DrawRectangle(GrafikaUtils.HoverPen, ((GrafikaBranchEndItem) HoverObject).StartItem.Bounds);
                    }
                    g.DrawRectangle(GrafikaUtils.HoverPen, HoverObject.Bounds);
                    GrafikaUtils.DrawTitleText(g, HoverObject.GrafikaObject.TitleText,
                        new Point(HoverObject.X, HoverObject.Y - 18));
                }
                if (HoverConnection != null)
                {
                    g.DrawRectangle(GrafikaUtils.HoverPen, HoverConnection.StartItem.Bounds);
                    g.DrawRectangle(GrafikaUtils.HoverPen, HoverConnection.EndItem.Bounds);
                    GrafikaUtils.DrawTitleText(g, HoverConnection.StartItem.GrafikaObject.TitleText,
                        new Point(HoverConnection.StartItem.X, HoverConnection.StartItem.Y - 18));
                    GrafikaUtils.DrawTitleText(g, HoverConnection.EndItem.GrafikaObject.TitleText,
                        new Point(HoverConnection.EndItem.X, HoverConnection.EndItem.Y - 18));
                    HoverConnection.Draw(g, true);
                }
                Listener.Draw(g);

                g.ResetTransform();
                if (Items.Count == 0)
                {
                    GrafikaUtils.DrawTitleText(
                        g,
                        "\n\n\n\nPrevucite objekte iz levog menija da biste ih dodali na platno. \n\n" +
                        "Nove objekte možete kreirati: \n" +
                        "- desnim klikom na već postojeće objekte u levom meniju \n" +
                        "- pomoću opcije Insert gornjem meniju \n" +
                        "- pomoću dijaloga Additional Activities u gornjem meniju \n" +
                        "\nDa automatski sortirate sve objekte pritisnite dugme Auto Arrange u gornjem meniju.",
                        new Rectangle(Point.Empty, Size),
                        Color.Black,
                        Color.White,
                        GrafikaUtils.TitleFont,
                        GrafikaUtils.StringFormatCenter);
                    GrafikaUtils.DrawTitleText(
                        g,
                        "\n\nPlatno je prazno.",
                        new Rectangle(Point.Empty, Size),
                        Color.Black,
                        Color.White,
                        GrafikaUtils.TitleFontBold,
                        GrafikaUtils.StringFormatCenter);
                }
            }
            else
            {
                g.ResetTransform();
            }

            foreach (var arrow in _arrows)
            {
                arrow.Draw(g);
            }
            if (HoverArrow != null && !HoverArrow.ScrollStarted)
            {
                HoverArrow.Draw(g);
            }
            //var mouse = TranslateOffset(PointToClient(Cursor.Position));
            //g.DrawString("x: " + mouse.X + " y: " + mouse.Y, Font, TextBrush, Point.Empty);
            //g.DrawString("x: " + Offset.X + " y: " + Offset.Y, Font, TextBrush, new Point(0, 20));
            //g.DrawString("z: " + Zoom, Font, TextBrush, new Point(0, 40));
        }

        private void DrawGrid(Graphics g)
        {
            int w = (int)(Width / Zoom);
            int h = (int)(Height / Zoom);
            int k = Offset.Y % GridSize;

            for (int i = Offset.X % GridSize; i < w; i += GridSize)
            {
                for (int j = k; j < h; j += GridSize)
                {
                    g.FillRectangle(GrafikaUtils.GridBrush, new Rectangle(i, j, 1, 1));
                }
            }
        }
    }
}
