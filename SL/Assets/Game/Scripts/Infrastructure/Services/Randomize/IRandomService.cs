namespace Game.Scripts.Infrastructure.Services.Randomize
{
	public interface IRandomService : IService
	{
		int Next(int min, int max);
	}
}