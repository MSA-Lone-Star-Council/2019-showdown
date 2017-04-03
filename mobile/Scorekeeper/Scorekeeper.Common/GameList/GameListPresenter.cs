using Admin.Common.API;
using Admin.Common.API.Entities;
using Common.Common;
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
            var token = await _client.GetToken(View.AccessToken);
            _client.Token = token;

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
