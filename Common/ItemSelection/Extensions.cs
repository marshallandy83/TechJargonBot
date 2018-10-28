using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
	public static class Extensions
	{
		private static Random _randomNumberGenerator = new Random();

		public static T PickAtRandom<T>(this IEnumerable<T> collection) =>
			collection.ElementAt(_randomNumberGenerator.Next(collection.Count()));

		public static IEnumerable<T> PickSomeAtRandom<T>(this IEnumerable<T> collection) =>
			collection.Where(item => new[] { true, false }.PickAtRandom());
	}
}
