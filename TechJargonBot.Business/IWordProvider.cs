using TechJargonBot.Vocabulary;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business
{
	internal interface IWordProvider
	{
		Word GetNewWord(Tag tag);
	}
}