using WarnerEngine.Lib.State;

namespace Warlocked.Entities.Units
{
    public abstract class ACharacterState<TCharacter, TCharacterStateTypes> : StateBase<ACharacterState<TCharacter, TCharacterStateTypes>, TCharacter, TCharacterStateTypes> where TCharacter : ACharacter<TCharacter, TCharacterStateTypes> { }
}
