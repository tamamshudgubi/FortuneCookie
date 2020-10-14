using System;
using TMPro;
using UnityEngine;


public class EnergyRestorer : MonoBehaviour
{
    [SerializeField] private PredictionReflector _predictionReflector;
    [SerializeField] private Buttons _buttons;
    [SerializeField] private TMP_Text _restoreText;

    public event Action RestoreFinished;


    private void OnEnable()
    {
        _predictionReflector.TriesEnded += OnTriesEnded;
    }

    private void OnDisable()
    {
        _predictionReflector.TriesEnded -= OnTriesEnded;
    }

    private void Update()
    {
        RestoreTimer();
    }

    public void OnTriesEnded()
    {
        _buttons.ActivateTriesEndedAnnouncer();
        PlayerPrefs.SetInt("IsTriesEnded", 1);
        PlayerPrefs.SetString("Time", DateTime.Now.AddHours(3).ToString());
    }

    private void RestoreTimer()
    {
        if (Convert.ToBoolean(PlayerPrefs.GetInt("IsTriesEnded", 1)))
        {
            if (CheckTimerEnds())
            {
                TimeSpan timeleft = DateTime.Parse(PlayerPrefs.GetString("Time")) - DateTime.Now;
                _restoreText.text = string.Format("{0}:{1}:{2}", timeleft.Hours.ToString("00"), timeleft.Minutes.ToString("00"), timeleft.Seconds.ToString("00"));
            }
        }
    } 

    private bool CheckTimerEnds()
    {
        if (DateTime.Parse(PlayerPrefs.GetString("Time")) - DateTime.Now <= TimeSpan.Zero)
        {
            PlayerPrefs.SetInt("IsTriesEnded", 0);
            PlayerPrefs.SetString("Time", TimeSpan.Zero.ToString());
            RestoreFinished?.Invoke();

            return false;
        }
        else
        {
            return true;
        }
    }
}

