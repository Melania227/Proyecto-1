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
        if (Path == "" || Path == null)
        {
            Path = EditorUtility.OpenFilePanel("Choose a file.", "", "txt");
            field.text = Path;
        }
        else {
            EditorUtility.DisplayDialog("Hey!", "The path has already been added. To change it, select reset.", "Ok");
        }
    }
        
}
