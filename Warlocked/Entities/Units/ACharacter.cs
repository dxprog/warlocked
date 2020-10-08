using System.Collections.Generic;

using Microsoft.Xna.Framework;

using WarnerEngine.Lib;
using WarnerEngine.Lib.Components;
using WarnerEngine.Lib.Helpers;

namespace Warlocked.Entities.Units
{
    public abstract class ACharacter : IUnit
    {
        protected const int ARRIVAL_THRESHOLD = 10;

        // Own properties/methods
        protected int health;
        protected int team;
        protected BackingBox backingBox;

        protected abstract int MoveSpeed { get; }

        // TODO: Make this more abstract so it can contain both points and units
        protected Queue<Vector2> MoveQueue;

        public ACharacter(int AssignedTeam)
        {
            health = MaxHealth;
            team = AssignedTeam;
            MoveQueue = new Queue<Vector2>();
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

        public virtual void DirectToUnit(IUnit Target) { }

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
            if (MoveQueue.Count > 0)
            {
                Vector2 currentMove = MoveQueue.Peek();
                Vector2 vectorToMove = currentMove - GraphicsHelper.FlattenVector3(backingBox.GetCenterPoint());
                if (vectorToMove.Length() > ARRIVAL_THRESHOLD) 
                {
                    Vector2 unitVector = Vector2.Normalize(vectorToMove);
                    backingBox.Move(new Vector3(unitVector.X, 0, unitVector.Y) * MoveSpeed * DT);
                }
                else
                {
                    MoveQueue.Dequeue();
                }
            }
        }

        // IDraw methods
        public abstract void Draw();
        public BackingBox GetBackingBox() => backingBox;
        public bool IsVisible() => true;
    }
}
