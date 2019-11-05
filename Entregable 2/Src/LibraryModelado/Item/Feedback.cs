//--------------------------------------------------------------------------------
// <copyright file="Feedback.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using Proyecto.LibraryModelado;

namespace Proyecto.Item
{
    /// <summary>
    /// Clase responsable de crear feedbacks en el modelado.
    /// Hereda de la clase abstracta <see cref="Items"/>.
    /// </summary>
    public class Feedback : Items
    {
        /// <summary>
        /// Initializes a new instance of Feedback.
        /// </summary>
        /// <param name="name">Nombre del texto.</param>
        /// <param name="level">Nivel al que pertence.</param>
        /// <param name="positionX">Posicion en eje horizontal en pixeles.</param>
        /// <param name="positionY">Posicion en eje vertical en pixeles.</param>
        /// <param name="width">Ancho en pixeles.</param>
        /// <param name="height">Altura en pixeles.</param>
        /// <param name="image">Imagen del texto.</param>
        /// <param name="text">Texto de la etiqueta.</param>
        public Feedback(string name, Space level, float positionX, float positionY, float width, float height, string image, string text)
        : base(name, level, positionX, positionY, width, height, image)
        {
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets del texto.
        /// </summary>
        /// <value></value>
        public string Text { get; set; }
    }
}