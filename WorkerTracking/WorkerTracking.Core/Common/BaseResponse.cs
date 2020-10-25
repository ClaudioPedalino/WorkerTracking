namespace WorkerTracking.Core.Common
{
    public class BaseResponse<T>
    {
        public BaseResponse() { }

        public BaseResponse(T response)
        {
            Data = response;
        }

        public T Data { get; set; }
    }

}
