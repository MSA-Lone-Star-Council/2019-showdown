using System;
namespace Common.Common.Models
{
	public struct Score
	{
		public string Team { get; set; }

		// TODO: map this field to JSON field "Score" - can't use Score as a field name
		public int Points { get; set; }
	}
}
