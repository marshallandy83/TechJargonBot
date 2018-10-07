using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TechJargonBot.Vocabulary;

namespace TechJargonBot.Twitter
{
	public class AllWordsQueryFactory : IQueryFactory
	{
		public string Create(IEnumerable<Word> words) =>
			String.Join(" AND ", words.Select(word => QueryAny(word.Forms)));

		private String QueryAny(ReadOnlyCollection<Form> forms) => $"({String.Join(" OR ", forms.Select(form => form.Value))})";
	}
}