<script setup lang="ts">
import {NMessageProvider} from "naive-ui";
</script>

<template>
    <n-message-provider>
        <router-view />
    </n-message-provider>
</template>

<style>
body {
    width: 100%;
    height: 100%;
    margin: 0;
}

#app {
    width: 100%;
    height: 100%;
    padding: 1vh 1vw 1vh 1vw;
    box-sizing: border-box;
}
</style>
