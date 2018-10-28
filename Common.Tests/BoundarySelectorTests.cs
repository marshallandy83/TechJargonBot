using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Common.ItemSelection.Weighted
{
	public class BoundarySelectorTests
	{
		private readonly BoundaryCalculator<String> _boundaryCalculator = new BoundaryCalculator<String>();

		public BoundarySelectorTests()
		{
			var item1 = new Item<String>("1", 1);
			var item2 = new Item<String>("2", 2);
			var item3 = new Item<String>("3", 3);

			_boundaryCalculator.Calculate(new[] { item1, item2, item3 });
		}

		[Theory]
		[InlineData(0, "1")]
		[InlineData(1, "2")]
		[InlineData(2, "2")]
		[InlineData(3, "3")]
		[InlineData(4, "3")]
		[InlineData(5, "3")]
		[InlineData(6, "3")]
		public void ItemAtPosition(
			Int32 position,
			String expectedItem)
		{
			Assert.Equal(expectedItem, _boundaryCalculator.ItemAtPosition(position));
		}
	}
}
