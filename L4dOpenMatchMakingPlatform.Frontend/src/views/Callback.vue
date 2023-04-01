<script setup lang="ts">
import {IsAuthenticated} from "../apis/Authentication";
import {useRoute, useRouter} from "vue-router";

let route = useRoute();
let router = useRouter();

localStorage.setItem("token", route.query.jwts!.toString());
IsAuthenticated().then(
    result => {
        if (result)
        {
            console.log("succeeded");
            setTimeout(
                () => {
                    router.push({name: "Index"});
                },
                3000
            );
        }
        else
        {
            console.log("failed");
        }
    }
);
</script>

<template>
    <div>
        <span>正在验证身份，请稍后……</span>
    </div>
</template>

<style scoped>
</style>
