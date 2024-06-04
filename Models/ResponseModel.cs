namespace DemoApp.Models
{
    public class ResponseModel<T>
    {
        public T? Data { set; get; }
        public bool IsSucceed { set; get; }
        public string ErrorMessage { set; get; }
        public string ResultMessageString { set; get; }
        public int StatusCode { set; get; }
    }
}
