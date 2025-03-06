using UnityEngine;
using System.IO;
using System.Collections;

public class ScreenshotManager : MonoBehaviour
{
    private string screenshotPath;

    void Start()
    {
        screenshotPath = Path.Combine(Application.dataPath, "Screenshots");

        if (!Directory.Exists(screenshotPath))
        {
            Directory.CreateDirectory(screenshotPath);
        }
    }

    [ContextMenu("Screenshot")]
    public void TakeScreenshot()
    {
        string fileName = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string fullPath = Path.Combine(screenshotPath, fileName);

        ScreenCapture.CaptureScreenshot(fullPath);
        Debug.Log("Screenshot saved: " + fullPath);
    }
}