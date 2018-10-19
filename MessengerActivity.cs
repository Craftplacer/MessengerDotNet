using System;
using System.Runtime.InteropServices;

namespace MessengerDotNet
{
	public static class MessengerActivity
	{
		/// <summary>
		/// Sets your status of what you're doing right now. (Show what I'm listening to)
		/// </summary>
		public static void SetActivity(MessengerActivityType type, string text)
		{
			string activity = "";
			switch (type)
			{
				case MessengerActivityType.Music:  activity = "Music"; break;
				case MessengerActivityType.Games:  activity = "Games"; break;
				case MessengerActivityType.Office: activity = "Office"; break;
			}
			string format = $"\\0{activity}\\01\\0{text}\\0\0";
			var data = new COPYDATASTRUCT
			{
				dwData = (IntPtr)0x547,
				lpData = VarPtr(format),
				cbData = format.Length * 2
			};

			IntPtr ptr = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "MsnMsgrUIManager", null);
			if (ptr.ToInt32() > 0)
			{
				SendMessage(ptr, 0x4a, IntPtr.Zero, VarPtr(data));
			}
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool PostMessage(IntPtr hWnd, uint iMsg, long wParam, long lParam);

		[DllImport("user32.dll", EntryPoint = "FindWindowExA")]
		private static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);

		private static IntPtr VarPtr(object e)
		{
			var handle = GCHandle.Alloc(e, GCHandleType.Pinned);
			IntPtr ptr = handle.AddrOfPinnedObject();
			handle.Free();
			return ptr;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct COPYDATASTRUCT
		{
			public IntPtr dwData;
			public int cbData;
			public IntPtr lpData;
		}
	}

	public enum MessengerActivityType
	{
		/// <summary>
		/// Used for Windows Media Player
		/// Icon Description: Headphones
		/// </summary>
		Music,

		/// <summary>
		/// Used for Games?
		/// Icon Description:
		/// </summary>
		Games,

		/// <summary>
		/// Used for Office Communicator?
		/// Icon Description: Office 2007 Logo (without transparency)
		/// </summary>
		Office,
	}
}