function fnbApp() {
    return {
        // Data
        cart: [],
        currentOrder: null,
        orderHistory: [],
        menu: [
            { id: 1, name: 'Gà chiên giòn', description: 'Gà chiên giòn với sốt thơm ngon', price: 120000, image: 'https://placehold.co/600x400?text=Gà+chiên+giòn', quantity: 1 },
            { id: 2, name: 'Pizza hải sản', description: 'Pizza hải sản đặc biệt', price: 180000, image: 'https://placehold.co/600x400?text=Pizza+hải+sản', quantity: 1 },
            { id: 3, name: 'Nước cam ép', description: 'Nước cam tươi 100%', price: 40000, image: 'https://placehold.co/600x400?text=Nước+cam+ép', quantity: 1 },
            { id: 4, name: 'Coca Cola', description: 'Nước ngọt có ga', price: 30000, image: 'https://placehold.co/600x400?text=Coca+Cola', quantity: 1 },
            { id: 5, name: 'Combo 1', description: 'Gà chiên + 2 nước ngọt', price: 150000, image: 'https://placehold.co/600x400?text=Combo+Gà+và+nước+ngọt', quantity: 1 },
            { id: 6, name: 'Khoai tây chiên', description: 'Khoai tây chiên giòn rụm', price: 50000, image: 'https://placehold.co/600x400?text=Khoai+tây+chiên', quantity: 1 },
        ],

        // Computed
        get total() {
            return this.cart.reduce((sum, item) => sum + (item.price * item.quantity), 0);
        },

        // Methods
        formatCurrency(amount) {
            return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(amount);
        },

        addToCart(item) {
            const existingItem = this.cart.find(cartItem => cartItem.id === item.id);
            if (existingItem) {
                existingItem.quantity += item.quantity;
            } else {
                this.cart.push({ ...item });
            }
            item.quantity = 1; // Reset về 1 sau khi thêm
        },

        removeFromCart(index) {
            this.cart.splice(index, 1);
        },

        placeOrder() {
            if (this.cart.length > 0) {
                const newOrder = {
                    id: Date.now(),
                    time: new Date().toLocaleTimeString('vi-VN'),
                    status: 'pending',
                    items: [...this.cart]
                };
                this.currentOrder = newOrder;
                this.orderHistory.push(newOrder);
                this.cart = [];
                this.updateOrderStatus(newOrder.id);
            }
        },

        updateOrderStatus(orderId) {
            setTimeout(() => {
                const order = this.orderHistory.find(o => o.id === orderId);
                if (order) order.status = 'preparing';
                if (this.currentOrder && this.currentOrder.id === orderId) this.currentOrder.status = 'preparing';
            }, 5000);

            setTimeout(() => {
                const order = this.orderHistory.find(o => o.id === orderId);
                if (order) order.status = 'delivered';
                if (this.currentOrder && this.currentOrder.id === orderId) this.currentOrder.status = 'delivered';
            }, 10000);
        },

        calculateOrderTotal(order) {
            return order.items.reduce((sum, item) => sum + (item.price * item.quantity), 0);
        },

        getOrderStatusText(status) {
            const statusText = {
                'pending': 'Chờ xác nhận',
                'preparing': 'Đang làm',
                'delivered': 'Đã bưng',
                'completed': 'Hoàn tất'
            };
            return statusText[status] || status;
        },

        getOrderStatusClass(status) {
            const statusClass = {
                'pending': 'status-pending',
                'preparing': 'status-preparing',
                'delivered': 'status-delivered',
                'completed': 'status-completed'
            };
            return statusClass[status] || '';
        }
    };
}