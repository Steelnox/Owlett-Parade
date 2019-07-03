using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VNConversationController : MonoBehaviour
{
    [Header("Display Vars")]
    [SerializeField] float DisplayTextSpeed;
    [SerializeField] float TextPunctuationSpeed;
    [SerializeField] float TextDramaticSpeed;
    [SerializeField] float ChangeSideSpeed;
    [SerializeField] float ShakeStrength;
    [SerializeField] float ShakeDureation;

    [Header("Public Vars")]
    public VNConversation CurrentConversation;

    [Header ("Private Vars")]
    [SerializeField] GameObject ConversationCanvas;
    [SerializeField] Text Title;
    [SerializeField] Text DialogueText;
    [SerializeField] Image LCharacterImage;
    [SerializeField] Image RCharacterImage;
    [SerializeField] Animation DialogueAnimation;
    [SerializeField] Animation LImageAnimation;
    [SerializeField] Animation RImageAnimation;

    [Header("Data")]
    [SerializeField] List<VNCharacter> Characters;

    VNCharacter currentCharacter;
    VNSlide currentSlide;
    Font DefaultFont;
    bool onConversation;

    private void Awake()
    {
        DefaultFont = DialogueText.font;
    }

    #region ENTER // EXIT CONVERSATION

    //ENTER
    public void EnterConversation()
    {
        if (onConversation)
            return;

        onConversation = true;

        if (!ConversationCanvas.activeSelf)
            ConversationCanvas.SetActive(true);

        NextSlide();
    }
    //EXIT
    public void ExitConversation()
    {
        ConversationCanvas.SetActive(false);

        onConversation = false;

        DialogueAnimation.CrossFade("TextOut", ChangeSideSpeed);
        RImageAnimation.CrossFade("RImageOut", ChangeSideSpeed);
        LImageAnimation.CrossFade("LImageOut", ChangeSideSpeed);
    }
    #endregion

    //NEXT BUTTON
    public void VNNextButton ()
    {
        if (ConversationCanvas.activeSelf)
        {
            if (OnDisplayText)
                VNFastDisplay();
            else
                NextSlide();
        }
    }

    

    //PASS SLIDE
    void NextSlide ()
    {
        //get new slide
        currentSlide = CurrentConversation.NextSlide();

        //check for finish conversation
        if (currentSlide == null)
        {
            ExitConversation();
            return;
        }

        //Set up Images
        if (GetCharacter())
        {
            if (currentCharacter.CharName == VNCharName.Owlet || currentCharacter.CharName == VNCharName.Parasite)
            {
                switch (currentSlide.CharFace)
                {
                    case VNCharImage.Neutral:
                        LCharacterImage.sprite = currentCharacter.Neutral;
                        break;
                    case VNCharImage.Special0:
                        LCharacterImage.sprite = currentCharacter.Special0;
                        break;
                    case VNCharImage.Special1:
                        LCharacterImage.sprite = currentCharacter.Special1;
                        break;
                    case VNCharImage.Other:
                        LCharacterImage.sprite = currentSlide.OtherFace;
                        break;
                }
            }
            else
            {
                switch (currentSlide.CharFace)
                {
                    case VNCharImage.Neutral:
                        RCharacterImage.sprite = currentCharacter.Neutral;
                        break;
                    case VNCharImage.Special0:
                        RCharacterImage.sprite = currentCharacter.Special0;
                        break;
                    case VNCharImage.Special1:
                        RCharacterImage.sprite = currentCharacter.Special1;
                        break;
                    case VNCharImage.Other:
                        RCharacterImage.sprite = currentSlide.OtherFace;
                        break;
                }
            }
        }
        else
        {
            LCharacterImage.sprite = null;
            Debug.Log(currentSlide.CharName + " has no Character in the Data section add one!");
        }

        //Transitions
        if (CurrentConversation.CheckChangeSide() == 'l')
        {
            DialogueAnimation.CrossFade("TextLeft", ChangeSideSpeed);
            LImageAnimation.CrossFade("LImageIn", ChangeSideSpeed);
            RImageAnimation.CrossFade("RImageOut", ChangeSideSpeed);

        }
        else if (CurrentConversation.CheckChangeSide() == 'r')
        {
            DialogueAnimation.CrossFade("TextRight", ChangeSideSpeed);
            LImageAnimation.CrossFade("LImageOut", ChangeSideSpeed);
            RImageAnimation.CrossFade("RImageIn", ChangeSideSpeed);
        }

        //check shake
        if (currentSlide.Shake)
        {
            EndShake();
            StartShake();
        }

        //change Text font
        DialogueText.font = currentCharacter.CharFont;

        //write text
        Title.text = currentSlide.CharName.ToString();
        StartDialogueDisplay (currentSlide.Text);
    }

    //Get character for Slide
    bool GetCharacter()
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            if (Characters[i].CharName == currentSlide.CharName)
            {
                currentCharacter = Characters[i];
                return true;
            }
        }

        return false;
    }

    #region Display Dialogue

    //Start
    void StartDialogueDisplay (string textToDisplay)
    {
        DialogueText.text = null;
        DisplayTextCoHolder = StartCoroutine(DisplayText(textToDisplay));
    }

    //Skip
    public void VNFastDisplay()
    {
        StopCoroutine(DisplayTextCoHolder);
        DialogueText.text = null;
        for (int i = 0; i < currentSlide.Text.Length; i++)
        {
            if (currentSlide.Text[i] == '/') i++;
            DialogueText.text = string.Concat(DialogueText.text, currentSlide.Text[i]);
        }
        EndShake();
        OnDisplayText = false;
    }

    //Coroutine
    bool OnDisplayText;
    bool lastPunctuation;
    Coroutine DisplayTextCoHolder;
    private IEnumerator DisplayText(string text)
    {
        OnDisplayText = true;
        for (int i = 0; i < text.Length; i++)
        {
            if (char.IsPunctuation(text[i]))
            {
                if (text[i] == '/')
                {
                    i++;
                    DialogueText.text = string.Concat(DialogueText.text, text[i]);
                    yield return new WaitForSeconds(TextDramaticSpeed);
                }
                else
                {
                    if (!lastPunctuation)
                    {
                        DialogueText.text = string.Concat(DialogueText.text, text[i]);
                        yield return new WaitForSeconds(TextPunctuationSpeed);
                    }
                    else
                    {
                        DialogueText.text = string.Concat(DialogueText.text, text[i]);
                        yield return new WaitForSeconds(DisplayTextSpeed);
                    }
                    lastPunctuation = true;
                }

            }
            else
            {
                lastPunctuation = false;
                DialogueText.text = string.Concat(DialogueText.text, text[i]);
                yield return new WaitForSeconds(DisplayTextSpeed);
            }
            
        }
        OnDisplayText = false;

        yield return new WaitForSeconds(1);
        EndShake();
    }
    #endregion

    #region Camerashake

    //Start
    void StartShake ()
    {
        ShakeCoHolder = StartCoroutine(Shake(ShakeDureation, ShakeStrength));
    }

    //End
    void EndShake ()
    {
        if (ShakeCoHolder != null)
        {
            StopCoroutine(ShakeCoHolder);
            OnShake = false;
        }

    }

    //Coroutine
    bool OnShake;
    Coroutine ShakeCoHolder;
    public IEnumerator Shake(float duration, float magnitude)
    {
        OnShake = true;
        Vector3 orignalPosition = DialogueText.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            DialogueText.transform.localPosition = new Vector3(orignalPosition.x + x, orignalPosition.y + y, DialogueText.transform.localPosition.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        DialogueText.transform.localPosition = orignalPosition;
        OnShake = false;
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            EnterConversation();
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown (KeyCode.Mouse0))
            VNNextButton();
    }
}
