using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Gun : Weapon
{
	[Header("Gun Settings")]
	public bool isAuto;
	public float recoilTime = 0.4f;
	public LayerMask shootLayerMask;

	[Space(10)]
	public ParticleSystem[] hitFX;
	public ParticleSystem[] hitZombieFX;
	int hitFXIndex = 0;
	int hitZombieFXIndex = 0;
	public Transform shootPoint;
	public Transform aimPoint;
	public Recoiler recoiler; //Used by WeapanManager to quickly access recoiler (not used yet)
	public Aimer aimer; //Used by WeapanManager to quickly access recoiler

	public Transform pointToCenterOfScreenTransform;


	public void TakeClips(int reserveAmmo) { this.reserveAmmo += reserveAmmo; }

	[Header("Reload Settings")]

	public int clipSize = 10;
	public float reloadTime = 1.2f;
	public int startclips = 3;

	[Header("Outer Refs")]
	public TextMeshProUGUI clipAmmoText;
	public TextMeshProUGUI reserveAmmoText;
	public Transform character;


	public delegate void OnBlankEvent();

	[Space(10)]
	public UnityEvent OnFireEvent;
	public UnityEvent OnReloadFinishEvent;
	public UnityEvent OnReloadEvent;

	[HideInInspector] public bool isReloading;

	private int _clips;
	private int reserveAmmo
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

	void Start()
	{
		ammoInClip = clipSize;
		reserveAmmo = startclips;
	}
	private void UpdateClipText()
	{
		if (reserveAmmoText != null)
		{
			reserveAmmoText.text = "" + reserveAmmo;
		}
	}
	private void UpdateAmmoText()
	{
		if (clipAmmoText != null)
		{
			clipAmmoText.text = "" + ammoInClip;
		}
	}


	//This is used by all guns
	protected bool isRecoiling { get { return !(Time.time - timeLastFired >= recoilTime); } }

	protected float timeLastFired = -1000;
	public UnityEvent OnHit;
	//Fire the gun
	public override void Fire()
	{
		if (OnFireEvent != null) OnFireEvent.Invoke();

		timeLastFired = Time.time;

		ammoInClip--;

		//Fire the bullet
		if (hit.point != null)
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

	RaycastHit hit;
	void Update()
	{
		if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit rayHit, 100, shootLayerMask, QueryTriggerInteraction.Ignore))
		{
			hit = rayHit;
			UpdateGunAngle();
		}
		else
		{
			hit = new RaycastHit();
		}

		bool firePressed = isAuto ? Input.GetKey(KeyCode.Mouse0) : Input.GetKeyDown(KeyCode.Mouse0);
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
	public LayerMask pointToScreenMask;
	void UpdateGunAngle()
	{

		if (!isReloading && !isRecoiling && Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit screenHit, pointToScreenMask))
			pointToCenterOfScreenTransform.LookAt(screenHit.point);
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
		if (reserveAmmo <= 0) return;
		if (OnReloadEvent != null) OnReloadEvent.Invoke();

		isReloading = true;

		animator.SetTrigger("Reload");
	}

	public void ReloadFinish()
	{
		if (OnReloadFinishEvent != null) OnReloadFinishEvent.Invoke();

		animator.ResetTrigger("Reload");

		ammoInClip = Mathf.Min(clipSize, reserveAmmo);

		reserveAmmo = Mathf.Max(0, reserveAmmo - clipSize);

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