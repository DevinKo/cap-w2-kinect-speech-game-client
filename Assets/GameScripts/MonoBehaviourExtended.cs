using UnityEngine;

namespace Assets.GameScripts
{
    static public class MethodExtensionForMonoBehaviourTransform
    {
        /// <summary>
        /// Gets or add a component.
        /// </summary>
        static public T GetOrAddComponent<T>(this Component child) where T : Component
        {
            T result = child.GetComponent<T>();
            if (result == null)
            {
                result = child.gameObject.AddComponent<T>();
            }
            return result;
        }
    }

}