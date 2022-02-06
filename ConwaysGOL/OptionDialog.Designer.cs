
namespace ConwaysGOL
{
    partial class OptionDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTimeInterval = new System.Windows.Forms.Label();
            this.labelCellWidth = new System.Windows.Forms.Label();
            this.labelCellHeight = new System.Windows.Forms.Label();
            this.numericUpDownTimeInterval = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCellWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCellHeight = new System.Windows.Forms.NumericUpDown();
            this.Accept = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCellWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCellHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTimeInterval
            // 
            this.labelTimeInterval.AutoSize = true;
            this.labelTimeInterval.Location = new System.Drawing.Point(129, 34);
            this.labelTimeInterval.Name = "labelTimeInterval";
            this.labelTimeInterval.Size = new System.Drawing.Size(68, 13);
            this.labelTimeInterval.TabIndex = 0;
            this.labelTimeInterval.Text = "Time Interval";
            // 
            // labelCellWidth
            // 
            this.labelCellWidth.AutoSize = true;
            this.labelCellWidth.Location = new System.Drawing.Point(129, 87);
            this.labelCellWidth.Name = "labelCellWidth";
            this.labelCellWidth.Size = new System.Drawing.Size(62, 13);
            this.labelCellWidth.TabIndex = 1;
            this.labelCellWidth.Text = "Cell\'s Width";
            // 
            // labelCellHeight
            // 
            this.labelCellHeight.AutoSize = true;
            this.labelCellHeight.Location = new System.Drawing.Point(129, 138);
            this.labelCellHeight.Name = "labelCellHeight";
            this.labelCellHeight.Size = new System.Drawing.Size(65, 13);
            this.labelCellHeight.TabIndex = 2;
            this.labelCellHeight.Text = "Cell\'s Height";
            // 
            // numericUpDownTimeInterval
            // 
            this.numericUpDownTimeInterval.Location = new System.Drawing.Point(214, 34);
            this.numericUpDownTimeInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTimeInterval.Name = "numericUpDownTimeInterval";
            this.numericUpDownTimeInterval.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownTimeInterval.TabIndex = 3;
            // 
            // numericUpDownCellWidth
            // 
            this.numericUpDownCellWidth.Location = new System.Drawing.Point(214, 85);
            this.numericUpDownCellWidth.Name = "numericUpDownCellWidth";
            this.numericUpDownCellWidth.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownCellWidth.TabIndex = 4;
            // 
            // numericUpDownCellHeight
            // 
            this.numericUpDownCellHeight.Location = new System.Drawing.Point(214, 130);
            this.numericUpDownCellHeight.Name = "numericUpDownCellHeight";
            this.numericUpDownCellHeight.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownCellHeight.TabIndex = 5;
            // 
            // Accept
            // 
            this.Accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Accept.Location = new System.Drawing.Point(132, 190);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 23);
            this.Accept.TabIndex = 6;
            this.Accept.Text = "OK";
            this.Accept.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(258, 189);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // OptionDialog
            // 
            this.AcceptButton = this.Accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(462, 246);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.numericUpDownCellHeight);
            this.Controls.Add(this.numericUpDownCellWidth);
            this.Controls.Add(this.numericUpDownTimeInterval);
            this.Controls.Add(this.labelCellHeight);
            this.Controls.Add(this.labelCellWidth);
            this.Controls.Add(this.labelTimeInterval);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCellWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCellHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTimeInterval;
        private System.Windows.Forms.Label labelCellWidth;
        private System.Windows.Forms.Label labelCellHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownCellWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownCellHeight;
        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button Cancel;
    }
}