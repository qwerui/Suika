using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONParser
{
    public static void SaveJSON<T>(string path, T instance) where T : class
    {
        string json = JsonUtility.ToJson(instance);
        File.WriteAllTextAsync(path, json);
    }

    public static T ReadJSON<T>(string path) where T : class
    {
        if(!File.Exists(path))
        {
            return null;
        }
        string json = File.ReadAllTextAsync(path)?.Result;
        return JsonUtility.FromJson<T>(json);
    }
}
