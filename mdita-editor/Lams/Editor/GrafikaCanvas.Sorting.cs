using System;
using System.Collections.Generic;
using System.Drawing;

namespace mDitaEditor.Lams.Editor
{
    partial class GrafikaCanvas
    {
        public enum SortType
        {
            Columns,
            Rows,
            Rectange,
            Circle,
            ByObjects,
            Snake,
            Maze
        }

        private void SortInColumns(List<IGrafikaObject> objects)
        {
            int x = 40;
            int y = 40;
            for (int i = 0; i < objects.Count; ++i)
            {
                var obj = objects[i];

                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                x += 160;
                if (x >= Width/Zoom - 160)
                {
                    x = 40;
                    y += 120;
                }
            }
        }

        private void SortInRows(List<IGrafikaObject> objects)
        {
            int x = 40;
            int y = 40;
            for (int i = 0; i < objects.Count; ++i)
            {
                var obj = objects[i];
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                y += 120;
                if (y >= Height/Zoom - 120)
                {
                    y = 40;
                    x += 160;
                }
            }
        }

        private void SortInRectangle(List<IGrafikaObject> objects)
        {
            var side = objects.Count/4f;
            int sideCount = (int) side;
            if (side > sideCount)
            {
                ++sideCount;
            }

            int x = 40;
            int y = 40;
            int i = 0;

            for (int j = 0; j < sideCount; ++j)
            {
                var obj = objects[i++];
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                x += 160;
            }
            for (int j = 0; j < sideCount; ++j)
            {
                var obj = objects[i++];
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                y += 120;
            }
            for (int j = 0; j < sideCount; ++j)
            {
                if (i >= objects.Count)
                {
                    break;
                }
                var obj = objects[i++];
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                x -= 160;
            }
            for (int j = 0; j < sideCount; ++j)
            {
                if (i >= objects.Count)
                {
                    break;
                }
                var obj = objects[i++];
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                y -= 120;
            }
        }

        private void SortInCircle(List<IGrafikaObject> objects)
        {
            var side = objects.Count/4f;
            int sideCount = (int) side;
            if (side > sideCount)
            {
                ++sideCount;
            }

            int x = 40;
            int y = 40 + sideCount*120;
            int i = 0;

            for (int j = 0; j < sideCount; ++j)
            {
                var obj = objects[i++];
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                int dx = (int) (240*((float) (j + 1)/(sideCount + 1)));
                int dy = 240 - dx;
                x += dx;
                y -= dy;
            }
            for (int j = 0; j < sideCount; ++j)
            {
                var obj = objects[i++];
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                int dy = (int) (240*((float) (j + 1)/(sideCount + 1)));
                int dx = 240 - dy;
                x += dx;
                y += dy;
            }
            for (int j = 0; j < sideCount; ++j)
            {
                if (i >= objects.Count)
                {
                    break;
                }
                var obj = objects[i++];
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                int dx = (int) (240*((float) (j + 1)/(sideCount + 1)));
                int dy = 240 - dx;
                x -= dx;
                y += dy;
            }
            for (int j = 0; j < sideCount; ++j)
            {
                if (i >= objects.Count)
                {
                    break;
                }
                var obj = objects[i++];
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                int dy = (int) (240*((float) (j + 1)/(sideCount + 1)));
                int dx = 240 - dy;
                x -= dx;
                y -= dy;
            }
        }

        private void SortByObjects(List<IGrafikaObject> objects)
        {
            int x = -120;
            int y = 40;
            for (int i = 0; i < objects.Count; ++i)
            {
                var obj = objects[i];
                if (obj is LamsNoticeboard)
                {
                    x += 160;
                    y = 40;
                }
                else
                {
                    y += 120;
                }
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);
            }
        }

        private void SortSnake(List<IGrafikaObject> objects)
        {
            int x = 40;
            int y = 40;
            int sign = 1;
            for (int i = 0; i < objects.Count; ++i)
            {
                var obj = objects[i];
                var item = GrafikaItem.Create(this, new Point(x - (obj is LamsGate || obj is LamsOptional ? 0 : 20), y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                x += sign*160;
                if (i == 0)
                {
                    x -= 20;
                    y += 100;
                }
                else if (x <= 0 || x >= Width/Zoom - 160)
                {
                    sign *= -1;
                    x += sign*300;
                    y += 100;
                }
            }
        }

        private bool SortMaze(List<IGrafikaObject> objects)
        {
            var rand = new Random();
            int x = 40;
            int y = 40;
            var usedPoints = new HashSet<Point>();
            for (int i = 0; i < objects.Count; ++i)
            {
                var obj = objects[i];
                var p = new Point(x, y);
                var item = GrafikaItem.Create(this, obj is LamsGate || obj is LamsOptional ? p : new Point(p.X - 20, p.Y), obj);
                Items.Add(item);
                ParentPanel.ListControl.HideObject(item.GrafikaObject);

                var adjecent = new[]
                {
                    new Point(x + 140, y + 100), new Point(x + 140, y - 100),
                    new Point(x - 140, y + 100), new Point(x - 140, y - 100)
                };
                if (usedPoints.Contains(adjecent[0]) && usedPoints.Contains(adjecent[1]) &&
                    usedPoints.Contains(adjecent[2]) && usedPoints.Contains(adjecent[3]))
                {
                    Items.Clear();
                    return false;
                }

                usedPoints.Add(p);
                do
                {
                    p = new Point(x + (rand.NextDouble() > 0.5 ? 140 : -140),
                        y + (rand.NextDouble() > 0.5 ? 100 : -100));
                } while (usedPoints.Contains(p));
                x = p.X;
                y = p.Y;
            }
            ParentPanel.Canvas.NormalizeCoordinates();
            return true;
        }
    }
}
