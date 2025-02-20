using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.Rendering.DebugUI;

public class player1 : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] public float maxHP;
    [SerializeField] private LayerMask enemylayer;
    Animator animator;

    public float Health
    {
        get { return health; }
        set { health = Mathf.Clamp(value, 0f, maxHP); }

    }

    PlayerShieldBar shieldbar;
    healthbar healthbar;
    [SerializeField] GameObject laserray;
    [SerializeField] Transform pointer;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject missile;
    int bulletcooldown = 60;
    public float health;
    float rotation;
    float horizontal;
    float vertical;
    float currentrot;
    float shootcooldown;

    public float maxshield = 100;
    public float shield;
    float regentimeout = 0;
    [SerializeField] bool shieldsenabled;




    Vector2 lookdir = new Vector2(1, 0);

    movementbasestate _currentMovestate;
    Kbmove _kbmove;
    kbmousemove _kbmousemove;
    Action _switchstate;
    //skills
    private float BoostSkillCdDuration = 5.42f;
    private float BoostSkillActiveTimer = 0;
    private float boostSkillSpeedingStateDuration = 1;
    private float BoostSkillSlowingStateDUration = 3;
    private float boostSkillMultiplier = 10;
    private float BoostSkillCurrentSpeed = 0;
    private float BoostSkillCurrentCd = 0;
    private bool BoostSkillIsBoosting = false;
    private Action BoostSkillState;
    public Action<float> boostSkillCdNotifier;
    public player1.Boostskill boostskill;
    public player1.TeleportToMouseDelayed teleportToMouseDelayedSkill;

    private void Awake()
    {
        _switchstate = SwitchMouseKbControls;
        _kbmove = new Kbmove(this, _switchstate);
        _kbmousemove = new kbmousemove(this, _switchstate);
        _currentMovestate = _kbmove;


    }



    // Start is called before the first frame update
    void Start()
    {

        rigidbody2d = GetComponent<Rigidbody2D>();
        boostskill = new Boostskill(this);
        teleportToMouseDelayedSkill = new TeleportToMouseDelayed(this);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Rotation");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            newshoot();





        }
        if (Input.GetKeyDown(KeyCode.L)) {
            shootray();



        }

        if (Input.GetKeyDown(KeyCode.K)) {

            shootMissile();


            this.ToString();

        }





        if (shieldsenabled)
        {
            if (regentimeout <= 0)
            {
                if (shield < maxshield)
                {
                    shieldregen(Time.deltaTime);

                }



            }
            else if (regentimeout > 0) regentimeout -= Time.deltaTime;

        }




        _currentMovestate.OnUpdate();

        

    }
    private void FixedUpdate()
    {
        //// movement
        //Vector2 position = rigidbody2d.position;
        //position.x += Time.fixedDeltaTime * horizontal * 3;
        //position.y += Time.fixedDeltaTime * vertical * 3;
        //rigidbody2d.MovePosition(position);
        //currentrot = rigidbody2d.rotation;
        //rigidbody2d.MoveRotation(currentrot - rotation * Time.fixedDeltaTime * 100);

        _currentMovestate.OnFixedUpdate();
        if(BoostSkillIsBoosting) OnFixedUpdateBoostSkill();
        if (boostskill != null) boostskill.OnFixedUpdateBoostSkill();
        if(teleportToMouseDelayedSkill != null) teleportToMouseDelayedSkill.OnFixedUpdateBoostSkill();

    }
    public void SwitchMouseKbControls()
    {
        Debug.Log(_currentMovestate is Kbmove);
        if (_currentMovestate is kbmousemove) _currentMovestate = _kbmove;
        else if (_currentMovestate is Kbmove) _currentMovestate = _kbmousemove;

    }

    private void shoot(Vector3 direction, float force)
    {
        GameObject projectilebullet = Instantiate(bullet, rigidbody2d.position, Quaternion.identity);
        projectile projectilescript = projectilebullet.GetComponent<projectile>();
        projectilescript.lauch(direction, force);


    }
    public void newshoot()
    {
        GameObject projectilebullet = Instantiate(bullet, pointer.position, transform.rotation);
        projectile projectilescript = projectilebullet.GetComponent<projectile>();
        Vector3 direction = new Vector3();

        direction = (pointer.position - transform.position);
        direction.Normalize();
        // direction = new Vector3(direction.x-1,direction.y+1,direction.z);
        //Debug.Log(direction);
        projectilescript.lauch(direction, 1000);




    }
    public void shootray()

    {


        RaycastHit2D raycastHit2d = Physics2D.Raycast(pointer.position, pointer.position - transform.position, 10f, enemylayer);







        if (raycastHit2d.collider != null)
        {
            target hittarget = raycastHit2d.collider.GetComponent<target>();
            if (hittarget != null)
            {
                hittarget.takedamage(20);

                Debug.Log("target hit by ray");
                // Debug.DrawLine(transform.position, raycastHit2d.point);
            }
            GameObject laserrayinstance = Instantiate(laserray, pointer.position, transform.rotation);
            Laserray laserrayscript = laserrayinstance.GetComponent<Laserray>();
            Vector2 pointerpos = pointer.position;
            Vector2 distance = pointerpos - raycastHit2d.point;
            Debug.Log(distance.magnitude);
            laserrayscript.setsize(distance.magnitude);






        }
        else
        {

            /* Vector2 dir1 = pointer.localPosition;

             Vector2 dir = transform.TransformPoint(pointer.localPosition*20);
             Debug.DrawLine(transform.position,dir );
             Debug.Log(pointer.position - transform.position);
             */
            GameObject laserrayinstance = Instantiate(laserray, pointer.position, transform.rotation);
            Laserray laserrayscript = laserrayinstance.GetComponent<Laserray>();



        }


    }
    public void shootMissile()
    {
        GameObject projectilebullet = Instantiate(missile, pointer.position, transform.rotation);





    }








    public void takedamage(float damage)
    {
        if (damage > 0) regentimeout = 3;


        if (shield > 0 && shieldsenabled)
        {
            shield -= damage;

            // Debug.Log(shield);
            //Debug.Log(health);
            shieldbar = gameObject.GetComponentInChildren<PlayerShieldBar>();
            shieldbar.changehealth(-damage);
            if (shield <= 0)
            {
                shield = 0;

            }
        }





        else
        {

            health -= damage;
            Debug.Log(Health);

            healthbar = gameObject.GetComponentInChildren<healthbar>();
            healthbar.changehealthnew(-damage);
            if (health <= 0)
            {
                /*if (dropboxgameobject != null)
                {
                    Instantiate(dropboxgameobject, transform.position, quaternion.identity).GetComponent<dropbox>().containedloot = "LaserTurret";

                }*/



                Destroy(gameObject);


            }



        }

    }

    public void shieldregen(float regen)
    {




        shield += regen;

        //Debug.Log(shield);
        shieldbar = gameObject.GetComponentInChildren<PlayerShieldBar>();
        shieldbar.changehealth(regen);
        if (shield >= 100)
        {
            shield = maxshield;

        }


    }
    public void BoostSkill() {
        if (!BoostSkillIsBoosting)
        {
            BoostSkillState = BoostSkillStateSpeeding;
            BoostSkillIsBoosting = true;
            BoostSkillCurrentSpeed = 0;
            BoostSkillCurrentCd = BoostSkillCdDuration;
        }
    }
    private void OnUpdateBoostSkill() {
    
    }
    private void OnFixedUpdateBoostSkill()
    {
        BoostSkillState();
        //notify about cd
        boostSkillCdNotifier(BoostSkillCurrentCd);
        Debug.Log("notify");
    }
    private void BoostSkillStateSpeeding() 
    {
        BoostSkillActiveTimer += Time.fixedDeltaTime;
        if (BoostSkillActiveTimer > boostSkillSpeedingStateDuration)
        {
            BoostSkillState = BoostSkillStateSlowing;
            BoostSkillStateSlowing();

        }
        else {
            BoostSkillCurrentSpeed += 0.5f * boostSkillMultiplier;
            //Vector3 movementVector = new Vector3(0, 1,0);
            //transform.localToWorldMatrix.MultiplyVector(movementVector);
            //Vector2 movementVector2 = new Vector2(movementVector.x, movementVector.y);
            Vector2 movementVector2 = (pointer.position - transform.position);
            movementVector2.Normalize();
            movementVector2 = movementVector2 * BoostSkillCurrentSpeed * 0.05f;
            rigidbody2d.position += movementVector2*Time.fixedDeltaTime;
        
        }

    }
    private void BoostSkillStateSlowing() 
    {
        BoostSkillActiveTimer += Time.fixedDeltaTime;
        if (BoostSkillActiveTimer > boostSkillSpeedingStateDuration + BoostSkillSlowingStateDUration)
        {
            BoostSkillCurrentSpeed = 0;
            BoostSkillState = BoostSkillStateCooldown;
            BoostSkillStateCooldown();
        }
        else 
        {
            BoostSkillCurrentSpeed -= 0.1f * boostSkillMultiplier;
            //Vector3 movementVector = new Vector3(0, 1, 0);
            //transform.localToWorldMatrix.MultiplyVector(movementVector);
            //Vector2 movementVector2 = new Vector2(movementVector.x, movementVector.y);
            Vector2 movementVector2 = (pointer.position - transform.position);
            movementVector2.Normalize();
            movementVector2 = movementVector2 * BoostSkillCurrentSpeed * 0.05f;
            rigidbody2d.position += movementVector2 * Time.fixedDeltaTime;

        }
    }
    private void BoostSkillStateCooldown()
    {
        
        BoostSkillActiveTimer += Time.fixedDeltaTime;
        if (BoostSkillActiveTimer > boostSkillSpeedingStateDuration + BoostSkillSlowingStateDUration + BoostSkillCdDuration)
        {
            BoostSkillActiveTimer = 0;;
            BoostSkillIsBoosting = false;

        }
        else
        {
            BoostSkillCurrentCd = boostSkillSpeedingStateDuration + BoostSkillSlowingStateDUration + BoostSkillCdDuration - BoostSkillActiveTimer;
            Debug.Log(BoostSkillActiveTimer);

        }
    }
    public class Boostskill : SkillBarManager.ISkill
    {
        private float BoostSkillCdDuration = 5.42f;
        private float BoostSkillActiveTimer = 0;
        private float boostSkillSpeedingStateDuration = 0.3f;
        private float BoostSkillSlowingStateDUration = 0.6f;
        private float boostSkillMultiplier = 30;
        private float BoostSkillCurrentSpeed = 0;
        private float BoostSkillCurrentCd = 0;
        private bool BoostSkillIsBoosting = false;
        private Action BoostSkillState;
        private player1 PlayerInstance;
       
        public Boostskill(player1 player)
        {
           PlayerInstance= player;

        }
        
        public void ActivateSkill()
        {
            BoostSkill();

        }
        private void BoostSkill()
        {
            if (!BoostSkillIsBoosting)
            {
                BoostSkillState = BoostSkillStateSpeeding;
                BoostSkillIsBoosting = true;
                BoostSkillCurrentSpeed = 0;
                BoostSkillCurrentCd = BoostSkillCdDuration;
            }
        }
        public Action<float> CooldownNotifier { get; set; }
        public void OnFixedUpdateBoostSkill()
        {
            if(BoostSkillState !=null && BoostSkillIsBoosting == true) BoostSkillState(); 
            //notify about cd
            if(CooldownNotifier != null && BoostSkillIsBoosting == true) CooldownNotifier(BoostSkillCurrentCd);
            
        }
        private void BoostSkillStateSpeeding()
        {
            BoostSkillActiveTimer += Time.fixedDeltaTime;
            if (BoostSkillActiveTimer > boostSkillSpeedingStateDuration)
            {
                BoostSkillState = BoostSkillStateSlowing;
                BoostSkillStateSlowing();

            }
            else
            {
                BoostSkillCurrentSpeed += 0.5f * boostSkillMultiplier;
                //Vector3 movementVector = new Vector3(0, 1,0);
                //transform.localToWorldMatrix.MultiplyVector(movementVector);
                //Vector2 movementVector2 = new Vector2(movementVector.x, movementVector.y);
                Vector2 movementVector2 = (PlayerInstance.pointer.position - PlayerInstance.transform.position);
                movementVector2.Normalize();
                movementVector2 = movementVector2 * BoostSkillCurrentSpeed * 0.05f;
                PlayerInstance.rigidbody2d.position += movementVector2 * Time.fixedDeltaTime;

            }

        }
        private void BoostSkillStateSlowing()
        {
            BoostSkillActiveTimer += Time.fixedDeltaTime;
            if (BoostSkillActiveTimer > boostSkillSpeedingStateDuration + BoostSkillSlowingStateDUration)
            {
                BoostSkillCurrentSpeed = 0;
                BoostSkillState = BoostSkillStateCooldown;
                BoostSkillStateCooldown();
            }
            else
            {
                BoostSkillCurrentSpeed -= 0.1f * boostSkillMultiplier;
                //Vector3 movementVector = new Vector3(0, 1, 0);
                //transform.localToWorldMatrix.MultiplyVector(movementVector);
                //Vector2 movementVector2 = new Vector2(movementVector.x, movementVector.y);
                Vector2 movementVector2 = (PlayerInstance.pointer.position - PlayerInstance.transform.position);
                movementVector2.Normalize();
                movementVector2 = movementVector2 * BoostSkillCurrentSpeed * 0.05f;
                PlayerInstance.rigidbody2d.position += movementVector2 * Time.fixedDeltaTime;

            }
        }
        private void BoostSkillStateCooldown()
        {

            BoostSkillActiveTimer += Time.fixedDeltaTime;
            if (BoostSkillActiveTimer > boostSkillSpeedingStateDuration + BoostSkillSlowingStateDUration + BoostSkillCdDuration)
            {
                BoostSkillActiveTimer = 0;
                BoostSkillCurrentCd = 0;
                //notify coolldown is 0
                if (CooldownNotifier != null) CooldownNotifier(BoostSkillCurrentCd);
                BoostSkillIsBoosting = false;

            }
            else
            {
                BoostSkillCurrentCd = boostSkillSpeedingStateDuration + BoostSkillSlowingStateDUration + BoostSkillCdDuration - BoostSkillActiveTimer;
               // Debug.Log(BoostSkillActiveTimer);

            }
        }


    }
    public class TeleportToMouseDelayed : SkillBarManager.ISkill
    {
        private float SkillCdDuration = 30f;
        private float SkillActiveTimer = 0;
        private float boostSkillSpeedingStateDuration = 0.3f;
        private float BoostSkillSlowingStateDUration = 0.6f;
        private float boostSkillMultiplier = 5;
        private float BoostSkillCurrentSpeed = 0;
        private float SkillCurrentCd = 0;
        private bool SkillActive = false;
        private Action BoostSkillState;
        private player1 PlayerInstance;

        public TeleportToMouseDelayed(player1 player)
        {
            PlayerInstance = player;

        }

        public void ActivateSkill()
        {
            BoostSkill();

        }
        private void BoostSkill()
        {
            if (!SkillActive)
            {
                BoostSkillState = BoostSkillStateSpeeding;
                SkillActive = true;
                BoostSkillCurrentSpeed = 0;
                SkillCurrentCd = SkillCdDuration;
            }
        }
        public Action<float> CooldownNotifier { get; set; }
        public void OnFixedUpdateBoostSkill()
        {
            if (BoostSkillState != null && SkillActive == true) BoostSkillState();
            //notify about cd
            if (CooldownNotifier != null && SkillActive == true) CooldownNotifier(SkillCurrentCd);

        }
        private void BoostSkillStateSpeeding()
        {
            SkillActiveTimer += Time.fixedDeltaTime;
            if (SkillActiveTimer > boostSkillSpeedingStateDuration)
            {
                DoJump();
                BoostSkillState = BoostSkillStateSlowing;
                BoostSkillStateSlowing();

            }
            else
            {
                BoostSkillCurrentSpeed += 0.5f * boostSkillMultiplier;
                //Vector3 movementVector = new Vector3(0, 1,0);
                //transform.localToWorldMatrix.MultiplyVector(movementVector);
                //Vector2 movementVector2 = new Vector2(movementVector.x, movementVector.y);
                Vector2 movementVector2 = (PlayerInstance.pointer.position - PlayerInstance.transform.position);
                movementVector2.Normalize();
                movementVector2 = movementVector2 * BoostSkillCurrentSpeed * 0.05f;
                PlayerInstance.rigidbody2d.position += movementVector2 * Time.fixedDeltaTime;

            }

        }
        private void DoJump()
        {
           Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            PlayerInstance.rigidbody2d.position= mousePosition;
            

        }
        private void BoostSkillStateSlowing()
        {
            SkillActiveTimer += Time.fixedDeltaTime;
            if (SkillActiveTimer > boostSkillSpeedingStateDuration + BoostSkillSlowingStateDUration)
            {
                BoostSkillCurrentSpeed = 0;
                BoostSkillState = BoostSkillStateCooldown;
                BoostSkillStateCooldown();
            }
            else
            {
                BoostSkillCurrentSpeed -= 0.1f * boostSkillMultiplier;
                //Vector3 movementVector = new Vector3(0, 1, 0);
                //transform.localToWorldMatrix.MultiplyVector(movementVector);
                //Vector2 movementVector2 = new Vector2(movementVector.x, movementVector.y);
                Vector2 movementVector2 = (PlayerInstance.pointer.position - PlayerInstance.transform.position);
                movementVector2.Normalize();
                movementVector2 = movementVector2 * BoostSkillCurrentSpeed * 0.05f;
                PlayerInstance.rigidbody2d.position += movementVector2 * Time.fixedDeltaTime;

            }
        }
        private void BoostSkillStateCooldown()
        {

            SkillActiveTimer += Time.fixedDeltaTime;
            if (SkillActiveTimer > boostSkillSpeedingStateDuration + BoostSkillSlowingStateDUration + SkillCdDuration)
            {
                SkillActiveTimer = 0;
                SkillCurrentCd = 0;
                //notify coolldown is 0
                if (CooldownNotifier != null) CooldownNotifier(SkillCurrentCd);
                SkillActive = false;

            }
            else
            {
                SkillCurrentCd = boostSkillSpeedingStateDuration + BoostSkillSlowingStateDUration + SkillCdDuration - SkillActiveTimer;
                // Debug.Log(BoostSkillActiveTimer);

            }
        }
    }
}


