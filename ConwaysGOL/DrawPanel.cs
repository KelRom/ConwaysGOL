using System.Windows.Forms;

namespace ConwaysGOL
{
    public class DrawPanel : Panel
    {
        public DrawPanel()
        {
            this.DoubleBuffered = true;

            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
    }
}
