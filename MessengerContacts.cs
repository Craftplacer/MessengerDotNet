using MessengerAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MessengerDotNet
{
    public class MessengerContacts : ICollection<MessengerContact>
    {
        /// <summary>
        /// Caches instances of <see cref="MessengerContact"/>s to reduce RAM usage.
        /// </summary>
        public Dictionary<string, MessengerContact> CachedContacts { get; }

        public bool IsReadOnly => false;

		public MessengerContacts() => this.CachedContacts = new Dictionary<string, MessengerContact>();

		int ICollection<MessengerContact>.Count => ((IMessengerContacts)MessengerAPI.Messenger.MyContacts).Count;

        private string GetId(IMessengerContact contact) => contact.ServiceId + contact.SigninName;

        public IEnumerator<MessengerContact> GetEnumerator()
        {
            foreach (IMessengerContact contact in MessengerAPI.Messenger.MyContacts)
            {
                var id = GetId(contact as IMessengerContact);
                if (!this.CachedContacts.ContainsKey(id))
                {
                    this.CachedContacts[id] = new MessengerContact(contact as IMessengerContact);
                }
                yield return this.CachedContacts[id];
            }
        }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// See <see cref="MessengerAPI.AddContact(string)"/>
		/// </summary>
		/// <param name="contact"></param>
		public void Add(MessengerContact contact) => MessengerAPI.AddContact(contact.Email);

        public void Clear()
        {
            foreach (MessengerContact contact in this)
            {
                this.Remove(contact);
            }
        }

        public bool Contains(MessengerContact contact)
        {
            foreach (var c in this)
            {
                if (c.SignInName == contact.SignInName && c.ServiceId == contact.ServiceId)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(MessengerContact[] array, int arrayIndex)
        {
            if (array == null)
			{
				throw new ArgumentNullException("array");
			}

			var i = 0;
			foreach (var item in this)
			{
				array[i + arrayIndex] = item;
				i++;
			}
        }

        public bool Remove(MessengerContact contact)
        {
            try
            {
                this.CachedContacts.Remove(GetId(contact._contact));
                ((IMessengerContacts)MessengerAPI.Messenger.MyContacts).Remove(contact._contact);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
