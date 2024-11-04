using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class MetaManager : MonoBehaviour
{
    public Text text;
    public bool canRunBatch = false;

    // Start is called before the first frame update
    void Start()
    {
        text.text = File.Exists(Path.Combine(Application.dataPath, "file.bat")) ? "yes" : "no";
        if (File.Exists(Path.Combine(Application.dataPath, "file.bat")))
        {
            canRunBatch = true;
        }
    }

    private void OnApplicationQuit()
    {
        if (canRunBatch)
        {
            RunBatchFile();
        }
    }

    void RunBatchFile()
    {
        Process process = new Process();
        string path = Application.dataPath;
        string batchFilePath = Path.Combine(path, "file.bat");

        process.StartInfo.FileName = batchFilePath;
        process.StartInfo.WorkingDirectory = path;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        int exitCode = -1;
        string output = null;

        try
        {
            process.Start();
            output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            text.text = "Everything went alright";
        }
        catch (System.Exception e)
        {
            text.text = e.Message;
        }
        finally
        {
            exitCode = process.ExitCode;

            process.Dispose();
            process = null;
        }
    }
}
