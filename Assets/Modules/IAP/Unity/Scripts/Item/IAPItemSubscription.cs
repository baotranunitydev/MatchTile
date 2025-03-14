using System;
using System.Threading;
using IAP.Utils;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using Sirenix.OdinInspector;

namespace IAP
{
  public class IAPItemSubscription : IAPItem
  {
    [SerializeField] private TextMeshProUGUI txtCountDown;
    private DateTime subscriptionEndDate;
    private CancellationTokenSource cancellationTokenItemSubscription;

    private void CancelToken()
    {
      if (cancellationTokenItemSubscription != null)
      {
        cancellationTokenItemSubscription.Cancel();
        cancellationTokenItemSubscription.Dispose();
        cancellationTokenItemSubscription = null;
      }
    }
    protected override void InitBtnBuy(string productId)
    {
      base.InitBtnBuy(productId);
      var isSubscript = IsSubscript(product.receipt);
      if (isSubscript)
      {
        CountDown();
      }
      SetStatusBtnIsBuy(isSubscript);
    }

    protected override void OnDestroy()
    {
      CancelToken();
      base.OnDestroy();
    }

    protected override void OnPurchaseSuccess(string productId)
    {
      // subscriptionEndDate = DateTime.UtcNow.AddDays(model.value);
      // SetStatusBtnIsBuy(true);
      // CountDown();
      if (this.model.productId == productId)
      {
        InitIAPItem();
        base.OnPurchaseSuccess(productId);
      }
    }


    [Button]
    private void SetStatusBtnIsBuy(bool isSubscript)
    {
      txtBuy.text = isSubscript ? "Subscripted" : "Buy";
      btnIAP.interactable = !isSubscript;
      UnityEngine.Debug.Log($"IAPItemSubscription isSubscript - {isSubscript}");
    }

    private void CountDown()
    {
      CancelToken();
      cancellationTokenItemSubscription = new CancellationTokenSource();
      var remainTime = DateTime.UtcNow - subscriptionEndDate;
      var totalSecond = (int)remainTime.TotalSeconds;
      IAPUtils.CountDownTimeSubscription(totalSecond, (time) =>
      {
        if (time < 0) return;
        if (time > 0)
        {
          txtCountDown.text = IAPUtils.FormatTimeRemaining(totalSecond);
        }
        else
        {
          SetStatusBtnIsBuy(false);
        }
      }, cancellationTokenItemSubscription.Token).Forget();
    }

    private bool IsSubscript(string receipt)
    {
      bool IsSubscript = false;
      if (!string.IsNullOrEmpty(receipt))
      {
        subscriptionEndDate = GetExpireDay(model.productId, receipt);
        IsSubscript = subscriptionEndDate > DateTime.UtcNow;
      }
      return IsSubscript;
    }

    private DateTime GetExpireDay(string productId, string receipt)
    {
      try
      {
        SubscriptionManager p = new SubscriptionManager(product, receipt);
        SubscriptionInfo info = p.getSubscriptionInfo();
        Debug.Log($"Subscription info: \n" +
                  $"id: {info.getProductId()} \n" +
                  $"Purchase date: {info.getPurchaseDate()} \n" +
                  $"IsSubscribed: {info.isSubscribed()} \n" +
                  $"IsExpired: {info.isExpired()} \n" +
                  $"IsCancelled: {info.isCancelled()} \n" +
                  $"ExpireDate: {info.getExpireDate()} \n" +
                  $"FreeTrialPeriod: {info.getFreeTrialPeriod()} \n" +
                  $"FreeTrialPeriodString: {info.getFreeTrialPeriodString()} \n");
        return info.getExpireDate();
      }
      catch (Exception error)
      {
        Debug.Log($"GetExpireDay - {error}");
        return DateTime.UtcNow;
      }
    }

    private DateTime GetSubscriptionEndDate(string receipt)
    {
      try
      {
        var receiptData = JObject.Parse(receipt);

#if UNITY_ANDROID
        string payload = receiptData["Payload"].ToString();
        var payloadData = JObject.Parse(payload);

        // On Android, we get the purchase time, and we need to calculate the end date
        // based on the subscription period (1 month, 1 year, etc.)
        string purchaseTimeMillis = payloadData["purchaseTime"].ToString();
        long timestamp = long.Parse(purchaseTimeMillis);
        DateTime purchaseDate = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;

        // Determine subscription length (this is simplified)
        // In a real app, you'd get the subscription period from Google Play Developer API
        return purchaseDate.AddMonths(1); // Assuming 1-month subscription

#elif UNITY_IOS
                    string latestReceipt = receiptData["Payload"].ToString();
                    var receiptJson = JObject.Parse(latestReceipt);
                    
                    // On iOS, we can get the expiration date from the receipt
                    string expirationDateString = receiptJson["expires_date"].ToString();
                    return DateTime.Parse(expirationDateString);
#else
                    Debug.LogError("Unsupported platform for subscriptions");
                    return DateTime.MinValue;
#endif
      }
      catch (Exception e)
      {
        Debug.LogError($"Error parsing subscription date: {e.Message}");
        return DateTime.MinValue;
      }
    }
  }
}
