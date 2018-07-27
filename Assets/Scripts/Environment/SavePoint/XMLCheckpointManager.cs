using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XMLCheckpointManager : MonoBehaviour
{

    [SerializeField]
    private float loadBuffer;

    static XMLCheckpointManager _instance = null;
    private Chara player;

    public bool trigger;

    public bool deleteXML;

    [SerializeField]
    private GameObject[] checkpoints;

    public int checkpointNumber;

    public class XMLCheckpointNum
    {
        public int checkNum;
    }

    private void Awake()
    {
        //DontDestroyOnLoad(this);

        checkpointNumber = -1;
        if (!instance)
        {
            instance = this;
            //DontDestroyOnLoad(this);
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
        if (deleteXML)
        {
            delete();
            deleteXML = false;
        }
    }

    public void save()
    {
        if (!Directory.Exists("SaveFiles"))
        {
            Debug.Log("Making New Directory: Save Files");
            Directory.CreateDirectory("SaveFiles");
            Debug.Log("Making New Directory: BoxSaves");
            Directory.CreateDirectory("SaveFiles/BoxSaves");
        }
        else
        {
            if (!Directory.Exists("SaveFiles/BoxSaves"))
            {
                Debug.Log("Making New Directory: BoxSaves");
                Directory.CreateDirectory("SaveFiles/BoxSaves");
            }
        }

        // FOR CHECKPOINT
        XMLCheckpointNum checkpointXML = new XMLCheckpointNum();
        checkpointXML.checkNum = checkpointNumber;
        XmlSerializer checkpointSerializer = new XmlSerializer(typeof(XMLCheckpointNum));
        StreamWriter checkWriter = new StreamWriter("SaveFiles/Checkpointnum.xml");
        checkpointSerializer.Serialize(checkWriter.BaseStream, checkpointXML);
        checkWriter.Close();


        // FOR SCENE
        GameManager.CurrentScene sceneXML = GameManager.instance.GetScene();

        XmlSerializer sceneSerializer = new XmlSerializer(typeof(GameManager.CurrentScene));
        StreamWriter sceneWriter = new StreamWriter("SaveFiles/Scene.xml");
        sceneSerializer.Serialize(sceneWriter.BaseStream, sceneXML);
        sceneWriter.Close();

        // FOR PLAYER //
        player = FindObjectOfType<Chara>();
        Chara.XMLPlayer playerXML = player.GetXMLPlayer();

        XmlSerializer playerSerializer = new XmlSerializer(typeof(Chara.XMLPlayer));
        StreamWriter playerWriter = new StreamWriter("SaveFiles/Player.xml");
        playerSerializer.Serialize(playerWriter.BaseStream, playerXML);
        playerWriter.Close();

        // FOR OBJECTS
        BoxSave[] boxlist = FindObjectsOfType<BoxSave>();
        int i = 0;
        foreach (BoxSave box in boxlist)
        {
            BoxSave.Box boxXML = box.GetBox();

            XmlSerializer boxSerializer = new XmlSerializer(typeof(BoxSave.Box));
            StreamWriter boxWriter = new StreamWriter("SaveFiles/BoxSaves/" + box.name + ".xml");
            boxSerializer.Serialize(boxWriter.BaseStream, boxXML);
            boxWriter.Close();
            i++;
        }


        //For Switches
        SwitchSave switches = FindObjectOfType<SwitchSave>();

        SwitchSave.switchXML switchXML = switches.GetSwitch();

        XmlSerializer switchSerializer = new XmlSerializer(typeof(SwitchSave.switchXML));
        StreamWriter switchWriter = new StreamWriter("SaveFiles/SwitchOn.xml");
        switchSerializer.Serialize(switchWriter.BaseStream, switchXML);
        switchWriter.Close();

    }

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine("LevelBuffer");

    }

    IEnumerator LevelBuffer()
    {
        Debug.Log("Starting wait");
        yield return new WaitForSeconds(0.00005f);

        Debug.Log("Wait finished");

        
        // FOR OBJECTS
        BoxSave[] boxlist = FindObjectsOfType<BoxSave>();

        int i = 0;
        foreach (BoxSave box in boxlist)
        {
            foreach (BoxSave boxCheck in boxlist)
            {
                if (box.name == boxCheck.name)
                {
                    Debug.Log(box.name + " " + boxCheck.name);
                    XmlSerializer boxSerializer = new XmlSerializer(typeof(BoxSave.Box));
                    StreamReader boxReader = new StreamReader("SaveFiles/BoxSaves/" + box.name + ".xml");
                    BoxSave.Box loadedBox = (BoxSave.Box)boxSerializer.Deserialize(boxReader.BaseStream);
                    boxReader.Close();

                    box.SaveXML(loadedBox);
                    i++;
                }
            }

            // FOR CHECKPOINT
            XMLCheckpointNum checkpointXML = new XMLCheckpointNum();
            XmlSerializer checkpointSerializer = new XmlSerializer(typeof(XMLCheckpointNum));
            StreamReader checkReader = new StreamReader("SaveFiles/Checkpointnum.xml");
            XMLCheckpointNum loadedCheckpoint = (XMLCheckpointNum)checkpointSerializer.Deserialize(checkReader.BaseStream);
            checkReader.Close();

            checkpointNumber = loadedCheckpoint.checkNum;

            destroyCheckpoint();


            // FOR PLAYER //
            player = FindObjectOfType<Chara>();

            Debug.Log(player.transform.name);
            XmlSerializer playerSerializer = new XmlSerializer(typeof(Chara.XMLPlayer));
            StreamReader playerReader = new StreamReader("SaveFiles/Player.xml");
            Chara.XMLPlayer loadedPlayer = (Chara.XMLPlayer)playerSerializer.Deserialize(playerReader.BaseStream);
            playerReader.Close();

            player.SaveXML(loadedPlayer);

            //For Powerable Objects
            SwitchSave switches = FindObjectOfType<SwitchSave>();

            XmlSerializer switchSerializer = new XmlSerializer(typeof(SwitchSave.switchXML));
            StreamReader switchReader = new StreamReader("SaveFiles/SwitchOn.xml");
            SwitchSave.switchXML loadedSwitch = (SwitchSave.switchXML)switchSerializer.Deserialize(switchReader.BaseStream);
            switchReader.Close();

            switches.SaveXML(loadedSwitch);
        }
    }


    public void load()
    {
        if (Directory.Exists("SaveFiles"))
        {
            // FOR SCENE
            XmlSerializer sceneSerializer = new XmlSerializer(typeof(GameManager.CurrentScene));
            StreamReader sceneReader = new StreamReader("SaveFiles/Scene.xml");
            GameManager.CurrentScene sceneLoad = (GameManager.CurrentScene)sceneSerializer.Deserialize(sceneReader.BaseStream);
            sceneReader.Close();

            GameManager.instance.loadSave(sceneLoad);

            OnLevelWasLoaded(sceneLoad.sceneNumber);
        }
    }


    public void delete()
    {
        DirectoryInfo newDirectory = new DirectoryInfo("SaveFiles");

        if (Directory.Exists("SaveFiles"))
        {
            foreach (FileInfo file in newDirectory.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in newDirectory.GetDirectories())
            {
                dir.Delete(true);
            }

            StartCoroutine("SaveBuffer");
        }
    }
    IEnumerator SaveBuffer()
    {
        yield return new WaitForSeconds(0.00005f);
        Directory.Delete("SaveFiles");
    }

    public void loadScene()
    {
        // FOR SCENE

        if (Directory.Exists("SaveFiles"))
        {
            XmlSerializer sceneSerializer = new XmlSerializer(typeof(GameManager.CurrentScene));
            StreamReader sceneReader = new StreamReader("SaveFiles/Scene.xml");
            GameManager.CurrentScene loadedScene = (GameManager.CurrentScene)sceneSerializer.Deserialize(sceneReader.BaseStream);
            sceneReader.Close();

            SceneManager.LoadScene(loadedScene.sceneNumber);
        }
        else
        {
            Debug.Log("No Save File to load");
            HealthManager.instance.respawn();
        }

    }
    
    public void setCheckpoint(GameObject x)
    {
        int i = 0;
        foreach(GameObject check in checkpoints)
        {
            if (x == check)
            {
                checkpointNumber = i;
            }
            i++;
        }
    }
    public void destroyCheckpoint()
    {
        if(checkpointNumber >= 0)
        {
            Destroy(checkpoints[checkpointNumber]);
        }
    }

    public void newGame()
    {
        if (Directory.Exists("SaveFiles"))
        {
            delete();
        }
        SceneManager.LoadScene(1);
    }

    public void loadGame()
    {
        if (Directory.Exists("SaveFiles"))
        {
            loadScene();
        }
    }

    public static XMLCheckpointManager instance
    {
        get;
        set;
    }
}
