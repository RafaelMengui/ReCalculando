//--------------------------------------------------------------------------------
// <copyright file="EngineScientificExercise1.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using System;
using Proyecto.Item;
using Proyecto.Item.ScientistLevel;

namespace Proyecto.LibraryModelado.Engine
{
    /// <summary>
    /// Clase EngineScientific, responsable de implementar la logica del nivel scientific.
    ///  Paraa realizar los motores utilizamos el patrón SINGLETON, este nos permite garantizar 
    /// la existencia de una sola instancia de clase. Además el acceso a esa única instancia tiene
    ///  que ser global. Esto es de mucha utilidad debido a que, vamos a necesitar llamar al motor
    ///  de este juego desde diferentes partes del código. Como se ejecuta una única vez nos aseguramos
    /// de que solo haya un motor de este juego.
    /// Hereda de las clases abstractas <see cref="IEngine"/> y <see cref="ILevelEngine"/>
    /// </summary>
    public class EngineScientificExercise1 : IEngine, ILevelEngine
    {
        /// <summary>
        /// Variable Level utilizada para instanciar un nivel asignable.
        /// </summary>
        private Space level;

        /// <summary>
        /// Instancia unica del motor general.
        /// </summary>
        private EngineGame engineGame = Singleton<EngineGame>.Instance;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EngineScientificExercise1()
        {
            this.ResultsOfPage = new bool[2];
            this.ResultsOfLevel = new bool[2];
            this.PageCounter = 0;
            this.LevelCounter = 0;
        }

        /// <summary>
        /// Gets or sets del nivel asociado a este Motor.
        /// </summary>
        /// <value></value>
        public Space Level { get { return level; } }

        /// <summary>
        /// Gets or sets de la etiqueta de texto utilizado para especificar si la accion fue correcta o incorrecta.
        /// </summary>
        /// <value>Etiqueta <see cref="Label"/>.</value>
        public Label Feedback { get; private set; }

        /// <summary>
        /// Gets or sets de contador utilizado para saber en que pagina del nivel nos encontramos.
        /// Existen dos paginas en el nivel.
        /// </summary>
        /// <value>Int.</value>
        public int LevelCounter { get; private set; }

        /// <summary>
        /// Gets or sets de contador utilizado para saber en que operacion de la pagina nos encontramos.
        /// Existen dos operaciones dentro de la pagina.
        /// </summary>
        /// <value>Int.</value>
        public int PageCounter { get; private set; }

        /// <summary>
        /// Gets or sets de los resultados de las sumas de una pagina.
        /// Por predeterminado los dos parametros son False.
        /// true = resutlado correcto.
        /// false = resultado Incorrecto.
        /// </summary>
        /// <value>Array de Bools.</value>
        public bool[] ResultsOfPage { get; private set; }

        /// <summary>
        /// Gets or sets de los resultados del nivel.
        /// Por predeterminado los dos parametros son False.
        /// true = Completo una pagina correctamente (los dos parametros de this.ResultsOfPage son true).
        /// false = No completo las dos operaciones de la pagina.
        /// </summary>
        /// <value>Array de Bools.</value>
        public bool[] ResultsOfLevel { get; private set; }

        /// <summary>
        /// Metodo responsable de verificar si el objeto tipo Money soltado dentro del MoneyContainer,
        /// tiene el valor que acepta el container.
        /// </summary>
        /// <param name="moneyContainer">Container tipo <see cref="MoneyContainer"/>.</param>
        /// <param name="money">DraggableItem tipo <see cref="Money"/>.</param>
        /// <returns>Bool si el valor es correcto o no.</returns>
        public static bool VerifyOperation(MoneyContainer moneyContainer, Money money)
        {
            return moneyContainer.AcceptableValue == money.Value;
        }

