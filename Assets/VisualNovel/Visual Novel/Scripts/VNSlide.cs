using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VNCharName {Owlet, Mentor, Parasite, CrazyKiddo, ResistanceLider};
public enum VNCharImage {Neutral, Special0, Special1, Other};

//SLIDE
[System.Serializable]
public class VNSlide
{
    [Space (5)]
    public VNCharName CharName;
    public VNCharImage CharFace;
    [Tooltip ("only if Charface == Other")]
    public Sprite OtherFace;
    [TextArea]
    [Tooltip ("Use '/' to make dramatic pause. Any other punctuation will automatically add a little pause to the text")]
    public string Text;
    public bool Shake;
}

//CONVERSATION
[System.Serializable]
public class VNConversation
{
    public List<VNSlide> Slides;
    int slideIndex = -1;

    //Get Next Slide
    public VNSlide NextSlide ()
    {
        slideIndex++;

        if (slideIndex > Slides.Count - 1)
            return null;

        return Slides[slideIndex];
    }

    //Check for Secondary character in slide
    public char CheckChangeSide()
    {
        if (slideIndex == 0)
        {
            if (Slides[slideIndex].CharName != VNCharName.Owlet || Slides[slideIndex].CharName == VNCharName.Parasite)
                return 'r';
            else
                return 'l';
        }

        if (Slides[slideIndex].CharName != Slides[slideIndex - 1].CharName)
        {
            if (Slides[slideIndex].CharName == VNCharName.Owlet || Slides[slideIndex].CharName == VNCharName.Parasite)
                return 'l';
            else
                return 'r';
        }

        return 'n';
    }
}

[CreateAssetMenu(fileName = "Character", menuName = "Visual Novel/Character", order = 1)]
public class VNCharacter : ScriptableObject
{
    public VNCharName CharName;
    public Font CharFont;

    [Space (5)]
    public Sprite Neutral;
    public Sprite Special0;
    public Sprite Special1;
}