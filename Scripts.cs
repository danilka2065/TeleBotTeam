using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace TBParser
{
    class ChatParser
    {
        public static void membersParser()
        {
            ScriptEngine engine = Python.CreateEngine();
            engine.ExecuteFile("MembersParser.py");
        }
    }
}
//