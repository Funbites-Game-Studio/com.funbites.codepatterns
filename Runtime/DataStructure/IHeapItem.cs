namespace Funbites.Patterns
{
	public interface IHeapItem<T> : System.IComparable<T>
	{
		int HeapIndex {
			get;
			set;
		}
	}
}