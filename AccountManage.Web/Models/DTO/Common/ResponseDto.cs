namespace AccountManagement.Web.Models.DTO.Common;

public class ResponseDto
{
    public object Result { get; set; }

    public bool IsSucess { get; set; } = true;

    public string Message { get; set; } = "";
}

public class ServiceResult
{
    public string MessageType { get; set; }
    public List<string> Message { get; set; }
    public bool Status { get; set; }
}

public class ServiceResult<T> : ServiceResult
{
    private T ResposeData { get; set; }
    public T Data
    {
        get => ResposeData;
        set => ResposeData = value;
    }
}

