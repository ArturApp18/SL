using Game.Scripts.Data;

namespace Game.Scripts.Infrastructure.Services.PersistentProgress
{

	public class PersistentProgressService : IPersistentProgressService
	{
		public PlayerProgress Progress { get; set; }
	}
}