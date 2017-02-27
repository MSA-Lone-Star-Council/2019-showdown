using System;
using System.Threading.Tasks;

namespace Common.Common
{
	public class LoginPresenter : Presenter<ILoginView>
	{
		public enum LoginResult { Success, Failure };

		private string temp { get; set; }

		public async Task SetupView()
		{
			// TODO: Get access token from storage
			string accessToken = await Task.FromResult<string>(temp);

			if (!string.IsNullOrEmpty(accessToken))
			{
				// TODO: Validate against server
				bool isValid = await Task.FromResult<bool>(true);
				if (isValid) view.Advance();
			}

		}

		/// <summary>
		/// Saves the Facebook access token
		/// </summary>
		/// <remarks>
		/// Note: This method will run when the view is null! When the view reappears, SetupView will be called
		/// </remarks>
		/// <returns>The access token.</returns>
		/// <param name="accessToken">Access token.</param>
		public async Task OnAccessToken(string accessToken)
		{
			// TODO: Save access token to storage
			temp = await Task.FromResult(accessToken);

		}

	}
}
