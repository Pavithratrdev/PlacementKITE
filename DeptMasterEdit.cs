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

namespace KITEPlacement
{
    public partial class DeptMasterEdit : Form
    {
        #region Constructor
        public DeptMasterEdit()
        {
            InitializeComponent();
        }
        #endregion

        #region Declaration
        private ArrayList objParams = new ArrayList();
        private ArrayList objValues = new ArrayList();
        #endregion

        #region Load Grid
        private void LoadGrid()
        {
            try
            {
                dgvDeptData.Columns[0].HeaderText = "Department ID";
                dgvDeptData.Columns[1].HeaderText = "Department Code";
                dgvDeptData.Columns[2].HeaderText = "Department Name";

                dgvDeptData.Columns[0].Width = 50;
                dgvDeptData.Columns[1].Width = 150;
                dgvDeptData.Columns[2].Width = 350;

                dgvDeptData.Columns[0].HeaderCell.Style.Font = new Font("Times New Roman", 10.75F, FontStyle.Bold);
                dgvDeptData.Columns[1].HeaderCell.Style.Font = new Font("Times New Roman", 10.75F, FontStyle.Bold);
                dgvDeptData.Columns[2].HeaderCell.Style.Font = new Font("Times New Roman", 10.75F, FontStyle.Bold);

                dgvDeptData.Columns[0].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Form Load
        private void DeptMasterEdit_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Method Loaddata
        private void LoadData()
        {
            try
            {
                ArrayList ObjParams = new ArrayList();
                ArrayList ObjValues = new ArrayList();
                String ErrMsg = "Successful";

                ObjParams = CommonMethods.CreateArrayList(new object[] { "@pMode", "@pDeptCode", "@pDeptName" });
                ObjValues = CommonMethods.CreateArrayList(new object[] { "SEL", "", "" });

                DataSet dsView = ConnectionString.SqlExecFetchDataSet("Pr_Dept_Det", ObjParams, ObjValues, out ErrMsg);

                if (ErrMsg != "Successful")
                    throw new Exception(ErrMsg);
                else
                {
                    if (dsView != null && dsView.Tables.Count > 0 && dsView.Tables[0].Rows.Count > 0)
                    {
                        dgvDeptData.DataSource = dsView.Tables[0];
                        LoadGrid();
                    }
                    else
                        MessageBox.Show("No Records Found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Cancel Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Save Click
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtData = new DataTable();
                dtData = (DataTable)dgvDeptData.DataSource;

                string strout = "Successful";
                ConnectionString.FnSqlBulkInsert("Dept_Edit_Stgn", dtData, out strout);
                if (strout != "Successful")
                {
                    MessageBox.Show(strout, "Error", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    strout = "Successful";
                    objParams = CommonMethods.CreateArrayList(new object[] { "@pMode","@pDeptCode", "@pDeptName" });
                    objValues = CommonMethods.CreateArrayList(new object[] { "UPD", "","" });
                    ConnectionString.SqlExecInsProc("Pr_Dept_Det", objParams, objValues, out strout);
                    if (strout != "Successful")
                    {
                        MessageBox.Show(strout, "Error", MessageBoxButtons.OK);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records Updated Successfully");
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
    }
}
