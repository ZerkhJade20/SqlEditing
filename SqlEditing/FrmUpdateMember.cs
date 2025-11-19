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
    public partial class FrmUpdateMember : Form
    {
        ClubRegistrationQuery query = new ClubRegistrationQuery();
        long selectedID;

        public FrmUpdateMember()
        {
            InitializeComponent();
        }
        bool isLoaded = false;

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            query.DisplayList();

            cmbStudentID.DisplayMember = "StudentId";
            cmbStudentID.ValueMember = "StudentId";
            cmbStudentID.DataSource = query.dataTable;

            isLoaded = true;
        }

        private void cmbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoaded) return;
        
            if (cmbStudentID.SelectedValue == null) return;

            long id;
            if (!long.TryParse(cmbStudentID.SelectedValue.ToString(), out id))
                return; // avoid crash

            selectedID = id;

            foreach (DataRow row in query.dataTable.Rows)
            {
                if ((long)row["StudentId"] == selectedID)
                {
                    txtFirstName.Text = row["FirstName"].ToString();
                    txtMiddleName.Text = row["MiddleName"].ToString();
                    txtLastName.Text = row["LastName"].ToString();
                    txtAge.Text = row["Age"].ToString();
                    cmbGender.Text = row["Gender"].ToString();
                    cmbProgram.Text = row["Program"].ToString();
                    break;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            query.UpdateStudent(
            selectedID,
            txtFirstName.Text,
            txtMiddleName.Text,
            txtLastName.Text,
            int.Parse(txtAge.Text),
            cmbGender.Text,
            cmbProgram.Text);

            MessageBox.Show("Updated Successfully!");
            this.Close();
        }
    }
}
