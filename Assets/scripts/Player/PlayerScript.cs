using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;

namespace Player
{


    public class PlayerScript : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Animator anim;
        Collision col;
        public SpriteHelper sh;
        public LayerMask platformLayerMask;
        public LayerMask climbableLayerMask;
        
        bool onPlatform;
        public bool left = true;
        public bool jumpFlag, jumpButtonPressed, jumpButtonReleased;
        public bool shootButtonPressed, shootButtonReleased;
        public bool crouchButtonPressed, crouchButtonReleased;
        public bool upButtonPressed, downButtonPressed;

        public float fall = 0.2f;
        public float jumpGravity = 0.6f;
        public float initialJumpVel = 10f;
        public float xv, yv;
        

        Dir lastDir;
        Dir currentDir;

        public GameObject lanceWeapon;


        public float runSpeed = 6;

        

        LevelManager lm;


        // variables holding the different player states
        public StandingState standingState;
        public JumpingState jumpingState;
        public CrouchingState crouchingState;
        public MovingState movingState;
        public StandingShootState standingShootState;
        public ForwardJumpingState forwardJumpingState;
        public ClimbingState climbingState;

        public StateMachine sm;


        private void Awake()
        {
            sh = gameObject.AddComponent<SpriteHelper>();
        }




        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            col = gameObject.AddComponent<Collision>();
            lm = LevelManager.lm;
            sm = gameObject.AddComponent<StateMachine>();

            // add new states here
            standingState = new StandingState(this, sm);
            jumpingState = new JumpingState(this, sm);
            crouchingState = new CrouchingState(this, sm);
            movingState = new MovingState(this, sm);
            standingShootState = new StandingShootState(this, sm);
            forwardJumpingState = new ForwardJumpingState(this, sm);
            climbingState = new ClimbingState(this, sm);
            
            // initialise the statemachine with the default state
            sm.Init(standingState);

            
            sh.SetSpriteXDirection(Dir.Right);

            jumpFlag = true;

        }

        // Update is called once per frame
        public void Update()
        {
          
            sm.CurrentState.HandleInput();
            sm.CurrentState.LogicUpdate();

            //CheckForCrouch();
            //CheckForJump();
            //CheckForStand();
            //CheckForMove();
            //CheckForLand();

          

            if (onPlatform == false)
            {
                //yv = -2;
            }
            

            //output debug info to the canvas
            string s;
            s = string.Format("onplat={0} jumpFlag={1}\nlast state={2}\ncurrent state={3}", onPlatform, jumpFlag, sm.LastState, sm.CurrentState);
            UIscript.ui.DrawText(s);

            s = string.Format("current dir2={0} lastdir={1} yv={2}", currentDir, lastDir, yv);
            UIscript.ui.DrawText(s);

            s = string.Format("shoot button={0} ", shootButtonPressed);
            UIscript.ui.DrawText(s);

            // Press R to reset the player's position
            DebugPlayer();

        }



        void FixedUpdate()
        {
            
            // this is called for all states
            col.CheckTileCollisionPlatform(platformLayerMask, 0.38f, 0.4f, 0.11f);
            onPlatform = col.PlatformHit();
            col.ShowDebugCollisionPoints();

         

            

            sm.CurrentState.PhysicsUpdate();
            rb.velocity = new Vector2(xv, yv);
        }


        public void CheckForLand()
        {
            Vector2 pos = rb.position;


            // check for landing on a platform

            if ((yv <= 1) && (onPlatform == true))
            {
                yv = 0;
                xv = 0;
                rb.velocity = new Vector2(0, 0);

                jumpFlag = false;

                /* round to 0.5
                pos.y = Mathf.Round(pos.y);

                pos.y = (Mathf.Round(pos.y * 2)) / 2;

                print("Landed! y was=" + transform.position.y + "  and is now " + pos.y);*/

                
                //sm.ChangeState(standingState);

                rb.transform.position = pos;
            }
            else
            {
                jumpFlag = true;
                yv -= 6f * Time.deltaTime;
            }
            
         
            
        }



        public void CheckForStand()
        {

            if (!Input.GetKey("right") && !Input.GetKey("left") && crouchButtonPressed == false && onPlatform == true && yv < 0.1)
            {
                sm.ChangeState(standingState);


            }

            // check for changing direction
            if (currentDir != lastDir)
            {
                // player has changed direction
                sm.ChangeState(standingState);
            }
        }

        public void CheckForJump()
        {
            if (jumpFlag == false)
            {
                if (jumpButtonPressed == true)
                {
                    sm.ChangeState(jumpingState);
                }
            }
        }

        public void CheckForCrouch()
        {
            if (jumpFlag == false)
            {
                if (crouchButtonPressed == true)
                {
                    if (!Input.GetKey("right"))
                    {
                        if (!Input.GetKey("left"))
                        {
                            sm.ChangeState(crouchingState);
                        }
                    }
                  
                    
                }
            }

        }

        
        public void CheckForWalk()
        {
            if (Input.GetKey("right"))
            {
                sm.ChangeState(movingState);
            }
            if (Input.GetKey("left"))
            {
                
                sm.ChangeState(movingState);
            }
        }

        public void CheckForStandingShoot()
        {
            if (shootButtonPressed == true)
            {
                sm.ChangeState(standingShootState);
             
            }
        }

        public void CheckForForwardJump()
        {
            if (jumpFlag == false)
            {
                if (jumpButtonPressed == true)
                {
                    sm.ChangeState(forwardJumpingState);
                }
            }
        }

        public void CheckForClimb()
        {
            
        }

        public void ReadInputKeys()
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                crouchButtonPressed = true;
            }
            else
            {
                crouchButtonPressed = false;
            }

            if (Input.GetKey(KeyCode.LeftControl) && (shootButtonReleased == true))
            {
                shootButtonReleased = false;
                shootButtonPressed = true;
            }
            else
            {
                shootButtonPressed = false;
            }

            if (Input.GetKey(KeyCode.LeftControl) == false)
            {
                shootButtonReleased = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                jumpButtonPressed = true;
            }
            else
            {
                jumpButtonPressed = false;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                upButtonPressed = true;
            }
            else
            {
                upButtonPressed = false;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                downButtonPressed = true;
            }
            else
            {
                downButtonPressed = false;
            }

        }



        void DebugPlayer()
        {
            // reset player position with "R" key
            if (Input.GetKeyDown("r"))
            {
                gameObject.transform.position = new Vector2(-7, 4);
                rb.velocity = new Vector2(0, 0);
                xv = yv = 0;
            }

        }

    }

}