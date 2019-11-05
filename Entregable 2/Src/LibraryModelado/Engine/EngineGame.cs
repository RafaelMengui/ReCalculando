//--------------------------------------------------------------------------------
// <copyright file="EngineGame.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto.Item;
using Proyecto.Item.ScientistLevel;

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
    /// Hereda de la clase abstracta <see cref="IEngine"/>.
    /// </summary>
    public class EngineGame : IEngine
    {
        /// <summary>
        /// Diccionario en donde se le asocia a un nivel, su respectivo Motor.
        /// </summary>
        private Dictionary<Space, IEngine> levelEngines = new Dictionary<Space, IEngine>();

        /// <summary>
        /// Instancia de la clase EngineUnity.
        /// </summary>
        private EngineUnity engineUnity = Singleton<EngineUnity>.Instance;

        /// <summary>
        /// Motor generico <see cref="IEngine"/>.
        /// </summary>
        private IEngine engine;

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
        /// Gets or sets del diccionario de motores y niveles.
        /// </summary>
        /// <value>Diccionario de clave <see cref="Space"/> y valor <see cref="IEngine"/>.</value>
        public Dictionary<Space, IEngine> LevelEngines { get; set; }

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
                        if (level.Name != "MainPage")
                        {
                            this.engine = Activator.CreateInstance(Type.GetType("Proyecto.LibraryModelado.Engine.Engine" + level.Name)) as IEngine;
                            this.LevelEngines.Add(level, this.engine);
                            (this.engine as ILevelEngine).Level = level;
                        }
                    }
                    catch (System.Exception)
                    {
                        throw new Exception($"Engine \"Engine{level.Name}\" does not exist or couldn't be created.");
                    }
                }
            }
        }

        public void SetFeedbacks(IComponent component)
        {
            if (component is Feedback)
            {
                Feedback feedback = component as Feedback;
                ILevelEngine levelEngines = this.LevelEngines[feedback.Level] as ILevelEngine;
                levelEngines.LevelFeedback = feedback;
            }
        }

        /// <summary>
        /// Metodo que asigna una operacion en su respectivo nivel.
        /// </summary>
        /// <param name="component"></param>
        public override void SetOperations(IComponent component)
        {
            if (component is Operation)
            {
                Operation operation = component as Operation;
                this.levelEngines[operation.Level].SetOperations(component);
            }
        }

        /// <summary>
        /// Sobrescribe el metodo abstracto de <see cref="IEngine"/>, en donde ejecuta para cada
        /// motor de los niveles el metodo de crear un boton que muestre la pagina principal.
        /// </summary>
        public override IComponent ButtonGoToMain()
        {
            foreach (var engines in this.levelEngines)
            {
                IComponent button = engines.Value.ButtonGoToMain();
                this.engineUnity.SendComponentToUFactory(button);
            }

            return null;
        }

        /// <summary>
        /// Metodo responsable de delegar la responsabilidad al motor de unity, de crear un
        /// objeto en unity.
        /// </summary>
        /// <param name="component"></param>
        public void CreateInUnity(IComponent component)
        {
            this.engineUnity.SendComponentToUFactory(component);
        }

        /// <summary>
        /// Metodo responsable de llamar al motor de unity para que actualize el texto de un
        /// <see cref="Label"/>.
        /// </summary>
        /// <param name="feedback"></param>
        public void UpdateFeedback(Feedback feedback)
        {
            this.engineUnity.UpdateFeedback(feedback);
        }

        /// <summary>
        /// Metodo utilizado para iniciar o reiniciar el motor del juego de un determinado nivel.
        /// </summary>
        /// <param name="level"></param>
        public void StartLevelEngine(Space level)
        {
            (this.LevelEngines[level] as ILevelEngine).StartLevel();
        }
    }
}