using System.Text;

public static class StringExtensions
{
	public static string ToCommaSeparatedList(this List<string> items)
	{
		StringBuilder sb = new StringBuilder();
		
		int count = items.Count - 1;
		for (int n = 0; n <= count; n++) 
		{
			sb.Append(items[n]);

			if(n < count)
			{
				sb.Append(", ");
			}
		}

		return sb.ToString();
	}
}