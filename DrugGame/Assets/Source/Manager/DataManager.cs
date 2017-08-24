using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine;
[System.Serializable]
public class DataManager
{
    private static DataManager instance = null;
    public static DataManager inst
    {
        get
        {
            if (instance == null)
            {
                instance = new DataManager();
            }
            return instance;
        }
    }

    public const int characterSize = 3;

    public int coin = 0;
    public int savedCoin = 0;
    public int point = 0;
    public int highestPoint = 0;
    public double playTime = 0;
    public double totalPlayTime = 0;
    public bool[] characterUnlock = new bool[characterSize];

    public void Save()
    {
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.IndentChars = ("\t");

        string path = Path.Combine(Application.persistentDataPath, @"save.xml");
        Debug.Log(path);

        using (XmlWriter writer = XmlWriter.Create(path, settings))
        {
            writer.WriteStartDocument();

            writer.WriteStartElement("data");

            writer.WriteStartElement("coin");
            writer.WriteAttributeString("amount", savedCoin.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("point");
            writer.WriteAttributeString("value", highestPoint.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("playtime");
            writer.WriteAttributeString("time", totalPlayTime.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("character");
            for (int i = 0; i < characterSize; ++i)
            {
                writer.WriteAttributeString("character" + i.ToString(), characterUnlock[i].ToString());
            }
            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();
        }
    }

    public void Load()
    {
        XmlDocument doc = new XmlDocument();
        try
        {
            doc.Load(Path.Combine(Application.persistentDataPath, @"save.xml"));
        }
        catch(FileNotFoundException e)
        {
            savedCoin = 0;
            highestPoint = 0;
            totalPlayTime = 0;
            for (int i=0; i<characterSize; ++i)
            {
                characterUnlock[i] = false;
            }
            return;
        }

        XmlElement data = doc["data"];

        savedCoin = System.Convert.ToInt32(data["coin"].GetAttribute("amount"));
        highestPoint = System.Convert.ToInt32(data["point"].GetAttribute("value"));
        totalPlayTime = System.Convert.ToDouble(data["playtime"].GetAttribute("time"));

        for(int i = 0 ; i < characterSize; ++i)
        {
            characterUnlock[i] = System.Convert.ToBoolean(data["character"].GetAttribute("character"+ i.ToString()));
        }
    }
}