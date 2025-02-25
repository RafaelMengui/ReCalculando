//--------------------------------------------------------------------------------
// <copyright file="Image.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using Proyecto.LibraryModelado;

namespace Proyecto.Item
{
    /// <summary>
    /// Clase responsable de crear imagenes en el modelado.
    /// Hereda de la clase abstracta <see cref="Items"/>.
    /// </summary>
    public class Image : Items
    {
        /// <summary>
        /// Initializes a new instance of Image.
        /// </summary>
        /// <param name="name">Nombre de la imagen.</param>
        /// <param name="level">Nivel al que pertence.</param>
        /// <param name="positionX">Posicion en eje horizontal en pixeles.</param>
        /// <param name="positionY">Posicion en eje vertical en pixeles.</param>
        /// <param name="width">Ancho en pixeles.</param>
        /// <param name="height">Altura en pixeles.</param>
        /// <param name="image">Imagen de la imagen.</param>
        public Image(string name, Space level, float positionX, float positionY, float width, float height, string image)
        : base(name, level, positionX, positionY, width, height, image)
        {
        }
    }
}