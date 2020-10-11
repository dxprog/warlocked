using WarnerEngine.Lib;

namespace Warlocked.Entities.Units.Grunts
{
    public class GruntStateGathering : ACharacterState<Grunt, Grunt.GruntStateTypes>
    {
        private const int CARRYING_CAPACITY = 10;

        private const int GATHER_DURATION = 2000;

        private AutoTween gatherTimer;

        private AResource resourceTarget;

        public GruntStateGathering(AResource ResourceTarget)
        {
            resourceTarget = ResourceTarget;
        }

        public override Grunt.GruntStateTypes GetStateType()
        {
            return Grunt.GruntStateTypes.Gathering;
        }

        public override void Enter(Grunt Target, ACharacterState<Grunt, Grunt.GruntStateTypes> PreviousState) 
        {
            gatherTimer = new AutoTween(0, 1, GATHER_DURATION).Start();
        }

        public override void Exit(Grunt Target) { }

        public override ACharacterState<Grunt, Grunt.GruntStateTypes> Update(Grunt Target, float DT)
        {
            if (!gatherTimer.Update().IsRunning)
            {
                Target.ReceiveResourceBundle(resourceTarget.TryExploit(CARRYING_CAPACITY));
                return new GruntStateStopped();
            }
            return this;
        }

        public override ACharacterState<Grunt, Grunt.GruntStateTypes> ConsiderStateChange(ACharacterState<Grunt, Grunt.GruntStateTypes> CandidateState, Grunt Target)
        {
            return this;
        }
    }
}
