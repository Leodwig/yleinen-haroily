﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundController : MonoBehaviour {
	public static GroundController instance;

	// Peruslattian prefab
	public GameObject basicGround;
	
	public int width = 10;
	public int height = 10;
	
	public float tileWidth = 0.5f;
	public float tileHeight = 0.5f;
	
	// Erikoistilejen prefabit
	public GameObject[] tiles;
	// Erikoistilejen koordinaatit
	public Vector2[] coordinates;

	private float offset;
	private float offsetAmount;
	
	// Taulukko tileistä, joka on kätketty inspectorissa (tarkoitus käsitellä koodin kautta)
	[HideInInspector]
	public GameObject[,] tilesAsArray;
	
	void Start () {
		Init();
		GenerateLevel();
	}
	
	public GameObject[] GetNeighbours(GameObject tile) {
		List<GameObject> neighbours = new List<GameObject>();

		// Etsitään tilen koordinaatit
		int x = -1;
		int y = -1;
		for (int i = 0; i < tilesAsArray.GetLength(0); i++) {
			for (int j = 0; j < tilesAsArray.GetLength(1); j++) {
				if (tilesAsArray[i,j] == tile) {
					x = i;
					y = j;
					break;
				}
			}
		}
		
		// Nyt tiedetään etsityn tilen koordinaatit, x ja y
		// Etsitään tilen "tile" naapurit ja lisätää ne neighboursiin
		// Eli: neighboursiin lisätään komennolla neighbours.Add(naapuriTile);.
		// Nykyinen tile on tilesAsArray[x,y] ja naapurit esimerkiksi tilesAsArray[x+1,y],
		// tilesAsArray[x-1,y], tilesAsArray[x,y+1] ja tilesAsArray[x,y-1]. Mutta
		// koska meillä on tämä hassu vinottaisuussysteemi.
		
		// Noniin, testaillaan!
		
		// Vasen naapuri
		if (x > 0) {
			neighbours.Add(tilesAsArray[x-1,y]);
		}
		
		// Oikea naapuri
		if (x < width - 1) {
			neighbours.Add(tilesAsArray[x+1,y]);
		}
		
		// Alanaapuri
		if (y > 0) {
			neighbours.Add(tilesAsArray[x,y-1]);
		}
		
		// Ylänaapuri
		if (y < height - 1) {
			neighbours.Add(tilesAsArray[x,y+1]);
		}
		
		// Parilliset rivit
		if (y%2==0) {
			if (x < width - 1) {
				
				// Oikea alanaaputi
				if (y > 0) {
					neighbours.Add(tilesAsArray[x+1,y-1]);
				}
				
				// Oikea ylänaapuri
				if (y < height - 1) {
					neighbours.Add(tilesAsArray[x+1,y+1]);
				}
			}
		}
		
		// Parittomat rivit
		if (y%2!=0) {
			if (x > 0) {
				
				// Vasen alanaaputi
				if (y > 0) {
					neighbours.Add(tilesAsArray[x-1,y-1]);
				}
				
				// Vasen ylänaapuri
				if (y < height - 1) {
					neighbours.Add(tilesAsArray[x-1,y+1]);
				}
			}
		}
		
		return neighbours.ToArray();
	}
	
	private void Init() {
		// Tekee tästä globaalin
		instance = this;
		
		// Asetetaan muuttujien alkuarvot
		offsetAmount = tileWidth * 0.4f;
		tilesAsArray = new GameObject[width,height];
	}
	
	private void GenerateLevel() {
		// Luo pohjatilet
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				
				// Vinoutus - katsotaan onko rivi parillinen vai pariton
				if(j%2==0) {
					offset = offsetAmount;
				} else {
					offset = 0f;
				}
				
				//Instantioi basicGround-objektin tilen koordinaatteihin (i,j)
				GameObject tile = (GameObject)Instantiate(basicGround, new Vector3((i * tileWidth) + offset, j * tileHeight, 1), Quaternion.identity);
				//Asettaa luodun tilen tämän lapseksi
				tile.transform.parent = transform;
				
				// Lisää tilen taulukkoon
				tilesAsArray[i,j] = tile;
			}
		}
		
		// Luo erikoistilet
		for (int i = 0; i < tiles.Length; i++) {
			GameObject tile = tiles[i];
			
			// Lasketaan tilen koordinaatit
			Vector3 coord = (Vector3)coordinates[i];
			
			// Vinoutus
			if(coordinates[i].y%2==0) {
				offset = offsetAmount;
			} else {
				offset = 0f;
			}
			
			// Instantioi erikoistilen koordinaatteihin
			GameObject instantiatedTile = (GameObject)Instantiate(tile, new Vector3((coord.x * tileWidth) + offset, coord.y * tileHeight, 0.9f), Quaternion.identity);
			// Asettaa luodun tilen tämän lapseksi
			instantiatedTile.transform.parent = transform;
			
			// Lisää tilen taulukkoon
			tilesAsArray[(int)coordinates[i].x,(int)coordinates[i].y] = instantiatedTile;
		}
		
		// Keskittää kentän
		transform.position = new Vector3(-(width*tileWidth)/2,-(height*tileHeight)/2,1);
	}
}
