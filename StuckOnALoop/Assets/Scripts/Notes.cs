using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;


public class Notes : MonoBehaviour
{
    string NotePath = "Assets/Resources/Notes.txt";

    List<string> NoteList;
    // Start is called before the first frame update
    void Start()
    {
        NoteList = new List<string>();
        
    }

    
    public bool isNotesFocused()
    {
        InputField[] noteFields = transform.GetComponent<PlayerControl>().NotesPanel.GetComponentsInChildren<InputField>();

        foreach (InputField inf in noteFields)
        {
            if(inf.isFocused == true)
            {
                return true;
            }
        }
        return false;
    }

    void UpdateList()
    {
        InputField[] noteFields = transform.GetComponent<PlayerControl>().NotesPanel.GetComponentsInChildren<InputField>();
        NoteList.Clear();
        for (int i = 0; i < noteFields.GetLength(0); i++)
        {
            NoteList.Add(noteFields[i].text);            
        }
    }

    void UpdateNotes()
    {
        InputField[] noteFields = transform.GetComponent<PlayerControl>().NotesPanel.GetComponentsInChildren<InputField>();

        for (int i = 0; i < noteFields.GetLength(0); i++)
        {
            noteFields[i].text = NoteList[i];
            
        }
    }

    public void SaveNotes()
    {
        UpdateList();
        string noteJson = JsonConvert.SerializeObject(NoteList, Formatting.Indented);

        StreamWriter sw = new StreamWriter(NotePath);
        sw.Write(noteJson);
        sw.Close();
    }
    public void LoadNotes()
    {
      
        if (File.Exists(NotePath))
        { 
            StreamReader sr = new StreamReader(NotePath);
            string noteJson = sr.ReadToEnd();
            NoteList = JsonConvert.DeserializeObject<List<string>>(noteJson);
            sr.Close();
            UpdateNotes();
        }

    }
}
