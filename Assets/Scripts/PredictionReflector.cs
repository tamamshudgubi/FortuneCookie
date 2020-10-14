using UnityEngine;
using TMPro;
using System;


public class PredictionReflector : MonoBehaviour
{
    [SerializeField] private Buttons _buttons;
    [SerializeField] private TMP_Text _predictionText;
    [SerializeField] private Cookie _cookie;
    [SerializeField] private EnergyRestorer _energyRestorer;

    private int _maxTries = 3;
    private int _initTries = 0;

    public event Action TriesEnded;


    private void OnEnable()
    {
        _cookie.Destroyed += OnDestroyed;
        _energyRestorer.RestoreFinished += OnRestoreFinished;
    }

    private void OnDisable()
    {
        _cookie.Destroyed -= OnDestroyed;
        _energyRestorer.RestoreFinished -= OnRestoreFinished;
    }

    private void OnRestoreFinished()
    {
        PlayerPrefs.SetInt("Tries", _initTries);
        _buttons.TriesRestored();
    }

    public void OnDestroyed(string predictionText)
    {
        _predictionText.text = predictionText;
        PlayerPrefs.SetString("LastPrediction", predictionText);

        int currentTries = PlayerPrefs.GetInt("Tries");
        PlayerPrefs.SetInt("Tries", ++currentTries);

        if (PlayerPrefs.GetInt("Tries") >= _maxTries)
        {
            TriesEnded?.Invoke();
        }
        else
        {
            _buttons.ActivateAllButtons();
        }
    }
}
