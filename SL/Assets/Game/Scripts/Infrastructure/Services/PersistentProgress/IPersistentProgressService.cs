using Game.Scripts.Data;

namespace Game.Scripts.Infrastructure.Services.PersistentProgress
{
	public interface IPersistentProgressService : IService
	{
		PlayerProgress Progress { get; set; }
	}
}