//--------------------------------------------------------------------------------
// <copyright file="ButtonShowImage.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using System;
using Proyecto.LibraryModelado;
using Proyecto.LibraryModelado.Engine;

namespace Proyecto.Item
{
    /// <summary>
    /// Clase responsable de crear botones del tipo showimage , con la funcionalidad de mostrar la imagen correspondiente.
    /// Hereda de la clase abstracta <see cref="Items"/>, e implementa la interfaz <see cref="IButton"/>.
    /// </summary>
    public class ButtonShowImage : Items, IButton
    {
        /// <summary>
        /// Accion de mostrar otra pagina.
        /// </summary>
        private Action<string> evento;

        /// <summary>
        /// CInitializes a new instance of ButtonShowImage.
        /// </summary>
        /// <param name="name">Nombre del boton.</param>
        /// <param name="level">Nivel al que pertence.</param>
        /// <param name="positionX">Posicion en eje horizontal en pixeles.</param>
        /// <param name="positionY">Posicion en eje vertical en pixeles.</param>
        /// <param name="width">Ancho en pixeles.</param>
        /// <param name="height">Altura en pixeles.</param>
        /// <param name="image">Imagen del boton.</param>
        /// <param name="color">Color del boton en Hexadecimal.</param>
        /// <param name="imageName">Pagina para mostrar.</param>
        public ButtonShowImage(string name, Space level, float positionX, float positionY, float width, float height, string image, string color, string imageName)
        : base(name, level, positionX, positionY, width, height, image)
        {
            this.Color = color;
            this.ImageName = imageName;
            this.Event = this.evento;
            this.Pushable = true;
        }

        /// <summary>
        /// Gets or sets indicating whether el boton es presionable.
        /// Por defecto es true.
        /// </summary>
        /// <value>Bool.</value>
        public bool Pushable { get; set; }

        /// <summary>
        /// Gets or sets del Color del Boton.
        /// </summary>
        /// <value>string codigo en hexadecimal.</value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets de la imagen a mostrar.
        /// </summary>
        /// <value>string nombre del la imagen.</value>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets del evento del boton.
        /// </summary>
        /// <value>Action.</value>
        public Action<string> Event { get; set; }

        /// <summary>
        /// Acciones realizadas por el boton.
        /// Busca el nivel que coincida con el nivel que mostrara al ser apretado, y obtiene su ID.
        /// </summary>
        /// <param name="text">Sin funcionalidad.</param>
        public void Click(string text)
        {
            EngineGame engineGame = Singleton<EngineGame>.Instance;
            if (this.Pushable)
            {
                if (this.ImageName == this.Name)
                {
                    // Si el boton que mostrara este boton es este mismo boton, se ocultara a el mismo.
                    engineGame.SetActive(this, false);
                }
                else
                {
                    // En caso contrario, mostrara el boton.
                    Items image = this.Level.ItemList.Find(delegate (Items item) { return item.Name == this.ImageName; });
                    engineGame.SetActive(image, true);
                }
            }
        }
    }
}