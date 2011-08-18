using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace WowArmory.Core.Managers
{
	public static class EncryptionManager
	{
		//----------------------------------------------------------------------
		#region --- Methods ---
		//----------------------------------------------------------------------
		/// <summary>
		/// Encrypts the specified data using the password and salt.
		/// </summary>
		/// <param name="dataToEncrypt">The data to encrypt.</param>
		/// <param name="password">The password.</param>
		/// <param name="salt">The salt.</param>
		/// <returns></returns>
		public static string Encrypt(string dataToEncrypt, string password, string salt)
		{
			AesManaged aes = null;
			MemoryStream memoryStream = null;
			CryptoStream cryptoStream = null;
			try
			{
				var rfc2898 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);
				aes = new AesManaged();
				aes.Key = rfc2898.GetBytes(32);
				aes.IV = rfc2898.GetBytes(16);
				memoryStream = new MemoryStream();
				cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

				var data = Encoding.UTF8.GetBytes(dataToEncrypt);
				cryptoStream.Write(data, 0, data.Length);
				cryptoStream.FlushFinalBlock();

				return Convert.ToBase64String(memoryStream.ToArray());
			}
			catch (Exception ex)
			{
				return String.Empty;
			}
			finally
			{
				if (cryptoStream != null)
				{
					cryptoStream.Close();
				}

				if (memoryStream != null)
				{
					memoryStream.Close();
				}

				if (aes != null)
				{
					aes.Clear();
				}
			}
		}

		/// <summary>
		/// Decrypts the specified data using the password and salt.
		/// </summary>
		/// <param name="dataToDecrypt">The data to decrypt.</param>
		/// <param name="password">The password.</param>
		/// <param name="salt">The salt.</param>
		/// <returns></returns>
		public static string Decrypt(string dataToDecrypt, string password, string salt)
		{
			AesManaged aes = null;
			MemoryStream memoryStream = null;

			try
			{
				var rfc2898 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);
				aes = new AesManaged();
				aes.Key = rfc2898.GetBytes(32);
				aes.IV = rfc2898.GetBytes(16);
				memoryStream = new MemoryStream();
				var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

				var data = Convert.FromBase64String(dataToDecrypt);
				cryptoStream.Write(data, 0, data.Length);
				cryptoStream.FlushFinalBlock();
				var decryptBytes = memoryStream.ToArray();

				if (cryptoStream != null)
				{
					cryptoStream.Dispose();
				}

				return Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
			}
			catch (Exception ex)
			{
				return String.Empty;
			}
			finally
			{
				if (memoryStream != null)
					memoryStream.Dispose();

				if (aes != null)
					aes.Clear();
			}
		}
		//----------------------------------------------------------------------
		#endregion
		//----------------------------------------------------------------------
	}
}