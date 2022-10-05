
using UnityEngine;
namespace Player
{
    public class ForwardJumpingState : State
    {
        // constructor
        public ForwardJumpingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.anim.Play("arthur_jump_forward", 0, 0);
            if (player.jumpFlag == false)
            {
                player.yv = 5;
                
                if(player.left == true)
                {
                    player.xv = -5;
                }
                else
                {
                    player.xv = 5;
                }
                
            }
            else
            {
                Exit();
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

            if (player.jumpFlag == false && player.yv <= 0)
            {
                player.CheckForWalk();
            }
            player.CheckForLand();
            player.CheckForStand();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

    }
}


