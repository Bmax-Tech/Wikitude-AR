using UnityEngine;

public enum skinselect {SkinA,SkinB,SkinC};
public enum eyesselect {Type0,Type1,Type2,Type3,Type4,Type5,Type6,Type7,Type8,Type9,Type10,Type11,Type12,Type13,Type14,Type15};
public enum lodselect {Auto=-1,Lod_0,Lod_1,Lod_2};

public class camera_cs : MonoBehaviour
{
	//dinos script
	//Vol.1
	/*
	anky_cs anky;
	brach_cs brach;
	*/
	comp_cs comp;
	/*
	dilo_cs dilo;
	dime_cs dime;
	ovi_cs ovi;
	para_cs para;
	ptera_cs ptera;
	rap_cs rap;
	rex_cs rex;
	spino_cs spino;
	steg_cs steg;
	tric_cs tric;
	//Vol.2
	arge_cs arge;
	pachy_cs pachy;
	igua_cs igua;
	styra_cs styra;
	kent_cs kent;
	bary_cs bary;
	carn_cs carn;
	quet_cs quet;
	galli_cs galli;
	proto_cs proto;
	dimo_cs dimo;
	oura_cs oura;
	troo_cs troo;
	*/
	int cammode=0; //camera mode : 0=chase cam, 1=free cam, 2=locked cam
	bool wireframe,gui; //wireframe mode, hide/show gui
	float yadd,distance,zoom, x, y; //camera position
	Vector3 distanceVector; //distance to the target
	float timer, frame, fps; //variables used for fps counter

	[SerializeField] GameObject[] target; //target of the camera
	string fullname,infos; //full dino name and mesh infos
	int lod, body, eyes, dino; //store dino infos
	float scale;
	bool load=false, AI=false;

	//Find all dinos prefab in scene
	void Start ()
	{
		if(target.Length==0)
		target = GameObject.FindGameObjectsWithTag("Dino");
	}


	//***************************************************************************************
	//Camera behavior
	void Update ()
	{
		if(target.Length>0)
		{
			//Very simple Fps counter 
			frame += 1.0f;
			timer += Time.deltaTime;
			if(timer>1.0f) { fps = frame; timer = 0.0F; frame = 0.0F; }

			// Zoom or dezoom using mouse wheel
			if ( Input.GetAxis("Mouse ScrollWheel") > 0.0f)
			{
				if(zoom<distance)zoom += 2.0f;
				distanceVector = new Vector3(0.0f,yadd*target[dino].transform.localScale.x,(-distance+zoom)*target[dino].transform.localScale.x);
			}
			else if ( Input.GetAxis("Mouse ScrollWheel") < 0.0f)
			{
				if(zoom<100)zoom -= 2.0f;
				distanceVector = new Vector3(0.0f,yadd*target[dino].transform.localScale.x,(-distance+zoom)*target[dino].transform.localScale.x);
			}
			else distanceVector = new Vector3(0.0f,yadd*target[dino].transform.localScale.x,(-distance+zoom)*target[dino].transform.localScale.x);

			if(cammode ==0) // free cam
			{
				// rotate the camera when the middle mouse button is pressed
				if(Input.GetKey(KeyCode.Mouse2))
				{
					x += Input.GetAxis("Mouse X") * 5.0F;
					y += -Input.GetAxis("Mouse Y")* 5.0F;
				}
				
				Quaternion rotation = Quaternion.Euler(y,x,0.0f);
				Vector3 position = rotation * distanceVector + target[dino].transform.position;
				transform.rotation = rotation;
				transform.position = position;
			}
			else if(cammode ==1) // chase cam
			{
				// rotate the camera when the middle mouse button is pressed
				if(Input.GetKey(KeyCode.Mouse2)) y -= Input.GetAxis("Mouse Y")*5.0F;
				
				if(Input.GetKey(KeyCode.Mouse2)) x += Input.GetAxis("Mouse X")*10.0f;
				// reset camera rotation
				else x = Mathf.LerpAngle(x,target[dino].transform.eulerAngles.y,0.05f);
				
				
				Quaternion rotation = Quaternion.Euler(target[dino].transform.eulerAngles.z+y, x, 0.0f);
				Vector3 position = rotation * distanceVector + target[dino].transform.position;
				transform.rotation = rotation;
				transform.position = position;
			}
			else // locked cam
				transform.LookAt(target[dino].transform);
		}
	}
	

