using System;
namespace Common.Common.Screens.Login
{
	public interface ILoginView
	{
		void Advance();

		string ErrorText { set; }
	}
}
