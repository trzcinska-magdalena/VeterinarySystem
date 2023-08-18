namespace VeterinarySystem.Service.IService
{
    public interface ILoggerService
    {
        void SetLogError(Exception ex);
        void SetLogInfo(string information);
        void SetLogWarning(string information);
    }
}
