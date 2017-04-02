using Admin.Common.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Scorekeeper.Common
{
    public interface IGameListView
    {
        List<Game> Games { set; }

        void OpenGame(Game game);
    }
}
