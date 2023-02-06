using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] Button _StartGame;
    [SerializeField] Button _HowPlay;
    [SerializeField] Text _TextFieldManager;
    [SerializeField] Text _TextField;
    [SerializeField] Text _ButtonText;
    int which = 0;

    // Start is called before the first frame update
    void Start()
    {
        _StartGame.onClick.AddListener(StartNewGame);
        _HowPlay.onClick.AddListener(HowToPlayGame);

    }

    private void StartNewGame()
    {
        ScenesManager.Instance.LoadNextScene();
    }

    private void HowToPlayGame()
    {
        string text;
        string buttonText;
        if (which == 0)
        {
            text = "Arrows, A and D to Move.\nSpace to shoot.\n\nHit targets to earn points. Extra funding from Ministery earns 300 extra points.";
            buttonText = "Tell the story";
            which = 1;
        }
        else
        {
            text = "In this game you are playing as M*TROPOLIA, a great school for promising game developers. Your object is to shoot down projects from competing schools and possible hit even the jackpot from Ministery of Culture- and Education!\r\n\r\nBe careful though, other schools try to shoot you down as well and if you're not fast enough, they might overwhelm you. Use shields to protect yourself.\r\n\r\nGood Luck Space Educational Institute!\r\n\r\n\r\nPS. It's pronounced EM STAR TROPOLIA";

            buttonText = "How to play";
            which = 0;
        }
        SetTextFieldText(_TextFieldManager, text);
        SetButtonFieldText(_ButtonText, buttonText);
    }
    /*

        In this game you are playing as M*TROPOLIA, a great school for promising game developers. Your object is to shoot down projects from competing schools and possible hit even the jackpot from Ministery of Culture- and Education!

    Be careful though, other schools try to shoot you down as well and if you're not fast enough, they might overwhelm you. Use shields to protect yourself.

    Good Luck Space Educational Institute!


    PS. It's pronounced EM STAR TROPOLIA


        */
    public void SetTextFieldText(Text textField, string text)
    {
        textField.text = text;
    }
    public void SetButtonFieldText(Text buttonText, string text)
    {
        buttonText.text = text;
    }

}
