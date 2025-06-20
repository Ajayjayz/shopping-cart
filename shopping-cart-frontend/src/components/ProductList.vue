<template>
  <div>
    <div class="header">
      <div class="container">
        <div class="logo">🧢 <span>My Store</span></div>
        <router-link to="/cart" class="cart-button">🛍️ Cart</router-link>
      </div>
    </div>

    <ul class="product-list">
      <li v-for="product in products" :key="product.id" class="product-card">
        <img :src="product.imageUrl" alt="Product Image" />
        <h3>{{ product.title }}</h3>
        <p>₹{{ product.price }} ({{ product.category }})</p>
        <button @click="addToCart(product)">Add to Cart</button>
      </li>
    </ul>
  </div>
</template>


<script>
import axios from 'axios';

export default {
  name: "ProductList",
  data() {
    return {
      products: []
    };
  },
  methods: {
    addToCart(product) {
      let cart = JSON.parse(localStorage.getItem('cart') || '[]');
      cart.push(product);
      localStorage.setItem('cart', JSON.stringify(cart));
    }
  },
  mounted() {
    axios.get("http://localhost:5290/api/products")
      .then(response => {
        this.products = response.data;
      })
      .catch(() => {
        alert("Failed to load products");
      });
  }
};
</script>

<style scoped>
.header {
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

.logo {
  font-size: 22px;
  font-weight: bold;
  color: white;
  display: flex;
  align-items: center;
  gap: 8px;
}

.cart-button {
  background-color: white;
  color: #00796b;
  padding: 8px 16px;
  border-radius: 6px;
  font-weight: bold;
  text-decoration: none;
  transition: background 0.3s ease;
}

.cart-button:hover {
  background-color: #d9f5ee;
}

.product-list {
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  gap: 20px;
  list-style: none;
  padding: 0;
  margin-top: 80px; /* Push below fixed header */
}
.product-card {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  border: 1px solid #ccc;
  border-radius: 8px;
  padding: 12px;
  height: 360px; /* consistent height */
  text-align: center;
  box-sizing: border-box;
  background-color: white;
}

.product-card img {
  width: 100%;
  height: 150px;
  object-fit: contain;
  margin-bottom: 10px;
  border-radius: 4px;
}

.product-card h3 {
  margin: 10px 0 4px;
  font-size: 18px;
  font-weight: bold;
}

.product-card p {
  margin: 0 0 10px;
  font-size: 15px;
  color: #555;
}

button {
  margin-top: auto;
  background-color: #42b983;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #369870;
}

</style>
