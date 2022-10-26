using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SchoolOfFineArtsDB;
using SchoolOfFineArtsModels;
using System.ComponentModel;
using System.Diagnostics;

namespace SchoolOfFineArts
{
    public partial class Form1 : Form
    {
        //use readonly as they are only set at form creation
        private readonly string _cnstr;
        private readonly DbContextOptionsBuilder _optionsBuilder;

        public Form1()
        {
            InitializeComponent();
            dgvResults.DataSource = listTeachers;
            _cnstr = Program._configuration["ConnectionStrings:SchoolOfFineArtsDB"];
            _optionsBuilder = new DbContextOptionsBuilder<SchoolOfFineArtsDBContext>().UseSqlServer(_cnstr);
        }

        BindingList<Teacher> listTeachers = new BindingList<Teacher>();

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            bool modified = false;
            if (rdoTeacher.Checked)
            {
                var newTeacher = new Teacher();
                newTeacher.Id = Convert.ToInt32(numId.Value);
                newTeacher.FirstName = txtFirstName.Text;
                newTeacher.LastName = txtLastName.Text;
                newTeacher.Age = Convert.ToInt32(numAge.Text);

                //Ensure teacher not in database
                using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
                {
                    if (newTeacher.Id > 0)
                    {
                        var existingTeacher = context.Teachers.SingleOrDefault(teacher => teacher.Id == newTeacher.Id);
                        if (existingTeacher is not null)
                        {
                            //update
                            existingTeacher.FirstName = newTeacher.FirstName;
                            existingTeacher.LastName = newTeacher.LastName;
                            existingTeacher.Age = newTeacher.Age;
                            context.SaveChanges();
                            modified = true;
                        }
                        else
                        {
                            MessageBox.Show("Teacher not found, could not update.");
                        }
                    }
                    else
                    {
                        var existingTeacher = context.Teachers.SingleOrDefault(teacher => teacher.FirstName.ToLower() == newTeacher.FirstName.ToLower()
                                                                 && teacher.LastName.ToLower() == newTeacher.LastName.ToLower()
                                                                 && teacher.Age == newTeacher.Age);
                        //if not add teacher
                        if (existingTeacher == null)
                        {
                            context.Teachers.Add(newTeacher);
                            context.SaveChanges();
                            modified = true;
                        }
                        else
                        {
                            MessageBox.Show("Teacher already exists, did you mean to update?");
                        }
                    }
                    if (modified)
                    {
                        //reload teachers
                        ResetForm();
                        var dbTeachers = new BindingList<Teacher>(context.Teachers.ToList());
                        dgvResults.DataSource = dbTeachers;
                        dgvResults.Refresh();
                    }
                }
            }
            else if (rdoStudent.Checked)
            {
                var newStudent = new Student();
                newStudent.Id = Convert.ToInt32(numId.Value);
                newStudent.FirstName = txtFirstName.Text;
                newStudent.LastName = txtLastName.Text;
                newStudent.DateOfBirth = dtDateOfBirth.Value;

                //Ensure Student not in database
                using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
                {
                    if (newStudent.Id > 0)
                    {
                        var existingStudent = context.Students.SingleOrDefault(Student => Student.Id == newStudent.Id);
                        if (existingStudent is not null)
                        {
                            //update
                            existingStudent.FirstName = newStudent.FirstName;
                            existingStudent.LastName = newStudent.LastName;
                            existingStudent.DateOfBirth = newStudent.DateOfBirth;
                            context.SaveChanges();
                            modified = true;
                        }
                        else
                        {
                            MessageBox.Show("Student not found, could not update.");
                        }
                    }
                    else
                    {
                        var existingStudent = context.Students.SingleOrDefault(Student => Student.FirstName.ToLower() == newStudent.FirstName.ToLower()
                                                                 && Student.LastName.ToLower() == newStudent.LastName.ToLower()
                                                                 && Student.DateOfBirth == newStudent.DateOfBirth);
                        //if not add Student
                        if (existingStudent == null)
                        {
                            context.Students.Add(newStudent);
                            context.SaveChanges();
                            modified = true;
                        }
                        else
                        {
                            MessageBox.Show("Student already exists, did you mean to update?");
                        }
                    }
                    if (modified)
                    {
                        //reload Students
                        ResetForm();
                        var dbStudents = new BindingList<Student>(context.Students.ToList());
                        dgvResults.DataSource = dbStudents;
                        dgvResults.Refresh();
                    }
                }
            }

