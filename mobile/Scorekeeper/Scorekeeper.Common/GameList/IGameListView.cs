using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Common.Models;


namespace Scorekeeper.Common
{
    public interface IGameListView
    {
        string Token { get; set; }

        List<Game> Games { set; }

        void OpenGame(Game game);
    }
}
