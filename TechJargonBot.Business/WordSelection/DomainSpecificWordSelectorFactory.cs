using TechJargonBot.Business.Data;

namespace TechJargonBot.Business.WordSelection
{
	internal class DomainSpecificWordSelectorFactory : IWordSelectorFactory
	{
		public WordSelector Create() =>
			new DomainSpecificWordSelector(
				wordProvider:
					new RandomWordProvider(
						dataProvider:
							new DataProvider(
								dataReader: new CsvFileReader()),
						wordSuitabilityPredicate: word => true),
				stringFormatter: new RegularStringFormatter());
	}
}