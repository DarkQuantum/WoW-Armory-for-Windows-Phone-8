using System;
using WowArmory.Core.Models;

namespace WowArmory.Core.Managers
{
	public static class AuthenticationManager
	{
		//----------------------------------------------------------------------
		#region --- Fields ---
		//----------------------------------------------------------------------
		private static bool _useAuthentication = false;
		private static EncryptionData _encryptionData = null;
		private static AuthenticationData _authenticationData = null;
		private static string _publicKey = String.Empty;
		private static string _privateKey = String.Empty;
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Properties ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Gets or sets a value indicating whether authentication is used.
		/// </summary>
		/// <value>
		///   <c>true</c> if authentication is used; otherwise, <c>false</c>.
		/// </value>
		public static bool UseAuthentication
		{
			get
			{
				return _useAuthentication;
			}
		}

		/// <summary>
		/// Gets the encryption data.
		/// </summary>
		public static EncryptionData EncryptionData
		{
			get
			{
				return _encryptionData;
			}
		}

		/// <summary>
		/// Gets the authentication data.
		/// </summary>
		public static AuthenticationData AuthenticationData
		{
			get
			{
				return _authenticationData;
			}
		}

		/// <summary>
		/// Gets the public key.
		/// </summary>
		public static string PublicKey
		{
			get
			{
				return _publicKey;
			}
		}

		/// <summary>
		/// Gets the private key.
		/// </summary>
		public static string PrivateKey
		{
			get
			{
				return _privateKey;	
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------


		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Loads the public and private key from the authentication key file.
		/// </summary>
		public static void LoadKeys()
		{
			if (IsolatedStorageManager.GetEncryptionKeys(out _encryptionData) && IsolatedStorageManager.GetAuthenticationKeys(out _authenticationData))
			{
				_publicKey = EncryptionManager.Decrypt(AuthenticationData.PublicKey, EncryptionData.Password, EncryptionData.Salt);
				_privateKey = EncryptionManager.Decrypt(AuthenticationData.PrivateKey, EncryptionData.Password, EncryptionData.Salt);

				if (!String.IsNullOrEmpty(PublicKey) && !String.IsNullOrEmpty(PrivateKey))
				{
					_useAuthentication = true;
				}
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}