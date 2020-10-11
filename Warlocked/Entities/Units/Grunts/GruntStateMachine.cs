namespace Warlocked.Entities.Units.Grunts
{
    public class GruntStateMachine : ACharacterStateMachine<Grunt, Grunt.GruntStateTypes>
    {
        public GruntStateMachine(ACharacterState<Grunt, Grunt.GruntStateTypes> InitialState, Grunt Target) : base(InitialState, Target) { }
    }
}
