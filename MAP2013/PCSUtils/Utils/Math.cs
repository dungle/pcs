namespace PCSUtils.Utils
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;


	public class ConvertNumberToWord
	{
		#region Convert Number to English language
		private object VB_Name;
		public ConvertNumberToWord()
		{
			this.VB_Name = "math";
		}

		private static int div3(string numb)
		{
			int num3 = 0;
			int num4 = Strings.Len(numb);
			for (int num2 = 1; num2 <= num4; num2++)
			{
				num3 = (int) Math.Round(num3 + Conversion.Val(Strings.Mid(numb, num2, 1)));
			}
			return (num3 % 3);
		}

		public static string IntToWord(string numb)
		{
			int num1;
			string text2 = string.Empty;
			string text3 = string.Empty;
			bool flag2 = false;
			if (Strings.InStr(numb, ".", CompareMethod.Binary) > 0)
			{
				text3 = Strings.Mid(numb, Strings.InStr(numb, ".", CompareMethod.Binary) + 1);
				numb = Strings.Left(numb, Strings.InStr(numb, ".", CompareMethod.Binary) - 1);
				flag2 = true;
			}
			Label_0043:
			if (StringType.StrCmp(numb, "0", false) == 0)
			{
				return StringType.FromInteger(0);
			}
			if (StringType.StrCmp(Strings.Left(numb, 1), "0", false) == 0)
			{
				numb = Strings.Mid(numb, 2);
				goto Label_0043;
			}
			if (StringType.StrCmp(Strings.Left(numb, 1), "-", false) == 0)
			{
				numb = Strings.Mid(numb, 2);
				text2 = "negative ";
			}
			if (Strings.Len(numb) == 1)
			{
				text2 = text2 + word3(numb);
			}
			else if (Strings.Len(numb) == 2)
			{
				text2 = text2 + word3(numb);
			}
			else if (Strings.Len(numb) == 3)
			{
				text2 = text2 + word3(numb);
			}
			else
			{
				int num2 = 0;
				if ((Strings.Len(numb) % 3) == 0)
				{
					num2 = 3;
				}
				if ((Strings.Len(numb) % 3) == 1)
				{
					num2 = 1;
				}
				if ((Strings.Len(numb) % 3) == 2)
				{
					num2 = 2;
				}
				bool flag1 = true;
				int num5 = Strings.Len(numb) - 3;
				for (num1 = 1; num1 <= num5; num1 += 3)
				{
					string text5 = string.Empty;
					if (flag1)
					{
						text5 = Strings.Mid(numb, 1, num2);
						flag1 = false;
					}
					else
					{
						switch (num2)
						{
							case 1:
								text5 = Strings.Mid(numb, num1 - 2, 3);
								break;

							case 2:
								text5 = Strings.Mid(numb, num1 - 1, 3);
								break;

							case 3:
								text5 = Strings.Mid(numb, num1, 3);
								break;
						}
					}
					int num3 = (Strings.Len(numb) - num1) + 1;
					text2 = text2 + word2(text5, word4(num3));
				}
				text2 = text2 + word3(Strings.Right(numb, 3));
				text2 = Strings.RTrim(Strings.LTrim(text2));
			}
			if (flag2)
			{
				numb = text3;
				text2 = text2 + " point ";
				int num4 = Strings.Len(numb);
				for (num1 = 1; num1 <= num4; num1++)
				{
					text2 = text2 + Word(Strings.Mid(numb, num1, 1)) + " ";
				}
			}
			return Strings.RTrim(Strings.LTrim(text2));
		}

		private static string Word(string numb)
		{
			string text1 = string.Empty;
			if (DoubleType.FromString(numb) == 0)
			{
				return "zero";
			}
			if (DoubleType.FromString(numb) == 1)
			{
				return "one";
			}
			if (DoubleType.FromString(numb) == 2)
			{
				return "two";
			}
			if (DoubleType.FromString(numb) == 3)
			{
				return "three";
			}
			if (DoubleType.FromString(numb) == 4)
			{
				return "four";
			}
			if (DoubleType.FromString(numb) == 5)
			{
				return "five";
			}
			if (DoubleType.FromString(numb) == 6)
			{
				return "six";
			}
			if (DoubleType.FromString(numb) == 7)
			{
				return "seven";
			}
			if (DoubleType.FromString(numb) == 8)
			{
				return "eight";
			}
			if (DoubleType.FromString(numb) == 9)
			{
				return "nine";
			}
			if (DoubleType.FromString(numb) == 11)
			{
				return "eleven";
			}
			if (DoubleType.FromString(numb) == 12)
			{
				return "twelve";
			}
			if (DoubleType.FromString(numb) == 13)
			{
				return "thirteen";
			}
			if (DoubleType.FromString(numb) == 14)
			{
				return "fourteen";
			}
			if (DoubleType.FromString(numb) == 15)
			{
				return "fifteen";
			}
			if (DoubleType.FromString(numb) == 16)
			{
				return "sixteen";
			}
			if (DoubleType.FromString(numb) == 17)
			{
				return "seventeen";
			}
			if (DoubleType.FromString(numb) == 18)
			{
				return "eighteen";
			}
			if (DoubleType.FromString(numb) == 19)
			{
				return "nineteen";
			}
			if (DoubleType.FromString(numb) == 10)
			{
				return "ten";
			}
			if (DoubleType.FromString(numb) == 20)
			{
				return "twenty";
			}
			if (DoubleType.FromString(numb) == 30)
			{
				return "thirty";
			}
			if (DoubleType.FromString(numb) == 40)
			{
				return "fourty";
			}
			if (DoubleType.FromString(numb) == 50)
			{
				return "fifty";
			}
			if (DoubleType.FromString(numb) == 60)
			{
				return "sixty";
			}
			if (DoubleType.FromString(numb) == 70)
			{
				return "seventy";
			}
			if (DoubleType.FromString(numb) == 80)
			{
				return "eighty";
			}
			if (DoubleType.FromString(numb) == 90)
			{
				return "ninety";
			}
			if (DoubleType.FromString(numb) == 90)
			{
				text1 = "ninety";
			}
			return text1;
		}

		private static string word2(string numb, string param)
		{
			string[] textArray1;
			string text2 = "";
			if (Strings.Len(numb) == 1)
			{
				textArray1 = new string[] { text2, Word(numb), " ", param, " " };
				return string.Concat(textArray1);
			}
			if (Strings.Len(numb) == 2)
			{
				if (Conversion.Val(numb) == 0)
				{
					return text2;
				}
				if ((DoubleType.FromString(numb) > 0) & (DoubleType.FromString(numb) < 20))
				{
					textArray1 = new string[] { text2, Word(numb), " ", param, " " };
					return string.Concat(textArray1);
				}
				textArray1 = new string[] { text2, Word(StringType.FromDouble(DoubleType.FromString(Strings.Left(numb, 1)) * 10)), " ", Word(Strings.Mid(numb, 2, 1)), " ", param, " " };
				return string.Concat(textArray1);
			}
			if (Strings.Len(numb) == 3)
			{
				string text1 = Strings.Mid(numb, 1, 1);
				if (Conversion.Int(Conversion.Val(text1)) != 0)
				{
					text2 = text2 + Word(Strings.Mid(numb, 1, 1)) + " hundred ";
				}
				text1 = Strings.Mid(numb, 2, 2);
				if (Conversion.Val(text1) == 0)
				{
					if (DoubleType.FromString(Strings.Mid(numb, 1, 1)) != 0)
					{
						text2 = text2 + " " + param + " ";
					}
				}
				else if (((Conversion.Val(text1) > 0) & (Conversion.Val(text1) < 20)) | ((DoubleType.FromString(text1) % 10) == 0))
				{
					textArray1 = new string[] { text2, Word(Strings.Mid(numb, 2, 2)), " ", param, " " };
					text2 = string.Concat(textArray1);
				}
				else
				{
					textArray1 = new string[] { text2, Word(StringType.FromDouble(DoubleType.FromString(Strings.Mid(numb, 2, 1)) * 10)), " ", Word(Strings.Mid(numb, 3, 1)), " ", param, " " };
					text2 = string.Concat(textArray1);
				}
			}
			return text2;
		}

		private static string word3(string numb)
		{
			string text2 = string.Empty;
			if (Strings.Len(numb) == 1)
			{
				return (text2 + Word(numb));
			}
			if (Strings.Len(numb) == 2)
			{
				if (((DoubleType.FromString(numb) > 1) & (DoubleType.FromString(numb) < 20)) | ((DoubleType.FromString(numb) % 10) == 0))
				{
					return (text2 + Word(numb));
				}
				return (text2 + Word(StringType.FromDouble(DoubleType.FromString(Strings.Left(numb, 1)) * 10)) + " " + Word(Strings.Right(numb, 1)));
			}
			if (Strings.Len(numb) == 3)
			{
				string text1 = Word(StringType.FromDouble(Conversion.Int(DoubleType.FromString(numb) / 100)));
				if (StringType.StrCmp(text1, "zero", false) != 0)
				{
					text2 = text2 + text1 + " hundred ";
				}
				text1 = StringType.FromDouble(DoubleType.FromString(Strings.Mid(numb, 2)) % 10);
				if (StringType.StrCmp(text1, "0", false) == 0)
				{
					text1 = Word(Strings.Mid(numb, 2));
					if (StringType.StrCmp(text1, "zero", false) != 0)
					{
						text2 = text2 + text1;
					}
				}
				else if ((DoubleType.FromString(Strings.Right(numb, 2)) > 0) & (DoubleType.FromString(Strings.Right(numb, 2)) < 20))
				{
					text2 = text2 + Word(Strings.Right(numb, 2));
				}
				else
				{
					text2 = text2 + Word(StringType.FromDouble(Conversion.Int(DoubleType.FromString(Strings.Mid(numb, 2, 1)) * 10))) + " " + ConvertNumberToWord.Word(Strings.Right(numb, 1));
				}
			}
			return text2;
		}

		private static string word4(int numb)
		{
			string text1 = string.Empty;
			if ((numb >= 4) & (numb <= 6))
			{
				return "thousand";
			}
			if ((numb >= 7) & (numb <= 9))
			{
				return "million";
			}
			if ((numb >= 10) & (numb <= 12))
			{
				return "billion";
			}
			if ((numb >= 13) & (numb <= 15))
			{
				return "trillion";
			}
			if ((numb >= 0x10) & (numb <= 0x12))
			{
				return "quadrillion";
			}
			if ((numb >= 0x13) & (numb <= 0x15))
			{
				return "sextillion";
			}
			if ((numb >= 0x16) & (numb <= 0x18))
			{
				return "septillion";
			}
			if ((numb >= 0x19) & (numb <= 0x1b))
			{
				return "octillion";
			}
			if ((numb >= 0x1c) & (numb <= 30))
			{
				return "nonillion";
			}
			if ((numb >= 0x1f) & (numb <= 0x21))
			{
				return "decillion";
			}
			if ((numb >= 0x22) & (numb <= 0x24))
			{
				return "undecillion";
			}
			if ((numb >= 0x25) & (numb <= 0x27))
			{
				return "dodecillion";
			}
			if ((numb >= 40) & (numb <= 0x2a))
			{
				return "tredecillion";
			}
			if ((numb >= 0x2b) & (numb <= 0x2d))
			{
				return "quattuordecillion";
			}
			if ((numb >= 0x2e) & (numb <= 0x30))
			{
				return "quindecillion";
			}
			if ((numb >= 0x31) & (numb <= 0x33))
			{
				return "sexdecillion";
			}
			if ((numb >= 0x34) & (numb <= 0x36))
			{
				return "septendecillion";
			}
			if ((numb >= 0x37) & (numb <= 0x39))
			{
				return "octodecillion";
			}
			if ((numb >= 0x3a) & (numb <= 60))
			{
				return "novemdecillion";
			}
			if ((numb >= 0x3d) & (numb <= 0x3f))
			{
				return "vigintillion";
			}
			if ((numb >= 0x40) & (numb <= 0x42))
			{
				return "unvigintillion";
			}
			if ((numb >= 0x43) & (numb <= 0x45))
			{
				return "dovigintillion";
			}
			if ((numb >= 70) & (numb <= 0x48))
			{
				return "trevigintillion";
			}
			if ((numb >= 0x49) & (numb <= 0x4b))
			{
				return "quattuorvigintillion";
			}
			if ((numb >= 0x4c) & (numb <= 0x4e))
			{
				return "quinvigintillion";
			}
			if ((numb >= 0x4f) & (numb <= 0x51))
			{
				return "sexvigintillion";
			}
			if ((numb >= 0x52) & (numb <= 0x54))
			{
				return "septenvigintillion";
			}
			if ((numb >= 0x55) & (numb <= 0x57))
			{
				return "octovigintillion";
			}
			if ((numb >= 0x58) & (numb <= 90))
			{
				return "novemvigintillion";
			}
			if ((numb >= 0x5b) & (numb <= 0x5d))
			{
				return "trigintillion";
			}
			if ((numb >= 0x5e) & (numb <= 0x60))
			{
				return "untrigintillion";
			}
			if ((numb >= 0x61) & (numb <= 0x63))
			{
				return "dotrigintillion";
			}
			if ((numb >= 100) & (numb <= 0x66))
			{
				return "tretrigintillion";
			}
			if ((numb >= 0x67) & (numb <= 0x69))
			{
				return "quattuortrigintillion";
			}
			if ((numb >= 0x6a) & (numb <= 0x6c))
			{
				return "quintrigintillion";
			}
			if ((numb >= 0x6d) & (numb <= 0x6f))
			{
				return "sextrigintillion";
			}
			if ((numb >= 0x70) & (numb <= 0x72))
			{
				return "septentrigintillion";
			}
			if ((numb >= 0x73) & (numb <= 0x75))
			{
				return "octotrigintillion";
			}
			if ((numb >= 0x76) & (numb <= 120))
			{
				text1 = "novemtrigintillion";
			}
			return text1;
		}
		#endregion

		#region Convert Number to Vietnamese language
		/// <summary>
		/// Chuyển phần thập phân thành chữ
		/// </summary>
		/// <param name="pstrInput"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, May 15 2006</date>
		private static string ChuyenPhanThapPhan(string pstrInput)
		{
			const string SPACE = " ";
			string[] astrChuSo = {"không", "một", "hai", "ba", "bốn", "lăm", "sáu", "bảy", "tám", "chín", "mười", "phẩy", "năm"};		
			string strReturn = string.Empty;
			if (pstrInput.Length > 0)
			{
				int intIndex = -1;
				strReturn += astrChuSo[11] + SPACE;
				for (int i = 0; i < pstrInput.Length; i++)
				{
					if ((intIndex == 0) && (Int32.Parse(pstrInput[i].ToString()) == 5))
					{
						intIndex = 12;
						strReturn += astrChuSo[intIndex] + SPACE;
					}
					else
					{
						if ((Int32.Parse(pstrInput[i].ToString()) == 5)
							&& (i != pstrInput.Length - 1))
						{
							intIndex = 12;
							strReturn += astrChuSo[intIndex] + SPACE;
						}
						else
						{
							intIndex = Int32.Parse(pstrInput[i].ToString());
							strReturn += astrChuSo[intIndex] + SPACE;
						}
					}
				}
			}
			return strReturn;
		}
		/// <summary>
		/// Chuyển số thành chữ
		/// </summary>
		/// <param name="pdecInput">Đầu vào là số, có thể có phần thập phân</param>
		/// <returns>Trả ra xâu đánh vần số theo ngữ pháp Việt Nam</returns>
		/// <author>Trada</author>
		/// <date>Monday, May 15 2006</date>
		public static string ChuyenSoThanhChu(decimal pdecInput)
		{
			const string SPACE = " ";
			string strReturn = string.Empty;
			string strXau = string.Empty;
			//Kiem tra so la nguyen hay la le
			if (pdecInput.ToString().IndexOf(".") != -1)
			{
				//tach thanh 2 phan nguyen va thap phan
				string strNguyen = pdecInput.ToString().Substring(0, pdecInput.ToString().IndexOf("."));
				string strThapPhan = pdecInput.ToString().Substring(pdecInput.ToString().IndexOf(".") + 1, pdecInput.ToString().Length - pdecInput.ToString().IndexOf(".") - 1);
				strReturn += ChuyenSoNguyenThanhChu(strNguyen);
				strReturn += SPACE;
				strReturn += ChuyenPhanThapPhan(strThapPhan);
				strReturn += " đồng";
			}
			else
			{
				strReturn += ChuyenSoNguyenThanhChu(pdecInput.ToString());
				strReturn += " đồng chẵn";
			}
			//Viet hoa chu so dau
			if (strReturn.Length > 0)
			{
				strXau = strReturn[0].ToString().ToUpper();
				strXau += strReturn.Substring(1, strReturn.Length - 1);
			}
			return strXau.Replace("  "," ");
		}
		/// <summary>
		/// Chuyển phần số nguyên thành chữ theo ngữ pháp Việt Nam
		/// </summary>
		/// <param name="pstrInput"></param>
		/// <returns></returns>
		/// <author>Trada</author>
		/// <date>Monday, May 15 2006</date>
		private static string ChuyenSoNguyenThanhChu(string pstrInput)
		{
			string[] _DigitPlace = { "-", "nghìn", "triệu", "tỉ", "nghìn", "trăm", "triệu", "quá lớn" };
			string[] _Digits = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín", "lăm" };
				string[] _FromTenToTwenty = {"mười", "mười một", "mười hai", "mười ba", "mười bốn", "mười lăm", "mười sáu", "mười bảy", "mười tám", "mười chín" };
				string[] _Tens = {"-", "mười", "hai mươi", "ba mươi", "bốn mươi", "năm mươi", "sáu mươi", "bảy mươi", "tám mươi", "chín mươi"};

				
				Int64 amount = Convert.ToInt64(pstrInput);
				string sb = string.Empty;
				string amountString = "" + amount.ToString();
				
				string[] amount_Digits = new string[pstrInput.Length];
				int[] amountDigitValues = new int[pstrInput.Length];
				// khoi tao mang so
				for(int i = 0; i < amountString.Length; i++ ) 
				{
					amount_Digits[i] = amountString[i].ToString();
					amountDigitValues[i] = 0;
				}
						
				// So am 
				if (amount < 10 && amount > -10) 
				{
					if(amount < 0) 
					{
						sb += "Âm ";
						amount = -amount;
					}
					sb += (_Digits[amount]);
				
					return sb;
				}
			
				// Vong lap
				for(int i = 0; i < amount_Digits.Length; i++)
				{
					if( amount_Digits[i].ToString() != '-'.ToString() )
					{
						amountDigitValues[i] = Convert.ToInt32(amount_Digits[i]);
					}

					int offset = amount_Digits.Length - i - 1;

					// Ki tu am
					if( amount_Digits[i].ToString() == '-'.ToString() )
						sb += ("Âm ");
					else if(amountDigitValues[i] > 0)
					{
						// Cho Ti, Trieu, Tram, Nghan.
						if(offset > 2)
						{
							int nextOffset = offset - (offset % 3);
							string chunk = amountString.Substring(i, amount_Digits.Length - nextOffset - i);
							i = amount_Digits.Length - nextOffset - 1;
							int digitOffset = Convert.ToInt32((offset / 3));
						
							sb +=  ChuyenSoNguyenThanhChu(chunk);
							sb += " ";
							sb +=  _DigitPlace[digitOffset];
							sb += " ";
						}

						else if(offset == 2) // Tram
						{
							sb +=  _Digits[amountDigitValues[i]]; 
							sb += " ";
							sb +=  "trăm ";
						}
						else if(offset == 1) // Chuc
						{
							if (i > 1)
							{
								if (amountDigitValues[i - 1].ToString() == 0.ToString())
								{
									sb += "không trăm ";
								}
							}
							if(amountDigitValues[i].ToString() == 1.ToString()) // Từ Mười đến Hai Mươi
							{
								amountDigitValues[i + 1] = Convert.ToInt32(amount_Digits[i + 1]);
								sb +=  _FromTenToTwenty[amountDigitValues[i + 1]];
								sb += " ";
								i++;
							}
							else
							{
								sb +=  _Tens[amountDigitValues[i]];
								sb += " ";
							}
						}
						else if(offset == 0) // So don vi
						{
							if(amountDigitValues[i - 1].ToString() == 0.ToString())
							{
								sb += "linh ";
								sb +=  _Digits[amountDigitValues[i]];
							}
							else if(amountDigitValues[i - 1].ToString() == 1.ToString())
							{
								sb +=  _Digits[amountDigitValues[i]];
								sb += "";
							}
							else if(amountDigitValues[i].ToString() == 1.ToString())
							{
								sb += " mốt ";
							}
							else
							{
								if (amountDigitValues[i].ToString() == 5.ToString())
								{
									if (i > 0)
									{
										if ((amountDigitValues[i - 1].ToString() != 0.ToString())
											&& (((amountDigitValues.Length - i - 1)%3) == 0))
										{
											sb +=  _Digits[10]; //Lăm		
										}
									}
									
								}
								else
								{
									sb +=  _Digits[amountDigitValues[i]];
									sb += "";	
								}
							}
						}
					}
				}
			
				return sb;
		}
		#endregion
	}



}

