var quickChatContainer = document.getElementById("quickChatContainer");
var chatContainer = document.getElementById("chatContainer");
var overlay = document.getElementById("overlay");

function toggleChatBox() {
    if (quickChatContainer.style.display === "none" || quickChatContainer.style.display === "") {
        quickChatContainer.style.display = "block";
        quickChatContainer.style.opacity = "1";

    } else {
        quickChatContainer.style.opacity = "0";

        setTimeout(() => {
            quickChatContainer.style.display = "none";
        }, 300);
    }
    // Ẩn hộp chat đầy đủ khi mở hộp chat nhanh
    if (chatContainer.style.display === "block") {
        chatContainer.style.display = "none";
        chatContainer.style.opacity = "0";
    }
}

function openChat() {
    quickChatContainer.style.display = "none";
    quickChatContainer.style.opacity = "0";
    chatContainer.style.display = "block";
    chatContainer.style.opacity = "1";
    overlay.style.display = "block";
}

function closeChat() {
    chatContainer.style.opacity = "0";
    overlay.style.display = "none";
    setTimeout(() => {
        chatContainer.style.display = "none";
    }, 300);
}

function closeAllChats() {
    // Đóng hộp chat nhanh
    quickChatContainer.style.opacity = "0";
    setTimeout(() => {
        quickChatContainer.style.display = "none";
    }, 300);

    // Đóng hộp chat đầy đủ
    chatContainer.style.opacity = "0";
    setTimeout(() => {
        chatContainer.style.display = "none";
    }, 300);

    // Ẩn overlay
    overlay.style.display = "none";
}

function sendMessage() {
    let inputBox = document.getElementById("inputBox");
    let chatBox = document.getElementById("chatBox");
    let userMessage = inputBox.value.trim();

    if (userMessage !== "") {
        appendMessage("Bạn", userMessage, "user");
        inputBox.value = "";

        setTimeout(() => {
            appendMessage("Hệ thống", "Cảm ơn bạn đã liên hệ. Chúng tôi sẽ phản hồi sớm nhất.", "bot");
            chatBox.scrollTop = chatBox.scrollHeight;
        }, 1000);
    }
}

function appendMessage(sender, message, type) {
    let chatBox = document.getElementById("chatBox");
    let msgElement = document.createElement("div");
    msgElement.classList.add("message", type);
    msgElement.innerHTML = `<strong>${sender}:</strong> ${message}`;
    chatBox.appendChild(msgElement);
    chatBox.scrollTop = chatBox.scrollHeight; // Tự động cuộn xuống tin nhắn mới
}

function changeLanguage(lang) {
    if (lang === 'vi') {
        alert('Đã chuyển sang tiếng Việt!');
        // Thực hiện các thay đổi ngôn ngữ khác tại đây
    } else if (lang === 'en') {
        alert('Switched to English!');
        // Thực hiện các thay đổi ngôn ngữ khác tại đây
    }
}
$(document).ready(function () {
    $('.capacity-button').click(function () {
        var productId = $(this).data('product-id');
        var capacity = $(this).data('capacity');
        var priceDisplay = $('#price-' + productId);
        var productDetails = $('.product').find('a[href*="/ChiTietSanPham/GetChiTiet/' + productId + '"]').siblings('ul').find('li[data-capacity="' + capacity + '"]');

        if (productDetails.length > 0) {
            var price = productDetails.data('price');
            priceDisplay.text('Giá: ' + price + ' đ');
        } else {
            priceDisplay.text('Giá: N/A');
        }
    });
});