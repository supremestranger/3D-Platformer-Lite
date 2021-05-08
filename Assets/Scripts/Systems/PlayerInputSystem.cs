using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Platformer
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerInputComponent>().End();
            var playerInputPool = ecsSystems.GetWorld().GetPool<PlayerInputComponent>();

            foreach (var i in filter)
            {
                ref var playerInputComponent = ref playerInputPool.Get(i);

                playerInputComponent.moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerInputComponent.jumpInput = true;
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
}