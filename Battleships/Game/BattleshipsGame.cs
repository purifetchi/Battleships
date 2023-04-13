﻿using System.Numerics;
using Battleships.Framework;
using Battleships.Framework.Assets;
using Battleships.Framework.Data;
using Battleships.Framework.Rendering;
using Battleships.Game.Objects;
using Raylib_cs;

namespace Battleships.Game
{
    /// <summary>
    /// The game logic for battleships.
    /// </summary>
    internal class BattleshipsGame : NetworkedGame
    {
        /// <summary>
        /// The camera.
        /// </summary>
        private Camera? _camera;

        /// <summary>
        /// The game coordinator.
        /// </summary>
        private GameCoordinator? _gameCoordinator;

        /// <summary>
        /// Construct a new battleship logic with the given launch options.
        /// </summary>
        /// <param name="opts">The launch options.</param>
        public BattleshipsGame(LaunchOptions opts)
            : base(new Vector2Int(800, 600), "Battleships", opts)
        {

        }

        /// <inheritdoc/>
        protected override void RegisterMessages()
        {
            
        }

        /// <inheritdoc/>
        protected override void Start()
        {
            AssetDatabase.Load<SoundAsset>("explosion", "./assets/explosion.wav");
            AssetDatabase.Load<ModelAsset>("ship_head", "./assets/ship_head.obj");
            AssetDatabase.Load<ModelAsset>("ship_body", "./assets/ship_body.obj");
            AssetDatabase.Load<ModelAsset>("ship_tail", "./assets/ship_tail.obj");

            _camera = AddGameObject<Camera>()
                .WithPosition(new Vector3(0, 10f, 10f))
                .WithFOV(10f)
                .WithProjectionType(CameraProjection.CAMERA_ORTHOGRAPHIC);

            _camera.Rotate(new Vector3(45, 0, 0));
            CurrentRenderer = _camera;

            _gameCoordinator = AddGameObject<GameCoordinator>();
            _gameCoordinator.SetPlayfieldForPlayer(0, AddGameObject<ShipPlayfield>());
        }

        /// <inheritdoc/>
        protected override void Update(float dt)
        {
            base.Update(dt);

            var dim = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            var positionWithinScreen = (Raylib.GetMousePosition() - dim / 2f) / dim;
            positionWithinScreen.Y = -positionWithinScreen.Y;

            if (MathF.Abs(positionWithinScreen.Y) >= 0.4f ||
                MathF.Abs(positionWithinScreen.X) >= 0.4f)
            {
                var newPos = new Vector3(positionWithinScreen.X, 0f, positionWithinScreen.Y) * 8f * dt;
                _camera!.Move(newPos);
            }
        }
    }
}
