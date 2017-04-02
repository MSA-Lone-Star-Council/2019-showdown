using System;
using System.Threading.Tasks;
using Admin.Common.API;

namespace Common.Common.Screens.Login
{
	public class LoginPresenter : Presenter<ILoginView>
	{
	    private static string BackendToken = "BACKEND_TOKEN";

	    private AdminRESTClient _client;
	    private IStorage _storage;

		public LoginPresenter(AdminRESTClient client, IStorage storage)
	    {
	        _client = client;
	        _storage = storage;
	    }

	    public async Task SetupView()
	    {
            string backendToken = _storage.GetString(BackendToken, "");

	        if (!string.IsNullOrEmpty(backendToken))
	        {
	            _client.Token = backendToken;
	            View.Advance();
	        }

	        // Exchange the facebook access token (if it exists, else do nothing)

	        string facebookAccessToken = _storage.GetString("FACEBOOK_TOKEN", "");
	        if (!string.IsNullOrEmpty(facebookAccessToken))
	        {
	            backendToken = await _client.GetToken(facebookAccessToken);
	            _storage.Save(BackendToken, backendToken);
	            _client.Token = backendToken;
				if(View != null) View.Advance();
	        }
	    }
	}
}