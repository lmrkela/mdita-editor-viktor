using System;
using System.Windows.Forms;

namespace mDitaEditor.Dita.Controls
{
    public partial class DivControl
    {
        private bool _isDragging = false;
        private int _DDradius = 40;
        private int _mX = 0;
        private int _mY = 0;

        /// <summary>
        /// Na mouse down čuvamo kordinate pritiska miša i stavljamo status isDragging na false
        /// jer još uvek korisnik nije počeo da vuče miša. Provera da li vuče miša se radi u metodi
        /// Mouse Move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DivControl_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
            _mX = e.X;
            _mY = e.Y;
            _isDragging = false;
        }

        /// <summary>
        /// Proverava da li korisnik pomera miša dok je MouseDown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DivControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                return;
            }
            // Provera da li se miš pomera dok je levi klik pritisnut
            // Bez ovoga DragNDrop će se pozvati odma čim se kontrola klikne
            if (e.Button == MouseButtons.Left && _DDradius > 0)
            {
                int num1 = _mX - e.X;
                int num2 = _mY - e.Y;
                if (((num1*num1) + (num2*num2)) > _DDradius)
                {
                    DoDragDrop(this, DragDropEffects.All);
                    _isDragging = true;
                }
            }
        }

        /// <summary>
        /// Kada levi klik miša nije pritisnut ne radimo Drag n drop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DivControl_MouseUp(object sender, EventArgs e)
        {
            _isDragging = false;
        }

        /// <summary>
        /// Postavlja slicicu za Move da je vidljiva
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DivControl_MouseEnter(object sender, EventArgs e)
        {
            panTransparent.SendToBack();
            picMove.Visible = true;
            picMove.BringToFront();
        }

        /// <summary>
        /// Na kraju draga skida transparentni panel sa webBrowser kontrole kako bi
        /// mogli da koristimo webBrowser kontorlu dalje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DivControl_MouseLeave(object sender, EventArgs e)
        {
            var p = PointToClient(Cursor.Position);
            picMove.Visible = p.X > 3 && p.X < Width - 4 && p.Y > 3 && p.Y < Height - 4;
        }

        /// <summary>
        /// Na kraju draga skida transparentni panel sa webBrowser kontrole kako bi
        /// mogli da koristimo webBrowser kontorlu dalje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panTransparent_MouseLeave(object sender, EventArgs e)
        {
            panTransparent.SendToBack();
        }

        /// <summary>
        /// Pri početku Drag-a stavlja transparentni panel preko webBrowser kontrole kako bi
        /// mogao da se radi Drag n drop preko nje
        /// </summary>
        public void StartDrag()
        {
            panTransparent.BringToFront();
        }
    }
}
