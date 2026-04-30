using UnityEditor.Build.Reporting;
using UnityEngine;

///<summary>
/// Static utility for handling UUID via PlayerPrefs.
///</summary>
public class UUIDManager
{
    const string KEY = "USER_UUID";//the key to store and retrieve UUID from local storage

    //Func to generate a new universal UUID, save it, and return it (Call when 'New User' btn is clicked)
    public static string Create()
    {
        string id = System.Guid.NewGuid().ToString();
        PlayerPrefs.SetString(KEY, id);
        PlayerPrefs.Save();//Force a save to disk immediately
        return id;
    }
    
    //Func to check if a UUID has already been saved to this device; toggle 'Continue' btn if it does
    public static bool Exists()
    {
        return PlayerPrefs.HasKey(KEY);
    }

    //Func to retrieve the saved UUID
    public static string Get()
    {
        return PlayerPrefs.GetString(KEY, string.Empty);
    }
}