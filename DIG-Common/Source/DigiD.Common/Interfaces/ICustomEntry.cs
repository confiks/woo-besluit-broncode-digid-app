namespace DigiD.Common.Interfaces
{
    public interface ICustomEntry
    {
        string AutomationId { get; set; }
        bool IsValid { get; set; }
        string ErrorText { get; set; }
        void Focus();
    }
}
