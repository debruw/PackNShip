using UnityEditor;
using UnityEngine;

namespace QuantumForgeStudio.EasyScreenshotTool
{
    [InitializeOnLoad]
    public class ScreenshotButtonEditor
    {
        static ScreenshotButtonEditor()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();
            GUIStyle buttonStyle = new GUIStyle("Command");
            if (GUILayout.Button(EditorGUIUtility.IconContent("Camera Icon"), buttonStyle, GUILayout.Width(32)))
            {
                TakeScreenshot();
            }
        }

        private static void TakeScreenshot()
        {
            string path = $"{Application.dataPath}/Screenshots/screenshot_{System.DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
            System.IO.Directory.CreateDirectory($"{Application.dataPath}/Screenshots");

            if (Application.isPlaying)
            {
                ScreenCapture.CaptureScreenshot(path, 1);
                Debug.Log($"Screenshot taken and saved at: {path}");
            }
            else
            {
                CaptureCameraScreenshot(path);
            }
        }

        private static void CaptureCameraScreenshot(string path)
        {
            Camera camera = Camera.main;

            if (camera != null)
            {
                int width = 1920;
                int height = 1080;

                RenderTexture rt = new RenderTexture(width, height, 24);
                camera.targetTexture = rt;

                RenderTexture.active = rt;
                camera.Render();

                Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);
                screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
                screenShot.Apply();

                camera.targetTexture = null;
                RenderTexture.active = null;

                Object.DestroyImmediate(rt);

                byte[] bytes = screenShot.EncodeToPNG();
                System.IO.File.WriteAllBytes(path, bytes);
                AssetDatabase.Refresh();
                Debug.Log($"Screenshot of the main camera taken and saved at: {path}");
            }
            else
            {
                Debug.LogWarning("No main camera found to capture the screenshot.");
            }
        }
    }

}

