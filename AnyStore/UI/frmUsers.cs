using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AnyStore.UI
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }
 
        userBLL u = new userBLL();
        userDAL dal = new userDAL();
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Getting Data from UI ( & Set all the data in setter that is in Business Logic Layer
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            u.added_by = 1;

            // Inserting Data into Database
            bool success = dal.Insert(u);
            // If the data is successfully inserted then the value of the success will be true else it will be false

            if (success)
            {
                // Data Succesfully Inserted
                MessageBox.Show("User Succesfully Created");
                clear();
            }
            else
            {
                // Failed to insert to data
                MessageBox.Show("Failed to Add New User");
            }

            // Refreshing the Data Grid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;
        }

        private void clear()
        {
            txtUserId.Text = String.Empty;
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtUsername.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtContact.Text = String.Empty;
            txtAddress.Text = String.Empty;
            cmbGender.Text= "";
            cmbUserType.Text = "";
        }

        private void dgvUsers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get the index of the particular row
            int rowIndex = e.RowIndex;

            txtUserId.Text = dgvUsers.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUsers.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUsers.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUsers.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dgvUsers.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUsers.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUsers.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUsers.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text = dgvUsers.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dgvUsers.Rows[rowIndex].Cells[9].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the value from UI
            u.id = Convert.ToInt32(txtUserId.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            u.added_by = 1;

            // Updating Data into Database
            bool success = dal.Update(u);

            // If data is updated successfully then the value of success will be true else it will be false
            if (success)
            {
                MessageBox.Show("User Successfully Updated");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Update User");
            }

            // Refreshing the Data Grid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //  Get the value from UI
            u.id = Convert.ToInt32(txtUserId.Text);

            bool success = dal.Delete(u);

            if (success)
            {
                MessageBox.Show("User Deleted Successfully");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete User");
            }

            // Refreshing the Data Grid View
            DataTable dt = dal.Select();
            dgvUsers.DataSource = dt;

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Get Key word from Text Box
            string keywords = Convert.ToString(txtSearch.Text);

            // Check if the keywords has value or not
            if (keywords != null)
            {
                // Show user based on the keyword
                DataTable dt = dal.Search(keywords);
                dgvUsers.DataSource = dt;
            }
            else
            {
                // Show all users from Database
                DataTable dt = dal.Select();
                dgvUsers.DataSource = dt;

            }
        }
    }
}
