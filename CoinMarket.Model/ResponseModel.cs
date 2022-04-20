namespace CoinMarket.Model
{
    public class ResponseModel
    {
        public bool Done { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
    }
}