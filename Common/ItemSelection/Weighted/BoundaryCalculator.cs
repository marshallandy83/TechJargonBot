using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.ItemSelection.Weighted
{
	internal class BoundaryCalculator<T>
	{
		private Dictionary<T, Boundaries> _boundaries = new Dictionary<T, Boundaries>();

		public void Calculate(Item<T>[] weightedItems)
		{
			_boundaries.Clear();

			Int32 upperBoundaryOfPreviousItem = -1;

			foreach (Item<T> weightedItem in weightedItems)
			{
				Int32 lowerBoundary = upperBoundaryOfPreviousItem + 1;
				Int32 upperBoundary = lowerBoundary + weightedItem.Weighting - 1;

				_boundaries.Add(
					weightedItem.Contents,
					new Boundaries(
						lowerBoundary,
						upperBoundary));

				upperBoundaryOfPreviousItem = upperBoundary;
			}
		}

		public T ItemAtPosition(Int32 position)
		{
			T item = FindBoundaries(position).Key;

			return
				item != null
				? item
				: LastBoundaries.Key;
		}

		private KeyValuePair<T, Boundaries> FindBoundaries(Int32 position) =>
			_boundaries
			.Where(boundary =>
				boundary.Value.Lower <= position
				&&
				boundary.Value.Upper >= position)
			.SingleOrDefault();

		private KeyValuePair<T, Boundaries> LastBoundaries =>
			_boundaries
			.OrderByDescending(boundaries => boundaries.Value.Upper)
			.First();
	}
}
