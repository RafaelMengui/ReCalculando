//--------------------------------------------------------------------------------
// <copyright file="EngineUnity.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using Proyecto.Common;
using Proyecto.Factory.Unity;
using Proyecto.Item;

namespace Proyecto.LibraryModelado.Engine
{
    /// <summary>
    /// Motor resposable de conocer y asignar los metodos relacionados a Unity.
    /// </summary>
    public class EngineUnity
    {
        /// <summary>
        /// Instancia de la UnityFactory.
        /// </summary>
        private UFactory unityFactory = Singleton<UFactory>.Instance;

        /// <summary>
        /// Instancia del motor general.
        /// </summary>
        private EngineGame engineGame = Singleton<EngineGame>.Instance;

        /// <summary>
        /// Un <see cref="IMainViewAdapter"/> que permite construir una interfaz de usuario interactiva.
        /// </summary>
        private IMainViewAdapter adapter;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EngineUnity()
        {
            this.Adapter = adapter;
        }

        /// <summary>
        /// Gets or sets del adaptador.
        /// </summary>
        /// <value>Adaptador del tipo <see cref="IMainViewAdapter"/>.</value>
        public IMainViewAdapter Adapter { get; set; }

        /// <summary>
        /// Metodo Drop de un draggableItem.
        /// </summary>
        /// <param name="draggableItemID"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void OnDrop(string draggableItemID, float x, float y)
        {
            IContainer destination;
            IDraggable draggableItem;
            this.Adapter.Debug($"Drop '{draggableItemID}' {x}@{y}");
            try
            {
                destination = this.FindDragContainer(x, y);
                draggableItem = this.FindItem(draggableItemID) as IDraggable;
            }
            catch(System.InvalidCastException)
            {
                throw new System.InvalidCastException($"Failed cast operation of \"{draggableItemID}\" as DraggableItem.");
            }

            if (destination != null && !destination.SavedItems.Contains(draggableItem as Items) && draggableItem.Drop(destination))
            {
                // Mueve el elemento arrastrado al destino si se suelta arriba del destino.
                // Se actualiza el container del item.
                draggableItem.Container.SavedItems.Remove(draggableItem as Items);
                destination.SavedItems.Add(draggableItem as Items);
                this.Adapter.Center(draggableItem.ID, destination.ID);
            }
            else
            {
                this.Adapter.Center(draggableItem.ID, draggableItem.Container.ID);
            }

            this.Adapter.MakeDraggable(draggableItem.ID, draggableItem.Draggable);
        }

        /// <summary>
        /// Si existe un container en esas coordenadas, lo devolvera.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private IContainer FindDragContainer(float x, float y)
        {
            foreach (Items item in this.engineGame.CurrentPage.ItemList)
            {
                if (item is IContainer && this.Adapter.Contains(item.ID, x, y))
                {
                    return item as IContainer;
                }
            }
            return null;
        }

        /// <summary>
        /// Metodo responsable de buscar en la pagina en la que se encuentre el usuario,
        /// un Item que tenga el mismo UnityID que el entrante por parametro.
        /// En este método utilizamos una expeción, la finalidad de esta es indicar que el programa
        /// no puede continuar ejecutando en su estado actual, y como tal, terminarlo. Para maneja
        /// la excepción y darle una adecuada solucion al programa para que este siga operando. En este caso,
        /// lanza una expecipon en caso que no encuentre ningún item que tenga el mismo UnityID.
        /// </summary>
        /// <param name="unityID"></param>
        /// <returns>Devuelve el item encontrado.</returns>
        private Items FindItem(string unityID)
        {
            foreach (Items item in this.engineGame.CurrentPage.ItemList)
            {
                if (unityID == item.ID)
                {
                    return item;
                }
            }
            throw new System.ArgumentNullException($"Item \"{unityID}\" not found.");
        }

        /// <summary>
        /// Metodo responsable de enviar un IComponent a la UnityFactory.
        /// </summary>
        /// <param name="component"></param>
        public void SendComponentToUFactory(IComponent component)
        {
            this.unityFactory.MakeUnityItem(this.Adapter, component);
        }

        /// <summary>
        /// Metodo responsable de actualizar el mensaje de feedback mostrado en pantalla.
        /// </summary>
        /// <param name="feedback"></param>
        public void UpdateFeedback(Feedback feedback)
        {
            this.Adapter.SetText(feedback.ID, feedback.Text);
        }

        /// <summary>
        /// Metodo que actualiza la imagen de un unity item.
        /// </summary>
        /// <param name="items">Item que se va cambiar la imagen.</param>
        /// <param name="image">string del nombre de la nueva imagen.</param>
        public void UpdateItemImage(Items items, string image)
        {
            this.Adapter.SetImage(items.ID, image);
        }
    }
}