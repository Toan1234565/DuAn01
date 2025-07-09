//let selectedFilters = {}; // Đối tượng lưu trữ các bộ lọc đã chọn (ví dụ: { hang: ['samsung'], ram: ['8'] })
//let selectedPriceButton = null; // Biến lưu trữ nút giá đã được chọn (để bỏ chọn khi chọn nút khác hoặc dùng slider)
//const selectedFiltersContainer = document.getElementById('selected-filters'); // Lấy phần tử div hiển thị các bộ lọc đã chọn
//const minPriceInput = document.getElementById('min-price'); // Lấy ô nhập giá tối thiểu
//const maxPriceInput = document.getElementById('max-price'); // Lấy ô nhập giá tối đa
//const priceSliderMin = document.getElementById('price-slider-min'); // Lấy thanh trượt giá tối thiểu
//const priceSliderMax = document.getElementById('price-slider-max'); // Lấy thanh trượt giá tối đa
//const minPriceLimit = 0; // Giới hạn giá tối thiểu
//const maxPriceLimit = 99999999; // Giới hạn giá tối đa
//const filterSection = document.getElementById("loc");
//const openFilterBtn = document.getElementById("open-filter-btn");

//openFilterBtn.addEventListener('click', toggleChatBox);

//function toggleChatBox() {
//    filterSection.classList.toggle('show');
//    filterSection.style.opacity = filterSection.classList.contains('show') ? "1" : "0";
//    if (!filterSection.classList.contains('show')) {
//        setTimeout(() => {
//            filterSection.style.display = "none";
//        }, 300);
//    } else {
//        filterSection.style.display = "block";
//    }
//}

//function updateSelectedFiltersDisplay() {
//    selectedFiltersContainer.innerHTML = ''; // Xóa nội dung hiện tại của vùng hiển thị bộ lọc đã chọn
//    for (const category in selectedFilters) { // Duyệt qua từng danh mục bộ lọc trong đối tượng selectedFilters
//        if (selectedFilters[category]) { // Kiểm tra nếu có giá trị nào được chọn cho danh mục này
//            const tag = document.createElement('span'); // Tạo một phần tử span mới (sẽ hiển thị như một "tag")
//            tag.classList.add('filter-tag'); // Thêm class 'filter-tag' để áp dụng kiểu dáng
//            tag.textContent = `${category}: ${Array.isArray(selectedFilters[category]) ? selectedFilters[category].join(', ') : selectedFilters[category]}`; // Thiết lập nội dung của tag (tên danh mục và giá trị đã chọn)
//            selectedFiltersContainer.appendChild(tag); // Thêm tag vào vùng hiển thị
//        }
//    }
//    // Hiển thị khoảng giá từ input
//    const priceTag = document.createElement('span');
//    priceTag.classList.add('filter-tag');
//    priceTag.textContent = `Giá: ${formatCurrency(minPriceInput.value)} - ${formatCurrency(maxPriceInput.value)}`;
//    selectedFiltersContainer.appendChild(priceTag);
//}

//function toggleFilter(button, category) {
//    // Hàm xử lý khi một nút bộ lọc (không phải giá) được nhấp vào
//    if (!selectedFilters[category]) {
//        selectedFilters[category] = []; // Nếu chưa có mảng cho danh mục này, hãy tạo một mảng mới
//    }
//    const value = button.dataset.value; // Lấy giá trị bộ lọc từ thuộc tính 'data-value' của nút
//    const index = selectedFilters[category].indexOf(value); // Kiểm tra xem giá trị đã tồn tại trong mảng chưa

//    if (index > -1) {
//        selectedFilters[category].splice(index, 1); // Nếu đã tồn tại, hãy xóa nó khỏi mảng (bỏ chọn)
//        button.classList.remove("selected"); // Loại bỏ class 'selected' để thay đổi kiểu dáng nút
//    } else {
//        selectedFilters[category].push(value); // Nếu chưa tồn tại, hãy thêm nó vào mảng (chọn)
//        button.classList.add("selected"); // Thêm class 'selected' để thay đổi kiểu dáng nút
//    }

//    // Logic để chỉ chọn một giá trị cho bộ lọc "Hãng sản xuất"
//    if (category === 'hang') {
//        const buttons = button.parentNode.querySelectorAll('button'); // Lấy tất cả các nút trong cùng nhóm "Hãng sản xuất"
//        buttons.forEach(btn => {
//            if (btn !== button) { // Bỏ chọn tất cả các nút khác trong nhóm
//                btn.classList.remove('selected');
//                const indexToRemove = selectedFilters['hang'] ? selectedFilters['hang'].indexOf(btn.dataset.value) : -1;
//                if (indexToRemove > -1) {
//                    selectedFilters['hang'].splice(indexToRemove, 1);
//                }
//            }
//        });
//        selectedFilters['hang'] = [value]; // Chỉ giữ lại giá trị của nút vừa được chọn
//    }

//    updateSelectedFiltersDisplay(); // Cập nhật hiển thị các bộ lọc đã chọn
//}

//function selectPriceRange(button) {
//    // Hàm xử lý khi một nút chọn khoảng giá được nhấp vào
//    // Bỏ chọn nút giá đã chọn trước đó
//    if (selectedPriceButton && selectedPriceButton !== button) {
//        selectedPriceButton.classList.remove("selected");
//    }
//    // Chọn nút hiện tại
//    button.classList.add("selected");
//    selectedPriceButton = button;

//    // Cập nhật giá trị input và slider
//    minPriceInput.value = button.dataset.min; // Đặt giá trị tối thiểu từ thuộc tính 'data-min' của nút
//    maxPriceInput.value = button.dataset.max; // Đặt giá trị tối đa từ thuộc tính 'data-max' của nút
//    updatePriceSlider(); // Cập nhật vị trí của thanh trượt
//    updateSelectedFiltersDisplay(); // Cập nhật hiển thị các bộ lọc đã chọn
//}

//function updatePriceInputs() {
//    minPriceInput.value = priceSliderMin.value;
//    maxPriceInput.value = priceSliderMax.value;
//    // Bỏ chọn nút giá khi slider thay đổi thủ công
//    if (selectedPriceButton) {
//        selectedPriceButton.classList.remove("selected");
//        selectedPriceButton = null;
//    }
//    updateSelectedFiltersDisplay();
//}

//function updatePriceSlider() {
//    priceSliderMin.value = minPriceInput.value;
//    priceSliderMax.value = maxPriceInput.value;
//}

//function formatCurrency(number) {
//    return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(number);
//}

//function applyFilters() {
//    const filtersToSend = { ...selectedFilters, minPrice: minPriceInput.value, maxPrice: maxPriceInput.value };
//    alert("Đã lọc theo: " + JSON.stringify(filtersToSend));
//    // Ở đây bạn sẽ gửi `filtersToSend` đến backend API của mình
//}

//// Thiết lập sự kiện cho input số và slider giá
//minPriceInput.addEventListener('input', updatePriceSlider);
//maxPriceInput.addEventListener('input', updatePriceSlider);
//priceSliderMin.addEventListener('input', updatePriceInputs);
//priceSliderMax.addEventListener('input', updatePriceInputs);

//filterSection.style.display = 'none'; // Đóng bộ lọc sau khi áp dụng
//// Hiển thị ban đầu
//updateSelectedFiltersDisplay();