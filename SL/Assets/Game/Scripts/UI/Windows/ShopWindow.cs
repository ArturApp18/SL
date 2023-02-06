using TMPro;

namespace Game.Scripts.UI.Windows
{
	public class ShopWindow : WindowBase
	{
		public TextMeshProUGUI DiskText;

		protected override void Initialize() =>
			RefreshDiskText();

		protected override void SubscribeUpdates() =>
			Progress.WorldData.LootData.Changed += RefreshDiskText;
		
		protected override void CleanUp()
		{
			base.CleanUp();
			Progress.WorldData.LootData.Changed -= RefreshDiskText;
		}

		private void RefreshDiskText() =>
			DiskText.text = Progress.WorldData.LootData.Collected.ToString();
	}
}