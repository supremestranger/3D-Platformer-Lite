using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var gameData = ecsSystems.GetShared<GameData>();

            var playerEntity = ecsWorld.NewEntity();

            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            playerPool.Add(playerEntity);
            ref var playerComponent = ref playerPool.Get(playerEntity);
            var playerInputPool = ecsWorld.GetPool<PlayerInputComponent>();
            playerInputPool.Add(playerEntity);
            ref var playerInputComponent = ref playerInputPool.Get(playerEntity);

            var playerGO = GameObject.FindGameObjectWithTag("Player");
            playerGO.GetComponentInChildren<GroundCheckerView>().groundedPool = ecsSystems.GetWorld().GetPool<GroundedComponent>();
            playerGO.GetComponentInChildren<GroundCheckerView>().playerEntity = playerEntity;
            playerGO.GetComponentInChildren<CollisionCheckerView>().ecsWorld = ecsWorld;
            playerComponent.playerSpeed = gameData.configuration.playerSpeed;
            playerComponent.playerTransform = playerGO.transform;
            playerComponent.playerJumpForce = gameData.configuration.playerJumpForce;
            playerComponent.playerCollider = playerGO.GetComponent<CapsuleCollider>();
            playerComponent.playerRB = playerGO.GetComponent<Rigidbody>();
        }
    }
}