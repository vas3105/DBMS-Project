
// Services/EmployeeStateService.cs
using System;
using System.Threading.Tasks;

namespace DBMSproj.Services;

public class EmployeeStateService
{
    private string _employeeId; //fetch the id during login

    public string EmployeeId
    {
        get => _employeeId;
        set
        {
            if (_employeeId != value)
            {
                _employeeId = value;
                NotifyStateChanged();
            }
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
