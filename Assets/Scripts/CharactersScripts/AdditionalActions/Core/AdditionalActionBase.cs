using UnityEngine;

namespace Pawny.AdditionalAction
{
    public abstract class AdditionalActionBase
    {
        /// <summary>
        /// Call it if need to get components from transform
        /// </summary>
        public virtual void Initialize(Transform transform) { }

        public abstract void PerformAction();
    }
}
