import {Global} from "./Global";

type TeamSummary = {
    current_player_count: number,
    max_player_count: number,
}

type TeamDetail = {
    team_id: string,
    team_name: string,
    current_players: string[],
    max_player_count: number,
};

type LobbySummary = {
    id: string,
    name: string,
    type: string,
    team1: TeamSummary,
    team2: TeamSummary,
    team3: TeamSummary,
};

type LobbyDetail = {
    id: string,
    name: string,
    description: string,
    type: string,
    owner: string,
    endpoint: string | null,
    team1: TeamDetail,
    team2: TeamDetail,
    team3: TeamDetail,
    created_at: string,
    ended_at: string | null,
};

type QueryLobbySummaryListResponseDTO = {
    lobby_summary_list: LobbySummary[],
    page_index: number,
    page_size: number,
    page_count: number,
    total_count: number,
};

class CustomGameManager
{
    private uri_: string = `${Global.realm}/apis/custom-games`;

    public constructor()
    {
    }

    public async CreateLobby(): Promise<LobbyDetail>
    {
        let response = await fetch(
            this.uri_,
            {
                method: "POST",
                headers: new Headers(
                    {
                        "Content-Type": "application/json"
                    }
                ),
                body: JSON.stringify(
                    {
                        "lobby_type": "string"
                    }
                )
            }
        );
        return await response.json() as LobbyDetail;
    }

    public async QueryLobbySummaryList(page_index: number, page_size: number): Promise<QueryLobbySummaryListResponseDTO>
    {
        let response = await fetch(
            `${this.uri_}?page_index=${page_index}&page_size=${page_size}`,
            {
                method: "GET",
            }
        );
        return await response.json();
    }

    public async QueryLobbyDetail(id: string): Promise<LobbyDetail>
    {
        let response = await fetch(
            `${this.uri_}/${id}`,
            {
                method: "GET",
            }
        );
        return response.json();
    }
}

export type {TeamSummary, TeamDetail, LobbySummary, LobbyDetail, QueryLobbySummaryListResponseDTO};
export {CustomGameManager};
