using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;        //Allows us to use SceneManager

namespace Overworld
{

    //Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
    public class Player : MovingObject
    {
        public AudioClip moveSound1;
        public AudioClip moveSound2;
        public bool CanMove = true;
        public bool CameraFollow = true;
        public int CameraFollowSpeed = 20;
        public Animator animator; //Used to store a reference to the Player's animator component.
        public SpriteRenderer spriteRenderer;

        public static Player instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                SaveGame(); // this happens on scene load.
            }
            else
            {
                Destroy(this);
            }
        }

        private void SaveGame()
        {
            if (!Application.isEditor)
            {
                GameManager.instance.SaveGame();
            }
        }

        //Start overrides the Start function of MovingObject
        protected override void Start()
        {
            CanMove = true;

            //Call the Start function of the MovingObject base class.
            base.Start();
        }


        //This function is called when the behaviour becomes disabled or inactive.
        private void OnDisable()
        {
        }

        protected override bool UpdateMove()
        {
            bool inMotion = base.UpdateMove();
            animator.SetBool("Walk", inMotion);
            return inMotion;
        }

        void Update()
        {
            if (!CanMove || UpdateMove())
            {
                return;
            }

            int horizontal = 0; //Used to store the horizontal move direction.
            int vertical = 0; //Used to store the vertical move direction.


            //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
            horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

            //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
            vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));

            //Check if we have a non-zero value for horizontal or vertical
            if (horizontal != 0 || vertical != 0)
            {
                if (horizontal < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (horizontal > 0)
                {
                    spriteRenderer.flipX = false;
                }

                AttemptMove(horizontal, vertical);
            }
        }

        void LateUpdate()
        {
            if (Camera.main != null && CameraFollow)
            {
                Vector3 dest = Vector3.MoveTowards(Camera.main.transform.position,
                    transform.position, Time.deltaTime * CameraFollowSpeed);
                Camera.main.transform.position = new Vector3(dest.x, dest.y, Camera.main.transform.position.z);
            }
        }

        //AttemptMove overrides the AttemptMove function in the base class MovingObject
        //AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
        protected override void AttemptMove(int xDir, int yDir)
        {
            //Hit allows us to reference the result of the Linecast done in Move.
            RaycastHit2D hit;

            //If Move returns true, meaning Player was able to move into an empty space.
            bool moved = Move(xDir, yDir, out hit);
            if (moved)
            {
                animator.SetBool("Walk", true);
                //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
                SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
            }
            else if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                NPC npc = hit.collider.gameObject.GetComponent<NPC>();
                if (npc.Activate(OnNPCActivateFinished))
                {
                    CanMove = false;
                }
            }
        }

        void OnNPCActivateFinished()
        {
            CanMove = true;
        }

        //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
        private void OnTriggerEnter2D(Collider2D other)
        {
            //Check if the tag of the trigger collided with is Exit.
            if (other.tag == "Exit")
            {
                other.gameObject.GetComponent<Exit>().Transition();
                CanMove = false; // todo make a transition fade.
            }
        }
    }
}