using AppForm.HttpServers;
using AppForm.Storage;
using System;
using System.Windows.Forms;

namespace AppForm
{
    public partial class Form1 : Form
    {
        private HttpServer _httpServer;

        public Form1()
        {
            InitializeComponent();
            _httpServer = new HttpServer("http://localhost:18018/");
            _httpServer.Start();

            ChromeCache.Url = string.Empty;
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnConsultar");
            Console.WriteLine($"Chrome URL atual: {ChromeCache.Url}");
        }
    }
}

