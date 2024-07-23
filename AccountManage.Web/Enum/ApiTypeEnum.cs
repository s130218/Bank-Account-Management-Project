namespace AccountManage.Web.Enum
{
    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public enum CustomerAccountStatus
    {
        INACTIVE = 0,
        ACTIVE = 1
    }

    public enum TransactionTypeEnum
    {
        DEPOSIT = 1,
        WITHDRAW = 2,
    }
}
