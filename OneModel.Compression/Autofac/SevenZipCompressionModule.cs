using System;
using Autofac;
using OneModel.Compression.Compression;

namespace OneModel.Compression.Autofac
{
    public class SevenZipCompressionModule : Module
    {
        public string PathToSevenZip { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            if (string.IsNullOrEmpty(PathToSevenZip))
                throw new Exception("Path to 7zip executable not specified.");

            builder.RegisterType<SevenZipCompressor>()
                .WithParameter("pathToSevenZip", PathToSevenZip)
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}