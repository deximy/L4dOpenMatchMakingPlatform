<script setup lang="ts">
import {NCard} from "naive-ui";
import {NIcon} from "naive-ui";
import {NTag} from "naive-ui";

const props = defineProps<
    {
        user_id: string | null
    }
>();
</script>

<template>
    <div class="player-profile">
        <n-card
            v-if="user_id !== null"
            :bordered="false"
            content-style="display: flex; align-items: center;"
        >
            <n-icon :size="24">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 32 32">
                    <path d="M16 4a5 5 0 1 1-5 5a5 5 0 0 1 5-5m0-2a7 7 0 1 0 7 7a7 7 0 0 0-7-7z" fill="currentColor"></path>
                    <path d="M26 30h-2v-5a5 5 0 0 0-5-5h-6a5 5 0 0 0-5 5v5H6v-5a7 7 0 0 1 7-7h6a7 7 0 0 1 7 7z" fill="currentColor"></path>
                </svg>
            </n-icon>
            <span class="user-id" v-text="user_id"></span>
            <n-tag class="user-role" size="small" type="info">管理员</n-tag>
        </n-card>
        <n-card v-else content-style="text-align: center;">
            <span>EMPTY</span>
        </n-card>
    </div>
</template>

<style scoped>
.user-id {
    margin-left: 1em;
}

.user-role {
    margin-left: 5px;
}
</style>
