using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public AudioSource audios;
    public AudioClip audi;
    public AudioClip audi1;

    public Image[] images;
    public GameObject panel;
    public GameObject panel2;
    public TextMeshProUGUI txt1;
    public TextMeshProUGUI txt2;
    public TextMeshProUGUI txt3;
    public TextMeshProUGUI txt4;
    public TextMeshProUGUI txt5;
    public TextMeshProUGUI txt6;
    public TextMeshProUGUI txt7;
    public TextMeshProUGUI txt8;
    public TextMeshProUGUI txt9;
    public TextMeshProUGUI txt10;
    public TextMeshProUGUI txt11;
    public TextMeshProUGUI txt12;
    public TextMeshProUGUI txt13;

    public Transform[] dice;
    bool roll = false;
    public TextMeshProUGUI txts1;
    public TextMeshProUGUI txts2;
    public TextMeshProUGUI txts3;
    public TextMeshProUGUI txts4;
    public TextMeshProUGUI txts5;
    public TextMeshProUGUI txts6;
    public TextMeshProUGUI txts7;
    public TextMeshProUGUI txts8;
    public TextMeshProUGUI txts9;
    public TextMeshProUGUI txts10;
    public TextMeshProUGUI txts11;
    public TextMeshProUGUI txts12;
    public TextMeshProUGUI txts13;
    public int speed = 20;
    [HideInInspector] public int[] facenum;
    [HideInInspector] public bool[] select;
    int totalCategories = 13;
    int usedP1 = 0;
    int usedP2 = 0;

    int index;

    public int totalscore = 0;        // Player 1 score
    public int totalscore2 = 0;       // Player 2 score
    int currentPlayer = 1;            // 1 or 2

    public int rolls = 0;
    public TextMeshProUGUI scoretxt;
    public TextMeshProUGUI scoretxt2;
    public TextMeshProUGUI turntxt;
    public TextMeshProUGUI wintxt;



    public Button onesButton, twosButton, threesButton, foursButton, fivesButton, sixesButton;
    public Button threeKindButton, fourKindButton, fullHouseButton, smallStraightButton, largeStraightButton, yahtzeeButton, chanceButton;

    public Button onesButton2, twosButton2, threesButton2, foursButton2, fivesButton2, sixesButton2;
    public Button threeKindButton2, fourKindButton2, fullHouseButton2, smallStraightButton2, largeStraightButton2, yahtzeeButton2, chanceButton2;

    Quaternion[] diceface = {
        Quaternion.Euler(180, 0, 0),
        Quaternion.Euler(-90, 0, 0),
        Quaternion.Euler(0, -90, 0),
        Quaternion.Euler(0, 90, 0),
        Quaternion.Euler(90, 0, 0),
        Quaternion.Euler(0, 0, 0),
    };

    void Start()
    {
        facenum = new int[dice.Length];
        select = new bool[dice.Length];

        turntxt.text = "PLAYER 1 TURN";
        turntxt.color = Color.blue;
        panel.SetActive(false);
    }

    void Update()
    {
        if (roll)
        {
            for (int i = 0; i < dice.Length; i++)
                if (!select[i])
                    dice[i].Rotate(speed * Time.deltaTime, speed * Time.deltaTime, speed * Time.deltaTime);
        }

        scoretxt.text = "Score = " + totalscore;
        scoretxt2.text = "Score = " + totalscore2;
    }

    public void Diceroll()
    {
        if (rolls < 3)
        {
            StartCoroutine(Dice());
            audios.PlayOneShot(audi1);

        }
    }

    IEnumerator Dice()
    {
        roll = true;
        yield return new WaitForSeconds(2f);
        roll = false;

        for (int i = 0; i < dice.Length; i++)
        {
            if (!select[i])
            {
                index = Random.Range(0, diceface.Length);
                dice[i].rotation = diceface[index];
                facenum[i] = index + 1;
            }
        }
        rolls++;
    }

    public void SelectDice(int j)
    {
        select[j] = !select[j];
        images[j].enabled = select[j];
    }

    int[] CountDice()
    {
        int[] c = new int[7];

        for (int i = 0; i < facenum.Length; i++)
        {
            int d = facenum[i];
            c[d] = c[d] + 1;
        }

        return c;
    }

    int SumAll()
    {
        int s = 0;
        for (int i = 0; i < facenum.Length; i++)
            s += facenum[i];
        return s;
    }

    public int ScoreOnes() { int[] c = CountDice(); return c[1] * 1; }
    public int ScoreTwos() { int[] c = CountDice(); return c[2] * 2; }
    public int ScoreThrees() { int[] c = CountDice(); return c[3] * 3; }
    public int ScoreFours() { int[] c = CountDice(); return c[4] * 4; }
    public int ScoreFives() { int[] c = CountDice(); return c[5] * 5; }
    public int ScoreSixes() { int[] c = CountDice(); return c[6] * 6; }

    public int ThreeOfKind()
    {
        int[] c = CountDice();
        for (int i = 1; i <= 6; i++)
            if (c[i] >= 3)
                return i * 3;
        return 0;
    }
    public int FourOfKind()
    {
        int[] c = CountDice();
        for (int i = 1; i <= 6; i++)
            if (c[i] >= 4)
                return i * 4;
        return 0;
    }
    public int FullHouse() { int[] c = CountDice(); bool three = false, two = false; for (int i = 1; i <= 6; i++) { if (c[i] == 3) three = true; if (c[i] == 2) two = true; } return (three && two) ? 25 : 0; }
    public int SmallStraight() { int[] c = CountDice(); if (c[1] > 0 && c[2] > 0 && c[3] > 0 && c[4] > 0) return 30; if (c[2] > 0 && c[3] > 0 && c[4] > 0 && c[5] > 0) return 30; if (c[3] > 0 && c[4] > 0 && c[5] > 0 && c[6] > 0) return 30; return 0; }
    public int LargeStraight() { int[] c = CountDice(); if (c[1] == 1 && c[2] == 1 && c[3] == 1 && c[4] == 1 && c[5] == 1) return 40; if (c[2] == 1 && c[3] == 1 && c[4] == 1 && c[5] == 1 && c[6] == 1) return 40; return 0; }
    public int Yahtzee() { int[] c = CountDice(); for (int i = 1; i <= 6; i++) if (c[i] == 5) return 50; return 0; }
    public int Chance() { return SumAll(); }


    public void OnClick_Ones()
    {
        AddScore(ScoreOnes(), onesButton);
        txt1.text = "" + ScoreOnes();
        audios.PlayOneShot(audi);


    }
    public void OnClick_Twos()
    {
        AddScore(ScoreTwos(), twosButton);
        txt2.text = "" + ScoreTwos();
        audios.PlayOneShot(audi);
    }
    public void OnClick_Threes()
    {
        AddScore(ScoreThrees(), threesButton);
        txt3.text = "" + ScoreThrees();
        audios.PlayOneShot(audi);
    }
    public void OnClick_Fours()
    {
        AddScore(ScoreFours(), foursButton);
        txt4.text = "" + ScoreFours();
        audios.PlayOneShot(audi);
    }
    public void OnClick_Fives()
    {
        AddScore(ScoreFives(), fivesButton);
        txt5.text = "" + ScoreFives();
        audios.PlayOneShot(audi);
    }
    public void OnClick_Sixes()
    {
        AddScore(ScoreSixes(), sixesButton);
        txt6.text = "" + ScoreSixes();
        audios.PlayOneShot(audi);
    }
    public void OnClick_ThreeKind()
    {
        AddScore(ThreeOfKind(), threeKindButton);
        txt7.text = "" + ThreeOfKind();
        audios.PlayOneShot(audi);
    }
    public void OnClick_FourKind()
    {
        AddScore(FourOfKind(), fourKindButton);
        txt8.text = "" + FourOfKind();
        audios.PlayOneShot(audi);
    }
    public void OnClick_FullHouse()
    {
        AddScore(FullHouse(), fullHouseButton);
        txt9.text = "" + FullHouse();
        audios.PlayOneShot(audi);
    }
    public void OnClick_SmallStraight()
    {
        AddScore(SmallStraight(), smallStraightButton);
        txt10.text = "" + SmallStraight();
        audios.PlayOneShot(audi);
    }
    public void OnClick_LargeStraight()
    {
        AddScore(LargeStraight(), largeStraightButton);
        txt11.text = "" + LargeStraight();
        audios.PlayOneShot(audi);
    }
    public void OnClick_Yahtzee()
    {
        AddScore(Yahtzee(), yahtzeeButton);
        txt12.text = "" + Yahtzee();
        audios.PlayOneShot(audi);
    }
    public void OnClick_Chance()
    {
        AddScore(Chance(), chanceButton);
        txt13.text = "" + Chance();
        audios.PlayOneShot(audi);
    }


    public void OnClick_Ones2() { AddScore2(ScoreOnes(), onesButton2); txts1.text = "" + ScoreOnes(); audios.PlayOneShot(audi); }
    public void OnClick_Twos2() { AddScore2(ScoreTwos(), twosButton2); txts2.text = "" + ScoreTwos(); audios.PlayOneShot(audi); }
    public void OnClick_Threes2() { AddScore2(ScoreThrees(), threesButton2); txts3.text = "" + ScoreThrees(); audios.PlayOneShot(audi); }
    public void OnClick_Fours2() { AddScore2(ScoreFours(), foursButton2); txts4.text = "" + ScoreFours(); audios.PlayOneShot(audi); }
    public void OnClick_Fives2() { AddScore2(ScoreFives(), fivesButton2); txts5.text = "" + ScoreFives(); audios.PlayOneShot(audi); }
    public void OnClick_Sixes2() { AddScore2(ScoreSixes(), sixesButton2); txts6.text = "" + ScoreSixes(); audios.PlayOneShot(audi); }
    public void OnClick_ThreeKind2() { AddScore2(ThreeOfKind(), threeKindButton2); txts7.text = "" + ThreeOfKind(); audios.PlayOneShot(audi); }
    public void OnClick_FourKind2() { AddScore2(FourOfKind(), fourKindButton2); txts8.text = "" + FourOfKind(); audios.PlayOneShot(audi); }
    public void OnClick_FullHouse2() { AddScore2(FullHouse(), fullHouseButton2); txts9.text = "" + FullHouse(); audios.PlayOneShot(audi); }
    public void OnClick_SmallStraight2() { AddScore2(SmallStraight(), smallStraightButton2); txts10.text = "" + SmallStraight(); audios.PlayOneShot(audi); }
    public void OnClick_LargeStraight2() { AddScore2(LargeStraight(), largeStraightButton2); txts11.text = "" + LargeStraight(); audios.PlayOneShot(audi); }
    public void OnClick_Yahtzee2() { AddScore2(Yahtzee(), yahtzeeButton2); txts12.text = "" + Yahtzee(); audios.PlayOneShot(audi); }
    public void OnClick_Chance2() { AddScore2(Chance(), chanceButton2); txts13.text = "" + Chance(); audios.PlayOneShot(audi); }

    void AddScore(int s, Button b)
    {
        if (currentPlayer != 1) return;
        if (rolls < 3) return;
        totalscore += s;
        b.interactable = false;
        usedP1++;
        EndTurn();
    }

    void AddScore2(int s, Button b)
    {
        if (currentPlayer != 2) return;
        if (rolls < 3) return;

        totalscore2 += s;
        b.interactable = false;
        usedP2++;
        EndTurn();
    }

    void EndTurn()
    {
        rolls = 0;

        for (int i = 0; i < select.Length; i++)
        {
            select[i] = false;
            images[i].enabled = false;
        }


        currentPlayer = (currentPlayer == 1 ? 2 : 1);
        if (currentPlayer == 1)
        {
            turntxt.color = Color.blue;
        }
        else
        {
            turntxt.color = Color.red;
        }
        turntxt.text = "PLAYER " + currentPlayer + " TURN";
        if (usedP1 == totalCategories && usedP2 == totalCategories)
        {
            panel.SetActive(true);
            if (totalscore > totalscore2)
            {
                wintxt.text = "PLAYER 1 WINS!";
            }
            else if (totalscore2 > totalscore)
            {
                wintxt.text = "PLAYER 2 WINS!";
            }
            else
            {
                wintxt.text = "DRAW!";
            }
        }
    }
    public void Homebutton()
    {
        SceneManager.LoadScene(2);
        audios.PlayOneShot(audi);
    }
    public void Aboutbutton()
    {
        SceneManager.LoadScene(1);
        audios.PlayOneShot(audi);
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        panel.SetActive(false);
        audios.PlayOneShot(audi);
    }
    public void Pause()
    {
        panel2.SetActive(true);
        audios.PlayOneShot(audi);
    }
    public void Pauseback()
    {
        panel2.SetActive(false);
        audios.PlayOneShot(audi);
    }
    public void Pausehome()
    {
        SceneManager.LoadScene(2);
        audios.PlayOneShot(audi);
    }
}
