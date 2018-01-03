using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThiToeic
{
    public static class Global
    {
        public static string SEVER_LOCAL_URL = "http://localhost:51989";
        public static string SEVER_VPS_URL = "http://dangkythitoeic.com";
        public static string WATING_URL = "de-thi/doi";
        public static void ShowWaitForm(Form form)
        {
            SplashScreenManager.ShowForm(form, typeof(WaitForm1), true, true, false, ParentFormState.Locked);

            //The Wait Form is opened in a separate thread. To change its Description, use the SetWaitFormDescription method.
            for (int i = 1; i <= 100; i++)
            {
                SplashScreenManager.Default.SetWaitFormDescription(i.ToString() + "%");
                Random random = new Random();
                int sleepRandom = random.Next(20, 50);
                Thread.Sleep(sleepRandom);
            }

            //Close Wait Form
            SplashScreenManager.CloseForm();
        }
    }
}
