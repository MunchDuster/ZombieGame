using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class HitInfo
{
	public readonly bool wasAlreadyDead;
	public readonly bool killedEnemy;
	public readonly float damageDealt;
	public readonly Vector3 point;
	public readonly IWeapon weapon;
	public readonly Transform attacker;
	public readonly Transform hitPerson;
	public readonly string causeOfDamage; //System hitType only

	public HitInfo()
	{
		this.wasAlreadyDead = true;
	}
	public HitInfo(bool killedEnemy, float damageDealt, IWeapon weapon, Transform damager, Transform hitPerson, Vector3 point)
	{
		this.killedEnemy = killedEnemy;
		this.damageDealt = damageDealt;
		this.weapon = weapon;
		this.attacker = damager;
		this.hitPerson = hitPerson;
		this.point = point;
	}
}