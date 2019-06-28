using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HistoryProductionRFID
{
    public class SQLDataProvider 
    {
        private string connectionStringName = DBConnection.Connection;
        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionStringName);
        }
        public int QUANTITY__PROCESS_FINISH(int Process)
        {
            
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "QUANTITY_ORDER_PROCESS_FINISH";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PROCESS", SqlDbType.Int).Value = Process;
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                int ketQua = int.Parse(ds.Tables[0].Rows[0][0].ToString().TrimEnd());
                return ketQua;
            }
        }
        public int QUANTITY__PROCESS_READY(int Process)
        {
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "QUANTITY_ORDER_PROCESS_READY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PROCESS", SqlDbType.Int).Value = Process;
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                int ketQua = int.Parse(ds.Tables[0].Rows[0][0].ToString().TrimEnd());
                return ketQua;
            }
        }
        public int QUANTITY_ORDER_DAY()
        {
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "QUANTITY_ORDER_DAY";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                int ketQua = int.Parse(ds.Tables[0].Rows[0][0].ToString().TrimEnd());
                return ketQua;
            }
        }
        public int QUANTITY_ORDER_MONTH()
        {
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "QUANTITY_ORDER_MONTH";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                int ketQua = int.Parse(ds.Tables[0].Rows[0][0].ToString().TrimEnd());
                return ketQua;
            }
        }
        public int QUANTITY_FINISH_DAY()
        {
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "QUANTITY_FINISH_DAY";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                int ketQua = int.Parse(ds.Tables[0].Rows[0][0].ToString().TrimEnd());
                return ketQua;
            }
        }
        public int QUANTITY_FINISH_MONTH()
        {
            using (SqlConnection cnn = GetConnection())
            {
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "QUANTITY_FINISH_MONTH";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                int ketQua = int.Parse(ds.Tables[0].Rows[0][0].ToString().TrimEnd());
                return ketQua;
            }
        }
    }
}