using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CoinMarket.Model.Models
{
    public class CoinMarketResponse
    {
        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }
    public class Status
    {
        public DateTime Timestamp { get; set; }

        public int error_code { get; set; }

        public string error_message { get; set; }

        public int Elapsed { get; set; }

        public int credit_count { get; set; }

        public string Notice { get; set; }
    }

    public class USD
    {
        public double Price { get; set; }

        public double volume_24h { get; set; }

        public double volume_change_24h { get; set; }


        public double percent_change_1h { get; set; }

        public double percent_change_24h { get; set; }

        public double percent_change_7d { get; set; }

        public double percent_change_30d { get; set; }

        public double percent_change_60d { get; set; }

        public double percent_change_90d { get; set; }

        public double market_cap { get; set; }
        public double market_cap_dominance { get; set; }
        public double fully_diluted_market_cap { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    public class Quote
    {
        [JsonPropertyName("USD")]
        public USD USD { get; set; }
    }

    public class BNB
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Slug { get; set; }

        public int num_market_pairs { get; set; }


        public DateTime date_added { get; set; }

        public List<string> Tags { get; set; }

        public int max_supply { get; set; }

        public double circulating_supply { get; set; }

        public double total_supply { get; set; }

        public int is_active { get; set; }

        public string Platform { get; set; }

        public int cmc_rank { get; set; }

        public int is_fiat { get; set; }

        public string self_reported_circulating_supply { get; set; }

        public string self_reported_market_cap { get; set; }

        public DateTime last_updated { get; set; }

        public Quote Quote { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("BNB")]
        public BNB BNB { get; set; }
    }

}