<script setup lang="ts">
import {LobbyDetail} from "../apis/CustomGameManager";

import MapCard from "./MapCard.vue";

const props = defineProps<
    {
        lobby_detail: LobbyDetail | null
    }
>();
</script>

<template>
    <div class="lobby-basic-information-card" v-if="lobby_detail !== null">
        <MapCard />
        <h2 class="lobby-name" v-text="lobby_detail.name" />
        <p class="lobby-owner" v-text="lobby_detail.owner == null ? '系统分配的对局' : lobby_detail.owner" />
        <p class="lobby-description" v-text="lobby_detail.description == null ? '无介绍' : lobby_detail.description" />
    </div>
</template>

<style scoped>
.lobby-basic-information-card {
    padding: 20px;
    background-color: rgba(241, 243, 244, 0.6);
}
</style>
