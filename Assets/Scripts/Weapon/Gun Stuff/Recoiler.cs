using UnityEngine;

//From Harrison, but seriously improved
public class Recoiler : MonoBehaviour
{
	[Header("Main Settings")]
	public Vector3 hipRecoilRotation;
	public Vector3 hipRecoilKickback;

	[Space(10)]
	public Vector3 aimRecoilRotation;
	public Vector3 aimRecoilKickback;

	[Header("Transforms")]
	public Transform RecoilPositionTranform;
	public Transform RecoilRotationTranform;

	[Header("Damping")]
	public float positionDampTime;
	public float rotationDampTime;

	[Header("Speeds")]
	public float recoilPositionSpeed;
	public float recoilRotationSpeed;

	[Header("Refs")]
	public Gun gun;
	public PlayerMovement playerMovement;

	[HideInInspector]
	public bool isAiming;

	//Private vars
	public delegate void OnChanged(Vector3 changePos, Vector3 changeRot);
	public event OnChanged OnRecoilChanged;

	private Vector3 localRotation;

	private Vector3 recoilRotation;
	private Vector3 recoilPosition;

	private void Start()
	{
		gun.OnFireEvent.AddListener(FireRecoil);
	}
	void LateUpdate()
	{
		//Lerp the rotation
		Vector3 oldRotation = recoilRotation;
		recoilRotation = Vector3.Lerp(recoilRotation, Vector3.zero, recoilPositionSpeed * Time.deltaTime);

		Vector3 oldRecoilPosition = recoilPosition;
		recoilPosition = Vector3.Lerp(recoilPosition, Vector3.zero, recoilRotationSpeed * Time.deltaTime);

		if (OnRecoilChanged != null) OnRecoilChanged(recoilPosition - oldRecoilPosition, recoilRotation - oldRotation);

		//Set the position
		RecoilPositionTranform.localPosition = Vector3.Lerp(RecoilPositionTranform.localPosition, recoilPosition, positionDampTime * Time.fixedDeltaTime);

		//Set the rotation
		localRotation = Vector3.Slerp(localRotation, recoilRotation, rotationDampTime * Time.fixedDeltaTime);
		RecoilRotationTranform.localRotation = Quaternion.Euler(localRotation);
	}
	private void FireRecoil()
	{
		//Add rotation
		Vector3 targetRecoilRotation = (isAiming) ? aimRecoilRotation : hipRecoilRotation;
		float xRot = -targetRecoilRotation.x;
		float yRot = Random.Range(-targetRecoilRotation.y, targetRecoilRotation.y);
		float zRot = Random.Range(-targetRecoilRotation.z, targetRecoilRotation.z);
		recoilRotation += new Vector3(xRot, yRot, zRot);

		//Add kickback
		Vector3 targetRecoilPosition = (isAiming) ? aimRecoilKickback : hipRecoilKickback;
		float xPos = Random.Range(-aimRecoilKickback.x, aimRecoilKickback.x);
		float yPos = Random.Range(-aimRecoilKickback.y, aimRecoilKickback.y);
		float zPos = -aimRecoilKickback.z;
		recoilPosition += new Vector3(xPos, yPos, zPos);
	}
}