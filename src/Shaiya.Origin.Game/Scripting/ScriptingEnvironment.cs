using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;

namespace Shaiya.Origin.Game.Scripting
{
    public static class ScriptingEnvironment
    {
        private static ScriptEngine engine = Python.CreateEngine();
        private static ScriptScope scope;

        public static void Init()
        {
            scope = engine.CreateScope();

            dynamic clr = engine.Runtime.GetClrModule();
            clr.AddReferenceToFileAndPath(Directory.GetCurrentDirectory() + "/Shaiya.Origin.Game.dll");

            scope.SetVariable("CommandDispatcher", GameService.GetCommandDispatcher());

            LoadAllScripts("/Scripts/");
        }

        private static void LoadScript(string path)
        {
            ScriptSource source = engine.CreateScriptSourceFromFile(path);

            source.Execute(scope);
        }

        private static void LoadAllScripts(string basePath)
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + basePath, "*.py", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                LoadScript(file);
            }
        }
    }
}