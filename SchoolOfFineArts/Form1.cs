using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SchoolOfFineArtsDB;
using SchoolOfFineArtsModels;
using SchoolOfFineArtsModels.DTOs;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
                        LoadTeachers();
                        /*var dbTeachers = new BindingList<Teacher>(context.Teachers.ToList());
                        dgvResults.DataSource = dbTeachers;
                        dgvResults.Refresh();*/
                    }
                }
            }
            else if (rdoStudent.Checked)
            {
                var newStudent = new Student();
                newStudent.Id = Convert.ToInt32(numId.Value);
                newStudent.FirstName = txtFirstName.Text;
                newStudent.LastName = txtLastName.Text;
                newStudent.DateOfBirth = dtDateOfBirth.Value.Date;

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
                        /*var databaseTeachers = new BindingList<Teacher>(context.Teachers.ToList());
                        dgvResults.DataSource = databaseTeachers;*/
                        LoadTeachers();
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
            LoadTeachers();
        }

        private void LoadTeachers(bool isSearch = false)
        {
            //take advantage of disposability of the connection and context:

            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                var dbTeachers = new BindingList<Teacher>(context.Teachers.ToList());
                dgvResults.DataSource = dbTeachers;
                dgvResults.Refresh();
            }

            if (!isSearch)
            {
                cboTeacher.SelectedIndex = -1;
                cboTeacher.Items.Clear();
                var tList = dgvResults.DataSource as BindingList<Teacher>;
                cboTeacher.Items.AddRange(tList.ToArray<Teacher>());
                cboTeacher.DisplayMember = "FriendlyName";
                cboTeacher.ValueMember = "Id";
            }
        }

        private void btnLoadStudents_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                var dbStudents = new BindingList<Student>(context.Students.ToList());
                dgvResults.DataSource = dbStudents;
                dgvResults.Refresh();

                lstStudents.Items.Clear();
                lstStudents.Items.AddRange(dbStudents.ToArray());

            }

        }

        private void rdoTeacher_CheckedChanged(object sender, EventArgs e)
        {
            ToggleControlVisibility();
            if (rdoTeacher.Checked)
            {
                LoadTeachers();
                ResetForm();
            }
        }

        private void rdoStudent_CheckedChanged(object sender, EventArgs e)
        {
            ToggleControlVisibility();
            if (rdoStudent.Checked)
            {
                LoadStudents();
                ResetForm();
            }
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
            try
            {
                if (e.RowIndex < 0)
                {
                    MessageBox.Show("Bad row clicked");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bad row clicked");
                return;
            }

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

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStudents();
            LoadCourses();
            LoadTeachers();
            ResetForm();
            ResetCourseForm();
            LoadCourseInfoDTOs();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (rdoTeacher.Checked)
            {
                LoadTeachers(true);
                var teacherList = dgvResults.DataSource as BindingList<Teacher>;
                var fList = teacherList.Where(x => x.LastName.ToLower().Contains(txtLastName.Text.ToLower()) &&
                                              x.FirstName.ToLower().Contains(txtFirstName.Text.ToLower())).ToList();
                dgvResults.DataSource = new BindingList<Teacher>(fList);
                dgvResults.ClearSelection();
            }
            else if (rdoStudent.Checked)
            {
                LoadStudents();
                var studentList = dgvResults.DataSource as BindingList<Student>;
                var sList = studentList.Where(x => x.LastName.ToLower().Contains(txtLastName.Text.ToLower()) &&
                                              x.FirstName.ToLower().Contains(txtFirstName.Text.ToLower())).ToList();
                dgvResults.DataSource = new BindingList<Student>(sList);
                dgvResults.ClearSelection();
            }
        }

        private void btnAddUpdateCourse_Click(object sender, EventArgs e)
        {
            bool modified = false;
            var teacherId = ((Teacher)cboTeacher.SelectedItem).Id;
            var newCourse = new Course();
            newCourse.Id = Convert.ToInt32(lblCourseId.Text);
            newCourse.Name = txtCourseName.Text;
            newCourse.Abbreviation = txtAbbreviation.Text;
            newCourse.Department = txtDepartment.Text;
            newCourse.NumCredits = Convert.ToInt32(cboNumCredits.SelectedItem);
            newCourse.TeacherId = teacherId;


            //Ensure course not in database
            if (newCourse.Id > 0)
            {
                using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
                {
                    var existingCourse = context.Courses.SingleOrDefault(course => course.Id == newCourse.Id);
                    if (existingCourse is not null)
                    {
                        //update
                        existingCourse.Name = newCourse.Name;
                        existingCourse.Abbreviation = newCourse.Abbreviation;
                        existingCourse.Department = newCourse.Department;
                        existingCourse.NumCredits = Convert.ToInt32(cboNumCredits.SelectedItem);
                        existingCourse.TeacherId = teacherId;
                        context.SaveChanges();
                        modified = true;
                    }
                    else
                    {
                        MessageBox.Show("Course not found, could not update.");
                    }
                }
            }
            else
            {
                using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
                {
                    var existingCourse = context.Courses.SingleOrDefault(course => course.Name.ToLower() == newCourse.Name.ToLower()
                                                         && course.Abbreviation.ToLower() == newCourse.Abbreviation.ToLower()
                                                         && course.TeacherId == newCourse.TeacherId);
                    //if not add course
                    if (existingCourse == null)
                    {
                        context.Courses.Add(newCourse);
                        context.SaveChanges();
                        modified = true;
                    }
                    else
                    {
                        MessageBox.Show("Course already exists, did you mean to update?");
                    }
                }
            }
            if (modified)
            {
                //reload courses
                LoadCourses();
                ResetCourseForm();
            }
        }


        private void LoadCourses()
        {
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                var dbCourses = new BindingList<Course>(context.Courses.Include(x => x.Teacher).ToList());
                /*var dbCourses = context.Courses.Include(x => x.Teacher).Select(y => new
                {
                    Id = y.Id,
                    Name = y.Name,
                    Abbreviation = y.Abbreviation,
                    TeacherId = y.TeacherId,
                    TeacherName = $"{y.Teacher.FirstName} {y.Teacher.LastName}"
                });*/
                //dgvCourses.DataSource = dbCourses.ToList();
                dgvCourses.DataSource = dbCourses;
                dgvCourses.Refresh();
                dgvCourseAssignments.DataSource = dbCourses.ToList();
                dgvCourseAssignments.Refresh();
            }
        }

        private void btnLoadCourses_Click(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void btnRemoveCourse_Click(object sender, EventArgs e)
        {
            var Id = Convert.ToInt32(lblCourseId.Text);
            var confirmDelete = MessageBox.Show("Are you sure you want to delete this item?"
                , "Are you sure?"
                , MessageBoxButtons.YesNo);
            if (confirmDelete == DialogResult.No)
            {
                return;
            }
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                var d = context.Courses.SingleOrDefault(t => t.Id == Id);
                if (d != null)
                {
                    context.Courses.Remove(d);
                    context.SaveChanges();
                    LoadCourses();
                }
                else
                {
                    MessageBox.Show("Teacher not found, couldn't delete.");
                }
            }
            dgvCourses.Refresh();
            ResetCourseForm();
        }

        private void ResetCourseForm()
        {
            lblCourseId.Text = "0";
            cboNumCredits.SelectedIndex = 2;
            cboTeacher.SelectedIndex = -1;
            txtAbbreviation.Text = string.Empty;
            txtCourseName.Text = string.Empty;
            txtCourseName.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            LoadCourses();
        }

        private void btnResetCourseForm_Click(object sender, EventArgs e)
        {
            ResetCourseForm();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selIndex = ((TabControl)sender).SelectedIndex;
            switch (selIndex)
            {
                case 0:
                    LoadTeachers();
                    break;
                case 1:
                    LoadCourses();
                    break;
                case 2:
                    LoadCourses();
                    break;
                default:
                    break;
            }
        }

        private void dgvCourses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    MessageBox.Show("Bad row clicked");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bad row clicked");
                return;
            }

            var theRow = dgvCourses.Rows[e.RowIndex];
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
                        ResetCourseForm();
                        return;
                    }
                }
            }
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                var d = context.Courses.SingleOrDefault(x => x.Id == dataId);

                if (d is not null)
                {
                    lblCourseId.Text = d.Id.ToString();
                    txtCourseName.Text = d.Name;
                    txtAbbreviation.Text = d.Abbreviation;
                    txtDepartment.Text = d.Department;
                    foreach (var item in cboNumCredits.Items)
                    {
                        if (Convert.ToInt32(item) == d.NumCredits)
                        {
                            cboNumCredits.SelectedItem = item;
                        }
                    }
                    foreach (var item in cboTeacher.Items)
                    {
                        var t = (Teacher)item;
                        if (t.Id == d.TeacherId)
                        {
                            cboTeacher.SelectedItem = item;
                        }
                    }
                }
            }
        }

        private void dgvCourseAssignments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    MessageBox.Show("Bad row clicked");
                    ResetCourseInfo();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bad row clicked");
                ResetCourseInfo();
                return;
            }
            var theRow = dgvCourseAssignments.Rows[e.RowIndex];
            var dataId = 0;

            foreach (DataGridViewTextBoxCell cell in theRow.Cells)
            {
                if (cell.OwningColumn.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    dataId = (int)cell.Value;
                    if (dataId == 0)
                    {
                        MessageBox.Show("Bad row clicked");
                        ResetCourseInfo();
                        return;
                    }
                    lblSelectedCourseId.Text = dataId.ToString();
                }
                if (cell.OwningColumn.Name.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    txtSelectedCourseName.Text = $"{cell.Value}";
                }
                if (cell.OwningColumn.Name.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    txtSelectedCourseName.Text = $"{cell.Value}";
                }
            }
        }
        private void ResetCourseAssignmentForm()
        {
            ResetCourseInfo();
            ClearStudentSelections();
            LoadCourseInfoDTOs();
        }

        private void ResetCourseInfo()
        {
            dgvCourseAssignments.ClearSelection();
            lblCourseId.Text = "0";
            lblSelectedCourseId.Text = "0";
            txtSelectedCourseName.Text = string.Empty;
            lblSelectedStudentId.Text = "0";
            txtSelectedStudentName.Text = string.Empty;
        }

        private void ClearStudentSelections()
        {
            foreach (int index in lstStudents.CheckedIndices)
            {
                lstStudents.SetItemCheckState(index, CheckState.Unchecked);
                lstStudents.ClearSelected();
            }
        }

        private void btnResetCourseAssignmentsForm_Click(object sender, EventArgs e)
        {
            ResetCourseAssignmentForm();
        }

        private void btnResetStudentList_Click(object sender, EventArgs e)
        {
            ClearStudentSelections();
        }

        private void btnAssociate_Click(object sender, EventArgs e)
        {
            // verify at least one student
            if (lstStudents.CheckedIndices.Count == 0)
            {
                MessageBox.Show("You must select at least one student.",
                                           "Select a student",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                return;
            }
            // verify a course is selected
            if (string.IsNullOrWhiteSpace(lblSelectedCourseId.Text) || (Convert.ToInt32(lblSelectedCourseId.Text) == 0))
            {
                MessageBox.Show("You must select a course.",
                                           "Select a course",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                return;
            }
            // check do I want to associate
            var students = lstStudents.CheckedItems.Cast<Student>().ToList();

            // create a string that stores all selected student names concatenated
            // i.e. Eric, Chris, Sally, ..., etc.

            StringBuilder sb = new();
            foreach (var s in students)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }

                sb.Append($"{s.FirstName} {s.LastName}");
            }
            var studentNames = sb.ToString();

            // display the student(s) and course(s) I want to associate
            var message = $"Are you sure you want to associate {studentNames} to {txtSelectedCourseName.Text}?";
            var confirmAssociate = MessageBox.Show(message, "Yes/No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmAssociate == DialogResult.No)
            {
                var resetForm = MessageBox.Show("Reset form?", "Yes/No", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resetForm == DialogResult.Yes)
                {
                    ResetCourseAssignmentForm();
                }
                return;
            }

            // if yes, add course enrollments to the student object
            int courseId = Convert.ToInt32(lblSelectedCourseId.Text);
            var courseTitle = txtSelectedCourseName.Text;

            var success = AssociateStudentsToCourse(students, courseTitle, courseId);
            if (success)
            {
                ResetCourseAssignmentForm();
            }
        }

        private bool AssociateStudentsToCourse(List<Student> students, string courseTitle, int courseId)
        {
            bool modified = false;
            // create database context
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                // check is good course
                var existingCourse = context.Courses.Include(x => x.CourseEnrollments).SingleOrDefault(t => t.Id == courseId);

                //  if not message the user - bad course
                if (existingCourse is null)
                {
                    MessageBox.Show("Course does not exist.");
                    // bad course - return false
                    return false;
                }

                // iterate the students
                foreach (var student in students)
                {
                    // check to see if student is valid
                    var existingStudent = context.Students.Include(x => x.CourseEnrollments).SingleOrDefault(s => s.Id == student.Id);

                    // if not, message the user - bad student
                    if (existingStudent is null)
                    {
                        MessageBox.Show($"{student.FriendlyName} does not exist.");
                        // move to next student
                        continue;
                    }

                    var courseExists = false;
                    // Check if association exists
                    foreach (var enrollment in existingStudent.CourseEnrollments)
                    {
                        if (enrollment.CourseId == existingCourse.Id)
                        {
                            MessageBox.Show($"{student.FriendlyName} is already associated with {courseTitle}");

                            courseExists = true;
                            break;
                        }
                    }
                    if (courseExists)
                    {
                        continue;
                    }

                    // create association
                    CourseEnrollment courseEnrollment = new();
                    courseEnrollment.CourseId = courseId;
                    courseEnrollment.StudentId = existingStudent.Id;

                    /*courseEnrollment.Student = existingStudent;
                    courseEnrollment.Course = existingCourse;*/

                    existingStudent.CourseEnrollments.Add(courseEnrollment);
                    //existingCourse.CourseEnrollments.Add(courseEnrollment);
                    modified = true;
                }
                if (modified)
                {
                    // save changes
                    MessageBox.Show("Successfully associated");
                    context.SaveChanges();
                }
            }
            return true;
        }

        private void dgvCourseInfos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    MessageBox.Show("Bad row clicked");
                    ResetCourseInfoGrid();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bad row clicked");
                return;
            }

            var theRow = dgvCourseInfos.Rows[e.RowIndex];
            int dataId = 0;

            foreach (DataGridViewTextBoxCell cell in theRow.Cells)
            {
                if (cell.OwningColumn.Name.Equals("CourseId", StringComparison.OrdinalIgnoreCase))
                {
                    dataId = (int)cell.Value;
                    if (dataId == 0)
                    {
                        MessageBox.Show("Bad row clicked");
                        ResetForm();
                        return;
                    }
                    lblSelectedCourseId.Text = $"{dataId}";


                }
                if (cell.OwningColumn.Name.Equals("CourseName", StringComparison.OrdinalIgnoreCase))
                {
                    txtSelectedCourseName.Text = $"{cell.Value}";
                }
                if (cell.OwningColumn.Name.Equals("StudentId", StringComparison.OrdinalIgnoreCase))
                {
                    dataId = (int)cell.Value;
                    if (dataId == 0)
                    {
                        MessageBox.Show("Bad row clicked");
                        ResetForm();
                        return;
                    }
                    lblSelectedStudentId.Text = $"{dataId}";


                }
                if (cell.OwningColumn.Name.Equals("StudentName", StringComparison.OrdinalIgnoreCase))
                {
                    txtSelectedStudentName.Text = $"{cell.Value}";
                }
            }
        }

        private void ResetCourseInfoGrid()
        {
            dgvCourseInfos.ClearSelection();
        }

        public void LoadCourseInfoDTOs()
        {
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                var dbCourseInfoDTOs = new BindingList<CourseInfoDTO>(context.CourseInfoDTOs
                    .FromSqlRaw("SELECT * FROM vwCourseInfo " +
                    "            ORDER BY StudentName, CourseName")
                    .ToList());
                dgvCourseInfos.DataSource = dbCourseInfoDTOs;
                dgvCourseInfos.Refresh();
            }
        }

        private void btnRemoveStudentsFromCourse_Click(object sender, EventArgs e)
        {
            if (lblSelectedStudentId.Text == "0")
            {
                MessageBox.Show("Must select at least one student");
                return;
            }
            if (string.IsNullOrWhiteSpace(lblSelectedStudentId.Text) || Convert.ToInt32(lblSelectedStudentId.Text) == 0)
            {
                MessageBox.Show("Must select at least one course");
                return;
            }

            // confirm delete
            var confirmDelete = MessageBox.Show("Are you sure you want to disenroll this student?"
                , "Are you sure?"
                , MessageBoxButtons.YesNo);
            if (confirmDelete == DialogResult.No)
            {
                // if not, reset the form
                ResetCourseAssignmentForm();
                return;
            }

            // if yes, disenroll student
            var studentId = Convert.ToInt32(lblSelectedStudentId.Text);
            var courseId = Convert.ToInt32(lblSelectedCourseId.Text);
            bool success = RemoveStudentFromCourseEnrollment(studentId, courseId);

            if (success)
            {
                LoadCourseInfoDTOs();
                ResetCourseAssignmentForm();
            }

            ResetCourseInfoGrid();
        }

        private bool RemoveStudentFromCourseEnrollment(int studentId, int courseId)
        {
            string studentName = txtSelectedStudentName.Text;
            string courseName = txtSelectedCourseName.Text;
            // create database context
            using (var context = new SchoolOfFineArtsDBContext(_optionsBuilder.Options))
            {
                // check is good course and student
                var existingCourse = context.Courses.Include(x => x.CourseEnrollments).SingleOrDefault(t => t.Id == courseId);
                if (existingCourse == null)
                {
                    MessageBox.Show($"{studentName} is not associated with {courseName}", "Does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                var existingStudent = context.Students.Include(x => x.CourseEnrollments).SingleOrDefault(s => s.Id == studentId);
                if (existingStudent == null)
                {
                    MessageBox.Show($"{studentName} is not associated with {courseName}.", "Does not exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                var enrollmentToDelete = new CourseEnrollment();
                foreach (var enrollment in existingStudent.CourseEnrollments)
                {
                    if(enrollment.CourseId == courseId)
                    {
                        enrollmentToDelete = enrollment;
                        break;
                    }
                }
                if (enrollmentToDelete.Id > 0 && enrollmentToDelete.StudentId == studentId && enrollmentToDelete.CourseId == courseId)
                {
                    existingStudent.CourseEnrollments.Remove(enrollmentToDelete);
                    context.SaveChanges();
                    MessageBox.Show($"Removed {studentName} from {courseName}");
                    return true;
                }
            }
            return false;
        }

        private void bnClearCourseInfoGridSelections_Click(object sender, EventArgs e)
        {
            dgvCourseInfos.ClearSelection();
            ResetCourseAssignmentForm();
        }
    }
}

            









