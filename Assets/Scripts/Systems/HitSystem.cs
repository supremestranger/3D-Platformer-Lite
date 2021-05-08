using Leopotam.EcsLite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class HitSystem : IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameData>();
            var hitFilter = ecsSystems.GetWorld().Filter<HitComponent>().End();
            var hitPool = ecsSystems.GetWorld().GetPool<HitComponent>();
            var playerFilter = ecsSystems.GetWorld().Filter<PlayerComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();

            foreach (var i in hitFilter)
            {
                ref var hitComponent = ref hitPool.Get(i);

                foreach(var j in playerFilter)
                {
                    ref var playerComponent = ref playerPool.Get(j);

                    if (hitComponent.other.CompareTag("Coin"))
                    {
                        hitComponent.other.gameObject.SetActive(false);
                        playerComponent.coins += 1;
                        gameData.coinCounter.text = playerComponent.coins.ToString();
                    }

                    if (hitComponent.other.CompareTag("BadCoin"))
                    {
                        hitComponent.other.gameObject.SetActive(false);
                        playerComponent.coins -= 1;
                        gameData.coinCounter.text = playerComponent.coins.ToString();
                    }

                    if (hitComponent.other.CompareTag("Dangerous"))
                    {
                        playerComponent.playerTransform.gameObject.SetActive(false);
                        ecsSystems.GetWorld().DelEntity(j);
                        gameData.gameOverPanel.SetActive(true);
                    }

                    if (hitComponent.other.CompareTag("WinPoint"))
                    {
                        playerComponent.playerTransform.gameObject.SetActive(false);
                        ecsSystems.GetWorld().DelEntity(j);
                        gameData.playerWonPanel.SetActive(true);
                    }

                    if (hitComponent.other.CompareTag("JumpBuff"))
                    {
                        hitComponent.other.gameObject.SetActive(false);
                        playerComponent.playerJumpForce *= 2f;
                        var jumpPool = ecsSystems.GetWorld().GetPool<JumpBuffComponent>();
                        jumpPool.Add(j);
                        ref var jumpBuffComponent = ref jumpPool.Get(j);
                        jumpBuffComponent.timer = gameData.configuration.jumpBuffDuration;
                    }

                    if (hitComponent.other.CompareTag("SpeedBuff"))
                    {
                        hitComponent.other.gameObject.SetActive(false);
                        playerComponent.playerSpeed *= 2f;
                        var speedPool = ecsSystems.GetWorld().GetPool<SpeedBuffComponent>();
                        speedPool.Add(j);
                        ref var speedBuffComponent = ref speedPool.Get(j);
                        speedBuffComponent.timer = gameData.configuration.speedBuffDuration;
                    }
                }
                
            }
        }
    }
}