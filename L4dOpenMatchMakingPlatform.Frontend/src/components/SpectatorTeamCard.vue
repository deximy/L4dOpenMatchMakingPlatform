<script setup lang="ts">
import {LobbyDetail} from "../apis/CustomGameManager";

import {NGrid, NGridItem} from "naive-ui";
import PlayerProfileTag from "./PlayerProfileTag.vue";

const props = defineProps<
    {
        lobby_detail: LobbyDetail | null
    }
>();
</script>

<template>
    <div class="spectator-team-card" v-if="lobby_detail !== null">
        <p class="spectator-span">SPECTATORS</p>
        <n-grid cols="1 330:2 660:3 990:4" :x-gap="30">
            <n-grid-item v-for="index in lobby_detail.team3.max_player_count">
                <PlayerProfileTag
                    :user_id="index < lobby_detail.team3.players.length ? lobby_detail.team3.players[index] : null"
                />
            </n-grid-item>
        </n-grid>
    </div>
</template>

<style scoped>
.spectator-team-card {
    padding: 20px;
    background-color: rgba(241, 243, 244, 0.6);
}

.spectator-span {
    font-weight: bold;
    margin-left: 2em;
}
</style>
