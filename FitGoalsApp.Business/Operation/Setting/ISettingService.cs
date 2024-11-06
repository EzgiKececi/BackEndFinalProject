using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Setting
{
    public interface ISettingService
    {
        Task ToggleMaintenence();          
        bool GetMaintenenceState();
    }
}
