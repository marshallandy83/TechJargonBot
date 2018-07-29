using System;
using System.Linq;
using TechJargonBot.Business.Data;
using TechJargonBot.Business.Data.Tags;

namespace TechJargonBot.Business
{
	internal class RandomWordProvider : IWordProvider
	{
		private readonly IDataProvider _dataProvider;
		private readonly Random _randomNumberGenerator;

		public RandomWordProvider(
			IDataProvider dataProvider,
			Random randomNumberGenerator)
		{
			_dataProvider = dataProvider;
			_randomNumberGenerator = randomNumberGenerator;
		}

		public Word GetNewWord(Tag tag)
		{
			return
				_dataProvider
					.GetWordLists()
					.SelectMany(words => words)
					.Where(word => word.Forms.Any(form => form.HasTag(tag)))
					.Where(word => tag.IsSuitable(word))
					.PickAtRandom(_randomNumberGenerator);
		}
	}
}