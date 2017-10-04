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



namespace AgilentTuneReportProcessorOrig
{
    public abstract class AgilentReport
    {
        private List<DataTable> _datatables;
        protected List<string> ConnectionStrings
        {
            get
            {
                List<string> strConns = new List<string>();
                string[] connStrings = new string[2];

                

                OleDbConnectionStringBuilder strAccess = new OleDbConnectionStringBuilder();
                string accessProvider = "Microsoft.ACE.OLEDB.12.0";
                string accessDataSource = @"C:\Users\Alan\Documents\Database1.accdb";
                strAccess.Provider = accessProvider;
                strAccess.OleDbServices = -14;
                strAccess.DataSource = accessDataSource;
                strConns.Add(strAccess.ToString());

                OracleConnectionStringBuilder strOracle = new OracleConnectionStringBuilder();
                string oracleUserID = "achiang";
                string oraclePassword = "centos";
                string oracleDataSource = "192.168.1.252:1521/ORCL";
                bool oraclePooling = true;
                strOracle.DataSource = oracleDataSource;
                strOracle.UserID = oracleUserID;
                strOracle.Password = oraclePassword;
                strOracle.Pooling = oraclePooling;
                strConns.Add(strOracle.ToString());

                return strConns;
            }
        }
        abstract public string ModelName { get; }
        abstract public string SerialNumber { get; }
        public string FullPath { get; set; }
        public AgilentReport(string _FullPath)
        {
            this.FullPath = _FullPath;
        }
        public DataSet Dataset
        {
            get
            {
                DataSet ds = new DataSet();
                ds.ReadXml(this.FullPath);
                return ds;
            }
        }
        public DateTime ModifiedTime
        {
            get
            {
                //find date of last modified
                DateTime lastModified = System.IO.File.GetLastWriteTime(this.FullPath);
                return lastModified;
            }
        }
    }

    public class PerformanceReport : AgilentReport
    {
        public PerformanceReport(string _FullPath) : base(_FullPath)
        {
            ReportSets = new ReportSets(this);
            return;
        }
        public void InsertToOracle()
        {
            List<Task> tasks = new List<Task>();

            List<string> rsids = this.Dataset.Tables["TuneReport"].AsEnumerable().Select(r => r.Field<string>("ReportSetID")).ToList();

            List<DataTable> datatables = Dataset.Tables.Cast<DataTable>().AsEnumerable().Where(dt => !dt.TableName.Contains("TuneReportDataSet")).ToList();
            foreach (string rs in rsids)
            {
                //Find list of ReportsetIDs

                //Find filtered table based on ReportsetID
                List<DataTable> newtbls = new List<DataTable>();
                foreach (DataTable dt in datatables)
                {
                    //reportsetid does not exist, NEED TO FIX
                    IEnumerable<DataRow> drs = dt.AsEnumerable().Where(r => r.Field<int>("ReportSetID") == 0);
                    if (drs.Any() == true)
                    {
                        DataTable tbl = drs.CopyToDataTable();
                        newtbls.Add(tbl);
                    }
                }
            }
        }
        public override string ModelName
        {
            get
            {
                string result;
                DataTable tuneDT = this.Dataset.Tables["TuneReport"];
                DataRow dr = tuneDT.Rows[0];
                return result = dr["ModelName"].ToString();
            }
        }
        public override string SerialNumber
        {
            get
            {
                string result;
                DataTable tuneDT = this.Dataset.Tables["TuneReport"];
                DataRow dr = tuneDT.Rows[0];
                return result = dr["SerialNumber"].ToString();
            }
        }
        public string CompositeKey
        {
            get
            {

                string result = this.ModelName + " - " + this.SerialNumber + " - " + this.ModifiedTime;

                return result;
            }
        }
        public ReportSets ReportSets { get; set; }

    }

    public class ReportSet : IReportable
    {
        private int rs;
        private DataSet _dataset;
        private List<DataTable> _dts;

        public List<DataTable> Datatables
        {
            get
            {
                return _dts;
            }
            set
            {
                _dts = value;
            }
        }
        public DataSet Dataset
        {
            get
            {
                return _dataset;
            }
            set
            {
                _dataset = value;
            }
        }
        public int ReportSetID
        {
            get
            {
                return rs;
            }
            set
            {
                rs = value;
            }
        }
        public ReportSet(int rsid, ReportSets RSs)
        {
            this.ReportSetID = rsid;
            this.Dataset = RSs.Dataset;
            List<DataTable> newtbls = new List<DataTable>();
            foreach (DataTable dt in this.Dataset.Tables)
            {
                if (dt.TableName != "TuneReportDataSet")
                {

                    //reportsetid does not exist, NEED TO FIX
                    IEnumerable<DataRow> drs = dt.AsEnumerable().Where(r => r.Field<string>("ReportSetID") == rsid.ToString());
                    if (drs.Any() == true)
                    {
                        DataTable tbl = drs.CopyToDataTable();
                        tbl.TableName = dt.TableName;
                        newtbls.Add(tbl);
                    }
                }
            }
            this.Datatables = newtbls;
        }

