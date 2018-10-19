using MessengerAPI;
using System;
using System.Drawing;

namespace MessengerDotNet
{
	public class MessengerWindow
	{
		private IMessengerWindow _window;

		public MessengerWindow(IMessengerWindow window) => this._window = window;

		public IntPtr Handle => new IntPtr(this.HandlePointer);

		public int HandlePointer => this._window.HWND;

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

		#region Visibility

		public bool IsClosed => this._window.IsClosed;

		public void Show() => this._window.Show();

		public void Close() => this._window.Close();

		#endregion Visibility

		#region Position

		public Point Location
		{
			get => new Point(this._window.left, this._window.top);
			set
			{
				this._window.left = value.X;
				this._window.top = value.Y;
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

		#region Size

		public Size Size
		{
			get => new Size(this.Width, this.Height);
			set
			{
				this.Width = value.Width;
				this.Height = value.Height;
			}
		}

		public int Width
		{
			get => this._window.Width;
			set => this._window.Width = value;
		}

		public int Height
		{
			get => this._window.Height;
			set => this._window.Height = value;
		}

		#endregion Size
	}
}