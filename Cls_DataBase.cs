using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriverParameterReSet
{
    class Cls_DataBase
    {
        private OleDbConnection odcon = new OleDbConnection();
        public DataTable dt;
        public string strErrorMessage;

        public DataTable getDataTable() => dt;

        /// <summary>打开数据库</summary>
        public bool DBOpen()
        {
            string strPath = Application.StartupPath + @"\Data\";
            //string strC = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + strPath + "ServoDataBase.mdb";   //x86
            string strC = "Provider=Microsoft.ACE.OleDb.12.0;Data Source=" + strPath + "ServoDataBase.mdb";    //x64

            if (!File.Exists(strPath + "ServoDataBase.mdb"))
            {
                strErrorMessage = "找不到数据库文件";
                return false;
            }

            odcon.ConnectionString = strC;

            try
            {
                odcon.Open();
                return true;
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.ToString();
            }

            return false;
        }

        /// <summary>关闭数据库</summary>
        public void DBClose()
        {
            if (odcon != null)
            {
                try
                {
                    odcon.Close();
                }
                catch (Exception ex)
                {
                    strErrorMessage = ex.ToString();
                }
            }
        }

        /// <summary>
        /// 数据库指令
        /// </summary>
        /// <param name="command">指令</param>
        public bool DBSQLCommand(string command)
        {
            if (!DBOpen())
            {
                return false;
            }

            try
            {
                OleDbCommand oleDbCommand = new OleDbCommand(command, odcon);
                oleDbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.ToString();
                return false;
            }

            DBClose();

            return true;
        }

        /// <summary>加载数据库表</summary>
        public DataTable LoadingDataTable()
        {
            if (!DBOpen())
            {
                return null;
            }

            try
            {
                OleDbDataAdapter odda = new OleDbDataAdapter("SELECT * FROM Param", odcon);
                dt = new DataTable();
                odda.Fill(dt);

                DBClose();

                return dt;
            }
            catch (Exception ex)
            {
                DBClose();
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
