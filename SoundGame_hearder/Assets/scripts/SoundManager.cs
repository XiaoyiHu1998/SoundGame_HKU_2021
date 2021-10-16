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
    PlayerWalk,
};

public class SoundManager : MonoBehaviour
{
    public int maxQueueLength;
    // Start is called before the first frame update
    public AudioClip errorSound;
    public GodSource godSource;
    public (List<AudioClip>, Queue<int>) creatureWalk;
    public (List<AudioClip>, Queue<int>) creatureRun;
    public (List<AudioClip>, Queue<int>) creatureIdle;
    public (List<AudioClip>, Queue<int>) playerWalk;

    public (List<AudioClip>, Queue<int>) godIdle;
    public (List<AudioClip>, Queue<int>) godDrown;
    public (List<AudioClip>, Queue<int>) godCapture;

    private System.Random random;
    void Start()
    {
        creatureWalk = (new List<AudioClip>(), new Queue<int>());
        creatureRun  = (new List<AudioClip>(), new Queue<int>());
        creatureIdle = (new List<AudioClip>(), new Queue<int>());
        playerWalk   = (new List<AudioClip>(), new Queue<int>());

        godIdle     = (new List<AudioClip>(), new Queue<int>());
        godDrown    = (new List<AudioClip>(), new Queue<int>());
        godCapture  = (new List<AudioClip>(), new Queue<int>());

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
                return creatureIdle.Item1[getRandomClip(ref creatureIdle.Item1,  ref creatureIdle.Item2)];
            case ClipType.CreatureWalk:
                return creatureWalk.Item1[getRandomClip(ref creatureWalk.Item1, ref creatureWalk.Item2)];
            case ClipType.CreatureRun:
                return creatureRun.Item1[getRandomClip(ref creatureRun.Item1, ref creatureRun.Item2)];
            case ClipType.PlayerWalk:
                return playerWalk.Item1[getRandomClip(ref playerWalk.Item1, ref playerWalk.Item2)];
            default:
                return errorSound;
        }
    }

    public void playGodLine(GodLine godLine)
    {
        switch (godLine)
        {
            case GodLine.GodIdle:
                godSource.PlayLine(godIdle.Item1[getRandomClip(ref godIdle.Item1, ref godIdle.Item2)]);
                break;
            case GodLine.GodDrown:
                godSource.PlayLine(godDrown.Item1[getRandomClip(ref godDrown.Item1, ref godDrown.Item2)]);
                break;
            case GodLine.GodCapture:
                godSource.PlayLine(godCapture.Item1[getRandomClip(ref godCapture.Item1, ref godCapture.Item2)]);
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
