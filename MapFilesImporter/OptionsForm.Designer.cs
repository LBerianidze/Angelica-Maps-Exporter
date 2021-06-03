namespace MapFilesImporter
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.ElementsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Browser = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonZ1 = new LBLIBRARY.Components.ButtonC();
            this.buttonZ2 = new LBLIBRARY.Components.ButtonC();
            this.buttonZ3 = new LBLIBRARY.Components.ButtonC();
            this.GamesCombobox = new LBLIBRARY.Components.ComboBoxA();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // ElementsTextBox
            // 
            this.ElementsTextBox.Location = new System.Drawing.Point(156, 1);
            this.ElementsTextBox.Name = "ElementsTextBox";
            this.ElementsTextBox.Size = new System.Drawing.Size(415, 20);
            this.ElementsTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(-2, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Путь до папки element:";
            // 
            // buttonZ1
            // 
            this.buttonZ1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonZ1.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ1.BorderColor = System.Drawing.Color.Gray;
            this.buttonZ1.BorderWidth = 1;
            this.buttonZ1.ButtonShape = LBLIBRARY.Components.ButtonC.ButtonsShapes.RoundRect;
            this.buttonZ1.EndColor = System.Drawing.Color.MidnightBlue;
            this.buttonZ1.FlatAppearance.BorderSize = 0;
            this.buttonZ1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ1.Font = new System.Drawing.Font("Segoe UI", 16.25F);
            this.buttonZ1.ForeColor = System.Drawing.Color.Yellow;
            this.buttonZ1.GradientAngle = 90;
            this.buttonZ1.Image = null;
            this.buttonZ1.Image_Location = new System.Drawing.Point(0, 0);
            this.buttonZ1.ImageToHeight = false;
            this.buttonZ1.Location = new System.Drawing.Point(1, 63);
            this.buttonZ1.MouseClickColor1 = System.Drawing.Color.Yellow;
            this.buttonZ1.MouseClickColor2 = System.Drawing.Color.Red;
            this.buttonZ1.MouseHoverColor1 = System.Drawing.Color.Turquoise;
            this.buttonZ1.MouseHoverColor2 = System.Drawing.Color.DarkSlateGray;
            this.buttonZ1.Name = "buttonZ1";
            this.buttonZ1.ShowButtontext = true;
            this.buttonZ1.Size = new System.Drawing.Size(313, 49);
            this.buttonZ1.SizeToContent = false;
            this.buttonZ1.StartColor = System.Drawing.Color.DodgerBlue;
            this.buttonZ1.TabIndex = 22;
            this.buttonZ1.Text = "Принять";
            this.buttonZ1.TextLocation_X = 110;
            this.buttonZ1.TextLocation_Y = 9;
            this.buttonZ1.Transparent1 = 250;
            this.buttonZ1.Transparent2 = 250;
            this.buttonZ1.UseVisualStyleBackColor = false;
            this.buttonZ1.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // buttonZ2
            // 
            this.buttonZ2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonZ2.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ2.BorderColor = System.Drawing.Color.Gray;
            this.buttonZ2.BorderWidth = 1;
            this.buttonZ2.ButtonShape = LBLIBRARY.Components.ButtonC.ButtonsShapes.RoundRect;
            this.buttonZ2.EndColor = System.Drawing.Color.MidnightBlue;
            this.buttonZ2.FlatAppearance.BorderSize = 0;
            this.buttonZ2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ2.Font = new System.Drawing.Font("Segoe UI", 16.25F);
            this.buttonZ2.ForeColor = System.Drawing.Color.Yellow;
            this.buttonZ2.GradientAngle = 90;
            this.buttonZ2.Image = null;
            this.buttonZ2.Image_Location = new System.Drawing.Point(0, 0);
            this.buttonZ2.ImageToHeight = false;
            this.buttonZ2.Location = new System.Drawing.Point(320, 63);
            this.buttonZ2.MouseClickColor1 = System.Drawing.Color.Yellow;
            this.buttonZ2.MouseClickColor2 = System.Drawing.Color.Red;
            this.buttonZ2.MouseHoverColor1 = System.Drawing.Color.Turquoise;
            this.buttonZ2.MouseHoverColor2 = System.Drawing.Color.DarkSlateGray;
            this.buttonZ2.Name = "buttonZ2";
            this.buttonZ2.ShowButtontext = true;
            this.buttonZ2.Size = new System.Drawing.Size(313, 49);
            this.buttonZ2.SizeToContent = false;
            this.buttonZ2.StartColor = System.Drawing.Color.DodgerBlue;
            this.buttonZ2.TabIndex = 23;
            this.buttonZ2.Text = "Отмена";
            this.buttonZ2.TextLocation_X = 111;
            this.buttonZ2.TextLocation_Y = 9;
            this.buttonZ2.Transparent1 = 250;
            this.buttonZ2.Transparent2 = 250;
            this.buttonZ2.UseVisualStyleBackColor = false;
            this.buttonZ2.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // buttonZ3
            // 
            this.buttonZ3.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ3.BorderColor = System.Drawing.Color.Black;
            this.buttonZ3.BorderWidth = 1;
            this.buttonZ3.ButtonShape = LBLIBRARY.Components.ButtonC.ButtonsShapes.Rect;
            this.buttonZ3.EndColor = System.Drawing.Color.MidnightBlue;
            this.buttonZ3.FlatAppearance.BorderSize = 0;
            this.buttonZ3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.buttonZ3.ForeColor = System.Drawing.Color.Yellow;
            this.buttonZ3.GradientAngle = 90;
            this.buttonZ3.Image = null;
            this.buttonZ3.Image_Location = new System.Drawing.Point(0, 0);
            this.buttonZ3.ImageToHeight = false;
            this.buttonZ3.Location = new System.Drawing.Point(572, 1);
            this.buttonZ3.MouseClickColor1 = System.Drawing.Color.Yellow;
            this.buttonZ3.MouseClickColor2 = System.Drawing.Color.Red;
            this.buttonZ3.MouseHoverColor1 = System.Drawing.Color.Turquoise;
            this.buttonZ3.MouseHoverColor2 = System.Drawing.Color.DarkSlateGray;
            this.buttonZ3.Name = "buttonZ3";
            this.buttonZ3.ShowButtontext = true;
            this.buttonZ3.Size = new System.Drawing.Size(66, 20);
            this.buttonZ3.SizeToContent = false;
            this.buttonZ3.StartColor = System.Drawing.Color.DodgerBlue;
            this.buttonZ3.TabIndex = 24;
            this.buttonZ3.Text = "Выбрать";
            this.buttonZ3.TextLocation_X = 5;
            this.buttonZ3.TextLocation_Y = 1;
            this.buttonZ3.Transparent1 = 250;
            this.buttonZ3.Transparent2 = 250;
            this.buttonZ3.UseVisualStyleBackColor = false;
            this.buttonZ3.Click += new System.EventHandler(this.MapsButton_Click);
            // 
            // GamesCombobox
            // 
            this.GamesCombobox.ArrowColor = System.Drawing.Color.Black;
            this.GamesCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GamesCombobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GamesCombobox.FormattingEnabled = true;
            this.GamesCombobox.Items.AddRange(new object[] {
            "Perfect World",
            "Forsaken World",
            "Jade Dynasty",
            "Legends of Martial Arts",
            "Heroes of Three Kingdoms(HoTK)",
            "Ether Saga Online(Eso)"});
            this.GamesCombobox.Location = new System.Drawing.Point(156, 44);
            this.GamesCombobox.Name = "GamesCombobox";
            this.GamesCombobox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.GamesCombobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.None;
            this.GamesCombobox.SetFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.GamesCombobox.Size = new System.Drawing.Size(200, 21);
            this.GamesCombobox.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(115, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "Игра:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton1.Location = new System.Drawing.Point(156, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(107, 21);
            this.radioButton1.TabIndex = 27;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "From pck files";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton2.Location = new System.Drawing.Point(287, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(172, 21);
            this.radioButton2.TabIndex = 28;
            this.radioButton2.Text = "From pck.files directories";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 113);
            this.Controls.Add(this.GamesCombobox);
            this.Controls.Add(this.buttonZ3);
            this.Controls.Add(this.buttonZ2);
            this.Controls.Add(this.buttonZ1);
            this.Controls.Add(this.ElementsTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Укажите путь до папки element:";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ElementsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog Browser;
        private LBLIBRARY.Components.ButtonC buttonZ1;
        private LBLIBRARY.Components.ButtonC buttonZ2;
        private LBLIBRARY.Components.ButtonC buttonZ3;
        private LBLIBRARY.Components.ComboBoxA GamesCombobox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}