using System.Collections.Generic;
using System.Collections.ObjectModel;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Business
{
	internal interface IDataProvider
	{
		ReadOnlyCollection<List<Word>> GetWordLists();
	}
}