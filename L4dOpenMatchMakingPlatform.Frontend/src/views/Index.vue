<script setup lang="ts">
import {IsAuthenticated, GetUserIdFromToken} from "../apis/Authentication";
import {ref} from "vue";

const is_authenticated = ref(false);
IsAuthenticated().then(result => is_authenticated.value = result);

const user_id = ref("");
GetUserIdFromToken().then(result => user_id.value = result);
</script>

<template>
    <div class="index">
        <div v-if="!is_authenticated">
            <span>这是一个什么都没有的首页，</span>
            <a href="http://localhost:5173/authentication/?redirect=http://localhost:5174/callback">点击跳转到登录页</a>
        </div>
        <div v-else>
            <span>这是一个什么都没有的首页，你已登录，ID：</span><span v-text="`${user_id}`"></span>
        </div>
    </div>
</template>

<style scoped>
</style>
