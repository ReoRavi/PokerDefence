using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PokerRank { None, Top, OnePair, TwoPair, Triple, FullHouse, FourCard, BackStraight, Straight, Mountain, Flush, BackStraightFlush, StraightFlush, RoyalStraightFlush };

public class CardBoard : MonoBehaviour {

    // Card UI
    private List<GameObject> cards;
    // CardBoard UI
    private List<GameObject> boardObjects;
    // Card Rank Text
    private Text rankText;
    // Card Sprite Cashed Object
    private Sprite[] cardSprites;
    // Suffled Card Index
    private List<int> suffledCardIndexs;
    
    // Board State
    private bool isBoardOpen;
    //TurretPrefabs
    private GameObject[] turretsPrefabs;

    // Poker Rank
    private PokerRank rank;

    #region Test Option Variable
    [Header("Test Option")]
    [SerializeField]
    private bool testOption;
    [SerializeField]
    private PokerRank testRank;
    private int cardCountbyShape;
    #endregion
    

    // Turret Will Set This Tile Position
    private Transform currentTile; 

    private void Start()
    {
        cards = new List<GameObject>();

        for (int i = 0; i < 5; i++)
        {
            GameObject card = GameObject.Find("Card" + (i + 1).ToString());

            if (card != null)
            {
                card.SetActive(false);
                cards.Add(card);
            }
        }

        boardObjects = new List<GameObject>();

        GameObject table = GameObject.Find("Table");
        table.SetActive(false);
        boardObjects.Add(table);

        GameObject build = GameObject.Find("Build");
        build.SetActive(false);
        boardObjects.Add(build);

        rankText = GameObject.Find("RankText").GetComponent<Text>();
        rankText.gameObject.SetActive(false);
        boardObjects.Add(rankText.gameObject);


        turretsPrefabs = Resources.LoadAll<GameObject>("prefab/turret");

        cardSprites = Resources.LoadAll<Sprite>("card");

        suffledCardIndexs = new List<int>();

        isBoardOpen = false;
        cardCountbyShape = cardSprites.Length / 4;
    }

    public void SetCard(Transform currentTile)
    {
        if (isBoardOpen)
            return;

        this.currentTile = currentTile;

        isBoardOpen = true;

        SetBoardActive(true);

        suffledCardIndexs.Clear();

        // Test Option On
        if (testOption)
        {
            int shapeAddCount = 1;
            int currentShapeCount = 0;
            int maxShapeCount = 3;

            List<int> cardNumbers = new List<int>(cards.Count);
            switch (testRank)
            {
                case PokerRank.OnePair:
                    cardNumbers.AddRange(new int[] { 0, 0, 2, 4, 8 });

                    maxShapeCount = 3;

                    break;

                case PokerRank.TwoPair:
                    cardNumbers.AddRange(new int[] { 0, 0, 2, 2, 4 });

                    maxShapeCount = 1;

                    break;

                case PokerRank.Triple:
                    cardNumbers.AddRange(new int[]{ 0, 0, 0, 2, 8 });

                    maxShapeCount = 2;

                    break;

                case PokerRank.FullHouse:

                    cardNumbers.AddRange(new int[] { 0, 0, 0, 8, 8 });

                    maxShapeCount = 2;

                    break;

                case PokerRank.FourCard:

                    cardNumbers.AddRange(new int[] { 0, 0, 0, 0, 8 });

                    maxShapeCount = 1;

                    break;

                case PokerRank.Flush:

                    cardNumbers.AddRange(new int[] { 0, 2, 4, 7, 8 });

                    break;

                case PokerRank.BackStraight:
                case PokerRank.BackStraightFlush:

                    cardNumbers.AddRange(new int[] { 0, 1, 2, 3, 4 });

                    break;

                case PokerRank.Straight:
                case PokerRank.StraightFlush:

                    cardNumbers.AddRange(new int[] { 1, 2, 3, 4, 5 });

                    break;

                case PokerRank.Mountain:
                case PokerRank.RoyalStraightFlush:

                    cardNumbers.AddRange(new int[] { 0, 5, 6, 7, 8 });

                    break;
            }

            if (testRank == PokerRank.Flush ||
                testRank == PokerRank.BackStraightFlush ||
                testRank == PokerRank.StraightFlush ||
                testRank == PokerRank.RoyalStraightFlush)
                shapeAddCount = 0;

            for (int i = 0; i < cards.Count; i++)
            {
                int spriteIndex = (currentShapeCount * cardCountbyShape) + cardNumbers[i];

                GameObject card = cards[i];
                card.GetComponent<Image>().sprite = cardSprites[spriteIndex];

                Card cardData = card.GetComponent<Card>();
                cardData.Init(spriteIndex / cardCountbyShape, cardNumbers[i]);

                if (currentShapeCount != maxShapeCount)
                    currentShapeCount += shapeAddCount;
            }
        }
        else
        {
            Suffle();

            for (int i = 0; i < cards.Count; i++)
            {
                int spriteIndex = suffledCardIndexs[0];

                GameObject card = cards[i];
                card.GetComponent<Image>().sprite = cardSprites[spriteIndex];

                Card cardData = card.GetComponent<Card>();
                cardData.Init(spriteIndex / cardCountbyShape, spriteIndex % cardCountbyShape);

                suffledCardIndexs.Remove(spriteIndex);
            }
        }
    }

