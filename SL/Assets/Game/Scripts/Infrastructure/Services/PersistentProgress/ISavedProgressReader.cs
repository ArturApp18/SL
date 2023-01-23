using Game.Scripts.Data;

namespace Game.Scripts.Infrastructure.Services.PersistentProgress
{
	public interface ISavedProgressReader
	{
		void LoadProgress(PlayerProgress progress);
	}

	public interface ISavedProgress : ISavedProgressReader
	{
		void UpdateProgress(PlayerProgress progress);
	}
}