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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool newObject = true;
            if (rdoTeacher.Checked)
            {
                var newTeacher = new Teacher();
                newTeacher.Id = 0;
                newTeacher.FirstName = txtFirstName.Text;
                newTeacher.LastName = txtLastName.Text;
                newTeacher.Age = Convert.ToInt32(numAge.Text);

                //Ensure teacher not in database
                using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
                {
                    var exists = context.Teachers.SingleOrDefault(teacher => teacher.FirstName.ToLower() == newTeacher.FirstName.ToLower()
                                                                 && teacher.LastName.ToLower() == newTeacher.LastName.ToLower()
                                                                 && teacher.Age == newTeacher.Age);
                    //if exists post error "did you mean to update"
                    if (exists is not null)
                    {
                        newObject = false;
                        MessageBox.Show("Teacher already exists, did you mean to update?");
                    }
                    else
                    {
                        //if not add teacher
                        context.Teachers.Add(newTeacher);
                        context.SaveChanges();

                        //reload teachers
                        var dbTeachers = new BindingList<Teacher>(context.Teachers.ToList());
                        dgvResults.DataSource = dbTeachers;
                        dgvResults.Refresh();
                    }
                }
            }
            else if (rdoStudent.Checked)
            {

            }

            bool validId = true;
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
                }

                dgvResults.Refresh();
            }
        }
    }
}