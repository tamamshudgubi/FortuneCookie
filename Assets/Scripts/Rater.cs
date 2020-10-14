using System;
using UnityEngine;

public class Rater : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void RateRequest()
    {
        if (Convert.ToBoolean(PlayerPrefs.GetInt("IsRated")) == false)
        {
            _animator.SetBool("IsReadyToRate", true);
        }
    }

    public void OnRateNowButtonClick()
    {
        Application.OpenURL("market://details?id=com.IJunior.LuckyCookie&hl=ru");
        _animator.SetBool("IsReadyToRate", false);
        PlayerPrefs.SetInt("IsRated", 1);
    }

    public void OnRateLaterButtonClick()
    {
        _animator.SetBool("IsReadyToRate", false);
    }
}
