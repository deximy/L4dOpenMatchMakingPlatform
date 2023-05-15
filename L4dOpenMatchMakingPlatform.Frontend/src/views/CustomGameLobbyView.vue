<script setup lang="ts">
import {useRoute as UseRoute} from "vue-router";
import {CustomGameManager} from "../apis/CustomGameManager";
import {LobbyDetail} from "../apis/CustomGameManager";
import {ref} from "vue";
import {GetUserIdFromToken} from "../apis/Authentication";

import LobbyBasicInfoCard from "../components/LobbyBasicInfoCard.vue";
import MatchTeamCard from "../components/MatchTeamCard.vue";
import SpectatorTeamCard from "../components/SpectatorTeamCard.vue";
import {NButton} from "naive-ui";

const route = UseRoute();
const custom_game_manager = new CustomGameManager();

const lobby_detail = ref<LobbyDetail | null>(null);
custom_game_manager
    .QueryLobbyDetail(route.params.id as string)
    .then(
        (result) => {
            lobby_detail.value = result;
        }
    );

let local_user_id: string | null = null;
GetUserIdFromToken().then((id) => local_user_id = id);
const IsCurrentUserInTeam = () => {
    if (local_user_id === null)
    {
        return false;
    }

    return (
        lobby_detail.value?.team1.players.includes(local_user_id)
        || lobby_detail.value?.team2.players.includes(local_user_id)
        || lobby_detail.value?.team3.players.includes(local_user_id)
    ) ?? false;
};
const HandleJoin = async () => {
    if (lobby_detail.value === null)
    {
        return;
    }

    await custom_game_manager.JoinTeam(lobby_detail.value.id, lobby_detail.value.team1.id);
    location.reload();
};
const HandleLeave = async () => {
    if (lobby_detail.value === null)
    {
        return;
    }

    await custom_game_manager.LeaveTeam(lobby_detail.value.id, lobby_detail.value.team1.id);
    location.reload();
};
</script>

<template>
    <div class="lobby-view">
        <h1 class="title">View Lobby</h1>
        <div class="info-display-container">
            <LobbyBasicInfoCard :lobby_detail="lobby_detail"/>
            <div class="player-display-container">
                <MatchTeamCard :lobby_detail="lobby_detail" />
                <SpectatorTeamCard style="margin-top: 50px" :lobby_detail="lobby_detail" />
            </div>
        </div>
        <div class="action-bar" v-if="lobby_detail !== null">
            <n-button v-show="!IsCurrentUserInTeam()" @click="HandleJoin">Join Lobby</n-button>
            <n-button v-show="IsCurrentUserInTeam()" @click="HandleLeave">Leave Lobby</n-button>
            <n-button :disabled="local_user_id !== lobby_detail.owner">Start Game</n-button>
        </div>
    </div>
</template>

<style scoped>
.title {
    transform: skew(-20deg, 0deg);
}

.info-display-container {
    display: flex;
    justify-content: space-around;
}

.player-display-container {
    display: flex;
    flex-direction: column;
}

.action-bar {
    display: flex;
    justify-content: space-around;
    align-items: center;

    margin-top: 20px;
}
</style>
