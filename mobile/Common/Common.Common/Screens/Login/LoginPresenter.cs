using System;
using System.Threading.Tasks;

namespace Common.Common.Screens.Login
{
	public class LoginPresenter : Presenter<ILoginView>
	{
	    private static string BackendToken = "BACKEND_TOKEN";

	    private ShowdownRESTClient _client;
	    private IStorage _storage;

	    public LoginPresenter(ShowdownRESTClient client, IStorage storage)
	    {
	        _client = client;
	        _storage = storage;
	    }

	    public async Task SetupView()
	    {
            string backendToken = await _storage.GetStringAsync(BackendToken, "");

	        if (!string.IsNullOrEmpty(backendToken))
	        {
	            _client.Token = backendToken;
	            View.Advance();
	        }

	        // Exchange the facebook access token (if it exists, else do nothing)

	        string facebookAccessToken = await _storage.GetStringAsync("FACEBOOK_TOKEN", "");
	        if (!string.IsNullOrEmpty(facebookAccessToken))
	        {
	            backendToken = await _client.GetToken(facebookAccessToken);
	            await _storage.SaveAsync(BackendToken, backendToken);
	            _client.Token = backendToken;
				if(View != null) View.Advance();
	        }
	    }
	}
}