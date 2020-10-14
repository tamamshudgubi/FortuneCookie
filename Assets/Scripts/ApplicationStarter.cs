using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ApplicationStarter : MonoBehaviour
{
    [SerializeField] private Buttons _buttons;
    [SerializeField] private TMP_Text _predictionText;
    [SerializeField] private Animator _animator;
    [SerializeField] private RectTransform _cookie;
    [SerializeField] private Material _wheel;

    private List<string> _dataKeys = new List<string>() { "SaveCollectedPredictions", "LastPrediction", "Tries", "Time", "IsTriesEnded", "LastSharedTime", "IsRated", "DaysInARaw", "LastDayInApp", "IsTaugth" };
    private DataSaver _dataSaver = new DataSaver();
    private float _yAxisEndValue = -667f;


    private void Awake()
    {
        //PlayerPrefs.DeleteAll();

        if (!_dataKeys.Any(data => PlayerPrefs.HasKey(data)))
        {
            InitializeDataKeys();
        }

        if (Convert.ToBoolean(PlayerPrefs.GetInt("IsTriesEnded")))
        {
            SetWaitingforRestoreCondition();
        }
    }

    private void Start()
    {
        ShowWhatClickAsync();
    }

    private void InitializeDataKeys()
    {
        PlayerPrefs.SetString("LastPrediction", "");
        PlayerPrefs.SetString("SaveCollectedPredictions", JsonUtility.ToJson(_dataSaver));
        PlayerPrefs.SetInt("Tries", 0);
        PlayerPrefs.SetString("Time", DateTime.Now.ToString());
        PlayerPrefs.SetInt("IsTriesEnded", 0);
        PlayerPrefs.SetString("LastSharedTime", DateTime.Now.ToString());
        PlayerPrefs.SetInt("IsRated", 0);
        PlayerPrefs.SetInt("DaysInARaw", 0);
        PlayerPrefs.SetString("LastDayInApp", DateTime.Now.ToString());
        PlayerPrefs.SetInt("IsTaugth", 1);
    }

    private void SetWaitingforRestoreCondition()
    {
        _animator.SetBool("IsDestroyed", true);
        _predictionText.text = PlayerPrefs.GetString("LastPrediction");
        _buttons.ActivateTriesEndedAnnouncer();
        _cookie.DOAnchorPosY(_yAxisEndValue, 0);
    }

    private async void ShowWhatClickAsync()
    {
        while (Convert.ToBoolean(PlayerPrefs.GetInt("IsTaugth")))
        {
            ShowWhatClick();
            await Task.Yield();            
        }

        StopShowingWhatClick();
    }

    private void ShowWhatClick()
    {
        if (_wheel.GetFloat("Thickness") == 0.002f)
        {
            _wheel.DOFloat(0.005f, "Thickness", 1);
        }
        else if (_wheel.GetFloat("Thickness") == 0.005f)
        {
            _wheel.DOFloat(0.002f, "Thickness", 1);
        }
    }

    private void StopShowingWhatClick()
    {
        _wheel.DOFloat(0, "Thickness", 0);
    }
}


