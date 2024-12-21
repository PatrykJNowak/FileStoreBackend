namespace FileStore.Api.Extensions;

public static class GuidExtension
{
    public static Guid? GetNullIfGuidIsEmpty(this Guid guid)
    {
        return guid == Guid.Empty ? null : guid;
    }
    
    public static Guid? GetNullIfGuidIsEmpty(this Guid? guid)
    {
        return guid == Guid.Empty ? null : guid;
    }
}