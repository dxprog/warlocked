namespace Warlocked.Entities.Units
{
    public abstract class ACharacterStateStopped<TCharacter, TCharacterStateTypes> : ACharacterState<TCharacter, TCharacterStateTypes> where TCharacter : ACharacter<TCharacter, TCharacterStateTypes>
    {
        public override ACharacterState<TCharacter, TCharacterStateTypes> Update(TCharacter Target, float DT)
        {
            if (Target.MoveQueue.Count > 0)
            {
                return GetMoveState();
            }
            return this;
        }

        public abstract ACharacterState<TCharacter, TCharacterStateTypes> GetMoveState();
    }
}
