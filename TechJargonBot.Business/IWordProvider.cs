using TechJargonBot.Business.Data;
using TechJargonBot.Business.Data.Tags;

namespace TechJargonBot.Business
{
	internal interface IWordProvider
	{
		Word GetNewWord(Tag tag);
	}
}