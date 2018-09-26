using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TechJargonBot.Business.Data.Tags
{
	internal class TagExtractor
	{
		public const Char TagStartCharacter = '[';
		public const Char TagEndCharacter = ']';

		private readonly ITagFactory _tagFactory;

		public TagExtractor(ITagFactory tagFactory)
		{
			_tagFactory = tagFactory;
		}

		internal IEnumerable<Tag> ExtractTags(String sentence)
		{
			MatchCollection tagStrings = Regex.Matches(sentence, $@"([{TagStartCharacter}][^[]+[{TagEndCharacter}])");

			var tags = new List<Tag>();

			foreach (Match match in tagStrings)
			{
				tags.Add(
					_tagFactory.CreateTag(
						match.Groups[1].Value));
			}

			return SetWordSuitabilityForIdentifiableHashtags(tags);
		}

		private IEnumerable<Tag> SetWordSuitabilityForIdentifiableHashtags(List<Tag> tags)
		{
			var identifiersWithHashtags =
				tags
					.Where(tag => tag.HasIdentifer && tag.IsForHashtag)
					.Select(tag => tag.Identifier);

			return
				tags.Select(
					tag => identifiersWithHashtags.Contains(tag.Identifier)
					? new Tag(tag.TagString, tag.Identifier, (word) => word.IsSuitableForHashtag, new TagReplacer(), tag.IsForHashtag)
					: tag);
		}
	}
}