namespace Warlocked.Entities.Units.Grunts
{
    public class GruntStateMove : ACharacterStateMove<Grunt, Grunt.GruntStateTypes>
    {
        public override Grunt.GruntStateTypes GetStateType()
        {
            return Grunt.GruntStateTypes.Stopped;
        }

        public override void Enter(Grunt Target, ACharacterState<Grunt, Grunt.GruntStateTypes> PreviousState) { }

        public override void Exit(Grunt Target) { }

        public override ACharacterState<Grunt, Grunt.GruntStateTypes> GetArrivalState(object Target)
        {
            return new GruntStateStopped();
        }

        public override ACharacterState<Grunt, Grunt.GruntStateTypes> ConsiderStateChange(ACharacterState<Grunt, Grunt.GruntStateTypes> CandidateState, Grunt Target)
        {
            return this;
        }
    }
}
