using System;
using System.Collections.Generic;
using TechJargonBot.Vocabulary;
using TechJargonBot.Vocabulary.Tags;
using Xunit;

namespace TechJargonBot.Business
{
	public class GeneratorTests : IClassFixture<GeneratorFixture>
	{
		private readonly GeneratorFixture _wordProviderFixture;

		public GeneratorTests(GeneratorFixture wordProviderFixture)
		{
			_wordProviderFixture = wordProviderFixture;
		}

		[Fact]
		public void TestGenerate_GeneratesSentence_WithVerbs()
		{
			String sentence =
				"Seas has [done] something. " +
				"Bubby is [doing] something. " +
				"Riley will [do] something. " +
				"All while Jimmy T [does] something.";

			String expectedGeneratedSentence =
				"Seas has patched something. " +
				"Bubby is patching something. " +
				"Riley will patch something. " +
				"All while Jimmy T patches something.";

			AssertOutputIsCorrect(
				sentence,
				expectedGeneratedSentence,
				new TagWithWord[]
				{
					CreateTagWithWord("[done]", _patch),
					CreateTagWithWord("[doing]", _patch),
					CreateTagWithWord("[do]", _patch),
					CreateTagWithWord("[does]", _patch)
				});
		}

		[Fact]
		public void TestGenerate_GeneratesSentence_WithNouns()
		{
			String sentence =
				"Seas has done something to the [thing]. " +
				"Bubby is doing something to the [things]. " +
				"Riley will do something to the [things].";

			String expectedGeneratedSentence =
				"Seas has done something to the server. " +
				"Bubby is doing something to the servers. " +
				"Riley will do something to the [things].";

			AssertOutputIsCorrect(
				sentence,
				expectedGeneratedSentence,
				new TagWithWord[]
				{
					CreateTagWithWord("[thing]", _server),
					CreateTagWithWord("[things]", _server)
				});
		}

		[Fact]
		public void TestGenerate_GeneratesSentence_WithAdjectives()
		{
			String sentence =
				"Seas has done something to [a tech] thing. " +
				"Bubby is doing something to the [tech] thing.";

			String expectedGeneratedSentence =
				"Seas has done something to an authenticated thing. " +
				"Bubby is doing something to the authenticated thing.";

			AssertOutputIsCorrect(
				sentence,
				expectedGeneratedSentence,
				new TagWithWord[]
				{
					CreateTagWithWord("[tech]", _authenticated),
					CreateTagWithWord("[a tech]", _authenticated)
				});
		}

		[Fact]
		public void TestGenerate_GeneratesSentence_WithIdentifiableWord()
		{
			String sentence = "Seas has [done1] something.";

			String expectedGeneratedSentence = "Seas has patched something.";

			AssertOutputIsCorrect(
				sentence,
				expectedGeneratedSentence,
				new TagWithWord[]
				{
					new TagWithWord(
						formatString: (word, tagString) => new RegularStringFormatter().FormatString(word, tagString),
						tag: new Tag(
							tagString: new TagString("[done1]", "[done]"),
							identifier: 1,
							wordSuitabilityPredicate: (_) => true,
							tagReplacer: new TagReplacer(),
							isForHashtag: false),
						word: _patch)
				});
		}

		[Fact]
		public void TestGenerate_GeneratesSentence_WithIdentifiableWordAndNonIdentifiableWord()
		{
			String sentence =
				"Seas has [done1] something. " +
				"Bubby has [done] something.";

			String expectedGeneratedSentence =
				"Seas has patched something. " +
				"Bubby has deleted something.";

			AssertOutputIsCorrect(
				sentence,
				expectedGeneratedSentence,
				new TagWithWord[]
				{
					new TagWithWord(
						formatString: (word, tagString) => new RegularStringFormatter().FormatString(word, tagString),
						tag: new Tag(
							tagString: new TagString("[done1]", "[done]"),
							identifier: 1,
							wordSuitabilityPredicate: (_) => true,
							tagReplacer: new TagReplacer(),
							isForHashtag: false),
						word: _patch),
					CreateTagWithWord("[done]", _delete)
				});
		}

