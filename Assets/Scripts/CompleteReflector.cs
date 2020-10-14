using UnityEngine;
using TMPro;
using System;

public class CompleteReflector : MonoBehaviour
{
    [SerializeField] private TMP_Text _collected;
    [SerializeField] private Cookie _cookie;
    [SerializeField] private Rater _rater;

    private DataSaver _dataSaver = new DataSaver();


    private void OnEnable()
    {
        _cookie.Destroyed += OnDestroyed;
    }

    private void OnDisable()
    {
        _cookie.Destroyed -= OnDestroyed;
    }

    private void Start()
    {
        _dataSaver.CollectedPredictions = _dataSaver.GetCollectedPredictions();
        _dataSaver.Predictions = _dataSaver.GetPredictions();
        SetCollectedPredictionsAmount();
    }

    private void OnDestroyed(string prediction)
    {
        if (_dataSaver.CollectedPredictions.Contains(prediction) == false)
        {
            _dataSaver.CollectedPredictions.Add(prediction);
            _collected.text = string.Format("{0} из {1}", _dataSaver.CollectedPredictions.Count.ToString(), _dataSaver.GetPredictions().Count);

            if (_dataSaver.CollectedPredictions.Count % 15 == 0 && _dataSaver.CollectedPredictions.Count != 0)
            {
                _rater.RateRequest();
            }
        }
    }

    private void SetCollectedPredictionsAmount()
    {
        _collected.text = string.Format("{0} из {1}", _dataSaver.CollectedPredictions.Count.ToString(), _dataSaver.GetPredictions().Count);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            _dataSaver.SaveData(_dataSaver);
        }
    }

    private void OnApplicationQuit()
    {
        _dataSaver.SaveData(_dataSaver);
    }
}
