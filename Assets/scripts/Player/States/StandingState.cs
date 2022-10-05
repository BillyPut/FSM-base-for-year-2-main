
using UnityEngine;
namespace Player
{
    public class StandingState : State
    {


        // constructor
        public StandingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_stand", 0, 0);
            player.xv = 0;
            
        }

        public override void Exit()
        {
            base.Exit();

            
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            player.CheckForWalk();
            player.CheckForLand();
            player.CheckForJump();
            player.CheckForCrouch();
            player.CheckForStandingShoot();

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}