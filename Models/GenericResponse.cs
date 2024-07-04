namespace BlogManagementApp_BE.models
{
    public class GenericResponse<T>
    {
        public string StatusMessage { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }
    }
}
