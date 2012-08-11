using System;

namespace BLL.Model.Managers
{
    public interface IParameterManager
    {
        bool Set(string key, string value);
        string Get(string key);

        DateTime GetCurrentFinancialYearStartDate();
        void SetCurrentFinancialYear(string currentFinancialYear);
        event EventHandler<BLLEventArgs> ManagerEvent;
        string ModuleName { get; }
        void InvokeManagerEvent(string message);
        void InvokeManagerEvent(EventType eventType, string messageKey = null, string messageDescription = null);
        void InvokeManagerEvent(Exception exception);
        void InvokeManagerEvent(BLLEventArgs eventArgs);
        string GetCurrentFinancialYear();
    }

}