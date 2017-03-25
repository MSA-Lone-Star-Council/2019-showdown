﻿using System;
using System.Collections.Generic;
using Common.Common.Models;

namespace Client.Common
{
	public interface IGameView
	{
		Game Game { get; }
		List<ScoreRecord> ScoreHistory { set; }

		void ShowMessage(string message);
	}
}
