<script setup lang="ts">
import TestImage from "../assets/67280596_p0.png";
</script>

<template>
    <div class="map-card">
        <div class="game-info-img-container">
            <img class="game-info-img" :src="TestImage" alt="">
            <span class="game-info-img-text">介绍图片</span>
        </div>
    </div>
</template>

<style scoped>
.game-info-img-container {
    width: 580px;
    height: 250px;
    overflow: hidden;
    position: relative;
}
.game-info-img {
    position: absolute;
    width: 100%;
    height: 100%;
    object-fit: cover;
}
.game-info-img-text {
    position: absolute;
    z-index: 100;
    color: #fff;
    margin-left: 10px;
    margin-top: 5px;
}
</style>
