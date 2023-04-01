<script setup lang="ts">
import {GameMode, ModeController} from "../apis/ModeController";
import {ref, watch} from "vue";

import ModeSelection from "../components/ModeSelection.vue";

import {NButton} from "naive-ui";

const mode_controller = new ModeController();

const mode_list = ref<GameMode[]>([]);
mode_controller.GetModeList().then(
    result => {
        mode_list.value = result;
    }
);

const mode_list_with_state = ref<Array<GameMode & {"selected": boolean}>>([]);
watch(
    () => mode_list.value,
    (mode_list) => {
        const key_set = new Set(mode_list.map(mode => mode.name));
        mode_list_with_state.value = mode_list_with_state.value.filter(mode_with_state => key_set.has(mode_with_state.name));
        for (const mode of mode_list)
        {
            if (mode_list_with_state.value.findIndex(mode_with_state => mode_with_state.name === mode.name) === -1)
            {
                mode_list_with_state.value.push({...mode, "selected": false});
            }
        }
    },
    {
        deep: true
    }
);
</script>

<template>
    <div class="quick-play-view">
        <h1 class="title">选择匹配模式</h1>
        <ModeSelection
            :mode_list="mode_list_with_state"
        />
        <div class="start-play">
            <n-button
                size="large"
                :disabled="mode_list_with_state.filter(mode_with_state => mode_with_state.selected).length === 0"
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

    margin-top: 4vh;
}
</style>
