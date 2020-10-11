using WarnerEngine.Lib.State;

namespace Warlocked.Entities.Units
{
    public abstract class ACharacterStateMachine<TCharacter, TCharacterStateTypes> : StateMachine<ACharacterState<TCharacter, TCharacterStateTypes>, TCharacter, TCharacterStateTypes> where TCharacter : ACharacter<TCharacter, TCharacterStateTypes> 
    {
        public ACharacterStateMachine(ACharacterState<TCharacter, TCharacterStateTypes> InitialState, TCharacter Target) : base(InitialState, Target) { }
    }
}
