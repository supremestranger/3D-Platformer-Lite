using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GroundCheckerView : MonoBehaviour
    {
        // auto-injected fields.
        public EcsPool<GroundedComponent> groundedPool;
        public int playerEntity;

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                if (!groundedPool.Has(playerEntity))
                {
                    groundedPool.Add(playerEntity);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                if (groundedPool.Has(playerEntity))
                {
                    groundedPool.Del(playerEntity);
                }
            }
        }
    }
}