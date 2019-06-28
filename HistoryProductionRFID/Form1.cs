using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HistoryProductionRFID
{
    public partial class Form1 : Form
    {
        SQLDataProvider sql = new SQLDataProvider();
        public string m_connect = File.OpenText("DBConnection.txt").ReadLine();
        SqlConnection con = null;
        public delegate void NewHome();
        public event NewHome OnNewHome;
        public Form1()
        {
            InitializeComponent();
            
            try
            {
                SqlClientPermission ss = new SqlClientPermission(System.Security.Permissions.PermissionState.Unrestricted);
                ss.Demand();
            }
            catch (Exception)
            {
                throw;
            }
            try
            {
                SqlDependency.Stop(m_connect);
                SqlDependency.Start(m_connect);
                con = new SqlConnection(m_connect);
            }
            catch { }
            
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want exit?", "Notification", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        //Create lable vertical
        newLabel lblready = new newLabel();
        newLabel lblFn = new newLabel();
        newLabel lblready2 = new newLabel();
        newLabel lblFn2 = new newLabel();
        newLabel lblOrder = new newLabel();
        newLabel lblFn3 = new newLabel();

        private void Form1_Load(object sender, EventArgs e)
        {
            RELOAD();
        }
        public void RELOAD()
        {
            lbTitle.Text = "RFID OPERATIONAL PROGRESS";
            timer1.Start();
            if (pnFill.HasChildren)
            {
                foreach (Control childControl in pnFill.Controls)
                {
                    if (childControl != lblOrder)
                        childControl.Font = new Font("Arial", 62, FontStyle.Bold);
                }
            }
            label26.Font = new Font("Arial", 32, label26.Font.Style);
            label27.Font = new Font("Arial", 32, label27.Font.Style);

            label28.Font = new Font("Arial", 54, FontStyle.Bold);
            label29.Font = new Font("Arial", 54, FontStyle.Bold);
            label30.Font = new Font("Arial", 54, FontStyle.Bold);
            label31.Font = new Font("Arial", 54, FontStyle.Bold);
            someResize();
            ReSize();

            OnNewHome += new NewHome(Form1_OnNewHome);
            LoadData();
        }
        public void Form1_OnNewHome()
        {
            ISynchronizeInvoke i = (ISynchronizeInvoke)this;
            if (i.InvokeRequired)
            {
                NewHome dd = new NewHome(Form1_OnNewHome);
                i.BeginInvoke(dd, null);
                return;
            }
            LoadData();
        }
        void LoadData()
        {
            try
            {
                DataTable dt = new DataTable();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //DB NIKE_RFID_HISTORY
                SqlCommand cmd = new SqlCommand("SELECT STEP_NO FROM dbo.PRO_HIS", con);
                cmd.Notification = null;
                SqlDependency de = new SqlDependency(cmd);
                de.OnChange += new OnChangeEventHandler(de_OnChange);
                dt.Load(cmd.ExecuteReader(CommandBehavior.CloseConnection));

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                Load1();
                ReSize();
            }
            catch(Exception ex) { }
        }

        public void de_OnChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency de = sender as SqlDependency;
            de.OnChange -= de_OnChange;
            if (OnNewHome != null)
            {
                OnNewHome();
            }

        }

        public void Load1()
        {
            try
            {
                //Thread.Sleep(1000);
                label2.Text = sql.QUANTITY__PROCESS_READY(2) == 0 ? "0": sql.QUANTITY__PROCESS_READY(2).ToString("#,###");
                label3.Text = sql.QUANTITY__PROCESS_FINISH(2) == 0 ? "0" : sql.QUANTITY__PROCESS_FINISH(2).ToString("#,###");

                label5.Text = sql.QUANTITY__PROCESS_READY(4) == 0 ? "0" : sql.QUANTITY__PROCESS_READY(4).ToString("#,###");
                label4.Text = sql.QUANTITY__PROCESS_FINISH(4) == 0 ? "0" : sql.QUANTITY__PROCESS_FINISH(4).ToString("#,###");

                label8.Text = sql.QUANTITY__PROCESS_READY(5) == 0 ? "0" : sql.QUANTITY__PROCESS_READY(5).ToString("#,###");
                label7.Text = sql.QUANTITY__PROCESS_FINISH(5) == 0 ? "0" : sql.QUANTITY__PROCESS_FINISH(5).ToString("#,###");

                label11.Text = sql.QUANTITY__PROCESS_READY(6) == 0 ? "0" : sql.QUANTITY__PROCESS_READY(6).ToString("#,###");
                label10.Text = sql.QUANTITY__PROCESS_FINISH(6) == 0 ? "0" : sql.QUANTITY__PROCESS_FINISH(6).ToString("#,###");

                label14.Text = sql.QUANTITY__PROCESS_READY(7) == 0 ? "0" : sql.QUANTITY__PROCESS_READY(7).ToString("#,###");
                label13.Text = sql.QUANTITY__PROCESS_FINISH(7) == 0 ? "0" : sql.QUANTITY__PROCESS_FINISH(7).ToString("#,###");

                label17.Text = sql.QUANTITY__PROCESS_READY(8) == 0 ? "0" : sql.QUANTITY__PROCESS_READY(8).ToString("#,###");
                label16.Text = sql.QUANTITY__PROCESS_FINISH(8) == 0 ? "0" : sql.QUANTITY__PROCESS_FINISH(8).ToString("#,###");

                label20.Text = sql.QUANTITY__PROCESS_READY(9) == 0 ? "0" : sql.QUANTITY__PROCESS_READY(9).ToString("#,###");
                label19.Text = sql.QUANTITY__PROCESS_FINISH(9) == 0 ? "0" : sql.QUANTITY__PROCESS_FINISH(9).ToString("#,###");

                label23.Text = sql.QUANTITY__PROCESS_READY(10) == 0 ? "0" : sql.QUANTITY__PROCESS_READY(10).ToString("#,###");
                label22.Text = sql.QUANTITY__PROCESS_FINISH(10) == 0 ? "0" : sql.QUANTITY__PROCESS_FINISH(10).ToString("#,###");

                label28.Text = sql.QUANTITY_ORDER_DAY() == 0 ? "0" : sql.QUANTITY_ORDER_DAY().ToString("#,###");
                label30.Text = sql.QUANTITY_FINISH_DAY() == 0 ? "0" : sql.QUANTITY_FINISH_DAY().ToString("#,###");

                label29.Text = sql.QUANTITY_ORDER_MONTH() == 0 ? "0" : sql.QUANTITY_ORDER_MONTH().ToString("#,###");
                label31.Text = sql.QUANTITY_FINISH_MONTH() == 0 ? "0" : sql.QUANTITY_FINISH_MONTH().ToString("#,###");
            }
            catch(Exception ex)
            {

            }
            
        }

        //Resize Font and Size
        private void someResize()
        {
            label1.Font = new Font("Arial", 32, label1.Font.Style);
            label6.Font = new Font("Arial", 32, label6.Font.Style);
            label9.Font = new Font("Arial", 32, label9.Font.Style);
            label12.Font = new Font("Arial", 32, label12.Font.Style);
            label15.Font = new Font("Arial", 32, label15.Font.Style);
            label18.Font = new Font("Arial", 32, label18.Font.Style);
            label21.Font = new Font("Arial", 32, label21.Font.Style);
            label24.Font = new Font("Arial", 32, label24.Font.Style);
            label25.Font = new Font("Arial", 32, label25.Font.Style);
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        //Resize lable
        private void ReSize()
        {
            //LB1
            panel1.Width = this.Width / 16;
            panel1.Height = pnFill.Height / 2;
            panel1.Location = new Point(0, 0);

            panel12.Width = panel1.Width;
            panel12.Height = panel1.Height / 5 * 2;
            panel12.Location = new Point(0, panel1.Height / 5);
            panel13.Width = panel1.Width;
            panel13.Height = panel1.Height / 5 * 2;
            panel13.Location = new Point(0, panel1.Height / 5*3);

            lblready.AutoSize = false;
            lblready.NewText = "READY";
            lblready.ForeColor = Color.Red;
            lblready.RotateAngle = -90;
            lblready.Parent = panel12;
            lblready.Height = lblready.Width * 2+15;
            lblready.Location = new Point(0, (panel12.Height - (lblready.Height / 2)) / 2);

            lblFn.AutoSize = false;
            lblFn.NewText = "FINISH";
            lblFn.ForeColor = Color.Blue;
            lblFn.RotateAngle = -90;
            lblFn.Parent = panel13;
            lblFn.Height = lblFn.Width * 2 + 15;
            lblFn.Location = new Point(0, (panel13.Height - (lblFn.Height / 2)) / 2);

            //Process 1
            panel2.Width = (this.Width / 16) * 3;
            panel2.Height = pnFill.Height / 2;
            panel2.Location = new Point(this.Width / 16, 0);

            panel16.Width = panel2.Width;
            panel16.Height = panel2.Height / 5;
            panel16.Location = new Point(0, 0);
            label1.Location = new Point((panel16.Width - label1.Width) / 2, (panel16.Height-label1.Height)/2);

            panel17.Width = panel2.Width;
            panel17.Height = (panel2.Height / 5)*2;
            panel17.Location = new Point(0, panel2.Height / 5);
            label2.Location = new Point((panel17.Width - label2.Width) / 2, (panel17.Height - label2.Height) / 2);

            panel18.Width = panel2.Width;
            panel18.Height = (panel2.Height / 5) * 2;
            panel18.Location = new Point(0, (panel2.Height / 5)*3);
            label3.Location = new Point((panel18.Width - label3.Width) / 2, (panel18.Height - label3.Height) / 2);


            //Process 2
            panel3.Width = (this.Width / 16) * 3;
            panel3.Height = pnFill.Height / 2;
            panel3.Location = new Point((this.Width / 16)*4, 0);

            panel21.Width = panel3.Width;
            panel21.Height = panel3.Height / 5;
            panel21.Location = new Point(0, 0);
            label6.Location = new Point((panel21.Width - label6.Width) / 2, (panel21.Height - label6.Height) / 2);

            panel20.Width = panel3.Width;
            panel20.Height = (panel3.Height / 5) * 2;
            panel20.Location = new Point(0, panel3.Height / 5);
            label5.Location = new Point((panel20.Width - label5.Width) / 2, (panel20.Height - label5.Height) / 2);

            panel19.Width = panel3.Width;
            panel19.Height = (panel3.Height / 5) * 2;
            panel19.Location = new Point(0, (panel3.Height / 5) * 3);
            label4.Location = new Point((panel19.Width - label4.Width) / 2, (panel19.Height - label4.Height) / 2);

            //Process 3
            panel4.Width = (this.Width / 16) * 3;
            panel4.Height = pnFill.Height / 2;
            panel4.Location = new Point((this.Width / 16) * 7, 0);

            panel24.Width = panel4.Width;
            panel24.Height = panel4.Height / 5;
            panel24.Location = new Point(0, 0);
            label9.Location = new Point((panel24.Width - label9.Width) / 2, (panel24.Height - label9.Height) / 2);

            panel23.Width = panel4.Width;
            panel23.Height = (panel4.Height / 5) * 2;
            panel23.Location = new Point(0, panel4.Height / 5);
            label8.Location = new Point((panel23.Width - label8.Width) / 2, (panel23.Height - label8.Height) / 2);

            panel22.Width = panel4.Width;
            panel22.Height = (panel4.Height / 5) * 2;
            panel22.Location = new Point(0, (panel4.Height / 5) * 3);
            label7.Location = new Point((panel22.Width - label7.Width) / 2, (panel22.Height - label7.Height) / 2);

            //Process 4
            panel5.Width = (this.Width / 16) * 3;
            panel5.Height = pnFill.Height / 2;
            panel5.Location = new Point((this.Width / 16) * 10, 0);

            panel27.Width = panel5.Width;
            panel27.Height = panel5.Height / 5;
            panel27.Location = new Point(0, 0);
            label12.Location = new Point((panel27.Width - label12.Width) / 2, (panel27.Height - label12.Height) / 2);

            panel26.Width = panel5.Width;
            panel26.Height = (panel5.Height / 5) * 2;
            panel26.Location = new Point(0, panel5.Height / 5);
            label11.Location = new Point((panel26.Width - label11.Width) / 2, (panel26.Height - label11.Height) / 2);

            panel25.Width = panel5.Width;
            panel25.Height = (panel5.Height / 5) * 2;
            panel25.Location = new Point(0, (panel5.Height / 5) * 3);
            label10.Location = new Point((panel25.Width - label10.Width) / 2, (panel25.Height - label10.Height) / 2);

            //Process 5
            panel6.Width = (this.Width / 16) * 3;
            panel6.Height = pnFill.Height / 2;
            panel6.Location = new Point((this.Width / 16) * 13, 0);

            panel30.Width = panel6.Width;
            panel30.Height = panel6.Height / 5;
            panel30.Location = new Point(0, 0);
            label15.Location = new Point((panel30.Width - label15.Width) / 2, (panel30.Height - label15.Height) / 2);

            panel29.Width = panel6.Width;
            panel29.Height = (panel6.Height / 5) * 2;
            panel29.Location = new Point(0, panel6.Height / 5);
            label14.Location = new Point((panel29.Width - label14.Width) / 2, (panel29.Height - label14.Height) / 2);

            panel28.Width = panel6.Width;
            panel28.Height = (panel6.Height / 5) * 2;
            panel28.Location = new Point(0, (panel6.Height / 5) * 3);
            label13.Location = new Point((panel28.Width - label13.Width) / 2, (panel28.Height - label13.Height) / 2);

            //LBL2
            panel7.Width = (this.Width / 16);
            panel7.Height = pnFill.Height / 2;
            panel7.Location = new Point(0, pnFill.Height / 2);

            panel14.Width = panel7.Width;
            panel14.Height = panel7.Height / 5 * 2;
            panel14.Location = new Point(0, panel7.Height / 5);
            panel15.Width = panel7.Width;
            panel15.Height = panel7.Height / 5 * 2;
            panel15.Location = new Point(0, panel7.Height / 5 * 3);

            lblready2.AutoSize = false;
            lblready2.NewText = "READY";
            lblready2.ForeColor = Color.Red;
            lblready2.RotateAngle = -90;
            lblready2.Parent = panel14;
            lblready2.Height = lblready2.Width * 2 + 15;
            lblready2.Location = new Point(0, (panel14.Height - (lblready2.Height / 2)) / 2);

            lblFn2.AutoSize = false;
            lblFn2.NewText = "FINISH";
            lblFn2.ForeColor = Color.Blue;
            lblFn2.RotateAngle = -90;
            lblFn2.Parent = panel15;
            lblFn2.Height = lblFn.Width * 2 + 15;
            lblFn2.Location = new Point(0, (panel15.Height - (lblFn2.Height / 2)) / 2);

            //Process 6
            panel8.Width = (this.Width / 16) * 3;
            panel8.Height = pnFill.Height / 2;
            panel8.Location = new Point(this.Width / 16, pnFill.Height / 2);

            panel33.Width = panel8.Width;
            panel33.Height = panel8.Height / 5;
            panel33.Location = new Point(0, 0);
            label18.Location = new Point((panel33.Width - label18.Width) / 2, (panel33.Height - label18.Height) / 2);

            panel32.Width = panel8.Width;
            panel32.Height = (panel8.Height / 5) * 2;
            panel32.Location = new Point(0, panel8.Height / 5);
            label17.Location = new Point((panel32.Width - label17.Width) / 2, (panel32.Height - label17.Height) / 2);

            panel31.Width = panel8.Width;
            panel31.Height = (panel8.Height / 5) * 2;
            panel31.Location = new Point(0, (panel8.Height / 5) * 3);
            label16.Location = new Point((panel31.Width - label16.Width) / 2, (panel31.Height - label16.Height) / 2);

            //Process 7
            panel9.Width = (this.Width / 16) * 3;
            panel9.Height = pnFill.Height / 2;
            panel9.Location = new Point((this.Width / 16) * 4, pnFill.Height / 2);

            panel36.Width = panel9.Width;
            panel36.Height = panel9.Height / 5;
            panel36.Location = new Point(0, 0);
            label21.Location = new Point((panel36.Width - label21.Width) / 2, (panel36.Height - label21.Height) / 2);

            panel35.Width = panel9.Width;
            panel35.Height = (panel9.Height / 5) * 2;
            panel35.Location = new Point(0, panel9.Height / 5);
            label20.Location = new Point((panel35.Width - label20.Width) / 2, (panel35.Height - label20.Height) / 2);

            panel34.Width = panel9.Width;
            panel34.Height = (panel9.Height / 5) * 2;
            panel34.Location = new Point(0, (panel9.Height / 5) * 3);
            label19.Location = new Point((panel34.Width - label19.Width) / 2, (panel34.Height - label19.Height) / 2);

            //Process 8
            panel10.Width = (this.Width / 16) * 3;
            panel10.Height = pnFill.Height / 2;
            panel10.Location = new Point((this.Width / 16) * 7, pnFill.Height / 2);

            panel39.Width = panel10.Width;
            panel39.Height = panel10.Height / 5;
            panel39.Location = new Point(0, 0);
            label24.Location = new Point((panel39.Width - label24.Width) / 2, (panel39.Height - label24.Height) / 2);

            panel38.Width = panel10.Width;
            panel38.Height = (panel10.Height / 5) * 2;
            panel38.Location = new Point(0, panel10.Height / 5);
            label23.Location = new Point((panel38.Width - label23.Width) / 2, (panel38.Height - label23.Height) / 2);

            panel37.Width = panel10.Width;
            panel37.Height = (panel10.Height / 5) * 2;
            panel37.Location = new Point(0, (panel10.Height / 5) * 3);
            label22.Location = new Point((panel37.Width - label22.Width) / 2, (panel37.Height - label22.Height) / 2);

            //All
            panel11.Width = (this.Width / 16) * 6;
            panel11.Height = pnFill.Height / 2;
            panel11.Location = new Point((this.Width / 16) * 10, pnFill.Height / 2);

            panel40.Width = panel11.Width;
            panel40.Height = panel11.Height / 5;
            panel40.Location = new Point(0, 0);
            label25.Location = new Point((panel40.Width - label25.Width) / 2, (panel40.Height - label25.Height) / 2);

            panel41.Width = panel11.Width;
            panel41.Height = (panel11.Height / 5)*4;
            panel41.Location = new Point(0, panel11.Height / 5);

            panel42.Width = (panel41.Width/8)*3;
            panel42.Height = (panel41.Height / 5);
            panel42.Location = new Point(panel41.Width / 8, 0);
            label26.Location = new Point((panel42.Width - label26.Width) / 2, (panel42.Height - label26.Height) / 2);

            panel43.Width = (panel41.Width / 8) * 4;
            panel43.Height = (panel41.Height / 5);
            panel43.Location = new Point((panel41.Width / 8)*4, 0);
            label27.Location = new Point((panel43.Width - label27.Width) / 2, (panel43.Height - label27.Height) / 2);

            panel48.Width = (panel41.Width / 8);
            panel48.Height = (panel41.Height / 5)*2;
            panel48.Location = new Point(0, panel41.Height / 5);

            panel44.Width = (panel41.Width / 8)*3;
            panel44.Height = (panel41.Height / 5) * 2;
            panel44.Location = new Point((panel41.Width / 8), (panel41.Height / 5));
            label28.Location = new Point((panel44.Width - label28.Width) / 2, (panel44.Height - label28.Height) / 2);

            panel45.Width = (panel41.Width / 8)*4;
            panel45.Height = (panel41.Height / 5) * 2;
            panel45.Location = new Point((panel41.Width / 8)*4, (panel41.Height / 5));
            label29.Location = new Point((panel45.Width - label29.Width) / 2, (panel45.Height - label29.Height) / 2);

            panel49.Width = (panel41.Width / 8);
            panel49.Height = (panel41.Height / 5) * 2;
            panel49.Location = new Point(0,( panel41.Height / 5)*3);

            panel46.Width = (panel41.Width / 8) * 3;
            panel46.Height = (panel41.Height / 5) * 2;
            panel46.Location = new Point((panel41.Width / 8), (panel41.Height / 5)*3);
            label30.Location = new Point((panel46.Width - label30.Width) / 2, (panel46.Height - label30.Height) / 2);

            panel47.Width = (panel41.Width / 8) * 4;
            panel47.Height = (panel41.Height / 5) * 2;
            panel47.Location = new Point((panel41.Width / 8) * 4, (panel41.Height / 5)*3);
            label31.Location = new Point((panel47.Width - label31.Width) / 2, (panel47.Height - label31.Height) / 2);

            lblOrder.AutoSize = false;
            lblOrder.NewText = "ORDER";
            lblOrder.ForeColor = Color.Red;
            lblOrder.RotateAngle = -90;
            lblOrder.Parent = panel48;
            lblOrder.Height = lblOrder.Width * 2 + 20;
            lblOrder.Location = new Point(-20, (panel48.Height - (lblOrder.Height / 2)) / 2);

            lblFn3.AutoSize = false;
            lblFn3.NewText = "FINISH";
            lblFn3.ForeColor = Color.Blue;
            lblFn3.RotateAngle = -90;
            lblFn3.Parent = panel49;
            lblFn3.Height = lblFn3.Width * 2 + 20;
            lblFn3.Location = new Point(-20, (panel49.Height - (lblFn3.Height / 2)) / 2);

            Line();
        }

        //Draw line
        private void Line()
        {
            //TOP
            label35.Location = new Point(panel1.Width-1, 0);
            label35.Size = new Size(1, panel1.Height / 5);

            label34.Location = new Point(0, 0);
            label34.Size = new Size(1, panel2.Height / 5);

            label32.Location = new Point(panel2.Width-1, 0);
            label32.Size = new Size(1, panel2.Height/5);

            label33.Location = new Point(0, 0);
            label33.Size = new Size(1, panel3.Height / 5);

            label36.Location = new Point(panel3.Width - 1, 0);
            label36.Size = new Size(1, panel3.Height / 5);

            label37.Location = new Point(0, 0);
            label37.Size = new Size(1, panel4.Height / 5);

            label38.Location = new Point(panel4.Width - 1, 0);
            label38.Size = new Size(1, panel4.Height / 5);

            label39.Location = new Point(0, 0);
            label39.Size = new Size(1, panel5.Height / 5);

            label40.Location = new Point(panel4.Width - 1, 0);
            label40.Size = new Size(1, panel5.Height / 5);

            label41.Location = new Point(0, 0);
            label41.Size = new Size(1, panel6.Height / 5);

            //BOT
            label42.Location = new Point(panel7.Width - 1, panel7.Height / 30);
            label42.Size = new Size(1, panel7.Height / 5);

            label43.Location = new Point(0, panel8.Height / 30);
            label43.Size = new Size(1, panel8.Height / 5);

            label44.Location = new Point(panel8.Width - 1, panel8.Height / 30);
            label44.Size = new Size(1, panel8.Height / 5);

            label45.Location = new Point(0, panel9.Height / 30);
            label45.Size = new Size(1, panel9.Height / 5);

            label46.Location = new Point(panel9.Width - 1, panel9.Height / 30);
            label46.Size = new Size(1, panel9.Height / 5);

            label47.Location = new Point(0, panel10.Height / 30);
            label47.Size = new Size(1, panel10.Height / 5);

            label48.Location = new Point(panel10.Width - 1, panel10.Height / 30);
            label48.Size = new Size(1, panel10.Height / 5);

            label49.Location = new Point(0, panel11.Height / 30);
            label49.Size = new Size(1, panel11.Height / 5);

        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            DBSetting frm = new DBSetting();
            this.Hide();
            frm.Show();
        }

    }
}
