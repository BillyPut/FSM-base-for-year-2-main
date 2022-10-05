
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
            if (Input.GetKey("right"))
            {
                player.xv = 5;
            }
            if (Input.GetKey("left"))
            {
                player.xv = -5;
            }

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

            player.CheckForStand();
            player.CheckForLand();
            player.CheckForForwardJump();
            player.CheckForCrouch();
            player.CheckForStandingShoot();
        }

        public override void PhysicsUpdate()
        {
            if (Input.GetKey("right"))
            {
                player.xv = 5;
                player.sh.SetSpriteXDirection(Dir.Right);
                player.left = false;
            }
            if (Input.GetKey("left"))
            {
                player.xv = -5;
                player.sh.SetSpriteXDirection(Dir.Left);
                player.left = true;
            }
            base.PhysicsUpdate();
        }
    }
}
