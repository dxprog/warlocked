using Microsoft.Xna.Framework;
using WarnerEngine.Lib;
using WarnerEngine.Lib.Components;

namespace Warlocked.Entities.Units
{
    public abstract class AResource : IUnit
    {
        // Own properties/methods
        protected int remainingResources;
        protected BackingBox backingBox;

        public AResource()
        {
            remainingResources = MaxHealth;
        }

        public abstract ResourceBundle.Resource ResourceType { get; }
        public virtual ResourceBundle? TryExploit(int RequestedAmount)
        {
            switch (remainingResources)
            {
                case var r when r >= RequestedAmount:
                    remainingResources -= RequestedAmount;
                    return new ResourceBundle()
                    {
                        ResourceType = ResourceType,
                        Amount = RequestedAmount,
                    };
                case var r when r > 0:
                    remainingResources = 0;
                    return new ResourceBundle()
                    {
                        ResourceType = ResourceType,
                        Amount = r,
                    };
                default:
                    return null;
            }
        }

        // IUnit properties/methods
        public int Team => -1;
        public Rectangle SelectableArea => backingBox.GetBoundingRectangle();
        public int Health => remainingResources;
        public abstract int MaxHealth { get; }

        public void Select() { }
        public void UnSelect() { }
        public void DirectToPoint(Vector2 Point, bool IsAdditive = false) { }
        public void DirectToUnit(IUnit Target) { }
        public UnitSubType GetUnitSubtype()
        {
            return UnitSubType.Resource;
        }

        // ISceneEntity methods
        public void OnAdd(Scene ParentScene) { }
        public void OnRemove(Scene ParentScene) { }

        // IPreDraw methods
        public virtual void PreDraw(float DT) { }

        // IDraw methods
        public abstract void Draw();
        public BackingBox GetBackingBox() => backingBox;
        public bool IsVisible() => true;
    }
}
