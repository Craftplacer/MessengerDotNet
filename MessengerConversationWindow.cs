using MessengerAPI;
using System;
using System.Drawing;

namespace MessengerDotNet
{
    public class MessengerConversationWindow
	{
		internal IMessengerConversationWnd _window;

		public MessengerConversationWindow(IMessengerConversationWnd window) => this._window = window;

		public IntPtr Handle => new IntPtr(_window.HWND);

		public string History => _window.History;

		#region Properties

		public bool ToolbarVisible
		{
			get => (bool)this._window.Property[MWINDOWPROPERTY.MWINDOWPROP_VIEW_TOOLBAR];
			set => this._window.Property[MWINDOWPROPERTY.MWINDOWPROP_VIEW_TOOLBAR] = value;
		}

		public bool SidebarVisible
		{
			get => (bool)this._window.Property[MWINDOWPROPERTY.MWINDOWPROP_VIEW_SIDEBAR];
			set => this._window.Property[MWINDOWPROPERTY.MWINDOWPROP_VIEW_SIDEBAR] = value;
		}

		#endregion Properties

		#region Contacts

		public MessengerContact[] Contacts => MessengerAPI.ToArray(this._window.Contacts);

		public void AddContact(MessengerContact contact) => this._window.AddContact(contact._contact);

		#endregion Contacts

		#region Visibility

		public bool IsClosed => this._window.IsClosed;

		public void Show() => this._window.Show();

		public void Close() => this._window.Close();

		#endregion Visibility

		#region Position

		public Point Location
		{
			get => new Point(this.Left, this.Top);
			set
			{
				this.Left = value.X;
				this.Top = value.Y;
			}
		}

		public int Left
		{
			get => this._window.left;
			set => this._window.left = value;
		}

		public int Top
		{
			get => this._window.top;
			set => this._window.top = value;
		}

		#endregion Position
	}
}