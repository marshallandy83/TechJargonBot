using System;
using System.Collections.Generic;
using System.Linq;

namespace TechJargonBot.Business
{
	internal static class Extensions
	{
		internal static T PickAtRandom<T>(
			this IEnumerable<T> collection,
			Random randomNumberGenerator)
		{
			return
				collection
					.ElementAt(randomNumberGenerator.Next(collection.Count()));
		}
	}
}
