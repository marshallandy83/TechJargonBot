using System;

namespace Common.ItemSelection.Weighted
{
	internal class Boundaries : IEquatable<Boundaries>
	{
		public Boundaries(Int32 lower, Int32 upper)
		{
			Lower = lower;
			Upper = upper;
		}

		public Int32 Lower { get; }
		public Int32 Upper { get; }

		public Boolean Equals(Boundaries other) =>
			Lower == other.Lower
			&&
			Upper == other.Upper;
	}
}
