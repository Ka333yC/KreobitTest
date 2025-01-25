using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
	public static class ListExtensions
	{
		public static void Shuffle<T>(this IList<T> list)  
		{  
			Random random = new Random();
			for(int i = list.Count - 1; i >= 0; i--)
			{
				int randomIndex = random.Next(i + 1);
				var value = list[randomIndex];
				list[randomIndex] = list[i];
				list[i] = value;
			}
		}
	}
}