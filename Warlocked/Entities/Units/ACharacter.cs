using System.Collections.Generic;

using Microsoft.Xna.Framework;

using WarnerEngine.Lib;
using WarnerEngine.Lib.Components;

namespace Warlocked.Entities.Units
{
    public abstract class ACharacter<TCharacter, TCharacterStateTypes> : IUnit where TCharacter : ACharacter<TCharacter, TCharacterStateTypes>
    {
        // Own properties/methods
        protected int health;
        protected int team;
        protected BackingBox backingBox;

        public abstract ACharacterStateMachine<TCharacter, TCharacterStateTypes> StateMachine { get; }

        public abstract int MoveSpeed { get; }

        public Queue<object> MoveQueue { get; private set; }

        public ACharacter(int AssignedTeam)
        {
            health = MaxHealth;
            team = AssignedTeam;
            MoveQueue = new Queue<object>();
        }

        public virtual void DirectToPoint(Vector2 Point, bool IsAdditive = false)
        {
            if (IsAdditive)
            {
                MoveQueue.Enqueue(Point);
            } 
            else
            {
                MoveQueue.Clear();
                MoveQueue.Enqueue(Point);
            }
        }

        public virtual void DirectToUnit(IUnit Target) 
        {
            MoveQueue.Clear();
            MoveQueue.Enqueue(Target);
        }

        protected abstract void OnArrivalAtPoint(Vector2 Point);
        protected abstract void OnArrivalAtUnit(IUnit Unit);

        // IUnit properties/methods
        public int Team => team;
        public Rectangle SelectableArea => backingBox.GetBoundingRectangle();
        public int Health => health;
        public abstract int MaxHealth { get; }

        public abstract void Select();
        public abstract void UnSelect();
        public UnitSubType GetUnitSubtype()
        {
            return UnitSubType.Character;
        }

        // ISceneEntity methods
        public void OnAdd(Scene ParentScene) { }
        public void OnRemove(Scene ParentScene) { }

        // IPreDraw methods
        public virtual void PreDraw(float DT)
        {
            StateMachine.Update(this as TCharacter, DT);
        }

        // IDraw methods
        public abstract void Draw();
        public BackingBox GetBackingBox() => backingBox;
        public bool IsVisible() => true;
    }
}
