using Sirenix.OdinInspector;

namespace IAP
{
  public class IAPItemNonConsumable : IAPItem
  {
    private bool isBuy;
    protected override void InitBtnBuy(string productId)
    {
      base.InitBtnBuy(productId);
      isBuy = IsBuy();
      SetStatusBtnBuy(isBuy);
    }

    [Button]
    private void SetStatusBtnBuy(bool isBuy)
    {
      txtBuy.text = isBuy ? "Purchased" : "Buy";
      btnIAP.interactable = !isBuy;
      UnityEngine.Debug.Log($"IAPItemNonConsumable isBuy - {isBuy}");
    }

    protected override void OnPurchaseSuccess(string productId)
    {
      if (this.model.productId == productId)
      {
        isBuy = true;
        SetStatusBtnBuy(isBuy);
        // InitIAPItem();
        base.OnPurchaseSuccess(productId);
      }
    }

    private bool IsBuy()
    {
      return product.hasReceipt;
    }
  }
}
