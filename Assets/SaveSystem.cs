using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string filePath =>
        Path.Combine(Application.persistentDataPath, "save.json");

    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(filePath, json);
    }

    public static SaveData Load()
    {
        if (!File.Exists(filePath))
            return null;

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static bool SaveExists()
    {
        return File.Exists(filePath);
    }

    public static void DeleteSave()
    {
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}

