using System;
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
    public partial class Menus : Form
    {
        public Menus()
        {
            InitializeComponent();
        }

       

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {               
                DepartmentMaster objDept = new DepartmentMaster();
                objDept.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Menus_Load(object sender, EventArgs e)
        {
            try
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Maximized;

                this.BackgroundImage = Properties.Resources.Placement;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void departmentMasterUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DepartmentMastUpload objDeptUpld = new DepartmentMastUpload();
                objDeptUpld.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void depattmentEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DeptMasterEdit objDeptEdit = new DeptMasterEdit();
                objDeptEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
