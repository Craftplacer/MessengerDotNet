using MessengerAPI;
using System;
using System.Collections.Generic;

namespace MessengerDotNet
{
	//TODO: Telephone numbers
	//TODO: Mailing
	/// <summary>
	/// API for communicating with Windows Live Messenger.
	/// </summary>
	public static partial class MessengerAPI
	{
		static MessengerAPI() => Messenger = new Messenger();
		
		/// <summary>
		/// The Messenger class used by the .NET wrapper.
		/// </summary>
		public static Messenger Messenger { get; private set; }

		public static IMessengerService[] Services
		{
			get
			{
				var list = new List<IMessengerService>();
				var services = (IMessengerServices)Messenger.Services;
				for (int i = 0; i < services.Count; i++)
				{
					list.Add(services.Item(i));
				}
                
				return list.ToArray();
			}
		}

		public static MessengerService PrimaryService => ((IMessengerServices)Messenger.Services).PrimaryService;

		/// <summary>
		/// Views the profile of another <see cref="MessengerContact"/>,
		/// may cause crashes.
		/// </summary>
		public static void ViewProfile(this MessengerContact contact) => Messenger.ViewProfile(contact);

		/// <summary>
		/// Pages another <see cref="MessengerContact"/>
		/// </summary>
		/// <param name="contact">The contact to be paged to.</param>
		public static void Page(this MessengerContact contact)
		{
			if (contact.CanPage)
			{
				Messenger.Page(contact._contact);
			}
			else
			{
				throw new Exception("This contact can't be paged.");
			}
		}

		#region Groups

		public static MessengerGroups Groups { get; } = new MessengerGroups();

		public static void CreateGroup(string name, MessengerService service) => Messenger.CreateGroup(name, service._service);

		public static void RemoveGroup(MessengerGroup group) => Messenger.MyGroups.Remove(group._group);

		#endregion Groups

		#region Contacts

		#region Properties

		public static MessengerContact Me => new MessengerContact(Messenger.GetContact(Messenger.MySigninName, Messenger.MyServiceId));

		public static MessengerContacts Contacts { get; } = new MessengerContacts();

		#endregion Properties

		#region Methods

		/// <summary>
		/// Opens the add-contact window with pre-set name
		/// </summary>
		public static void AddContact(string Email) => Messenger.AddContact(((IMessengerWindow)Messenger.Window).HWND, Email);

		/// <summary>
		/// Retrieves a <see cref="MessengerContact"/> by it's <paramref name="SignInName"/> and <paramref name="ServiceId"/>.
		/// </summary>
		/// <param name="SignInName">The sign in name of the contact</param>
		/// <param name="ServiceId">The service id of the contact</param>
		/// <returns>The retrieved <see cref="MessengerContact"/></returns>
		public static MessengerContact GetContact(string SignInName, string ServiceId)
		{
			return new MessengerContact((IMessengerContact)Messenger.GetContact(SignInName, ServiceId));
		}

		/// <summary>
		/// Opens up the find contact dialog, pre-set with values
		/// </summary>
		public static void FindContact(string firstName, string lastName)
		{
			Messenger.FindContact(Messenger.Window,
									   firstName,
									   lastName);
		}

		#endregion Methods

		#endregion Contacts

		#region Windows

		/// <summary>
		/// Opens a new instant message window.
		/// (INFO) When called the window gets focused, so it is recommended to keep the conversation window object.
		/// </summary>
		public static MessengerConversationWindow OpenIMWindow(this MessengerContact contact) => (MessengerConversationWindow)Messenger.InstantMessage(contact._contact);

		public static MessengerWindow Window => new MessengerWindow(Messenger.Window);

		#endregion Windows

		#region Signing In & Out

		/// <summary>
		/// Initiates signin dialog and populates signin name and password edit boxes.
		/// </summary>
		/// <param name="hwndParent">The window handle that should be used as parent window.</param>
		public static void SignIn(string signInName, string password) => Messenger.Signin(Messenger.Window, signInName, password);

