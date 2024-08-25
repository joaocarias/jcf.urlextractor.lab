using AppForm.Client;
using AppForm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnConsultar");

            ChromeWebSocketClient client = new ChromeWebSocketClient();
            _ = client.GetActiveTabUrlAsync();
            
        }
    }
}
