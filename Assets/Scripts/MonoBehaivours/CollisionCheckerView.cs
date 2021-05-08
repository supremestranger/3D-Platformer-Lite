using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CollisionCheckerView : MonoBehaviour
    {
        // auto-injected fields.
        public EcsWorld ecsWorld { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            var hit = ecsWorld.NewEntity();

            var hitPool = ecsWorld.GetPool<HitComponent>();
            hitPool.Add(hit);
            ref var hitComponent = ref hitPool.Get(hit);

            hitComponent.first = transform.root.gameObject;
            hitComponent.other = collision.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            var hit = ecsWorld.NewEntity();

            var hitPool = ecsWorld.GetPool<HitComponent>();
            hitPool.Add(hit);
            ref var hitComponent = ref hitPool.Get(hit);

            hitComponent.first = transform.root.gameObject;
            hitComponent.other = other.gameObject;
        }
    }
}