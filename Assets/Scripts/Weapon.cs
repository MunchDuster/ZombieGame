public interface IWeapon
{
	public abstract void Fire();
	public abstract bool CanUse();
	public void SetActive(bool active);
}