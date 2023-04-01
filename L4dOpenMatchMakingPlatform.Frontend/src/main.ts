import {createApp} from "vue";
import {createRouter, createWebHistory} from "vue-router";

import App from "./App.vue";
import Index from "./views/Index.vue";
import Callback from "./views/Callback.vue";
import QuickPlayView from "./views/QuickPlayView.vue";

const routes = [
    {
        name: "Index",
        path: "/",
        component: Index
    },
    {
        name: "Callback",
        path: "/callback",
        component: Callback
    },
    {
        name: "QuickPlay",
        path: "/quick-play",
        component: QuickPlayView
    },
];

const router = createRouter(
    {
        history: createWebHistory(),
        routes
    }
);

const app = createApp(App);
app.use(router);
app.mount("#app");
