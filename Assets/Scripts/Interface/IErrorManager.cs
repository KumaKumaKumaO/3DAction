public interface IErrorManager
{
	public void InstantiationMyInstance();
	public void DeleteMyInstance();
	public void SingleTonError(string className);
	public void NullError(string className);
}
