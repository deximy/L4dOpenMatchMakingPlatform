<script setup lang="ts">
import {GameMode, ModeController} from "../apis/ModeController";
import {ref, watch} from "vue";
import * as SignalR from "@microsoft/signalr";

import ModeSelection from "../components/ModeSelection.vue";

import {NButton} from "naive-ui";

const mode_controller = new ModeController();

const mode_list = ref<GameMode[]>([]);
mode_controller.GetModeList().then(
    result => {
        mode_list.value = result;
    }
);

const mode_list_with_status = ref<
    Array<
        GameMode & {
            "selected": boolean,
            "connection": SignalR.HubConnection | null,
        }
    >
>([]);
watch(
    () => mode_list.value,
    (mode_list) => {
        const key_set = new Set(mode_list.map(mode => mode.name));
        mode_list_with_status.value = mode_list_with_status.value.filter(mode_with_status => key_set.has(mode_with_status.name));
        for (const mode of mode_list)
        {
            let mode_index = mode_list_with_status.value.findIndex(mode_with_status => mode_with_status.name === mode.name);
            if (mode_index === -1)
            {
                mode_list_with_status.value.push(
                    {
                        ...mode,
                        "selected": false,
                        "connection": null
                    }
                );
            }
            else
            {
                mode_list_with_status.value[mode_index] = {
                    ...mode_list_with_status.value[mode_index],
                    ...mode,
                    "selected": mode.enabled && mode_list_with_status.value[mode_index].selected
                };
            }
        }
    },
    {
        deep: true
    }
);

const HandleEnterQueue = () => {
    mode_list_with_status.value
        .filter(mode_with_status => mode_with_status.selected == true)
        .map(
            async (mode_with_status) => {
                const connection = new SignalR.HubConnectionBuilder().withUrl(`/${mode_with_status.name}`).build();
                await connection.start();
                mode_with_status.connection = connection;
            }
        );
};
const HandleQuitQueue = () => {
    mode_list_with_status.value
        .filter(mode_with_status => mode_with_status.connection !== null)
        .map(
            async (mode_with_status) => {
                await mode_with_status.connection?.stop();
                mode_with_status.connection = null;
            }
        );
};
</script>

<template>
    <div class="quick-play-view">
        <h1 class="title">选择匹配模式</h1>
        <ModeSelection
            :mode_list="mode_list_with_status"
        />
        <div class="start-play">
            <n-button
                v-if="mode_list_with_status.filter(mode_with_status => mode_with_status.connection !== null).length === 0"
                size="large"
                :disabled="mode_list_with_status.filter(mode_with_status => mode_with_status.selected).length === 0"
                @click="HandleEnterQueue"
            >
                开始
            </n-button>
            <n-button
                v-else
                size="large"
                @click="HandleQuitQueue"
            >
                取消
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