        /// <summary>
        /// Verifica que ambas sumas de la pagina esten hechas correctamente.
        /// Si fueron realizadas de manera correcta, los parametros del this.ResultsOfLevel pasan a ser true.
        /// </summary>
        /// <returns>Bool.</returns>
        public bool VerifyWinPage()
        {
            if (this.ResultsOfPage[0] && this.ResultsOfPage[1])
            {
                this.ResultsOfLevel[this.LevelCounter] = true;
                this.LevelCounter += 1;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Verifica que se hayan completado las dos paginas del nivel.
        /// </summary>
        /// <returns>Bool.</returns>
        public bool VerifyWinLevel()
        {
            return this.ResultsOfLevel[0] && this.ResultsOfLevel[1];
        }

        /// <summary>
        /// Metodo que se utiliza para verificar que este correctamente realizada una de las operaciones.
        /// Si esta bien hecha la operacion, el bool (en this.ResultsOfPage) asociado a la parte del nivel pasa a ser true.
        /// Al contador se le suma 1, y el container del money, pasa a ser el container en donde es dropeado.
        /// </summary>
        public bool VerifyExercise(MoneyContainer moneyContainer, Money money)
        {
            if (VerifyOperation(moneyContainer, money))
            {
                this.ResultsOfPage[this.PageCounter] = true;
                this.PageCounter += 1;
                money.Container = moneyContainer;
                this.GoodFeedback();
                return true;
            }
            else
            {
                this.BadFeedback();
                return false;
            }
        }

        /// <summary>
        /// Metodo utilizado para iniciar o reiniciar el motor del juego.
        /// </summary>
        public void StartLevel()
        {
            this.ResultsOfPage = new bool[2];
            this.ResultsOfLevel = new bool[2];
            this.LevelCounter = 0;
            this.PageCounter = 0;
            this.Feedback = CreateFeedback();
        }

        /// <summary>
        /// Metodo utilizado para iniciar o reiniciar la pagina.
        /// </summary>
        public void StartPage()
        {
            this.ResultsOfPage = new bool[2];
            this.PageCounter = 0;
        }

        /// <summary>
        /// Metodo responsable de crear la etiqueta de texto que servira de feedback a las acciones realizadas.
        /// </summary>
        /// <returns>Etiqueta <see cref="Label"/>.</returns>
        public Label CreateFeedback()
        {
            foreach (var space in this.engineGame.LevelEngines)
            {
                if (space.Value is EngineScientificExercise1)
                {
                    this.level = space.Key;
                }
            }
            Label feedback = new Label("Feedback", this.level, 600, 240, 100, 50, "Vacio.png", string.Empty);
            this.level.ItemList.Add(feedback);
            return feedback;
        }

        /// <summary>
        /// Metodo que asigna al texto un buen feedback. Utilizado cuando la accion realizada es correcta.
        /// </summary>
        public void GoodFeedback()
        {
            this.Feedback.Text = "Muy buen trabajo, ¡Continua asi!";
        }

        /// <summary>
        /// Metodo que asigna al texto un mal feedback. Utilizado cuando la accion realizada es incorrecta.
        /// </summary>
        public void BadFeedback()
        {
            this.Feedback.Text = "Esa suma no es correcta, ¡Intentalo de nuevo!";
        }

        /// <summary>
        /// Sobrescribe el metodo abstracto de <see cref="IEngine"/>, en donde se recorre el diccionario
        /// de motores asociados a niveles (EngineGame.LevelEngines), para reconocer en que nivel
        /// se debe crear el boton que mostrara la pagina principal al ejecutarlo.
        /// </summary>
        public override void ButtonGoToMain()
        {
            foreach (var space in engineGame.LevelEngines)
            {
                if (space.Value is EngineScientificExercise1)
                {
                    this.level = space.Key;
                }
            }
            ButtonGoToPage goToMain = new ButtonGoToPage("GoToMain", this.level, -600, -240, 100, 50, "huevo.png", "#FCFCFC", "MainPage");
            this.level.ItemList.Add(goToMain);
        }
    }
}