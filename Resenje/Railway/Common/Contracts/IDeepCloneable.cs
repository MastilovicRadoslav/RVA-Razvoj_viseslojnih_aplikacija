namespace Common.Contracts
{
	public interface IDeepCloneable<T> where T : class //Ovaj interfejs definiše metodu za kreiranje duboke kopije objekta.
	{
		T DeepCopy();
	}
}