		public static void SignOut() => Messenger.Signout();

		#endregion Signing In & Out

		#region Calling

		public static void StartVoiceCall(MessengerContact contact) => Messenger.StartVoice(contact._contact);

		public static void StartVideoCall(MessengerContact contact) => Messenger.StartVideo(contact._contact);

		#endregion Calling

		#region Internal Functions

		/// <summary>
		/// Converts <paramref name="contacts"/> to a more friendly <see cref="MessengerContact[]"/>.
		/// </summary>
		/// <param name="contacts">The contacts that should be converted</param>
		/// <returns>Converted Contacts</returns>
		internal static MessengerContact[] ToArray(IMessengerContacts contacts)
		{
			var list = new List<MessengerContact>();
			for (int i = 0; i < contacts.Count; i++)
			{
				list.Add(new MessengerContact(contacts.Item(i)));
			}
			return list.ToArray();
		}

		/// <summary>
		/// Converts <see cref="MISTATUS"/> to <see cref="MessengerStatus"/>
		/// </summary>
		internal static MessengerStatus ToStatus(MISTATUS status)
		{
			switch (status)
			{
				// Online
				case MISTATUS.MISTATUS_ONLINE: return MessengerStatus.Online;
				//Away
				case MISTATUS.MISTATUS_IDLE:
				case MISTATUS.MISTATUS_AWAY: return MessengerStatus.Away;
				case MISTATUS.MISTATUS_BE_RIGHT_BACK: return MessengerStatus.BeRightBack;
				case MISTATUS.MISTATUS_OUT_TO_LUNCH: return MessengerStatus.OutToLunch;
				//Busy
				case MISTATUS.MISTATUS_BUSY: return MessengerStatus.Busy;
				case MISTATUS.MISTATUS_ON_THE_PHONE: return MessengerStatus.OnThePhone;
				//Offline
				case MISTATUS.MISTATUS_OFFLINE: return MessengerStatus.Offline;
				case MISTATUS.MISTATUS_INVISIBLE: return MessengerStatus.Invisible;
				default: return MessengerStatus.Unknown;
			}
		}

		/// <summary>
		/// Converts <see cref="MessengerStatus"/> to <see cref="MISTATUS"/>
		/// </summary>
		internal static MISTATUS ToMIStatus(MessengerStatus status)
		{
			switch (status)
			{
				case MessengerStatus.Online: return MISTATUS.MISTATUS_ONLINE;
				case MessengerStatus.Away: return MISTATUS.MISTATUS_AWAY;
				case MessengerStatus.BeRightBack: return MISTATUS.MISTATUS_BE_RIGHT_BACK;
				case MessengerStatus.OutToLunch: return MISTATUS.MISTATUS_OUT_TO_LUNCH;
				case MessengerStatus.Busy: return MISTATUS.MISTATUS_BUSY;
				case MessengerStatus.OnThePhone: return MISTATUS.MISTATUS_ON_THE_PHONE;
				case MessengerStatus.Offline: return MISTATUS.MISTATUS_OFFLINE;
				case MessengerStatus.Invisible: return MISTATUS.MISTATUS_INVISIBLE;
				default:
				case MessengerStatus.Unknown: return MISTATUS.MISTATUS_UNKNOWN;
			}
		}

		#endregion Internal Functions

		#region Miscellanous

		/// <summary>
		/// Initiates the audio tuning wizard.
		/// </summary>
		public static void MediaWizard() => Messenger.MediaWizard(Messenger.Window);

		/// <summary>
		/// Version from Messenger.
		/// </summary>
		public static int Version => Messenger.Property[MMESSENGERPROPERTY.MMESSENGERPROP_VERSION];

		/// <summary>
		///     Returns the path to the receive directory.
		/// </summary>
		public static string ReceiveFileDirectory => Messenger.ReceiveFileDirectory;

		#endregion Miscellanous
	}
}