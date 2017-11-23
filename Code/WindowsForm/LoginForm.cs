using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForm.UC;

namespace WindowsForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            LoadDefault();
        }
        public void LoadDefault()
        {
            if (!panelControl.Contains(UCLoginWithCamera.Instance))
            {
                panelControl.Controls.Add(UCLoginWithCamera.Instance);
                UCLoginWithCamera.Instance.Dock = DockStyle.Fill;
                UCLoginWithCamera.Instance.BringToFront();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //panelControl.Controls.Clear();
            if (!panelControl.Contains(UCLoginWithPassword.Instance))
            {
                panelControl.Controls.Clear();
                panelControl.Controls.Add(UCLoginWithPassword.Instance);
                UCLoginWithPassword.Instance.Dock = DockStyle.Fill;
                UCLoginWithPassword.Instance.BringToFront();
            }
            else
            {
                panelControl.Controls.Clear();
                panelControl.Controls.Add(UCLoginWithCamera.Instance);
                UCLoginWithCamera.Instance.Dock = DockStyle.Fill;
                UCLoginWithCamera.Instance.BringToFront();
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
