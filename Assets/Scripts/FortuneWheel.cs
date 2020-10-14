using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class FortuneWheel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform _cookieTransform;
    [SerializeField] private AudioSource _audioSource;

    private float _yAxisEndValue = -667f;
    private float _minAngle = 480f;
    private float _maxAngle = 1024f;
    private float _volume;

    private void Start()
    {
        _volume = _audioSource.volume;
    }

    private void DropCookie(float time)
    {
        _cookieTransform.DOAnchorPosY(_yAxisEndValue, 1).SetDelay(time);
    }

    private void SpinWheel(float time)
    {
        transform.DORotate(new Vector3(0, 0, Random.Range(_minAngle, _maxAngle)), time, RotateMode.FastBeyond360);
        PlaySoundOfWheel(time);
    }

    private void PlaySoundOfWheel(float time)
    {
        _audioSource.Play();
        _audioSource.DOFade(0, time);
        _audioSource.volume = _volume;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        float motionTime = Random.Range(2, 5);

        PlayerPrefs.SetInt("IsTaugth", 0);
        SpinWheel(motionTime);
        DropCookie(motionTime);
        enabled = false;
    }
}
