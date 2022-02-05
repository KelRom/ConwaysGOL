using System;
using System.Drawing;
using System.Windows.Forms;

namespace ConwaysGOL
{
    //Todo: Implement Randomize by seed
    //Todo: implement all settings menu tasks, task dialog, color dialog
    //Todo: Implement a way to open a file, openfile dialog, or file dialog
    //Todo: Implement a way to save current config for cells and settings, savefiledialog

    public partial class Form1 : Form
    {
        private bool[,] cells = new bool[30, 30];
        private int cellX, cellsY;
        int Generations = 0;
        bool showNeighbor = true;
        bool showGrid = true;
       
        public Form1()
        {
            InitializeComponent();  
        }

        #region Drawing Universe, Rules, Neighbor count and show method, Cells Alive, Generation Count, timer
        private void DrawUniverse(object sender, PaintEventArgs e)
        {
            float cellWidth = (float)DrawPanel.ClientSize.Width / cells.GetLength(0);
            float cellHeight = (float)DrawPanel.ClientSize.Height / cells.GetLength(1);

            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(Color.Gray);
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    RectangleF cellRect = RectangleF.Empty;
                    cellRect.X = cellWidth * x;
                    cellRect.Y = cellHeight * y;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    if (cells[x, y])
                    {
                        e.Graphics.FillRectangle(brush, cellRect);
                    }

                    if (showGrid)
                    { 
                        e.Graphics.DrawRectangle(pen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);
                    }    
                    ShowNeighbors(e, x, y, cellRect);
                }
            }
            brush.Dispose();
            pen.Dispose();

            ShowGen();
            cellsAlive();
        }

        private void RuleForCell()
        {
            bool[,] scratchPad = new bool[cells.GetLength(0), cells.GetLength(1)];
           
            int neighbors = 0;
            
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    if (finiteToolStripMenuItem.Checked)
                    {
                        neighbors = NeighborsCountFinite(x, y);
                    }

                    else if (toroidalToolStripMenuItem.Checked)
                    {
                        neighbors = NeighborsCountToroidal(x, y);
                    }
                    
                    if (cells[x, y])
                    {
                        if (neighbors < 2 || neighbors > 3)
                        {
                            scratchPad[x, y] = false;
                        }
                        else
                        {
                            scratchPad[x, y] = true;
                        }
                    }
                    else if (!cells[x, y])
                    {
                        if(neighbors == 3)
                        {
                            scratchPad[x, y] = true;
                        }
                    }
                }
            }
            cells = scratchPad;
        }

        private int NeighborsCountFinite(int row, int col)
        {
            int neighbors = 0;

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int neighborsX = row + xOffset;
                    int neighborsY = col + yOffset;

                    if (xOffset == 0 && yOffset == 0) continue;
                    else if (neighborsX < 0 || neighborsX >= cells.GetLength(0)) continue;
                    else if (neighborsY < 0 || neighborsY >= cells.GetLength(0)) continue;
                    else if (cells[neighborsX, neighborsY]) neighbors++;
                }
            }
            return neighbors;
        }

        private int NeighborsCountToroidal(int row, int col)
        {
            int neighbors = 0;
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int neighborsX = row + xOffset;
                    int neighborsY = col + yOffset;

                    if (xOffset == 0 && yOffset == 0) continue;
                    if(neighborsX < 0) neighborsX = cells.GetLength(0) - 1;
                    if(neighborsX >= cells.GetLength(0)) neighborsX = 0;
                    
                    if(neighborsY < 0) neighborsY = cells.GetLength(1) -1 ;
                    
                    if(neighborsY >= cells.GetLength(1)) neighborsY = 0;
                   
                    if (cells[neighborsX, neighborsY]) neighbors++;
                }
            }
            return neighbors;
        }
        
        private void ShowNeighbors(PaintEventArgs e, int row, int col, RectangleF cellRect)
        {

            if (!showNeighbor)
            {
                return;
            }

            int neighborCount = 0;
            if (finiteToolStripMenuItem.Checked)
            {
                toroidalToolStripMenuItem.Checked = false;
                neighborCount = NeighborsCountFinite(row, col);
            }

            else if (toroidalToolStripMenuItem.Checked)
            {
                finiteToolStripMenuItem.Checked = false;
                neighborCount = NeighborsCountToroidal(row, col);
            }

            Brush brush;
            Font font = new Font("Arial", 12.0f);
            StringFormat format = new StringFormat();

            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            if (neighborCount > 0)
            {
                    brush = new SolidBrush(Color.Green);
                    e.Graphics.DrawString(neighborCount.ToString(), font, brush, cellRect, format);
                    brush.Dispose();
            }
        }
        
        private void ChangeStateOnPanelClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float cellWidth = (float)DrawPanel.ClientSize.Width / cells.GetLength(0);
                float cellHeight = (float)DrawPanel.ClientSize.Height / cells.GetLength(1);
                int row = (int)(e.X / cellWidth);
                int col = (int)(e.Y / cellHeight);
                cells[row, col] = !cells[row, col];
                DrawPanel.Invalidate();
            }
        }
        
        private void NextGen()
        {
            RuleForCell();
            Generations++;
            ShowGen();
        }
        
        private void ShowGen()
        {
            GenerationStripStatusLabel.Text = "Generations = " + Generations.ToString();
            statusStrip1.Refresh();
        }
        
        private void cellsAlive()
        {
            int cellsAlive = 0;
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    if (cells[x, y]) cellsAlive++;
                }
            }
            cellsAliveStripStatusLabel.Text = "Cells Alive = " + cellsAlive.ToString();
            statusStrip1.Refresh();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGen();
            DrawPanel.Invalidate();
        }
        #endregion

        #region New, Exit for both file menu and tool strip
        private void NewStripButton_Click(object sender, System.EventArgs e)
        {
            cells = new bool[30, 30];
            Generations = 0;
            DrawPanel.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region View menu items
        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (neighborCountToolStripMenuItem.Checked) showNeighbor = true;
            else showNeighbor = false;
            DrawPanel.Invalidate();
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridToolStripMenuItem.Checked) showGrid = true;
            else showGrid = false;
            DrawPanel.Invalidate();
        }

        private void toroidalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finiteToolStripMenuItem.Checked = false;
            DrawPanel.Invalidate();
        }

        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toroidalToolStripMenuItem.Checked = false;
            DrawPanel.Invalidate();

        }
        #endregion

        #region Run Menu items plus the tool strip
        private void PlayButton_Click(object sender, EventArgs e)
        {
            PauseStripButton.Enabled = true;
            NextStripButton.Enabled = false;
            PlayStripButton.Enabled = false;
            timer.Interval = 150;
            timer.Enabled = true; 
        }
        
        private void NextStripButton_Click(object sender, EventArgs e)
        {
            RuleForCell();
            Generations++;
            DrawPanel.Invalidate();
            ShowGen();
        }
        
        private void PauseButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            PauseStripButton.Enabled = false;
            NextStripButton.Enabled = true;
            PlayStripButton.Enabled = true;
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            color.Color = DrawPanel.BackColor;
            if (DialogResult.OK == color.ShowDialog())
            {
                DrawPanel.BackColor = color.Color;
                DrawPanel.Invalidate();
            }
        }


        #endregion

        #region Randomize Menu items
        private void RandomizeByTime(object sender, EventArgs e)
        {
            cells = new bool[30, 30];
            Random rand = new Random(DateTime.Now.Second);
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    if (rand.Next(0, 2) == 1) cells[x, y] = !cells[x, y];
                }
            }
            DrawPanel.Invalidate();
        }

        #endregion

        #region Settings Menu items
       

        #endregion
    }
}
