<template>
  <div>
    <div class="cart-header">
      <div class="container">
        <router-link to="/" class="back-button">🛒 Back to Store</router-link>
        <h2 class="cart-title">🧺 My Cart</h2>
      </div>
    </div>

    <ul class="cart-list">
      <li v-for="(item, index) in cart" :key="index" class="cart-item">
        <img :src="item.imageUrl" alt="Product Image" />
        <div class="item-info">
          <h3>{{ item.title }}</h3>
          <p>₹{{ item.price }} × {{ item.quantity }}</p>
          <div class="controls">
            <button @click="increaseQty(index)">+</button>
            <button @click="decreaseQty(index)" :disabled="item.quantity <= 1">-</button>
            <button @click="removeItem(index)">❌ Remove</button>
          </div>
        </div>
      </li>
    </ul>

    <div class="promo-section">
      <label for="promo">Promo Code:</label>
      <input id="promo" v-model="promoCode" placeholder="Enter code like DISCOUNT10" />
      <button @click="applyPromo">Apply</button>

      <p v-if="promoSuccess" style="color: green;">✅ Promo code applied: 10% OFF</p>
      <p v-else-if="promoTried && !promoSuccess" style="color: red;">❌ Invalid promo code.</p>
    </div>

    <div class="total-section">
      <hr style="margin: 16px 0;" />
      <p><strong>Total (Before Discount):</strong> ₹{{ beforeDiscount }}</p>

      <p v-if="isFirstTimeUser" style="color: green;">
        🎉 First-time user discount applied: ₹200 off!
      </p>

      <p><strong>Total (After Discount):</strong> ₹{{ totalPrice }}</p>
    </div>
  </div>
</template>

<script>
/* eslint-disable vue/multi-word-component-names */
export default {
  name: "Cart",
  data() {
    return {
      cart: [],
      isFirstTimeUser: false,
      promoCode: '',
      promoApplied: false,
      promoSuccess: false,
      promoTried: false
    };
  },
  computed: {
    beforeDiscount() {
      return this.cart.reduce((sum, item) => sum + item.price * item.quantity, 0);
    },
    totalPrice() {
      let total = this.beforeDiscount;

      if (total > 3000) total -= 500;

      const electronics = this.cart.filter(p => p.category.toLowerCase() === "electronics");
      const electronicsTotal = electronics.reduce((sum, item) => sum + item.price * item.quantity, 0);
      total -= electronicsTotal * 0.1;

      const totalItems = this.cart.reduce((sum, item) => sum + item.quantity, 0);
      if (totalItems >= 3) total -= 1000;

      const books = this.cart.filter(p => p.category.toLowerCase() === "education");
      let bookUnits = [];
      books.forEach(item => {
        for (let i = 0; i < item.quantity; i++) bookUnits.push(item.price);
      });
      bookUnits.sort((a, b) => a - b);
      const freeBooks = Math.floor(bookUnits.length / 3);
      const freeAmount = bookUnits.slice(0, freeBooks).reduce((sum, price) => sum + price, 0);
      total -= freeAmount;

      if (this.isFirstTimeUser) {
        total -= 200;
        localStorage.setItem('firstTimeUser', 'false');
      }

      if (this.promoApplied) {
        total -= total * 0.1;
      }

      return Math.max(total, 0);
    }
  },
  methods: {
    increaseQty(index) {
      this.cart[index].quantity++;
      this.saveCart();
    },
    decreaseQty(index) {
      if (this.cart[index].quantity > 1) {
        this.cart[index].quantity--;
        this.saveCart();
      }
    },
    removeItem(index) {
      this.cart.splice(index, 1);
      this.saveCart();
    },
    saveCart() {
      localStorage.setItem('cart', JSON.stringify(this.cart));
    },
    applyPromo() {
      this.promoTried = true;
      if (this.promoCode.trim().toUpperCase() === "DISCOUNT10") {
        this.promoApplied = true;
        this.promoSuccess = true;
      } else {
        this.promoApplied = false;
        this.promoSuccess = false;
      }
    }
  },
  mounted() {
    const savedCart = JSON.parse(localStorage.getItem('cart') || '[]');
    this.cart = savedCart.map(item => ({
      ...item,
      quantity: item.quantity || 1
    }));

    this.isFirstTimeUser = localStorage.getItem('firstTimeUser') !== 'false';
  }
};
</script>

<style>
.cart-header {
  background: linear-gradient(to right, #0f9d58, #00796b);
  padding: 16px 0;
  width: 100%;
  position: fixed;
  top: 0;
  left: 0;
  z-index: 100;
}

.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 24px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.cart-title {
  color: white;
  font-size: 22px;
  font-weight: bold;
}

.back-button {
  background-color: white;
  color: #00796b;
  padding: 8px 14px;
  border-radius: 6px;
  text-decoration: none;
  font-weight: bold;
  transition: background 0.3s ease;
}

.back-button:hover {
  background-color: #e6fff4;
}

/* push cart content down */
.cart-list, .promo-section, .total-section {
  margin-top: 80px;
}


.cart-item {
  display: flex;
  align-items: center;
  border-bottom: 1px solid #ccc;
  padding: 12px 0;
}

.cart-item img {
  width: 100px;
  height: 100px;
  object-fit: contain;
  margin-right: 16px;
  border: 1px solid #ddd;
  border-radius: 8px;
}

.item-info {
  flex: 1;
}

.item-info h3 {
  margin: 0 0 4px;
  font-size: 18px;
}

.controls button {
  margin-right: 8px;
  padding: 4px 10px;
  border: none;
  border-radius: 4px;
  background-color: #42b983;
  color: white;
  cursor: pointer;
}

.controls button:hover {
  background-color: #369870;
}

.promo-section {
  margin-top: 24px;
  display: flex;
  flex-direction: column;
  max-width: 400px;
  gap: 10px;
}

.promo-section input {
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.promo-section button {
  background-color: #42b983;
  color: white;
  padding: 8px 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  width: fit-content;
}

.promo-section button:hover {
  background-color: #369870;
}

.total-section {
  margin-top: 24px;
  font-size: 16px;
}
</style>