	//***************************************************************************************
	//Gui
	void OnGUI ()
	{
		if(target.Length>0)
		{
			//Show or hide GUI
			if(Input.mousePosition.x<5) gui=true;
			else if(Input.mousePosition.x>240) gui=false;

			//Get dino script
			switch (target[dino].transform.GetChild(0).name)
			{
			//Vol.1
			case "Comp": if(!load) { comp=target[dino].GetComponent<comp_cs>(); fullname = "Compsognathus"; yadd=2; distance=5;
				infos=comp.infos; AI=comp.AI; body=comp.BodySkin.GetHashCode(); eyes=comp.EyesSkin.GetHashCode(); lod=comp.LodLevel.GetHashCode(); scale=comp.scale; load=true;}
				else if(gui) { infos = comp.infos; comp.AI=AI; comp.BodySkin = (skinselect) body; comp.EyesSkin = (eyesselect) eyes; comp.LodLevel = (lodselect) lod; comp.scale=scale;}
				break;
			/*
			case "Rap": if(!load) { rap=target[dino].GetComponent<rap_cs>(); fullname = "Velociraptor"; yadd=7; distance=10;
				infos=rap.infos; AI=rap.AI; body=rap.BodySkin.GetHashCode(); eyes=rap.EyesSkin.GetHashCode(); lod=rap.LodLevel.GetHashCode(); scale=rap.scale;load=true;}
				else if(gui) { infos = rap.infos; rap.AI=AI; rap.BodySkin = (skinselect) body; rap.EyesSkin = (eyesselect) eyes; rap.LodLevel = (lodselect) lod; rap.scale=scale;}
				break;
			case "Ovi": if(!load) { ovi=target[dino].GetComponent<ovi_cs>(); fullname = "Oviraptor"; yadd=7; distance=10; 
				infos=ovi.infos; AI=ovi.AI; body=ovi.BodySkin.GetHashCode(); eyes=ovi.EyesSkin.GetHashCode(); lod=ovi.LodLevel.GetHashCode(); scale=ovi.scale; load=true;}
				else if(gui) { infos = ovi.infos; ovi.AI=AI; ovi.BodySkin = (skinselect) body; ovi.EyesSkin = (eyesselect) eyes; ovi.LodLevel = (lodselect) lod; ovi.scale=scale;}
				break;
			case "Dilo": if(!load) { dilo=target[dino].GetComponent<dilo_cs>(); fullname = "Dilophosaurus"; yadd=7; distance=10;
				infos=dilo.infos; AI=dilo.AI; body=dilo.BodySkin.GetHashCode(); eyes=dilo.EyesSkin.GetHashCode(); lod=dilo.LodLevel.GetHashCode(); scale=dilo.scale; load=true;}
				else if(gui) { infos = dilo.infos; dilo.AI=AI; dilo.BodySkin = (skinselect) body; dilo.EyesSkin = (eyesselect) eyes; dilo.LodLevel = (lodselect) lod; dilo.scale=scale;}
				break;
			case "Rex": if(!load) { rex=target[dino].GetComponent<rex_cs>(); fullname = "Tyrannosaurus Rex"; yadd=20; distance=35;
				infos=rex.infos; AI=rex.AI; body=rex.BodySkin.GetHashCode(); eyes=rex.EyesSkin.GetHashCode(); lod=rex.LodLevel.GetHashCode(); scale=rex.scale;load=true;}
				else if(gui) { infos = rex.infos; rex.AI=AI; rex.BodySkin = (skinselect) body; rex.EyesSkin = (eyesselect) eyes; rex.LodLevel = (lodselect) lod; rex.scale=scale;}
				break;
			case "Spino": if(!load) { spino=target[dino].GetComponent<spino_cs>(); fullname = "Spinosaurus"; yadd=20; distance=35;
				infos=spino.infos; AI=spino.AI; body=spino.BodySkin.GetHashCode(); eyes=spino.EyesSkin.GetHashCode(); lod=spino.LodLevel.GetHashCode(); scale=spino.scale; load=true;}
				else { infos = spino.infos; spino.AI=AI; spino.BodySkin = (skinselect) body; spino.EyesSkin = (eyesselect) eyes; spino.LodLevel = (lodselect) lod; spino.scale=scale;}
				break;
			case "Tric": if(!load) { tric=target[dino].GetComponent<tric_cs>(); fullname = "Triceratops"; yadd=15; distance=20;
				infos=tric.infos; AI=tric.AI; body=tric.BodySkin.GetHashCode(); eyes=tric.EyesSkin.GetHashCode(); lod=tric.LodLevel.GetHashCode(); scale=tric.scale; load=true;}
				else if(gui) { infos = tric.infos; tric.AI=AI; tric.BodySkin = (skinselect) body; tric.EyesSkin = (eyesselect) eyes; tric.LodLevel = (lodselect) lod; tric.scale=scale;}
				break;
			case "Brach": if(!load) { brach=target[dino].GetComponent<brach_cs>(); fullname = "Brachiosaurus"; yadd=20; distance=40;
				infos=brach.infos; AI=brach.AI; body=brach.BodySkin.GetHashCode(); eyes=brach.EyesSkin.GetHashCode(); lod=brach.LodLevel.GetHashCode(); scale=brach.scale; load=true;}
				else if(gui) { infos = brach.infos; brach.AI=AI; brach.BodySkin = (skinselect) body; brach.EyesSkin = (eyesselect) eyes; brach.LodLevel = (lodselect) lod; brach.scale=scale;}
				break;
			case "Dime": if(!load) { dime=target[dino].GetComponent<dime_cs>(); fullname = "Dimetrodon"; yadd=10; distance=15;
				infos=dime.infos; AI=dime.AI; body=dime.BodySkin.GetHashCode(); eyes=dime.EyesSkin.GetHashCode(); lod=dime.LodLevel.GetHashCode(); scale=dime.scale; load=true;}
				else if(gui) { infos = dime.infos; dime.AI=AI; dime.BodySkin = (skinselect) body; dime.EyesSkin = (eyesselect) eyes; dime.LodLevel = (lodselect) lod; dime.scale=scale;}
				break;
			case "Para": if(!load) { para=target[dino].GetComponent<para_cs>(); fullname = "Parasaurolophus"; yadd=15; distance=20;
				infos=para.infos; AI=para.AI; body=para.BodySkin.GetHashCode(); eyes=para.EyesSkin.GetHashCode(); lod=para.LodLevel.GetHashCode(); scale=para.scale; load=true;}
				else if(gui) { infos = para.infos; para.AI=AI; para.BodySkin = (skinselect) body; para.EyesSkin = (eyesselect) eyes; para.LodLevel = (lodselect) lod; para.scale=scale;}
				break;
			case "Steg": if(!load) { steg=target[dino].GetComponent<steg_cs>(); fullname = "Stegosaurus"; yadd=10; distance=20;
				infos=steg.infos; AI=steg.AI; body=steg.BodySkin.GetHashCode(); eyes=steg.EyesSkin.GetHashCode(); lod=steg.LodLevel.GetHashCode(); scale=steg.scale; load=true;}
				else if(gui) { infos = steg.infos; steg.AI=AI; steg.BodySkin = (skinselect) body; steg.EyesSkin = (eyesselect) eyes; steg.LodLevel = (lodselect) lod; steg.scale=scale;}
				break;
			case "Anky": if(!load){ anky=target[dino].GetComponent<anky_cs>(); fullname = "Ankylosaurus"; yadd=10; distance=20;
				infos=anky.infos; AI=anky.AI; body=anky.BodySkin.GetHashCode(); eyes=anky.EyesSkin.GetHashCode(); lod=anky.LodLevel.GetHashCode(); scale=anky.scale; load=true;}
				else if(gui) { infos = anky.infos; anky.AI=AI; anky.BodySkin = (skinselect) body; anky.EyesSkin = (eyesselect) eyes; anky.LodLevel = (lodselect) lod; anky.scale=scale;}
				break;
			case "Ptera": if(!load) { ptera=target[dino].GetComponent<ptera_cs>(); fullname = "Pteranodon"; yadd=10; distance=15;
					infos=ptera.infos; AI=ptera.AI; body=ptera.BodySkin.GetHashCode(); eyes=ptera.EyesSkin.GetHashCode(); lod=ptera.LodLevel.GetHashCode(); scale=ptera.scale; load=true;}
				else if(gui) { infos = ptera.infos; ptera.AI=AI; ptera.BodySkin = (skinselect) body; ptera.EyesSkin = (eyesselect) eyes; ptera.LodLevel = (lodselect) lod; ptera.scale=scale;}
				break;
			//Vol.2
			case "Pachy": if(!load) { pachy=target[dino].GetComponent<pachy_cs>(); fullname = "Pachycephalosaurus"; yadd=10; distance=15;
				infos=pachy.infos; AI=pachy.AI; body=pachy.BodySkin.GetHashCode(); eyes=pachy.EyesSkin.GetHashCode(); lod=pachy.LodLevel.GetHashCode(); scale=pachy.scale; load=true;}
				else if(gui) { infos = pachy.infos; pachy.AI=AI; pachy.BodySkin = (skinselect) body; pachy.EyesSkin = (eyesselect) eyes; pachy.LodLevel = (lodselect) lod; pachy.scale=scale;}
				break;
			case "Arge": if(!load) { arge=target[dino].GetComponent<arge_cs>(); fullname = "Argentinosaurus"; yadd=20; distance=45;
				infos=arge.infos; AI=arge.AI; body=arge.BodySkin.GetHashCode(); eyes=arge.EyesSkin.GetHashCode(); lod=arge.LodLevel.GetHashCode(); scale=arge.scale; load=true;}
				else if(gui) { infos = arge.infos; arge.AI=AI; arge.BodySkin = (skinselect) body; arge.EyesSkin = (eyesselect) eyes; arge.LodLevel = (lodselect) lod; arge.scale=scale;}
				break;
			case "Igua": if(!load) { igua=target[dino].GetComponent<igua_cs>(); fullname = "Iguanodon"; yadd=10; distance=20;
					infos=igua.infos; AI=igua.AI; body=igua.BodySkin.GetHashCode(); eyes=igua.EyesSkin.GetHashCode(); lod=igua.LodLevel.GetHashCode(); scale=igua.scale; load=true;}
				else if(gui) { infos = igua.infos; igua.AI=AI; igua.BodySkin = (skinselect) body; igua.EyesSkin = (eyesselect) eyes; igua.LodLevel = (lodselect) lod; igua.scale=scale;}
				break;
			case "Styra": if(!load) { styra=target[dino].GetComponent<styra_cs>(); fullname = "Styracosaurus"; yadd=10; distance=15;
					infos=styra.infos; AI=styra.AI; body=styra.BodySkin.GetHashCode(); eyes=styra.EyesSkin.GetHashCode(); lod=styra.LodLevel.GetHashCode(); scale=styra.scale; load=true;}
				else if(gui) { infos = styra.infos; styra.AI=AI; styra.BodySkin = (skinselect) body; styra.EyesSkin = (eyesselect) eyes; styra.LodLevel = (lodselect) lod; styra.scale=scale;}
				break;
			case "Kent": if(!load) { kent=target[dino].GetComponent<kent_cs>(); fullname = "Kentrosaurus"; yadd=8; distance=15;
					infos=kent.infos; AI=kent.AI; body=kent.BodySkin.GetHashCode(); eyes=kent.EyesSkin.GetHashCode(); lod=kent.LodLevel.GetHashCode(); scale=kent.scale; load=true;}
				else if(gui) { infos = kent.infos; kent.AI=AI; kent.BodySkin = (skinselect) body; kent.EyesSkin = (eyesselect) eyes; kent.LodLevel = (lodselect) lod; kent.scale=scale;}
				break;
			case "Bary": if(!load) { bary=target[dino].GetComponent<bary_cs>(); fullname = "Baryonyx"; yadd=15; distance=30;
					infos=bary.infos; AI=bary.AI; body=bary.BodySkin.GetHashCode(); eyes=bary.EyesSkin.GetHashCode(); lod=bary.LodLevel.GetHashCode(); scale=bary.scale; load=true;}
				else if(gui) { infos = bary.infos; bary.AI=AI; bary.BodySkin = (skinselect) body; bary.EyesSkin = (eyesselect) eyes; bary.LodLevel = (lodselect) lod; bary.scale=scale;}
				break;
			case "Carn": if(!load) { carn=target[dino].GetComponent<carn_cs>(); fullname = "Carnotaurus"; yadd=15; distance=30;
					infos=carn.infos; AI=carn.AI; body=carn.BodySkin.GetHashCode(); eyes=carn.EyesSkin.GetHashCode(); lod=carn.LodLevel.GetHashCode(); scale=carn.scale;load=true;}
				else if(gui) { infos = carn.infos; carn.AI=AI; carn.BodySkin = (skinselect) body; carn.EyesSkin = (eyesselect) eyes; carn.LodLevel = (lodselect) lod; carn.scale=scale;}
				break;
			case "Quet": if(!load) { quet=target[dino].GetComponent<quet_cs>(); fullname = "Quetzalcoatlus"; yadd=15; distance=30;
					infos=quet.infos; AI=quet.AI; body=quet.BodySkin.GetHashCode(); eyes=quet.EyesSkin.GetHashCode(); lod=quet.LodLevel.GetHashCode(); scale=quet.scale; load=true;}
				else if(gui) { infos = quet.infos; quet.AI=AI; quet.BodySkin = (skinselect) body; quet.EyesSkin = (eyesselect) eyes; quet.LodLevel = (lodselect) lod; quet.scale=scale;}
				break;
			case "Galli": if(!load) { galli=target[dino].GetComponent<galli_cs>(); fullname = "Gallimimus"; yadd=7; distance=10; 
					infos=galli.infos; AI=galli.AI; body=galli.BodySkin.GetHashCode(); eyes=galli.EyesSkin.GetHashCode(); lod=galli.LodLevel.GetHashCode(); scale=galli.scale; load=true;}
				else if(gui) { infos = galli.infos; galli.AI=AI; galli.BodySkin = (skinselect) body; galli.EyesSkin = (eyesselect) eyes; galli.LodLevel = (lodselect) lod; galli.scale=scale;}
				break;
			case "Proto": if(!load) { proto=target[dino].GetComponent<proto_cs>(); fullname = "Protoceratops"; yadd=5; distance=10; 
					infos=proto.infos; AI=proto.AI; body=proto.BodySkin.GetHashCode(); eyes=proto.EyesSkin.GetHashCode(); lod=proto.LodLevel.GetHashCode(); scale=proto.scale; load=true;}
				else if(gui) { infos = proto.infos; proto.AI=AI; proto.BodySkin = (skinselect) body; proto.EyesSkin = (eyesselect) eyes; proto.LodLevel = (lodselect) lod; proto.scale=scale;}
				break;
			case "Dimo": if(!load) { dimo=target[dino].GetComponent<dimo_cs>(); fullname = "Dimorphodon"; yadd=2; distance=5;
					infos=dimo.infos; AI=dimo.AI; body=dimo.BodySkin.GetHashCode(); eyes=dimo.EyesSkin.GetHashCode(); lod=dimo.LodLevel.GetHashCode(); scale=dimo.scale; load=true;}
				else if(gui) { infos = dimo.infos; dimo.AI=AI; dimo.BodySkin = (skinselect) body; dimo.EyesSkin = (eyesselect) eyes; dimo.LodLevel = (lodselect) lod; dimo.scale=scale;}
				break;
			case "Oura": if(!load) { oura=target[dino].GetComponent<oura_cs>(); fullname = "Ouranosaurus"; yadd=10; distance=20;
					infos=oura.infos; AI=oura.AI; body=oura.BodySkin.GetHashCode(); eyes=oura.EyesSkin.GetHashCode(); lod=oura.LodLevel.GetHashCode(); scale=oura.scale; load=true;}
				else if(gui) { infos = oura.infos; oura.AI=AI; oura.BodySkin = (skinselect) body; oura.EyesSkin = (eyesselect) eyes; oura.LodLevel = (lodselect) lod; oura.scale=scale;}
				break;
			case "Troo": if(!load) { troo=target[dino].GetComponent<troo_cs>(); fullname = "Troodon"; yadd=7; distance=10; 
					infos=troo.infos; AI=troo.AI; body=troo.BodySkin.GetHashCode(); eyes=troo.EyesSkin.GetHashCode(); lod=troo.LodLevel.GetHashCode(); scale=troo.scale; load=true;}
				else if(gui) { infos = troo.infos; troo.AI=AI; troo.BodySkin = (skinselect) body; troo.EyesSkin = (eyesselect) eyes; troo.LodLevel = (lodselect) lod; troo.scale=scale;}
				break;
			*/
			}


			if(gui)
			{
				//Box
				GUI.Box (new Rect (0, 0, 240, Screen.height), fullname); GUI.color=Color.yellow;
				//Display buttons help
				GUI.Label(new Rect(5,20,Screen.width,Screen.height),"KEYS:");
				GUI.Label(new Rect(5,30,Screen.width,Screen.height),"---------------------------------------------------------");
				GUI.Label(new Rect(5,40,Screen.width,Screen.height),"Middle Mouse = Camera/Zoom");
				GUI.Label(new Rect(5,60,Screen.width,Screen.height),"Right Mouse = Spine move");
				GUI.Label(new Rect(5,80,Screen.width,Screen.height),"Left Mouse = Attack/Rise");
				GUI.Label(new Rect(5,100,Screen.width,Screen.height),"W,A,S,D = Moves");
				GUI.Label(new Rect(5,120,Screen.width,Screen.height),"LeftShift = Run/Landing/Fly Down");
				GUI.Label(new Rect(5,140,Screen.width,Screen.height),"Space = Steps/Jump/Takeoff/Fly Up");
				GUI.Label(new Rect(5,160,Screen.width,Screen.height),"Ctrl = Steps/Attack pose");
				GUI.Label(new Rect(5,180,Screen.width,Screen.height),"E = Growl");
				GUI.Label(new Rect(5,200,Screen.width,Screen.height),"Num 1-9 = Idles/Eat/Drink/Sit/Sleep/Die");

				//Screen buttons
				GUI.Label(new Rect(5,220,Screen.width,Screen.height),"SCREEN:");
				GUI.Label(new Rect(5,230,Screen.width,Screen.height),"---------------------------------------------------------");

				GUI.Label(new Rect (5,250,55,20), "Fps "+fps); //Fps counter

				if (GUI.Button (new Rect (60,250,120,20), "Fullscreen")) //Full screen
					Screen.fullScreen = !Screen.fullScreen; 

				if (cammode == 0 && GUI.Button (new Rect (60, 270, 120, 20), "Cam Free")) cammode = 1; // free cam
				else if (cammode == 1 && GUI.Button (new Rect (60, 270, 120, 20), "Cam Chase")) cammode = 2; // locked cam
				else if (cammode == 2 && GUI.Button (new Rect (60, 270, 120, 20), "Cam Lock")) cammode = 0; // chase cam 

				if (wireframe == true && GUI.Button (new Rect (60,290,120,20), "Wireframe : ON"))  wireframe = false; //Wireframe mode
				else if(wireframe == false && GUI.Button (new Rect (60,290,120,20), "Wireframe : OFF")) wireframe = true;

				if (GUI.Button (new Rect (60,310,120,20), "Reset World")) Application.LoadLevel(0); //Reset

				//Model buttons
				GUI.Label(new Rect(5,360,Screen.width,Screen.height),"MODEL:");
				GUI.Label(new Rect(5,370,Screen.width,Screen.height),"---------------------------------------------------------");

				GUI.Label(new Rect(5,390,Screen.width,Screen.height),infos); //Model infos and lod
				if (GUI.Button (new Rect (100,390,120,20), lod==-1 ? "LOD_Auto":"LOD_"+lod.ToString())) { if(lod<2)lod++; else lod=-1; }

				GUI.Label(new Rect(5,430,Screen.width,Screen.height),"Dino AI"); //AI
				if (GUI.Button (new Rect (100,430,120,20), AI.ToString()))  { if(AI)AI = false; else AI=true; }

				GUI.Label(new Rect(5,450,Screen.width,Screen.height),"Body skin"); //Body skin
				if (GUI.Button (new Rect (100,450,120,20), "Skin "+body.ToString())) { if(body<2)body++; else body=0; }
		
				GUI.Label(new Rect(5,470,Screen.width,Screen.height),"Eyes skin"); //Eyes skin
				if (GUI.Button (new Rect (100,470,120,20), "Type "+eyes.ToString())) { if(eyes<15)eyes++; else eyes=0; }
		
				GUI.Label(new Rect(5,490,Screen.width,Screen.height),"Model Scale"); //Model scale
				scale=GUI.HorizontalSlider(new Rect (100,490,120,20),scale,0.05f,0.5f);

				GUI.Label(new Rect(5,510,Screen.width,Screen.height),"Change Dino"); //Dino Select
				if ((GUI.Button (new Rect (100, 510, 120,20), target[dino].transform.GetChild(0).name))) { if(dino<target.Length-1) dino++; else dino =0; load=false; }

			}
		}
		else
		{
			GUI.color=Color.yellow;
			GUI.Box (new Rect (Screen.width/2-200,Screen.height/2, 400,25), "No dino found, please drag a dino prefab in your scene");
		}
	}

	
	//***************************************************************************************
	//wireframe mode
	void OnPreRender()
	{
		if (wireframe == true) GL.wireframe = true;
		else GL.wireframe = false;
	}
	void OnPostRender()
	{
		GL.wireframe = false;
	}
	
	
}
