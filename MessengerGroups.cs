using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengerAPI;

namespace MessengerDotNet
{
	public class MessengerGroups : ICollection<MessengerGroup>
	{
		/// <summary>
		/// Caches instances of <see cref="MessengerGroup"/>s to reduce RAM usage.
		/// </summary>
		public Dictionary<string, MessengerGroup> CachedGroups { get; }

		public bool IsReadOnly => false;

		public MessengerGroups() => this.CachedGroups = new Dictionary<string, MessengerGroup>();

		int ICollection<MessengerGroup>.Count => ((IMessengerGroups)MessengerAPI.Messenger.MyGroups).Count;

		private string GetId(IMessengerGroup group) => group.Name + ((IMessengerService)group.Service).ServiceId;

		public IEnumerator<MessengerGroup> GetEnumerator()
		{
			foreach (MessengerContact contact in MessengerAPI.Messenger.MyContacts)
			{
				var id = GetId(contact as IMessengerGroup);
				if (!this.CachedGroups.ContainsKey(id))
				{
					this.CachedGroups[id] = new MessengerGroup(contact as IMessengerGroup);
				}
				yield return this.CachedGroups[id];
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		
		public void Add(string name, MessengerService service) => MessengerAPI.CreateGroup(name, service);

		/// <summary>
		/// Please use <see cref="Add(string, MessengerService)"/>.
		/// <exception cref="InvalidOperationException"></exception>
		/// </summary>
		[Obsolete("Please use MessengerGroups.Add(string, MessengerService).")]
		public void Add(MessengerGroup group) => throw new InvalidOperationException("Please use MessengerGroups.Add(string, MessengerService).");

		public void Clear()
		{
			foreach (MessengerGroup group in this)
			{
				this.Remove(group);
			}
		}

		public bool Contains(MessengerGroup group)
		{
			foreach (var g in this)
			{
				if (g.Name == group.Name && g.Service.ServiceId == group.Service.ServiceId)
				{
					return true;
				}
			}
			return false;
		}

		public void CopyTo(MessengerGroup[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}

			var ppArray = array as MessengerGroup[];
			if (ppArray == null)
			{
				throw new ArgumentException();
			}

			((ICollection<MessengerGroup>)this).CopyTo(ppArray, arrayIndex);
		}

		public bool Remove(MessengerGroup group)
		{
			try
			{
				this.CachedGroups.Remove(GetId(group._group));
				((IMessengerGroups)MessengerAPI.Messenger.MyGroups).Remove(group._group);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
