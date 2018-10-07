using System;
using System.Collections.Generic;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Twitter
{
	public interface IQueryFactory
	{
		String Create(IEnumerable<Word> words);
	}
}