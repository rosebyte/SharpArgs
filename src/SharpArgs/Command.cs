using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RoseByte.SharpArgs.Parsers;

namespace RoseByte.SharpArgs
{
    public class Command<T> : Command
    {
        protected sealed override void Execute() => Execute(default);
        protected sealed override Task ExecuteAsync() => ExecuteAsync(default);

        protected virtual void Execute(T context) { }
        protected virtual Task ExecuteAsync(T context) => Task.CompletedTask;

        public Command() { }
        public Command(params Command[] commands) : base(commands) { }
        
        protected bool TryDelegate(T context, string[] args)
        {
            var subcommand = Commands?
                .Where(x => x.Name != null)
                .FirstOrDefault(x => string.Equals(x.Name, args?.FirstOrDefault(), StringComparison.OrdinalIgnoreCase));

            if (subcommand == null)
            {
                return false;
            }

            if (subcommand is Command<T> sc)
            {
                sc.Run(context, args?.Skip(1).ToArray());
            }
            else
            {
                subcommand.Run(args?.Skip(1).ToArray());
            }
            
            return true;
        }
        
        public void Run(T context, params string[] args) => Run(context, Parser.Unix, args);
        
        public void Run(T context, IArgumentsParser parser, params string[] args)
        {
            parser = parser ?? Parser.Unix;
            
            if (!TryDelegate(context, args))
            {
                parser.ParseArgs(args, Arguments);
                Validate();
                Execute(context);
            }
        }
        
        public Task RunAsync(T context, params string[] args) => RunAsync(context, Parser.Unix, args);
        
        public async Task RunAsync(T context, IArgumentsParser parser, params string[] args)
        {
            parser = parser ?? Parser.Unix;
            
            if (!TryDelegate(context, args))
            {
                parser.ParseArgs(args, Arguments);
                Validate();
                await ExecuteAsync(context);
            }
        }
    }
    
    public class Command
    {
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
        
        public void Run(IArgumentsParser parser, params string[] args)
        {
            parser = parser ?? Parser.Unix;
            
            if (!TryDelegate(args))
            {
                parser.ParseArgs(args, Arguments);
                Validate();
                Execute();
            }
        }
        
        public void Run(params string[] args)
        {
            Run(Parser.Unix, args);
        }
        
        public async Task RunAsync(IArgumentsParser parser, params string[] args)
        {
            parser = parser ?? Parser.Unix;
            
            if (!TryDelegate(args))
            {
                parser.ParseArgs(args, Arguments);
                Validate();
                await ExecuteAsync();
            }
        }
        
        public Task RunAsync(params string[] args)
        {
            return RunAsync(Parser.Unix, args);
        }

        protected virtual bool TryDelegate(string[] args)
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