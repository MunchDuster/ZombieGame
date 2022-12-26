using UnityEngine;
using UnityEngine.Events;

public class Pickupable : Interactable
{
	public UnityEvent OnPickup;

	Player player;
	public override InteractionInfo Interact(Player player, bool active)
	{
		if (!active)
		{
			return InteractionInfo.Fail("Press E to pick up.", hoverName);
		}
		this.player = player;
		if (OnPickup != null) OnPickup.Invoke();
		return InteractionInfo.Success();
	}

	public void EquipPlayer(GameObject prefab)
	{
		player.weaponManager.PickupWeapon(prefab);
	}
}