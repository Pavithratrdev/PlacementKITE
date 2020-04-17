using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KgfslReader.Xls;

namespace KITEPlacement
{
    public partial class DepartmentMastUpload : Form
    {
        #region Constructor
        public DepartmentMastUpload()
        {
            InitializeComponent();
        }

        #endregion

        #region Declarations
        Boolean Contains_Header;
        int Row_Start_No;
        DataSet dsFile = new DataSet();
        private ArrayList objParams = new ArrayList();
        private ArrayList objValues = new ArrayList();
        #endregion

        #region Browse Button Click
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpnDlg = new OpenFileDialog();
                OpnDlg.Filter = "(*.xls)|*.xls";
                if (OpnDlg.ShowDialog(this) == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    txtFileUpld.Text = OpnDlg.FileName;
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Clearcontrols
        private void Clearcontrols()
        {
            txtFileUpld.Text = "";
            Row_Start_No = 0;
        }
        #endregion

        #region Import Button Click
        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string strout = "Successful";
                if (txtFileUpld.Text == "")
                    MessageBox.Show("Select the File");
                else
                {
                    Contains_Header = true;
                    Row_Start_No = 2;
                   
                    LoadExcelFile("D:\\Pavi\\KITE\\KITEPlacement\\KITEPlacement\\InputFiles\\DepartmentMaster.xls");
                    ConnectionString.FnSqlBulkInsert("Dept_Mast_Stgn", dsFile.Tables[0], out strout);
                    if (strout != "Successful")
                    {
                        MessageBox.Show(strout, "Error", MessageBoxButtons.OK);
                        return;
                    }
                    else
                    {
                        strout = "Successful";
                        objParams = CommonMethods.CreateArrayList(new object[] { "@pMode"});
                        objValues = CommonMethods.CreateArrayList(new object[] {"" });
                        ConnectionString.SqlExecInsProc("Pr_DeptMast_Upload", objParams, objValues, out strout);
                        if (strout != "Successful")
                        {
                            MessageBox.Show(strout, "Error", MessageBoxButtons.OK);
                            return;
                        }
                        else
                            MessageBox.Show("File Uploaded Successfully");
                    }
                    Clearcontrols();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Load Excel File
        private bool LoadExcelFile(string FilePath)
        {
            try
            {                
                int nRowHeader = 0;
                XlsReader Reader = new XlsReader(FilePath);
                dsFile.Tables.Add(Reader.Table);
                if (Contains_Header == true)
                {
                    nRowHeader = Row_Start_No - 1;
                    for (int i = 0; i < Row_Start_No - 1; i++)
                    {
                        if (nRowHeader > 0)
                        {
                            i = 0;
                            dsFile.Tables[0].Rows.RemoveAt(i);
                            nRowHeader = nRowHeader - 1;
                        }
                        else
                            break;
                    }
                }
                Reader.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        #endregion

        #region Reset
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtFileUpld.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
    }
}
