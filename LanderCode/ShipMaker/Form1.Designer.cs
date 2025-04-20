namespace ShipMaker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _shipImage = new PictureBox();
            _hTrackBar = new TrackBar();
            _vTrackBar = new TrackBar();
            _saveButton = new Button();
            _updateTimer = new System.Windows.Forms.Timer(components);
            _visibleCount = new TextBox();
            _hexDump = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)_shipImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_hTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_vTrackBar).BeginInit();
            SuspendLayout();
            // 
            // _shipImage
            // 
            _shipImage.Location = new Point(12, 12);
            _shipImage.Name = "_shipImage";
            _shipImage.Size = new Size(128, 128);
            _shipImage.SizeMode = PictureBoxSizeMode.StretchImage;
            _shipImage.TabIndex = 0;
            _shipImage.TabStop = false;
            // 
            // _hTrackBar
            // 
            _hTrackBar.LargeChange = 4;
            _hTrackBar.Location = new Point(12, 146);
            _hTrackBar.Maximum = 32;
            _hTrackBar.Name = "_hTrackBar";
            _hTrackBar.Size = new Size(128, 45);
            _hTrackBar.TabIndex = 1;
            _hTrackBar.TickFrequency = 4;
            _hTrackBar.ValueChanged += TrackerMoved;
            // 
            // _vTrackBar
            // 
            _vTrackBar.LargeChange = 4;
            _vTrackBar.Location = new Point(146, 12);
            _vTrackBar.Maximum = 16;
            _vTrackBar.Name = "_vTrackBar";
            _vTrackBar.Orientation = Orientation.Vertical;
            _vTrackBar.Size = new Size(45, 128);
            _vTrackBar.TabIndex = 2;
            _vTrackBar.TickFrequency = 2;
            _vTrackBar.ValueChanged += TrackerMoved;
            // 
            // _saveButton
            // 
            _saveButton.Location = new Point(12, 275);
            _saveButton.Name = "_saveButton";
            _saveButton.Size = new Size(166, 26);
            _saveButton.TabIndex = 3;
            _saveButton.Text = "Save";
            _saveButton.UseVisualStyleBackColor = true;
            _saveButton.Click += SaveButton_Click;
            // 
            // _updateTimer
            // 
            _updateTimer.Tick += DoUpdate;
            // 
            // _visibleCount
            // 
            _visibleCount.Location = new Point(12, 197);
            _visibleCount.Name = "_visibleCount";
            _visibleCount.ReadOnly = true;
            _visibleCount.Size = new Size(166, 23);
            _visibleCount.TabIndex = 4;
            // 
            // _hexDump
            // 
            _hexDump.Location = new Point(12, 226);
            _hexDump.Name = "_hexDump";
            _hexDump.ReadOnly = true;
            _hexDump.Size = new Size(166, 43);
            _hexDump.TabIndex = 5;
            _hexDump.Text = "";
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(190, 313);
            Controls.Add(_hexDump);
            Controls.Add(_visibleCount);
            Controls.Add(_saveButton);
            Controls.Add(_vTrackBar);
            Controls.Add(_hTrackBar);
            Controls.Add(_shipImage);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "ShipMaker";
            ((System.ComponentModel.ISupportInitialize)_shipImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)_hTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)_vTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox _shipImage;
        private TrackBar _hTrackBar;
        private TrackBar _vTrackBar;
        private Button _saveButton;
        private System.Windows.Forms.Timer _updateTimer;
        private TextBox _visibleCount;
        private RichTextBox _hexDump;
    }
}
