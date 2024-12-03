using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoaLuan.Helpers
{
	public class RandomHelper
	{
		public static int RandomVerificationCode()
		{
			Random r = new Random();
			return r.Next(100000, 999999);
		}

		public static string RandomNewPassword()
		{
			string[] s = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
				"N", "O", "P", "Q", "R","S","T","U","V","W","X","Y","Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m",
				"n", "o", "p", "q", "r","s","t","u","v","w","x","y","z" }; //62
			Random r = new Random();
			string password = string.Empty;
			for(int i = 0; i < 8; i++)
			{
				password += s[r.Next(62)];
			}
			return password;
		}
	}
}