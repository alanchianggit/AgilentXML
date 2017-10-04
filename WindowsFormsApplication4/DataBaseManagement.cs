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

namespace DatabaseManagement
{
    class MSAccess
    {
        string connString
        {
            get
            {

                OleDbConnectionStringBuilder strAccess = new OleDbConnectionStringBuilder();
                string accessProvider = "Microsoft.ACE.OLEDB.12.0";
                string accessDataSource = @"C:\Users\Alan\Documents\Database1.accdb";
                strAccess.Provider = accessProvider;
                strAccess.OleDbServices = -14;
                strAccess.DataSource = accessDataSource;
                return strAccess.ToString();
            }
        }
}
}
