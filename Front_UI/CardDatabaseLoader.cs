using System.Data;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Xml.Linq;

public class CardDatabaseLoader : MonoBehaviour
{
    [SerializeField] private CardInfo cardInfo;
    [SerializeField] private Sprite[] icons;
    [SerializeField] private Sprite[] images;
    private string dbPath;

    private void Start()
    {
        string streamingPath = Application.streamingAssetsPath;
        if (!streamingPath.EndsWith("/"))
            streamingPath += "/";
        dbPath = "URI=file:" + streamingPath + "UIDB.db";
        Debug.Log(dbPath);
        cardInfo.InitializeDictionary();
        LoadCardsFromDB();
    }

    private void LoadCardsFromDB()
    {
        using (var connection = new SqliteConnection(dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM \"Cards\"";
                using (IDataReader reader = command.ExecuteReader())
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        Card newCard = new Card();
                        newCard.cardName = reader["Name"].ToString();
                        newCard.cardGrade = (CardGrade)System.Enum.Parse(typeof(CardGrade), reader["Grade"].ToString());
                        newCard.cardType = (CardType)System.Enum.Parse(typeof(CardType), reader["Type"].ToString());
                        newCard.cardIcon = icons[index];
                        newCard.cardImage = images[index];
                        newCard.weight = int.Parse(reader["Level"].ToString());
                        newCard.typeLevel = int.Parse(reader["Level"].ToString());
                        newCard.breakthrough = int.Parse(reader["Breakthrough"].ToString());

                        index++;
                        // CardInfo의 리스트에 추가
                        cardInfo.cardList.Add(newCard);
                    }
                }
            }
        }

        cardInfo.InitializeDictionary();

        Debug.Log($"Loaded {cardInfo.cardList.Count} cards into CardInfo.");
    }


    private class DBCardData
    {
        public string cardName;
        public CardGrade cardGrade;
        public CardType cardType;
        public int weight;
        public int typeLevel;
        public int breakthrough;
    }
}