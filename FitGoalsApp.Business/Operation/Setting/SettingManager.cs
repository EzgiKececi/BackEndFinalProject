using FitGoalsApp.Data.Entities;
using FitGoalsApp.Data.Repositories;
using FitGoalsApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Setting
{
    public class SettingManager : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<SettingEntity> _settingRepository;

        public SettingManager(IUnitOfWork unitOfWork, IRepository<SettingEntity> settingRepository)
        {
            _settingRepository = settingRepository;
            _unitOfWork = unitOfWork;
        }

        public bool GetMaintenenceState()
        {
            var maintenenceState = _settingRepository.GetById(1).MaintenenceMode;

            return maintenenceState;
        }

        public async Task ToggleMaintenence()
        {
            var setting = _settingRepository.GetById(1);

            setting.MaintenenceMode = !setting.MaintenenceMode;

            _settingRepository.Update(setting);

            try
            {
                await _unitOfWork.SaveChangesAsync();   
            }
            catch (Exception)
            {

                throw new Exception("Bakım durumu güncellenirken bir hata ile karşılaşıldı.");
            }
        }
    }
}
