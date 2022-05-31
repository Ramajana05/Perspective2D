using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour { 

    public GameObject dialogBox;
    public Text dialogText;
    public int lettersPerSeconds;

    public static DialogManager Instance { get; private set; }

    Dialog dialog;
    public int currentLine = 0;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Zeigt den Dialog, Zeile für Zeile
    /// </summary>
    /// <param name="dialog"></param>
    public void ShowDialog(Dialog dialog)
    {
        if (dialogBox.activeSelf)
        {
            HandleUpdate();
        }
        else { 
            this.dialog = dialog;
            dialogBox.SetActive(true);
            StartCoroutine(TypeDialog(dialog.Lines[0]));
        }
    }

    /// <summary>
    /// Resettet den Dialog und deaktiviert das Dialogfenster
    /// </summary>
    public void ResetDialog()
    {
        currentLine = 0;
        dialogBox.SetActive(false);
    }

    /// <summary>
    /// Prüft ob es eine weitere Zeile Code gibt, die angezeigt werden kann
    /// </summary>
    public void HandleUpdate()
    {
        ++currentLine;
        if(currentLine < dialog.Lines.Count)
        {
            StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
        }
        else
        {
             ResetDialog();
            dialogBox.SetActive(false);
        }
      
    }

    /// <summary>
    /// Füllt das Dialogfenster Buchstabe für Buchstabe
    /// </summary>
    /// <param name="dialog"></param>
    /// <returns></returns>
    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach(var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSeconds);
        }
    }

    /// <summary>
    /// Prüft ob das Dialogfenster aktiv ist
    /// </summary>
    /// <returns></returns>
    public bool CheckActiveDialogbox()
    {
        return dialogBox.activeSelf;
    }
}
