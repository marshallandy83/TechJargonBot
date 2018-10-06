using System.Linq;
using TechJargonBot.Vocabulary;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business
{
	internal class RandomWordProvider : IWordProvider
	{
		private readonly IDataProvider _dataProvider;

		public RandomWordProvider(IDataProvider dataProvider)
		{
			_dataProvider = dataProvider;
		}

		public Word GetNewWord(Tag tag)
		{
			return
				_dataProvider
					.GetWordLists()
					.SelectMany(words => words)
					.Where(word => word.Forms.Any(form => form.HasTag(tag)))
					.Where(word => tag.IsSuitable(word))
					.PickAtRandom();
		}
	}
}