using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRegister.DbAccess
{
    public class EntityAccess 
    {
        public EntityAccess()
        {
            strConn = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=ProductCatalog;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            conn = sqlConnection;
            SqlCommand sqlCommand = new SqlCommand();
            SqlCommand = sqlCommand;

        }

        #region AtributosPrivados
        private string _strConn = string.Empty;               
        private DbConnection _conn;
        private DbCommand _SqlCommand;
        #endregion

        #region AtributosPublicos
        public string strConn { get { return this._strConn; } set { this._strConn = value; } }        
        public DbConnection conn { get { return this._conn; } set { this._conn = value; } }
        public DbCommand SqlCommand { get { return this._SqlCommand; } set { this._SqlCommand = value; } }
        
        #endregion

        #region MetodosPublicos        

        public void openConnection()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void closeConnection()
        {
            try
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable ExecNativeQuery(string strQueryText, List<QueryParameter> parametros)
        {
            SqlCommand.CommandText = "";
            SqlCommand.Parameters.Clear();                        

            DbDataReader dataReader = null;
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                for (int i = 0; i < parametros.Count; i++)
                {
                    DbParameter parameter = SqlCommand.CreateParameter();
                    parameter.ParameterName = parametros[i].strParameterName;
                    parameter.Value = parametros[i].objParameterValue;
                    SqlCommand.Parameters.Add(parameter);
                }
                SqlCommand.CommandText = strQueryText;
                SqlCommand.Connection = _conn;

                dataReader = SqlCommand.ExecuteReader();
                dt.Load(dataReader);

            }
            catch (Exception ex)
            {                
                throw ex;                
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }                
            }
            return dt;
        }
        
        #endregion
    }
}
