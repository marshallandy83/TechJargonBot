using TechJargonBot.Business.Data;

namespace TechJargonBot.Business.WordSelection
{
	internal class AnyWordSelectorFactory : IWordSelectorFactory
	{
		public WordSelector Create() =>
			new AnyWordSelector(
				wordProvider:
					new RandomWordProvider(
						dataProvider:
							new DataProvider(
								dataReader: new CsvFileReader()),
						wordSuitabilityPredicate: word => true),
				stringFormatter: new RegularStringFormatter());
	}
}