public class movementbasestate
{
 protected float horizontal;
 protected    float   vertical;
 protected    float   rotation;
    protected player1 _player;
    protected Rigidbody2D rigidbody2d;
    protected Action _switchstate;
    protected float currentrot;


    public virtual void OnUpdate() {
        
        
    }

    public virtual void OnFixedUpdate()  {  }
    public virtual void Switchstate()
    {
        if (Input.GetKeyDown(KeyCode.Y)) { _switchstate(); }
    }

  public  movementbasestate(player1 player,Action switchstate) 
    { 
    
    _player= player;
     rigidbody2d =   _player.GetComponent<Rigidbody2D>();
        _switchstate = switchstate;
    }
    protected float GetAngleFromVectorcorectedforsprite(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        n -= 90f;
        if (n < 0) n += 360;
        return n;



    }

}

public class Kbmove : movementbasestate
   
{
    public Kbmove(player1 player, Action switchstate) : base(player,switchstate)
    {


    }

    public override void OnFixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x += Time.fixedDeltaTime * horizontal * 3;
        position.y += Time.fixedDeltaTime * vertical * 3;
        rigidbody2d.MovePosition(position);
        currentrot = rigidbody2d.rotation;
        rigidbody2d.MoveRotation(currentrot - rotation * Time.fixedDeltaTime * 100);
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Y))  Switchstate();
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            rotation = Input.GetAxis("Rotation");

        
    }

   
}

public class kbmousemove : movementbasestate
{
    Vector3 mousePoz;
    public kbmousemove(player1 player, Action switchstate) : base(player, switchstate)
    {
        

    }

    public override void OnFixedUpdate()
    {
        mousePoz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoz.z = 0;
        Vector2 position = rigidbody2d.position;
        position.x += Time.fixedDeltaTime * horizontal * 3;
        position.y += Time.fixedDeltaTime * vertical * 3;
        rigidbody2d.MovePosition(position);
        currentrot = rigidbody2d.rotation;
        rigidbody2d.SetRotation(GetAngleFromVectorcorectedforsprite(mousePoz - _player.transform.position));
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Y))  Switchstate();
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        
    }

   
}