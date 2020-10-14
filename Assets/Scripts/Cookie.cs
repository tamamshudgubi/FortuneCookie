using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(RectTransform))]
public class Cookie : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    private DataSaver _dataSaver = new DataSaver();
    private RectTransform _rectTransform;
    private float _yAxisEndValue = 1653f;

    public event Action<string> Destroyed;


    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _dataSaver.Predictions = _dataSaver.GetPredictions();
    }

    public void SetUpCookie()
    {
        _rectTransform.DOAnchorPosY(_yAxisEndValue, 1);
    }

    public void SetShowPredictiobCondition()
    {
        if (Convert.ToBoolean(PlayerPrefs.GetInt("IsTriesEnded")) == false)
        {
            string predictionText = _dataSaver.Predictions.ElementAt(UnityEngine.Random.Range(0, _dataSaver.Predictions.Count()));

            Destroyed?.Invoke(predictionText);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        enabled = false;
        _audioSource.Play();
        _animator.SetBool("IsDestroyed", true);
    }
}


