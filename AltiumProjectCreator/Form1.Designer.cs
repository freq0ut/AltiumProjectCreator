namespace AltiumProjectCreator
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label_templateTarget = new System.Windows.Forms.Label();
            this.textBox_templateProjectFilePath = new System.Windows.Forms.TextBox();
            this.label_newProjectTarget = new System.Windows.Forms.Label();
            this.textBox_newProjectTargetFilePath = new System.Windows.Forms.TextBox();
            this.label_newProjectName = new System.Windows.Forms.Label();
            this.textBox_newProjectName = new System.Windows.Forms.TextBox();
            this.button_createProject = new System.Windows.Forms.Button();
            this.progressBar_fileCopy = new System.Windows.Forms.ProgressBar();
            this.label_debugMessages = new System.Windows.Forms.Label();
            this.button_searchTemplateFilePath = new System.Windows.Forms.Button();
            this.button_searchNewProjectFilePath = new System.Windows.Forms.Button();
            this.folderBrowserDialog_searchTemplate = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog_searchNewProject = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox_debug = new System.Windows.Forms.TextBox();
            this.textBox_projectDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_projectDesigner = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_templateTarget
            // 
            this.label_templateTarget.AutoSize = true;
            this.label_templateTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_templateTarget.Location = new System.Drawing.Point(12, 74);
            this.label_templateTarget.Name = "label_templateTarget";
            this.label_templateTarget.Size = new System.Drawing.Size(104, 13);
            this.label_templateTarget.TabIndex = 97;
            this.label_templateTarget.Text = "Template Target:";
            // 
            // textBox_templateProjectFilePath
            // 
            this.textBox_templateProjectFilePath.Location = new System.Drawing.Point(14, 90);
            this.textBox_templateProjectFilePath.Name = "textBox_templateProjectFilePath";
            this.textBox_templateProjectFilePath.Size = new System.Drawing.Size(468, 20);
            this.textBox_templateProjectFilePath.TabIndex = 2;
            // 
            // label_newProjectTarget
            // 
            this.label_newProjectTarget.AutoSize = true;
            this.label_newProjectTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_newProjectTarget.Location = new System.Drawing.Point(12, 138);
            this.label_newProjectTarget.Name = "label_newProjectTarget";
            this.label_newProjectTarget.Size = new System.Drawing.Size(121, 13);
            this.label_newProjectTarget.TabIndex = 98;
            this.label_newProjectTarget.Text = "New Project Target:";
            // 
            // textBox_newProjectTargetFilePath
            // 
            this.textBox_newProjectTargetFilePath.Location = new System.Drawing.Point(15, 154);
            this.textBox_newProjectTargetFilePath.Name = "textBox_newProjectTargetFilePath";
            this.textBox_newProjectTargetFilePath.Size = new System.Drawing.Size(467, 20);
            this.textBox_newProjectTargetFilePath.TabIndex = 4;
            // 
            // label_newProjectName
            // 
            this.label_newProjectName.AutoSize = true;
            this.label_newProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_newProjectName.Location = new System.Drawing.Point(12, 18);
            this.label_newProjectName.Name = "label_newProjectName";
            this.label_newProjectName.Size = new System.Drawing.Size(116, 13);
            this.label_newProjectName.TabIndex = 96;
            this.label_newProjectName.Text = "New Project Name:";
            // 
            // textBox_newProjectName
            // 
            this.textBox_newProjectName.Location = new System.Drawing.Point(15, 34);
            this.textBox_newProjectName.Name = "textBox_newProjectName";
            this.textBox_newProjectName.Size = new System.Drawing.Size(467, 20);
            this.textBox_newProjectName.TabIndex = 1;
            // 
            // button_createProject
            // 
            this.button_createProject.Location = new System.Drawing.Point(15, 315);
            this.button_createProject.Name = "button_createProject";
            this.button_createProject.Size = new System.Drawing.Size(146, 47);
            this.button_createProject.TabIndex = 8;
            this.button_createProject.Text = "Create New Project";
            this.button_createProject.UseVisualStyleBackColor = true;
            this.button_createProject.Click += new System.EventHandler(this.button_createProject_Click);
            // 
            // progressBar_fileCopy
            // 
            this.progressBar_fileCopy.Location = new System.Drawing.Point(15, 383);
            this.progressBar_fileCopy.Name = "progressBar_fileCopy";
            this.progressBar_fileCopy.Size = new System.Drawing.Size(553, 23);
            this.progressBar_fileCopy.TabIndex = 9;
            // 
            // label_debugMessages
            // 
            this.label_debugMessages.AutoSize = true;
            this.label_debugMessages.Location = new System.Drawing.Point(12, 409);
            this.label_debugMessages.Name = "label_debugMessages";
            this.label_debugMessages.Size = new System.Drawing.Size(98, 13);
            this.label_debugMessages.TabIndex = 16;
            this.label_debugMessages.Text = "Debug messages...";
            // 
            // button_searchTemplateFilePath
            // 
            this.button_searchTemplateFilePath.Location = new System.Drawing.Point(497, 87);
            this.button_searchTemplateFilePath.Name = "button_searchTemplateFilePath";
            this.button_searchTemplateFilePath.Size = new System.Drawing.Size(71, 25);
            this.button_searchTemplateFilePath.TabIndex = 3;
            this.button_searchTemplateFilePath.Text = "Search";
            this.button_searchTemplateFilePath.UseVisualStyleBackColor = true;
            this.button_searchTemplateFilePath.Click += new System.EventHandler(this.button_searchTemplateFilePath_Click);
            // 
            // button_searchNewProjectFilePath
            // 
            this.button_searchNewProjectFilePath.Location = new System.Drawing.Point(497, 151);
            this.button_searchNewProjectFilePath.Name = "button_searchNewProjectFilePath";
            this.button_searchNewProjectFilePath.Size = new System.Drawing.Size(71, 25);
            this.button_searchNewProjectFilePath.TabIndex = 5;
            this.button_searchNewProjectFilePath.Text = "Search";
            this.button_searchNewProjectFilePath.UseVisualStyleBackColor = true;
            this.button_searchNewProjectFilePath.Click += new System.EventHandler(this.button_searchNewProjectFilePath_Click);
            // 
            // textBox_debug
            // 
            this.textBox_debug.Location = new System.Drawing.Point(15, 437);
            this.textBox_debug.Multiline = true;
            this.textBox_debug.Name = "textBox_debug";
            this.textBox_debug.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_debug.Size = new System.Drawing.Size(553, 320);
            this.textBox_debug.TabIndex = 10;
            this.textBox_debug.WordWrap = false;
            // 
            // textBox_projectDescription
            // 
            this.textBox_projectDescription.Location = new System.Drawing.Point(15, 218);
            this.textBox_projectDescription.Name = "textBox_projectDescription";
            this.textBox_projectDescription.Size = new System.Drawing.Size(467, 20);
            this.textBox_projectDescription.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 102;
            this.label1.Text = "Project Description:";
            // 
            // textBox_projectDesigner
            // 
            this.textBox_projectDesigner.Location = new System.Drawing.Point(15, 280);
            this.textBox_projectDesigner.Name = "textBox_projectDesigner";
            this.textBox_projectDesigner.Size = new System.Drawing.Size(467, 20);
            this.textBox_projectDesigner.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 104;
            this.label2.Text = "Designer Name:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 785);
            this.Controls.Add(this.textBox_projectDesigner);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_projectDescription);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_debug);
            this.Controls.Add(this.button_searchNewProjectFilePath);
            this.Controls.Add(this.button_searchTemplateFilePath);
            this.Controls.Add(this.label_debugMessages);
            this.Controls.Add(this.progressBar_fileCopy);
            this.Controls.Add(this.button_createProject);
            this.Controls.Add(this.textBox_newProjectName);
            this.Controls.Add(this.label_newProjectName);
            this.Controls.Add(this.textBox_newProjectTargetFilePath);
            this.Controls.Add(this.label_newProjectTarget);
            this.Controls.Add(this.textBox_templateProjectFilePath);
            this.Controls.Add(this.label_templateTarget);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tesla Altium Project Creator";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_templateTarget;
        private System.Windows.Forms.TextBox textBox_templateProjectFilePath;
        private System.Windows.Forms.Label label_newProjectTarget;
        private System.Windows.Forms.TextBox textBox_newProjectTargetFilePath;
        private System.Windows.Forms.Label label_newProjectName;
        private System.Windows.Forms.TextBox textBox_newProjectName;
        private System.Windows.Forms.Button button_createProject;
        private System.Windows.Forms.ProgressBar progressBar_fileCopy;
        private System.Windows.Forms.Label label_debugMessages;
        private System.Windows.Forms.Button button_searchTemplateFilePath;
        private System.Windows.Forms.Button button_searchNewProjectFilePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_searchTemplate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_searchNewProject;
        private System.Windows.Forms.TextBox textBox_debug;
        private System.Windows.Forms.TextBox textBox_projectDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_projectDesigner;
        private System.Windows.Forms.Label label2;
    }
}

