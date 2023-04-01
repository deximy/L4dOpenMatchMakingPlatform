import {Global} from "./Global";

type GameMode = {
    name: string,
    enabled: boolean,
};

class ModeController
{
    private uri_: string = `${Global.realm}/modes`;

    constructor()
    {

    }

    async GetModeList()
    {
        let mode_list = new Array<GameMode>();
        mode_list.push({name: "Hunter1v1", enabled: true});
        mode_list.push({name: "Zonemod1v1", enabled: false});
        mode_list.push({name: "Zonemod4v4", enabled: false});
        mode_list.push({name: "原版包抗4v4", enabled: false});
        mode_list.push({name: "原版4人战役", enabled: false});
        mode_list.push({name: "原版清道夫", enabled: false});
        mode_list.push({name: "Witch Party", enabled: false});

        return mode_list;
    }
}

export type {GameMode};
export {ModeController};