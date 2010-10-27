﻿using System.Linq;
using System.Xml;
using Xunit;
using Xunit.Sdk;

namespace CQRSlite.Extentions
{
    public class ThenAttribute : FactAttribute
    {
        protected override System.Collections.Generic.IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            return base.EnumerateTestCommands(method).Select(command => new ThenCommand(command));
        }
    }
    public class ThenCommand : ITestCommand
    {
        private readonly ITestCommand _innerCommand;

        public ThenCommand(ITestCommand innerCommand)
        {
            _innerCommand = innerCommand;
        }

        public MethodResult Execute(dynamic testClass)
        {
            testClass.Run();
            return _innerCommand.Execute(testClass);
        }

        public XmlNode ToStartXml()
        {
            return _innerCommand.ToStartXml();
        }

        public string DisplayName
        {
            get { return _innerCommand.DisplayName; }
        }

        public bool ShouldCreateInstance
        {
            get { return _innerCommand.ShouldCreateInstance; }
        }

        public int Timeout
        {
            get { return _innerCommand.Timeout; }
        }
    }
}
