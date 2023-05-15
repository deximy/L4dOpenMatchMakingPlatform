<script setup lang="ts">
import {ref} from "vue";
import LobbyList from "../components/LobbyList.vue";
import {CustomGameManager} from "../apis/CustomGameManager";

import {NInputGroup, NInput, NButton} from "naive-ui";

interface LobbyListRef
{
    RefreshList: () => void,
}
const lobby_list_ref = ref<LobbyListRef | null>(null);

const custom_game_manager = new CustomGameManager();

const HandleCreateLobby = () => {
    custom_game_manager.CreateLobby().then(() => lobby_list_ref.value?.RefreshList());
};
</script>

<template>
    <div class="custom-game-view">
        <h1 class="title">Browse Custom Games</h1>
        <div class="tools-bar">
            <n-input-group>
                <n-button type="primary">
                    Filter
                </n-button>
                <n-input />
                <n-button
                    type="primary"
                    ghost
                    @click="HandleCreateLobby"
                >
                    创建大厅
                </n-button>
            </n-input-group>
        </div>
        <LobbyList ref="lobby_list_ref" class="lobby-list"/>
    </div>
</template>

<style scoped>
.title {
    transform: skew(-20deg, 0deg);
}
.tools-bar {
    float: right;
}
.lobby-list {
    clear: both;
    padding-top: 3vh;
}
</style>
