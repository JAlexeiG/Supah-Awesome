using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XMLCheckpointManager : MonoBehaviour
{

    static XMLCheckpointManager _instance = null;
    private Chara player;

    public bool trigger;

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
        if (trigger)
        {
            loadScene();
            trigger = false;
        }
    }

    public void save()
    {

        // FOR SCENE
        GameManager.CurrentScene sceneXML = GameManager.instance.GetScene();

        XmlSerializer sceneSerializer = new XmlSerializer(typeof(GameManager.CurrentScene));
        StreamWriter sceneWriter = new StreamWriter("Scene.xml");
        sceneSerializer.Serialize(sceneWriter.BaseStream, sceneXML);
        sceneWriter.Close();

        // FOR PLAYER //
        player = FindObjectOfType<Chara>();
        Chara.XMLPlayer playerXML = player.GetXMLPlayer();

        XmlSerializer playerSerializer = new XmlSerializer(typeof(Chara.XMLPlayer));
        StreamWriter playerWriter = new StreamWriter("Player.xml");
        playerSerializer.Serialize(playerWriter.BaseStream, playerXML);
        playerWriter.Close();

        // FOR OBJECTS
        BoxSave[] boxlist = FindObjectsOfType<BoxSave>();
        int i = 0;
        foreach (BoxSave box in boxlist)
        {

            BoxSave.Box boxXML = box.GetBox();
            
            XmlSerializer boxSerializer = new XmlSerializer(typeof(BoxSave.Box));
            StreamWriter boxWriter = new StreamWriter("Boxes.xml" + i);
            boxSerializer.Serialize(boxWriter.BaseStream, boxXML);
            boxWriter.Close();
            i++;
        }

        //For Powerable Objects
        PowerSave power = FindObjectOfType<PowerSave>();

        PowerSave.powerOn powerXML = power.GetPower();
        XmlSerializer pwrSerializer = new XmlSerializer(typeof(PowerSave.powerOn));
        StreamWriter powerWriter = new StreamWriter("PowOn.xml");
        pwrSerializer.Serialize(powerWriter.BaseStream, powerXML);
        powerWriter.Close();

    }

    public void load()
    {
        // FOR OBJECTS
        BoxSave[] boxlist = FindObjectsOfType<BoxSave>();
        int i = 0;
        foreach (BoxSave box in boxlist)
        {
            XmlSerializer boxSerializer = new XmlSerializer(typeof(BoxSave.Box));
            StreamReader boxReader = new StreamReader("Boxes.xml" + i);
            BoxSave.Box loadedBox = (BoxSave.Box)boxSerializer.Deserialize(boxReader.BaseStream);
            boxReader.Close();

            box.SaveXMLPlayer(loadedBox);
            i++;
        }
        // FOR PLAYER //
        player = FindObjectOfType<Chara>();

        XmlSerializer playerSerializer = new XmlSerializer(typeof(Chara.XMLPlayer));
        StreamReader playerReader = new StreamReader("Player.xml");
        Chara.XMLPlayer loadedPlayer = (Chara.XMLPlayer)playerSerializer.Deserialize(playerReader.BaseStream);
        playerReader.Close();

        player.SaveXMLPlayer(loadedPlayer);

        //For Powerable Objects
        PowerSave power = FindObjectOfType<PowerSave>();


        XmlSerializer powerSerializer = new XmlSerializer(typeof(PowerSave.powerOn));
        StreamReader powerReader = new StreamReader("PowOn.xml");
        PowerSave.powerOn loadedPower = (PowerSave.powerOn)powerSerializer.Deserialize(powerReader.BaseStream);
        powerReader.Close();

        power.SaveXMLPlayer(loadedPower);
    }

    public void loadScene()
    {
        // FOR SCENE

        XmlSerializer sceneSerializer = new XmlSerializer(typeof(GameManager.CurrentScene));
        StreamReader sceneReader = new StreamReader("Scene.xml");
        GameManager.CurrentScene loadedScene = (GameManager.CurrentScene)sceneSerializer.Deserialize(sceneReader.BaseStream);
        sceneReader.Close();

        SceneManager.LoadScene(loadedScene.sceneNumber);
        

    }
    
    public static XMLCheckpointManager instance
    {
        get;
        set;
    }
}