        public void InsertToOracle()
        {
            foreach (DataTable dt in this.Datatables)   
            {

            }
        }
    }

    public class ReportSets
    {
        private DataSet _dataset;
        private List<ReportSet> _reportsetids;
        public List<ReportSet> ReportSetIDs
        {
            get
            {
                return _reportsetids;
            }
            set
            {
                _reportsetids = value;
            }
        }
        public DataSet Dataset
        {
            get
            {
                return _dataset;
            }
            set
            {
                this._dataset = value;
            }
        }
        public ReportSets(DataSet ds)
        {
            this.Dataset = ds;
        }
        private void GetReportSets()
        {
            List<ReportSet> newReportSets = new List<ReportSet>();
            //Find list of ReportsetIDs
            List<string> rsids = this.Dataset.Tables["TuneReport"].AsEnumerable().Select(r => r.Field<string>("ReportSetID")).ToList();
            foreach (string rsid in rsids)
            {
                ReportSet nrs = new ReportSet(Convert.ToInt32(rsid), this);
                //Add class to collection
                newReportSets.Add(nrs);
            }
            this.ReportSetIDs = newReportSets;
        }
        public ReportSets(UserTuneReport utr)
        {
            this.Dataset = utr.Dataset;
            GetReportSets();
        }
        public ReportSets(PerformanceReport pr)
        {
            this.Dataset = pr.Dataset;
            GetReportSets();
        }
        public void InsertToOracle()
        {
            Console.WriteLine("test");
        }
    }

    interface IReportable
    {
        void InsertToOracle();
        //void InsertToAccess();
        //void InsertToSQLServer();
        //void InsertToMySQL();
    }

