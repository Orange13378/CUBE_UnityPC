using CubeMVC;
using System.Collections.Generic;
using UnityEngine;

public class PliteScripts : MonoBehaviour
{
    #region Singleton

	public static PliteScripts instance;

	void Awake ()
	{
		instance = this;
	}

    #endregion

    [SerializeField]
    private ContextProvider _contextProvider;
    private DialogModel _dialogModel;

    public List<int> pressedID = new ();
    public List<int> pressedElectroID = new ();

    [System.NonSerialized]
    public static bool correctCode, correctCode1,
    correctElectroCode, id1, id2, id3, id4, nextStage, nextStage1, nextStage2;

    public AudioClip buttonSound;
    private AudioSource audioSource;

    public GameObject electro;

    private void Start()
    {
        _dialogModel = _contextProvider.GetContext().DialogModel;

        audioSource = GetComponent<AudioSource>();
        nextStage1 = false;
        nextStage = true;
        nextStage2 = false;
    }

    public void GetCode(PliteID pliteID)
    {
        pressedID.Add(pliteID.id);
        if (pressedID.Exists(x => x == pliteID.id)) pliteID.counts++;

        if (nextStage)
        {
            if (pliteID.id == 37 && pliteID.counts >= 2) id1 = true;
            else if (pliteID.id == 37 && pliteID.counts < 2) id1 = false;


            if(id1 && !pressedID.Exists(x => x == 36))
            {
                id1 = false; id2 = false;

                _dialogModel.OnDialogStart.Invoke("Кажется что-то приближается");

                correctCode = true;
                nextStage = false;
                nextStage1 = true;
            }
        }

        if (nextStage1)
        {
        
            if (pliteID.id == 8 && pliteID.counts >= 2) id1 = true;
            else if (pliteID.id == 8 && pliteID.counts < 2) id1 = false;

            if (pliteID.id == 7 && pliteID.counts >= 2) id2 = true;
            else if (pliteID.id == 7 && pliteID.counts < 2) id2 = false;


            if(id1 && id2 && !pressedID.Exists(x => x == 6) && !pressedID.Exists(x => x == 9) && !pressedID.Exists(x => x == 10) && !pressedID.Exists(x => x == 11))
            {
                id1 = false; id2 = false;

                _dialogModel.OnDialogStart.Invoke("Что это?");

                correctCode1 = true;
                nextStage = false;
                if (nextStage1) 
                    electro.SetActive(true);
                nextStage1 = false;
                nextStage = false;
                nextStage2 = true;
            }
        }

        audioSource.clip = buttonSound;
        audioSource.Play();
    }

    public void OutCode(PliteID pliteID)
    {
        if (pressedID.Exists(x => x == pliteID.id)) pliteID.counts--;
        pressedID.Remove(pliteID.id);

        if (nextStage)
        {
            if (pliteID.id == 37 && pliteID.counts >= 2) id1 = true;
            else if (pliteID.id == 37 && pliteID.counts < 2) id1 = false;


            if((id1) && !pressedID.Exists(x => x == 36))
            {
                id1 = false; id2 = false;

                _dialogModel.OnDialogStart.Invoke("Кажется я что-то слышу");

                correctCode = true;
                nextStage = false;
                nextStage1 = true;
            }
        }

        if (nextStage1)
        {
        
            if (pliteID.id == 8 && pliteID.counts >= 2) id1 = true;
            else if (pliteID.id == 8 && pliteID.counts < 2) id1 = false;

            if (pliteID.id == 7 && pliteID.counts >= 2) id2 = true;
            else if (pliteID.id == 7 && pliteID.counts < 2) id2 = false;


            if((id1) && (id2) && !pressedID.Exists(x => x == 6) && !pressedID.Exists(x => x == 9) && !pressedID.Exists(x => x == 10) && !pressedID.Exists(x => x == 11))
            {
                id1 = false; id2 = false;

                _dialogModel.OnDialogStart.Invoke("Что это?");

                correctCode1 = true;
                if(nextStage1) electro.SetActive(true);
                nextStage1 = false;
                nextStage = false;
                nextStage2 = true;
            }
        }
        audioSource.clip = buttonSound;
        audioSource.Play();
    }

    public void GetCodeElectro(PliteID pliteID)
    {
        pressedElectroID.Add(pliteID.id);
        if (pressedElectroID.Exists(x => x == pliteID.id)) pliteID.electroCounts++;

        if(nextStage2)
        {
            if (pliteID.id == 10 && pliteID.electroCounts >= 2) id4 = true;
            else if (pliteID.id == 10 && pliteID.electroCounts < 2) id4 = false;

            if((id4) && pressedElectroID.Exists(x => x == 11) && !pressedElectroID.Exists(x => x == 6) && !pressedElectroID.Exists(x => x == 7) && !pressedElectroID.Exists(x => x == 8) && !pressedElectroID.Exists(x => x == 9))
            {
                id3 = false; id4 = false;
                correctElectroCode = true;

                _dialogModel.OnDialogStart.Invoke("Что-то движется сюда");

                nextStage2 = false;
                //pliteID.OnEnable();
            }
        }

        audioSource.clip = buttonSound;
        audioSource.Play();
    }

    public void OutCodeElectro(PliteID pliteID)
    {
        if (pressedElectroID.Exists(x => x == pliteID.id)) pliteID.electroCounts--;
        pressedElectroID.Remove(pliteID.id);

        if(nextStage2)
        {
            if (pliteID.id == 10 && pliteID.electroCounts >= 2) id4 = true;
            else if (pliteID.id == 10 && pliteID.electroCounts < 2) id4 = false;

            if((id4) && pressedElectroID.Exists(x => x == 11) && !pressedElectroID.Exists(x => x == 6) && !pressedElectroID.Exists(x => x == 7) && !pressedElectroID.Exists(x => x == 8) && !pressedElectroID.Exists(x => x == 9))
            {
                id3 = false; id4 = false;
                correctElectroCode = true;

                _dialogModel.OnDialogStart.Invoke("Что-то движется сюда");
                nextStage2 = false;
                //pliteID.OnEnable();
            }
        }

        audioSource.clip = buttonSound;
        audioSource.Play();
    }

}
