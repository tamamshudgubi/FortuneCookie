using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class DailyBonus : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<Reward> _rewards = new List<Reward>();

    private int[] _rewardsCost = new int[6] { 1, 2, 3, 4, 5, 6 };

    private void Start()
    {
        if (CheckDailyBonusAvailability())
        {
            CreateDailyBonusPanel();
        }
    }

    private void CreateDailyBonusPanel()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("DaysInARaw"); i++)
        {
            _rewards.ElementAt(i).SetTaken(true);
        }

        _animator.SetBool("IsBonusReady", true);
    }

    private void RefreshBonusPanel()
    {
        foreach (var reward in _rewards)
        {
            reward.SetTaken(false);
        }
    }

    private bool CheckDailyBonusAvailability()
    {
        int daysInARaw = PlayerPrefs.GetInt("DaysInARaw");
        DateTime currentDate = DateTime.Now;
        TimeSpan timeDifference = currentDate.Subtract(DateTime.Parse(PlayerPrefs.GetString("LastDayInApp")));

        if (timeDifference.Days >= 1 && timeDifference.Days < 2)
        {
            if (daysInARaw++ > _rewardsCost.Length)
            {
                PlayerPrefs.SetInt("DaysInARaw", _rewardsCost.Length);
            }
            else
            {
                PlayerPrefs.SetInt("DaysInARaw", daysInARaw);
            }

            return true;
        }
        else if (timeDifference.Days < 1)
        {
            return false;
        }
        else if (timeDifference.Days > 2)
        {
            PlayerPrefs.SetInt("DaysInARaw", 0);
            PlayerPrefs.SetString("LastDayInApp", DateTime.Now.ToString());
            RefreshBonusPanel();

            return false;
        }
        else
        {
            return false;
        }
    }

    public void ClaimReward()
    {
        int currentTries = PlayerPrefs.GetInt("Tries");
        int triesWithRewards = currentTries - _rewardsCost.ElementAt(PlayerPrefs.GetInt("DaysInARaw") - 1);

        PlayerPrefs.SetInt("Tries", triesWithRewards);
        PlayerPrefs.SetString("LastDayInApp", DateTime.Now.ToString());

        _animator.SetBool("IsBonusReady", false);
    }
}
