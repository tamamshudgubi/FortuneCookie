using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
class DataSaver
{
    public List<string> CollectedPredictions;
    public List<string> Predictions;

    public List<string> GetPredictions()
    {
        TextAsset predictions = Resources.Load("predictions") as TextAsset;
        string jsonString = predictions.text;

        return JsonUtility.FromJson<DataSaver>(jsonString).Predictions;
    }

    public List<string> GetCollectedPredictions()
    {
        return JsonUtility.FromJson<DataSaver>(PlayerPrefs.GetString("SaveCollectedPredictions")).CollectedPredictions;
    }

    public void SaveData(DataSaver dataSaver)
    {
        PlayerPrefs.SetString("SaveCollectedPredictions", JsonUtility.ToJson(dataSaver));
    }
}
