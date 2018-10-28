using System;
using System.Linq;

namespace Common.ItemSelection.Weighted
{
	public class Selector<T> : ISelector<T>
	{
		private readonly Random _randomNumberGenerator;
		private readonly BoundaryCalculator<T> _boundaryCalculator;
		private readonly Int32 _highestBoundary;

		public Selector(Item<T>[] items)
		{
			_randomNumberGenerator = new Random();
			_highestBoundary = items.Sum(item => item.Weighting) - 1;

			_boundaryCalculator = new BoundaryCalculator<T>();
			_boundaryCalculator.Calculate(items);
		}

		public T Select() =>
			_boundaryCalculator.ItemAtPosition(
				_randomNumberGenerator.Next(0, _highestBoundary));
	}
}