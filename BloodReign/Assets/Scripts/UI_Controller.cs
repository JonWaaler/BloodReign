using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
	public GameObject playerTXT_01, playerTXT_02, playerTXT_03, playerTXT_04;
	public GameObject pressA_01, pressA_02, pressA_03, pressA_04;
	public GameObject playerSelect, mainMenu;
	public GameObject grappleIMG_01, orbIMG_01, rollIMG_01;
	public GameObject grappleIMG_02, orbIMG_02, rollIMG_02;
	public GameObject grappleIMG_03, orbIMG_03, rollIMG_03;
	public GameObject grappleIMG_04, orbIMG_04, rollIMG_04;
	public GameObject pistolIMG_01, sniperIMG_01, shotgunIMG_01, rocketIMG_01;
	public GameObject pistolIMG_02, sniperIMG_02, shotgunIMG_02, rocketIMG_02;
	public GameObject pistolIMG_03, sniperIMG_03, shotgunIMG_03, rocketIMG_03;
	public GameObject pistolIMG_04, sniperIMG_04, shotgunIMG_04, rocketIMG_04;
	public GameObject charPointers_01, gunPointers_01;
	public GameObject charPointers_02, gunPointers_02;
	public GameObject charPointers_03, gunPointers_03;
	public GameObject charPointers_04, gunPointers_04;
	public PlayerSettings playerSettings;
 
	private bool CharacterSelect_01, CharacterSelect_02, CharacterSelect_03, CharacterSelect_04;
	private bool GunSelect_01, GunSelect_02, GunSelect_03, GunSelect_04;
	private int charcounter_01 = 0;
	private int charcounter_02 = 0;
	private int charcounter_03 = 0;
	private int charcounter_04 = 0;
	private int guncounter_01 = 0;
	private int guncounter_02 = 0;
	private int guncounter_03 = 0;
	private int guncounter_04 = 0;
	private bool axisInUse_01 = false;
	private bool axisInUse_02 = false;
	private bool axisInUse_03 = false;
	private bool axisInUse_04 = false;
	private bool p2weapon = false;
	private bool p3weapon = false;
	private bool p4weapon = false;

	List<int> controllers = new List<int>();

	void Awake()
	{
		playerTXT_01.SetActive(false);
		playerTXT_02.SetActive(false);
		playerTXT_03.SetActive(false);
		playerTXT_04.SetActive(false);
		pressA_01.SetActive(true);
		pressA_02.SetActive(true);
		pressA_03.SetActive(true);
		pressA_04.SetActive(true);
		CharacterSelect_01 = false;
		CharacterSelect_02 = false;
		CharacterSelect_03 = false;
		CharacterSelect_04 = false;
		GunSelect_01 = false;
		GunSelect_02 = false;
		GunSelect_03 = false;
		GunSelect_04 = false;
		grappleIMG_01.SetActive(false);
        orbIMG_01.SetActive(false);
        rollIMG_01.SetActive(false);
		grappleIMG_02.SetActive(false);
        orbIMG_02.SetActive(false);
        rollIMG_02.SetActive(false);
		grappleIMG_03.SetActive(false);
        orbIMG_03.SetActive(false);
        rollIMG_03.SetActive(false);
		grappleIMG_04.SetActive(false);
        orbIMG_04.SetActive(false);
        rollIMG_04.SetActive(false);
		pistolIMG_01.SetActive(false);
		pistolIMG_02.SetActive(false);
		pistolIMG_03.SetActive(false);
		pistolIMG_04.SetActive(false);
		sniperIMG_01.SetActive(false);
		sniperIMG_02.SetActive(false);
		sniperIMG_03.SetActive(false);
		sniperIMG_04.SetActive(false);
		shotgunIMG_01.SetActive(false);
		shotgunIMG_02.SetActive(false);
		shotgunIMG_03.SetActive(false);
		shotgunIMG_04.SetActive(false);
		rocketIMG_01.SetActive(false);
		rocketIMG_02.SetActive(false);
		rocketIMG_03.SetActive(false);
		rocketIMG_04.SetActive(false);
		charPointers_01.SetActive(false);
		charPointers_02.SetActive(false);
		charPointers_03.SetActive(false);
		charPointers_04.SetActive(false);
		gunPointers_01.SetActive(false);
		gunPointers_02.SetActive(false);
		gunPointers_03.SetActive(false);
		gunPointers_04.SetActive(false);
	}

	void Update()
	{
		// ---- Gun Selection ---- //
		if (GunSelect_01)
		{
			pistolIMG_01.SetActive(false);
			sniperIMG_01.SetActive(false);
			shotgunIMG_01.SetActive(false);
			rocketIMG_01.SetActive(false);
			gunPointers_01.SetActive(true);

			if (Input.GetAxisRaw("H_LStick1") == 1)
			{
				if (axisInUse_01 == false)
				{
					guncounter_01++;
					axisInUse_01 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick1") == -1)
			{
				if (axisInUse_01 == false)
				{
					guncounter_01--;
					axisInUse_01 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick1") == 0)
				axisInUse_01 = false;

			guncounter_01 = (guncounter_01 > 2) ? 0 : guncounter_01;
			guncounter_01 = (guncounter_01 < 0) ? 2 : guncounter_01;

			if (guncounter_01 == 0)
				pistolIMG_01.SetActive(true);
			else if (guncounter_01 == 1)
				sniperIMG_01.SetActive(true);
			else if (guncounter_01 == 2)
				shotgunIMG_01.SetActive(true);
			else if (guncounter_01 == 3)
				rocketIMG_01.SetActive(true);
			
			if (Input.GetKeyDown("joystick 1 button 0") && (p2weapon || p3weapon || p3weapon))
			{
				GunSelect_01 = false;
				gunPointers_01.SetActive(false);

				// ---- Update Current Settings ---- //
				playerSettings.playerActive_01 = playerTXT_01.activeSelf;
				playerSettings.playerActive_02 = playerTXT_02.activeSelf;
				playerSettings.playerActive_03 = playerTXT_03.activeSelf;
				playerSettings.playerActive_04 = playerTXT_04.activeSelf;

				playerSettings.characterSelection_01 = charcounter_01;
				playerSettings.characterSelection_02 = charcounter_02;
				playerSettings.characterSelection_03 = charcounter_03;
				playerSettings.characterSelection_04 = charcounter_04;

				playerSettings.gunSelection_01 = guncounter_01;
				playerSettings.gunSelection_02 = guncounter_02;
				playerSettings.gunSelection_03 = guncounter_03;
				playerSettings.gunSelection_04 = guncounter_04;

				SceneManager.LoadScene(1);
			}
		}
		if (GunSelect_02)
		{
			pistolIMG_02.SetActive(false);
			sniperIMG_02.SetActive(false);
			shotgunIMG_02.SetActive(false);
			rocketIMG_02.SetActive(false);
			gunPointers_02.SetActive(true);

			if (Input.GetAxisRaw("H_LStick2") == 1)
			{
				if (axisInUse_02 == false)
				{
					guncounter_02++;
					axisInUse_02 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick2") == -1)
			{
				if (axisInUse_02 == false)
				{
					guncounter_02--;
					axisInUse_02 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick2") == 0)
				axisInUse_02 = false;

			guncounter_02 = (guncounter_02 > 2) ? 0 : guncounter_02;
			guncounter_02 = (guncounter_02 < 0) ? 2 : guncounter_02;

			if (guncounter_02 == 0)
				pistolIMG_02.SetActive(true);
			else if (guncounter_02 == 1)
				sniperIMG_02.SetActive(true);
			else if (guncounter_02 == 2)
				shotgunIMG_02.SetActive(true);
			else if (guncounter_02 == 3)
				rocketIMG_02.SetActive(true);
			
			if (Input.GetKeyDown("joystick 2 button 0"))
			{
				GunSelect_02 = false;
				p2weapon = true;
				gunPointers_02.SetActive(false);
			}
		}
		if (GunSelect_03)
		{
			pistolIMG_03.SetActive(false);
			sniperIMG_03.SetActive(false);
			shotgunIMG_03.SetActive(false);
			rocketIMG_03.SetActive(false);
			gunPointers_03.SetActive(true);

			if (Input.GetAxisRaw("H_LStick3") == 1)
			{
				if (axisInUse_03 == false)
				{
					guncounter_03++;
					axisInUse_03 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick3") == -1)
			{
				if (axisInUse_03 == false)
				{
					guncounter_03--;
					axisInUse_03 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick3") == 0)
				axisInUse_03 = false;

			guncounter_03 = (guncounter_03 > 2) ? 0 : guncounter_03;
			guncounter_03 = (guncounter_03 < 0) ? 2 : guncounter_03;

			if (guncounter_03 == 0)
				pistolIMG_03.SetActive(true);
			else if (guncounter_03 == 1)
				sniperIMG_03.SetActive(true);
			else if (guncounter_03 == 2)
				shotgunIMG_03.SetActive(true);
			else if (guncounter_03 == 3)
				rocketIMG_03.SetActive(true);

			if (Input.GetKeyDown("joystick 3 button 0"))
			{
				GunSelect_03 = false;
				p3weapon = true;
				gunPointers_03.SetActive(false);
			}
		}
		if (GunSelect_04)
		{
			pistolIMG_04.SetActive(false);
			sniperIMG_04.SetActive(false);
			shotgunIMG_04.SetActive(false);
			rocketIMG_04.SetActive(false);
			gunPointers_04.SetActive(true);

			if (Input.GetAxisRaw("H_LStick4") == 1)
			{
				if (axisInUse_04 == false)
				{
					guncounter_04++;
					axisInUse_04 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick4") == -1)
			{
				if (axisInUse_04 == false)
				{
					guncounter_04--;
					axisInUse_04 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick4") == 0)
				axisInUse_04 = false;

			guncounter_04 = (guncounter_04 > 2) ? 0 : guncounter_04;
			guncounter_04 = (guncounter_04 < 0) ? 2 : guncounter_04;

			if (guncounter_04 == 0)
				pistolIMG_04.SetActive(true);
			else if (guncounter_04 == 1)
				sniperIMG_04.SetActive(true);
			else if (guncounter_04 == 2)
				shotgunIMG_04.SetActive(true);
			else if (guncounter_04 == 3)
				rocketIMG_04.SetActive(true);

			if (Input.GetKeyDown("joystick 4 button 0"))
			{
				GunSelect_04 = false;
				p4weapon = true;
				gunPointers_04.SetActive(false);
			}
		}

		// ---- Character Selection ---- //
		if (CharacterSelect_01)
		{
			grappleIMG_01.SetActive(false);
			orbIMG_01.SetActive(false);
			rollIMG_01.SetActive(false);
			charPointers_01.SetActive(true);

			if (Input.GetAxisRaw("H_LStick1") == 1)
			{
				if (axisInUse_01 == false)
				{
					charcounter_01++;
					axisInUse_01 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick1") == -1)
			{
				if (axisInUse_01 == false)
				{
					charcounter_01--;
					axisInUse_01 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick1") == 0)
				axisInUse_01 = false;

			charcounter_01 = (charcounter_01 > 2) ? 0 : charcounter_01;
			charcounter_01 = (charcounter_01 < 0) ? 2 : charcounter_01;

			if (charcounter_01 == 0)
				grappleIMG_01.SetActive(true);
			else if (charcounter_01 == 1)
				orbIMG_01.SetActive(true);
			else if (charcounter_01 == 2)
				rollIMG_01.SetActive(true);
			
			if (Input.GetKeyDown("joystick 1 button 0"))
			{
				CharacterSelect_01 = false;
				GunSelect_01 = true;
				charPointers_01.SetActive(false);
			}
		}
		if (CharacterSelect_02)
		{
			grappleIMG_02.SetActive(false);
			orbIMG_02.SetActive(false);
			rollIMG_02.SetActive(false);
			charPointers_02.SetActive(true);

			if (Input.GetAxisRaw("H_LStick2") == 1)
			{
				if (axisInUse_02 == false)
				{
					charcounter_02++;
					axisInUse_02 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick2") == -1)
			{
				if (axisInUse_02 == false)
				{
					charcounter_02--;
					axisInUse_02 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick2") == 0)
				axisInUse_02 = false;

			charcounter_02 = (charcounter_02 > 2) ? 0 : charcounter_02;
			charcounter_02 = (charcounter_02 < 0) ? 2 : charcounter_02;

			if (charcounter_02 == 0)
				grappleIMG_02.SetActive(true);
			else if (charcounter_02 == 1)
				orbIMG_02.SetActive(true);
			else if (charcounter_02 == 2)
				rollIMG_02.SetActive(true);

			if (Input.GetKeyDown("joystick 2 button 0"))
			{
				CharacterSelect_02 = false;
				GunSelect_02 = true;
				charPointers_02.SetActive(false);
			}
		}
		if (CharacterSelect_03)
		{
			grappleIMG_03.SetActive(false);
			orbIMG_03.SetActive(false);
			rollIMG_03.SetActive(false);
			charPointers_03.SetActive(true);

			if (Input.GetAxisRaw("H_LStick3") == 1)
			{
				if (axisInUse_03 == false)
				{
					charcounter_03++;
					axisInUse_03 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick3") == -1)
			{
				if (axisInUse_03 == false)
				{
					charcounter_03--;
					axisInUse_03 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick3") == 0)
				axisInUse_03 = false;

			charcounter_03 = (charcounter_03 > 2) ? 0 : charcounter_03;
			charcounter_03 = (charcounter_03 < 0) ? 2 : charcounter_03;

			if (charcounter_03 == 0)
				grappleIMG_03.SetActive(true);
			else if (charcounter_03 == 1)
				orbIMG_03.SetActive(true);
			else if (charcounter_03 == 2)
				rollIMG_03.SetActive(true);

			if (Input.GetKeyDown("joystick 3 button 0"))
			{
				CharacterSelect_03 = false;
				GunSelect_03 = true;
				charPointers_03.SetActive(false);
			}
		}
		if (CharacterSelect_04)
		{
			grappleIMG_04.SetActive(false);
			orbIMG_04.SetActive(false);
			rollIMG_04.SetActive(false);
			charPointers_04.SetActive(true);

			if (Input.GetAxisRaw("H_LStick4") == 1)
			{
				if (axisInUse_04 == false)
				{
					charcounter_04++;
					axisInUse_04 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick4") == -1)
			{
				if (axisInUse_04 == false)
				{
					charcounter_04--;
					axisInUse_04 = true;
				}
			}
			if (Input.GetAxisRaw("H_LStick4") == 0)
				axisInUse_04 = false;

			charcounter_04 = (charcounter_04 > 2) ? 0 : charcounter_04;
			charcounter_04 = (charcounter_04 < 0) ? 2 : charcounter_04;

			if (charcounter_04 == 0)
				grappleIMG_04.SetActive(true);
			else if (charcounter_04 == 1)
				orbIMG_04.SetActive(true);
			else if (charcounter_04 == 2)
				rollIMG_04.SetActive(true);

			if (Input.GetKeyDown("joystick 4 button 0"))
			{
				CharacterSelect_04 = false;
				GunSelect_04 = true;
				charPointers_04.SetActive(false);
			}
		}
		

		// ---- Add Players ---- //
		if (playerSelect.activeSelf && !(mainMenu.activeSelf))
		{
			for (int i = 1; i <= 4; i++)
			{
				if (Input.GetKeyDown("joystick " + i + " button 0"))
				{
					if (!controllers.Contains(i))
					{
						controllers.Add(i);
						if (i == 1)
						{
							playerTXT_01.SetActive(true);
							pressA_01.SetActive(false);
							CharacterSelect_01 = true;
						}
						if (i == 2)
						{
							playerTXT_02.SetActive(true);
							pressA_02.SetActive(false);
							CharacterSelect_02 = true;
						}
						if (i == 3)
						{
							playerTXT_03.SetActive(true);
							pressA_03.SetActive(false);
							CharacterSelect_03 = true;
						}
						if (i == 4)
						{
							playerTXT_04.SetActive(true);
							pressA_04.SetActive(false);
							CharacterSelect_04 = true;
						}
					}
				}
			}
		}
	}
}
