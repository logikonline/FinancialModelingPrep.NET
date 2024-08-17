using System.Collections.Generic;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Text;

namespace MatthiWare.FinancialModelingPrep.Core.Http
{
	public class QueryStringBuilder
	{
		private readonly Dictionary<string, List<string>> queryParams;

		public QueryStringBuilder()
		{
			queryParams = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
		}

		public void Add(string key, object value)
		{
			if (string.IsNullOrEmpty(key))
			{
				return;
			}

			var valueToAdd = value?.ToString().Trim();

			if (!queryParams.TryGetValue(key, out var values))
			{
				values = new List<string>();
				queryParams[key] = values;
			}

			values.Add(valueToAdd);
		}

		public override string ToString()
		{
			if (queryParams.Count == 0)
			{
				return string.Empty;
			}

			var sb = new StringBuilder();
			sb.Append('?');

			bool first = true;
			foreach (var kvp in queryParams)
			{
				foreach (var value in kvp.Value)
				{
					if (!first)
					{
						sb.Append('&');
					}
					sb.Append(Uri.EscapeDataString(kvp.Key));
					sb.Append('=');
					sb.Append(Uri.EscapeDataString(value ?? string.Empty));
					first = false;
				}
			}

			return sb.ToString();
		}

		public static QueryStringBuilder Parse(string query)
		{
			var builder = new QueryStringBuilder();
			if (string.IsNullOrEmpty(query))
				return builder;

			query = query.TrimStart('?');
			foreach (var pair in query.Split('&'))
			{
				var parts = pair.Split(new[] { '=' }, 2);
				var key = Uri.UnescapeDataString(parts[0]);
				var value = parts.Length > 1 ? Uri.UnescapeDataString(parts[1]) : null;
				builder.Add(key, value);
			}
			return builder;
		}
	}
}
