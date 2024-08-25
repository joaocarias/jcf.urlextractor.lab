using AppForm.Caches;
using AppForm.HttpServers;
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

            UrlExtractorCache.Url = string.Empty;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Console.WriteLine("btnConsultar");

            
        }
    }
}

