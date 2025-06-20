import { createApp } from 'vue'
import App from './App.vue'
import router from './router'  // 👈 add this line

createApp(App)
  .use(router)                // 👈 add this line
  .mount('#app')
