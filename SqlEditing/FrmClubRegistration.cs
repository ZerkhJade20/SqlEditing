using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlEditing
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;

        int ID, Age, count = 0;
        string FirstName, MiddleName, LastName, Gender, Program;

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmUpdateMember updateForm = new FrmUpdateMember();
            updateForm.ShowDialog();
            RefreshListOfClubMembers();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtStudentID.Text))
                    throw new Exception("Student ID is required.");

                if (!long.TryParse(txtStudentID.Text, out StudentId))
                    throw new Exception("Student ID must be a valid number.");

                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                    throw new Exception("First name is required.");

                if (string.IsNullOrWhiteSpace(txtMiddleName.Text))
                    throw new Exception("Middle name is required.");

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                    throw new Exception("Last name is required.");

                if (string.IsNullOrWhiteSpace(txtAge.Text))
                    throw new Exception("Age is required.");

                if (!int.TryParse(txtAge.Text, out Age))
                    throw new Exception("Age must be a valid number.");

                if (Age < 1 || Age > 120)
                    throw new Exception("Age must be between 1 and 120.");

                if (cmbGender.SelectedIndex == -1)
                    throw new Exception("Please select a Gender.");

                Gender = cmbGender.Text;

                if (cmbProgram.SelectedIndex == -1)
                    throw new Exception("Please select a Program.");

                Program = cmbProgram.Text;                
                ID = RegistrationID();

                FirstName = txtFirstName.Text;
                MiddleName = txtMiddleName.Text;
                LastName = txtLastName.Text;
              
                clubRegistrationQuery.RegisterStudent(
                    ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program
                );

                MessageBox.Show("Successfully Registered!");

                RefreshListOfClubMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    


        long StudentId;
        public FrmClubRegistration()
        {
            InitializeComponent();
        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();
        }
        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;
        }
        private int RegistrationID()
        {
            count++;
            return count;
        }


    }
}
