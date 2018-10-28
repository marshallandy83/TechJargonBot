using System;
using System.Linq;
using Common;
using TechJargonBot.Vocabulary;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business
{
	internal class RandomWordProvider : IWordProvider
	{
		private readonly IDataProvider _dataProvider;
		private readonly Func<Word, Boolean> _isSuitable;

		public RandomWordProvider(
			IDataProvider dataProvider,
			Func<Word, Boolean> wordSuitabilityPredicate)
		{
			_dataProvider = dataProvider;
			_isSuitable = wordSuitabilityPredicate;
		}

		public Word GetNewWord(Tag tag)
		{
			return
				_dataProvider
					.GetWordLists()
					.SelectMany(words => words)
					.Where(word => _isSuitable(word))
					.Where(word => word.Forms.Any(form => form.HasTag(tag)))
					.Where(word => tag.IsSuitable(word))
					.PickAtRandom();
		}
	}
}