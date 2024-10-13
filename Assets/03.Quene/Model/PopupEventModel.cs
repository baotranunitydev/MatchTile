using UnityEngine.Events;
namespace PopupLoading
{
    public class PopupEventModel
    {
        public UnityAction onShow;
        public UnityAction onHide;
        public PopupEventModel() { }
        public PopupEventModel(UnityAction onShow, UnityAction onHide)
        {
            this.onShow = onShow;
            this.onHide = onHide;
        }
    }
}