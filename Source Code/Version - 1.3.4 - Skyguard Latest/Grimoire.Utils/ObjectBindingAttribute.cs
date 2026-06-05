using System;
using Grimoire.Tools;
using Newtonsoft.Json;
using PostSharp.Aspects;

namespace Grimoire.Utils
{
    [Serializable]
    public class ObjectBindingAttribute : LocationInterceptionAspect
    {
        private TypedValueProvider _defaultProvider = new DefaultTypedValueProvider();

        private bool _defaultProviderSet;

        public string[] Names { get; set; }

        public bool Get { get; set; } = true;

        public bool Set { get; set; } = true;

        public int GetIndex { get; set; }

        public Type ConvertType { get; set; }

        public object DefaultValue { get; set; }

        public string Select { get; set; }

        public string RequireNotNull { get; set; }

        public bool Static { get; set; }

        public Type DefaultProvider { get; set; }

        public ObjectBindingAttribute(params string[] names)
        {
            Names = names;
        }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            if (DefaultProvider != null && !_defaultProviderSet)
            {
                _defaultProvider = (TypedValueProvider)Activator.CreateInstance(DefaultProvider);
                _defaultProviderSet = true;
            }
            if (Get)
            {
                if (RequireNotNull == null || !IsNull(RequireNotNull))
                {
                    try
                    {
                        if (ConvertType == null)
                        {
                            ConvertType = args.Location.LocationType;
                        }
                        if (Select != null)
                        {
                            args.Value = JsonConvert.DeserializeObject(Flash.CallString("selectArrayObjects", Names[GetIndex], Select), ConvertType);
                        }
                        else
                        {
                            args.Value = JsonConvert.DeserializeObject(Flash.CallString("getGameObject" + (Static ? "S" : ""), Names[GetIndex]), ConvertType);
                        }
                        return;
                    }
                    catch
                    {
                        args.Value = DefaultValue ?? _defaultProvider.Provide(ConvertType);
                        return;
                    }
                }
                args.Value = DefaultValue ?? _defaultProvider.Provide(ConvertType);
            }
            else
            {
                base.OnGetValue(args);
            }
        }

        public override void OnSetValue(LocationInterceptionArgs args)
        {
            base.OnSetValue(args);
            if (Set)
            {
                string[] names = Names;
                foreach (string text in names)
                {
                    Flash.Call("setGameObject", text, args.Value);
                }
            }
        }

        public bool IsNull(string path)
        {
            return Flash.Call<bool>("isNull", new string[1] { path });
        }
    }
}