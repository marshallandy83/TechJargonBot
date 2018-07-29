using System.Collections.Generic;
using System.Collections.ObjectModel;
using TechJargonBot.Business.Data;

namespace TechJargonBot.Business
{
	internal interface IDataProvider
	{
		ReadOnlyCollection<List<Word>> GetWordLists();
	}
}