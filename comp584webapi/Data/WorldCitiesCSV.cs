﻿namespace comp584webapi.Data
{
    public class WorldCitiesCSV
    {
        public string city { get; set; } = null!;
        public string city_ascii { get; set; } = null!;
        public double lat { get; set; }
        public double lng { get; set; }
        public string country { get; set; } = null!;
        public string iso2 { get; set; } = null!;
        public string iso3 { get; set; } = null!;
        public decimal? population { get; set; }
        public long id { get; set; }
    }
}
