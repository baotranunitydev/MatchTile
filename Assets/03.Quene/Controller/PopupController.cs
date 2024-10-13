using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace PopupLoading.Enum
{
    public enum TypeRequest
    {
        Tap = 1,
    }
    public enum TypeResponse
    {
        GetUserData,
        GetItemConfig,
        PostWin,
        PostUseItem,
        PostBuyItem
    }

    public partial class PopupController : Singleton<PopupController>
    {
        public void ShowLoading(List<TypeResponse> types, string content = "")
        {
            var newBimBimBamBam = new PopupLoadingModel(types, content);
            foreach (var old in loadings)
            {
                if (old.IsTypeHaveTheSame(newBimBimBamBam))
                {
                    return;
                }
            }

            var lastest = GetPopup<PopupLoading>().GetComponent<PopupLoading>();
            //Kiểm tra popup đang mở xem có cùng loại không
            if (lastest.IsActive() && types.Any(t => lastest.types.Contains(t))) return;
            lock (loadingLock)
            {
                loadings.Add(newBimBimBamBam);
            }
            if (loadings.Count == 1) OnLoadingQueueCheck();

            //=========================
            void OnLoadingQueueCheck()
            {

                var comp = GetPopup<PopupLoading>().GetComponent<PopupLoading>();
                if (comp.IsActive()) return;
                if (loadings.Count == 0) return;
                Debug.Log("CheckLoadingList = " + loadings.Count);
                foreach (var xxxx in loadings)
                {
                    loadings.Remove(xxxx);
                    comp.InitEvent(new PopupEventModel() { onHide = () => OnLoadingQueueCheck() });
                    comp.SetType(xxxx.types);
                    comp.SetContent(xxxx.content);
                    comp.ShowPopup();
                    break;
                }

            }
        }


        public void HideLoading(TypeResponse type)
        {
            Debug.Log("Hideee - " + type.ToString());
            var comp = GetPopup<PopupLoading>().GetComponent<PopupLoading>();
            if (comp.IsActive() && comp.types.Contains(type))
            {
                comp.HidePopup();
            }
            else
            {
                lock (loadingLock)
                {
                    loadings.RemoveAll(item => item.types.Contains(type));
                }
            }
        }
        //public void ShowAlter(string content, bool isShowButton = true, string buttonContent = "OK", UnityAction onButton = null, UnityAction onShow = null, UnityAction onHide = null, bool isAutoDisable = true)
        //{

        //    if (onHide != null) onHide += OnAlertQueueCheck;
        //    else onHide = OnAlertQueueCheck;
        //    Debug.Log("Thêm 1 vào alert " + alertQueue.Count);

        //    alertQueue.Enqueue((content, isShowButton, buttonContent, onButton, new PopupEventModel(onShow, onHide), isAutoDisable));
        //    if (alertQueue.Count == 1)
        //    {
        //        Debug.Log("Alert show luôn " + alertQueue.Count);
        //        OnAlertQueueCheck();
        //    }
        //    void OnAlertQueueCheck()
        //    {
        //        lock (alertLock)
        //        {
        //            Debug.Log("OnAlertQueueCheck = " + alertQueue.Count);
        //            var x = GetPopup<PopupAlert>();
        //            var comp = x.GetComponent<PopupAlert>();
        //            if (comp.IsActive()) return;
        //            if (alertQueue.TryDequeue(out var alter))
        //            {
        //                comp.SetAlert(alter.Item1, alter.Item2, alter.Item3, alter.Item4);
        //                comp.InitEvent(alter.Item5);
        //                comp.ShowPopup();
        //            }
        //        }
        //    }
        //}
        public void ShowAlert(PopupAlertModel model, UnityAction onShow = null, UnityAction onHide = null)
        {
            if (onHide != null) onHide += OnAlertQueueCheck;
            else onHide = OnAlertQueueCheck;
            Debug.Log("Thêm 1 vào alert " + alertQueue.Count);

            alertQueue.Enqueue((model, new PopupEventModel(onShow, onHide)));
            if (alertQueue.Count == 1)
            {
                OnAlertQueueCheck();
            }
            void OnAlertQueueCheck()
            {
                lock (alertLock)
                {
                    Debug.Log("OnAlertQueueCheck = " + alertQueue.Count);
                    var x = GetPopup<PopupAlert>();
                    var comp = x.GetComponent<PopupAlert>();
                    if (comp.IsActive()) return;
                    if (alertQueue.TryDequeue(out var alter))
                    {
                        comp.SetAlert(alter.Item1);
                        comp.InitEvent(alter.Item2);
                        comp.ShowPopup();
                    }
                }
            }
        }
        public void HideAlertBySpecificType(PopupAlertSpecificTypeEnum type)
        {
            if (type == PopupAlertSpecificTypeEnum.None) return;
            //Wemon check in queue
            var tempQueue = new ConcurrentQueue<(PopupAlertModel, PopupEventModel)>();
            while (alertQueue.TryDequeue(out var alert))
            {
                if (alert.Item1.SpecificTypeEnum != type)
                {
                    tempQueue.Enqueue(alert);
                }
            }
            foreach (var item in tempQueue)
            {
                alertQueue.Enqueue(item);
            }
            //Wemoncheck in opening
            foreach (var i in popupCache)
            {
                var alert = i.GetComponent<PopupAlert>();
                if (alert != null && alert.SpecificTypeEnum == type)
                {
                    alert.HidePopup();
                }
            }
        }

        public void HideAllLoading()
        {
            Debug.Log("Hiding all loading popups");
            lock (loadingLock)
            {
                // Lặp qua danh sách loadings và ẩn từng popup
                foreach (var loading in loadings)
                {
                    var comp = GetPopup<PopupLoading>().GetComponent<PopupLoading>();
                    if (comp.IsActive())
                    {
                        comp.HidePopup();
                    }
                }

                // Xóa tất cả các mục trong danh sách loadings
                loadings.Clear();
            }
        }


        //public void HideAlert(string content)
        //{
        //    foreach (var i in popupCache)
        //    {
        //        var alert = i.GetComponent<PopupAlert>();
        //        if (alert != null && alert.content.Contains(content))
        //        {
        //            Debug.Log($"<color=black>======================= alert != null && alert.content.Contains(content)\n" +
        //                $"{alert.content} | {content}");

        //            alert.HidePopup();
        //            //GameHelper.Instance.popupController.HideLoading(TypeResponse.ConnectSuccess);
        //        }
        //    }
        //}
    }
    public partial class PopupController : Singleton<PopupController>
    {
        [SerializeField] GameObject[] popupPrefabs;
        /// <summary>
        /// Cache opened popup, only PopupBase.isCache = true
        /// </summary>
        [SerializeField] List<GameObject> popupCache;
        private void Start()
        {
            SceneManager.sceneLoaded += (scene, loadingMode) =>
            {
                // Xóa popup không có ở trong cache khi load scene để tránh duplicate
                // Pop không ở trong cache là loại pop dùng một lần nên không cần cache
                // Vì sao có loại popup không được vào cache (vì nó là isPool = false) dùng một lần hoặc ít
                // Cho nên khi chuyển scene hoặc reload scene, cần chủ động xóa nếu nó không được đóng
                try
                {
                    for (int i = transform.childCount - 1; i >= 0; i--)
                    {
                        Transform child = transform.GetChild(i);
                        if (!popupCache.Contains(child.gameObject))
                        {
                            Debug.Log($"Destroy popup {child.name}");
                            Destroy(child.gameObject);
                        }
                    }
                }
                catch { }
            };
        }
        /// <summary>
        /// Các loại popup không có stack, chỉ xuất hiện một lần và cũng không có parameters
        /// </summary>
        private GameObject GetPopup<T>() where T : PopupBase
        {
            //Tìm popup trong cache trước
            try
            {
                var popPop = popupCache.Find(i => i.GetComponent<PopupBase>() is T);
                //var popPop = popupCache.Find(i => i.GetComponent<PopupBase>().popupType == popupType);
                if (popPop != null)
                {
                    return popPop;
                }
                else
                {
                    //Chưa có thì sinh
                    var newPop = Instantiate(System.Array.Find(popupPrefabs,
                        i => i.GetComponent<PopupBase>() is T), gameObject.transform);
                    if (newPop.GetComponent<PopupBase>().IsPool()) popupCache.Add(newPop);
                    newPop.GetComponent<PopupBase>().Init();
                    return newPop;
                }
            }
            catch (System.Exception e)
            {
                throw new System.Exception("Tìm popup = " + typeof(T) + "\n" + e);
            }
        }
        /// <summary>
        /// Có bao nhiêu thứ mà client đang đợi server trả về, khi nào hết thì mới tắt loading popup <br/>
        /// Nếu nó đang hiện, ẩn để hiện cái mới <br/>
        /// Nếu không đang hiện thì check queue xem có nó không, có thì xoá khỏi queue. <br/>
        /// Vì phải chủ động xoá item nên không dùng Queue được, phải dùng Dic
        /// </summary>
        private List<PopupLoadingModel> loadings = new List<PopupLoadingModel>();
        private ConcurrentQueue<(PopupAlertModel, PopupEventModel)> alertQueue = new ConcurrentQueue<(PopupAlertModel, PopupEventModel)>();

        private readonly object loadingLock = new object();
        private readonly object alertLock = new object();
        private void Update()
        {

        }

        public void ClearQuenue()
        {
            alertQueue.Clear();
        }
    }
}