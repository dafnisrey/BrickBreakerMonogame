using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class GestionCirculo{
	private float velocidadCirculo = 3;
    public bool haTocadoLimiteDcha = false;
    public bool haTocadoLimiteIzda = true;
    public bool haTocadoLimiteInferior = false;
    public bool haTocadoLimiteSuperior = true;

	public Vector2 ActualizarPosicionCirculo(Vector2 posicionCirculo){
        if(posicionCirculo.X >= 1090){
            haTocadoLimiteDcha = true;
            haTocadoLimiteIzda = false;
        }
        if(posicionCirculo.X <= 10){
            haTocadoLimiteIzda = true;
            haTocadoLimiteDcha = false;
        }
        if(haTocadoLimiteIzda){
            posicionCirculo.X = posicionCirculo.X + velocidadCirculo;
        }
        if(haTocadoLimiteDcha){
            posicionCirculo.X = posicionCirculo.X - velocidadCirculo;
        }
        if(posicionCirculo.Y >= 850){
            haTocadoLimiteInferior = true;
            haTocadoLimiteSuperior = false;
        }
        if(posicionCirculo.Y <= 10){
            haTocadoLimiteSuperior = true;
            haTocadoLimiteInferior = false;
        }
        if(haTocadoLimiteSuperior){
            posicionCirculo.Y = posicionCirculo.Y + velocidadCirculo;
        }
        if(haTocadoLimiteInferior){
            posicionCirculo.Y = posicionCirculo.Y - velocidadCirculo;
        }
        return posicionCirculo;
    }
}

public class Circulo{
	public Vector2 posicion;
	public float radioCirculo{get; set;}
	public Circulo(Vector2 posicion, float radioCirculo){
		this.posicion = posicion;
		this.radioCirculo = radioCirculo;
	}
}