    private void SetBoardActive(bool active)
    {
        foreach (GameObject obj in boardObjects)
        {
            obj.SetActive(active);
        }

        foreach (GameObject obj in cards)
        {
            obj.SetActive(active);
            obj.transform.GetChild(0).gameObject.SetActive(active);
        }

        rank = GetPokerRank();
    }

    public void BulidTower()
    {
        SetBoardActive(false);
        isBoardOpen = false;

        int turretLevel = 0;

        switch (rank)
        {
            case PokerRank.Top:
                turretLevel = 0;

                break;

            case PokerRank.OnePair:
                turretLevel = 1;

                break;

            case PokerRank.TwoPair:
                turretLevel = 2;

                break;

            case PokerRank.Triple:
                turretLevel = 3;

                break;

            case PokerRank.FullHouse:
                turretLevel = 4;

                break;

            case PokerRank.FourCard:
                turretLevel = 5;

                break;

            case PokerRank.BackStraight:
                turretLevel = 6;

                break;

            case PokerRank.Straight:
                turretLevel = 7;

                break;

            case PokerRank.Mountain:
                turretLevel = 8;

                break;

            case PokerRank.Flush:
                turretLevel = 9;

                break;

            case PokerRank.BackStraightFlush:
                turretLevel = 10;

                break;

            case PokerRank.StraightFlush:
                turretLevel = 11;

                break;

            case PokerRank.RoyalStraightFlush:
                turretLevel = 12;

                break;
        }

        TowerTile tile = currentTile.GetComponent<TowerTile>();
        Instantiate(turretsPrefabs[turretLevel], new Vector3(currentTile.position.x, currentTile.position.y, turretsPrefabs[turretLevel].transform.position.z), Quaternion.identity);

        rankText.text = rank.ToString();
        Debug.Log(rank);
    }

    public void ChangeCard(int cardIndex)
    {
        int spriteIndex = suffledCardIndexs[0];

        GameObject card = cards[cardIndex];
        card.GetComponent<Image>().sprite = cardSprites[spriteIndex];

        Card cardData = card.GetComponent<Card>();
        cardData.Init(spriteIndex / cardCountbyShape, spriteIndex % cardCountbyShape);

        suffledCardIndexs.Remove(spriteIndex);

        card.transform.GetChild(0).gameObject.SetActive(false);

        rank = GetPokerRank();
    }

