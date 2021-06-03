namespace MapFilesImporter
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.OptionsButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.выйтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Information = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GfxFixToVersion = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.NewBmdTextureName = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ForLowVersion = new System.Windows.Forms.CheckBox();
            this.FixGfx = new System.Windows.Forms.CheckBox();
            this.FixEcm = new System.Windows.Forms.CheckBox();
            this.CopyingFile = new System.Windows.Forms.Label();
            this.ProgressText = new System.Windows.Forms.Label();
            this.CreateHMap = new System.Windows.Forms.CheckBox();
            this.CreateBht = new System.Windows.Forms.CheckBox();
            this.SearchDestinationDirectory = new LBLIBRARY.Components.ButtonC();
            this.buttonZ1 = new LBLIBRARY.Components.ButtonC();
            this.MapsCombobox = new LBLIBRARY.Components.ComboBoxA();
            this.CreateNewDirectory = new System.Windows.Forms.CheckBox();
            this.DestinationTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DestinationDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ImportingProgress = new System.Windows.Forms.ProgressBar();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GfxFixToVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.Information});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(593, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsButton,
            this.toolStripSeparator1,
            this.выйтиToolStripMenuItem});
            this.toolStripSplitButton1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSplitButton1.Image = global::MapFilesImporter.Properties.Resources.Folder;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(68, 22);
            this.toolStripSplitButton1.Text = "Файл";
            // 
            // OptionsButton
            // 
            this.OptionsButton.Image = global::MapFilesImporter.Properties.Resources.Options;
            this.OptionsButton.Name = "OptionsButton";
            this.OptionsButton.Size = new System.Drawing.Size(134, 22);
            this.OptionsButton.Text = "Настройки";
            this.OptionsButton.Click += new System.EventHandler(this.Options_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // выйтиToolStripMenuItem
            // 
            this.выйтиToolStripMenuItem.Image = global::MapFilesImporter.Properties.Resources.Exit;
            this.выйтиToolStripMenuItem.Name = "выйтиToolStripMenuItem";
            this.выйтиToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.выйтиToolStripMenuItem.Text = "Выйти";
            // 
            // Information
            // 
            this.Information.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Information.Image = global::MapFilesImporter.Properties.Resources.info;
            this.Information.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Information.Name = "Information";
            this.Information.Size = new System.Drawing.Size(23, 22);
            this.Information.Text = "Info";
            this.Information.Click += new System.EventHandler(this.Information_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.GfxFixToVersion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.NewBmdTextureName);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.ForLowVersion);
            this.groupBox1.Controls.Add(this.FixGfx);
            this.groupBox1.Controls.Add(this.FixEcm);
            this.groupBox1.Controls.Add(this.CopyingFile);
            this.groupBox1.Controls.Add(this.ProgressText);
            this.groupBox1.Controls.Add(this.CreateHMap);
            this.groupBox1.Controls.Add(this.CreateBht);
            this.groupBox1.Controls.Add(this.SearchDestinationDirectory);
            this.groupBox1.Controls.Add(this.buttonZ1);
            this.groupBox1.Controls.Add(this.MapsCombobox);
            this.groupBox1.Controls.Add(this.CreateNewDirectory);
            this.groupBox1.Controls.Add(this.DestinationTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(0, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 301);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Основное";
            // 
            // GfxFixToVersion
            // 
            this.GfxFixToVersion.Increment = new decimal(new int[] {
            34,
            0,
            0,
            0});
            this.GfxFixToVersion.Location = new System.Drawing.Point(223, 143);
            this.GfxFixToVersion.Maximum = new decimal(new int[] {
            103,
            0,
            0,
            0});
            this.GfxFixToVersion.Name = "GfxFixToVersion";
            this.GfxFixToVersion.Size = new System.Drawing.Size(40, 20);
            this.GfxFixToVersion.TabIndex = 54;
            this.GfxFixToVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.GfxFixToVersion.Value = new decimal(new int[] {
            58,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.label1.Location = new System.Drawing.Point(264, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 53;
            this.label1.Text = "для версий 1.4.2";
            // 
            // NewBmdTextureName
            // 
            this.NewBmdTextureName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NewBmdTextureName.Location = new System.Drawing.Point(245, 168);
            this.NewBmdTextureName.MaxLength = 8;
            this.NewBmdTextureName.Name = "NewBmdTextureName";
            this.NewBmdTextureName.Size = new System.Drawing.Size(71, 23);
            this.NewBmdTextureName.TabIndex = 51;
            this.NewBmdTextureName.Text = "textures";
            this.NewBmdTextureName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.checkBox1.Location = new System.Drawing.Point(4, 168);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(250, 21);
            this.checkBox1.TabIndex = 50;
            this.checkBox1.Text = "Переименовывать папку \"textures\" в ";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ForLowVersion
            // 
            this.ForLowVersion.AutoSize = true;
            this.ForLowVersion.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.ForLowVersion.Location = new System.Drawing.Point(167, 118);
            this.ForLowVersion.Name = "ForLowVersion";
            this.ForLowVersion.Size = new System.Drawing.Size(159, 21);
            this.ForLowVersion.TabIndex = 49;
            this.ForLowVersion.Text = "для версий ниже 1.4.2";
            this.ForLowVersion.UseVisualStyleBackColor = true;
            // 
            // FixGfx
            // 
            this.FixGfx.AutoSize = true;
            this.FixGfx.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.FixGfx.Location = new System.Drawing.Point(4, 143);
            this.FixGfx.Name = "FixGfx";
            this.FixGfx.Size = new System.Drawing.Size(224, 21);
            this.FixGfx.TabIndex = 48;
            this.FixGfx.Text = "Пофиксить .gfx файлы до версии";
            this.FixGfx.UseVisualStyleBackColor = true;
            // 
            // FixEcm
            // 
            this.FixEcm.AutoSize = true;
            this.FixEcm.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.FixEcm.Location = new System.Drawing.Point(4, 118);
            this.FixEcm.Name = "FixEcm";
            this.FixEcm.Size = new System.Drawing.Size(165, 21);
            this.FixEcm.TabIndex = 47;
            this.FixEcm.Text = "Пофиксить .ecm файлы";
            this.FixEcm.UseVisualStyleBackColor = true;
            // 
            // CopyingFile
            // 
            this.CopyingFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CopyingFile.AutoSize = true;
            this.CopyingFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CopyingFile.Location = new System.Drawing.Point(1, 279);
            this.CopyingFile.Name = "CopyingFile";
            this.CopyingFile.Size = new System.Drawing.Size(0, 17);
            this.CopyingFile.TabIndex = 46;
            // 
            // ProgressText
            // 
            this.ProgressText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ProgressText.AutoSize = true;
            this.ProgressText.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ProgressText.Location = new System.Drawing.Point(171, 257);
            this.ProgressText.Name = "ProgressText";
            this.ProgressText.Size = new System.Drawing.Size(230, 17);
            this.ProgressText.TabIndex = 45;
            this.ProgressText.Text = "Выберите карту и нажмите на кнопку";
            this.ProgressText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ProgressText.TextChanged += new System.EventHandler(this.ProgressText_TextChanged);
            // 
            // CreateHMap
            // 
            this.CreateHMap.AutoSize = true;
            this.CreateHMap.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.CreateHMap.Location = new System.Drawing.Point(4, 93);
            this.CreateHMap.Name = "CreateHMap";
            this.CreateHMap.Size = new System.Drawing.Size(545, 21);
            this.CreateHMap.TabIndex = 44;
            this.CreateHMap.Text = "Создать .hmap, .sev, .octr, .wmap, npcgen.data файл(ы) для сервера в конечной пап" +
    "ке \r\n";
            this.CreateHMap.UseVisualStyleBackColor = true;
            // 
            // CreateBht
            // 
            this.CreateBht.AutoSize = true;
            this.CreateBht.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.CreateBht.Location = new System.Drawing.Point(4, 68);
            this.CreateBht.Name = "CreateBht";
            this.CreateBht.Size = new System.Drawing.Size(328, 21);
            this.CreateBht.TabIndex = 43;
            this.CreateBht.Text = "Создать .bht файл для сервера в конечной папке \r\n";
            this.CreateBht.UseVisualStyleBackColor = true;
            // 
            // SearchDestinationDirectory
            // 
            this.SearchDestinationDirectory.BackColor = System.Drawing.Color.Transparent;
            this.SearchDestinationDirectory.BorderColor = System.Drawing.Color.Black;
            this.SearchDestinationDirectory.BorderWidth = 1;
            this.SearchDestinationDirectory.ButtonShape = LBLIBRARY.Components.ButtonC.ButtonsShapes.Rect;
            this.SearchDestinationDirectory.EndColor = System.Drawing.Color.MidnightBlue;
            this.SearchDestinationDirectory.FlatAppearance.BorderSize = 0;
            this.SearchDestinationDirectory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.SearchDestinationDirectory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.SearchDestinationDirectory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SearchDestinationDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.SearchDestinationDirectory.ForeColor = System.Drawing.Color.Yellow;
            this.SearchDestinationDirectory.GradientAngle = 90;
            this.SearchDestinationDirectory.Image = null;
            this.SearchDestinationDirectory.Image_Location = new System.Drawing.Point(0, 0);
            this.SearchDestinationDirectory.ImageToHeight = false;
            this.SearchDestinationDirectory.Location = new System.Drawing.Point(510, 14);
            this.SearchDestinationDirectory.MouseClickColor1 = System.Drawing.Color.Yellow;
            this.SearchDestinationDirectory.MouseClickColor2 = System.Drawing.Color.Red;
            this.SearchDestinationDirectory.MouseHoverColor1 = System.Drawing.Color.Turquoise;
            this.SearchDestinationDirectory.MouseHoverColor2 = System.Drawing.Color.DarkSlateGray;
            this.SearchDestinationDirectory.Name = "SearchDestinationDirectory";
            this.SearchDestinationDirectory.ShowButtontext = true;
            this.SearchDestinationDirectory.Size = new System.Drawing.Size(80, 20);
            this.SearchDestinationDirectory.SizeToContent = false;
            this.SearchDestinationDirectory.StartColor = System.Drawing.Color.DodgerBlue;
            this.SearchDestinationDirectory.TabIndex = 42;
            this.SearchDestinationDirectory.Text = "Указать";
            this.SearchDestinationDirectory.TextLocation_X = 12;
            this.SearchDestinationDirectory.TextLocation_Y = 1;
            this.SearchDestinationDirectory.Transparent1 = 250;
            this.SearchDestinationDirectory.Transparent2 = 250;
            this.SearchDestinationDirectory.UseVisualStyleBackColor = false;
            this.SearchDestinationDirectory.Click += new System.EventHandler(this.SearchDestinationDirectory_Click);
            // 
            // buttonZ1
            // 
            this.buttonZ1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonZ1.BackColor = System.Drawing.Color.Transparent;
            this.buttonZ1.BorderColor = System.Drawing.Color.Transparent;
            this.buttonZ1.BorderWidth = 1;
            this.buttonZ1.ButtonShape = LBLIBRARY.Components.ButtonC.ButtonsShapes.RoundRect;
            this.buttonZ1.EndColor = System.Drawing.Color.MidnightBlue;
            this.buttonZ1.FlatAppearance.BorderSize = 0;
            this.buttonZ1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonZ1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonZ1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZ1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.buttonZ1.ForeColor = System.Drawing.Color.Yellow;
            this.buttonZ1.GradientAngle = 90;
            this.buttonZ1.Image = null;
            this.buttonZ1.Image_Location = new System.Drawing.Point(0, 0);
            this.buttonZ1.ImageToHeight = false;
            this.buttonZ1.Location = new System.Drawing.Point(24, 216);
            this.buttonZ1.MouseClickColor1 = System.Drawing.Color.Yellow;
            this.buttonZ1.MouseClickColor2 = System.Drawing.Color.Red;
            this.buttonZ1.MouseHoverColor1 = System.Drawing.Color.Turquoise;
            this.buttonZ1.MouseHoverColor2 = System.Drawing.Color.DarkSlateGray;
            this.buttonZ1.Name = "buttonZ1";
            this.buttonZ1.ShowButtontext = true;
            this.buttonZ1.Size = new System.Drawing.Size(543, 41);
            this.buttonZ1.SizeToContent = false;
            this.buttonZ1.StartColor = System.Drawing.Color.DodgerBlue;
            this.buttonZ1.TabIndex = 41;
            this.buttonZ1.Text = "Экспортировать";
            this.buttonZ1.TextLocation_X = 175;
            this.buttonZ1.TextLocation_Y = 7;
            this.buttonZ1.Transparent1 = 250;
            this.buttonZ1.Transparent2 = 250;
            this.buttonZ1.UseVisualStyleBackColor = false;
            this.buttonZ1.Click += new System.EventHandler(this.StartImporting_Click);
            // 
            // MapsCombobox
            // 
            this.MapsCombobox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.MapsCombobox.ArrowColor = System.Drawing.Color.Blue;
            this.MapsCombobox.DropDownHeight = 160;
            this.MapsCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MapsCombobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MapsCombobox.FormattingEnabled = true;
            this.MapsCombobox.IntegralHeight = false;
            this.MapsCombobox.Location = new System.Drawing.Point(196, 195);
            this.MapsCombobox.Name = "MapsCombobox";
            this.MapsCombobox.SelectionColor = System.Drawing.Color.Green;
            this.MapsCombobox.SetColorBlack = LBLIBRARY.Components.ComboBoxA.MyEnum.None;
            this.MapsCombobox.SetFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.MapsCombobox.Size = new System.Drawing.Size(193, 21);
            this.MapsCombobox.TabIndex = 40;
            // 
            // CreateNewDirectory
            // 
            this.CreateNewDirectory.AutoSize = true;
            this.CreateNewDirectory.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            this.CreateNewDirectory.Location = new System.Drawing.Point(4, 44);
            this.CreateNewDirectory.Name = "CreateNewDirectory";
            this.CreateNewDirectory.Size = new System.Drawing.Size(339, 21);
            this.CreateNewDirectory.TabIndex = 39;
            this.CreateNewDirectory.Text = "Создать папку с названием карты в конечной папке\r\n";
            this.CreateNewDirectory.UseVisualStyleBackColor = true;
            // 
            // DestinationTextBox
            // 
            this.DestinationTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.DestinationTextBox.Location = new System.Drawing.Point(113, 14);
            this.DestinationTextBox.Name = "DestinationTextBox";
            this.DestinationTextBox.Size = new System.Drawing.Size(395, 20);
            this.DestinationTextBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(1, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Конечная папка:";
            // 
            // ImportingProgress
            // 
            this.ImportingProgress.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ImportingProgress.Location = new System.Drawing.Point(2, 330);
            this.ImportingProgress.Name = "ImportingProgress";
            this.ImportingProgress.Size = new System.Drawing.Size(589, 25);
            this.ImportingProgress.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(593, 356);
            this.Controls.Add(this.ImportingProgress);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maps Importer By Luka";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GfxFixToVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem OptionsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem выйтиToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton Information;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FolderBrowserDialog DestinationDialog;
        private System.Windows.Forms.TextBox DestinationTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CreateNewDirectory;
        private System.Windows.Forms.ProgressBar ImportingProgress;
        private LBLIBRARY.Components.ButtonC buttonZ1;
        private LBLIBRARY.Components.ComboBoxA MapsCombobox;
        private LBLIBRARY.Components.ButtonC SearchDestinationDirectory;
        private System.Windows.Forms.CheckBox CreateBht;
        private System.Windows.Forms.CheckBox CreateHMap;
        private System.Windows.Forms.Label ProgressText;
        private System.Windows.Forms.Label CopyingFile;
        private System.Windows.Forms.CheckBox FixGfx;
        private System.Windows.Forms.CheckBox FixEcm;
        private System.Windows.Forms.CheckBox ForLowVersion;
        private System.Windows.Forms.TextBox NewBmdTextureName;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown GfxFixToVersion;
        private System.Windows.Forms.Label label1;
    }
}

