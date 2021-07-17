using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip walk, glory, hit, intro, dump, bravo, outro, bg, menu;
    static AudioSource audioSrc;
    // Start is called before the first frame update

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        menu = Resources.Load<AudioClip>("menu");
        bg = Resources.Load<AudioClip>("background");
        glory = Resources.Load<AudioClip>("openchest");
        hit = Resources.Load<AudioClip>("hitchest");
        intro = Resources.Load<AudioClip>("intro");
        dump = Resources.Load<AudioClip>("dumpchest");
        bravo = Resources.Load<AudioClip>("bravo");
        outro = Resources.Load<AudioClip>("outro");

        audioSrc = GetComponent<AudioSource>();
        audioSrc.clip = menu;
        audioSrc.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void changeVol(float val)
    {
        audioSrc.volume = val;
    }

    public static void playSound(string clip)
    {
        if (audioSrc == null)
            return;
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
            case "stop":
                audioSrc.Stop();
                break;
            case "bg":
                audioSrc.clip = bg;
                audioSrc.Play();
                break;
            default:
                break;
        }
    }
}
