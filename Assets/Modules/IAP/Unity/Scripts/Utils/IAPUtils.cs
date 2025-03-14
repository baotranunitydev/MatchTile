using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace IAP.Utils
{
  public static class IAPUtils
  {
    public static string FormatTimeRemaining(int totalSeconds)
    {
      TimeSpan remaining = TimeSpan.FromSeconds(totalSeconds);
      return FormatTimeRemaining(remaining);
    }

    public static string FormatTimeRemaining(TimeSpan remaining)
    {
      int years = remaining.Days / 365;
      int months = (remaining.Days % 365) / 30;
      int weeks = (remaining.Days % 30) / 7;
      int days = remaining.Days % 7;
      int hours = remaining.Hours;
      int minutes = remaining.Minutes;
      int seconds = remaining.Seconds;

      if (years > 0)
        return months > 0 ? $"{years:D2}y {months:D2}mo" : $"{years:D2}y";
      if (months > 0)
        return days > 0 ? $"{months:D2}mo {days:D2}d" : $"{months:D2}mo";
      if (weeks > 0)
        return days > 0 ? $"{weeks:D2}w {days:D2}d" : $"{weeks:D2}w";
      if (days > 0)
        return $"{days:D2}d";
      if (hours > 0)
        return minutes > 0 ? $"{hours:D2}h {minutes:D2}m" : $"{hours:D2}h";
      if (minutes > 0)
        return seconds > 0 ? $"{minutes:D2}m {seconds:D2}s" : $"{minutes:D2}m";
      return $"{seconds:D2}s";
    }

    public static async UniTask CountDownTimeSubscription(int duration, Action<int> onCountDown, CancellationToken cancellationTokenCountDown)
    {
      try
      {
        var timeCountDown = 1;
        var timer = duration;
        onCountDown?.Invoke(timer);
        while (timer > 0)
        {
          await UniTask.WaitForSeconds(timeCountDown, cancellationToken: cancellationTokenCountDown);
          timer -= timeCountDown;
          onCountDown?.Invoke(timer);
        }
        onCountDown?.Invoke(0);
      }
      catch (Exception err)
      {
        Debug.Log($"Cancel: {err}");
        onCountDown?.Invoke(-1);
      }
    }
  }
}