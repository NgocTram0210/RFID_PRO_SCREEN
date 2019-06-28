using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HistoryProductionRFID
{
    public partial class DBSetting : Form
    {
        public DBSetting()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form1 fm = new Form1();
            this.Hide();
            fm.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (txtDBName.Text != "" && txtIP.Text != "" && txtUserID.Text != "" && txtPass.Text != "")
            {
                DBConnection.Connection = @"Data Source=" + txtIP.Text + ";Initial Catalog=" + txtDBName.Text + ";User ID=" + txtUserID.Text + ";Password=" + txtPass.Text + "";
                WriteDBConnection(DBConnection.Connection);
                Form1 fm = new Form1();
                this.Hide();
                fm.Show();
            }
            else
                MessageBox.Show("DATA IS NULL!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void WriteDBConnection( string text)
        {
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter("DBConnection.txt");

                //Write a line of text
                sw.WriteLine(text);

                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
            }
        }
        
    }
}
