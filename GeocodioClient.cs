﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Arex388.Geocodio {
	public sealed class GeocodioClient {
		private HttpClient Client { get; }
		private string Key { get; }

		public GeocodioClient(
			HttpClient client,
			string key) {
			Client = client;
			Key = key;
		}

		public async Task<GeocodeResponse> GetGeocodeAsync(
			string address,
			params string[] fields) {
			return await GetGeocodeAsync(new GeocodeRequest {
				Address = address,
				Fields = fields
			});
		}

		public async Task<GeocodeResponse> GetGeocodeAsync(
			GeocodeRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<GeocodeResponse>(response);
		}

		public async Task<GeocodeBatchResponse> GetGeocodeBatchAsync(
			IEnumerable<string> addresses,
			params string[] fields) {
			return await GetGeocodeBatchAsync(new GeocodeBatchRequest {
				Addresses = addresses,
				Fields = fields
			});
		}

		public async Task<GeocodeBatchResponse> GetGeocodeBatchAsync(
			GeocodeBatchRequest request) {
			if (request == null
				|| request.Addresses.Count() > 10000) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<GeocodeBatchResponse>(response);
		}

		public async Task<GeocodeResponse> GetReverseGeocodeAsync(
			decimal latitude,
			decimal longitude,
			params string[] fields) {
			return await GetReverseGeocodeAsync(new ReverseGeocodeRequest {
				Fields = fields,
				Latitude = latitude,
				Longitude = longitude
			});
		}

		public async Task<GeocodeResponse> GetReverseGeocodeAsync(
			ReverseGeocodeRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<GeocodeResponse>(response);
		}

		public async Task<GeocodeBatchResponse> GetReverseGeocodeBatchAsync(
			IEnumerable<string> coordinates,
			params string[] fields) {
			return await GetReverseGeocodeBatchAsync(new ReverseGeocodeBatchRequest {
				Coordinates = coordinates,
				Fields = fields
			});
		}

		public async Task<GeocodeBatchResponse> GetReverseGeocodeBatchAsync(
			ReverseGeocodeBatchRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<GeocodeBatchResponse>(response);
		}

		private async Task<string> GetResponseAsync(
			RequestBase request) {
			var endpoint = $"{request.Endpoint}&api_key={Key}";

			try {
				if (request.Method == HttpMethod.Get) {
					return await Client.GetStringAsync(endpoint);
				}

				using (var content = new StringContent(request.Body, Encoding.UTF8, "application/json")) {
					using (var message = await Client.PostAsync(endpoint, content)) {
						return await message.Content.ReadAsStringAsync();
					}
				}
			} catch (HttpRequestException) {
				return null;
			}
		}
	}
}