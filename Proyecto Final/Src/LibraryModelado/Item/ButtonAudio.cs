//--------------------------------------------------------------------------------
// <copyright file="ButtonAudio.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using System;
using Proyecto.LibraryModelado;

namespace Proyecto.Item
{
    /// <summary>
    /// Clase responsable de crear botones, con la funcionalidad de reproducir un audio en el modelado.
    /// Hereda de la clase abstracta <see cref="Items"/>, e implementa la interfaz <see cref="IButton"/>.
    /// </summary>
    public class ButtonAudio : Items, IButton
    {
        /// <summary>
        /// Accion de reproducir el sonido.
        /// </summary>
        private Action<string> evento;

        /// <summary>
        /// Inicializa una nueva instancia de ButtonAudio.
        /// </summary>
        /// <param name="name">Nombre del boton.</param>
        /// <param name="level">Nivel al que pertence.</param>
        /// <param name="positionX">Posicion en eje horizontal en pixeles.</param>
        /// <param name="positionY">Posicion en eje vertical en pixeles.</param>
        /// <param name="width">Ancho en pixeles.</param>
        /// <param name="height">Altura en pixeles.</param>
        /// <param name="image">Imagen del boton.</param>
        /// <param name="color">Color del boton en Hexadecimal.</param>
        /// <param name="audioFile">Audio a reproducir por el boton.</param>
        public ButtonAudio(string name, Space level, float positionX, float positionY, float width, float height, string image, string color, string audioFile)
        : base(name, level, positionX, positionY, width, height, image)
        {
            this.Color = color;
            this.AudioFile = audioFile;
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
        /// Gets or sets del Audio del boton.
        /// </summary>
        /// <value>string nombre del audio.</value>
        public string AudioFile { get; set; }

        /// <summary>
        /// Gets or sets del evento del boton.
        /// </summary>
        /// <value>Action.</value>
        public Action<string> Event { get; set; }

        /// <summary>
        /// Acciones realizadas por el boton.
        /// </summary>
        /// <param name="text">Sin funcionalidad.</param>
        public void Click(string text)
        {
            if (this.Pushable)
            {
                this.Event(this.AudioFile);
            }
        }
    }
}