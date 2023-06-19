namespace Ottawa.Pigeon.Application.Interfaces
{
    public interface IUserAccessor
    {
        public bool AllowUserAccess(int requestId);
    }
}
