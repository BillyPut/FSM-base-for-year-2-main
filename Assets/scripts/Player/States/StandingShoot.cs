
using UnityEngine;

namespace Player
{
    public class StandingShootState : State
    {
        public GameObject lance;

        // constructor
        public StandingShootState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.xv = 0;
            //Instantiate(lance, new Vector3(player.transform.position.x, player.transform.position.y, 0), Quaternion.identity);
          
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
            player.CheckForWalk();
            player.CheckForLand();
            player.CheckForJump();
            player.CheckForCrouch();

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}