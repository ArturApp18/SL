using Game.Scripts.Data;

namespace Game.Scripts.Infrastructure.Services.SaveLoad
{
	public interface ISaveLoadService : IService
	{
		void SaveProgress();
		PlayerProgress LoadProgress();
	}


}