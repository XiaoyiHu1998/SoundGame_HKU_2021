using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public Player player;
    public Center center;
    public Cage cage;
    public ScoreManager scoreManager;
    public SoundManager soundManager;
    public AudioSource audioSource;
    public AudioSource audioSourcePickupDrop;
    public int seed;
    public float minMoveTime;
    public float maxMoveTime;
    public float minSoundTime;
    public float maxSoundTime;
    
    public float clapDistance;
    public float clapTime;
    public float lureDistance;
    public float lureTime;
    public float respawnTime;

    public float moveSpeed;
    public float runSpeed;
    public bool pickedUp;

    public float divineEffectCooldown;
    private float minCageDistance;
    private float maxCenterDistance;
    private float runFromPlayerDistance;

    private bool doneMoving;
    private bool nearPlayer;
    private bool cageTooClose;
    private bool centerTooFar;

    private Stopwatch respawnTimer;
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
        respawnTimer = new Stopwatch();
        respawnTimer.Stop();
        respawnTimer.Reset();
        moveTimer = new Stopwatch();
        moveTimer.Start();
        divineEffectTimer = new Stopwatch();
        random = new System.Random(seed);
        doneMoving = true;
        nearPlayer = false;
        cageTooClose = false;
        centerTooFar = false;
        divineEffect = false;

        maxCenterDistance = center.GetMaxCageDistance();
        minCageDistance = cage.GetMinCageDistance();
        runFromPlayerDistance = player.GetCreatureRunDistance();
    }
    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (pickedUp)
            return;

        checkRespawn();
        checkState();
        setMoveDirection();
        Move();
    }

    private void checkRespawn()
    {
        if(Vector3.Distance(cage.GetLocation(), gameObject.transform.position) <= cage.GetRespawnDistance() && !respawnTimer.IsRunning)
        {
            scoreManager.incrementScore(1);
            respawnTimer.Restart();
        }

        if(respawnTimer.ElapsedMilliseconds >= respawnTime && respawnTimer.IsRunning)
        {
            respawnTimer.Stop();
            respawnTimer.Reset();
            respawn();
        }
    }

    private void checkState()
    {
        nearPlayer = Vector3.Distance(player.GetLocation(), gameObject.transform.position) <= runFromPlayerDistance;
        centerTooFar = Vector3.Distance(center.GetLocation(), gameObject.transform.position) >= maxCenterDistance;
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

        if (nearPlayer)
        {
            UnityEngine.Debug.Log("nearPlayer");
        }
        else if (centerTooFar)
        {
            UnityEngine.Debug.Log("tooFar");
        }
        else if (cageTooClose)
        {
            UnityEngine.Debug.Log("tooClose");
        }
        else if (doneMoving)
        {
            UnityEngine.Debug.Log("doneMoving");
        }
    }

    private void setMoveDirection()
    {
        Vector3 creatureToPlayer = (gameObject.transform.position - player.GetLocation()).normalized;
        Vector3 creatureToCenter = (gameObject.transform.position - center.GetLocation()).normalized;
        Vector3 creatureToCage   = (gameObject.transform.position - cage.GetLocation()).normalized;
        creatureToPlayer.y = 0;
        creatureToCenter.y = 0;
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
        else if (centerTooFar)
        {
            moveDirection = -1 * creatureToCenter;
            moveDirection.Normalize();
        }
        else if (doneMoving)
        {
            doneMoving = false;

            moveDirection = randomDirection();
            audioSource.Stop();
            if(moveDirection == new Vector3(0, 0, 0))
            {
                PlayClip(soundManager.getAudioClip(ClipType.CreatureIdle));
            }
            else
            {
                PlayClip(soundManager.getAudioClip(ClipType.CreatureWalk));
            }

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

    private Vector3 randomDirection()
    {

        bool negativeX = random.Next(0, 2) < 1;
        bool negativeZ = random.Next(0, 2) < 1;

        float moveX = (float)random.NextDouble();
        float moveZ = (float)random.NextDouble();

        if (negativeX)
            moveX *= -1f;
        if (negativeZ)
            moveZ *= -1f;

        int randomResult = random.Next(0, 101);
        if (randomResult <= 50 && randomResult % 2 == 0)
        {
            return new Vector3(0, 0, 0);
        }

        return new Vector3(moveX, 0, moveZ).normalized;
    }

    private void respawn()
    {
        float distanceFromCenter = (float)(maxCenterDistance * random.NextDouble());
        Vector3 respawnLocation = center.GetLocation() + randomDirection() * distanceFromCenter;

        while(Vector3.Distance(respawnLocation, cage.GetLocation()) <= minCageDistance * 1.5f)
        {
            respawnLocation = center.GetLocation() + randomDirection() * distanceFromCenter;
        }

        respawnLocation.y = 2.2f;
        gameObject.transform.position = respawnLocation;
        PlayClip(soundManager.getAudioClip(ClipType.CreatureSpawn));
    }

    public void PlayClip(AudioClip audioClip, float delay = 0.0f)
    {
        audioSource.clip = audioClip;
        audioSource.PlayDelayed(delay);
    }

    public void Pickup()
    {
        pickedUp = true;
        PlayClip(soundManager.getAudioClip(ClipType.CreaturePickUp));
    }

    public void Drop()
    {
        pickedUp = false;
        if(Vector3.Distance(gameObject.transform.position, cage.GetLocation()) <= cage.GetRespawnDistance())
        {
            cage.PlayCagedSound();
        }
        {
            PlayClip(soundManager.getAudioClip(ClipType.CreatureDropped));
        }
    }
}
