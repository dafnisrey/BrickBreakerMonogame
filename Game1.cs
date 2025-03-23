using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Juego = Microsoft.Xna.Framework;

namespace MonoG1;

public class Game1 : Game{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D spriteLadrillos;
    private Texture2D spriteCirculo;
    private Texture2D spriteBase;
    private Rectangle recorteLadrilloBase;
    private Rectangle recortePurpura;
    private Rectangle recorteSalmon;
    private Rectangle recorteAzul;
    private Rectangle recorteVerde;
    private Rectangle recorteBlanco;
    private Vector2 posicionInicialBase;
    private MouseState mouseState;
    private KeyboardState estadoInicialTeclado;
    private GestionCirculo objAuxiliar;
    private Circulo circulo;
    private List<Ladrillo> listaLadrillos;
    private Ladrillo ladrilloBase;
    private Ladrillo ladrilloGenerico;
    private int idPurpura;
    private int idSalmon;
    private int idAzul;
    private int idVerde;
    private int idBlanco;
    private bool hasApretadoEspacio;
    private EstadoJuego estadoJuego = EstadoJuego.PantallaBienvenida;
    private MovimientoCirculo movimientoCirculo = MovimientoCirculo.PegadoABase;
    private SpriteFont fuente;
    private GestionColisiones objGestionColisiones;
    private Vector2 tamañoFin;
    private Vector2 tamañoBienvenida;
    private Vector2 tamañoVictoria;
    private Vector2 tamañoPulsaE;
    private Vector2 tamañoPulsaEsc;
    private Vector2 tamañoExplicacion;
    private int xFin;
    private int xBienvenida;
    private int xVictoria;
    private int xPulsaE;
    private int xPulsaEsc;
    private int xExplicacion;
    private String stringBienvenida;
    private String stringFin;
    private String stringPulsaE;
    private String stringPulsaEscape;
    private String stringVictoria;
    private String stringExplicacion;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1100;
        _graphics.PreferredBackBufferHeight = 800;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        estadoInicialTeclado = Keyboard.GetState();
        objAuxiliar = new GestionCirculo();
        circulo = new Circulo(new Vector2(50, 50), 10);
        recortePurpura = new Rectangle(0, 160, 100, 40);
        recorteSalmon = new Rectangle(0, 40, 100, 40);
        recorteAzul = new Rectangle(0, 80, 100, 40);
        recorteVerde = new Rectangle(0, 120, 100, 40);
        recorteBlanco = new Rectangle(0, 0, 100, 40);
        posicionInicialBase = new Vector2(375, 550);
        stringFin = "Fin de la partida.";
        stringBienvenida = "Bienvenido.";
        stringVictoria = "Enhorabuen! Lo has conseguido.";
        stringPulsaE = "Pulsa [E] para jugar.";
        stringPulsaEscape = "Pulsa [Escape] para salir.";
        stringExplicacion = "El juego se acaba cuando quede 1 solo bloque.";

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteLadrillos = Content.Load<Texture2D>("SpriteLadrillos");
        spriteBase = Content.Load<Texture2D>("SpriteBase");
        spriteCirculo = Content.Load<Texture2D>("Circulo");
        fuente = Content.Load<SpriteFont>("FuenteFutura");
        recorteLadrilloBase = new Rectangle(0, 0, 100, 40);
        ladrilloBase = new Ladrillo(posicionInicialBase, 200, 40, 0, 0);
        listaLadrillos = GestionLadrillos.crearListaLadrillos();
        objGestionColisiones = new GestionColisiones();
        objGestionColisiones.setCampos(listaLadrillos, objAuxiliar, circulo);
    }

    protected override void Update(GameTime gameTime)
    {

        if(estadoJuego == EstadoJuego.Activo){

            KeyboardState keyboardState = Keyboard.GetState();
            if(estadoInicialTeclado.IsKeyDown(Keys.Escape) && keyboardState.IsKeyUp(Keys.Escape)){
                estadoJuego = EstadoJuego.PantallaBienvenida;
            }
            if(estadoInicialTeclado.IsKeyDown(Keys.Space) && keyboardState.IsKeyUp(Keys.Space)){
                hasApretadoEspacio = true;
            }
            estadoInicialTeclado = keyboardState;

            if(hasApretadoEspacio == true){
                movimientoCirculo = MovimientoCirculo.Libre;
            }else{
                movimientoCirculo = MovimientoCirculo.PegadoABase;
            }

            if(movimientoCirculo == MovimientoCirculo.Libre){
                circulo.posicion = objAuxiliar.ActualizarPosicionCirculo(circulo.posicion);
            }else if (movimientoCirculo == MovimientoCirculo.PegadoABase){
                circulo.posicion.X = ladrilloBase.posicion.X + 100;
                circulo.posicion.Y = ladrilloBase.posicion.Y - 11;
            }

            mouseState = Mouse.GetState();
            if( mouseState.X > 100 && mouseState.X < 1000){
                ladrilloBase.posicion.X = mouseState.X - 100;
            }else if(mouseState.X >= 1000){
                ladrilloBase.posicion.X = 900;
            }else if(mouseState.X <= 100){
                ladrilloBase.posicion.X = 0;
            }
            ladrilloBase.posicion.Y = 720;
            ladrilloBase.actualizarLadrilloBase();

            if(objGestionColisiones.chequearColisionCirculoLinea(circulo.posicion, circulo.radioCirculo, ladrilloBase.lineaSuperior)){
                objAuxiliar.haTocadoLimiteInferior = true;
                objAuxiliar.haTocadoLimiteSuperior = false;
            }
            if(objGestionColisiones.chequearColisionCirculoLinea(circulo.posicion, circulo.radioCirculo, ladrilloBase.lineaIzda)){
                objAuxiliar.haTocadoLimiteDcha = true;
                objAuxiliar.haTocadoLimiteIzda = false;
            }
            if(objGestionColisiones.chequearColisionCirculoLinea(circulo.posicion, circulo.radioCirculo, ladrilloBase.lineaDcha)){
                objAuxiliar.haTocadoLimiteDcha = false;
                objAuxiliar.haTocadoLimiteIzda = true;
            }
            objGestionColisiones.activarColisiones();
            if(circulo.posicion.Y > 800){
                estadoJuego = EstadoJuego.Finalizado;
            }

            if(listaLadrillos.Count == 1){
                estadoJuego = EstadoJuego.Finalizado;
            }

        } else if(estadoJuego == EstadoJuego.Finalizado){
                KeyboardState keyboardState = Keyboard.GetState();
                if(estadoInicialTeclado.IsKeyDown(Keys.E) && keyboardState.IsKeyUp(Keys.E)){
                    circulo.posicion = new Vector2(ladrilloBase.posicion.X + 100, ladrilloBase.posicion.Y - 11);
                    hasApretadoEspacio = false;
                    listaLadrillos = GestionLadrillos.crearListaLadrillos();
                    objGestionColisiones.resetearListas(listaLadrillos);
                    estadoJuego = EstadoJuego.Activo;
                }
                if(estadoInicialTeclado.IsKeyDown(Keys.Escape) && keyboardState.IsKeyUp(Keys.Escape)){
                    Exit();
                }
                    estadoInicialTeclado = keyboardState;

        }else if(estadoJuego == EstadoJuego.PantallaBienvenida){
                KeyboardState keyboardState = Keyboard.GetState();
                if(estadoInicialTeclado.IsKeyDown(Keys.E) && keyboardState.IsKeyUp(Keys.E)){
                    circulo.posicion = new Vector2(ladrilloBase.posicion.X + 100, ladrilloBase.posicion.Y - 11);
                    hasApretadoEspacio = false;
                    listaLadrillos = GestionLadrillos.crearListaLadrillos();
                    objGestionColisiones.resetearListas(listaLadrillos);
                    estadoJuego = EstadoJuego.Activo;
                }
                if(estadoInicialTeclado.IsKeyDown(Keys.Escape) && keyboardState.IsKeyUp(Keys.Escape)){
                    Exit();
                }
                estadoInicialTeclado = keyboardState;
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
        tamañoFin = fuente.MeasureString(stringFin);
        xFin = (GraphicsDevice.Viewport.Width - (int)tamañoFin.X) / 2;

        tamañoBienvenida = fuente.MeasureString(stringBienvenida);
        xBienvenida = (GraphicsDevice.Viewport.Width - (int)tamañoBienvenida.X) / 2;

        tamañoPulsaE = fuente.MeasureString(stringPulsaE);
        xPulsaE = (GraphicsDevice.Viewport.Width - (int)tamañoPulsaE.X) / 2;

        tamañoPulsaEsc = fuente.MeasureString(stringPulsaEscape);
        xPulsaEsc = (GraphicsDevice.Viewport.Width - (int)tamañoPulsaEsc.X) / 2;

        tamañoExplicacion = fuente.MeasureString(stringExplicacion);
        xExplicacion = (GraphicsDevice.Viewport.Width - (int)tamañoExplicacion.X) / 2;

        tamañoVictoria = fuente.MeasureString(stringVictoria);
        xVictoria = (GraphicsDevice.Viewport.Width - (int)tamañoVictoria.X) / 2;


        if(estadoJuego == EstadoJuego.Activo){

            _spriteBatch.Draw(spriteBase, ladrilloBase.posicion, Color.White);

            for(int i = 0; i < objGestionColisiones.cantidadPurpura; i++){
                idPurpura = objGestionColisiones.idsPurpuraRestantes[i];
                ladrilloGenerico = listaLadrillos.FirstOrDefault(p => p.id == idPurpura);
                    _spriteBatch.Draw(spriteLadrillos, ladrilloGenerico.posicion, recortePurpura, Color.White);
            }
            for(int i = 0; i < objGestionColisiones.cantidadSalmon; i++){
                idSalmon = objGestionColisiones.idsSalmonRestantes[i];
                ladrilloGenerico = listaLadrillos.FirstOrDefault(p => p.id == idSalmon);
                    _spriteBatch.Draw(spriteLadrillos, ladrilloGenerico.posicion, recorteSalmon, Color.White);
            }
            for(int i = 0; i < objGestionColisiones.cantidadAzul; i++){
                idAzul = objGestionColisiones.idsAzulRestantes[i];
                ladrilloGenerico = listaLadrillos.FirstOrDefault(p => p.id == idAzul);
                    _spriteBatch.Draw(spriteLadrillos, ladrilloGenerico.posicion, recorteAzul, Color.White);
            }
            for(int i = 0; i < objGestionColisiones.cantidadVerde; i++){
                idVerde = objGestionColisiones.idsVerdeRestantes[i];
                ladrilloGenerico = listaLadrillos.FirstOrDefault(p => p.id == idVerde);
                    _spriteBatch.Draw(spriteLadrillos, ladrilloGenerico.posicion, recorteVerde, Color.White);
            }
            for(int i = 0; i < objGestionColisiones.cantidadBlanco; i++){
                idBlanco = objGestionColisiones.idsBlancoRestantes[i];
                ladrilloGenerico = listaLadrillos.FirstOrDefault(p => p.id == idBlanco);
                    _spriteBatch.Draw(spriteLadrillos, ladrilloGenerico.posicion, recorteBlanco, Color.White);
            }

            foreach(Ladrillo ladrillo in objGestionColisiones.listaLadrillosColisionados){
                switch(ladrillo.color){
                    case 0: _spriteBatch.Draw(spriteLadrillos, ladrillo.posicion, recortePurpura, new Color(40, 40, 40, 40), 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

                    break;
                    case 1: _spriteBatch.Draw(spriteLadrillos, ladrillo.posicion, recorteSalmon, new Color(40, 40, 40, 40), 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

                    break;
                    case 2: _spriteBatch.Draw(spriteLadrillos, ladrillo.posicion, recorteAzul, new Color(40, 40, 40, 40), 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

                    break;
                    case 3:_spriteBatch.Draw(spriteLadrillos, ladrillo.posicion, recorteVerde, new Color(40, 40, 40, 40), 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

                    break;
                    case 4: _spriteBatch.Draw(spriteLadrillos, ladrillo.posicion, recorteBlanco, new Color(40, 40, 40, 40), 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

                    break;
                }
            }

            _spriteBatch.Draw(spriteCirculo, circulo.posicion, null, Color.White, 0f, new Vector2(spriteCirculo.Width / 2f, spriteCirculo.Height / 2f), new Vector2(0.4f, 0.4f), SpriteEffects.None, 0f);
            _spriteBatch.DrawString(fuente, "Pulsa [Escape] para salir.", new Vector2(20, 20), Color.White);


        }else if(estadoJuego == EstadoJuego.Finalizado){
            _spriteBatch.DrawString(fuente, stringFin, new Vector2(xFin, 300), Color.White);
            _spriteBatch.DrawString(fuente, stringPulsaE, new Vector2(xPulsaE, 350), Color.White);
            _spriteBatch.DrawString(fuente, stringPulsaEscape, new Vector2(xPulsaEsc, 400), Color.White);

        }else if(estadoJuego == EstadoJuego.PantallaBienvenida){
            _spriteBatch.DrawString(fuente, stringBienvenida, new Vector2(xBienvenida, 250), Color.White);
            _spriteBatch.DrawString(fuente, stringExplicacion, new Vector2(xExplicacion, 300), Color.White);
            _spriteBatch.DrawString(fuente, stringPulsaE, new Vector2(xPulsaE, 350), Color.White);
            _spriteBatch.DrawString(fuente, stringPulsaEscape, new Vector2(xPulsaEsc, 400), Color.White);
        }else if(estadoJuego == EstadoJuego.Victoria){
            _spriteBatch.DrawString(fuente, stringVictoria, new Vector2(xVictoria, 300), Color.White);
            _spriteBatch.DrawString(fuente, stringPulsaE, new Vector2(xPulsaE, 350), Color.White);
            _spriteBatch.DrawString(fuente, stringPulsaEscape, new Vector2(xPulsaEsc, 400), Color.White);
        }

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void dibujarLadrillosColisionados(){

    }

}


enum EstadoJuego{
    PantallaBienvenida,
    Activo,
    Finalizado,
    Victoria
}

enum MovimientoCirculo{
    PegadoABase,
    Libre
}




