using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using BatchTuneReport;
using PerformanceReportCS;
using TuneBatch;
using AgilentXMLFiles;

namespace AgilentTuneReportProcessorOrig
{
    public partial class Form1 : Form
    {
        List<string> selectedReportSet = new List<string>();

        private List<DataTable> DataTables;
        private SynchronizationContext mainThread;

        public Form1()
        {
            //Winforms designer automatic setup
            InitializeComponent();
            mainThread = SynchronizationContext.Current;
            if (mainThread == null) mainThread = new SynchronizationContext();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DGV_Format()
        {
            foreach (DataGridView dg in this.Controls.OfType<DataGridView>())
            {
                //dg.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                //dg.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void SwitchReport(string strPath)
        {
            if (strPath != "")
            {
                //AgilentReport myReport;
                switch (strPath.Substring(strPath.LastIndexOf(@"\")))
                {
                    case "\\report.results.xml":
                        {

                            
                            BatchReport myReport = new BatchReport(strPath);
                            DataTables = GetDataTables(myReport);
                            DataBindDataGridView(DataTables);
                            break;
                        }
                    case "\\BatchTuneReport.xml":
                        {
                            UserTuneReport myReport = new UserTuneReport(strPath);
                            DataTables = GetDataTables(myReport);
                            DataBindDataGridView(DataTables);
                            var items = checkedListBox1.Items;
                            foreach (ReportSet rs in myReport.ReportSets.ReportSetIDs)
                            {
                                items.Add(rs.ReportSetID, true);
                            }
                            break;
                        }
                    case "\\PerformanceReport.xml":
                        {
                            PerformanceReport myReport = new PerformanceReport(strPath);
                            DataTables = GetDataTables(myReport);
                            DataBindDataGridView(DataTables);
                            var items = checkedListBox1.Items;
                            foreach (ReportSet rs in myReport.ReportSets.ReportSetIDs)
                            {
                                items.Add(rs.ReportSetID, true);
                            }

                            break;
                        }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strPath = FindXMLPath();
            SwitchReport(strPath);
        }

        private void bulkImport_Click(object sender, EventArgs e)
        {
            //Default path
            string strPath = @"C:\Users\Alan\Google Drive\Work";

            //Initialization
            List<string> filepaths = new List<string>();
            List<BatchReport> TuneReports = new List<BatchReport>();

            //search path for filepaths
            filepaths = DirSearch(strPath);
            filepaths = filepaths.Where(u => u.EndsWith("report.results.xml")).ToList();

            //Bulk lambda through Parallel Task Library
            Parallel.ForEach(filepaths, xmlPath =>
            {
                BatchReport myReport = new BatchReport(xmlPath);
                //Some report cannot be added because "Currently Locked" for access
            }
            );

        }

        private string FindFolderDialog()
        {
            //Cannot use FolderBrowserDialog, unusable UI
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) { return dialog.FileName; }
            else { return string.Empty; }
        }

        private List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                { files.Add(f); }
                foreach (string d in Directory.GetDirectories(sDir))
                { files.AddRange(DirSearch(d)); }
            }
            catch (System.Exception excpt)
            { MessageBox.Show(excpt.Message); }

            return files;
        }


        private string FindXMLPath()
        {
            //Find xml path using file explorer with filter for *.xml
            var x = new OpenFileDialog();
            x.Filter = "XML|*.xml";
            if (x.ShowDialog() == DialogResult.OK) { return x.FileName; }
            else { return string.Empty; }
        }

        private void TestXML()
        {
            //parse xml through xpath node selection
            string namespaceName = "ns";
            XmlDocument xDoc = new XmlDocument();
            string strXPath = "/AcqReportDataSet/Tune/TuneParameter/ParameterName";
            strXPath = strXPath.Replace("/", "/{0}");
            xDoc.Load(FindXMLPath());


            XmlNodeList nodeList = xDoc.SelectNodes(string.Format(strXPath, GetNameSpacePrefix(xDoc)), GetNameSpaceManager(xDoc, namespaceName));
            if (nodeList != null)
            {
                foreach (XmlNode node in nodeList)
                {
                    Console.WriteLine(node.InnerText);
                }
            }
        }

        private XmlNamespaceManager GetNameSpaceManager(XmlDocument xDoc, string namespaceName)
        {
            //determines if namespace exists
            XmlNamespaceManager nameSpaceManager = null;
            if (xDoc.FirstChild.NextSibling.Attributes != null)
            {
                var xmlns = xDoc.FirstChild.NextSibling.Attributes["xmlns"];
                if (xmlns != null)
                {
                    nameSpaceManager = new XmlNamespaceManager(xDoc.NameTable);
                    nameSpaceManager.AddNamespace(namespaceName, xmlns.Value);
                }
            }
            return nameSpaceManager;

        }
        private string GetNameSpacePrefix(XmlDocument xDoc)
        {
            //sets namespace name and prefix
            var namespaceName = "ns";
            var nameSpacePrefix = namespaceName + ":";
            return nameSpacePrefix;
        }

        private void DataBindDataGridView(List<DataTable> dts)
        {
            foreach (DataGridView dg in Controls.OfType<DataGridView>())
            {
                    dg.DataSource = dts[dg.TabIndex - 1];
            }
            DGV_Format();
        }


        private List<DataTable> GetDataTables(AgilentReport tr)
        {
            //Method Initialization
            List<DataTable> clonedts = new List<DataTable>();
            DataTable cloneTable = new DataTable();
            DataTableCollection tables = tr.Dataset.Tables;
            List<Task> tasks = new List<Task>();
            string strFilterOption = string.Empty;

            foreach (DataTable dt in tables)
            {
                // Create Task pooling for effective threading
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    //Clone table for population
                    cloneTable = dt.Clone();
                    if (tables.IndexOf(dt) == 0)
                    {
                        //Do nothing since do not want to filter header table
                    }
                    else if (tables.IndexOf(dt) > 0)
                    {
                        //Create filter options
                        switch (tr.GetType().Name)
                        {
                            case "BatchReport":
                                {
                                    strFilterOption = "TuneID<>''";
                                    break;
                                }
                            case "UserTuneReport":
                                {
                                    strFilterOption = "";
                                    break;
                                }
                        }
                    }
                    //import rows
                    foreach (DataRow row in dt.Select(strFilterOption))
                    {
                        cloneTable.ImportRow(row);
                    }
                    clonedts.Add(cloneTable);

                }
                ));
                Task.WaitAll(tasks.ToArray());
            }
            return clonedts;
        }

        private List<DataTable> GetDataTables(AgilentXML tr)
        {
            //Method Initialization
            List<DataTable> clonedts = new List<DataTable>();
            DataTable cloneTable = new DataTable();
            DataTableCollection tables = tr.Dataset.Tables;
            List<Task> tasks = new List<Task>();
            string strFilterOption = string.Empty;

            foreach (DataTable dt in tables)
            {
                // Create Task pooling for effective threading
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    //Clone table for population
                    cloneTable = dt.Clone();
                    if (tables.IndexOf(dt) == 0)
                    {
                        //Do nothing since do not want to filter header table
                    }
                    else if (tables.IndexOf(dt) > 0)
                    {
                        //Create filter options
                        switch (tr.GetType().Name)
                        {
                            case "BatchReport":
                                {
                                    strFilterOption = "TuneID<>''";
                                    break;
                                }
                            case "UserTuneReport":
                                {
                                    strFilterOption = "";
                                    break;
                                }
                        }
                    }
                    //import rows
                    foreach (DataRow row in dt.Select(strFilterOption))
                    {
                        cloneTable.ImportRow(row);
                    }
                    clonedts.Add(cloneTable);

                }
                ));
                Task.WaitAll(tasks.ToArray());
            }
            return clonedts;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(UpdateSelectedItems), null);

        }

        private void UpdateSelectedItems()
        {
            List<int> intSelectedItems = checkedListBox1.CheckedItems.OfType<int>().ToList();
            List<string> strSelectedItems = intSelectedItems.ConvertAll<string>(delegate (int i) { return i.ToString(); });

            string strFilter = string.Join("','", strSelectedItems);
            foreach (DataGridView dg in Controls.OfType<DataGridView>())
            {
                if (dg.Columns.Contains("ReportSetID"))
                {
                    (dg.DataSource as DataTable).DefaultView.RowFilter = "ReportSetID in ('" + strFilter + "')";
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (checkedListBox1.Items.Count != checkedListBox1.CheckedItems.Count)
            {
                //Select all
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
                button2.Text = "Deselect All";
            }
            else
            {
                //deselect all
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                button2.Text = "Select All";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strPath = FindXMLPath();
            //Use deserialized class
            AgilentXML agt = new AgilentXML(strPath);
            DataTables = GetDataTables(agt);
            DataBindDataGridView(DataTables);
        }
    }
}
