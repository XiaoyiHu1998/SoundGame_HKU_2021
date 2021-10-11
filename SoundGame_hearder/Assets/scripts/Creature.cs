using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public Player player;
    public Cage cage;
    public float minMoveTime;
    public float maxMoveTime;
    public float minSoundTime;
    public float maxSoundTime;
    public float minCageDistance;
    public float maxCageDistance;
    public float runFromPlayerDistance;

    public float clapDistance;
    public float clapTime;
    public float lureDistance;
    public float lureTime;

    public float moveSpeed;
    public float runSpeed;

    public float divineEffectCooldown;

    private bool doneMoving;
    private bool nearPlayer;
    private bool cageTooClose;
    private bool cageTooFar;


    private Stopwatch moveTimer;
    private System.Random random;
    private Vector3 moveDirection;
    private float moveTime;

    private bool divineEffect;
    private float divineEffectTime;
    private Stopwatch divineEffectTimer;
    private Vector3 divineEffectDirection;

    // Start is called before the first frame update
    void Start()
    {
        moveTimer = new Stopwatch();
        moveTimer.Start();
        divineEffectTimer = new Stopwatch();
        random = new System.Random();
        doneMoving = true;
        nearPlayer = false;
        cageTooClose = false;
        cageTooFar = false;
        divineEffect = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkState();
        setMoveDirection();
        Move();
    }

    private void checkState()
    {
        nearPlayer = Vector3.Distance(player.GetLocation(), gameObject.transform.position) <= runFromPlayerDistance;
        cageTooFar = Vector3.Distance(cage.GetLocation(), gameObject.transform.position) >= maxCageDistance;
        cageTooClose = Vector3.Distance(cage.GetLocation(), gameObject.transform.position) <= minCageDistance;
        doneMoving = moveTimer.ElapsedMilliseconds / 1000 >= moveTime;

        if (divineEffect)
        {
            divineEffect = divineEffectTime <= divineEffectTimer.ElapsedMilliseconds / 1000;
            if (!divineEffect)
            {
                divineEffectTimer.Restart();
                moveTimer.Start();
            }
        }
        UnityEngine.Debug.LogError("nearPlayer:" + nearPlayer.ToString() + "\ncageTooFar:" + cageTooFar.ToString() + 
                                   "\ncageTooClose:" + cageTooClose.ToString() + "\ndoneMoving:" + doneMoving.ToString() +
                                   "\ndivineEffect:" + divineEffect.ToString());
    }

    private void setMoveDirection()
    {
        Vector3 creatureToPlayer = (gameObject.transform.position - player.GetLocation()).normalized;
        Vector3 creatureToCage = (gameObject.transform.position - cage.GetLocation()).normalized;
        creatureToPlayer.y = 0;
        creatureToCage.y = 0;

        if (divineEffect)
        {
            return;
        }
        else if (nearPlayer)
        {
            moveDirection = creatureToPlayer;
        }
        else if (cageTooClose)
        {
            moveDirection = creatureToCage;
            moveDirection.Normalize();
        }
        else if (cageTooFar)
        {
            moveDirection = -1 * creatureToCage;
            moveDirection.Normalize();
        }
        else if (doneMoving)
        {
            doneMoving = false;
            bool negativeX = random.Next(0, 2) <= 1;
            bool negativeZ = random.Next(0, 2) <= 1;

            float moveX = (float)random.NextDouble();
            float moveZ = (float)random.NextDouble();

            if (negativeX)
                moveX *= -1f;
            if (negativeZ)
                moveZ *= -1f;

            Vector3 moveDirectionNew = new Vector3(moveX, 0, moveZ).normalized;
            if (moveDirectionNew == moveDirection)
                UnityEngine.Debug.LogError("same random direction");

            moveTime = (float)(minMoveTime + random.NextDouble() * (maxMoveTime - minMoveTime));
            moveTimer.Restart();
        }
    }

    private void Move()
    {
        if (divineEffect)
        {
            gameObject.transform.position += divineEffectDirection * runSpeed;
        }
        else
        {
            float movementSpeed;
            if (nearPlayer)
            {
                movementSpeed = runSpeed;
            }
            else
            {
                movementSpeed = moveSpeed;
            }
            gameObject.transform.position += moveDirection * movementSpeed;
        }
    }

    public void Clap()
    {
        float distance = Vector3.Distance(player.GetLocation(), gameObject.transform.position);
        bool inCooldown = !divineEffect && divineEffectTimer.ElapsedMilliseconds / 1000 <= divineEffectCooldown;
        if(divineEffect || inCooldown || distance >= clapDistance)
            return;
           
        divineEffect = true;
        moveTimer.Stop();
        divineEffectTimer.Restart();
        divineEffectTime = clapTime;
        divineEffectDirection = (player.GetLocation() - gameObject.transform.position).normalized;
    }

    public void Lure()
    {
        float distance = Vector3.Distance(player.GetLocation(), gameObject.transform.position);
        bool inCooldown = !divineEffect && divineEffectTimer.ElapsedMilliseconds / 1000 <= divineEffectCooldown;
        if(divineEffect || inCooldown || distance >= lureDistance)
            return;
           
        divineEffect = true;
        moveTimer.Stop();
        divineEffectTimer.Restart();
        divineEffectTime = lureTime;
        divineEffectDirection = (gameObject.transform.position - player.GetLocation()).normalized;
    }
}
