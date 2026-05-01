using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class APIManager : MonoBehaviour
{
    [Header("JSONBin Settings")]
    public string url;
    public string apiKey;

    // =========================
    // SAVE DECK
    // =========================
    public IEnumerator SaveDeck(string userId, List<string> newDeck)
{
    SaveData currentData = null;

    // 1. LOAD existing data
    yield return StartCoroutine(LoadDecks((data) =>
    {
        currentData = data;
    }));

    // 2. If no data exists -> create new
    if (currentData == null)
    {
        currentData = new SaveData();
        currentData.user_id = userId;
        currentData.decks = new List<List<string>>();
    }

    // 3. Ensure correct user
    if (currentData.user_id != userId)
    {
        currentData.user_id = userId;
        currentData.decks = new List<List<string>>();
    }

    // 4. Ensure decks list exists
    if (currentData.decks == null)
    {
        currentData.decks = new List<List<string>>();
    }

    // 5. APPEND NEW DECK 
    currentData.decks.Add(newDeck);

    // 6. SAVE FULL DATA BACK
    string json = JsonConvert.SerializeObject(currentData);

    UnityWebRequest req = new UnityWebRequest(url, "PUT");
    req.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
    req.downloadHandler = new DownloadHandlerBuffer();

    req.SetRequestHeader("Content-Type", "application/json");
    req.SetRequestHeader("X-Master-Key", apiKey);

    yield return req.SendWebRequest();

    if (req.result != UnityWebRequest.Result.Success)
    {
        Debug.LogError("Save failed: " + req.error);
    }
    else
    {
        Debug.Log("Deck appended successfully!");
    }
}

    // =========================
    // LOAD DECKS
    // =========================
    public IEnumerator LoadDecks(Action<SaveData> callback)
{
    UnityWebRequest req = UnityWebRequest.Get(url);
    req.SetRequestHeader("X-Master-Key", apiKey);

    yield return req.SendWebRequest();

    if (req.result != UnityWebRequest.Result.Success)
    {
        Debug.LogError("Load failed: " + req.error);
        callback?.Invoke(null);
        yield break;
    }

    string json = req.downloadHandler.text;
    Debug.Log("RAW JSON: " + json);

    try
    {
        // First try wrapper format
        JsonBinWrapper wrapper = JsonConvert.DeserializeObject<JsonBinWrapper>(json);

        if (wrapper != null && wrapper.record != null)
        {
            callback?.Invoke(wrapper.record);
            yield break;
        }

        // fallback: direct format
        SaveData direct = JsonConvert.DeserializeObject<SaveData>(json);
        callback?.Invoke(direct);
    }
    catch (Exception e)
    {
        Debug.LogError("Parse error: " + e.Message);
        callback?.Invoke(null);
    }
}
    // =========================
    // DATA MODELS
    // =========================
    [Serializable]
    public class SaveData
    {
        public string user_id;
        public List<List<string>> decks = new List<List<string>>();
    }

    [Serializable]
    public class JsonBinWrapper
    {
        public SaveData record;
    }
}