﻿using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Arex388.Geocodio {
	public sealed class GeocodeRequest :
		RequestBase {
		public string Address { get; set; }
		public override string Endpoint {
			get {
				var parameters = new HashSet<string> {
					$"q={WebUtility.UrlEncode(Address)}"
				};

				if (Fields.Any()) {
					var fields = string.Join(",", Fields);

					parameters.Add($"fields={fields}");
				}

				var query = string.Join("&", parameters);

				return $"geocode?{query}";
			}
		}
		public IList<string> Fields { get; set; } = new List<string>();
	}
}