 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour {

	public bool bl_estaDentroDoCarro;//Esta variavel sera responsavel por informar se o player esta no carro
	public GameObject cam_DoPlayer;//Esta variavel armazena a camera do player
	public GameObject cam_DoCarro;//Esta variavel armazena a camera do carro
	public KD_CarControler script_DoCarro;//Esta variavel armazena o script do carro
	public MovimentosHumanoid script_DoPlayer;//Esta variavel armazena o script do player
	public GameObject pos_FinalDoPlayer;//Esta variavel armazena posição final do player
	[Space(20)]
	public Animator anim_doPlayer;//Esta variavel armazena o anim do player
	public Animator anim_daPorta;//Esta variavel armazena o anim do porta
	public Transform addPoosition;
	[Range(0f, 20f)] public float nr_timpoParaEntrarNoCarro = 6.5f;//Esta variavel armazena compara o tempo para entrar no carro
	public KD_CarAudio carAudio;

	float timeForIn;
	int nr_inCollider;
	int set_Animations;
	int set_InCar;
	void Start () {
		
	}
	

	void Update () 
	{
		//Este if controla o player para entrar e sair do carro
		if (bl_estaDentroDoCarro == true)
		{
			timeForIn += Time.deltaTime * 2.5f;
			if (timeForIn >= nr_timpoParaEntrarNoCarro)
			{
				set_Animations = 0;
				pos_FinalDoPlayer.SetActive(true);
				script_DoPlayer.gameObject.SetActive(false);
				set_InCar = 1;
			}
            else
            {
				set_Animations = 1;
				pos_FinalDoPlayer.SetActive(false);
            }
			script_DoCarro.enabled = true;
			cam_DoCarro.SetActive(true);

			cam_DoPlayer.SetActive(false);

			carAudio.enabled = true;
		}
        else
        {
			if(set_InCar == 1)
            {
				script_DoPlayer.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				set_InCar = 0;
			}
			timeForIn = 0;
			set_Animations = 0;
			script_DoCarro.enabled = false;
			script_DoPlayer.gameObject.SetActive(true);
			pos_FinalDoPlayer.SetActive(false);
			cam_DoPlayer.SetActive(true);
			cam_DoCarro.SetActive(false);
			carAudio.enabled = false;
		}

		Quaternion quat = addPoosition.rotation;

		//Este if contro a variavel bl_estaDentroDoCarro para adicionar o valor true ou false 
		if (Input.GetKeyUp(KeyCode.T) && nr_inCollider == 1 )
		{
			entrarNoCarro();
		}
		if (Input.GetKey(KeyCode.T) && bl_estaDentroDoCarro == false)
		{
			script_DoPlayer.gameObject.transform.position = new Vector3(addPoosition.position.x, addPoosition.position.y, addPoosition.position.z);
			script_DoPlayer.gameObject.transform.rotation = quat;
		}



		anim_daPorta.SetInteger("abrirPorta", set_Animations);
		anim_doPlayer.SetInteger("entrar", set_Animations);
	}


	void entrarNoCarro()
    {
		bl_estaDentroDoCarro = !bl_estaDentroDoCarro;
    }

	void OnTriggerEnter()
    {
		nr_inCollider = 1;
    }
	void OnTriggerExit()
	{
		nr_inCollider = 0;
	}
}
