<script setup lang="ts">
import {LobbyDetail} from "../apis/CustomGameManager";

import {NList, NListItem} from "naive-ui";
import PlayerProfileTag from "./PlayerProfileTag.vue";

const props = defineProps<
    {
        lobby_detail: LobbyDetail | null
    }
>();
</script>

<template>
    <div class="match-team-card" v-if="lobby_detail !== null">
        <div class="team-wrapper">
            <p v-text="lobby_detail.team1.name" />
            <n-list hoverable :show-divider="false">
                <n-list-item
                    v-for="(_, index) in Array(lobby_detail.team1.max_player_count).fill(0)"
                >
                    <PlayerProfileTag
                        :user_id="index < lobby_detail.team1.players.length ? lobby_detail.team1.players[index] : null"
                    />
                </n-list-item>
            </n-list>
        </div>
        <div class="vs-span-wrapper">
            <p class="vs-span">VS</p>
        </div>
        <div class="team-wrapper">
            <p v-text="lobby_detail.team2.name" />
            <n-list hoverable :show-divider="false">
                <n-list-item
                    v-for="index in lobby_detail.team2.max_player_count"
                >
                    <PlayerProfileTag
                        :user_id="index < lobby_detail.team2.players.length ? lobby_detail.team2.players[index] : null"
                    />
                </n-list-item>
            </n-list>
        </div>
    </div>
</template>

<style scoped>
.match-team-card {
    padding: 20px;
    background-color: rgba(241, 243, 244, 0.6);

    display: flex;
}

.team-wrapper {
    width: 25vw;
}
.team-wrapper:deep(p) {
    font-size: 2em;
    margin-left: 1em;
}

.vs-span-wrapper {
    display: flex;
    align-items: center;
}

.vs-span {
    transform: skew(-20deg, 0deg);
    font-size: 5em;
    margin-left: 50px;
    margin-right: 50px;
    line-height: 100%;
}
</style>
