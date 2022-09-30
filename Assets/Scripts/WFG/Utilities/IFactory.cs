using UnityEngine;

namespace WFG.Utilities
{
    /// <summary>
    /// Interface that any factory must implement.
    /// </summary>
    /// <typeparam name="T">Type of the product that this factory will produce. Must be an Object and IProduct.</typeparam>
    public interface IFactory<T>  where T:  Object, IProduct
    {
        T Produce();
    }
}