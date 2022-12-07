using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Door : Interactable
{
	public float cost = 500;
	public bool unlocked;

	//UnityEvents triggering chain events
	public UnityEvent<bool> OnOpen;

	public UnityEvent<bool> OnUnlock;

	[SerializeField]
	private bool open;

	//Reference to animator
	public Animator[] animators;

	//Start is called before first update.
	private void Start()
	{

		//SetOpen(open);
		UpdateLocked(unlocked);
	}

	//Called by UnityEvents to chage hover info
	public void SetInfo(string info)
	{
		hoverInfoText = info;
	}


	//Attempt to toggle open/close if not locked
	public override InteractionInfo Interact(Transform player, bool active)
	{
		if (!unlocked)
		{
			PlayerMoney money = player.GetComponent<PlayerMoney>();
			if (money.money >= cost)
			{
				if (active)
				{
					money.AddMoney(-cost);
					SetOpen(true);
					unlocked = true;
				}
			}
			else
			{
				return InteractionInfo.Fail("Costs " + cost + " to unlock.");
			}

			return InteractionInfo.Success();
		}

		return InteractionInfo.Fail("Already unlocked");
	}

	//Main opening/closing door function
	public void SetOpen(bool open)
	{
		this.open = open;
		foreach (Animator animator in animators)
		{
			animator.SetBool("open", open);
		}

		if (OnOpen != null) OnOpen.Invoke(open);
	}

	//Call UnLock event when all requirements are met
	private void UpdateLocked(bool unlocked)
	{
		if (OnUnlock != null) OnUnlock.Invoke(unlocked);
	}

	public string autoCompleteTaskName;

	public void OpenAfter(float time)
	{
		StartCoroutine(SetOpenAfter(time, true));
	}

	private IEnumerator SetOpenAfter(float time, bool open)
	{
		yield return new WaitForSeconds(time);
		SetOpen(open);
	}
}
