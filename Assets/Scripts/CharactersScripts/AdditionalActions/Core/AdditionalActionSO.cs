using UnityEngine;

namespace Pawny.AdditionalAction
{
    public abstract class AdditionalActionSO: ScriptableObject
    {
        public abstract SpecificMoment ActionPerformMoment { get; }

        public abstract AdditionalActionBase CreateAdditionalAction();


        /// <summary>
		/// This enum is used to create flexible <c>AdditionalActions</c> which can execute in any of the 3 available "moments".
		/// The AdditionalAction in this case would have to use an if statement with this enum as a condition to decide whether to act or not in each moment.
		/// </summary>
        public enum SpecificMoment
        {
            OnEnable, OnUpdate, OnDisable
        }
    }

    /// <summary>
    /// Use this for standard behavior, otherwise implement own <see cref="AdditionalActionSO"/>
    /// </summary>
    /// <typeparam name="T"><see cref="AdditionalActionBase"/> that would be used in this SO</typeparam>
    public abstract class AdditionalActionSO<T> : AdditionalActionSO where T : AdditionalActionBase, new()
    {
        public override AdditionalActionBase CreateAdditionalAction() => new T();
    }
}
