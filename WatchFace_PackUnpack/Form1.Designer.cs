﻿namespace WatchFace_PackUnpack
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton_color2 = new System.Windows.Forms.RadioButton();
            this.radioButton_color1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton_gts = new System.Windows.Forms.RadioButton();
            this.radioButton_gtr42 = new System.Windows.Forms.RadioButton();
            this.radioButton_gtr47 = new System.Windows.Forms.RadioButton();
            this.checkBox_Watchface_Path = new System.Windows.Forms.CheckBox();
            this.button_unpack = new System.Windows.Forms.Button();
            this.button_pack = new System.Windows.Forms.Button();
            this.label_version = new System.Windows.Forms.Label();
            this.radioButton_amazfitx = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton_color2);
            this.panel1.Controls.Add(this.radioButton_color1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 45);
            this.panel1.TabIndex = 0;
            // 
            // radioButton_color2
            // 
            this.radioButton_color2.AutoSize = true;
            this.radioButton_color2.Location = new System.Drawing.Point(4, 27);
            this.radioButton_color2.Name = "radioButton_color2";
            this.radioButton_color2.Size = new System.Drawing.Size(117, 17);
            this.radioButton_color2.TabIndex = 1;
            this.radioButton_color2.Text = "Цветовая схема 2";
            this.radioButton_color2.UseVisualStyleBackColor = true;
            this.radioButton_color2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_color1
            // 
            this.radioButton_color1.AutoSize = true;
            this.radioButton_color1.Checked = true;
            this.radioButton_color1.Location = new System.Drawing.Point(4, 4);
            this.radioButton_color1.Name = "radioButton_color1";
            this.radioButton_color1.Size = new System.Drawing.Size(117, 17);
            this.radioButton_color1.TabIndex = 0;
            this.radioButton_color1.TabStop = true;
            this.radioButton_color1.Text = "Цветовая схема 1";
            this.radioButton_color1.UseVisualStyleBackColor = true;
            this.radioButton_color1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton_amazfitx);
            this.panel2.Controls.Add(this.radioButton_gts);
            this.panel2.Controls.Add(this.radioButton_gtr42);
            this.panel2.Controls.Add(this.radioButton_gtr47);
            this.panel2.Location = new System.Drawing.Point(12, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 46);
            this.panel2.TabIndex = 1;
            // 
            // radioButton_gts
            // 
            this.radioButton_gts.AutoSize = true;
            this.radioButton_gts.Location = new System.Drawing.Point(142, 4);
            this.radioButton_gts.Name = "radioButton_gts";
            this.radioButton_gts.Size = new System.Drawing.Size(47, 17);
            this.radioButton_gts.TabIndex = 4;
            this.radioButton_gts.Text = "GTS";
            this.radioButton_gts.UseVisualStyleBackColor = true;
            this.radioButton_gts.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_gtr42
            // 
            this.radioButton_gtr42.AutoSize = true;
            this.radioButton_gtr42.Location = new System.Drawing.Point(73, 4);
            this.radioButton_gtr42.Name = "radioButton_gtr42";
            this.radioButton_gtr42.Size = new System.Drawing.Size(63, 17);
            this.radioButton_gtr42.TabIndex = 3;
            this.radioButton_gtr42.Text = "GTR 42";
            this.radioButton_gtr42.UseVisualStyleBackColor = true;
            this.radioButton_gtr42.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton_gtr47
            // 
            this.radioButton_gtr47.AutoSize = true;
            this.radioButton_gtr47.Checked = true;
            this.radioButton_gtr47.Location = new System.Drawing.Point(4, 4);
            this.radioButton_gtr47.Name = "radioButton_gtr47";
            this.radioButton_gtr47.Size = new System.Drawing.Size(63, 17);
            this.radioButton_gtr47.TabIndex = 2;
            this.radioButton_gtr47.TabStop = true;
            this.radioButton_gtr47.Text = "GTR 47";
            this.radioButton_gtr47.UseVisualStyleBackColor = true;
            this.radioButton_gtr47.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // checkBox_Watchface_Path
            // 
            this.checkBox_Watchface_Path.Checked = true;
            this.checkBox_Watchface_Path.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Watchface_Path.Location = new System.Drawing.Point(12, 115);
            this.checkBox_Watchface_Path.Name = "checkBox_Watchface_Path";
            this.checkBox_Watchface_Path.Size = new System.Drawing.Size(200, 34);
            this.checkBox_Watchface_Path.TabIndex = 2;
            this.checkBox_Watchface_Path.Text = "Помещать распакованный циферблат в папку \"WatchFace\"";
            this.checkBox_Watchface_Path.UseVisualStyleBackColor = true;
            this.checkBox_Watchface_Path.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // button_unpack
            // 
            this.button_unpack.Location = new System.Drawing.Point(12, 155);
            this.button_unpack.Name = "button_unpack";
            this.button_unpack.Size = new System.Drawing.Size(90, 23);
            this.button_unpack.TabIndex = 3;
            this.button_unpack.Text = "Распаковать";
            this.button_unpack.UseVisualStyleBackColor = true;
            this.button_unpack.Click += new System.EventHandler(this.button_unpack_Click);
            // 
            // button_pack
            // 
            this.button_pack.Location = new System.Drawing.Point(122, 155);
            this.button_pack.Name = "button_pack";
            this.button_pack.Size = new System.Drawing.Size(90, 23);
            this.button_pack.TabIndex = 4;
            this.button_pack.Text = "Запаковать";
            this.button_pack.UseVisualStyleBackColor = true;
            this.button_pack.Click += new System.EventHandler(this.button_pack_Click);
            // 
            // label_version
            // 
            this.label_version.AutoSize = true;
            this.label_version.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_version.Location = new System.Drawing.Point(198, 2);
            this.label_version.Name = "label_version";
            this.label_version.Size = new System.Drawing.Size(14, 7);
            this.label_version.TabIndex = 5;
            this.label_version.Text = "0.0";
            // 
            // radioButton_amazfitx
            // 
            this.radioButton_amazfitx.AutoSize = true;
            this.radioButton_amazfitx.Location = new System.Drawing.Point(4, 27);
            this.radioButton_amazfitx.Name = "radioButton_amazfitx";
            this.radioButton_amazfitx.Size = new System.Drawing.Size(69, 17);
            this.radioButton_amazfitx.TabIndex = 4;
            this.radioButton_amazfitx.Text = "Amazfit X";
            this.radioButton_amazfitx.UseVisualStyleBackColor = true;
            this.radioButton_amazfitx.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 182);
            this.Controls.Add(this.label_version);
            this.Controls.Add(this.button_pack);
            this.Controls.Add(this.button_unpack);
            this.Controls.Add(this.checkBox_Watchface_Path);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Распаковка циферблатов GTR";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton_color2;
        private System.Windows.Forms.RadioButton radioButton_color1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton_gtr42;
        private System.Windows.Forms.RadioButton radioButton_gtr47;
        private System.Windows.Forms.CheckBox checkBox_Watchface_Path;
        private System.Windows.Forms.Button button_unpack;
        private System.Windows.Forms.Button button_pack;
        private System.Windows.Forms.RadioButton radioButton_gts;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.RadioButton radioButton_amazfitx;
    }
}

