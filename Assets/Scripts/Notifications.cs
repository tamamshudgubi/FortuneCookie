using System;
using Unity.Notifications.Android;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    private void Start()
    {
        CreateNotification();
    }

    private void CreateNotification()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel() { Id = "channel_id", Name = "Default Channel", Importance = Importance.High, Description = "Generic notifications" };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(DateTime time)
    {
        var notification = new AndroidNotification();

        notification.Title = "Новые печеньки готовы!";
        notification.Text = "Скорее!Спешите за предсказаниями!";
        notification.LargeIcon = "icon";
        notification.FireTime = time;

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (TimeSpan.Parse(PlayerPrefs.GetString("Time")) != TimeSpan.Zero)
            {
                SendNotification(Convert.ToDateTime(DateTime.Parse(PlayerPrefs.GetString("Time")) - DateTime.Now));
            }
            else
            {
                SendNotification(DateTime.Now.AddHours(6));
            }
        }
    }
}
