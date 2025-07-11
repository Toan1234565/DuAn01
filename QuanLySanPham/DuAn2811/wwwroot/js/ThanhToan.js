document.addEventListener('DOMContentLoaded', () => {
    // Logic cho tab phương thức thanh toán
    const tabButtons = document.querySelectorAll('.payment-tabs .tab-button');
    const paymentContents = document.querySelectorAll('.payment-content');

    tabButtons.forEach(button => {
        button.addEventListener('click', () => {
            // Remove active class from all buttons and contents
            tabButtons.forEach(btn => btn.classList.remove('active'));
            paymentContents.forEach(content => content.classList.remove('active'));

            // Add active class to clicked button and corresponding content
            button.classList.add('active');
            const targetTabId = button.dataset.tab;
            document.getElementById(targetTabId).classList.add('active');
        });
    });

    // Hàm định dạng tiền tệ
    function formatCurrency(amount) {
        return amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
    }

    // Hàm cập nhật tổng tiền (giữ nguyên, bạn cần thay đổi để lấy dữ liệu sản phẩm từ giỏ hàng thực)
    function updateOrderSummary() {
        const subtotalElement = document.getElementById('summary-subtotal');
        const shippingElement = document.getElementById('summary-shipping');
        const discountElement = document.getElementById('summary-discount');
        const grandTotalElement = document.getElementById('summary-grand-total');

        // Giá trị demo, trong thực tế bạn sẽ lấy từ dữ liệu giỏ hàng hoặc từ server
        let subtotal = 201700; // Ví dụ: 201,700 đ
        let shippingCost = 0;
        let discount = 0;

        document.querySelectorAll('input[name="shipping"]').forEach(radio => {
            radio.addEventListener('change', (event) => {
                if (event.target.value === 'express') {
                    shippingCost = 30000; // 30.000 đ
                    shippingElement.textContent = formatCurrency(shippingCost);
                } else {
                    shippingCost = 0;
                    shippingElement.textContent = 'Miễn phí';
                }
                updateGrandTotal();
            });
        });

        function updateGrandTotal() {
            let currentGrandTotal = subtotal + shippingCost - discount;
            grandTotalElement.textContent = formatCurrency(currentGrandTotal);
        }

        // Cập nhật tổng tiền hàng ban đầu (nếu nó là giá trị tĩnh trong HTML, bạn có thể bỏ qua)
        subtotalElement.textContent = formatCurrency(subtotal);
        updateGrandTotal();
    }

    // Khởi tạo tổng tiền
    updateOrderSummary();

    // Logic xử lý form (giữ nguyên)
    const placeOrderButton = document.querySelector('.place-order-button');
    placeOrderButton.addEventListener('click', (event) => {
        event.preventDefault(); // Ngăn chặn form submit mặc định

        // Lấy dữ liệu từ form
        const fullName = document.getElementById('fullName').value;
        const phone = document.getElementById('phone').value;
        const email = document.getElementById('email').value;
        const provinceCode = document.getElementById('province').value; // Lấy mã code
        const districtCode = document.getElementById('district').value; // Lấy mã code
        const wardCode = document.getElementById('ward').value;       // Lấy mã code
        const address = document.getElementById('address').value;
        const notes = document.getElementById('notes').value;
        const shippingMethod = document.querySelector('input[name="shipping"]:checked').value;
        const paymentMethod = document.querySelector('.payment-tabs .tab-button.active').dataset.tab;

        // Để gửi tên thay vì mã code lên server, bạn cần lấy textContent của option đã chọn
        const provinceName = provinceSelect.options[provinceSelect.selectedIndex].textContent;
        const districtName = districtSelect.options[districtSelect.selectedIndex].textContent;
        const wardName = wardSelect.options[wardSelect.selectedIndex].textContent;


        // Đây là nơi bạn sẽ gửi dữ liệu này đến backend của ASP.NET Core MVC của bạn
        // Ví dụ sử dụng fetch API để gửi dữ liệu dạng JSON
        const orderData = {
            fullName,
            phone,
            email,
            provinceCode, // Gửi mã code nếu backend cần xử lý theo mã
            provinceName, // Gửi tên để lưu trữ hoặc hiển thị
            districtCode,
            districtName,
            wardCode,
            wardName,
            address,
            notes,
            shippingMethod,
            paymentMethod,
            // Thêm thông tin sản phẩm và tổng tiền thực tế từ giỏ hàng
        };

        console.log("Dữ liệu đơn hàng sẽ được gửi:", orderData);

        // Ví dụ fetch POST request (bạn cần có một endpoint API tương ứng trên server)
        /*
        fetch('/api/orders/placeorder', { // Thay thế bằng endpoint API thực tế của bạn
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(orderData),
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert('Đơn hàng của bạn đã được đặt thành công!');
                // Chuyển hướng đến trang xác nhận đơn hàng
                // window.location.href = '/order-confirmation/' + data.orderId;
            } else {
                alert('Có lỗi xảy ra khi đặt hàng: ' + data.message);
            }
        })
        .catch((error) => {
            console.error('Lỗi:', error);
            alert('Không thể kết nối đến máy chủ. Vui lòng thử lại sau.');
        });
        */

        alert('Đơn hàng của bạn đã được đặt thành công! (Đây là demo UI, dữ liệu chưa gửi)');
    });


    // --- Logic lấy dữ liệu Tỉnh/Huyện/Xã từ API (sử dụng async/await) ---
    const provinceSelect = document.getElementById('province');
    const districtSelect = document.getElementById('district');
    const wardSelect = document.getElementById('ward');

    // Hàm chung để gọi API
    async function fetchData(url) {
        try {
            const response = await fetch(url);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return await response.json();
        } catch (error) {
            console.error("Lỗi khi fetch dữ liệu từ " + url + ":", error);
            return null;
        }
    }

    // Hàm tải tỉnh/thành phố
    async function loadProvinces() {
        const provinces = await fetchData('https://provinces.open-api.vn/api/p/');
        if (provinces) {
            provinceSelect.innerHTML = '<option value="">Chọn Tỉnh/Thành phố</option>';
            provinces.forEach(p => {
                const option = document.createElement('option');
                option.value = p.code; // Sử dụng mã code để tra cứu huyện/xã
                option.textContent = p.name;
                provinceSelect.appendChild(option);
            });
        }
    }

    // Hàm tải quận/huyện
    async function loadDistricts(provinceCode) {
        districtSelect.innerHTML = '<option value="">Chọn Quận/Huyện</option>';
        wardSelect.innerHTML = '<option value="">Chọn Phường/Xã</option>'; // Reset xã khi đổi huyện/tỉnh

        if (provinceCode) {
            // API này cho phép lấy huyện trực tiếp từ mã tỉnh và độ sâu (depth)
            const provinceData = await fetchData(`https://provinces.open-api.vn/api/p/${provinceCode}?depth=2`);
            if (provinceData && provinceData.districts) {
                provinceData.districts.forEach(d => {
                    const option = document.createElement('option');
                    option.value = d.code;
                    option.textContent = d.name;
                    districtSelect.appendChild(option);
                });
            }
        }
    }

    // Hàm tải phường/xã
    async function loadWards(districtCode) {
        wardSelect.innerHTML = '<option value="">Chọn Phường/Xã</option>';

        if (districtCode) {
            // API này cho phép lấy xã trực tiếp từ mã huyện và độ sâu (depth)
            const districtData = await fetchData(`https://provinces.open-api.vn/api/d/${districtCode}?depth=2`);
            if (districtData && districtData.wards) {
                districtData.wards.forEach(w => {
                    const option = document.createElement('option');
                    option.value = w.code;
                    option.textContent = w.name;
                    wardSelect.appendChild(option);
                });
            }
        }
    }

    // Event listeners cho các dropdown để tải dữ liệu động
    provinceSelect.addEventListener('change', () => {
        loadDistricts(provinceSelect.value);
    });

    districtSelect.addEventListener('change', () => {
        loadWards(districtSelect.value);
    });

    // Tải danh sách tỉnh/thành phố khi trang load lần đầu
    loadProvinces();
});