using Game.Scripts.Infrastructure.Services;

namespace Game.Scripts.UI.Services.Window
{
	public interface IWindowsService : IService
	{
		void Open(WindowId windowId);
	}
}