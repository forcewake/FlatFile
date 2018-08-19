namespace FluentFiles.Core
{
    /// <summary>
    /// Determines how master-detail record relationships are handled.
    /// </summary>
    public interface IMasterDetailTracker
    {
        void HandleMasterDetail(object entry, out bool isDetailRecord);
    }
}