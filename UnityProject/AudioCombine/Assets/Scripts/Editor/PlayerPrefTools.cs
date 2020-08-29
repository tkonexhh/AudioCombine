using System.Collections;
using System.Collections.Generic;
using System.IO;
using Qarth;
using UnityEngine;
using UnityEditor;

public class PlayerPrefTools
{
    [MenuItem("PlayerPref Tools/Clear Saved Data")]
    static public void ClearSavedData()
    {
        PlayerPrefs.DeleteAll();
    }
    // [MenuItem("PlayerPref Tools/Clear Serialized Data")]
    // static public void ClearFileData()
    // {
    //     bool isHaveData = Directory.Exists(Application.persistentDataPath + "/cache");
    //     if (isHaveData)
    //     {
    //         Directory.Delete(Application.persistentDataPath + "/cache", true);
    //     }
    // }
}
