using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectedDemo
{
    public partial class ProductCRUD : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public ProductCRUD()
        {
            InitializeComponent();
            con=new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConn"].ConnectionString);
        }

        private void ClearFormFields() { 
            txtPid.Clear();
            txtPname.Clear();
            txtPprice.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into product values(@pname, @pprice)";
                cmd=new SqlCommand(qry, con);

                // Assign values to parameters
                cmd.Parameters.AddWithValue("@pname", txtPname.Text);
                cmd.Parameters.AddWithValue("@pprice", Convert.ToInt32(txtPprice.Text));

                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Product added successfully !!!");
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
                string qry = "select pid, pname, pprice from product where pid=@pid";
                cmd=new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text));
                con.Open() ;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtPname.Text = dr["pname"].ToString();
                        txtPprice.Text = dr["pprice"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found !!!");
                }
            }
            catch(Exception ex)
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
                string qry = "update product set pname=@pname, pprice=@pprice where pid=@pid";
                cmd = new SqlCommand(qry, con);

                // Assign values to parameters
                cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text));
                cmd.Parameters.AddWithValue("@pname", txtPname.Text);
                cmd.Parameters.AddWithValue("@pprice", Convert.ToInt32(txtPprice.Text));

                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Product Updated successfully !!!");
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
                string qry = "delete from product where pid=@pid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPid.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Product deleted successfully !!!");
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

        private void btnShowProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from product";
                cmd=new SqlCommand(qry, con);
                con.Open();

                dr=cmd.ExecuteReader();
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
                con.Close() ;
            }
        }
    }
}
