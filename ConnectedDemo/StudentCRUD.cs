using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;


namespace ConnectedDemo
{
    public partial class StudentCRUD : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public StudentCRUD()
        {
            InitializeComponent();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConn"].ConnectionString);
        }

        private void ClearFormFields() { 
            txtSid.Clear();
            txtSname.Clear();
            comboSbranch.Items.Clear();
            txtSper.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from student";
                cmd = new SqlCommand(qry, con);
                con.Open();

                dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr);     // convert dr object into row & col format
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            try
            {
                string qry = "insert into student values(@sname, @sbranch, @sper)";
                cmd = new SqlCommand(qry, con);

                // Assign values to parameters
                cmd.Parameters.AddWithValue("@sname", txtSname.Text);
                cmd.Parameters.AddWithValue("@sbranch", comboSbranch.Text);
                cmd.Parameters.AddWithValue("@sper", Convert.ToInt32(txtSper.Text));

                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Student added successfully !!!");
                    ClearFormFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select sid, sname, sbranch, sper from student where sid=@sid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(txtSid.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtSname.Text = dr["sname"].ToString();
                        comboSbranch.Text = dr["sbranch"].ToString();
                        txtSper.Text = dr["sper"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found !!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update student set sname=@sname, sbranch=@sbranch, sper=@sper where sid=@sid";
                cmd = new SqlCommand(qry, con);

                // Assign values to parameters
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(txtSid.Text));
                cmd.Parameters.AddWithValue("@sname", txtSname.Text);
                cmd.Parameters.AddWithValue("@sbranch", comboSbranch.Text);
                cmd.Parameters.AddWithValue("@sper", Convert.ToInt32(txtSper.Text));

                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Student Updated successfully !!!");
                    ClearFormFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from student where sid=@sid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@sid", Convert.ToInt32(txtSid.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Student deleted successfully !!!");
                    ClearFormFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void txtSid_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
