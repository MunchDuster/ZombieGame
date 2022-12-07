using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Gun : MonoBehaviour, IWeapon
{

	public bool canUse;
	public bool CanUse() { return canUse; }

	[Header("Gun Settings")]
	public bool isAuto;
	public float recoilTime = 0.4f;
	public LayerMask shootLayerMask;
	public float damage = 30;

	[Space(10)]
	public ParticleSystem muzzleFlashFX;
	public ParticleSystem[] hitFX;
	public ParticleSystem[] hitZombieFX;
	int hitFXIndex = 0;
	int hitZombieFXIndex = 0;
	public Transform shootPoint;
	public Transform aimPoint;
	public Transform character;
	public Animator animator;
	public PlayerMoney playerMoney;


	public void TakeClips(int clips) { this.clips += clips; }

	[Header("Reload Settings")]

	public int clipSize = 10;
	public float reloadTime = 1.2f;
	public int startclips = 3;

	[Header("Outer Refs")]
	public TextMeshProUGUI clipText;
	public TextMeshProUGUI ammoText;

	public delegate void OnBlankEvent();

	[Space(10)]
	public OnBlankEvent OnFire;
	public UnityEvent OnFireEvent;
	public OnBlankEvent OnReload;
	public OnBlankEvent OnReloadFinish;
	public UnityEvent OnReloadFinishEvent;
	public UnityEvent OnReloadEvent;

	[HideInInspector] public bool isReloading;

	private int _clips;
	private int clips
	{
		get { return _clips; }
		set { _clips = value; UpdateClipText(); }
	}

	private int _ammoInClip;
	private int ammoInClip
	{
		get { return _ammoInClip; }
		set { _ammoInClip = value; UpdateAmmoText(); }

	}
	public void MakeUsable()
	{
		canUse = true;
	}

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}

	void Start()
	{
		ammoInClip = clipSize;
		clips = startclips;
	}
	private void UpdateClipText()
	{
		if (clipText != null)
		{
			clipText.text = "" + clips;
		}
	}
	private void UpdateAmmoText()
	{
		if (ammoText != null)
		{
			ammoText.text = "" + ammoInClip;
		}
	}


	//This is used by all guns
	protected bool isRecoiling { get { return !(Time.time - timeLastFired >= recoilTime); } }

	protected float timeLastFired = -1000;
	public UnityEvent OnHit;
	//Fire the gun
	public void Fire()
	{
		if (OnFire != null) OnFire();

		if (OnFireEvent != null) OnFireEvent.Invoke();

		timeLastFired = Time.time;

		ammoInClip--;

		//Show the muzzle flash
		muzzleFlashFX.Play();

		//Fire the bullet
		if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hit, 100, shootLayerMask, QueryTriggerInteraction.Ignore))
		{
			Health health = Health.FindHealth(hit.transform);
			if (health != null)
			{
				HitInfo hitInfo = health.TakeDamage(hit.point, hit.collider, this, character, damage);

				if (hitInfo != null)
				{
					OnHit.Invoke();
					if (hitInfo.killedEnemy)
						playerMoney.AddMoney(health.value);

				}

				hitZombieFXIndex++;
				hitZombieFXIndex %= hitZombieFX.Length;
				hitZombieFX[hitZombieFXIndex].transform.position = hit.point;
				hitZombieFX[hitZombieFXIndex].transform.rotation = Quaternion.LookRotation(hit.normal);
				hitZombieFX[hitZombieFXIndex].Play();
			}
			else
			{
				hitFXIndex++;
				hitFXIndex %= hitFX.Length;
				hitFX[hitFXIndex].transform.position = hit.point;
				hitFX[hitFXIndex].transform.rotation = Quaternion.LookRotation(hit.normal);
				hitFX[hitFXIndex].Play();
			}
		}
		StartCoroutine(Recoil());
	}
	void Update()
	{
		bool firePressed = Input.GetKey(KeyCode.Mouse0);
		if (firePressed)
		{
			if (ammoInClip <= 0)
			{
				Reload();
				return;
			}
			if (!isRecoiling && !isReloading)
			{
				Fire();
			}
			return;
		}

		bool reloadPressed = Input.GetKeyDown(KeyCode.R);
		if (reloadPressed && !isReloading)
		{
			Reload();
		}
	}
	private IEnumerator Recoil()
	{
		yield return new WaitForSeconds(recoilTime);

		//Auto reload if no ammo left
		bool firePressed = Input.GetKey(KeyCode.Mouse0);

		if (ammoInClip <= 0)
		{
			Reload();
		}
		else if (isAuto && firePressed)
		{
			Fire();
		}
	}
	private void Reload()
	{
		if (clips <= 0) return;

		if (OnReload != null) OnReload();
		if (OnReloadEvent != null) OnReloadEvent.Invoke();

		isReloading = true;

		animator.SetTrigger("Reload");
	}

	public void ReloadFinish()
	{
		if (OnReloadFinish != null) OnReloadFinish();
		if (OnReloadFinishEvent != null) OnReloadFinishEvent.Invoke();

		animator.ResetTrigger("Reload");

		ammoInClip = clipSize;

		clips--;

		isReloading = false;

		bool firePressed = Input.GetKey(KeyCode.Mouse0);
		if (isAuto && firePressed)
		{
			Fire();
		}
	}

	//Check if can be fired
	public bool CanBeFired()
	{
		return (!isRecoiling && !isReloading);
	}

	//Check if can be fired, ignoring isRecoiling
	public bool CanBeFiredIgnoreIsRecoiling()
	{
		return !isReloading;
	}

	//Get offset for aiming
	public virtual Vector3 GetAimOffset() { return (transform.position - aimPoint.position); }
}