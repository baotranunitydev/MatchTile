using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace PopupLoading
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopupBase : MonoBehaviour
    {
        [Tooltip("Chặn tương tác với các UI phía sau")]
        [BoxGroup("Base")]
        [SerializeField] protected Image imgFade;
        [BoxGroup("Base")]
        [Tooltip("Fade still enabled but u can't see it")]
        [SerializeField] protected bool imageFadeIsTransparent;
        [Tooltip("Cửa sổ chính của popup, show popup sẽ set true window này")]
        [BoxGroup("Base")]
        [SerializeField] protected RectTransform window;
        [SerializeField] CanvasGroup canvasGroup;
        [Tooltip("Vị trí gốc của window để animation hiển thị chuẩn bro")]
        [BoxGroup("Base")]
        [SerializeField] protected float anchorPosY;

        [Tooltip("True nếu popup được dùng thường xuyên, False sẽ destroy sau khi đóng popup.")]
        [BoxGroup("Base")]
        [SerializeField] protected bool isPool;

        [Tooltip("Nếu là popup con của một popup khác, sẽ không có hiệu lực với isPool, tức sẽ bị destroy nếu popup cha destroy")]
        [BoxGroup("Base")]
        [SerializeField] protected bool isChildren;

        [Tooltip("Các scrollrects khi hiện lại cần được reset về vị trí gốc, ta thường cuộn nó xuống cuối rồi ẩn")]
        [BoxGroup("Base")]
        [SerializeField] protected List<ScrollRect> scrollRects = new List<ScrollRect>();

        [Tooltip("Hehe")]
        [BoxGroup("Base")]
        [SerializeField] protected PopupAnimationTypeEnum animationType;

        public bool IsFirstRanking;
        protected PopupEventModel popupEvent;
        [SerializeField] protected float showHideTime = 0.5f;

        [Button(Name = "Get Game Object To Field", Icon = SdfIconType.Link)]
        protected virtual void OnValidate()
        {
            try
            {
                canvasGroup = GetComponent<CanvasGroup>();
                imgFade = transform.Find("Fade").GetComponent<Image>();
                window = transform.Find("Window").GetComponent<RectTransform>();
                anchorPosY = window.anchoredPosition.y;
            }
            catch
            {
                Debug.LogError("Cấu trúc prefab không đúng, thiếu Fade và Window " + GetType()); 
            }
        }
        protected virtual void Start()
        {
            IsFirstRanking = false;
        }
        public virtual void Init()
        {
            SetStatusPopup(false);
        }
        public void InitEvent(PopupEventModel popupEvent)
        {
            this.popupEvent = popupEvent;

        }
        /// <summary>
        /// Kiểm tra popup có đang hiển thị không, dành cho việc queue chờ nó ẩn để hiện nội dung tiếp theo
        /// </summary>
        /// <returns></returns>
        public bool IsActive()
        {
            return window.gameObject.activeSelf;
        }
        public bool IsPool()
        {
            return isPool;
        }
        public virtual void ShowPopup()
        {
            if (imgFade == null)
            {
                Debug.LogError("Window is null!");
                return;
            }

            if (window == null)
            {
                Debug.LogError("Window is null!");
                return;
            }
            ResetScrollReacts();
            switch (animationType)
            {
                case PopupAnimationTypeEnum.Fall:
                    SetStatusPopup(true);
                    imgFade.DOFade(imageFadeIsTransparent ? 0 : 0.8f, showHideTime / 3).SetLink(gameObject);
                    window.anchoredPosition = new Vector2(0, UnityEngine.Screen.height * 2);
                    window.DOAnchorPosY(anchorPosY, showHideTime).OnComplete(() =>
                    {
                        InvokeOnShow();
                    }).SetLink(gameObject);
                    break;
                case PopupAnimationTypeEnum.ScaleX:
                    window.localScale = new Vector2(0, 1);
                    SetStatusPopup(true);
                    imgFade.DOFade(imageFadeIsTransparent ? 0 : 0.8f, showHideTime / 3).SetLink(gameObject);
                    window.DOScaleX(1, showHideTime).OnComplete(() =>
                    {
                        InvokeOnShow();
                    }).SetLink(gameObject);
                    break;
                case PopupAnimationTypeEnum.FadeInFadeOut:
                    canvasGroup.alpha = 0;
                    SetStatusPopup(true);
                    imgFade.DOFade(imageFadeIsTransparent ? 0 : 0.8f, showHideTime / 3).SetLink(gameObject).OnComplete(() =>
                    {
                        
                    });
                    canvasGroup.DOFade(1, showHideTime).SetLink(gameObject).OnComplete(() =>
                    {
                        InvokeOnShow();
                    });

                    break;
            }
            //if (!IsActive())
            //{

            //    switch (animationType)
            //    {
            //        case PopupAnimationTypeEnum.Fall:
            //            SetStatusPopup(true);
            //            imgFade.DOFade(imageFadeIsTransparent ? 0 : 0.8f, showHideTime / 3).SetLink(gameObject);
            //            window.anchoredPosition = new Vector2(0, Screen.height * 2);
            //            window.DOAnchorPosY(anchorPosY, showHideTime).OnComplete(() =>
            //            {
            //                InvokeOnShow();
            //            }).SetLink(gameObject);
            //            break;
            //        case PopupAnimationTypeEnum.ScaleX:
            //            window.localScale = new Vector2(0, 1);
            //            SetStatusPopup(true);
            //            imgFade.DOFade(imageFadeIsTransparent ? 0 : 0.8f, showHideTime / 3).SetLink(gameObject);
            //            window.DOScaleX(1, showHideTime).OnComplete(() =>
            //            {
            //                InvokeOnShow();
            //            }).SetLink(gameObject);
            //            break;
            //        case PopupAnimationTypeEnum.FadeInFadeOut:
            //            SetStatusPopup(true);
            //            imgFade.DOFade(imageFadeIsTransparent ? 0 : 0.8f, showHideTime / 3).SetLink(gameObject).OnComplete(() =>
            //            {
            //                InvokeOnShow();
            //            });

            //            break;
            //    }
            //}
            //else
            //{
            //    InvokeOnShow();
            //}
        }
        private void InvokeOnShow()
        {
            IsFirstRanking = true;
            popupEvent?.onShow?.Invoke();
        }

        public UniTask WatingShow()
        {
            return UniTask.WaitUntil(() => IsFirstRanking);
        }
        private void InvokeOnHide()
        {
            popupEvent?.onHide?.Invoke();
        }
        protected virtual void ResetScrollReacts()
        {
            foreach (var react in scrollRects)
            {
                react.verticalNormalizedPosition = 1;
                react.horizontalNormalizedPosition = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onHideComplete">Event này dùng khi ta gọi hàm này, khác với this.onHide được dùng khi show (mặc dù cả 2 đều được invoke khi hide)</param>
        public virtual void HidePopup()
        {

            switch (animationType)
            {
                case PopupAnimationTypeEnum.Fall:
                    imgFade.DOFade(0, showHideTime).SetLink(gameObject);
                    window.DOAnchorPosY(UnityEngine.Screen.height * 2, showHideTime).OnComplete(() =>
                    {
                        SetStatusPopup(false);
                        InvokeOnHide();
                        if (!isPool && !isChildren)
                        {
                            Destroy(gameObject);
                        }
                    }).SetLink(gameObject);
                    break;
                case PopupAnimationTypeEnum.ScaleX:
                    imgFade.DOFade(0, showHideTime).SetLink(gameObject);
                    window.DOScaleX(0, showHideTime).OnComplete(() =>
                    {
                        SetStatusPopup(false);
                        window.localScale = new Vector2(1, 1);
                        InvokeOnHide();
                        if (!isPool && !isChildren)
                        {
                            Destroy(gameObject);
                        }
                    }).SetLink(gameObject);
                    break;
                case PopupAnimationTypeEnum.FadeInFadeOut:
                    imgFade.DOFade(0, showHideTime).SetLink(gameObject).OnComplete(() =>
                    {
  
                    });
                    canvasGroup.DOFade(0, showHideTime).SetLink(gameObject).OnComplete(() =>
                    {

                        SetStatusPopup(false);
                        InvokeOnHide();
                        if (!isPool && !isChildren)
                        {
                            Destroy(gameObject);
                        }
                    });
                    break;
            }

        }
        protected void SetStatusPopup(bool status)
        {
            window.gameObject.SetActive(status);
            imgFade.gameObject.SetActive(status);
        }

    }
}