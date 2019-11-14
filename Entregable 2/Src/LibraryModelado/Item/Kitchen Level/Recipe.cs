//--------------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using System.Collections.Generic;
using Proyecto.LibraryModelado;

namespace Proyecto.Item.KitchenLevel
{
    /// <summary>
    /// Clase responsable de crear recetas en el modelado.
    /// </summary>
    public class Recipe : IComponent
    {
        /// <summary>
        /// Initializes a new instance of Recipe.
        /// </summary>
        /// <param name="name">Nombre del Item.</param>
        /// <param name="level">Level que contiene la recipe.</param>
        /// <param name="foodList">Lista de alimentos.</param>
        public Recipe(string name, Space level, List<Food> foodList)
        {
            this.Name = name;
            this.FoodList = foodList;
        }

        private List<Food> foodList;

        /// <summary>
        /// Gets lista de objetos Food, que deberan ser soltados dentro del container para ganar el juego.
        /// </summary>
        /// <value>Alimentos<see cref="Food"/>.</value>
        public List<Food> FoodList { get; private set; }

        /// <summary>
        /// Gets y Sets del Nombre de la receta.
        /// </summary>
        /// <value>ID</value>
        public string Name { get; set; }
    }
}