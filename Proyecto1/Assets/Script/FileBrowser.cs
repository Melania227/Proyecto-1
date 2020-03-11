using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class FileBrowser : MonoBehaviour
{
    public string Path;
    public InputField field;

    public void OpenExplorer() {
        Path = EditorUtility.OpenFilePanel("Choose a file.", "", "txt");
        field.text = Path;
    }
        
}
