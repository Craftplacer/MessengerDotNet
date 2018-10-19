using MessengerAPI;

namespace MessengerDotNet
{
	public class MessengerService
	{
		internal IMessengerService _service;

		internal MessengerService(IMessengerService service) => this._service = service;

		public string ServiceId => _service.ServiceId;
		public string ServiceName => _service.ServiceName;

		public string FriendlyName => _service.MyFriendlyName;
		public string SignInName => _service.MySigninName;
		public MessengerStatus Status => MessengerAPI.ToStatus(_service.MyStatus);
	}
}