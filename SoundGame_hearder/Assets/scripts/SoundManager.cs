using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GodLine
{
    PlayerIdle,
    GodIdle,
    GodDrown,
    GodCapture,
}
public enum ClipType
{
    CreatureWalk,
    CreatureRun,
    CreatureIdle,
    CreaturePickUp,
    CreaturePickedUp,
    CreatureDropped,
    CreatureCaged,
    CreatureSpawn,
    PlayerWalk,
};

public class SoundManager : MonoBehaviour
{
    public AudioClip errorSound;
    public GodSource godSource;
    public bool tutorial;

    //audio clips
    public List<AudioClip> creatureWalk;
    public List<AudioClip> creatureRun;
    public List<AudioClip> creatureIdle;
    public List<AudioClip> CreaturePickUp;
    public List<AudioClip> CreaturePickedUp;
    public List<AudioClip> CreatureDropped;
    public List<AudioClip> CreatureCaged;
    public List<AudioClip> CreatureSpawn;
    public List<AudioClip> playerWalk;

    private Queue<int> creatureWalkQueue;
    private Queue<int> creatureRunQueue;
    private Queue<int> creatureIdleQueue;
    private Queue<int> CreaturePickUpQueue;
    private Queue<int> CreaturePickedUpQueue;
    private Queue<int> CreatureDroppedQueue;
    private Queue<int> CreatureCagedQueue;
    private Queue<int> CreatureSpawnQueue;
    private Queue<int> playerWalkQueue;

    //god voicelines
    public List<AudioClip> playerIdle;
    public List<AudioClip> godIdle;
    public List<AudioClip> godDrown;
    public List<AudioClip> godCapture;

    private Queue<int> playerIdleQueue;
    private Queue<int> godIdleQueue;
    private Queue<int> godDrownQueue;
    private Queue<int> godCaptureQueue;

    private System.Random random;
    void Start()
    {
        creatureWalkQueue    = new Queue<int>();
        creatureRunQueue     = new Queue<int>();
        creatureIdleQueue    = new Queue<int>();
        playerWalkQueue      = new Queue<int>();
        CreaturePickUpQueue = new Queue<int>();
        CreaturePickedUpQueue = new Queue<int>();
        CreatureDroppedQueue = new Queue<int>();
        CreatureCagedQueue   = new Queue<int>();
        CreatureSpawnQueue   = new Queue<int>();

        playerIdleQueue  = new Queue<int>();
        godIdleQueue     = new Queue<int>();
        godDrownQueue    = new Queue<int>();
        godCaptureQueue  = new Queue<int>();

        random = new System.Random();

        if (tutorial)
        {
            godSource.setAllowedList(new List<AudioClip>());
        }
        else
        {
            godSource.allowAll();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public AudioClip getAudioClip(ClipType clipType)
    {
        switch (clipType)
        {
            case ClipType.CreatureIdle:
                return creatureIdle[getRandomClip(ref creatureIdle, ref creatureIdleQueue)];

            case ClipType.CreatureWalk:
                return creatureWalk[getRandomClip(ref creatureWalk, ref creatureWalkQueue)];

            case ClipType.CreatureRun:
                return creatureRun[getRandomClip(ref creatureRun, ref creatureRunQueue)];

            case ClipType.CreaturePickUp:
                return CreaturePickUp[getRandomClip(ref CreaturePickUp, ref CreaturePickUpQueue)];

            case ClipType.CreaturePickedUp:
                return CreaturePickedUp[getRandomClip(ref CreaturePickedUp, ref CreaturePickedUpQueue)];

            case ClipType.CreatureDropped:
                return CreatureDropped[getRandomClip(ref CreatureDropped, ref CreatureDroppedQueue)];

            case ClipType.CreatureCaged:
                return CreatureCaged[getRandomClip(ref CreatureCaged, ref CreatureCagedQueue)];

            case ClipType.CreatureSpawn:
                return CreatureSpawn[getRandomClip(ref CreatureSpawn, ref CreatureSpawnQueue)];

            case ClipType.PlayerWalk:
                return playerWalk[getRandomClip(ref playerWalk, ref playerWalkQueue)];

            default:
                return errorSound;
        }
    }

    public void playGodLine(GodLine godLine, bool overrideCurrentLine = false)
    {
        switch (godLine)
        {
            case GodLine.PlayerIdle:
                godSource.PlayLine(playerIdle[getRandomClip(ref playerIdle, ref playerIdleQueue)], overrideCurrentLine);
                break;
            case GodLine.GodIdle:
                godSource.PlayLine(godIdle[getRandomClip(ref godIdle, ref godIdleQueue)], overrideCurrentLine);
                break;
            case GodLine.GodDrown:
                godSource.PlayLine(godDrown[getRandomClip(ref godDrown, ref godDrownQueue)], overrideCurrentLine);
                break;
            case GodLine.GodCapture:
                godSource.PlayLine(godCapture[getRandomClip(ref godCapture, ref godCaptureQueue)], overrideCurrentLine);
                break;
        }
    }

    //gives random index of a list of audioClips and updates a queue of last indices returned for that list
    private int getRandomClip(ref List<AudioClip> list, ref Queue<int> queue)
    {
        //get random index, always at least one loop
        int index;
        do
        {
            index = random.Next(0, list.Count);
        }   while (queue.Contains(index));

        //add index to queue and make sure only last maxQueueLength indices are remembered
        queue.Enqueue(index);
        if(queue.Count == list.Count)
        {
            int maxQueueLength = list.Count / 2;
            if (list.Count == 1)
                maxQueueLength = 0;

            while (queue.Count > maxQueueLength)
            {
                queue.Dequeue();
            }
        }

        return index;
    }

}
