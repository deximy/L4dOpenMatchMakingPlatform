<script setup lang="ts">
import {GameMode} from "../apis/ModeController";

import {NCard} from "naive-ui";
import {NCheckbox} from "naive-ui";
import {NGrid, NGridItem} from "naive-ui";

const props = defineProps(
    {
        mode_list: Array<GameMode>
    }
);

</script>

<template>
    <div class="mode-selection">
        <n-grid
            cols="1 600:2 900:3 1200:4 1500:5 1800:6"
            :x-gap="12"
            :y-gap="12"
        >
            <n-grid-item
                v-for="mode in mode_list"
            >
                <n-card
                    class="mode-card"
                    content-style="display: flex; justify-content: center; align-items: center;"
                    hoverable
                >
                    <span v-text="mode.name"/>
                    <template #action>
                        <n-checkbox />
                    </template>
                </n-card>
            </n-grid-item>
        </n-grid>

    </div>
</template>

<style scoped>
.mode-card {
    width: 300px;
    height: 300px;

    text-align: center;
}
</style>
