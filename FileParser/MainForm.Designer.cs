using System;

namespace FileParser
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.rBtnRead = new System.Windows.Forms.RadioButton();
            this.rBtnSearch = new System.Windows.Forms.RadioButton();
            this.rBtnDwld = new System.Windows.Forms.RadioButton();
            this.rBtnRename = new System.Windows.Forms.RadioButton();
            this.rBtnXML = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.главнаяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.rBtnXMLuniverse = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnShowBasePath = new System.Windows.Forms.Button();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(276, 192);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(187, 22);
            this.button2.TabIndex = 5;
            this.button2.Text = "Запустить программу";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(276, 52);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(366, 125);
            this.richTextBox2.TabIndex = 4;
            this.richTextBox2.Text = "";
            // 
            // rBtnRead
            // 
            this.rBtnRead.AutoSize = true;
            this.rBtnRead.Checked = true;
            this.rBtnRead.Location = new System.Drawing.Point(21, 40);
            this.rBtnRead.Name = "rBtnRead";
            this.rBtnRead.Size = new System.Drawing.Size(157, 17);
            this.rBtnRead.TabIndex = 0;
            this.rBtnRead.TabStop = true;
            this.rBtnRead.Text = "Прочитать файлы в папке";
            this.rBtnRead.UseVisualStyleBackColor = true;
            this.rBtnRead.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.rBtnRead.Click += new System.EventHandler(this.label2_Click);
            // 
            // rBtnSearch
            // 
            this.rBtnSearch.AutoSize = true;
            this.rBtnSearch.Location = new System.Drawing.Point(21, 65);
            this.rBtnSearch.Name = "rBtnSearch";
            this.rBtnSearch.Size = new System.Drawing.Size(197, 17);
            this.rBtnSearch.TabIndex = 1;
            this.rBtnSearch.TabStop = true;
            this.rBtnSearch.Text = "Выполнить поиск файлов в папке";
            this.rBtnSearch.UseVisualStyleBackColor = true;
            this.rBtnSearch.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            this.rBtnSearch.Click += new System.EventHandler(this.label2_Click);
            // 
            // rBtnDwld
            // 
            this.rBtnDwld.AutoSize = true;
            this.rBtnDwld.Location = new System.Drawing.Point(21, 90);
            this.rBtnDwld.Name = "rBtnDwld";
            this.rBtnDwld.Size = new System.Drawing.Size(233, 17);
            this.rBtnDwld.TabIndex = 3;
            this.rBtnDwld.TabStop = true;
            this.rBtnDwld.Text = "Скачать файлы по ссылкам из Интернет";
            this.rBtnDwld.UseVisualStyleBackColor = true;
            this.rBtnDwld.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            this.rBtnDwld.Click += new System.EventHandler(this.label2_Click);
            // 
            // rBtnRename
            // 
            this.rBtnRename.AutoSize = true;
            this.rBtnRename.Location = new System.Drawing.Point(21, 114);
            this.rBtnRename.Name = "rBtnRename";
            this.rBtnRename.Size = new System.Drawing.Size(143, 17);
            this.rBtnRename.TabIndex = 9;
            this.rBtnRename.TabStop = true;
            this.rBtnRename.Text = "Переименовать файлы";
            this.rBtnRename.UseVisualStyleBackColor = true;
            this.rBtnRename.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            this.rBtnRename.Click += new System.EventHandler(this.label2_Click);
            // 
            // rBtnXML
            // 
            this.rBtnXML.AutoSize = true;
            this.rBtnXML.Location = new System.Drawing.Point(21, 160);
            this.rBtnXML.Name = "rBtnXML";
            this.rBtnXML.Size = new System.Drawing.Size(160, 17);
            this.rBtnXML.TabIndex = 10;
            this.rBtnXML.TabStop = true;
            this.rBtnXML.Text = "Считать Яндекс YML в csv";
            this.rBtnXML.UseVisualStyleBackColor = true;
            this.rBtnXML.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            this.rBtnXML.Click += new System.EventHandler(this.label2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкиToolStripMenuItem,
            this.оПрограммеToolStripMenuItem,
            this.главнаяToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(654, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
            // 
            // главнаяToolStripMenuItem
            // 
            this.главнаяToolStripMenuItem.Name = "главнаяToolStripMenuItem";
            this.главнаяToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.главнаяToolStripMenuItem.Text = "Выход";
            this.главнаяToolStripMenuItem.Click += new System.EventHandler(this.главнаяToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(438, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // rBtnXMLuniverse
            // 
            this.rBtnXMLuniverse.AutoSize = true;
            this.rBtnXMLuniverse.Location = new System.Drawing.Point(21, 137);
            this.rBtnXMLuniverse.Name = "rBtnXMLuniverse";
            this.rBtnXMLuniverse.Size = new System.Drawing.Size(171, 17);
            this.rBtnXMLuniverse.TabIndex = 13;
            this.rBtnXMLuniverse.TabStop = true;
            this.rBtnXMLuniverse.Text = "Универсальный парсер XLM";
            this.rBtnXMLuniverse.UseVisualStyleBackColor = true;
            this.rBtnXMLuniverse.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(273, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Функционал";
            // 
            // btnShowBasePath
            // 
            this.btnShowBasePath.Location = new System.Drawing.Point(469, 192);
            this.btnShowBasePath.Name = "btnShowBasePath";
            this.btnShowBasePath.Size = new System.Drawing.Size(173, 23);
            this.btnShowBasePath.TabIndex = 16;
            this.btnShowBasePath.Text = "Открыть выходную папку";
            this.btnShowBasePath.UseVisualStyleBackColor = true;
            this.btnShowBasePath.Click += new System.EventHandler(this.btnShowBasePath_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(654, 233);
            this.Controls.Add(this.btnShowBasePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rBtnXMLuniverse);
            this.Controls.Add(this.rBtnXML);
            this.Controls.Add(this.rBtnRename);
            this.Controls.Add(this.rBtnDwld);
            this.Controls.Add(this.rBtnSearch);
            this.Controls.Add(this.rBtnRead);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label2);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RadioButton rBtnRead;
        private System.Windows.Forms.RadioButton rBtnSearch;
        private System.Windows.Forms.RadioButton rBtnDwld;
        private System.Windows.Forms.RadioButton rBtnRename;
        private System.Windows.Forms.RadioButton rBtnXML;
        private System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem главнаяToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rBtnXMLuniverse;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnShowBasePath;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
    }
}

