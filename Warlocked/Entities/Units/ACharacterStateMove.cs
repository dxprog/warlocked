using System;

using Microsoft.Xna.Framework;

using WarnerEngine.Lib.Helpers;

namespace Warlocked.Entities.Units
{
    public abstract class ACharacterStateMove<TCharacter, TCharacterStateTypes> : ACharacterState<TCharacter, TCharacterStateTypes> where TCharacter : ACharacter<TCharacter, TCharacterStateTypes>
    {
        protected const int ARRIVAL_THRESHOLD = 10;

        public override ACharacterState<TCharacter, TCharacterStateTypes> Update(TCharacter Target, float DT)
        {
            switch (Target.MoveQueue.Peek())
            {
                case Vector2 currentMove:
                    Vector2 vectorToMove = currentMove - GraphicsHelper.FlattenVector3(Target.GetBackingBox().GetCenterPoint());
                    if (vectorToMove.Length() > ARRIVAL_THRESHOLD)
                    {
                        Vector2 unitVector = Vector2.Normalize(vectorToMove);
                        Target.GetBackingBox().Move(new Vector3(unitVector.X, 0, unitVector.Y) * Target.MoveSpeed * DT);
                    }
                    else
                    {
                        Target.MoveQueue.Dequeue();
                        return GetArrivalState(currentMove);
                    }
                    break;
                case IUnit targetUnit:
                    vectorToMove = GraphicsHelper.FlattenVector3(targetUnit.GetBackingBox().GetCenterPoint() - Target.GetBackingBox().GetCenterPoint());
                    if (vectorToMove.Length() > (Target.GetBackingBox().Width + targetUnit.GetBackingBox().Width) / 2 + ARRIVAL_THRESHOLD)
                    {
                        Vector2 unitVector = Vector2.Normalize(vectorToMove);
                        Target.GetBackingBox().Move(new Vector3(unitVector.X, 0, unitVector.Y) * Target.MoveSpeed * DT);
                    }
                    else
                    {
                        Target.MoveQueue.Dequeue();
                        return GetArrivalState(targetUnit);
                    }
                    break;
                default:
                    throw new Exception("Unsupported move target type");
            }
            return this;
        }

        public abstract ACharacterState<TCharacter, TCharacterStateTypes> GetArrivalState(object ArrivalTarget);
    }
}
