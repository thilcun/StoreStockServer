using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConfereEstoque
{
    public static class Servidor
    {
        private static IDisposable _webapp;
        private static bool isRunning = false;
        public static void Start(string ip)
        {
            if (!isRunning)
            {
                isRunning = true;
                _webapp = WebApp.Start<Startup>("http://" + ip + ":8080/");
            }
        }
        public static void Finish()
        {
            if (isRunning)
            {
                isRunning = false;
                _webapp.Dispose();
            }
        }
        public static bool IsRunning()
        {
            return isRunning;
        }
    }
}
