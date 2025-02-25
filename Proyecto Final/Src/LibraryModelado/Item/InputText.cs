//--------------------------------------------------------------------------------
// <copyright file="InputText.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using System;
using Proyecto.LibraryModelado;

namespace Proyecto.Item
{
    /// <summary>
    /// Clase responsable de crear InputText en el modelado.
    /// Hereda de la clase abstracta <see cref="Items"/>.
    /// </summary>
    public class InputText : Items
    {
        /// <summary>
        /// Método a ejecutar cuando se cambia el texto de la entrada de texto.
        /// </summary>
        private Action<string, string> onChange;

        /// <summary>
        /// Método a ejecutar cuando se termina de editar el texto de la entrada de texto.
        /// </summary>
        private Action<string, string> onEdited;

        /// <summary>
        /// Initializes a new instance of InputText.
        /// </summary>
        /// <param name="name">Nombre de la imagen.</param>
        /// <param name="level">Nivel al que pertence.</param>
        /// <param name="positionX">Posicion en eje horizontal en pixeles.</param>
        /// <param name="positionY">Posicion en eje vertical en pixeles.</param>
        /// <param name="width">Ancho en pixeles.</param>
        /// <param name="height">Altura en pixeles.</param>
        /// <param name="image">Imagen del input.</param>
        /// <param name="size">Tamaño del texto.</param>
        /// <param name="bold">Bool si el texto es en bold.</param>
        /// <param name="italic">Bool si el texto es en italic.</param>
        public InputText(string name, Space level, float positionX, float positionY, float width, float height, string image, int size, bool bold, bool italic)
        : base(name, level, positionX, positionY, width, height, image)
        {
            this.CurrentText = string.Empty;
            this.Size = size;
            this.Bold = bold;
            this.Italic = italic;
        }

        /// <summary>
        /// Gets or sets del texto que se encuentra actualmente en el input text.
        /// </summary>
        /// <value></value>
        public string CurrentText { get; set; }

        /// <summary>
        /// Gets or sets del Tamaño del texto.
        /// </summary>
        /// <value>Int.</value>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets indicating whether el texto va en bold.
        /// </summary>
        /// <value>Bool.</value>
        public bool Bold { get; set; }

        /// <summary>
        /// Gets or sets indicating whether el texto va en italics.
        /// </summary>
        /// <value>Bool.</value>
        public bool Italic { get; set; }

        /// <summary>
        /// Action a ejecutar cuando se termine de editar un texto.
        /// </summary>
        /// <param name="itemID">Id del objeto.</param>
        /// <param name="text">Nuevo texto.</param>
        public void Edit(string itemID, string text)
        {
            this.CurrentText = text;
        }

        /// <summary>
        /// Metodo a ejecutar cuando se vaya cambiando un texto.
        /// No contiene funcionalidad debido a que no es necesario.
        /// </summary>
        /// <param name="itemID">Id del objeto.</param>
        /// <param name="text">Texto del objeto.</param>
        public void Change(string itemID, string text)
        {
        }
    }
}