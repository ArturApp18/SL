using System.Threading.Tasks;
using Game.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Scripts.Infrastructure.AssetManagement
{
	public interface IAssets : IService
	{
		Task<GameObject> Instantiate(string address);
		Task<GameObject> Instantiate(string address, Vector2 at);
		Task<T> Load<T>(AssetReference assetReference) where T : class;
		void Cleanup();
		Task<T> Load<T>(string address) where T : class;
		void Instantiate();
	}
}