namespace MessengerDotNet
{
	/// <summary>
	/// Status of a <see cref="MessengerContact"/>
	/// </summary>
	public enum MessengerStatus
	{
		Online,

		Away,

		/// <summary>
		/// Bases off <see cref="Away"/>
		/// </summary>
		BeRightBack,

		/// <summary>
		/// Bases off <see cref="Away"/>
		/// </summary>
		OutToLunch,

		Busy,

		/// <summary>
		/// Bases off <see cref="Busy"/>
		/// </summary>
		OnThePhone,

		Offline,

		/// <summary>
		/// Bases off <see cref="Offline"/>, offline for other users.
		/// </summary>
		Invisible,

		/// <summary>
		/// The status is unknown
		/// </summary>
		Unknown,
	}
}