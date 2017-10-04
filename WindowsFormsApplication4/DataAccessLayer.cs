using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Data.OleDb;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
//using System.Data.OracleClient;


namespace DAL
{
    public enum DatabaseType
    {
        Access,
        SQLServer,
        Oracle
        // any other data source type
    }

    public enum ParameterType
    {
        Integer,
        Char,
        VarChar
        // define a common parameter type set
    }

    public class DataFactory
    {
        private DataFactory() { }

        public static IDbConnection CreateConnection
           (string ConnectionString,
           DatabaseType dbtype)
        {
            IDbConnection cnn;

            switch (dbtype)
            {
                case DatabaseType.Access:
                    cnn = new OleDbConnection(ConnectionString);
                    break;
                case DatabaseType.SQLServer:
                    cnn = new SqlConnection(ConnectionString);
                    break;
                case DatabaseType.Oracle:
                    cnn = new OracleConnection(ConnectionString);
                    break;
                default:
                    cnn = new SqlConnection(ConnectionString);
                    break;
            }

            return cnn;
        }


        public static IDbCommand CreateCommand
           (string CommandText, DatabaseType dbtype,
           IDbConnection cnn)
        {
            IDbCommand cmd;
            switch (dbtype)
            {
                case DatabaseType.Access:
                    cmd = new OleDbCommand(CommandText,(OleDbConnection)cnn);
                    break;

                case DatabaseType.SQLServer:
                    cmd = new SqlCommand(CommandText,(SqlConnection)cnn);
                    break;

                case DatabaseType.Oracle:
                    cmd = new OracleCommand(CommandText,(OracleConnection)cnn);
                    break;
                default:
                    cmd = new SqlCommand(CommandText,(SqlConnection)cnn);
                    break;
            }

            return cmd;
        }


        public static DbDataAdapter CreateAdapter
           (IDbCommand cmd, DatabaseType dbtype)
        {
            DbDataAdapter da;
            switch (dbtype)
            {
                case DatabaseType.Access:
                    da = new OleDbDataAdapter((OleDbCommand)cmd);
                    break;

                case DatabaseType.SQLServer:
                    da = new SqlDataAdapter((SqlCommand)cmd);
                    break;

                case DatabaseType.Oracle:
                    da = new OracleDataAdapter((OracleCommand)cmd);
                    break;

                default:
                    da = new SqlDataAdapter((SqlCommand)cmd);
                    break;
            }

            return da;
        }
    }
}


namespace DatabaseManagement
{
    class MSAccess
    {
        private OleDbConnectionStringBuilder OledbConString
        {
            get
            {

                OleDbConnectionStringBuilder strAccess = new OleDbConnectionStringBuilder();
                string accessProvider = "Microsoft.ACE.OLEDB.12.0";
                string accessDataSource = @"C:\Users\Alan\Documents\Database1.accdb";
                strAccess.Provider = accessProvider;
                strAccess.OleDbServices = -14;
                strAccess.DataSource = accessDataSource;
                return strAccess;
            }
        }
}
}
