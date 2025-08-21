using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace DWinverNS
{

    public partial class CaD : Form
{
    public CaD()
    {
        InitializeComponent();
    }

    private void RunPowerShellScript()
    {
        //string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dwinver_VSResc.ps1");
        string scriptPath = Path.Combine(Path.GetTempPath(), "dwinver_VSResc.ps1");
        byte[] scriptContent = DWinver.Properties.Resources.DWinverPS;
        File.WriteAllBytes(scriptPath, scriptContent);

            if (!File.Exists(scriptPath))
        {
            MessageBox.Show("The main script was not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var psi = new ProcessStartInfo();
        psi.FileName = "powershell.exe";
        psi.Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\"";
        psi.UseShellExecute = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;
            if (ArgsMgr.debugMode)
            {
                psi.CreateNoWindow = false;
            }
            else
            {
                psi.CreateNoWindow = true;
            }

        Process psProcess = new Process();
        psProcess.StartInfo = psi;
        psProcess.OutputDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) Console.WriteLine(e.Data); };
        psProcess.ErrorDataReceived += (s, e) => { if (!string.IsNullOrEmpty(e.Data)) Console.WriteLine("ERROR: " + e.Data); };

        psProcess.Start();
        psProcess.BeginOutputReadLine();
        psProcess.BeginErrorReadLine();
    }

    private void DWinver_Load(object sender, EventArgs e)
    {
        RunPowerShellScript();
    }
}
}