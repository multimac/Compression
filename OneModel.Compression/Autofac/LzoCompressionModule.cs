using System;
using Autofac;
using OneModel.Compression.Compression;

namespace OneModel.Compression.Autofac
{
    public class LzoCompressionModule : Module
    {
        public string PathToLzop { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            if(string.IsNullOrEmpty(PathToLzop))
                throw new Exception("Path to Lzop executable not specified.");

            builder.RegisterType<LzoCompressor>()
                .WithParameter("pathToLzop", PathToLzop)
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}