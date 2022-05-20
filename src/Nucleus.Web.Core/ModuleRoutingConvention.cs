using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Nucleus.Web.Core;

public class ModuleRoutingConvention : IActionModelConvention
{
    private readonly IEnumerable<Module> _modules;

    public ModuleRoutingConvention(IEnumerable<Module> modules)
    {
        _modules = modules;
    }

    public void Apply(ActionModel action)
    {
        var module = _modules.FirstOrDefault(m => m.Assembly == action.Controller.ControllerType.Assembly);
        if (module == null)
        {
            return;
        }

        var moduleName = Path.GetFileNameWithoutExtension(module.Name);
        var modulePrefix = moduleName.Replace("Nucleus.Modules.", string.Empty);

        action.RouteValues.Add("module", modulePrefix);
    }
}