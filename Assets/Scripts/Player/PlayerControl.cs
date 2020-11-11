using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteChar;
    [SerializeField]
    private SpriteRenderer spriteHat;
    [SerializeField]
    private SpriteRenderer spritePet;

    [SerializeField]
    private List<Color> lsColors = new List<Color>();
    [SerializeField]
    private Sprite defaultSpriteChar;

    [SerializeField]
    private List<Sprite> lsHats = new List<Sprite>();
    [SerializeField]
    private List<Sprite> lsSkins = new List<Sprite>();
    [SerializeField]
    private List<Sprite> lsPets = new List<Sprite>();

   
    public void OnSetup(bool isColor)
    {
        int indexHat = DataAPIManager.Instance.GetHat();
        spriteHat.sprite = lsHats[indexHat];

        int indexPet = DataAPIManager.Instance.GetPet();
        spritePet.sprite = lsPets[indexPet];

        if (isColor)
        {
            int indexColor = DataAPIManager.Instance.GetColor();
            spriteChar.sprite = defaultSpriteChar;
            spriteChar.color = lsColors[indexColor];
        }
        else
        {
            int indexSkin = DataAPIManager.Instance.GetSkin();
            spriteChar.sprite = lsSkins[indexSkin];
        }

        GameplayView gameplayView = (GameplayView)ViewManager.Instance.currentView;
        GetComponent<CharacterInput>().joyStick = gameplayView.joyStick;
        GetComponent<CharacterHealth>().Setup(20);
    }

}
