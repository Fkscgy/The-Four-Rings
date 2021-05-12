using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class gui : MonoBehaviour
{
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
        }
    }
    public void ClearLog() //you can copy/paste this code to the bottom of your script
{
    var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
    var type = assembly.GetType("UnityEditor.LogEntries");
    var method = type.GetMethod("Clear");
    method.Invoke(new object(), null);
}
}
