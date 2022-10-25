using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SchoolOfFineArtsDB;
using SchoolOfFineArtsModels;
using System.ComponentModel;

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

        private void btnAddTeacher_Click(object sender, EventArgs e)
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

        private void btnLoadStudents_Click(object sender, EventArgs e)
        {
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                var dbStudents = new BindingList<Student>(context.Students.ToList());
                dgvResults.DataSource = dbStudents;
                dgvResults.Refresh();
            }
        }
    }
}