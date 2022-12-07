using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	[Header("Hover Info")]
	public string hoverName;
	public string hoverInfoText;

	[Header("Interactable References")]
	public Transform hoverInfoPoint;

	public void SetHoverName(string hoverName)
	{
		this.hoverName = hoverName;
	}
	public void SetHoverInfo(string hoverInfo)
	{
		hoverInfoText = hoverInfo;
	}

	//On click
	public abstract InteractionInfo Interact(Transform player, bool active);

	//Shows hover info
	public void StartHover(HoverInfo hoverInfo)
	{

		//Show info
		hoverInfo.gameObject.SetActive(true);
		hoverInfo.SetInfo(hoverName, hoverInfoText);
		hoverInfo.positionPoint = hoverInfoPoint;
	}

	//Hides hover info
	public void EndHover(HoverInfo hoverInfo)
	{
		hoverInfo.gameObject.SetActive(false);
	}
}
