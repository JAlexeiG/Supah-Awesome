using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class XMLCheckpointManager : MonoBehaviour
{

    static XMLCheckpointManager _instance = null;
    private Chara player;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        player = FindObjectOfType<Chara>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            save();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            load();
        }
    }

    public void save()
    {
        // FOR PLAYER //

        Chara.XMLPlayer playerXML = player.GetXMLPlayer();

        XmlSerializer serializer = new XmlSerializer(typeof(Chara.XMLPlayer));
        StreamWriter writer = new StreamWriter("Player.xml");
        serializer.Serialize(writer.BaseStream, playerXML);
        writer.Close();
    }

    public void load()
    {

        XmlSerializer serializer = new XmlSerializer(typeof(Chara.XMLPlayer));
        StreamReader reader = new StreamReader("Player.xml");
        Chara.XMLPlayer loadedPlayer = (Chara.XMLPlayer)serializer.Deserialize(reader.BaseStream);
        reader.Close();

        player.SaveXMLPlayer(loadedPlayer);
    }


    public static XMLCheckpointManager instance
    {
        get;
        set;
    }
}
