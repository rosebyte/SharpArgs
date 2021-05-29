using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoseByte.SharpArgs.Internal;
using RoseByte.SharpArgs.Internal.Parser;

namespace RoseByte.SharpArgs
{
    public class Command
    {
        public virtual IArgumentsParser Parser { get; set; } = UnixArgumentsParser.Instance;

        public virtual string Name => null;
        public virtual IEnumerable<Command> Commands { get; }
        private List<Argument> _arguments;

        protected List<Argument> Arguments
        {
            get
            {
                if (_arguments == null)
                {
                    _arguments = new List<Argument>();
                    var properties = GetType()
                        .GetProperties()
                        .Where(x => typeof(Argument).IsAssignableFrom(x.PropertyType))
                        .Select(prop =>
                        {
                            if (prop.GetValue(this) is Argument current)
                            {
                                return current;
                            }

                            var instance = Activator.CreateInstance(prop.PropertyType);
                            prop.SetValue(this, instance);
                            return (Argument) instance;
                        });
                    _arguments.AddRange(properties);
                    var fields = GetType()
                        .GetFields()
                        .Where(x => typeof(Argument).IsAssignableFrom(x.FieldType))
                        .Select(field =>
                        {
                            if (field.GetValue(this) is Argument current)
                            {
                                return current;
                            }

                            var instance = Activator.CreateInstance(field.FieldType);
                            field.SetValue(this, instance);
                            return (Argument) instance;
                        });
                    _arguments.AddRange(fields);
                }

                return _arguments;
            }
        }
        
        protected virtual void Execute() { }
        protected virtual Task ExecuteAsync()
        { 
            return Task.CompletedTask;
        }

        protected virtual void Validate() { }

        public void Run(params string[] args)
        {
            if (!TryDelegate(args))
            {
                Parser.ParseArgs(args, Arguments);
                Validate();
                Execute();
            }
        }
        
        public async Task RunAsync(params string[] args)
        {
            if (!TryDelegate(args))
            {
                Parser.ParseArgs(args, Arguments);
                Validate();
                await ExecuteAsync();
            }
        }

        private bool TryDelegate(string[] args)
        {
            var subcommand = Commands?
                .Where(x => x.Name != null)
                .FirstOrDefault(x => string.Equals(x.Name, args?.FirstOrDefault(), StringComparison.OrdinalIgnoreCase));

            if (subcommand == null)
            {
                return false;
            }
            
            subcommand.Run(args?.Skip(1).ToArray());
            return true;
        }

        public Command() { }
        
        public Command(params Command[] commands)
        {
            Commands = commands;
        }
    }
}