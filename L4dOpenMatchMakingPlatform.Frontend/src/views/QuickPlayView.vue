<script setup lang="ts">
import {GameMode, ModeController} from "../apis/ModeController";
import {ref, watch} from "vue";
import * as SignalR from "@microsoft/signalr";

import ModeSelection from "../components/ModeSelection.vue";

import {NButton} from "naive-ui";
import {useMessage as UseMessage, MessageReactive} from "naive-ui";

type GameModeWithStatus = GameMode & {
    "selected": boolean,
    "connection": SignalR.HubConnection | null,
    "message_box": MessageReactive | null,
    "start_at": number | null,
};

const mode_controller = new ModeController();

const mode_list = ref<GameMode[]>([]);
mode_controller.GetModeList().then(
    result => {
        mode_list.value = result;
    }
);

const mode_list_with_status = ref<GameModeWithStatus[]>([]);
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
                        "connection": null,
                        "message_box": null,
                        "start_at": null,
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

const message = UseMessage();
const QuitQueue = async (mode_with_status: GameModeWithStatus) => {
    await mode_with_status.connection?.stop();
    mode_with_status.connection = null;
    mode_with_status.selected = false;
    mode_with_status.message_box?.destroy();
    mode_with_status.message_box = null;
    mode_with_status.start_at = null;
};
const HandleEnterQueue = () => {
    (mode_list_with_status.value as GameModeWithStatus[])
        .filter(mode_with_status => mode_with_status.selected)
        .map(
            async (mode_with_status) => {
                const connection = new SignalR.HubConnectionBuilder().withUrl(`/${mode_with_status.name}`).build();
                await connection.start();
                mode_with_status.connection = connection;

                mode_with_status.message_box = message.info(
                    `正在队列中…… 模式：${mode_with_status.name} 排队时间：0:00`,
                    {
                        duration: 0,
                        closable: true,
                        onClose: () => {
                            QuitQueue(mode_with_status);
                        }
                    }
                );

                mode_with_status.start_at = new Date().getTime();
                const UpdateContent = () => {
                    if (mode_with_status.message_box === null || mode_with_status.start_at === null)
                    {
                        return;
                    }
                    let span = new Date(new Date().getTime() - mode_with_status.start_at);
                    let span_seconds = span.getUTCSeconds();
                    let span_minutes = span.getUTCMinutes();
                    let span_hours = span.getUTCHours();
                    let formatted_span = span_hours > 0 ? `${span_hours.toString().padStart(2, "0") + ":"}` : "";
                    formatted_span += span_minutes.toString() + ":";
                    formatted_span += span_seconds.toString().padStart(2, "0");
                    mode_with_status.message_box.content = `正在队列中…… 模式：${mode_with_status.name} 排队时间：${formatted_span}`;
                    setTimeout(
                        UpdateContent,
                        100
                    );
                };
                setTimeout(UpdateContent, 1000);
            }
        );
};
const HandleQuitQueue = () => {
    (mode_list_with_status.value as GameModeWithStatus[])
        .filter(mode_with_status => mode_with_status.connection !== null)
        .map(
            async (mode_with_status) => {
                await QuitQueue(mode_with_status);
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
