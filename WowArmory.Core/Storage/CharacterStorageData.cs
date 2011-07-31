using System;

namespace WowArmory.Core.Storage
{
	public class CharacterStorageData
	{
		//---------------------------------------------------------------------------
		#region --- Properties ---
		//---------------------------------------------------------------------------
		public string RealmName { get; set; }
		public string CharacterName { get; set; }
		public Guid StorageGuid { get; set; }
		//---------------------------------------------------------------------------
		#endregion
		//---------------------------------------------------------------------------
	}
}
