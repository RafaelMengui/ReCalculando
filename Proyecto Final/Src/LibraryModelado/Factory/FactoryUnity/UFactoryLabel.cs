//--------------------------------------------------------------------------------
// <copyright file="UFactoryLabel.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using Proyecto.Common;
using Proyecto.Item;
using Proyecto.LibraryModelado;

namespace Proyecto.Factory.Unity
{
    /// <summary>
    /// Esta clase es la resposable de agregar los componentes Label al juego.
    /// Hereda de la Clase abstracta <see cref="FactoryUnity"/>.
    /// </summary>
    public class UFactoryLabel : FactoryUnity
    {
        /// <summary>
        /// Objeto Label que se agregara a Unity.
        /// </summary>
        private Label label;

        /// <summary>
        /// Sobrescribe el metodo abstracto de FactoryUnity.
        /// Tiene la responsabilidad de agregar el componente de tipo <see cref="Label"/> a Unity.
        /// </summary>
        /// <param name="adapter">Adapter <see cref="IMainViewAdapter"/>.</param>
        /// <param name="component">Componente que se agregara a Unity <see cref="IComponent"/>.</param>
        public override void MakeUnityItem(IMainViewAdapter adapter, IComponent component)
        {
            try
            {
                // Se castea el componente como Label.
                this.label = component as Label;
            }
            catch (System.Exception)
            {
                throw new System.Exception("Fail to cast component as Label");
            }

            // Se crea el objeto en unity y se obtiene el UnityID.
            this.label.ID = adapter.CreateLabel(this.label.PositionX, this.label.PositionY, this.label.Width, this.label.Height);

            adapter.SetFont(this.label.ID, this.label.Bold, this.label.Italic, this.label.Size);

            adapter.SetText(this.label.ID, this.label.Text, true);
        }
    }
}