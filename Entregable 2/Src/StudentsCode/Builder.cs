//--------------------------------------------------------------------------------
// <copyright file="Builder.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//--------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto.Common;
using Proyecto.Factory.CSharp;
using Proyecto.Factory.Unity;
using Proyecto.LeerHTML;
using Proyecto.LibraryModelado;
using Proyecto.LibraryModelado.Engine;
using Proyecto.Item;

namespace Proyecto.StudentsCode
{
    /// <summary>
    /// Clase que implementa la interfaz IBuilder.
    /// El patrón de diseño Builder separa la creación de un objeto complejo
    /// de su representación de modo que el mismo proceso de construcción pueda
    /// crear representaciones diferentes.
    /// En el caso de nuestro código lo utilizamos para generar los archivos 'StudentsCode.dll'
    /// y 'Common.dll'.
    /// </summary>
    public class Builder : IBuilder
    {
        /// <summary>
        /// Instancia del motor principal.
        /// </summary>
        /// <returns>.</returns>
        private EngineGame engineGame = Singleton<EngineGame>.Instance;

        /// <summary>
        /// Instancia del motor de unity.
        /// </summary>
        private EngineUnity engineUnity = Singleton<EngineUnity>.Instance;

        /// <summary>
        /// Adapter del tipo <see cref="IMainViewAdapter"/>.
        /// </summary>
        private IMainViewAdapter adapter;

        /// <summary>
        /// Pagina inicial que se mostrara al ejecutar el juego.
        /// </summary>
        private Space firstPage;

        /// <summary>
        /// Instancia del unico mundo del tipo <see cref="World"/>.
        /// </summary>
        private World world = Singleton<World>.Instance;

        /// <summary>
        /// Construye una interfaz de usuario interactiva utilizando un <see cref="IMainViewAdapter"/>.
        /// </summary>
        /// <param name="providedAdapter">Un <see cref="IMainViewAdapter"/> que permite construir una interfaz de usuario interactiva.</param>
        public void Build(IMainViewAdapter providedAdapter)
        {
            this.adapter = providedAdapter ?? throw new ArgumentNullException(nameof(providedAdapter));
            this.adapter.AfterBuild = this.Setup;
            this.engineUnity.Adapter = this.adapter;

            const string XMLfile = @"..\..\..\Code\Entregable 2\Src\ArchivosHTML\1920x1080.xml";
            List<Tag> tags = Parser.ParserHTML(ReadHTML.ReturnHTML(XMLfile));
            List<IComponent> componentList = new List<IComponent>();

            foreach (Tag tag in tags)
            {
                IComponent component = FactoryComponent.InitializeFactories().MakeComponent(tag);
                componentList.Add(component);
            }

            this.engineGame.AsociateLevelsWithEngines(componentList);

            foreach (IComponent component in componentList)
            {
                UFactory.InitializeUnityFactories().MakeUnityItem(this.adapter, component);
            }
        }

        /// <summary>
        /// Accion a realizar luego de Compilar el programa. Muestra la primera pagina en Unity.
        /// </summary>
        private void Setup()
        {
            // Layout del programa.
            this.adapter.ChangeLayout(Layout.ContentSizeFitter);

            // Se asigna busca la pagina principal y se asigna en EngineGame.
            this.firstPage = this.world.SpaceList[0];
            this.engineGame.MainPage = this.firstPage;

            // Se asigna la pagina principal como la pagina activa actual.
            this.engineGame.CurrentPage = this.firstPage;

            // Se muestra la primera pagina.
            this.adapter.ShowPage(this.firstPage.ID);
        }
    }
}

/*
static Method() => Setup de tests, despues se llama al metodo en cada caso.

1656x828
*/