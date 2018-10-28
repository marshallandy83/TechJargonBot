using System.Collections.Generic;
using System.Linq;
using Common;
using TechJargonBot.Vocabulary.Tags;

namespace TechJargonBot.Business.WordSelection
{
	internal partial class DomainSpecificWordSelector : WordSelector
	{
		private readonly IWordProvider _domainSpecificWordProvider =
			new RandomWordProvider(
				new DataProvider(
					dataReader: new Data.CsvFileReader()),
				wordSuitabilityPredicate: word => word.IsDomainSpecific);

		public DomainSpecificWordSelector(
			IWordProvider wordProvider,
			IStringFormatter stringFormatter)
			: base(wordProvider, stringFormatter)
		{
		}

		public override IEnumerable<TagWithWord> CreateTagsWithWords(IEnumerable<Tag> tags) =>
			AssignWordProvidersToTags(tags)
			.Select(tagWithWordProvider =>
				CreateTagWithWord(
					tagWithWordProvider.Tag,
					tagWithWordProvider.WordProvider));

		private ICollection<TagWithWordProvider> AssignWordProvidersToTags(IEnumerable<Tag> tags)
		{
			Tag tagForDomainSpecificWord = tags.Where(tag => tag.IsForMandatoryWord).PickAtRandom();

			return
				new[]
				{
					new TagWithWordProvider(tagForDomainSpecificWord, _domainSpecificWordProvider)
				}
				.Concat(
					tags
					.Except(new[] { tagForDomainSpecificWord })
					.Select(tag => new TagWithWordProvider(tag, ChooseWordProviderAtRandom())))
				.ToList();
		}

		private IWordProvider ChooseWordProviderAtRandom() =>
			new[]
			{
				base.WordProvider,
				_domainSpecificWordProvider
			}
			.PickAtRandom();
	}
}
