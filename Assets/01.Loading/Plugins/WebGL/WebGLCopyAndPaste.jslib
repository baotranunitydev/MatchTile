// WebGLCopyPaste.jslib

mergeInto(LibraryManager.library, {
    // Hàm khởi tạo và thiết lập các callback cho sao chép và dán
    initWebGLCopyAndPaste: function(cutCopyCallback, pasteCallback) {
        // Thiết lập callback cho hàm sao chép
        Module.ccutCopyCallback = cutCopyCallback;

        // Thiết lập callback cho hàm dán
        Module.pasteCallback = pasteCallback;
    },

    // Hàm sao chép nội dung vào clipboard
    passCopyToBrowser: function(str) {
        var fn = typeof UTF8ToString === 'function' ? UTF8ToString : Pointer_stringify;
        var textarea = document.createElement('textarea');
        textarea.value = fn(str);
        document.body.appendChild(textarea);
        
        // Chọn toàn bộ nội dung trong textarea
        textarea.select();

        // Thử sao chép vào clipboard
        var success = document.execCommand('copy');

        // Xóa thẻ textarea tạm thời
        document.body.removeChild(textarea);

        // Trả về kết quả (true nếu thành công, false nếu thất bại)
        return success;
    }
});
