using System;

namespace TechJargonBot.Business.Data.Tags
{
	public class TagString
	{
		public TagString(
			String rawTagString,
			String sanitisedTagString)
		{
			Raw = rawTagString;
			Sanitised = sanitisedTagString;
		}

		public String Sanitised { get; }
		public String Raw { get; }

		public Boolean IsHashTag => Raw[1] == '#';
		public Boolean IsAllUppercase => Raw == Raw.ToUpper();

		public Boolean IsFirstLetterUppercase
		{
			get
			{
				var normal = Raw[0];
				var upper = Char.ToUpper(Raw[0]);

				return Raw[1] == Char.ToUpper(Raw[1]);
			}
		}
	}
}