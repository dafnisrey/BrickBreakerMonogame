using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

using MonoG1;

public class GestionColisiones{
	public List<Ladrillo> listaLadrillos;
    public List<Ladrillo> listaLadrillosSalmon;
	public GestionCirculo objAuxiliar;
	public Circulo circulo;
    public int cantidadPurpura = 9;
    public int cantidadSalmon = 6;
    public int cantidadAzul = 8;
    public int cantidadVerde = 10;
    public int cantidadBlanco = 7;
    public List<int> idsPurpuraRestantes = new List<int>{0, 1, 2, 3, 4, 5, 6, 7, 8};
    public List<int> idsSalmonRestantes = new List<int>{9, 10, 11, 12, 13, 14};
    public List<int> idsAzulRestantes = new List<int>{15, 16, 17, 18, 19, 20, 21, 22};
    public List<int> idsVerdeRestantes = new List<int>{23, 24, 25, 26, 27, 28, 29, 30, 31, 32};
    public List<int> idsBlancoRestantes = new List<int>{33, 34, 35, 36, 37, 38, 39};
    public List<Ladrillo> listaLadrillosColisionados = new List<Ladrillo>();

	public void setCampos(List<Ladrillo> lista, GestionCirculo aux, Circulo circ){
		listaLadrillos = lista;
		objAuxiliar = aux;
		circulo = circ;
	}
	public void resetearListas(List<Ladrillo> lista){
        listaLadrillos = lista;
        listaLadrillosColisionados = new List<Ladrillo>();
        cantidadPurpura = 9;
        cantidadSalmon = 6;
        cantidadAzul = 8;
        cantidadVerde = 10;
        cantidadBlanco = 7;
        idsPurpuraRestantes = new List<int>{0, 1, 2, 3, 4, 5, 6, 7, 8};
        idsSalmonRestantes = new List<int>{9, 10, 11, 12, 13, 14};
        idsAzulRestantes = new List<int>{15, 16, 17, 18, 19, 20, 21, 22};
        idsVerdeRestantes = new List<int>{23, 24, 25, 26, 27, 28, 29, 30, 31, 32};
        idsBlancoRestantes = new List<int>{33, 34, 35, 36, 37, 38, 39};
    }

    public bool chequearColisionCirculoLinea(Vector2 posicionCirculo, float circuloRadio, Linea linea){
        Vector2 lineaVector = linea.punto2 - linea.punto1;
        float t = Vector2.Dot(posicionCirculo - linea.punto1, lineaVector) / Vector2.Dot(lineaVector, lineaVector);
        t = MathHelper.Clamp(t, 0f, 1f);
        Vector2 puntoCercano = linea.punto1 + t * lineaVector;
        float distancia = Vector2.Distance(posicionCirculo, puntoCercano);
        return distancia <= circuloRadio;
    }

	public void activarColisiones(){
        for(int i = 0; i < listaLadrillos.Count; i++){
            if(chequearColisionCirculoLinea(circulo.posicion, circulo.radioCirculo, listaLadrillos[i].lineaSuperior)){
                //Console.WriteLine("Colision con linea superior de ladrillo: " + listaLadrillos[i].id);
                objAuxiliar.haTocadoLimiteInferior = true;
                objAuxiliar.haTocadoLimiteSuperior = false;
                listaLadrillos[i].haSidoColisionado = true;
                switch(listaLadrillos[i].color){
                    case 0: cantidadPurpura--; idsPurpuraRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 1: cantidadSalmon--; idsSalmonRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 2: cantidadAzul--; idsAzulRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 3: cantidadVerde--; idsVerdeRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 4: cantidadBlanco--; idsBlancoRestantes.Remove(listaLadrillos[i].id);
                        break;
                }
                listaLadrillosColisionados.Add(listaLadrillos[i]);
                listaLadrillos.RemoveAt(i);
            }
        }
        for(int i = 0; i < listaLadrillos.Count; i++){
            if(chequearColisionCirculoLinea(circulo.posicion, circulo.radioCirculo, listaLadrillos[i].lineaInferior)){
                //Console.WriteLine("Colision con linea inferior de ladrillo: " + listaLadrillos[i].id);
                objAuxiliar.haTocadoLimiteInferior = false;
                objAuxiliar.haTocadoLimiteSuperior = true;
                listaLadrillos[i].haSidoColisionado = true;
                switch(listaLadrillos[i].color){
                    case 0: cantidadPurpura--; idsPurpuraRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 1: cantidadSalmon--; idsSalmonRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 2: cantidadAzul--; idsAzulRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 3: cantidadVerde--; idsVerdeRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 4: cantidadBlanco--; idsBlancoRestantes.Remove(listaLadrillos[i].id);
                        break;
                }
                listaLadrillosColisionados.Add(listaLadrillos[i]);
                listaLadrillos.RemoveAt(i);
            }
        }
        for(int i = 0; i < listaLadrillos.Count; i++){
            if(chequearColisionCirculoLinea(circulo.posicion, circulo.radioCirculo, listaLadrillos[i].lineaIzda)){
                //Console.WriteLine("Colision con linea izquierda de ladrillo: " + listaLadrillos[i].id);
                objAuxiliar.haTocadoLimiteDcha = true;
                objAuxiliar.haTocadoLimiteIzda = false;
                listaLadrillos[i].haSidoColisionado = true;
                switch(listaLadrillos[i].color){
                    case 0: cantidadPurpura--; idsPurpuraRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 1: cantidadSalmon--; idsSalmonRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 2: cantidadAzul--; idsAzulRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 3: cantidadVerde--; idsVerdeRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 4: cantidadBlanco--; idsBlancoRestantes.Remove(listaLadrillos[i].id);
                        break;
                }
                listaLadrillosColisionados.Add(listaLadrillos[i]);
                listaLadrillos.RemoveAt(i);
            }
        }
        for(int i = 0; i < listaLadrillos.Count; i++){
            if(chequearColisionCirculoLinea(circulo.posicion, circulo.radioCirculo, listaLadrillos[i].lineaDcha)){
                //Console.WriteLine("Colision con linea derecha de ladrillo: " + listaLadrillos[i].id);
                objAuxiliar.haTocadoLimiteDcha = false;
                objAuxiliar.haTocadoLimiteIzda = true;
                listaLadrillos[i].haSidoColisionado = true;
                switch(listaLadrillos[i].color){
                    case 0: cantidadPurpura--; idsPurpuraRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 1: cantidadSalmon--; idsSalmonRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 2: cantidadAzul--; idsAzulRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 3: cantidadVerde--; idsVerdeRestantes.Remove(listaLadrillos[i].id);
                        break;
                    case 4: cantidadBlanco--; idsBlancoRestantes.Remove(listaLadrillos[i].id);
                        break;
                }
                listaLadrillosColisionados.Add(listaLadrillos[i]);
                listaLadrillos.RemoveAt(i);
            }
        }
    }
}

