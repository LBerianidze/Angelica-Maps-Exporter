using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapFilesImporter
{
    static class Program
    {
        private static string SendRequest(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    return client.DownloadString(new Uri(url));
                }

            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 mainform = new Form1();
            Application.Run(mainform);
        }
        static void DoTask(object ct)
        {
            var cancelTok = (CancellationToken)ct;
            LoadingForm fm = new LoadingForm(cancelTok);
            Application.Run(fm);
        }
    }
    }
