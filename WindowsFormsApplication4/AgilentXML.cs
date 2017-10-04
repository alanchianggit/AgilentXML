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
using System.Xml.Serialization;
using System.Text;
using System.Reflection;

namespace AgilentXMLFiles
{
    public class AgilentXML
    {
        public string strFullPath { get; set; }

        public ReportSets Reports { get; set; }
        private string CompositeKey
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb = sb.Append(ModelName);
                sb = sb.Append(" - ");
                sb = sb.Append(SerialNumber);
                sb = sb.Append(" - ");
                sb = sb.Append(ModifiedTime);
                return sb.ToString();
            }
        }

        object deserializedXML;

        private string ModelName { get; set; }
        private string SerialNumber { get; set; }

        public AgilentXML(string _fullPath)
        {
            this.strFullPath = _fullPath;
            this.ValidateReport();
            
        }

        public DataSet Dataset
        {
            get
            {
                DataSet ds = new DataSet();
                ds.ReadXml(this.strFullPath);
                return ds;
            }
        }

        public DateTime ModifiedTime
        {
            get
            {
                //find date of last modified
                DateTime lastModified = System.IO.File.GetLastWriteTime(this.strFullPath);
                return lastModified;
            }
        }


        private void ValidateReport()
        {
            if (this.strFullPath != "")
            {
                StreamReader fs = new StreamReader(this.strFullPath);
                //AgilentReport myReport;
                switch (this.strFullPath.Substring(this.strFullPath.LastIndexOf(@"\")))
                {
                    case "\\report.results.xml":
                        {

                            XmlSerializer xmlserializer = new XmlSerializer(typeof(TuneBatch.AcqReportDataSet));
                            deserializedXML = xmlserializer.Deserialize(fs);
                            dynamic dxml = deserializedXML;
                            SerialNumber = dxml.SerialNumber;
                            ModelName = dxml.ModelName;
                            break;
                        }
                    case "\\BatchTuneReport.xml":
                        {
                            XmlSerializer xmlserializer = new XmlSerializer(typeof(BatchTuneReport.TuneReportDataSet));
                            deserializedXML = xmlserializer.Deserialize(fs);
                            dynamic dxml = deserializedXML;
                            Reports = new ReportSets(this);
                            //SerialNumber = dxml.TuneReport[0].SerialNumber;
                            //ModelName= dxml.TuneReport[0].ModelName;
                            
                            break;
                        }
                    case "\\PerformanceReport.xml":
                        {
                            XmlSerializer xmlserializer = new XmlSerializer(typeof(PerformanceReportCS.TuneReportDataSet ));
                            deserializedXML = xmlserializer.Deserialize(fs);
                            dynamic dxml = deserializedXML;
                            Reports = new ReportSets(this);
                            //SerialNumber = dxml.SerialNumber;
                            //ModelName = dxml.ModelName;
                            break;
                        }
                }
                fs.Close();

            }
        }
    }

    public class ReportSet
    {
        private int rs;
        private DataSet _dataset;
        private List<DataTable> _dts;

        public List<DataTable> Datatables
        {
            get {return _dts;}
            set{_dts = value;}
        }
        public DataSet Dataset
        {
            get{return _dataset;}
            set{_dataset = value;}
        }
        public int ReportSetID
        {
            get{return rs;}
            set{rs = value;}
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
    }

    public class ReportSets
    {
        private DataSet _dataset;
        private List<ReportSet> _reportsetids;
        public List<ReportSet> ReportSetIDs
        {
            get{return _reportsetids;}
            set{_reportsetids = value;}
        }
        public DataSet Dataset
        {
            get{return _dataset;}
            set{this._dataset = value;}
        }
        public ReportSets(DataSet ds)
        {
            this.Dataset = ds;
        }

        public ReportSets(AgilentXML agt)
        {
            this.Dataset = agt.Dataset;
            GetReportSets();
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
    }
}
