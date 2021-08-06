using System.Collections.Generic;
using UnityEngine;
using Pawny.Systems.Factory;

namespace Pawny.Systems.PoolSystem
{
    public abstract class PoolSO<T> : ScriptableObject, IPool<T>
    {
        protected readonly Stack<T> Available = new Stack<T>();

        public abstract IFactory<T> Factory { get; set; }

        protected bool IsPrewarmed { get; set; } = false;

        public virtual void Prewarm(int num)
        {
            if(IsPrewarmed)
            {
#if UNITY_EDITOR
                Debug.Log("Already preawrmed");
#endif
                return;
            }
            for (int i = 0; i < num; i++)
            {
                Available.Push(Create());
            }
            IsPrewarmed = true;
        }

        protected virtual T Create()
        {
            return Factory.Create();
        }

        public virtual T Request()
        {
            return Available.Count > 0 ? Available.Pop() : Factory.Create();
        }

        public virtual void Return(T member)
        {
            Available.Push(member);
        }

        public virtual void OnDisable()
        {
            Available.Clear();
            IsPrewarmed = false;
        }
    }
}