		[Fact]
		public void TestGenerate_GeneratesSentence_WithHashtag()
		{
			String sentence = "[#Doing]Things";

			String expectedGeneratedSentence = "#PatchingThings";

			AssertOutputIsCorrect(
				sentence,
				expectedGeneratedSentence,
				new TagWithWord[]
				{
					new TagWithWord(
						formatString: (word, tagString) => new RegularStringFormatter().FormatString(word, tagString),
						tag: new Tag(
							tagString: new TagString("[#Doing]", "[Doing]"),
							identifier: 1,
							wordSuitabilityPredicate: (_) => true,
							tagReplacer: new TagReplacer(),
							isForHashtag: true),
						word: _patch),
				});
		}

		[Fact]
		public void TestGenerate_GeneratesSentence_WithHashtagAndIdentifier()
		{
			String sentence = "[Doing1] things. [#Doing1]Things";

			String expectedGeneratedSentence = "Patching things. #PatchingThings";

			AssertOutputIsCorrect(
				sentence,
				expectedGeneratedSentence,
				new TagWithWord[]
				{
					new TagWithWord(
						formatString: (word, tagString) => new RegularStringFormatter().FormatString(word, tagString),
						tag: new Tag(
							tagString: new TagString("[Doing1]", "[Doing]"),
							identifier: 1,
							wordSuitabilityPredicate: (_) => true,
							tagReplacer: new TagReplacer(),
							isForHashtag: false),
						word: _patch),
					new TagWithWord(
						formatString: (word, tagString) => new RegularStringFormatter().FormatString(word, tagString),
						tag: new Tag(
							tagString: new TagString("[#Doing1]", "[Doing]"),
							identifier: 1,
							wordSuitabilityPredicate: (_) => true,
							tagReplacer: new TagReplacer(),
							isForHashtag: true),
						word: _patch),
				});
		}

		[Fact]
		public void TestGenerate_GeneratesSentence_WithMultipleWordHashtag()
		{
			String sentence = "[#Doing][#Things]";

			String expectedGeneratedSentence = "#PatchingServers";

			AssertOutputIsCorrect(
				sentence,
				expectedGeneratedSentence,
				new TagWithWord[]
				{
					new TagWithWord(
						formatString: (word, tagString) => new RegularStringFormatter().FormatString(word, tagString),
						tag: new Tag(
							tagString: new TagString("[#Doing]", "[Doing]"),
							identifier: 1,
							wordSuitabilityPredicate: (_) => true,
							tagReplacer: new TagReplacer(),
							isForHashtag: true),
						word: _patch),
					new TagWithWord(
						formatString: (word, tagString) => new RegularStringFormatter().FormatString(word, tagString),
						tag: new Tag(
							tagString: new TagString("[#Things]", "[Things]"),
							identifier: 1,
							wordSuitabilityPredicate: (_) => true,
							tagReplacer: new TagReplacer(),
							isForHashtag: true),
						word: _server),
				});
		}

		private static Word _patch => new Verb(new String[] { "patch", "patching", "patched", "patches" });
		private static Word _delete => new Verb(new String[] { "delete", "deleting", "deleted", "deletes" });
		private static Word _server => new Noun(new String[] { "a", "server", "servers" });
		private static Word _authenticated => new Adjective(new String[] { "an", "authenticated" });

		private TagWithWord CreateTagWithWord(String tagWord, Word word)
		{
			return
				new TagWithWord(
					(wordToFormat, tagString) => new RegularStringFormatter().FormatString(wordToFormat, tagString),
					new Tag(
						tagString: new TagString(tagWord, tagWord),
						wordSuitabilityPredicate: (_) => true,
						tagReplacer: new TagReplacer(),
						isForHashtag: false),
					word);
		}

		private void AssertOutputIsCorrect(
			String sentence,
			String expectedGeneratedSentence,
			IEnumerable<TagWithWord> tagsWithWords)
		{
			Assert.Equal(
				expectedGeneratedSentence,
				_wordProviderFixture.GenerateSentence(sentence, tagsWithWords));
		}
	}
}
