using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITextAnimator : MonoBehaviour
{
    private TextMeshProUGUI text;

    public DynSFXEvent sound;
    // Start is called before the first frame update
    public bool hideDuringDelay;
    public bool playOnEnable = true;
    public float speed;
    public float delay;

    string fullString;
    char[] characters;
    
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        fullString = text.text;
        characters = fullString.ToCharArray();
    }

    private void OnEnable()
    {

        if (playOnEnable)
        {
            Play();
        }
    }

    bool isPlaying;

    public void Play()
    {

        if (isPlaying)
        {
            Cancel();
        }

        HandleAnimateText();
    }

    public void Set(string str)
    {
        fullString = str;
        characters = fullString.ToCharArray();
    }

    public void Cancel()
    {
        //LeanTween.cancel();

        StopCoroutine(textAnim);
        isPlaying = false;
        text.text = fullString;
    }

    Coroutine textAnim;

    private void HandleAnimateText()
    {
        
        isPlaying = true;
        
        text.text = "";

        textAnim = StartCoroutine(DelayAndPlay());

    }

    IEnumerator DelayAndPlay()
    {
        yield return new WaitForSeconds(delay);
        textAnim = StartCoroutine(AnimatePerCharacter());
    }


    IEnumerator AnimatePerCharacter()
    {
        

        for (int i=0; i<characters.Length; i++)
        {

            //Check if the next character is the start of a sprite and add the whole chunk. 
            if (characters[i] == '<')
            {
                string spriteText = "";

                while (characters[i] != '>')
                {
                    spriteText += characters[i];
                    i++;
                }

                spriteText += characters[i];

                text.text += spriteText;
            }
            else
            {

                text.text += characters[i];
            }
            if (sound != null)
            {
                sound.Play();
            }
            yield return new WaitForSecondsRealtime(speed);
        }

    }

    
}
