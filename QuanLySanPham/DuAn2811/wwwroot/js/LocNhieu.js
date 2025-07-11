document.addEventListener('DOMContentLoaded', function () {
    // --- DOM Element References ---
    const openFilterBtn = document.getElementById('open-filter-btn');
    const closeFilterBtn = document.getElementById('close-filter-btn');
    const applyFiltersBtn = document.getElementById('apply-filters-btn');
    const clearAllFiltersBtn = document.getElementById('clear-all-filters-btn');

    const filterContainer = document.getElementById('loc');
    const overlay = document.querySelector('.overlay'); // Ensure this element exists in your HTML

    const selectedFiltersContainer = document.getElementById('selected-filters');
    const minPriceInput = document.getElementById('min-price');
    const maxPriceInput = document.getElementById('max-price');
    const priceSliderMin = document.getElementById('price-slider-min');
    const priceSliderMax = document.getElementById('price-slider-max');

    // --- State Variables ---
    let selectedFilters = {
        hang: [],
        ram: [],
        dung_luong: [],
        price: { min: 0, max: 99999999 }
    };
    let selectedPriceButton = null; // Reference to the currently selected price range button

    // --- Constants for Price Range ---
    const DEFAULT_MIN_PRICE = 0;
    const DEFAULT_MAX_PRICE = 99999999;

    // --- Functions to control filter visibility and body scroll ---

    /**
     * Shows the filter container and overlay, and disables body scrolling.
     */
    function showFilterContainer() {
        filterContainer.classList.add('show');
        overlay.classList.add('show');
        document.body.style.overflow = 'hidden'; // Disable scrolling on the body
        filterContainer.focus(); // For accessibility
        console.log("Filter opened, body scroll disabled.");
    }

    /**
     * Hides the filter container and overlay, and enables body scrolling.
     */
    function hideFilterContainer() {
        filterContainer.classList.remove('show');
        overlay.classList.remove('show');
        document.body.style.overflow = ''; // Re-enable scrolling on the body
        console.log("Filter closed, body scroll enabled.");
    }

    // --- Event Listeners ---

    // Open filter button
    if (openFilterBtn) {
        openFilterBtn.addEventListener('click', showFilterContainer);
    }

    // Close filter button
    if (closeFilterBtn) {
        closeFilterBtn.addEventListener('click', hideFilterContainer);
    }

    // Click outside (on overlay) to close filter
    if (overlay) {
        overlay.addEventListener('click', function (event) {
            // Ensure the click was directly on the overlay, not on the filter container itself
            if (event.target === overlay) {
                hideFilterContainer();
            }
        });
    }

    // Apply filters button
    if (applyFiltersBtn) {
        applyFiltersBtn.addEventListener('click', applyFilters);
    }

    // Clear all filters button
    if (clearAllFiltersBtn) {
        clearAllFiltersBtn.addEventListener('click', clearAllFilters);
    }

    // Event delegation for category filter buttons (Hãng, RAM, Dung lượng)
    document.querySelectorAll('.filter-options.hang, .filter-options.ram, .filter-options.dl').forEach(group => {
        group.addEventListener('click', function (event) {
            const button = event.target.closest('button');
            if (button && button.parentElement === group) { // Ensure click is directly on a button within this group
                const category = group.dataset.filterCategory;
                const value = button.dataset.value;
                if (category && value) {
                    toggleCategoryFilter(button, category, value);
                }
            }
        });
    });

    // Event listeners for predefined price range buttons
    document.querySelectorAll('.filter-options.gia button').forEach(button => {
        button.addEventListener('click', function () {
            selectPriceRange(this);
        });
    });

    // Event listeners for price input fields and sliders
    if (minPriceInput && maxPriceInput) {
        minPriceInput.addEventListener('input', updatePriceSliderFromInput);
        maxPriceInput.addEventListener('input', updatePriceSliderFromInput);
    }
    if (priceSliderMin && priceSliderMax) {
        priceSliderMin.addEventListener('input', updatePriceInputsFromSlider);
        priceSliderMax.addEventListener('input', updatePriceInputsFromSlider);
    }

    // --- Initial Setup on DOM Load ---
    initializeFilters();

   
    function initializeFilters() {
        // Set initial values for price sliders based on inputs
        minPriceInput.value = DEFAULT_MIN_PRICE;
        maxPriceInput.value = DEFAULT_MAX_PRICE;
        priceSliderMin.value = DEFAULT_MIN_PRICE;
        priceSliderMax.value = DEFAULT_MAX_PRICE;

        // Sync initial price filter state
        selectedFilters.price.min = DEFAULT_MIN_PRICE;
        selectedFilters.price.max = DEFAULT_MAX_PRICE;

        updateSelectedFiltersDisplay(); // Always refresh selected tags on load
    }

   
    function updateSelectedFiltersDisplay() {
        selectedFiltersContainer.innerHTML = ''; // Clear current tags

        // Category filters (Hãng, RAM, Dung lượng)
        for (const category in selectedFilters) {
            if (['hang', 'ram', 'dung_luong'].includes(category) && selectedFilters[category].length > 0) {
                const displayName = getCategoryDisplayName(category);
                selectedFilters[category].forEach(value => {
                    addSelectedTag(`${displayName}: ${value}`, value, category);
                });
            }
        }

        // Price range filter
        const currentMinPrice = parseInt(minPriceInput.value);
        const currentMaxPrice = parseInt(maxPriceInput.value);

        if (currentMinPrice !== DEFAULT_MIN_PRICE || currentMaxPrice !== DEFAULT_MAX_PRICE) {
            addSelectedTag(`Giá: ${formatCurrency(currentMinPrice)} - ${formatCurrency(currentMaxPrice)}`,
                `${currentMinPrice}-${currentMaxPrice}`, 'price');
        }

        // Hide the selected-filters container if no filters are selected
        if (selectedFiltersContainer.children.length === 0) {
            selectedFiltersContainer.style.display = 'none';
        } else {
            selectedFiltersContainer.style.display = 'flex'; // Or 'block' depending on your CSS
        }
    }

   
    function addSelectedTag(text, value, type) {
        const tag = document.createElement('span');
        tag.className = 'filter-tag';
        tag.innerHTML = `${text} <span class="remove-tag" data-value="${value}" data-type="${type}">x</span>`;
        selectedFiltersContainer.appendChild(tag);

        // Add event listener for removing the tag
        tag.querySelector('.remove-tag').addEventListener('click', function () {
            const tagValue = this.dataset.value;
            const tagType = this.dataset.type;
            removeFilter(tagType, tagValue); // Call a centralized removal function
        });
    }

   
    function removeFilter(type, value) {
        if (type === 'price') {
            // Reset price filters
            minPriceInput.value = DEFAULT_MIN_PRICE;
            maxPriceInput.value = DEFAULT_MAX_PRICE;
            updatePriceSliderFromInput(); // Sync sliders
            selectedFilters.price.min = DEFAULT_MIN_PRICE;
            selectedFilters.price.max = DEFAULT_MAX_PRICE;
            // Deselect any price range button
            if (selectedPriceButton) {
                selectedPriceButton.classList.remove('selected');
                selectedPriceButton = null;
            }
        } else {
            // For other categories, remove from selectedFilters array and deselect button
            if (selectedFilters[type]) {
                selectedFilters[type] = selectedFilters[type].filter(val => String(val) !== String(value));
            }
            const buttonToDeselect = document.querySelector(`.filter-options.${type} button[data-value="${value}"]`);
            if (buttonToDeselect) {
                buttonToDeselect.classList.remove('selected');
            }
        }
        updateSelectedFiltersDisplay(); // Refresh tags
    }

    
    function toggleCategoryFilter(button, category, value) {
        // For 'hang' (manufacturer), only one can be selected
        if (category === 'hang') {
            const isCurrentlySelected = button.classList.contains('selected');
            // Deselect all buttons in this category
            document.querySelectorAll(`.filter-options.${category} button`).forEach(btn => {
                btn.classList.remove('selected');
            });
            selectedFilters[category] = []; // Clear previous selection

            if (!isCurrentlySelected) { // If the clicked button was NOT selected, select it
                button.classList.add('selected');
                selectedFilters[category].push(value);
            }
        } else {
            // For other categories (RAM, Dung lượng), multiple can be selected (toggle behavior)
            const index = selectedFilters[category].indexOf(value);
            if (index > -1) {
                selectedFilters[category].splice(index, 1);
                button.classList.remove('selected');
            } else {
                selectedFilters[category].push(value);
                button.classList.add('selected');
            }
        }
        updateSelectedFiltersDisplay(); // Refresh tags after selection change
    }

    /**
     * Handles selection of a predefined price range button.
     * @param {HTMLElement} button - The price range button clicked.
     */
    function selectPriceRange(button) {
        const min = parseInt(button.dataset.min);
        const max = parseInt(button.dataset.max);

        // Deselect previously selected price range button if different
        if (selectedPriceButton && selectedPriceButton !== button) {
            selectedPriceButton.classList.remove('selected');
        }

        // Toggle selection for the current button
        if (button.classList.contains('selected')) {
            button.classList.remove('selected');
            selectedPriceButton = null;
            // If deselected, reset price inputs/sliders to default
            selectedFilters.price.min = DEFAULT_MIN_PRICE;
            selectedFilters.price.max = DEFAULT_MAX_PRICE;
        } else {
            button.classList.add('selected');
            selectedPriceButton = button;
            selectedFilters.price.min = min;
            selectedFilters.price.max = max;
        }

        // Sync inputs and sliders with selected price range
        minPriceInput.value = selectedFilters.price.min;
        maxPriceInput.value = selectedFilters.price.max;
        updatePriceSliderFromInput(); // This will also update selectedFilters.price and tags

        updateSelectedFiltersDisplay(); // Refresh tags
    }

    /**
     * Updates price input fields based on slider movements.
     */
    function updatePriceInputsFromSlider() {
        let minVal = parseInt(priceSliderMin.value);
        let maxVal = parseInt(priceSliderMax.value);

        // Ensure min slider does not exceed max slider
        if (minVal > maxVal) {
            maxVal = minVal;
            priceSliderMax.value = minVal;
        }

        minPriceInput.value = minVal;
        maxPriceInput.value = maxVal;

        // Deselect any pre-defined price range button if sliders are manually adjusted
        if (selectedPriceButton) {
            selectedPriceButton.classList.remove('selected');
            selectedPriceButton = null;
        }
        // Update price in state and refresh tags
        selectedFilters.price.min = minVal;
        selectedFilters.price.max = maxVal;
        updateSelectedFiltersDisplay();
    }

    /**
     * Updates price sliders based on input field changes.
     */
    function updatePriceSliderFromInput() {
        let minVal = parseInt(minPriceInput.value);
        let maxVal = parseInt(maxPriceInput.value);

        // Enforce limits and ensure valid numbers
        minVal = isNaN(minVal) ? DEFAULT_MIN_PRICE : Math.max(DEFAULT_MIN_PRICE, Math.min(minVal, DEFAULT_MAX_PRICE));
        maxVal = isNaN(maxVal) ? DEFAULT_MAX_PRICE : Math.max(DEFAULT_MIN_PRICE, Math.min(maxVal, DEFAULT_MAX_PRICE));

        // Enforce min <= max
        if (minVal > maxVal) {
            maxVal = minVal;
            maxPriceInput.value = maxVal; // Update input field to reflect correction
        }

        minPriceInput.value = minVal;
        maxPriceInput.value = maxVal;

        priceSliderMin.value = minVal;
        priceSliderMax.value = maxVal;

        // Deselect any pre-defined price range button
        if (selectedPriceButton) {
            selectedPriceButton.classList.remove('selected');
            selectedPriceButton = null;
        }
        // Update price in state and refresh tags
        selectedFilters.price.min = minVal;
        selectedFilters.price.max = maxVal;
        updateSelectedFiltersDisplay();
    }
    function formatCurrency(number) {
        return new Intl.NumberFormat('vi-VN', { style: 'decimal', minimumFractionDigits: 0, maximumFractionDigits: 0 }).format(number) + ' đ';
    }

    function getCategoryDisplayName(category) {
        switch (category) {
            case 'hang': return 'Hãng';
            case 'ram': return 'RAM';
            case 'dung_luong': return 'Dung lượng';
            default: return category; // Fallback
        }
    }

    /**
     * Gathers all selected filters and initiates the filter application process (e.g., AJAX call).
     */
    function applyFilters() {
        const filtersToSend = {
            brands: selectedFilters.hang,
            ram: selectedFilters.ram,
            storage: selectedFilters.dung_luong,
            minPrice: selectedFilters.price.min,
            maxPrice: selectedFilters.price.max
        };

        //console.log("Filters to send:", filtersToSend);
        //alert("Áp dụng bộ lọc với: " + JSON.stringify(filtersToSend, null, 2)); // Pretty print JSON

        

        hideFilterContainer(); // Close the filter after applying
    }

    /**
     * Clears all selected filters and resets the UI.
     */
    function clearAllFilters() {
        // Reset all selected filters
        selectedFilters = {
            hang: [],
            ram: [],
            dung_luong: [],
            price: { min: DEFAULT_MIN_PRICE, max: DEFAULT_MAX_PRICE }
        };

        // Deselect all buttons in filter options
        document.querySelectorAll('.filter-options button.selected').forEach(button => {
            button.classList.remove('selected');
        });

        // Reset price inputs and sliders
        minPriceInput.value = DEFAULT_MIN_PRICE;
        maxPriceInput.value = DEFAULT_MAX_PRICE;
        priceSliderMin.value = DEFAULT_MIN_PRICE;
        priceSliderMax.value = DEFAULT_MAX_PRICE;

        updateSelectedFiltersDisplay(); // Update the displayed tags
        console.log("All filters cleared.");
        // Optionally, apply filters after clearing to refresh product list
        // applyFilters(); // Uncomment if you want to apply filters immediately after clearing
    }

    // Initial display update
    updateSelectedFiltersDisplay();
});