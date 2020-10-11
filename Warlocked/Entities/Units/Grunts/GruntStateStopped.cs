namespace Warlocked.Entities.Units.Grunts
{
    public class GruntStateStopped : ACharacterStateStopped<Grunt, Grunt.GruntStateTypes>
    {
        public override Grunt.GruntStateTypes GetStateType()
        {
            return Grunt.GruntStateTypes.Stopped;
        }
        public override void Enter(Grunt Target, ACharacterState<Grunt, Grunt.GruntStateTypes> PreviousState) { }

        public override void Exit(Grunt Target) { }

        public override ACharacterState<Grunt, Grunt.GruntStateTypes> GetMoveState()
        {
            return new GruntStateMove();
        }        

        public override ACharacterState<Grunt, Grunt.GruntStateTypes> ConsiderStateChange(ACharacterState<Grunt, Grunt.GruntStateTypes> CandidateState, Grunt Target)
        {
            return this;
        }
    }
}
