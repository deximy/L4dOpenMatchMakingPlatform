<script setup lang="ts">
import PlayerProfileTag from "../components/PlayerProfileTag.vue";
import {NButton} from "naive-ui";

import {IsAuthenticated, GetUserIdFromToken} from "../apis/Authentication";
import {Global} from "../apis/Global";
import {ref} from "vue";
import {onMounted} from "vue";
import {useRouter} from "vue-router";
import {useMessage} from "naive-ui";

const is_authenticated = ref(false);
IsAuthenticated().then(result => is_authenticated.value = result);

const user_id = ref("");
GetUserIdFromToken().then(result => user_id.value = result);

const player_profile_element = ref<HTMLElement | null>(null);
onMounted(
    () => {
        player_profile_element.value?.addEventListener(
            "contextmenu",
            (event: MouseEvent) => {
                event.preventDefault();
            },
            true
        );
    }
);

const router = useRouter();
const message = useMessage();
</script>

<template>
    <div class="index">
        <div v-show="!is_authenticated">
            <span>这是一个什么都没有的首页，</span>
            <a :href="`http://localhost:5000/authentication/?redirect=${Global.realm}/callback`">点击跳转到登录页</a>
        </div>
        <div v-show="is_authenticated">
            <div class="menu-bar">
                <div class="site-logo">
                    <h1>Open Match-Making Platform</h1>
                </div>
                <div class="player-profile" ref="player_profile_element">
                    <PlayerProfileTag />
                </div>
            </div>
            <div class="action-list">
                <n-button
                    text
                    @click="router.push({name: 'QuickPlay'})"
                >
                    <h1>Play</h1>
                </n-button>
                <n-button
                    text
                    @click="message.info('暂未开放，敬请期待')"
                >
                    <h1>Career Profile</h1>
                </n-button>
                <n-button
                    text
                    @click="message.info('暂未开放，敬请期待')"
                >
                    <h3>Social</h3>
                </n-button>
                <n-button
                    text
                    @click="message.info('暂未开放，敬请期待')"
                >
                    <h3>Setting</h3>
                </n-button>
                <n-button
                    text
                    @click="message.info('暂未开放，敬请期待')"
                >
                    <h3>Register as a developer</h3>
                </n-button>
            </div>
        </div>
    </div>
</template>

<style scoped>
.menu-bar {
    display: flex;
    align-items: center;
    justify-content: space-between;
}

.site-logo {
    margin-left: 50px;
}
.player-profile {
    margin-right: 50px;
}

.action-list {
    margin-top: 10%;
    margin-left: 50px;
}
.action-list > * {
    display: block;
}
.action-list:deep(h1) {
    margin-bottom: 35px;
}
.action-list:deep(h3) {
    margin-top: 10px;
    margin-bottom: 10px;
}
</style>
