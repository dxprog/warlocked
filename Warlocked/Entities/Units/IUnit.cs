using Microsoft.Xna.Framework;

using WarnerEngine.Lib.Entities;

namespace Warlocked.Entities.Units
{
    public enum UnitSubType { Character, Structure, Resource }

    public interface IUnit : ISceneEntity, IPreDraw, IDraw
    {
        int Team { get; }
        Rectangle SelectableArea { get; }
        int Health { get; }
        int MaxHealth { get; }

        void Select();
        void UnSelect();

        void DirectToPoint(Vector2 Point, bool IsAdditive = false);
        void DirectToUnit(IUnit Unit);

        UnitSubType GetUnitSubtype();
    }
}
