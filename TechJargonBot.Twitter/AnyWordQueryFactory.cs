using System;
using System.Collections.Generic;

namespace TechJargonBot.Twitter
{
	public class AnyWordQueryFactory : IQueryFactory
	{
		public string Create(IEnumerable<String> words)
		{
			return String.Join(" OR ", words);
		}
	}
}