    public class UserTuneReport : AgilentReport
    {
        public UserTuneReport(string _FullPath) : base(_FullPath)
        {
            ReportSets = new ReportSets(this);
            return;
        }
        public void InsertToOracle()
        {
            List<Task> tasks = new List<Task>();
            
        }
        public override string ModelName
        {
            get
            {
                string result;
                DataTable tuneDT = this.Dataset.Tables["TuneReport"];
                DataRow dr = tuneDT.Rows[0];
                return result = dr["ModelName"].ToString();
            }
        }
        public override string SerialNumber
        {
            get
            {
                string result;
                DataTable tuneDT = this.Dataset.Tables["TuneReport"];
                DataRow dr = tuneDT.Rows[0];
                return result = dr["SerialNumber"].ToString();
            }
        }
        public string CompositeKey
        {
            get
            {

                string result = this.ModelName + " - " + this.SerialNumber + " - " + this.ModifiedTime;

                return result;
            }
        }
        public void InsertToOracle_old()
        {
            //Maybe use temporary table

            //Set Oracle Connection from pre-built connection strings;
            OracleConnection conn = new OracleConnection(this.ConnectionStrings[1]);
            //Open Oracle connection
            conn.Open();
            //Start transaction for ACID
            OracleTransaction trans = conn.BeginTransaction();
            //Set data adapter
            OracleDataAdapter ad = new OracleDataAdapter();
            conn.Close();
            List<Task> tasks = new List<Task>();
            try
            {
                //loop through each table
                //Find Workable Tables that exludes Header table
                List<DataTable> datatables = (from dc in this.Dataset.Tables.Cast<DataTable>()
                                              where !dc.TableName.Contains("TuneReportDataSet")
                                              select dc).ToList();
                List<string> reportSetIDs = this.Dataset.Tables["TuneReport"].AsEnumerable().Select(r => r.Field<string>("ReportSetID")).ToList();
                foreach (string rsid in reportSetIDs)
                {
                }

                //MAYBE NEED TO LOOP THROUGH DIFFERENT REPORTSETID INSTEAD SINCE NO COMMON COMPOSITE KEY
                //foreach (DataTable currTable in this.Dataset.Tables)
                //{
                //    tasks.Add(Task.Factory.StartNew(() =>
                //    {
                //        //add each field name to list
                //        List<string> cn = (from dc in currTable.Columns.Cast<DataColumn>()
                //                           where !dc.ColumnName.Contains("Id")
                //                           select dc.ColumnName).ToList();
                //        //Add 'Composite Key' to field Names to work as FK & PK
                //        cn.Add("CompositeKey");
                //        //Create field name string
                //        string colNames = string.Join(",", cn.ToArray()).Replace("Value", "TuneValue").Replace("Count", "TuneCount");
                //        //Create parameter name string
                //        string paramNames = string.Join(",:", cn.ToArray()).Replace("Value", "TuneValue").Replace("Count", "TuneCount");
                //        //Construct parameterized query string from field names and parameter names
                //        string insqry = "INSERT INTO TEMP_" + currTable.TableName + " (" + colNames + ") VALUES (:" + paramNames + ")";

                //        try
                //        {
                //            //Loop through each record in current table
                //            foreach (DataRow drrow in currTable.Rows)
                //            {
                //                //Add new command for current record
                //                ad.InsertCommand = new OracleCommand(insqry, conn);
                //                ad.InsertCommand.Transaction = trans;
                //                foreach (string dc in cn)
                //                {
                //                    //If field name is not 
                //                    if (dc != "CompositeKey")
                //                    {
                //                        //Add field value for current row as parameter
                //                        if (drrow[dc] == null)
                //                        {
                //                            ad.InsertCommand.Parameters.Add(new OracleParameter(":" + dc, "null"));
                //                        }
                //                        else
                //                        {
                //                            ad.InsertCommand.Parameters.Add(new OracleParameter(":" + dc, drrow[dc]));
                //                        }
                //                    }
                //                    else
                //                    {
                //                        //Add pre-constructed composite key as FK; In Parent node, will serve as PK
                //                        ad.InsertCommand.Parameters.Add(new OracleParameter(":CompositeKey", "test"));
                //                    }
                //                }
                //                //Insert command to transaction
                //                conn.Open();
                //                ad.InsertCommand.ExecuteNonQuery();
                //                conn.Close();
                //            }

                //            //ad.UpdateCommand = "UPDATE " + currTable.TableName + 
                //        }
                //        catch (OracleException e)
                //        {
                //            //Catch Duplicate TuneReport
                //            if (e.ErrorCode == -2147467259)
                //            {
                //                Console.WriteLine(e.Message + "Due to existed Primary Key");
                //            }
                //            trans.Rollback();
                //            return;
                //        }
                //    }));
                //    Task.WaitAll(tasks.ToArray());
                //}
                //Commit transaction

                //trans.Commit();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.HResult);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.Message);
                //Dispose of transaction and connection
                //trans.Dispose();
                conn.Dispose();
            }
            finally
            {
                //Dispose of connection if all succeeded
                conn.Dispose();
            }

        }
        public ReportSets ReportSets { get; set; }
    }
    class BatchReport : AgilentReport, IReportable
    {
        public void InsertToAccess()
        {
            //loop through each table
            foreach (DataTable currTable in this.Dataset.Tables)
            {
                //add each field name to list
                List<string> cn = (from dc in currTable.Columns.Cast<DataColumn>()
                                   where !dc.ColumnName.Contains("Id")
                                   select dc.ColumnName).ToList();
                //Add 'Composite Key' to field Names to work as FK & PK
                cn.Add("CompositeKey");
                //Create field name string
                string colNames = string.Join(",", cn.ToArray()).Replace("Value", "TuneValue").Replace("Count", "TuneCount");
                //Create parameter name string
                string paramNames = string.Join(",@", cn.ToArray()).Replace("Value", "TuneValue").Replace("Count", "TuneCount");
                //Construct parameterized query string from field names and parameter names
                string insqry = "INSERT INTO " + currTable.TableName + " (" + colNames + ") VALUES (@" + paramNames + ")";
                //Set MS Access Connection from pre-built connection strings;
                OleDbConnection conn = new OleDbConnection(this.ConnectionStrings[0]);
                //Open MS Access connection
                conn.Open();
                //Set data adapter
                OleDbDataAdapter ad = new OleDbDataAdapter();

                try
                {
                    //Loop through each record in current table
                    foreach (DataRow drrow in currTable.Rows)
                    {
                        //Add new command for current record
                        ad.InsertCommand = new OleDbCommand(insqry, conn);
                        foreach (string dc in cn)
                        {
                            //If field name is not 
                            if (dc != "CompositeKey")
                            {
                                //Add field value for current row as parameter
                                ad.InsertCommand.Parameters.Add(new OleDbParameter("@" + dc, drrow[dc]));
                            }
                            else
                            {
                                //Add pre-constructed composite key as FK; In Parent node, will serve as PK
                                ad.InsertCommand.Parameters.Add(new OleDbParameter("@CompositeKey", this.CompositeKey));
                            }
                        }
                        //Insert command to transaction
                        ad.InsertCommand.ExecuteNonQuery();
                    }
                }
                catch (OleDbException e)
                {
                    if (e.ErrorCode == -2147467259)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
                }
                conn.Dispose();
            }
        }
        public void InsertToOracle()
        {
            //Set Oracle Connection from pre-built connection strings;
            OracleConnection conn = new OracleConnection(this.ConnectionStrings[1]);
            //Open Oracle connection
            conn.Open();
            //Start transaction for ACID
            OracleTransaction trans = conn.BeginTransaction();
            //Set data adapter
            OracleDataAdapter ad = new OracleDataAdapter();
            conn.Close();
            try
            {
                //loop through each table
                foreach (DataTable currTable in this.Dataset.Tables)
                {
                    //add each field name to list
                    List<string> cn = (from dc in currTable.Columns.Cast<DataColumn>()
                                       where !dc.ColumnName.Contains("Id")
                                       select dc.ColumnName).ToList();
                    //Add 'Composite Key' to field Names to work as FK & PK
                    cn.Add("CompositeKey");
                    //Create field name string
                    string colNames = string.Join(",", cn.ToArray()).Replace("Value", "TuneValue").Replace("Count", "TuneCount");
                    //Create parameter name string
                    string paramNames = string.Join(",:", cn.ToArray()).Replace("Value", "TuneValue").Replace("Count", "TuneCount");
                    //Construct parameterized query string from field names and parameter names
                    string insqry = "INSERT INTO " + currTable.TableName + " (" + colNames + ") VALUES (:" + paramNames + ")";

                    try
                    {
                        //Loop through each record in current table
                        foreach (DataRow drrow in currTable.Rows)
                        {
                            //Add new command for current record
                            ad.InsertCommand = new OracleCommand(insqry, conn);
                            ad.InsertCommand.Transaction = trans;
                            foreach (string dc in cn)
                            {
                                //If field name is not 
                                if (dc != "CompositeKey")
                                {
                                    //Add field value for current row as parameter
                                    if (drrow[dc] == null)
                                    {
                                        ad.InsertCommand.Parameters.Add(new OracleParameter(":" + dc, "null"));
                                    }
                                    else
                                    {
                                        ad.InsertCommand.Parameters.Add(new OracleParameter(":" + dc, drrow[dc]));
                                    }
                                }
                                else
                                {
                                    //Add pre-constructed composite key as FK; In Parent node, will serve as PK
                                    ad.InsertCommand.Parameters.Add(new OracleParameter(":CompositeKey", this.CompositeKey));
                                }
                            }
                            //Insert command to transaction
                            conn.Open();
                            ad.InsertCommand.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    catch (OracleException e)
                    {
                        //Catch Duplicate TuneReport
                        if (e.ErrorCode == -2147467259)
                        {
                            Console.WriteLine(e.Message + "Due to existed Primary Key");
                        }
                        trans.Rollback();
                        return;
                    }
                }
                //Commit transaction from temp table to orig table
                trans.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.HResult);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.Message);
                //Dispose of transaction and connection
                trans.Dispose();
                conn.Dispose();
            }
            finally
            {
                //Dispose of connection if all succeeded
                conn.Dispose();
            }

        }
        public string CompositeKey
        {
            get
            {
                string[] val = { this.ModelName, this.SerialNumber, this.ModifiedTime.ToString() };
                string sep = " - ";
                string result = string.Join(sep, val);

                return result;
            }
        }
        public override string ModelName
        {
            get
            {
                string result;
                DataTable tuneDT = this.Dataset.Tables["AcqReportDataSet"];
                DataRow dr = tuneDT.Rows[0];
                return result = dr["ModelName"].ToString();

            }
        }
        public override string SerialNumber
        {
            get
            {
                string result;
                DataRow dr = this.Dataset.Tables["AcqReportDataSet"].Rows[0];
                return result = dr["SerialNumber"].ToString();

            }
        }
        public BatchReport(string _FullPath) : base(_FullPath)
        {
            //Multi thread insertion
            //Thread accessThread = new Thread(this.InsertToAccess);
            Thread oracleThread = new Thread(this.InsertToOracle);
            //Invoke both threads
            //accessThread.Start();
            oracleThread.Start();
            return;
        }
    }
}
