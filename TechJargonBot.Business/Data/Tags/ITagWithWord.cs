using System;

namespace TechJargonBot.Business.Data.Tags
{
	internal interface ITagWithWord
	{
		String ReplacedWord { get; }
		Tag Tag { get; }
		Word Word { get; }
	}
}