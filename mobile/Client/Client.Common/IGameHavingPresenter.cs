using System;
using Common.Common.Models;
using System.Threading.Tasks;

namespace Client.Common
{
	public interface IGameHavingPresenter
	{
		Game GetGame(int index);
		int GameCount();
		void GameTapped(int index);
		bool IsSubscribed(int index);
		Task SubscribeTapped(int index);
	}
}
