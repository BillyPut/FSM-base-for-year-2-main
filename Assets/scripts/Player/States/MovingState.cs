
using UnityEngine;
namespace Player
{
    public class MovingState : State
    {


        // constructor
        public MovingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_run", 0, 0);
            player.xv = 5;
        }

        public override void Exit()
        {
            base.Exit();

            player.anim.SetBool("run", false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
