using System;

namespace TechJargonBot.Business.Data
{
	partial class SentenceType
	{
		internal class Status : SentenceType
		{
			public override String CsvFilename => "Sentences";
		}
	}
}
