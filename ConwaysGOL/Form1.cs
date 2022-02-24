using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ConwaysGOL
{
    //Todo: maybe implement a party mode button, idea- set background to black, make a glider in the center, play and have the color change from the cells every 2-5 iterations
    public partial class Form1 : Form
    {
        //member variables
        private bool[,] cells;
        private int cellsX, cellsY;
        private int Generations = 0;
        private int CellsAlive = 0;
        private int Seed;
        private string BoundaryType = String.Empty;
        private bool showNeighbor, showGrid, isToroidal, showHUD;
        private Color PenColor, BrushColor;

        public Form1()
        {
            InitializeComponent();
            LoadOrUpdateProperties();

            //if the previous setting the user had selected was toroidal then then menu for toroidal will be one else check the finite box
            if (isToroidal)
            {
                toroidalToolStripMenuItem.Checked = true;
                BoundaryType = "Toroidal";
            }
            else
            {
                finiteToolStripMenuItem.Checked = true;
                BoundaryType = "Finite";
            }
            
            if (showHUD == true) hUDToolStripMenuItem.Checked = true;
            else hUDToolStripMenuItem.Checked = false;
        }

        #region Drawing Universe, Rules, Neighbor count and show method, Cells Alive, Generation Count, timer
        private void DrawUniverse(object sender, PaintEventArgs e)
        {
            //Gets the width of the DrawPanel, the window that does not have the controls, and divides it by the size. It is a float because it draws to the whole panel and avoid white space.
            float cellWidth = (float)DrawPanel.ClientSize.Width / cellsX; // example: Panel 100, Cells 10 = 10
            float cellHeight = (float)DrawPanel.ClientSize.Height / cellsY;

            Pen pen = new Pen(PenColor);
            Brush brush = new SolidBrush(BrushColor);

            // iterating through the array
            for (int y = 0; y < cellsY; y++)
            {
                for (int x = 0; x < cellsX; x++)
                {
                    //make a rect then determine where the start of the x and y will be by multplying the cellWidth and the iterator
                    RectangleF cellRect = RectangleF.Empty;
                    cellRect.X = cellWidth * x; // example width 10 * 0 = 0
                    cellRect.Y = cellHeight * y;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    //if cell is alive it will fill the rect with the brush color
                    if (cells[x, y])
                    {
                        e.Graphics.FillRectangle(brush, cellRect);
                    }

                    //if grid wants to be seen then the grid will be drawn if not it won't draw anything
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
            ShowLiveCells();
            ShowHUD(e);
        }

        //implementing the rule for Conways game of life
        private void RuleForCell()
        {
            bool[,] scratchPad = new bool[cellsX, cellsY];
           
            int neighbors = 0;
            
            for (int y = 0; y < cellsY; y++)
            {
                for (int x = 0; x < cellsX; x++)
                {
                    //checks to which boundary type is on and does the corresponding neighbor method
                    if (finiteToolStripMenuItem.Checked) neighbors = NeighborsCountFinite(x, y);
                    else if (toroidalToolStripMenuItem.Checked) neighbors = NeighborsCountToroidal(x, y);
                    
                    //is cell alive or dead
                    if (cells[x, y])
                    {
                        // if neighbor is cell than two or more than three the cell will die, else the neighbor is 2 or 3 so the cell stays alive
                        if (neighbors < 2 || neighbors > 3) scratchPad[x, y] = false;
                        else scratchPad[x, y] = true;
                    }
                    else if (!cells[x, y])
                    {
                        if(neighbors == 3) scratchPad[x, y] = true;
                    }
                }
            }
            // assigning what the scratch pad has to the other array
            cells = scratchPad;
        }

        private int NeighborsCountFinite(int row, int col)
        {
            int neighbors = 0;

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    // get the current index of the cell and adds an offset to find the neighbors index. EX. cell[2,2], 2+1 = 3, 2+(-1) = 1, the top right neighbor index is [3,1]
                    int neighborsX = row + xOffset;
                    int neighborsY = col + yOffset;

                    if (xOffset == 0 && yOffset == 0) continue; // if offset is zero on both axis then we are on ourselves continue
                    else if (neighborsX < 0 || neighborsX >= cellsX) continue; //x is less than zero or same index or greater as the size of the array it is out of bounds continue,
                    else if (neighborsY < 0 || neighborsY >= cellsY) continue; //same as above but for y
                    else if (cells[neighborsX, neighborsY]) neighbors++; // if cell is alive increase the neighbor count
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
                    // same as finite method
                    int neighborsX = row + xOffset;
                    int neighborsY = col + yOffset;

                    if (xOffset == 0 && yOffset == 0) continue; // same as finite method
                    if(neighborsX < 0) neighborsX = cellsX - 1; //neighbor index less than zero we want it to be the last index of the x so it loops around
                    if(neighborsX >= cellsX) neighborsX = 0; // neighbor index greater than the index of the x then it will be zero
                    
                    //same as above but for y index
                    if(neighborsY < 0) neighborsY = cellsY -1;
                    if(neighborsY >= cellsY) neighborsY = 0;

                    if (cells[neighborsX, neighborsY]) neighbors++; // if cell is alive then increment neighbors
                }
            }
            return neighbors;
        }
        
        private void ShowNeighbors(PaintEventArgs e, int row, int col, RectangleF cellRect)
        {
            // return if neighbors don't want to be shown
            if (!showNeighbor)
            {
                return;
            }

            int neighborCount = 0;
            //which boundary is checked, do the correct neighbor check and uncheck the other menu item
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
            
            // aligns everything to the center
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // only want to show a number if the neigbor count is more than zero
            if (neighborCount > 0)
            {
                    brush = new SolidBrush(Color.Green);
                    e.Graphics.DrawString(neighborCount.ToString(), font, brush, cellRect, format);
                    brush.Dispose();
            }
        }
        
        private void ChangeStateOnPanelClick(object sender, MouseEventArgs e)
        {
            // if button is the left mouse button the check this
            if (e.Button == MouseButtons.Left)
            {
                float cellWidth = (float)DrawPanel.ClientSize.Width / cellsX;
                float cellHeight = (float)DrawPanel.ClientSize.Height / cellsY;
                int row = (int)(e.X / cellWidth);
                int col = (int)(e.Y / cellHeight);
                //when clicked it will do the opposite of current state
                cells[row, col] = !cells[row, col];
                if (cells[row, col]) CellsAlive++;
                else CellsAlive--;
                DrawPanel.Invalidate();
            }
        }
        
        private void ShowGen()
        {
            // changes the label to the current gen
            GenerationStripStatusLabel.Text = "Generations = " + Generations.ToString();
            statusStrip1.Refresh();
        }
        
        private void ShowLiveCells()
        {
            // changes the label to the current number of live cells
            cellsAliveStripStatusLabel.Text = "Cells Alive = " + CellsAlive.ToString();
            statusStrip1.Refresh();
        }

        private void NextGen()
        {
            RuleForCell();
            Generations++;
            ShowGen();
        }
        
        private void cellsAlive()
        {
            int liveCells = 0;
            for (int y = 0; y < cellsY; y++)
            {
                for (int x = 0; x < cellsX; x++)
                {
                    if (cells[x, y]) liveCells++;
                }
            }
            CellsAlive = liveCells;
           
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGen();
            DrawPanel.Invalidate();
        }
        #endregion

        #region New, Open, Save, Exit for both file menu and tool strip
        private void NewStripButton_Click(object sender, System.EventArgs e)
        {
            //When the new button or the menu item is clicked make a new array of the current size, generation is reset and update the panel to show the changes
            cells = new bool[cellsX, cellsY];
            Generations = 0;
            CellsAlive = 0;
            DrawPanel.Invalidate();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Cells|*.cells";
            openFileDialog.DefaultExt = "cells";
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                StreamReader sr = new StreamReader(openFileDialog.FileName);
                string sizeX = sr.ReadLine();
                string sizeY = sr.ReadLine();

                int width = 0;
                int height = 0;

                if (int.TryParse(sizeX.Trim('!'), out width)) cellsX = width;
                if (int.TryParse(sizeY.Trim('!'), out height)) cellsY = height;

                cells = new bool[cellsX, cellsY];
                for (int y = 0; y < height; y++)
                {
                    String row = sr.ReadLine();
                    for (int x = 0; x < width; x++)
                    {
                        if (row[x] == 'O') cells[x, y] = true;
                        else if (row[x] == '.') cells[x, y] = false;
                    }
                }
                DrawPanel.Invalidate();
                sr.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Cells|*.cells";
            saveDialog.DefaultExt = "cells";

            if (DialogResult.OK == saveDialog.ShowDialog())
            {
                StreamWriter sw = new StreamWriter(saveDialog.FileName);
                sw.WriteLine($"!{cellsX}");
                sw.WriteLine($"!{cellsY}");
                for (int y = 0; y < cellsY; y++)
                {
                    String writeRow = String.Empty;
                    for (int x = 0; x < cellsX; x++)
                    {
                        if (cells[x, y]) writeRow += 'O';
                        else writeRow += '.';

                    }
                    sw.WriteLine(writeRow);
                    writeRow = String.Empty;
                }
                sw.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region View menu items
        private void hUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showHUD = !showHUD;
            DrawPanel.Invalidate();
        }

        private void ShowHUD(PaintEventArgs e)
        {

            if (showHUD)
            {
                Font font = new Font("Times New Roman", 12.0f);
                StringFormat format = new StringFormat();

                // aligns everything to the center
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;
                Rectangle rect = new Rectangle(10, ClientSize.Height - 175, 300, 100);
                e.Graphics.DrawString($"Current Generation: {Generations.ToString()} " +
                    $"\nCells Alive: {CellsAlive.ToString()} \nUniverse Size (x,y): {cellsX.ToString()}, {cellsY}" +
                    $"\nBoundary Type: {BoundaryType}", font, Brushes.Black, rect, format);
            }
        }

        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // was button turned on if it was they want to see neighbors if it was turned off they don't want to see neighbors update
            if (neighborCountToolStripMenuItem.Checked) showNeighbor = true;
            else showNeighbor = false;
            DrawPanel.Invalidate();
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // was the button turned on if is they want to see the grid, if turned off they don't update
            showGrid = !showGrid;
            DrawPanel.Invalidate();
        }

        private void toroidalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if toroidal button was clicked they want that boundary, uncheck the finite and update
            isToroidal = true;
            BoundaryType = "Toroidal";
            finiteToolStripMenuItem.Checked = false;
            toroidalToolStripMenuItem.Checked = true;
            DrawPanel.Invalidate();
        }

        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //same as toroidal method but reversed for the finite
            isToroidal = false;
            BoundaryType = "Finite";
            toroidalToolStripMenuItem.Checked = false;
            finiteToolStripMenuItem.Checked = true;
            DrawPanel.Invalidate();
        }
        #endregion

        #region Run Menu items plus the tool strip
        private void PlayButton_Click(object sender, EventArgs e)
        {
            // when button/menu is pressed pause will now be enabled and play and next will be disabled, start the timer to increase the generation count
            PauseStripButton.Enabled = true;
            NextStripButton.Enabled = false;
            PlayStripButton.Enabled = false;
            timer.Enabled = true; 
        }
        
        private void NextStripButton_Click(object sender, EventArgs e)
        {
            // when button/menu item is clicked do the rules for the game increase the count, update the screen and then show the new generation count
            RuleForCell();
            Generations++;
            DrawPanel.Invalidate();
            ShowGen();
        }
        
        private void PauseButton_Click(object sender, EventArgs e)
        {
            // when clicked it will stop the timer, disable the pause button and enable the play and next button
            timer.Enabled = false;
            PauseStripButton.Enabled = false;
            NextStripButton.Enabled = true;
            PlayStripButton.Enabled = true;
        }

        #endregion

        #region Randomize Menu items
        private void fromSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // make new array by clearing the old one
            Random rand;
            // make custom dialog box
            RandomBySeed dialog = new RandomBySeed();
            // set the custom dialog box seed to current seed
            dialog.Seed = Seed;
            //was ok pressed
            if (DialogResult.OK == dialog.ShowDialog())
            {
                cells = new bool[cellsX, cellsY];
                // rand will now be the seed that the user put into the box
                rand = new Random(dialog.Seed);
                Seed = dialog.Seed;
                for (int y = 0; y < cellsY; y++)
                {
                    for (int x = 0; x < cellsX; x++)
                    {
                        // gets the next number from 0-1, if it is cell that cell will now be alive
                        if (rand.Next(0, 2) == 1) cells[x, y] = true;
                    }
                }
            }
            cellsAlive();
            DrawPanel.Invalidate();
        }

        private void RandomizeByTime(object sender, EventArgs e)
        {
            //make new array by clearing the old one
            cells = new bool[cellsX, cellsY];
            //default constructor use time dependent seed value
            Random rand = new Random();
            for (int y = 0; y < cellsY; y++)
            {
                for (int x = 0; x < cellsX; x++)
                {
                    // gets the next number from 0-1 if it is 1 the cell is now alive
                    if (rand.Next(0, 2) == 1) cells[x, y] = true;
                }
            }
            cellsAlive();
            DrawPanel.Invalidate();
        }

        #endregion

        #region Settings Menu items
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
         {
            // make custom dialog box then set the color selector to the current color if known
            ColorDialog color = new ColorDialog();
            color.Color = DrawPanel.BackColor;

            // if ok was pressed the backgroud color will now change
            if (DialogResult.OK == color.ShowDialog()) DrawPanel.BackColor = color.Color;      
         }

        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // make custom dialog box, then set the color selector to the current color if known
            ColorDialog color = new ColorDialog();
            color.Color = BrushColor;

            // if ok was pressed the color will now be what they selected, update to show the change in cells
            if(DialogResult.OK == color.ShowDialog())
            {
                BrushColor = color.Color;
                DrawPanel.Invalidate();
            }
        }

        private void gridLinesColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // custom dialog box and then set the color selector to the current color if known            
            ColorDialog color = new ColorDialog();
            color.Color = PenColor;

            // if ok was pressed then the new color will be what the user selected, update to show the grid lines with the new pen color
            if(DialogResult.OK == color.ShowDialog())
            {
                PenColor = color.Color;
                DrawPanel.Invalidate();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // makes custom dialog box sets the value inside to member variables
            OptionDialog options = new OptionDialog();
            options.TimeInterval = timer.Interval;
            options.CellWidth = cellsX;
            options.CellHeight = cellsY;
            
            // if ok was pressed do some more checks
            if (DialogResult.OK == options.ShowDialog())
            {
                //if the timer didn't change then make a new board with the cell size that was requested
                if(options.TimeInterval == timer.Interval)
                {
                    cellsX = options.CellWidth;
                    cellsY = options.CellHeight;
                    cells = new bool[cellsX, cellsY];
                }
                // if the timer is not the same but the cell sizes stayed the same then just update the timer no need to clear the board
                else if(options.TimeInterval != timer.Interval && options.CellWidth == cellsX && options.CellHeight == cellsY) 
                {
                    timer.Interval = options.TimeInterval;
                }
                // if anything else such as timer and cellx change it will make the board and update the timer
                else
                {
                    timer.Interval = options.TimeInterval;
                    cellsX = options.CellWidth;
                    cellsY = options.CellHeight;
                    cells = new bool[cellsX, cellsY];
                }
                
                DrawPanel.Invalidate();
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // will set everything back to the default settings and update
            Properties.Settings.Default.Reset();
            LoadOrUpdateProperties();
            DrawPanel.Invalidate();
        }
        
        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // will set everything back to the last user save data
            Properties.Settings.Default.Reload();
            LoadOrUpdateProperties();
            DrawPanel.Invalidate();
        }
        #endregion

        private void LoadOrUpdateProperties()
        {
            // everything here will set the member variables to what was last saved by system or default value I set
            DrawPanel.BackColor = Properties.Settings.Default.BackColor;
            PenColor = Properties.Settings.Default.GridColor;
            BrushColor = Properties.Settings.Default.CellColor;
            showGrid = Properties.Settings.Default.ShowGrid;
            showNeighbor = Properties.Settings.Default.ShowNeighbor;
            isToroidal = Properties.Settings.Default.IsToroidal;
            showHUD = Properties.Settings.Default.ShowHUD;
            cellsX = Properties.Settings.Default.CellX;
            cellsY = Properties.Settings.Default.CellY;
            neighborCountToolStripMenuItem.Checked = showNeighbor;
            gridToolStripMenuItem.Checked = showGrid;
            timer.Interval = Properties.Settings.Default.Interval;
            cells = new bool[cellsX, cellsY];
            Seed = Properties.Settings.Default.Seed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Everything here will update/save the users current settings 
            Properties.Settings.Default.BackColor = DrawPanel.BackColor;
            Properties.Settings.Default.GridColor = PenColor;
            Properties.Settings.Default.CellColor = BrushColor;
            Properties.Settings.Default.ShowGrid = showGrid;
            Properties.Settings.Default.ShowNeighbor = showNeighbor;
            Properties.Settings.Default.ShowHUD = showHUD;
            Properties.Settings.Default.IsToroidal = isToroidal;
            Properties.Settings.Default.CellX = cellsX;
            Properties.Settings.Default.CellY = cellsY;
            Properties.Settings.Default.Interval = timer.Interval;
            Properties.Settings.Default.Seed = Seed;
            Properties.Settings.Default.Save();
        }
    }
}
