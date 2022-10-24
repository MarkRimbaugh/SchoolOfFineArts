using SchoolOfFineArtsModels;
using System.ComponentModel;

namespace SchoolOfFineArts
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dgvResults.DataSource = listTeachers;
        }

        BindingList<Teacher> listTeachers = new BindingList<Teacher>();

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            Teacher teacher1 = new Teacher();
            teacher1.FirstName = txtTeacherFirstName.Text;
            teacher1.LastName = txtTeacherLastName.Text;
            //teacher1.MiddleName = txtTeacherMiddleName.Text;
            teacher1.Age = Convert.ToInt32(txtTeacherAge.Text);
            teacher1.Id = Convert.ToInt32(numTeacherId.Value);
            //MessageBox.Show(teacher1.ToString());

            bool validId = true;
            foreach (var teacher in listTeachers)
            {
                if (teacher.Id == teacher1.Id )
                {
                    MessageBox.Show("Teacher already exists");
                    validId = false;
                }
                if (teacher.LastName == teacher1.LastName && teacher.FirstName == teacher1.FirstName && teacher.Age == teacher1.Age)
                {
                    MessageBox.Show("Teacher already exists");
                    validId = false;
                }
            }
            if (validId)
            {
                listTeachers.Add(teacher1);
                dgvResults.Refresh();
            }
        }
    }
}