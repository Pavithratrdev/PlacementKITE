using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

namespace KITEPlacement
{
    public partial class DepartmentMaster : Form
    {
        #region Constructor
        public DepartmentMaster()
        {
            InitializeComponent();
        }
        #endregion

        #region Declarations

        private ArrayList objParams = new ArrayList();
        private ArrayList objValues = new ArrayList();

        #endregion

        #region Method to Clear the Controls
        private void ClearControls()
        {
            txtDeptCode.Text = "";
            txtDeptName.Text = "";
        }
        #endregion

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                objParams.Clear();
                objValues.Clear();

                string strout = "Successful";
                objParams = CommonMethods.CreateArrayList(new object[] { "@pMode", "@pDeptCode", "@pDeptName" });
                objValues = CommonMethods.CreateArrayList(new object[] { "ADD", txtDeptCode.Text, txtDeptName.Text });
                ConnectionString.SqlExecInsProc("Pr_Dept_Det", objParams, objValues, out strout);
                if (strout != "Successful")
                {
                    MessageBox.Show(strout, "Error", MessageBoxButtons.OK);
                    return;
                }
                else
                    MessageBox.Show("Records Saved Successfully");

                ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Form Load
        private void DepartmentMaster_Load(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

       
    }
}
