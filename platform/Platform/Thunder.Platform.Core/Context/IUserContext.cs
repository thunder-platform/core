namespace Thunder.Platform.Core.Context
{
    public interface IUserContext
    {
        void ImportContents(UserContextContents userContextContents);

        UserContextContents ExportContents();

        void SetUserContextStore(IUserContextStore userContextStore);

        T GetValue<T>(string contextKey);

        void SetValue(object value, string contextKey);

        void Clear();
    }
}
