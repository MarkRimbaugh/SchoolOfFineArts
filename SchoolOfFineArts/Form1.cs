using Microsoft.EntityFrameworkCore;
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
            Teacher teacher1 = new Teacher();
            teacher1.FirstName = txtFirstName.Text;
            teacher1.LastName = txtLastName.Text;
            //teacher1.MiddleName = txtTeacherMiddleName.Text;
            teacher1.Age = Convert.ToInt32(numAge.Text);
            teacher1.Id = Convert.ToInt32(numId.Value);
            //MessageBox.Show(teacher1.ToString());

            bool validId = true;
            foreach (var teacher in listTeachers)
            {
                if (teacher.Id == teacher1.Id )
                {
                    MessageBox.Show("Teacher ID already exists");
                    validId = false;
                }
                else if (teacher.LastName.Equals(teacher1.LastName, StringComparison.OrdinalIgnoreCase)
                    && teacher.FirstName.Equals(teacher1.FirstName, StringComparison.OrdinalIgnoreCase)
                    && teacher.Age == teacher1.Age)
                {
                    MessageBox.Show("This user already exists");
                    validId = false;
                }
            }
            if (validId)
            {
                listTeachers.Add(teacher1);
                dgvResults.Refresh();
            }
        }

        private void btnLoadTeachers_Click(object sender, EventArgs e)
        {
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