using MessengerAPI;

namespace MessengerDotNet
{
	public class MessengerGroup
	{
		internal IMessengerGroup _group;

		public MessengerGroup(IMessengerGroup group) => this._group = group;

		public string Name
		{
			get => _group.Name;
			set => _group.Name = value;
		}

		public MessengerService Service => new MessengerService(this._group.Service);

		#region Contacts

		public MessengerContact[] Contacts => MessengerAPI.ToArray(this._group.Contacts);

		/// <summary>
		/// Adds a contact to this <see cref="MessengerGroup"/>
		/// </summary>
		public void AddContact(MessengerContact c) => _group.AddContact(c._contact);

		/// <summary>
		/// Removes a contact from this <see cref="MessengerGroup"/>
		/// </summary>
		public void RemoveContact(MessengerContact c) => _group.RemoveContact(c._contact);

		#endregion Contacts
	}
}