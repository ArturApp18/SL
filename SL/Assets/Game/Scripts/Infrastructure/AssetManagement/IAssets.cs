using Game.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Game.Scripts.Infrastructure.AssetManagement
{
	public interface IAssets : IService
	{
		GameObject Instantiate(string path);
		GameObject Instantiate(string path, Vector2 at);
	}
}