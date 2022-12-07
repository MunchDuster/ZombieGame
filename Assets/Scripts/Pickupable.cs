using UnityEngine;
using UnityEngine.Events;

public class Pickupable : Interactable
{
	public UnityEvent OnPickup;
	public override InteractionInfo Interact(Transform player, bool active)
	{
		if (!active)
		{
			return InteractionInfo.Fail("Press E to pick up.");
		}
		if (OnPickup != null) OnPickup.Invoke();
		return InteractionInfo.Success();
	}
}