document.addEventListener('DOMContentLoaded', () => {
    const cartItemsContainer = document.querySelector('.cart-items');
    const subtotalElement = document.getElementById('subtotal');
    const grandTotalElement = document.getElementById('grand-total');
    const shippingCost = 0.00; // You can make this dynamic

    function formatCurrency(amount) {
        return amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
    }

    function updateCartTotals() {
        let currentSubtotal = 0;
        document.querySelectorAll('.cart-item').forEach(item => {
            const price = parseFloat(item.querySelector('.item-price').dataset.price);
            const quantity = parseInt(item.querySelector('.quantity-input').value);
            const itemSubtotal = price * quantity;
            item.querySelector('.item-subtotal').textContent = formatCurrency(itemSubtotal);
            currentSubtotal += itemSubtotal;
        });

        subtotalElement.textContent = formatCurrency(currentSubtotal);
        grandTotalElement.textContent = formatCurrency(currentSubtotal + shippingCost);
    }

    // Event delegation for quantity buttons and remove buttons
    cartItemsContainer.addEventListener('click', (event) => {
        const target = event.target;
        const cartItem = target.closest('.cart-item');

        if (!cartItem) return; // Not a cart item related click

        const quantityInput = cartItem.querySelector('.quantity-input');
        let currentQuantity = parseInt(quantityInput.value);

        if (target.classList.contains('quantity-minus')) {
            if (currentQuantity > 1) {
                quantityInput.value = currentQuantity - 1;
                updateCartTotals();
            }
        } else if (target.classList.contains('quantity-plus')) {
            quantityInput.value = currentQuantity + 1;
            updateCartTotals();
        } else if (target.classList.contains('remove-item')) {
            cartItem.remove();
            updateCartTotals();
            // Optional: Show a message if cart is empty
            if (cartItemsContainer.children.length === 0) {
                cartItemsContainer.innerHTML = '<p style="text-align: center; padding: 20px;">Giỏ hàng của bạn đang trống.</p>';
            }
        }
    });

    // Update totals when quantity input changes manually
    cartItemsContainer.addEventListener('change', (event) => {
        const target = event.target;
        if (target.classList.contains('quantity-input')) {
            let value = parseInt(target.value);
            if (isNaN(value) || value < 1) {
                target.value = 1; // Default to 1 if invalid input
            }
            updateCartTotals();
        }
    });

    // Initial calculation when page loads
    updateCartTotals();
});

document.getElementById('continueShoppingBtn').addEventListener('click', function () {
    window.location.href = '/ThanhToan/GetThanhToan';
}); 