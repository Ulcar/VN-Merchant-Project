using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Naninovel;
using UniRx.Async;
using UnityEngine;


[EditInProjectSettings]
public class InventoryConfiguration:Configuration
    {

    public const string DefaultPathPrefix = "Inventory";

    [Tooltip("Configuration of the resource loader used with inventory resources.")]
    public ResourceLoaderConfiguration Loader = new ResourceLoaderConfiguration { PathPrefix = DefaultPathPrefix };

}
