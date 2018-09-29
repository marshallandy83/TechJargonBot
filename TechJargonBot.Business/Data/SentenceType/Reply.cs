using System;

namespace TechJargonBot.Business.Data
{
	partial class SentenceType
	{
		internal class Reply : SentenceType
		{
			public override String CsvFilename => "Sentences";
		}
	}
}
