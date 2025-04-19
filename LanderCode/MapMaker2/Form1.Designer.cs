namespace MapMaker2
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
            label1 = new Label();
            _seed = new MaskedTextBox();
            _generateButton = new Button();
            _randomiseButton = new Button();
            _mapPicture = new PictureBox();
            _waterHeight = new TrackBar();
            _waterHeightValue = new TextBox();
            _saveButton = new Button();
            _changedTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)_mapPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_waterHeight).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 0;
            label1.Text = "Seed:";
            // 
            // _seed
            // 
            _seed.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _seed.Location = new Point(53, 12);
            _seed.Mask = "00000000";
            _seed.Name = "_seed";
            _seed.Size = new Size(94, 23);
            _seed.TabIndex = 1;
            _seed.Text = "12345";
            _seed.ValidatingType = typeof(int);
            // 
            // _generateButton
            // 
            _generateButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _generateButton.Location = new Point(239, 12);
            _generateButton.Name = "_generateButton";
            _generateButton.Size = new Size(80, 23);
            _generateButton.TabIndex = 2;
            _generateButton.Text = "Generate";
            _generateButton.UseVisualStyleBackColor = true;
            // 
            // _randomiseButton
            // 
            _randomiseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _randomiseButton.Location = new Point(153, 12);
            _randomiseButton.Name = "_randomiseButton";
            _randomiseButton.Size = new Size(80, 23);
            _randomiseButton.TabIndex = 3;
            _randomiseButton.Text = "Randomise";
            _randomiseButton.UseVisualStyleBackColor = true;
            // 
            // _mapPicture
            // 
            _mapPicture.Location = new Point(12, 41);
            _mapPicture.Name = "_mapPicture";
            _mapPicture.Size = new Size(256, 256);
            _mapPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            _mapPicture.TabIndex = 4;
            _mapPicture.TabStop = false;
            // 
            // _waterHeight
            // 
            _waterHeight.LargeChange = 20;
            _waterHeight.Location = new Point(274, 41);
            _waterHeight.Maximum = 100;
            _waterHeight.Name = "_waterHeight";
            _waterHeight.Orientation = Orientation.Vertical;
            _waterHeight.Size = new Size(45, 227);
            _waterHeight.SmallChange = 5;
            _waterHeight.TabIndex = 5;
            _waterHeight.TickFrequency = 10;
            _waterHeight.ValueChanged += WaterHeight_ValueChanged;
            // 
            // _waterHeightValue
            // 
            _waterHeightValue.Location = new Point(274, 274);
            _waterHeightValue.Name = "_waterHeightValue";
            _waterHeightValue.ReadOnly = true;
            _waterHeightValue.Size = new Size(45, 23);
            _waterHeightValue.TabIndex = 6;
            // 
            // _saveButton
            // 
            _saveButton.Location = new Point(12, 311);
            _saveButton.Name = "_saveButton";
            _saveButton.Size = new Size(307, 23);
            _saveButton.TabIndex = 7;
            _saveButton.Text = "Save";
            _saveButton.UseVisualStyleBackColor = true;
            _saveButton.Click += SaveButton_Click;
            // 
            // _changedTimer
            // 
            _changedTimer.Tick += ChangedTimer;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(331, 346);
            Controls.Add(_saveButton);
            Controls.Add(_waterHeightValue);
            Controls.Add(_waterHeight);
            Controls.Add(_mapPicture);
            Controls.Add(_randomiseButton);
            Controls.Add(_generateButton);
            Controls.Add(_seed);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "Form1";
            Text = "MapMaker";
            ((System.ComponentModel.ISupportInitialize)_mapPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)_waterHeight).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private MaskedTextBox _seed;
        private Button _generateButton;
        private Button _randomiseButton;
        private PictureBox _mapPicture;
        private TrackBar _waterHeight;
        private TextBox _waterHeightValue;
        private Button _saveButton;
        private System.Windows.Forms.Timer _changedTimer;
    }
}