            bool validId = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var Id = (int)numId.Value;
            var confirmDelete = MessageBox.Show("Are you sure you want to delete this item?"
                , "Are you sure?"
                , MessageBoxButtons.YesNo);
            if (confirmDelete == DialogResult.No)
            {
                return;
            }
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                if (rdoTeacher.Checked)
                {
                    var d = context.Teachers.SingleOrDefault(t => t.Id == Id);
                    if (d != null)
                    {
                        context.Teachers.Remove(d);
                        context.SaveChanges();
                        var databaseTeachers = new BindingList<Teacher>(context.Teachers.ToList());
                        dgvResults.DataSource = databaseTeachers;
                    }
                    else
                    {
                        MessageBox.Show("Teacher not found, couldn't delete.");
                    }
                }

                else if (rdoStudent.Checked)
                {
                    var d = context.Students.SingleOrDefault(s => s.Id == Id);
                    if (d != null)
                    {
                        context.Students.Remove(d);
                        context.SaveChanges();
                        var databaseStudents = new BindingList<Student>(context.Students.ToList());
                        dgvResults.DataSource = databaseStudents;
                    }
                    else
                    {
                        MessageBox.Show("Student not found, couldn't delete.");
                    }
                }
                dgvResults.Refresh();
                ResetForm();
            }
        }

        private void btnLoadTeachers_Click(object sender, EventArgs e)
        {
            //take advantage of disposability of the connection and context:
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                var dbTeachers = new BindingList<Teacher>(context.Teachers.ToList());
                dgvResults.DataSource = dbTeachers;
                dgvResults.Refresh();
            }
        }

        private void btnLoadStudents_Click(object sender, EventArgs e)
        {
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                var dbStudents = new BindingList<Student>(context.Students.ToList());
                dgvResults.DataSource = dbStudents;
                dgvResults.Refresh();
            }
        }

        private void rdoTeacher_CheckedChanged(object sender, EventArgs e)
        {
            ToggleControlVisibility();
        }

        private void rdoStudent_CheckedChanged(object sender, EventArgs e)
        {
            ToggleControlVisibility();
        }

        private void ToggleControlVisibility()
        {
            lblAge.Visible = rdoTeacher.Checked;
            lblDateOfBirth.Visible = rdoStudent.Checked;
            numAge.Visible = rdoTeacher.Checked;
            dtDateOfBirth.Visible = rdoStudent.Checked;
        }

        private void dgvResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var theRow = dgvResults.Rows[e.RowIndex];
            //var theCell = theRow.Cells[0];
            int dataId = 0;
            bool isTeacher = false;
            bool isStudent = false;

            foreach (DataGridViewTextBoxCell cell in theRow.Cells)
            {
                Debug.WriteLine(cell.ColumnIndex);
                Debug.WriteLine(cell.Value);
                Debug.WriteLine(cell.OwningColumn.Name);


                if (cell.OwningColumn.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    dataId = (int)cell.Value;
                    if (dataId == 0)
                    {
                        MessageBox.Show("Bad row clicked");
                        ResetForm();
                        return;
                    }
                }
                if (cell.OwningColumn.Name.Equals("Age", StringComparison.OrdinalIgnoreCase))
                {
                      isTeacher = true;
                        
                }
                if (cell.OwningColumn.Name.Equals("DateOfBirth", StringComparison.OrdinalIgnoreCase))
                {
                      isStudent = true;
                }
            }

            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                if (isTeacher)
                {
                    var t = context.Teachers.SingleOrDefault(t => t.Id == dataId);
                    if (t != null)
                    {
                        txtFirstName.Text = t.FirstName;
                        txtLastName.Text = t.LastName;
                        numAge.Value = t.Age;
                        numId.Value = t.Id;

                        rdoTeacher.Checked = true;
                        rdoStudent.Checked = false;
                        ToggleControlVisibility();
                    }
                }
                else if (isStudent)
                {
                    var s = context.Students.SingleOrDefault(s => s.Id == dataId);
                    if (s != null)
                    {
                        txtFirstName.Text = s.FirstName;
                        txtLastName.Text = s.LastName;
                        dtDateOfBirth.Value = s.DateOfBirth;
                        numId.Value = s.Id;

                        rdoTeacher.Checked = false;
                        rdoStudent.Checked = true;
                        ToggleControlVisibility();
                    }
                }
            }
        }

        private void btnResetForm_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            numId.Value = 0;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            numAge.Value = 0;
            dtDateOfBirth.Value = DateTime.Now;
            dgvResults.ClearSelection();
        }

        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}