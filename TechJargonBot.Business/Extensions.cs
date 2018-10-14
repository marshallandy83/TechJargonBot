using System;
using System.Collections.Generic;
using System.Linq;

namespace TechJargonBot.Business
{
	internal static class Extensions
	{
		private static Random _randomNumberGenerator = new Random();

		internal static T PickAtRandom<T>(this IEnumerable<T> collection) =>
			collection.ElementAt(_randomNumberGenerator.Next(collection.Count()));

		internal static IEnumerable<T> PickSomeAtRandom<T>(this IEnumerable<T> collection) =>
			collection.Where(item => new[] { true, false }.PickAtRandom());
	}
}
