namespace SchoolOfFineArts
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDateOfBirth = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.btnAddUpdate = new System.Windows.Forms.Button();
            this.dtDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.numId = new System.Windows.Forms.NumericUpDown();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblAge = new System.Windows.Forms.Label();
            this.btnLoadTeachers = new System.Windows.Forms.Button();
            this.rdoTeacher = new System.Windows.Forms.RadioButton();
            this.rdoStudent = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLoadStudents = new System.Windows.Forms.Button();
            this.numAge = new System.Windows.Forms.NumericUpDown();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnResetForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAge)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "First Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Last Name:";
            // 
            // lblDateOfBirth
            // 
            this.lblDateOfBirth.AutoSize = true;
            this.lblDateOfBirth.Location = new System.Drawing.Point(45, 159);
            this.lblDateOfBirth.Name = "lblDateOfBirth";
            this.lblDateOfBirth.Size = new System.Drawing.Size(76, 15);
            this.lblDateOfBirth.TabIndex = 6;
            this.lblDateOfBirth.Text = "Date of Birth:";
            this.lblDateOfBirth.Visible = false;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(121, 92);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(100, 23);
            this.txtFirstName.TabIndex = 2;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(121, 124);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(100, 23);
            this.txtLastName.TabIndex = 3;
            // 
            // btnAddUpdate
            // 
            this.btnAddUpdate.Location = new System.Drawing.Point(118, 212);
            this.btnAddUpdate.Name = "btnAddUpdate";
            this.btnAddUpdate.Size = new System.Drawing.Size(100, 24);
            this.btnAddUpdate.TabIndex = 5;
            this.btnAddUpdate.Text = "Add/Update";
            this.btnAddUpdate.UseVisualStyleBackColor = true;
            this.btnAddUpdate.Click += new System.EventHandler(this.btnAddUpdate_Click);
            // 
            // dtDateOfBirth
            // 
            this.dtDateOfBirth.Location = new System.Drawing.Point(121, 156);
            this.dtDateOfBirth.Name = "dtDateOfBirth";
            this.dtDateOfBirth.Size = new System.Drawing.Size(200, 23);
            this.dtDateOfBirth.TabIndex = 18;
            this.dtDateOfBirth.Visible = false;
            // 
            // numId
            // 
            this.numId.Enabled = false;
            this.numId.Location = new System.Drawing.Point(118, 60);
            this.numId.Maximum = new decimal(new int[] {
            2147000000,
            0,
            0,
            0});
            this.numId.Name = "numId";
            this.numId.ReadOnly = true;
            this.numId.Size = new System.Drawing.Size(120, 23);
            this.numId.TabIndex = 1;
            // 
            // dgvResults
            // 
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(14, 364);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowTemplate.Height = 25;
            this.dgvResults.Size = new System.Drawing.Size(664, 150);
            this.dgvResults.TabIndex = 20;
            this.dgvResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellClick);
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(87, 159);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(28, 15);
            this.lblAge.TabIndex = 24;
            this.lblAge.Text = "Age";
            // 
            // btnLoadTeachers
            // 
            this.btnLoadTeachers.Location = new System.Drawing.Point(224, 213);
            this.btnLoadTeachers.Name = "btnLoadTeachers";
            this.btnLoadTeachers.Size = new System.Drawing.Size(100, 23);
            this.btnLoadTeachers.TabIndex = 25;
            this.btnLoadTeachers.Text = "Load Teachers";
            this.btnLoadTeachers.UseVisualStyleBackColor = true;
            this.btnLoadTeachers.Click += new System.EventHandler(this.btnLoadTeachers_Click);
            // 
            // rdoTeacher
            // 
            this.rdoTeacher.AutoSize = true;
            this.rdoTeacher.Checked = true;
            this.rdoTeacher.Location = new System.Drawing.Point(3, 3);
            this.rdoTeacher.Name = "rdoTeacher";
            this.rdoTeacher.Size = new System.Drawing.Size(65, 19);
            this.rdoTeacher.TabIndex = 26;
            this.rdoTeacher.TabStop = true;
            this.rdoTeacher.Text = "Teacher";
            this.rdoTeacher.UseVisualStyleBackColor = true;
            this.rdoTeacher.CheckedChanged += new System.EventHandler(this.rdoTeacher_CheckedChanged);
            // 
            // rdoStudent
            // 
            this.rdoStudent.AutoSize = true;
            this.rdoStudent.Location = new System.Drawing.Point(92, 3);
            this.rdoStudent.Name = "rdoStudent";
            this.rdoStudent.Size = new System.Drawing.Size(66, 19);
            this.rdoStudent.TabIndex = 27;
            this.rdoStudent.Text = "Student";
            this.rdoStudent.UseVisualStyleBackColor = true;
            this.rdoStudent.CheckedChanged += new System.EventHandler(this.rdoStudent_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoTeacher);
            this.panel1.Controls.Add(this.rdoStudent);
            this.panel1.Location = new System.Drawing.Point(118, 287);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 24);
            this.panel1.TabIndex = 28;
            // 
            // btnLoadStudents
            // 
            this.btnLoadStudents.Location = new System.Drawing.Point(224, 242);
            this.btnLoadStudents.Name = "btnLoadStudents";
            this.btnLoadStudents.Size = new System.Drawing.Size(100, 23);
            this.btnLoadStudents.TabIndex = 29;
            this.btnLoadStudents.Text = "Load Students";
            this.btnLoadStudents.UseVisualStyleBackColor = true;
            this.btnLoadStudents.Click += new System.EventHandler(this.btnLoadStudents_Click);
            // 
            // numAge
            // 
            this.numAge.Location = new System.Drawing.Point(121, 156);
            this.numAge.Name = "numAge";
            this.numAge.Size = new System.Drawing.Size(120, 23);
            this.numAge.TabIndex = 30;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(121, 242);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(97, 23);
            this.btnDelete.TabIndex = 31;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnResetForm
            // 
            this.btnResetForm.Location = new System.Drawing.Point(330, 213);
            this.btnResetForm.Name = "btnResetForm";
            this.btnResetForm.Size = new System.Drawing.Size(75, 23);
            this.btnResetForm.TabIndex = 32;
            this.btnResetForm.Text = "Reset";
            this.btnResetForm.UseVisualStyleBackColor = true;
            this.btnResetForm.Click += new System.EventHandler(this.btnResetForm_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 584);
            this.Controls.Add(this.btnResetForm);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.numAge);
            this.Controls.Add(this.btnLoadStudents);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLoadTeachers);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.numId);
            this.Controls.Add(this.dtDateOfBirth);
            this.Controls.Add(this.btnAddUpdate);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblDateOfBirth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblDateOfBirth;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private Button btnAddUpdate;
        private DateTimePicker dtDateOfBirth;
        private NumericUpDown numId;
        private DataGridView dgvResults;
        private Label lblAge;
        private Button btnLoadTeachers;
        private RadioButton rdoTeacher;
        private RadioButton rdoStudent;
        private Panel panel1;
        private Button btnLoadStudents;
        private NumericUpDown numAge;
        private Button btnDelete;
        private Button btnResetForm;
    }
}