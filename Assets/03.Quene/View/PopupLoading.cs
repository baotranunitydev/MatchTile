using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PopupLoading.Enum;
namespace PopupLoading
{
    public class PopupLoading : PopupBase
    {
        /// <summary>
        /// Loading sẽ đợi cho đến khi server trả về đúng kiểu responese thì nó mới tắt
        /// </summary>
        public List<TypeResponse> types;
        [SerializeField] TextMeshProUGUI txContent;
        string content;
        [SerializeField] bool isLoadingTransparent;
        private Sequence loadingSequence;
        public void SetType(List<TypeResponse> types)
        {
            this.types = types;
        }
        public void SetContent(string content)
        {
            if(content.Length == 0)
            {
                txContent.enabled = false;
            }
            else
            {
                this.content = content;
                txContent.enabled = true;
            }
        }
        public override void ShowPopup()
        {
            StartLoadingAnimation();
            base.ShowPopup();
        }
        private void StartLoadingAnimation()
        {
            // Tạo chuỗi DOTween mới và lưu tham chiếu
            loadingSequence = DOTween.Sequence()
                // Thêm một dấu chấm sau mỗi 0.5 giây
                .AppendCallback(() => txContent.text = content + ".")
                .AppendInterval(0.5f)
                .AppendCallback(() => txContent.text = content + "..")
                .AppendInterval(0.5f)
                .AppendCallback(() => txContent.text = content + "...")
                .AppendInterval(0.5f)
                // Reset lại văn bản về "Loading" và lặp lại chuỗi
                .AppendCallback(() => txContent.text = content)
                .AppendInterval(0.5f)
                // Lặp lại chuỗi vô hạn
                .SetLoops(-1);
        }
        public void StopLoadingAnimation()
        {
            // Dừng chuỗi DOTween nếu nó đang chạy
            if (loadingSequence != null && loadingSequence.IsPlaying())
            {
                loadingSequence.Kill();
                txContent.text = string.Empty; // Đặt văn bản khi dừng
                content = string.Empty; // Đặt văn bản khi dừng
            }
        }
        public override void HidePopup()
        {
            base.HidePopup();
            StopLoadingAnimation();
        }
    }
}