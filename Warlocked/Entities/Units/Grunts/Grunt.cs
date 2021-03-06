﻿using Microsoft.Xna.Framework;

using WarnerEngine.Lib.Components;
using WarnerEngine.Services;

namespace Warlocked.Entities.Units.Grunts
{
    public sealed class Grunt : ACharacter<Grunt, Grunt.GruntStateTypes>
    {
        public enum GruntStateTypes { Stopped, Moving, Gathering }

        // Own properties/methods
        private const int WIDTH = 16;
        private const int HEIGHT = 16;

        private GruntStateMachine stateMachine;
        public override ACharacterStateMachine<Grunt, GruntStateTypes> StateMachine { get => stateMachine; }
        public override int MoveSpeed => 60;

        private ResourceBundle? carriedResourceBundle;

        public Grunt(int AssignedTeam, Vector2 StartingPoint) : base(AssignedTeam)
        {
            backingBox = new BackingBox(BackingBox.IType.Free, StartingPoint.X, 0, StartingPoint.Y, WIDTH, 0, HEIGHT);
            stateMachine = new GruntStateMachine(new GruntStateStopped(), this);
        }

        public void ReceiveResourceBundle(ResourceBundle? Bundle)
        {
            carriedResourceBundle = Bundle;
        }

        // IUnit properties/methods
        public override int MaxHealth => 50;

        public override void Select() { }

        public override void UnSelect() { }

        // IDraw methods
        public override void Draw()
        {
            GameService.GetService<IRenderService>().DrawQuad(
                GameService.GetService<IContentService>().GetWhiteTileTexture(),
                SelectableArea,
                new Rectangle(0, 0, 8, 8),
                Color.Red
            );
        }
    }
}
