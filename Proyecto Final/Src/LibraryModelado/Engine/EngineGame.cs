//--------------------------------------------------------------------------------
// <copyright file="EngineGame.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto.Item;


namespace Proyecto.LibraryModelado.Engine
{
    /// <summary>
    /// Motor general del juego.
    /// Tiene la responsabilidad de conocer y controlar la funcionalidad de los demas motores del juego.
    /// Para realizar los diferentes motores utilizamos el patrón SINGLETON, este nos permite garantizar
    /// la existencia de una sola instancia de clase. Además el acceso a esa única instancia tiene
    /// que ser global. Esto es de mucha utilidad debido a que vamos a necesitar llamar a los motores
    /// de los juego desde diferentes partes del código. Como se ejecuta una única vez nos aseguramos
    /// de que solo haya un motor de este juego.
    /// En esta clase se puede observar el patrón de INVERSIÓN DE DEPENDENCIAS, este nos dice que,
    /// Las clases de alto nivel no deben depender de clases de bajo nivel; ambas deben depender de abstracciones.
    /// Nuestra clase de alto nivel sería EngineGame, y la de bajo nivel todos los motores de cientifico.
    /// En este caso, creamos una interfaz ILevelEngine, de esta forma no accedemos directamente a los motores
    /// sino que, llamamos a los métodos de la intefaz (utilizamos una abstracción). Esto nos permite poder
    /// realizar cambios en la clase EngineScientific1 sin que cambie EngineGame y viceversa.
    /// Hereda de la clase abstracta <see cref="IEngine"/>.
    /// </summary>
    public class EngineGame : IEngine
    {
        /// <summary>
        /// Diccionario en donde se le asocia a un nivel, su respectivo Motor.
        /// </summary>
        private Dictionary<Space, ILevelEngine> levelEngines = new Dictionary<Space, ILevelEngine>();

        /// <summary>
        /// Instancia de la clase EngineUnity.
        /// </summary>
        private EngineUnity engineUnity = Singleton<EngineUnity>.Instance;

        /// <summary>
        /// Motor generico <see cref="IEngine"/>.
        /// </summary>
        private ILevelEngine engine;

        /// <summary>
        /// Pagina principal del juego, el motor la conoce para poder viajar a ella en cualquier momento.
        /// </summary>
        private Space mainPage;

        /// <summary>
        /// Pagina en que se encuentra actualmente el usuario.
        /// </summary>
        private Space currentPage;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EngineGame()
        {
            this.MainPage = mainPage;
            this.LevelEngines = levelEngines;
            this.CurrentPage = currentPage;
        }

        /// <summary>
        /// Gets del diccionario de motores y niveles.
        /// </summary>
        /// <value>Diccionario de clave <see cref="Space"/> y valor <see cref="IEngine"/>.</value>
        public Dictionary<Space, ILevelEngine> LevelEngines { get; set; }

        /// <summary>
        /// Gets or sets de la pagina en que se encuentra actualmente el usuario.
        /// </summary>
        public Space CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets de la pagina principal.
        /// </summary>
        /// <value>Space.</value>
        public Space MainPage { get; set; }

        /// <summary>
        /// Metodo responsable de asociarle a cada nivel su respectivo motor, y agregarlo al diccionario this.LevelEngines.
        /// En este método utilizamos una excepción, el fin de estas es indicar que el programa no puede continuar ejecutando
        /// en su estado actual, y como tal, terminarlo. Para manejar la excepción y darle una adecuada solucion al programa
        /// para que este siga operando. En este caso, lanza una excepción en caso que el motor no exista.
        /// </summary>
        /// <param name="componentList">Lista de componentes creados.</param>
        public void AsociateLevelsWithEngines(List<IComponent> componentList)
        {
            foreach (IComponent component in componentList)
            {
                if (component is Space)
                {
                    Space level = component as Space;
                    try
                    {
                        if (level.Name.Equals("MainPage"))
                        {
                            this.MainPage = level;
                            this.CurrentPage = level;
                        }
                        else if (!level.Name.Contains("Menu"))
                        {
                            Type engineType = Type.GetType("Proyecto.LibraryModelado.Engine.Engine" + level.Name);
                            this.engine = Activator.CreateInstance(engineType) as ILevelEngine;
                            this.engine.Level = level;
                            this.LevelEngines.Add(level, this.engine);
                        }
                    }
                    catch (System.Exception)
                    {
                        throw new Exception($"Engine \"Engine{level.Name}\" does not exist or couldn't be created.");
                    }
                }
            }
        }

        /// <summary>
        /// Metodo responsable de delegar la responsabilidad al motor de unity, de crear un
        /// objeto en unity.
        /// </summary>
        /// <param name="component">Componente</param>
        public void CreateInUnity(IComponent component)
        {
            this.engineUnity.CreateInUnity(component);
        }

        /// <summary>
        /// Metodo responsable de llamar al motor de unity para que actualize el texto de un
        /// <see cref="Label"/>.
        /// </summary>
        /// <param name="feedback"></param>
        /// <param name="text">Nuevo texto del feedback.</param>
        public void UpdateFeedback(Feedback feedback, string text)
        {
            this.engineUnity.UpdateFeedback(feedback, text);
            feedback.Text = text;
        }

        /// <summary>
        /// Metodo responsable de llamar al motor de unity para que actualize el texto de un objeto.
        /// </summary>
        /// <param name="items">Item que se vaya actualizar.</param>
        /// <param name="text">Nuevo texto del item.</param>
        public void UpdateText(Items items, string text)
        {
            this.engineUnity.UpdateText(items, text);
        }

        /// <summary>
        /// Metodo utilizado para iniciar o reiniciar el motor del juego de un determinado nivel.
        /// </summary>
        /// <param name="level">Nivel que este asociado al motor a iniciar.</param>
        public void StartLevelEngine(Space level)
        {
            this.LevelEngines[level].StartLevel();
        }

        /// <summary>
        /// Metodo responsable de llamar al motor de unity para Centrar un IDraggable en
        /// un IContainer.
        /// </summary>
        /// <param name="item">Item de tipo IDraggable.</param>
        public void CenterInContainer(Items item)
        {
            if (item is IDraggable)
            {
                IContainer container = (item as IDraggable).Container;
                this.engineUnity.CenterInUnity(item, container as Items);
            }
        }

        /// <summary>
        /// Metodo responsable de llamar al motor de unity para actualizar un item
        /// para que sea arrastrable o no.
        /// Si el item ya es arrastrable, no se ejecutara el metodo de unity para evitar errores.
        /// </summary>
        /// <param name="item">Item que se va a actualizar.</param>
        /// <param name="isDraggable">Bool que indica si va a ser arrastrable.</param>
        public void SetItemDraggable(Items item, bool isDraggable)
        {
            if (item is IDraggable)
            {
                IDraggable draggableItem = item as IDraggable;
                if (!draggableItem.Draggable.Equals(isDraggable))
                {
                    this.engineUnity.SetItemDraggable(item, isDraggable);
                    draggableItem.Draggable = isDraggable;
                }
            }
        }

        /// <summary>
        /// Metodo responsable de llamar al motor de unity para actualizar si un item es
        /// mostrado por pantalla u ocultado.
        /// </summary>
        /// <param name="item">item que se va a actualizar.</param>
        /// <param name="active">Bool que indica si se va a mostrar u ocultar.</param>
        public void SetActive(Items item, bool active)
        {
            if (!item.IsActive.Equals(active))
            {
                this.engineUnity.SetActive(item, active);
                item.IsActive = active;
            }
        }
    }
}