using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class GestionLadrillos{

	public static List<Ladrillo> crearListaLadrillos(){
		List<Ladrillo> listaLadrillos = new List<Ladrillo>();
		//---------PURPURA-----------------
		listaLadrillos.Add(new Ladrillo(new Vector2(350, 180), 100, 40, 0, 0));
		listaLadrillos.Add(new Ladrillo(new Vector2(650, 180), 100, 40, 1, 0));
		listaLadrillos.Add(new Ladrillo(new Vector2(150, 260), 100, 40, 2, 0));
		listaLadrillos.Add(new Ladrillo(new Vector2(350, 260), 100, 40, 3, 0));
		listaLadrillos.Add(new Ladrillo(new Vector2(650, 260), 100, 40, 4, 0));
		listaLadrillos.Add(new Ladrillo(new Vector2(850, 260), 100, 40, 5, 0));
		listaLadrillos.Add(new Ladrillo(new Vector2(500, 300), 100, 40, 6, 0));
		listaLadrillos.Add(new Ladrillo(new Vector2(400, 380), 100, 40, 7, 0));
		listaLadrillos.Add(new Ladrillo(new Vector2(600, 380), 100, 40, 8, 0));
		//-----------SALMON------------------------
		listaLadrillos.Add(new Ladrillo(new Vector2(400, 140), 100, 40, 9, 1));
		listaLadrillos.Add(new Ladrillo(new Vector2(600, 140), 100, 40, 10, 1));
		listaLadrillos.Add(new Ladrillo(new Vector2(250, 180), 100, 40, 11, 1));
		listaLadrillos.Add(new Ladrillo(new Vector2(750, 180), 100, 40, 12, 1));
		listaLadrillos.Add(new Ladrillo(new Vector2(450, 340), 100, 40, 13, 1));
		listaLadrillos.Add(new Ladrillo(new Vector2(550, 340), 100, 40, 14, 1));
		//-----------AZUL-------------------------
		listaLadrillos.Add(new Ladrillo(new Vector2(500, 140), 100, 40, 15, 2));
		listaLadrillos.Add(new Ladrillo(new Vector2(300, 220), 100, 40, 16, 2));
		listaLadrillos.Add(new Ladrillo(new Vector2(700, 220), 100, 40, 17, 2));
		listaLadrillos.Add(new Ladrillo(new Vector2(400, 300), 100, 40, 18, 2));
		listaLadrillos.Add(new Ladrillo(new Vector2(600, 300), 100, 40, 19, 2));
		listaLadrillos.Add(new Ladrillo(new Vector2(350, 340), 100, 40, 20, 2));
		listaLadrillos.Add(new Ladrillo(new Vector2(650, 340), 100, 40, 21, 2));
		listaLadrillos.Add(new Ladrillo(new Vector2(500, 380), 100, 40, 22, 2));
		//---------------VERDE---------------------
		listaLadrillos.Add(new Ladrillo(new Vector2(150, 180), 100, 40, 23, 3));
		listaLadrillos.Add(new Ladrillo(new Vector2(450, 180), 100, 40, 24, 3));
		listaLadrillos.Add(new Ladrillo(new Vector2(550, 180), 100, 40, 25, 3));
		listaLadrillos.Add(new Ladrillo(new Vector2(850, 180), 100, 40, 26, 3));
		listaLadrillos.Add(new Ladrillo(new Vector2(400, 220), 100, 40, 27, 3));
		listaLadrillos.Add(new Ladrillo(new Vector2(600, 220), 100, 40, 28, 3));
		listaLadrillos.Add(new Ladrillo(new Vector2(250, 260), 100, 40, 29, 3));
		listaLadrillos.Add(new Ladrillo(new Vector2(450, 260), 100, 40, 30, 3));
		listaLadrillos.Add(new Ladrillo(new Vector2(550, 260), 100, 40, 31, 3));
		listaLadrillos.Add(new Ladrillo(new Vector2(750, 260), 100, 40, 32, 3));
		//-----------------BLANCO------------------
		listaLadrillos.Add(new Ladrillo(new Vector2(300, 140), 100, 40, 33, 4));
		listaLadrillos.Add(new Ladrillo(new Vector2(700, 140), 100, 40, 34, 4));
		listaLadrillos.Add(new Ladrillo(new Vector2(200, 220), 100, 40, 35, 4));
		listaLadrillos.Add(new Ladrillo(new Vector2(500, 220), 100, 40, 36, 4));
		listaLadrillos.Add(new Ladrillo(new Vector2(800, 220), 100, 40, 37, 4));
		listaLadrillos.Add(new Ladrillo(new Vector2(300, 300), 100, 40, 38, 4));
		listaLadrillos.Add(new Ladrillo(new Vector2(700, 300), 100, 40, 39, 4));
		return listaLadrillos;
	}
}

public class Linea{
	public Vector2 punto1;
	public Vector2 punto2;

	public Linea(Vector2 punto1, Vector2 punto2){
		this.punto1 = punto1;
		this.punto2 = punto2;
	}
}

public class Ladrillo{
	public int id;
	public Vector2 posicion;
	public Linea lineaSuperior;
	public Linea lineaInferior;
	public Linea lineaIzda;
	public Linea lineaDcha;
	public bool haSidoColisionado;
	public int color;
	public Ladrillo(Vector2 posicion, float ancho, float alto, int id, int color){
		this.posicion = posicion;
		this.id = id;
		this.color = color;
		lineaSuperior = new Linea(posicion, new Vector2(posicion.X + ancho, posicion.Y));
		lineaInferior = new Linea(new Vector2(posicion.X, posicion.Y + alto), new Vector2(posicion.X + ancho, posicion.Y + alto));
		lineaIzda = new Linea(posicion, new Vector2(posicion.X, posicion.Y + alto));
		lineaDcha = new Linea(new Vector2(posicion.X + ancho, posicion.Y), new Vector2(posicion.X + ancho, posicion.Y + alto));
		haSidoColisionado = false;
	}

	public void actualizarLadrilloBase(){//Este método solo lo llama el ladrillo base
		lineaSuperior.punto1.X = posicion.X;
		lineaSuperior.punto1.Y = posicion.Y;
		lineaSuperior.punto2.X = posicion.X + 200;
		lineaSuperior.punto2.Y = posicion.Y;

		lineaInferior.punto1.X = posicion.X;
		lineaInferior.punto1.Y = posicion.Y + 40;
		lineaInferior.punto2.X = posicion.X + 200;
		lineaInferior.punto2.Y = posicion.Y + 40;

		lineaIzda.punto1.X = posicion.X;
		lineaIzda.punto1.Y = posicion.Y;
		lineaIzda.punto2.X = posicion.X;
		lineaIzda.punto2.Y = posicion.Y + 40;

		lineaDcha.punto1.X = posicion.X + 200;
		lineaDcha.punto1.Y = posicion.Y;
		lineaDcha.punto2.X = posicion.X + 200;
		lineaDcha.punto2.Y = posicion.Y + 40;
	}
}
