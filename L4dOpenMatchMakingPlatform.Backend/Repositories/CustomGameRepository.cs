using L4dOpenMatchMakingPlatform.Backend.Models;

namespace L4dOpenMatchMakingPlatform.Backend.Repositories
{
    public class CustomGameRepository
    {
        private readonly IList<CustomGameModel> custom_game_list_;

        public CustomGameRepository()
        {
            custom_game_list_ = new List<CustomGameModel>();
        }

        public List<Guid> QueryLobbyIds()
        {
            return custom_game_list_.Select(game => game.lobby_id).ToList();
        }

        public CustomGameModel Query(Guid lobby_id)
        {
            return custom_game_list_.First(game => game.lobby_id == lobby_id);
        }

        public IEnumerable<CustomGameModel> Query(Func<CustomGameModel, bool> where_expression)
        {
            return custom_game_list_.Where(where_expression);
        }

        public IEnumerable<CustomGameModel> Query(Func<CustomGameModel, bool> where_expression, int? page_index = null, int page_size = 20)
        {
            return Query(where_expression).Skip((page_index - 1) * page_size ?? 0).Take(page_size);
        }

        public CustomGameModel Create(CustomGameModel new_custom_game)
        {
            custom_game_list_.Add(new_custom_game);
            return custom_game_list_.First(i => i.lobby_id == new_custom_game.lobby_id);
        }

        public CustomGameModel Update(CustomGameModel custom_game)
        {
            var index = custom_game_list_.IndexOf(custom_game_list_.First(i => i.lobby_id == custom_game.lobby_id));
            custom_game_list_[index] = custom_game;
            return custom_game_list_.First(i => i.lobby_id == custom_game.lobby_id);
        }
    }
}
