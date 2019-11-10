//--------------------------------------------------------------------------------
// <copyright file="IComponent.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
namespace Proyecto.LibraryModelado
{
    /// <summary>
    /// Interfaz IComponent.
    /// Todos los objetos creados en el modelado(World, Space, Items) seran de tipo IComponent.
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// Gets or sets del ID de un objeto asignado al crearlo en unity.
        /// </summary>
        /// <value></value>
        string ID { get; set; }

        /// <summary>
        /// Gets or sets que indican si el item esta actualmente activo en pantalla.
        /// Por predeterminado para todos los items, el valor sera true.
        /// </summary>
        /// <value></value>
        bool IsActive { get; set; }
    }
}