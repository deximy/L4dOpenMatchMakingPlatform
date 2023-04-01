<script setup lang="ts">
import {GameMode, ModeController} from "../apis/ModeController";
import {ref} from "vue";

import ModeSelection from "../components/ModeSelection.vue";


const mode_controller = new ModeController();
const mode_list = ref<GameMode[]>();
mode_controller.GetModeList().then(
    result => {
        mode_list.value = result;
    }
);
</script>

<template>
    <div class="quick-play-view">
        <ModeSelection
            :mode_list="mode_list"
        />
    </div>
</template>

<style scoped>
.quick-play-view {
    width: 100%;
    height: 100%;
}
</style>
