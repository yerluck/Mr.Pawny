using UnityEngine;

namespace Pawny.AdditionalAction
{
    // What have character to perform with other action (ex. Pawn enemy jumps (addiotional action) when attacks).
    public abstract class AdditionalActionBase
    {
        /// <summary>
        /// Call it if need to get components from transform
        /// </summary>
        public virtual void Initialize(Transform transform) { }

        public abstract void PerformAction();
    }
}
