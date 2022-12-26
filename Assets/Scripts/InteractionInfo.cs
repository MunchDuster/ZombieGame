public class InteractionInfo
{
	public bool success;
	public string interactableName;
	public string info;

	public static InteractionInfo Success()
	{
		InteractionInfo info = new InteractionInfo();

		info.success = true;

		return info;
	}
	public static InteractionInfo Fail(string error, string interactableName)
	{
		InteractionInfo info = new InteractionInfo();

		info.interactableName = interactableName;
		info.success = false;
		info.info = error;

		return info;
	}
}