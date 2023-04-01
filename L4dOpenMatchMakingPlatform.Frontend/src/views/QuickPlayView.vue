<script setup lang="ts">
import {GameMode, ModeController} from "../apis/ModeController";
import {ref} from "vue";

import ModeSelection from "../components/ModeSelection.vue";

import {NButton} from "naive-ui";

const mode_controller = new ModeController();

const mode_list = ref<GameMode[]>();
mode_controller.GetModeList().then(
    result => {
        mode_list.value = result;
    }
);

const selected_mode_list = ref<GameMode[]>(new Array<GameMode>());
</script>

<template>
    <div class="quick-play-view">
        <h1 class="title">选择匹配模式</h1>
        <ModeSelection
            :mode_list="mode_list"
        />
        <div class="start-play">
            <n-button
                size="large"
                :disabled="selected_mode_list.length === 0"
            >
                开始
            </n-button>
        </div>
    </div>
</template>

<style scoped>
.quick-play-view {
    width: 100%;
    height: 100%;
}

.title {
    text-align: center;
}

.start-play {
    display: flex;
    justify-content: center;
    align-items: center;
}
</style>
