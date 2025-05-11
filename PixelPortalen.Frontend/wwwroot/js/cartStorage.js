window.cartStorage = {
    saveCart: function (cartJson) {
        localStorage.setItem("cart", cartJson);
    },
    loadCart: function () {
        return localStorage.getItem("cart");
    },
    clearCart: function () {
        localStorage.removeItem("cart");
    }
};