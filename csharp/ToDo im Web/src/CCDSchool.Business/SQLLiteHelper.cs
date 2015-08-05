using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Configuration;
namespace CCDSchool.Business
{
    public class SQLLiteHelper
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        public SQLLiteHelper()
        {
        }
        public SQLLiteHelper(string Connectionstring)
        {
            this.ConnectionString = Connectionstring;
        }
        public string _conn;
        public string ConnectionString
        {
            get
            {
                return this._conn;
            }
            set
            {
                this._conn = value;
            }
        }
        private void SetConnection()
        {

            //sql_con = new SQLiteConnection(this.ConnectionString);
           
        }
        public DataTable ExecuteQuery(string Query,string Conn)
        {
             
            sql_con = new SQLiteConnection(Conn);
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = Query;
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            return DT;
        }
        public void ExecutegeneralQuery(string txtQuery, string Conn)
        {
            sql_con = new SQLiteConnection(Conn);
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }
    }
}