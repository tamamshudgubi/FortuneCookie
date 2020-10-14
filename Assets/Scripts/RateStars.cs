using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateStars : MonoBehaviour
{
    [SerializeField] private List<Star> _rateStars;
    [SerializeField] private Sprite _filledStarTemplate;
    [SerializeField] private GameObject _nowButton;
    [SerializeField] private GameObject _laterButton;

    private void OnEnable()
    {
        foreach (var star in _rateStars)
        {
            star.Rated += Rate;
        }
    }

    private void OnDisable()
    {
        foreach (var star in _rateStars)
        {
            star.Rated -= Rate;
        }
    }

    private void Rate(int starNumber)
    {
        foreach (var star in _rateStars)
        {
            star.enabled = false;
        }

        FillRateStars(starNumber);
        _nowButton.SetActive(true);
        _laterButton.SetActive(true);
    }

    private void FillRateStars(int number)
    {
        for (int i = 0; i < number; i++)
        {
            _rateStars[i].GetComponent<Image>().sprite = _filledStarTemplate;
        }
    }
}
