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
            var tryJumpPool = ecsSystems.GetWorld().GetPool<TryJump>();
            var gameData = ecsSystems.GetShared<GameData>();

            foreach (var entity in filter)
            {
                ref var playerInputComponent = ref playerInputPool.Get(entity);

                playerInputComponent.moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    var tryJump = ecsSystems.GetWorld().NewEntity();
                    tryJumpPool.Add(tryJump);
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    gameData.sceneService.ReloadScene();
                }
            }
        }
    }
}