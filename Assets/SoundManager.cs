using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip walk, glory, hit, intro, dump, bravo, outro, bg;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        bg = Resources.Load<AudioClip>("background");
        glory = Resources.Load<AudioClip>("openchest");
        hit = Resources.Load<AudioClip>("hitchest");
        intro = Resources.Load<AudioClip>("intro");
        dump = Resources.Load<AudioClip>("dumpchest");
        bravo = Resources.Load<AudioClip>("bravo");
        outro = Resources.Load<AudioClip>("outro");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void playSound(string clip)
    {
        switch (clip)
        {
            case "outro":
                audioSrc.PlayOneShot(outro);
                break;
            case "glory":
                audioSrc.PlayOneShot(glory);
                break;
            case "intro":
                audioSrc.PlayOneShot(intro);
                break;
            case "hit":
                audioSrc.PlayOneShot(hit);
                break;
            case "dump":
                audioSrc.PlayOneShot(dump);
                break;
            case "bravo":
                audioSrc.PlayOneShot(bravo);
                break;
            default:
                break;
        }
    }
}
