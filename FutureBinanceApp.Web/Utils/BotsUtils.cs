using System.Diagnostics;

namespace FutureBinanceApp.Web.Utils
{
    public static class BotsUtils
    {
        public static Process RunBotExe(string FilePath)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = FilePath;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            return process;
        }

        public static void KillAllBots(string ProcessName)
        {
            Process[] process = Process.GetProcessesByName(ProcessName);
            foreach (var item in process)
            {
                item.Kill();
            }
        }

        public static void KillBotForId(int id)
        {
            if (Process.GetProcessById(id) != null)
            {
                Process process = Process.GetProcessById(id);
                process.Kill();
            }
        }
    }
}
