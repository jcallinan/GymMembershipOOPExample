using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymMembershipForms
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'gymMembershipDataSet.users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.gymMembershipDataSet.users);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.usersTableAdapter.Update(this.gymMembershipDataSet.users);
            MessageBox.Show("Users updated!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddNewUser();
        }
        public void AddNewUser()
        {
            string constr = System.Configuration.ConfigurationManager.ConnectionStrings["GymMembershipForms.Properties.Settings.GymMembershipConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            string sql = "insert into users  ([user_first_name],[user_last_name],[user_join_date],[user_birthday]) values (@user_first_name,@user_last_name,@user_join_date,@user_birthday)";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@user_first_name", txtFirstName.Text);
            param[1] = new SqlParameter("@user_last_name", txtLastName.Text);
            param[2] = new SqlParameter("@user_join_date", dtpJoinDate.Value);
            param[3] = new SqlParameter("@user_birthday", dtpBirthday.Value);
            cmd.Parameters.Add(param[0]);
            cmd.Parameters.Add(param[1]);
            cmd.Parameters.Add(param[2]);
            cmd.Parameters.Add(param[3]);
            con.Open();
            object res = cmd.ExecuteScalar();
            MessageBox.Show(txtFirstName.Text + " " + txtLastName.Text + " was added!");
            con.Close();
            this.usersTableAdapter.Fill(this.gymMembershipDataSet.users);
        }
    }
}
