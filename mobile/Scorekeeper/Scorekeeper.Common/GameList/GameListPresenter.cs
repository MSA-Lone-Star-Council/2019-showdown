using Admin.Common.API;
using Common.Common;
using Common.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorekeeper.Common
{
    public class GameListPresenter : Presenter<IGameListView>
    {
        private readonly AdminRESTClient _client;

        public GameListPresenter(AdminRESTClient client)
        {
            _client = client;
        }

        public async Task OnBegin()
        {
            await UpdateFromServer();
        }

        public void OnClickRow(Game row)
        {
            View.OpenGame(row);
        }

        private async Task UpdateFromServer()
        {
            List<Game> games = await _client.GetScoreKeeperGames();
            if (View != null)
            {
                View.Games = games;
            }
        }
    }
}