    // Not Use 3,4,5,6
    private void Suffle()
    {
        List<int> dummyIndexs = new List<int>();

        for (int i = 0; i < cardSprites.Length; i++)
        {
            dummyIndexs.Add(i);
        }

        for (int i = 0; i < 10; i++)
        {
            int cardIndex = dummyIndexs[Random.Range(0, dummyIndexs.Count)];

            suffledCardIndexs.Add(cardIndex);
            dummyIndexs.Remove(cardIndex);
        }
    }

#region Rank
    private PokerRank GetPokerRank()
    {
        PokerRank pair = CheckPair();
        PokerRank straight = CheckStraight();
        bool flush = CheckFlush();

        // Top
        if (pair == PokerRank.None && straight == PokerRank.None && !flush)
        {
            return PokerRank.Top;
        }
        else if (straight != PokerRank.None)
        {
            // Flush, StraightFlush, BackStraightFlush, RoyalStraightFlush
            if (flush)
            {
                switch (straight)
                {
                    case PokerRank.Straight:
                        return PokerRank.StraightFlush;

                    case PokerRank.BackStraight:
                        return PokerRank.BackStraightFlush;

                    case PokerRank.Mountain:
                        return PokerRank.RoyalStraightFlush;

                    case PokerRank.None:
                        return PokerRank.Flush;

                }
            }
            // Straight, BackStraight, Mountain
            else
            {
                return straight;
            }
        }
        else if (flush)
        {
            return PokerRank.Flush;
        }
        // OnePair, TwoPair, Triple, FullHouse, FourCard
        else
        {
            return pair;
        }

        return PokerRank.None;
    }

    // OnePair, TwoPair, Triple, FourCard, FullHouse
    private PokerRank CheckPair()
    {
        int pairCount = 0;
        int tripleCount = 0;

        List<int> checkedIndex = new List<int>();

        for (int checkCard = 0; checkCard < cards.Count; checkCard++)
        {
            int rankCount = 1;
            int checkNumber = cards[checkCard].GetComponent<Card>().number;

            for (int currentCard = checkCard + 1; currentCard < cards.Count; currentCard++)
            {
                if (checkedIndex.Contains(currentCard))
                    continue;

                if (checkNumber == cards[currentCard].GetComponent<Card>().number)
                {
                    checkedIndex.Add(currentCard);
                    rankCount++;
                }
            }

            switch (rankCount)
            {
                // Pair
                case 2:
                    pairCount++;
                    break;

                // Triple
                case 3:
                    tripleCount++;
                    break;
                
                // FourK
                case 4:
                    return PokerRank.FourCard;
            }
        }

        if (pairCount == 1 && tripleCount == 1)
            return PokerRank.FullHouse;
        else if (tripleCount == 1)
            return PokerRank.Triple;
        else if (pairCount == 2)
            return PokerRank.TwoPair;
        else if (pairCount == 1)
            return PokerRank.OnePair;
        else
            return PokerRank.None;
    }

    // Flush
    private bool CheckFlush()
    {
        int flushShape = cards[0].GetComponent<Card>().shape;

        for (int checkCard = 1; checkCard < cards.Count; checkCard++)
        {
            int checkShape = cards[checkCard].GetComponent<Card>().shape;

            if (flushShape != checkShape)
                return false;
        }

        return true;
    }

    // Check Straight
    private PokerRank CheckStraight()
    {
        List<int> numberList = new List<int>();

        for (int checkCard = 0; checkCard < cards.Count; checkCard++)
        {
            numberList.Add(cards[checkCard].GetComponent<Card>().number);
        }

        numberList.Sort(delegate (int a, int b)
        {
            if (a > b)
                return 1;
            else if (a < b)
                return -1;
            return 0;
        });

        int firstIndex = numberList[0];

        if (firstIndex == 0)
        {
            // Check Mountain, Royal Straight Flush (A, 10, J, Q, K)
            int[] royalNumber = { 0, 5, 6, 7, 8 };

            for (int index = 1; index < cards.Count; index++)
            {
                if (royalNumber[index] != numberList[index])
                    break;

                if (index == 4)
                    return PokerRank.Mountain;
            }

            // Check Back Straight (A, 2, 7, 8, 9)
            int[] backNumber = { 0, 1, 2, 3, 4 };

            for (int index = 1; index < cards.Count; index++)
            {
                if (backNumber[index] != numberList[index])
                    break;

                if (index == 4)
                    return PokerRank.BackStraight;
            }
        }

        for (int index = 1; index < cards.Count; index++)
        {
            if (numberList[index] - firstIndex != 1)
                return PokerRank.None;

            firstIndex = numberList[index];
        }

        return PokerRank.Straight;
    }
#endregion
}
