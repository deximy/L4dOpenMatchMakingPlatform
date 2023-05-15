import {createApp} from "vue";
import {createRouter, createWebHistory} from "vue-router";

import App from "./App.vue";
import Index from "./views/Index.vue";
import Callback from "./views/Callback.vue";
import QuickPlayView from "./views/QuickPlayView.vue";
import CustomGameBrowseView from "./views/CustomGameBrowseView.vue";
import CustomGameLobbyView from "./views/CustomGameLobbyView.vue";

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
    {
        name: "BrowseCustomGame",
        path: "/custom-games/browse",
        component: CustomGameBrowseView
    },
    {
        name: "BrowseLobby",
        path: "/custom-games/browse/:id",
        component: CustomGameLobbyView
    }
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
