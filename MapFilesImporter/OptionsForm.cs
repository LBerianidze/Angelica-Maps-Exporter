using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MapFilesImporter
{
    public partial class OptionsForm : Form
    {
        public OptionsForm(Form1 f1)
        {
            MainForm = f1;
            InitializeComponent();
        }
        Form1 MainForm;
        private void MapsButton_Click(object sender, EventArgs e)
        {
            if (Browser.ShowDialog() == DialogResult.OK)
            {
                ElementsTextBox.Text = Browser.SelectedPath;
            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            if (CheckPath() == true)
            {
                MainForm.ElementPath = ElementsTextBox.Text;
                switch (GamesCombobox.SelectedIndex)
                {
                    case 0: MainForm.ChosenGame = Game.Pw; break;
                    case 1: MainForm.ChosenGame = Game.Fw; break;
                    case 2: MainForm.ChosenGame = Game.Jd; break;
                    case 3: MainForm.ChosenGame = Game.Loma; break;
                    case 4: MainForm.ChosenGame = Game.HoTK; break;
                    case 5: MainForm.ChosenGame = Game.Eso; break;
                    default:
                        MessageBox.Show("Вы не выбрали игру.Возможно возникнут некоторые проблемы в ходе операции.", "MapFilesImporter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
                MainForm.ModifePaths();
                MainForm.RefreshCombobox();
                this.Hide();
                MainForm.FromPck = radioButton1.Checked ? true : false;
                MainForm.LoadPcks(ElementsTextBox.Text);
            }
            else
            {
                MessageBox.Show("Указан неверный путь!!...", "MapFilesImporter", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public bool CheckPath()
        {
            if (!string.IsNullOrWhiteSpace(ElementsTextBox.Text))
            {
                if (Directory.Exists(ElementsTextBox.Text))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Путь не указан!!...", "MapFilesImporter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        public void RefreshInfo(string M, Game gm)
        {
            ElementsTextBox.Text = M;
            GamesCombobox.SelectedIndex = (int)gm;
        }
    }
}
