using UnityEditor;
using UnityEngine;

public class CommandLine : EditorWindow
{
    [MenuItem("Window/Command Line Window")]
    static void Init()
    {
        CommandLine window = (CommandLine)EditorWindow.GetWindow(typeof(CommandLine));
        window.Show();
    }

    void OnGUI()
    {
        // Komut satırı giriş alanı oluşturun
        string command = "";
        command = EditorGUILayout.TextField("Enter Command:", command);

        // Komutu çalıştırmak için bir düğme oluşturun
        if (GUILayout.Button("Run Command"))
        {
            // Komut satırını proje dizininde çalıştırın
            System.Diagnostics.Process.Start("cmd.exe", "/c " + command);
        }
    }
}