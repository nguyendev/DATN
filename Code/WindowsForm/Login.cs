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
    public partial class Login : Form
    {
        public Login()
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
            else
                UCLoginWithCamera.Instance.BringToFront();
        }
    }
}
