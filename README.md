﻿# Arex388.Geocodio
Geocod.io API Implementation in C#

To use, create a new instance of `GeocodioClient` and pass in an instance of `HttpClient` and your API key. The original API documentation can be found [here][0]. For more information, please visit [arex388.com][1].

    var geocodio = new GeocodioClient(httpClient, "{key}");

**Get Geocode**

    var geocode = await geocodio.GetGeocodeAsync("1600 Pennsylvania Ave NW, Washington, DC 20500");

**Get Geocode Batch**

    var geocodeBatch = await geocodio.GetGeocodeBatchAsync(new[] {
		"1600 Pennsylvania Ave NW, Washington, DC 20500",
		"East Capitol St NE & First St SE, Washington, DC 20004"
	});

**Get Reverse Geocode**

    var reverse = await geocodio.GetReverseGeocodeAsync("38.897675,-77.036547");

**Get Reverse Geocode Batch**

    var reverseBatch = await geocodio.GetReverseGeocodeBatchAsync(new[] {
		"38.897675,-77.036547",
		"38.898976,-77.038219"
	});

[0]:https://www.geocod.io/docs
[1]:https://arex388.com