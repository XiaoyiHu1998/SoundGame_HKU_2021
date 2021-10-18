using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GodLine
{
    GodIdle,
    GodDrown,
    GodCapture
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
    public int maxQueueLength;
    public AudioClip errorSound;
    public GodSource godSource;

    //audio clips
    public List<AudioClip> creatureWalk;
    public List<AudioClip> creatureRun;
    public List<AudioClip> creatureIdle;
    public List<AudioClip> CreaturePickUp;
    public List<AudioClip> CreatureDropped;
    public List<AudioClip> CreatureCaged;
    public List<AudioClip> CreatureSpawn;
    public List<AudioClip> playerWalk;

    private Queue<int> creatureWalkQueue;
    private Queue<int> creatureRunQueue;
    private Queue<int> creatureIdleQueue;
    private Queue<int> CreaturePickUpQueue;
    private Queue<int> CreatureDroppedQueue;
    private Queue<int> CreatureCagedQueue;
    private Queue<int> CreatureSpawnQueue;
    private Queue<int> playerWalkQueue;

    //god voicelines
    public List<AudioClip> godIdle;
    public List<AudioClip> godDrown;
    public List<AudioClip> godCapture;

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
        CreaturePickUpQueue  = new Queue<int>();
        CreatureDroppedQueue = new Queue<int>();
        CreatureCagedQueue   = new Queue<int>();
        CreatureSpawnQueue   = new Queue<int>();

        godIdleQueue     = new Queue<int>();
        godDrownQueue    = new Queue<int>();
        godCaptureQueue  = new Queue<int>();

        random = new System.Random();
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
            case ClipType.PlayerWalk:
                return playerWalk[getRandomClip(ref playerWalk, ref playerWalkQueue)];
            default:
                return errorSound;
        }
    }

    public void playGodLine(GodLine godLine)
    {
        switch (godLine)
        {
            case GodLine.GodIdle:
                godSource.PlayLine(godIdle[getRandomClip(ref godIdle, ref godIdleQueue)]);
                break;
            case GodLine.GodDrown:
                godSource.PlayLine(godDrown[getRandomClip(ref godDrown, ref godDrownQueue)]);
                break;
            case GodLine.GodCapture:
                godSource.PlayLine(godCapture[getRandomClip(ref godCapture, ref godCaptureQueue)]);
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
        while (queue.Count > maxQueueLength)
        {
            queue.Dequeue();
        }

        return index;
    }

}
