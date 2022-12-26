using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerSense : MonoBehaviour
{
	public float raycastDist = 5f;
	public LayerMask layerMask;
	public Transform raycastPoint;

	public delegate void OnEvent(Interactable item);
	public OnEvent OnHoverItem;

	private bool isOn = true;
	public TextMeshProUGUI infoText;
	public TextMeshProUGUI nameText;
	public GameObject textParent;

	Player player;
	void Start()
	{
		player = GetComponent<Player>();
	}

	//Update is called every frame.
	private void Update()
	{
		if (!isOn) return;

		GetCurrentHoverItem();
		UpdateHoverInfo();
	}

	//To disable/enable sensing and also disable hover over any curent ineractable."
	public void TurnOn()
	{
		isOn = true;
	}
	public void TurnOff()
	{
		isOn = false;

	}

	private void UpdateHoverInfo()
	{
		if (curHover != null)
		{
			textParent.SetActive(true);
			InteractionInfo hoverInfo = curHover.Interact(player, Input.GetKey(KeyCode.E));

			nameText.text = hoverInfo.interactableName;
			infoText.text = hoverInfo.info;
		}
		else
			textParent.SetActive(false);
	}

	Interactable curHover;


	private void GetCurrentHoverItem()
	{
		if (Physics.Raycast(raycastPoint.position, raycastPoint.forward, out RaycastHit hit, raycastDist, layerMask))
		{
			Debug.DrawRay(raycastPoint.position, raycastPoint.forward * hit.distance, Color.red);

			curHover = hit.collider.gameObject.GetComponentInParent<Interactable>();

		}
		else
		{
			Debug.DrawRay(raycastPoint.position, raycastPoint.forward * raycastDist, Color.green);
			curHover = null;
		}
	}
}