using System;

namespace TechJargonBot.Vocabulary.Tags
{
	internal interface ITagWithWord
	{
		String ReplacedWord { get; }
		Tag Tag { get; }
		Word Word { get; }
	}
}