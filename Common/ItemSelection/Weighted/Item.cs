using System;

namespace Common.ItemSelection.Weighted
{
	public partial class Item<T>
	{
		public Item(T contents, Int32 weighting)
		{
			Contents = contents;
			Weighting = weighting;
		}

		public T Contents { get; }
		public Int32 Weighting { get; }
	}
}