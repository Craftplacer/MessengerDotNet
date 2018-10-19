using MessengerAPI;
using System;

namespace MessengerDotNet
{
	public class MessengerContact
	{
		internal IMessengerContact _contact;

		internal MessengerContact(IMessengerContact contact) => this._contact = contact;

		/// <summary>
		/// If the <see cref="MessengerContact"/> is yourself.
		/// </summary>
		public bool IsSelf => _contact.IsSelf;

		/// <summary>
		/// If the <see cref="MessengerContact"/> is blocked.
		/// </summary>
		public bool IsBlocked => _contact.Blocked;

		/// <summary>
		/// If the <see cref="MessengerContact"/> can page.
		/// </summary>
		public bool CanPage => _contact.CanPage;

		/// <summary>
		/// The service name of the <see cref="MessengerContact"/>
		/// </summary>
		public string ServiceName => _contact.ServiceName;

		/// <summary>
		/// The service id of the <see cref="MessengerContact"/>
		/// </summary>
		public string ServiceId => _contact.ServiceId;

		/// <summary>
		/// The name used to sign in.
		/// </summary>
		public string SignInName => _contact.SigninName;

		/// <summary>
		/// The email of the <see cref="MessengerContact"/>
		/// </summary>
		public string Email => (string)_contact.Property[MCONTACTPROPERTY.MCONTACTPROP_EMAIL];

		/// <summary>
		/// The name of the <see cref="MessengerContact"/>
		/// </summary>
		public string Name => _contact.FriendlyName;

		public string GetPhoneNumber(MessengerPhoneType phoneType) => _contact.PhoneNumber[(MPHONE_TYPE)phoneType];

		public string UserTilePath
		{
			get
			{
				MessengerAPI.Messenger.FetchUserTile(this._contact.SigninName,0,0);
				var value = _contact.Property[MCONTACTPROPERTY.MCONTACTPROP_USERTILE_PATH];
				;
				return value;
			}
			
		}

		public MessengerStatus Status
		{
			get => MessengerAPI.ToStatus(_contact.Status);
			set
			{
				if (this.IsSelf)
				{
					MessengerAPI.Messenger.MyStatus = MessengerAPI.ToMIStatus(value);
				}
				else
				{
					throw new Exception("Setting someone else's status is not allowed.");
				}
			}
		}
	}
}