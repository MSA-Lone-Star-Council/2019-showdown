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
            int temp = await Task.FromResult<int>(10);
            List<Game> games = new List<Game>();
            if (View != null)
            {
                View.Games = games;
            }
        }
    }
}
