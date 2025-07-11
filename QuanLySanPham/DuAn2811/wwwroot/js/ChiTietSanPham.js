$(document).ready(function () {
    // Biến toàn cục để lưu trữ lựa chọn hiện tại cho mỗi sản phẩm
    // Nếu chỉ có một sản phẩm trên trang, bạn có thể dùng biến đơn giản
    let selectedCapacity = null;
    let selectedColor = null;

    // Hàm cập nhật giá dựa trên dung lượng và màu đã chọn
    function updatePrice(productId) {
        const priceDisplay = $('#price-' + productId);
        let foundPrice = 'Sản phẩm không có'; // Giá mặc định

        // Lấy tất cả các chi tiết sản phẩm thuộc sản phẩm hiện tại
        const productDetailsElements = $('.product-details[data-product-id="' + productId + '"] li');

        productDetailsElements.each(function () {
            const detailCapacity = $(this).data('capacity');
            const detailColor = $(this).data('color');
            const detailPrice = $(this).data('price');

            // So sánh có kiểm tra kiểu dữ liệu để tránh lỗi
            const matchesCapacity = (selectedCapacity === null || String(detailCapacity) === String(selectedCapacity));
            const matchesColor = (selectedColor === null || String(detailColor) === String(selectedColor));

            if (matchesCapacity && matchesColor) {
                foundPrice = detailPrice;
                return false; // Dừng vòng lặp khi tìm thấy kết quả khớp
            }
        });

        // Định dạng giá trước khi hiển thị
        if (typeof foundPrice === 'number') {
            priceDisplay.text('Giá: ' + foundPrice.toLocaleString('vi-VN') + ' đ');
        } else if (foundPrice !== 'Sản phẩm không có') {
            priceDisplay.text('Giá: ' + foundPrice + ' đ');
        } else {
            priceDisplay.text(foundPrice);
        }
    }

    // Xử lý sự kiện click cho các nút dung lượng
    $('.capacity-button').click(function () {
        const productId = $(this).data('product-id');
        selectedCapacity = $(this).data('capacity');

        // Loại bỏ lớp 'selected' khỏi tất cả các nút dung lượng của sản phẩm này
        $('.product[data-product-id="' + productId + '"]').find('.capacity-button').removeClass('selected');
        // Thêm lớp 'selected' vào nút vừa click
        $(this).addClass('selected');

        updatePrice(productId); // Cập nhật giá
    });

    // Xử lý sự kiện click cho các nút màu sắc
    $('.color-button').click(function () {
        const productId = $(this).data('product-id');
        selectedColor = $(this).data('color');

        // Loại bỏ lớp 'selected' khỏi tất cả các nút màu sắc của sản phẩm này
        $('.product[data-product-id="' + productId + '"]').find('.color-button').removeClass('selected');
        // Thêm lớp 'selected' vào nút vừa click
        $(this).addClass('selected');

        updatePrice(productId); // Cập nhật giá
    });

    // --- Khởi tạo ban đầu khi tải trang ---
    // Tự động chọn tùy chọn đầu tiên (dung lượng và màu) và cập nhật giá
    // Điều này đảm bảo giá hiển thị ngay cả khi người dùng chưa chọn gì
    const defaultProductId = '@Model.MaSanPham'; // Lấy MaSanPham từ Model
    if (defaultProductId) {
        // Tự động click vào nút dung lượng đầu tiên nếu có
        const firstCapacityButton = $('.product[data-product-id="' + defaultProductId + '"] .capacity-button').first();
        if (firstCapacityButton.length) {
            firstCapacityButton.click();
        }

        // Tự động click vào nút màu đầu tiên nếu có
        const firstColorButton = $('.product[data-product-id="' + defaultProductId + '"] .color-button').first();
        if (firstColorButton.length) {
            firstColorButton.click();
        }

        // Gọi updatePrice một lần nữa để đảm bảo giá hiển thị đúng nếu không có nút nào được click
        // (ví dụ: chỉ có 1 dung lượng/màu)
        updatePrice(defaultProductId);
    }
});