using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MapFilesImporter
{
    public partial class LoadingForm : Form
    {
        private bool simulateError = false;
        CancellationToken tk;
        public LoadingForm(CancellationToken t)
        {
            InitializeComponent();
            tk = t;
            // set initial image
            this.pictureBox.Image = MapFilesImporter.Properties.Resources.Animation;
            // start background operation
            this.backgroundWorker.RunWorkerAsync();
        }
        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (this.backgroundWorker.CancellationPending || this.tk.IsCancellationRequested)
                {
                    try
                    {
                        tk.ThrowIfCancellationRequested();
                        return;
                    }
                    catch
                    {
                        return;
                    }
                }
                // report progress
                this.backgroundWorker.ReportProgress(-1, string.Format("Performing step {0}...", 1));
                // simulate operation step
                System.Threading.Thread.Sleep(200);
                if (this.simulateError)
                {
                    this.simulateError = false;
                    throw new Exception("Unexpected error!");
                }
            }
        }


        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // hide animation
            this.FormClosed -= LoadingForm_FormClosed;
            this.Close();
        }

        private void LoadingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
