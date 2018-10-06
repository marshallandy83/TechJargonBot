using System;
using System.Collections.Generic;

namespace TechJargonBot.Twitter
{
	public interface IQueryFactory
	{
		String Create(IEnumerable<String> words);
	}
}