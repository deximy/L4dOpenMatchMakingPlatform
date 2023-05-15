<script setup lang="ts">
import {ref, reactive} from "vue";
import {useRouter as UseRouter} from "vue-router";
import {CustomGameManager, QueryLobbySummaryListResponseDTO} from "../apis/CustomGameManager";

import {NDataTable} from "naive-ui";

const router = UseRouter();

type LobbyViewModel = {
    id: string,
    name: string,
    type: string,
    teams: string,
    player_count: string,
}

const columns = [
    {
        title: "NAME",
        key: "name",
    },
    {
        title: "CURRENT GAME",
        key: "type",
    },
    {
        title: "TEAMS",
        key: "teams",
    },
    {
        title: "PLAYER COUNT",
        key: "player_count"
    }
];
const pagination = reactive(
    {
        page: 1,
        pageCount: 1,
        itemCount: 1,
        pageSize: 5,
        showSizePicker: true,
        pageSizes: [3, 5, 7],
    }
);
const UpdateListByQueryResult = (dto: QueryLobbySummaryListResponseDTO) => {
    data.value.length = 0;
    dto.lobby_summary_list.forEach(
        (summary) => {
            data.value.push(
                {
                    id: summary.id,
                    name: summary.name,
                    type: summary.type,
                    teams: `${summary.team1.max_player_count} VS ${summary.team2.max_player_count}`,
                    player_count: `${summary.team1.current_player_count + summary.team2.current_player_count + summary.team3.current_player_count}/${summary.team1.max_player_count + summary.team2.max_player_count + summary.team3.max_player_count}`,
                }
            );
        }
    );
    pagination.page = dto.page_index;
    pagination.pageSize = dto.page_size;
    pagination.pageCount = dto.page_count;
    pagination.itemCount = dto.total_count;
};
const HandlePageChange = (current_page: number) => {
    custom_game_manager
        .QueryLobbySummaryList(current_page, pagination.pageSize)
        .then(UpdateListByQueryResult);
};
const HandlePageSizeChange = (current_page_size: number) => {
    custom_game_manager
        .QueryLobbySummaryList(1, current_page_size)
        .then(UpdateListByQueryResult);
};
const RefreshList = () => {
    custom_game_manager
        .QueryLobbySummaryList(pagination.page, pagination.pageSize)
        .then(UpdateListByQueryResult);
};
const data = ref<LobbyViewModel[]>([]);
const custom_game_manager = new CustomGameManager();

RefreshList();

const row_props = (row_data: LobbyViewModel, row_index : number) => {
    return {
        style: 'cursor: pointer;',
        onDblclick: () => {
            router.push({name: "BrowseLobby", params: {id: row_data.id}});
        }
    }
};

defineExpose(
    {
        RefreshList,
    }
);
</script>

<template>
    <div class="lobby-list">
        <n-data-table
            :columns="columns"
            :data="data"
            :bordered="false"
            :row-props="row_props"
            :pagination="pagination"
            remote
            @update:page="HandlePageChange"
            @update:page-size="HandlePageSizeChange"
        />
    </div>
</template>

<style scoped>
</style>
