using Microsoft.Xna.Framework;

using WarnerEngine.Lib;
using WarnerEngine.Lib.Components;

namespace Warlocked.Entities.Units
{
    public abstract class ACharacter : IUnit
    {
        // Own properties/methods
        protected int health;
        protected int team;
        protected BackingBox backingBox;

        public ACharacter(int AssignedTeam)
        {
            health = MaxHealth;
            team = AssignedTeam;
        }

        public abstract void DirectToPoint(Vector2 Point);
        public abstract void DirectToUnit(IUnit Target);

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
        public abstract void PreDraw(float DT);

        // IDraw methods
        public abstract void Draw();
        public BackingBox GetBackingBox() => backingBox;
        public bool IsVisible() => true;
    }
}
